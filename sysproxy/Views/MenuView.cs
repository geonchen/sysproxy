using System;
using System.Drawing;
using System.Windows.Forms;
using sysproxy.Properties;
using sysproxy.Utils;
using sysproxy.Presenters;
using System.Diagnostics;

namespace sysproxy.Views
{
  
    public class MenuView : IMenuView
    {
        private MenuPresenter pressenter;

        private Bitmap icon_baseBitmap;
        private Icon icon;

        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;
        private MenuItem disableItem;
        private MenuItem globalItem;
        private MenuItem pacItem;

        public bool DisableToggle 
        { 
            get { return disableItem.Checked; }
            set { disableItem.Checked = value; }
        }
        public bool GlobalToggle
        {
            get { return globalItem.Checked; }
            set { globalItem.Checked = value; }
        }
        public bool PACToggle
        {
            get { return pacItem.Checked; }
            set { pacItem.Checked = value; }
        }

        public event EventHandler LoadData;
        public event EventHandler SaveData;
        public event EventHandler UpdateGFWList_Click;
        public event EventHandler EditUserRule_Click;
        public event EventHandler LoadSettingForm;

        public MenuView()
        {
            LoadMenu();
            DrawIcon();
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = icon;
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.Visible = true;
            pressenter = new MenuPresenter(this);
            LoadData?.Invoke(this, EventArgs.Empty);
        }


        private void DrawIcon()
        {
            int dpi;
            Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            dpi = (int)graphics.DpiX;
            graphics.Dispose();
            icon_baseBitmap = null;
            if (dpi < 97)
            {
                // dpi = 96;
                icon_baseBitmap = Resources.ss16;
            }
            else if (dpi < 121)
            {
                // dpi = 120;
                icon_baseBitmap = Resources.ss20;
            }
            else
            {
                icon_baseBitmap = Resources.ss24;
            }
            icon = Icon.FromHandle(icon_baseBitmap.GetHicon());
        }




        private void LoadMenu()
        {
            contextMenu = new ContextMenu(new MenuItem[]
            {
                CreateMenuGroup("ProxyMode",new MenuItem[]{
                   disableItem = CreateMenuItem ("Disable",new EventHandler(DisableItem_Click)),
                   globalItem = CreateMenuItem ("Global",new EventHandler(GlobalItem_Click)),
                   pacItem = CreateMenuItem ("PAC",new EventHandler(PACItem_Click))
                }),
                CreateMenuGroup("PACFile",new MenuItem[]
                {
                    CreateMenuItem ("Update PAC from gfwlist",new EventHandler(UpdateGFWListItem_Click)),
                    CreateMenuItem ("Edit user rule",new EventHandler(EditUserRuleItem_Click))
                }),
                CreateMenuItem ("Setting",new EventHandler(SettingItem_Click)),
                CreateMenuItem ("About",new EventHandler(AboutItem_Click)),
                CreateMenuItem ("Quit",new EventHandler(QuitItem_Click))
            });
        }

        public void LoadChildForm(Type childFormType)
        {
            var constructors = childFormType.GetConstructors();
            var destinationView = constructors[0].Invoke(new object[] { }) as Form;
            destinationView.ShowDialog();
        }
        private MenuItem CreateMenuGroup(string text, MenuItem[] items)
        {
            return new MenuItem(I18N.GetString(text), items);
        }
        private MenuItem CreateMenuItem(string text, EventHandler click)
        {
            return new MenuItem(I18N.GetString(text), click);
        }

        private void DisableItem_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(disableItem, EventArgs.Empty);
        }
        private void GlobalItem_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(globalItem, EventArgs.Empty);
        }
        private void PACItem_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(pacItem, EventArgs.Empty);
        }


        public void ShowBalloonTip(int icon_index,string message, int timeout)
        {
            //index=0:None&index=1:Info&index=2:Warning&index=3:Error
            notifyIcon.BalloonTipTitle = "sysproxy";
            notifyIcon.BalloonTipIcon = (ToolTipIcon)icon_index;
            notifyIcon.BalloonTipText = I18N.GetString(message);
            notifyIcon.ShowBalloonTip(timeout);
        }
        private void UpdateGFWListItem_Click(object sender, EventArgs e)
        {
            UpdateGFWList_Click?.Invoke(this, EventArgs.Empty);
        }
        private void EditUserRuleItem_Click(object sender, EventArgs e)
        {
            EditUserRule_Click?.Invoke(this, EventArgs.Empty);
        }
        private void SettingItem_Click(object sender, EventArgs e)
        {
            LoadSettingForm?.Invoke(this, EventArgs.Empty);
        }

        private void AboutItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/keyonchen/sysproxy");
        }
        private void QuitItem_Click(object sender, EventArgs e)
        {
            pressenter.Stop();
            notifyIcon.Visible = false;
            Application.Exit();
        }

       
        public void StartService()
        {
            pressenter.Start();
        }

        public void StopService()
        {
            pressenter?.Stop();
        }
    }
}
