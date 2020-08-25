using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Microsoft.Win32;
using sysproxy.Utils;
using sysproxy.Views;
namespace sysproxy
{
    static class Program
    {
        private static MenuView menuView;


        // XXX: Don't change this name
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Check OS since we are using dual-mode socket
            if (!RuntimeUtil.IsWinVistaOrHigher())
            {
                MsgBox.Error("Unsupported operating system,use Windows Vista at least");
                return;
            }
            // Check .NET Framework version
            if (!RuntimeUtil.GetVersionFromRegistry())
            {
                if(DialogResult.OK == MsgBox.ErrorRes("Unsupported .NET Framework,require 3.5 version"))
                {
                    Process.Start("https://dotnet.microsoft.com/download/dotnet-framework/net35-sp1");
                }
                return;
            }
            //Check explore.exe is running,so we can read correct setting file


            //Check running sysproxy
            using (Mutex mutex = new Mutex(false, "Global\\sysproxy" + Application.StartupPath.GetHashCode()))
            {
                SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
                if (!mutex.WaitOne(0, false))
                {
                    Process[] oldProcesses = Process.GetProcessesByName("sysproxy");
                    if (oldProcesses.Length > 0)
                    {
                        MsgBox.Show(I18N.GetString("Find sysproxy icon in your notify tray,which is usually on bottom right corner"));
                        return;
                    }
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                menuView = new MenuView();
                Application.Run();
            }
          
        }

        /// <summary>
        /// Sleep Detection by PowerModeChanged
        /// </summary>
        private static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    System.Timers.Timer timer = new System.Timers.Timer(5 * 1000);
                    timer.Elapsed += Timer_Elapsed;
                    timer.AutoReset = false;
                    timer.Enabled = true;
                    timer.Start();
                    break;
                case PowerModes.Suspend:
                    menuView?.StopService();
                    break;
            }
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                menuView?.StartService();
            }
            catch (Exception)
            {
                //TODO
            }
            finally
            {
                try
                {
                    System.Timers.Timer timer = (System.Timers.Timer)sender;
                    timer.Enabled = false;
                    timer.Stop();
                    timer.Dispose();
                }
                catch (Exception)
                {
                    //TODO
                }
            }
        }
    }
}
