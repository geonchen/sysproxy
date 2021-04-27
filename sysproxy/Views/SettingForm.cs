using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sysproxy.Presenters;
using sysproxy.Utils;

namespace sysproxy.Views
{
    public partial class SettingForm : Form,ISettingView
    {
        private SettingPresenter presenter;

        public string LocalPort_Text {
            get { return LocalPort_TB.Text; }
            set { LocalPort_TB.Text = value; }
        }
        public string RemotePort_Text
        {
            get { return RemotePort_TB.Text; }
            set { RemotePort_TB.Text = value; }
        }
        private string remoteClientPath;
        public string RemoteClientPath {
            get{ return remoteClientPath; }
            set 
            {
                remoteClientPath = value;
                if (string.IsNullOrEmpty(remoteClientPath))
                    OpenFile_Button.Text = I18N.GetString("OpenFile");
                else
                    OpenFile_Button.Text = Path.GetFileNameWithoutExtension(remoteClientPath);
            }
        }

        public string RemoteClientArgs
        {
            get { return RemoteClientArgs_TB.Text; }
            set { RemoteClientArgs_TB.Text = value; }
        }
        public bool HideClient_Toggle
        {
            get { return HideClient_Item.Checked; }
            set { HideClient_Item.Checked = value; }
        }

        public bool AutoStart_Toggle
        {
            get { return AutoStart_Item.Checked; }
            set { AutoStart_Item.Checked = value; }
        }

       

        public event EventHandler LoadData;
        public event EventHandler SaveData;

        public SettingForm()
        {
            presenter = new SettingPresenter(this);
            presenter.CloseForm += CloseForm;
            InitializeComponent();
            Localization();
        }

       

        private void Localization()
        {
            Text = I18N.GetString("Setting");
            LocalPort_LB.Text = I18N.GetString("LocalPort");
            RemotePort_LB.Text = I18N.GetString("RemotePort");
            RemoteClient_LB.Text = I18N.GetString("RemoteClient");
            RemoteClientArgs_LB.Text = I18N.GetString("RemoteClientArgs");
            OpenFile_Button.Text = I18N.GetString("OpenFile");
            AutoStart_Item.Text = I18N.GetString("AutoStart");
            HideClient_Item.Text = I18N.GetString("HideClient");
            Save_Button.Text = I18N.GetString("Save");
            Reset_Button.Text = I18N.GetString("Reset");
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData?.Invoke(this, EventArgs.Empty);
        }

        private void OpenFile_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
            fileDialog.FilterIndex = 1;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                RemoteClientPath = fileDialog.FileName;
            };
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(this, EventArgs.Empty);
        }

        private void Reset_Button_Click(object sender, EventArgs e)
        {
            LocalPort_TB.Text = "8848";
            RemotePort_TB.Text = "8080";
            RemoteClientPath = null;
            RemoteClientArgs_TB.Text = null;
            AutoStart_Item.Checked = false;
            HideClient_Item.Checked = false;
        }

        private void CloseForm(object sender, EventArgs e)
        {
            Close();
        }
    }
}
