using System;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using sysproxy.Models;
using sysproxy.Services;
using sysproxy.Views;
using sysproxy.Utils;

namespace sysproxy.Presenters
{
    #region Tip argument
    public class BalloonTip : EventArgs
    {
        public int IconIndex { get; set; }

        public string Message { get; set; }

        public int timeout { get; set; }
    }

    #endregion
    public class MenuPresenter : Presenter<IMenuView>
    {

        private PACServer pacServer;
        private RemoteClient remoteClient;
        private static Job job;

        public MenuPresenter(IMenuView view) : base(view)
        {
            View.UpdateGFWList_Click += UpdateGFWList;
            View.EditUserRule_Click += View_EditUserRule;
            View.LoadSettingForm += View_LoadSettingForm;
        }

       

        protected override void View_LoadData(object sender, EventArgs e)
        {
            base.View_LoadData(sender, e);
            Setting.Instance.Saved += Setting_Saved;
            if(job == null)
            {
                job = new Job();
            }
            if (pacServer == null)
            {
                pacServer = new PACServer(Setting.Instance);
                pacServer.ShowBalloonTip += Menu_ShowBalloonTip;
                pacServer.UserRuleChanged += PACServer_UserRuleChanged;
            }
            if(remoteClient == null)
            {
                remoteClient = new RemoteClient(Setting.Instance);
                remoteClient.ShowBalloonTip += Menu_ShowBalloonTip;
                remoteClient.AddProcess += RemoteClient_AddProcess;
            }
            Start();
        }

        /// <summary>
        /// Invoke this event when toggle system proxy mode
        /// </summary>
        protected override void View_SaveData(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ProxyMode index = (ProxyMode)menuItem.Index;
            if (Setting.Instance.Mode == index)
                return;
           Setting.Instance.Mode = index;
           Setting.Instance.Save();
        }

        private void Menu_ShowBalloonTip(object sender, EventArgs e)
        {
            BalloonTip tip = e as BalloonTip;
            View.ShowBalloonTip(tip.IconIndex, tip.Message, tip.timeout);
        }

        public void Start()
        {
            switch (Setting.Instance.Mode)
            {
                case ProxyMode.Disable:
                    Stop();
                    View.DisableToggle = true;
                    View.GlobalToggle = false;
                    View.PACToggle = false;
                    break;
                case ProxyMode.Global:
                    Stop();
                    remoteClient?.Start();
                    SystemProxy.EnableGlobal(Setting.Instance.RemotePort);
                    View.DisableToggle = false;
                    View.GlobalToggle = true;
                    View.PACToggle = false;
                    break;
                case ProxyMode.PAC:
                    Stop();
                    pacServer?.Start();
                    remoteClient.Start();
                    SystemProxy.EnablePAC(pacServer.pac_url);
                    View.DisableToggle = false;
                    View.GlobalToggle = false;
                    View.PACToggle = true;
                    break;
            }
        }

        /// <summary>
        /// Can't read the correct setting file when operating system reboot.
        /// It need to read setting file again when operating system wake up.
        /// </summary>
     

        /// <summary>
        /// Invoke this event when setting file save finish.
        /// </summary>
        private void Setting_Saved(object sender, EventArgs e)
        {
            if (pacServer != null)
            {
                pacServer.Refresh(Setting.Instance);
            }
            if (remoteClient != null)
            {
                remoteClient.Refresh(Setting.Instance);
            }
            Start();
        }

        private void UpdateGFWList(object sender, EventArgs e)
        {
            if (pacServer != null)
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    pacServer.UpdateGFWList(Setting.Instance.Mode==ProxyMode.Disable);
                }));
                thread.Start();
            }
        }

        private void View_EditUserRule(object sender, EventArgs e)
        {
            string path = pacServer.GetUserRuleFilePath();
            Process.Start("notepad.exe", path);
        }

        private void PACServer_UserRuleChanged(object sender, EventArgs e)
        {
            if (Setting.Instance.Mode != ProxyMode.PAC)
                return;
            SystemProxy.Disable(); //Reset System Proxy
            Start();
        }

        private void RemoteClient_AddProcess(object sender, EventArgs e)
        {
            if (remoteClient == null ||remoteClient.handler == IntPtr.Zero)
                return;
            job.AddProcess(remoteClient.handler);
        }

        private void View_LoadSettingForm(object sender, EventArgs e)
        {
            Type settingForm = GetViewTypeFromInterface(typeof(ISettingView));
            View.LoadChildForm(settingForm);
        }

        private Type GetViewTypeFromInterface(Type type)
        {

            Type viewType = null;

            var assembly = Assembly.GetExecutingAssembly();

            foreach (var exportedType in assembly.GetExportedTypes().Where(t => t.IsSubclassOf(typeof(Control))))
            {
                if (type.IsAssignableFrom(exportedType))
                {
                    viewType = exportedType;
                    break;
                }
            }
            return viewType;
        }

   


        public void Stop()
        {
            pacServer?.Stop();
            remoteClient?.Stop();
            SystemProxy.Disable();
        }

    }

    
 



}
