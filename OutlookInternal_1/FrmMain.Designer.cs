namespace OutlookInternal_1
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.BtnMoveInbox = new System.Windows.Forms.Button();
            this.BtnAddDr = new System.Windows.Forms.Button();
            this.TxtNewDr = new System.Windows.Forms.TextBox();
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSShow = new System.Windows.Forms.ToolStripMenuItem();
            this.TSHide = new System.Windows.Forms.ToolStripMenuItem();
            this.TSProcessInbox = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnMoveInbox
            // 
            this.BtnMoveInbox.Location = new System.Drawing.Point(12, 12);
            this.BtnMoveInbox.Name = "BtnMoveInbox";
            this.BtnMoveInbox.Size = new System.Drawing.Size(101, 40);
            this.BtnMoveInbox.TabIndex = 0;
            this.BtnMoveInbox.Text = "&Process Inbox";
            this.BtnMoveInbox.UseVisualStyleBackColor = true;
            this.BtnMoveInbox.Click += new System.EventHandler(this.BtnMoveInbox_Click);
            // 
            // BtnAddDr
            // 
            this.BtnAddDr.Location = new System.Drawing.Point(12, 67);
            this.BtnAddDr.Name = "BtnAddDr";
            this.BtnAddDr.Size = new System.Drawing.Size(101, 40);
            this.BtnAddDr.TabIndex = 1;
            this.BtnAddDr.Text = "&Add Doctor";
            this.BtnAddDr.UseVisualStyleBackColor = true;
            this.BtnAddDr.Click += new System.EventHandler(this.BtnAddDr_Click);
            // 
            // TxtNewDr
            // 
            this.TxtNewDr.Location = new System.Drawing.Point(119, 78);
            this.TxtNewDr.MaxLength = 25;
            this.TxtNewDr.Name = "TxtNewDr";
            this.TxtNewDr.Size = new System.Drawing.Size(168, 20);
            this.TxtNewDr.TabIndex = 2;
            this.TxtNewDr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNewDr_KeyPress);
            // 
            // NotifyIcon1
            // 
            this.NotifyIcon1.ContextMenuStrip = this.ContextMenuStrip1;
            this.NotifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon1.Icon")));
            this.NotifyIcon1.Text = "PARS Email";
            this.NotifyIcon1.Visible = true;
            this.NotifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // ContextMenuStrip1
            // 
            this.ContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(17, 17);
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSShow,
            this.TSHide,
            this.TSProcessInbox});
            this.ContextMenuStrip1.Name = "ContextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(158, 70);
            this.ContextMenuStrip1.Text = "Menu";
            // 
            // TSShow
            // 
            this.TSShow.Name = "TSShow";
            this.TSShow.Size = new System.Drawing.Size(157, 22);
            this.TSShow.Text = "Show";
            this.TSShow.Click += new System.EventHandler(this.TSShow_Click);
            // 
            // TSHide
            // 
            this.TSHide.Name = "TSHide";
            this.TSHide.Size = new System.Drawing.Size(157, 22);
            this.TSHide.Text = "Hide";
            this.TSHide.Click += new System.EventHandler(this.TSHide_Click);
            // 
            // TSProcessInbox
            // 
            this.TSProcessInbox.Name = "TSProcessInbox";
            this.TSProcessInbox.Size = new System.Drawing.Size(157, 22);
            this.TSProcessInbox.Text = "Process Inbox";
            this.TSProcessInbox.Click += new System.EventHandler(this.TSProcessInbox_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 131);
            this.Controls.Add(this.TxtNewDr);
            this.Controls.Add(this.BtnAddDr);
            this.Controls.Add(this.BtnMoveInbox);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PARS Email";
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.ContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnMoveInbox;
        private System.Windows.Forms.Button BtnAddDr;
        private System.Windows.Forms.TextBox TxtNewDr;
        private System.Windows.Forms.NotifyIcon NotifyIcon1;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSShow;
        private System.Windows.Forms.ToolStripMenuItem TSHide;
        private System.Windows.Forms.ToolStripMenuItem TSProcessInbox;
    }
}