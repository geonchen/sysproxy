using sysproxy.Models;
using sysproxy.Presenters;
using sysproxy.Utils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace sysproxy.Services
{
    public class RemoteClient : IService
    {


        private Process process = null;
        private string fullPath;
        private bool hide;
        private string args;
        public IntPtr handler;



        public event EventHandler ShowBalloonTip;
        public event EventHandler AddProcess;

        public RemoteClient(Setting setting)
        {
            fullPath = setting.RemoteClientPath;
            args = setting.RemoteClientArgs;
            hide = setting.HideClient;
        }

        public void Start()
        {
            if (string.IsNullOrEmpty(fullPath))
                return;
            if (process == null)
            {
                string fileName = Path.GetFileName(fullPath);
                Process[] existingProcess = Process.GetProcessesByName(fileName);
                foreach (Process p in existingProcess.Where(IsChildProcess))
                {
                    KillProcess(p);
                }
                process = new Process();

                process.StartInfo.FileName = fullPath;
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(fullPath);
                if(!args.IsNullOrEmpty())
                    process.StartInfo.Arguments = @args;
                //Double click exe,bat,cmd...
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = hide;
                process.Start();
                /*
                * Add this process to job obj associated with this process handler, so that
                * when sysproxy exit unexpectedly, this process will be forced killed by system.
                */
                handler = process.Handle;
                AddProcess?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Refresh(Setting setting)
        {
            if (fullPath == setting.RemoteClientPath && args == setting.RemoteClientArgs && hide == setting.HideClient)
                return;
            fullPath = setting.RemoteClientPath;
            args = setting.RemoteClientArgs;
            hide = setting.HideClient;
        }

    

        private bool IsChildProcess(Process p)
        {
            try
            {
                var cmd = p.GetCommandLine();

                return cmd.Contains(p.StartInfo.FileName);
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

        private  void KillProcess(Process p)
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
                //Try to kill a closed process
            }
        }

  
   

    }
}
