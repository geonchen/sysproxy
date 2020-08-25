using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using sysproxy.Properties;
using sysproxy.Utils;
using System.IO;
using System.ComponentModel;
using sysproxy.Models;

namespace sysproxy.Services
{
    public class Privoxy:IService
    {

        private const string folder = "privoxy";
        private const string mgwz_dll = "mgwz.dll";
        private const string privoxy_exe = "ss_privoxy.exe";
        private const string config_txt = "config.txt";
        private string directory = string.Empty;


        private Process process = null;
        private static Job job;
        private int proxyPort;
        private int delayPort;
        private bool shareLan;

        static Privoxy()
        {
            job = new Job();
        }
        public Privoxy(Setting setting)
        {
            proxyPort = setting.proxyPort;
            delayPort = setting.forwardPort;
            shareLan = setting.shareLan;
            try
            {
                directory = FileUtil.GetPath(folder);
                Directory.CreateDirectory(directory);
                FileUtil.UnGzip(Path.Combine(directory,mgwz_dll), Resources.mgwz_dll);
                FileUtil.UnGzip(Path.Combine(directory, privoxy_exe), Resources.privoxy_exe);
                UpdateConfig();
            }
            catch (IOException)
            {
                MsgBox.Error("Can't find privoxy file");
            }
        }

        public void Start()
        {
            if(process == null)
            {
                Process[] existingPrivoxy = Process.GetProcessesByName("ss_privoxy");
                foreach (Process p in existingPrivoxy.Where(IsChildProcess))
                {
                    KillProcess(p);
                }
                process = new Process();
                process.StartInfo.FileName = "ss_privoxy.exe";
                process.StartInfo.Arguments = Path.Combine(directory, config_txt);
                process.StartInfo.WorkingDirectory = directory;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                /*
                * Add this process to job obj associated with this ss process, so that
                * when ss exit unexpectedly, this process will be forced killed by system.
                */
                job.AddProcess(process.Handle);
            }
        }

        public void Refresh(Setting setting)
        {
            if (shareLan == setting.shareLan & proxyPort == setting.proxyPort & delayPort == setting.forwardPort)
                return;
            shareLan = setting.shareLan;
            proxyPort = setting.proxyPort;
            delayPort = setting.forwardPort;
            UpdateConfig();
        }

        private void UpdateConfig()
        {
            string config = Resources.config;
            config = config.Replace("__Local_IP__", shareLan ? "0.0.0.0" : "127.0.0.1");
            config = config.Replace("__Proxy_PORT__", proxyPort.ToString());
            config = config.Replace("__Delay_PORT__", delayPort.ToString());
            FileUtil.ByteArrayToFile(Path.Combine(directory, config_txt), Encoding.UTF8.GetBytes(config));
        }

        private bool IsChildProcess(Process p)
        {
            try
            {
                var cmd = p.GetCommandLine();

                return cmd.Contains(privoxy_exe);
            }
            catch (Win32Exception ex)
            {
                if ((uint)ex.ErrorCode != 0x80004005)
                {
                    throw;
                }
            }
            return false;
        }




        public void Stop()
        {
            if (process != null)
            {
                KillProcess(process);
                process = null;
            }
        }

        private static void KillProcess(Process p)
        {
            try
            {
                p.CloseMainWindow();
                p.WaitForExit(100);
                if (!p.HasExited)
                {
                    p.Kill();
                    p.WaitForExit();
                }
            }
            catch (Exception)
            {
                MsgBox.Warning("Kill privoxy process fail");
            }
        }

    }
}
