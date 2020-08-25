using sysproxy.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sysproxy.Views
{
   
    public interface IMenuView : IView
    {
        bool DisableToggle { get; set; }
        bool GlobalToggle { get; set; }
        bool PACToggle { get; set; }
        void LoadChildForm(Type childFormType);
        event EventHandler LoadSettingForm;
        void ShowBalloonTip(int icon_index,string message,int timeout);
        event EventHandler EditUserRule_Click;
        event EventHandler UpdateGFWList_Click;


    }
}
