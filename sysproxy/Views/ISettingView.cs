using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sysproxy.Views
{
    public interface ISettingView : IView
    {
        string LocalPort_Text { get; set; }
        string RemotePort_Text { get; set; }
        string RemoteClientPath { get; set; }
        string RemoteClientArgs { get; set; }
        bool HideClient_Toggle { get; set; }
        bool AutoStart_Toggle { get; set; }
        

    }
}
