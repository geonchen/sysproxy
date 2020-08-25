using System;
using sysproxy.Views;
using sysproxy.Models;
using sysproxy.Utils;
using sysproxy.Services;

namespace sysproxy.Presenters
{
    public class SettingPresenter : Presenter<ISettingView>
    {
        public event EventHandler CloseForm;
        public SettingPresenter(ISettingView view) : base(view)
        {
        }

        protected override void View_LoadData(object sender, EventArgs e)
        {

            base.View_LoadData(sender, e);
            View.LocalPort_Text = Setting.Instance.LocalPort.ToString();
            View.RemotePort_Text = Setting.Instance.RemotePort.ToString();
            View.RemoteClientPath = Setting.Instance.RemoteClientPath;
            View.RemoteClientArgs = Setting.Instance.RemoteClientArgs;
            View.HideClient_Toggle = Setting.Instance.HideClient;
            View.AutoStart_Toggle = AutoStart.Check();

        }
        
        protected override void View_SaveData(object sender, EventArgs e)
        {
            base.View_SaveData(sender, e);
            try
            {
                Setting.Instance.LocalPort = int.Parse(View.LocalPort_Text);
                Setting.Instance.RemotePort = int.Parse(View.RemotePort_Text);
                Setting.Instance.RemoteClientPath = View.RemoteClientPath;
                Setting.Instance.RemoteClientArgs = View.RemoteClientArgs;
                Setting.Instance.HideClient = View.HideClient_Toggle;
                AutoStart.Set(View.AutoStart_Toggle);
                Setting.Instance.Save();
                CloseForm?.Invoke(this, EventArgs.Empty);
            }
            catch (FormatException)
            {
                MsgBox.Error("Illegal port number format");
                return;
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
                return;
            }
        }
    }
}
