namespace sysproxy.Views
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Save_Button = new System.Windows.Forms.Button();
            this.Reset_Button = new System.Windows.Forms.Button();
            this.RemotePort_TB = new System.Windows.Forms.TextBox();
            this.RemoteClient_LB = new System.Windows.Forms.Label();
            this.RemoteClientArgs_TB = new System.Windows.Forms.TextBox();
            this.LocalPort_LB = new System.Windows.Forms.Label();
            this.OpenFile_Button = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.AutoStart_Item = new System.Windows.Forms.CheckBox();
            this.RemoteClientArgs_LB = new System.Windows.Forms.Label();
            this.RemotePort_LB = new System.Windows.Forms.Label();
            this.LocalPort_TB = new System.Windows.Forms.TextBox();
            this.HideClient_Item = new System.Windows.Forms.CheckBox();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Save_Button
            // 
            this.Save_Button.Location = new System.Drawing.Point(125, 155);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(75, 23);
            this.Save_Button.TabIndex = 0;
            this.Save_Button.Text = "Save";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Reset_Button
            // 
            this.Reset_Button.Location = new System.Drawing.Point(12, 155);
            this.Reset_Button.Name = "Reset_Button";
            this.Reset_Button.Size = new System.Drawing.Size(75, 23);
            this.Reset_Button.TabIndex = 0;
            this.Reset_Button.Text = "Reset";
            this.Reset_Button.UseVisualStyleBackColor = true;
            this.Reset_Button.Click += new System.EventHandler(this.Reset_Button_Click);
            // 
            // RemotePort_TB
            // 
            this.RemotePort_TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RemotePort_TB.Location = new System.Drawing.Point(113, 31);
            this.RemotePort_TB.Name = "RemotePort_TB";
            this.RemotePort_TB.Size = new System.Drawing.Size(84, 21);
            this.RemotePort_TB.TabIndex = 3;
            this.RemotePort_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RemoteClient_LB
            // 
            this.RemoteClient_LB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RemoteClient_LB.Location = new System.Drawing.Point(3, 64);
            this.RemoteClient_LB.Name = "RemoteClient_LB";
            this.RemoteClient_LB.Size = new System.Drawing.Size(79, 12);
            this.RemoteClient_LB.TabIndex = 13;
            this.RemoteClient_LB.Text = "RemoteClient";
            // 
            // RemoteClientArgs_TB
            // 
            this.RemoteClientArgs_TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoteClientArgs_TB.Location = new System.Drawing.Point(113, 87);
            this.RemoteClientArgs_TB.Name = "RemoteClientArgs_TB";
            this.RemoteClientArgs_TB.Size = new System.Drawing.Size(84, 21);
            this.RemoteClientArgs_TB.TabIndex = 14;
            this.RemoteClientArgs_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LocalPort_LB
            // 
            this.LocalPort_LB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LocalPort_LB.Location = new System.Drawing.Point(3, 8);
            this.LocalPort_LB.Name = "LocalPort_LB";
            this.LocalPort_LB.Size = new System.Drawing.Size(59, 12);
            this.LocalPort_LB.TabIndex = 10;
            this.LocalPort_LB.Text = "LocalPort";
            // 
            // OpenFile_Button
            // 
            this.OpenFile_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenFile_Button.Location = new System.Drawing.Point(113, 59);
            this.OpenFile_Button.Name = "OpenFile_Button";
            this.OpenFile_Button.Size = new System.Drawing.Size(84, 21);
            this.OpenFile_Button.TabIndex = 9;
            this.OpenFile_Button.Text = "OpenFile";
            this.OpenFile_Button.UseVisualStyleBackColor = true;
            this.OpenFile_Button.Click += new System.EventHandler(this.OpenFile_Button_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.ColumnCount = 2;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.MainPanel.Controls.Add(this.AutoStart_Item, 1, 4);
            this.MainPanel.Controls.Add(this.RemoteClientArgs_LB, 0, 3);
            this.MainPanel.Controls.Add(this.RemotePort_LB, 0, 1);
            this.MainPanel.Controls.Add(this.LocalPort_TB, 1, 0);
            this.MainPanel.Controls.Add(this.LocalPort_LB, 0, 0);
            this.MainPanel.Controls.Add(this.RemoteClientArgs_TB, 1, 3);
            this.MainPanel.Controls.Add(this.OpenFile_Button, 1, 2);
            this.MainPanel.Controls.Add(this.RemotePort_TB, 1, 1);
            this.MainPanel.Controls.Add(this.RemoteClient_LB, 0, 2);
            this.MainPanel.Controls.Add(this.HideClient_Item, 0, 4);
            this.MainPanel.Location = new System.Drawing.Point(12, 9);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 5;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.Size = new System.Drawing.Size(200, 140);
            this.MainPanel.TabIndex = 11;
            // 
            // AutoStart_Item
            // 
            this.AutoStart_Item.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AutoStart_Item.AutoSize = true;
            this.AutoStart_Item.Location = new System.Drawing.Point(113, 118);
            this.AutoStart_Item.Name = "AutoStart_Item";
            this.AutoStart_Item.Size = new System.Drawing.Size(78, 16);
            this.AutoStart_Item.TabIndex = 18;
            this.AutoStart_Item.Text = "AutoStart";
            this.AutoStart_Item.UseVisualStyleBackColor = true;
            // 
            // RemoteClientArgs_LB
            // 
            this.RemoteClientArgs_LB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RemoteClientArgs_LB.Location = new System.Drawing.Point(3, 92);
            this.RemoteClientArgs_LB.Name = "RemoteClientArgs_LB";
            this.RemoteClientArgs_LB.Size = new System.Drawing.Size(104, 12);
            this.RemoteClientArgs_LB.TabIndex = 17;
            this.RemoteClientArgs_LB.Text = "RemoteClientArgs";
            // 
            // RemotePort_LB
            // 
            this.RemotePort_LB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RemotePort_LB.Location = new System.Drawing.Point(3, 36);
            this.RemotePort_LB.Name = "RemotePort_LB";
            this.RemotePort_LB.Size = new System.Drawing.Size(75, 12);
            this.RemotePort_LB.TabIndex = 16;
            this.RemotePort_LB.Text = "RemotePort";
            // 
            // LocalPort_TB
            // 
            this.LocalPort_TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LocalPort_TB.Location = new System.Drawing.Point(113, 3);
            this.LocalPort_TB.Name = "LocalPort_TB";
            this.LocalPort_TB.Size = new System.Drawing.Size(84, 21);
            this.LocalPort_TB.TabIndex = 15;
            this.LocalPort_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // HideClient_Item
            // 
            this.HideClient_Item.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.HideClient_Item.AutoSize = true;
            this.HideClient_Item.Location = new System.Drawing.Point(3, 118);
            this.HideClient_Item.Name = "HideClient_Item";
            this.HideClient_Item.Size = new System.Drawing.Size(84, 16);
            this.HideClient_Item.TabIndex = 8;
            this.HideClient_Item.Text = "HideClient";
            this.HideClient_Item.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 182);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.Reset_Button);
            this.Controls.Add(this.Save_Button);
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button Reset_Button;
        private System.Windows.Forms.TextBox RemotePort_TB;
        private System.Windows.Forms.CheckBox HideClient_Item;
        private System.Windows.Forms.Button OpenFile_Button;
        private System.Windows.Forms.Label LocalPort_LB;
        private System.Windows.Forms.Label RemoteClient_LB;
        private System.Windows.Forms.TextBox RemoteClientArgs_TB;
        private System.Windows.Forms.TableLayoutPanel MainPanel;
        private System.Windows.Forms.TextBox LocalPort_TB;
        private System.Windows.Forms.Label RemotePort_LB;
        private System.Windows.Forms.Label RemoteClientArgs_LB;
        private System.Windows.Forms.CheckBox AutoStart_Item;
    }
}