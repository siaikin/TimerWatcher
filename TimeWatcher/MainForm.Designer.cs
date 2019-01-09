namespace TimeWatcher
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AppSmallIconList = new System.Windows.Forms.ImageList(this.components);
            this.FormStatus = new System.Windows.Forms.StatusStrip();
            this.OSRunTimeFixedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OSRunTImeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OSUseTimeFixedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OSUseTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timeWatcherTab = new System.Windows.Forms.TabControl();
            this.WatcherPage = new System.Windows.Forms.TabPage();
            this.AppList = new System.Windows.Forms.ListView();
            this.ApplicationName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ApplicationTimeRatio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ApplicationUseTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HistoryPage = new System.Windows.Forms.TabPage();
            this.HistoryList = new System.Windows.Forms.ListView();
            this.WindowName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TotalUseTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AppPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FormStatus.SuspendLayout();
            this.timeWatcherTab.SuspendLayout();
            this.WatcherPage.SuspendLayout();
            this.HistoryPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // AppSmallIconList
            // 
            this.AppSmallIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.AppSmallIconList.ImageSize = new System.Drawing.Size(24, 24);
            this.AppSmallIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FormStatus
            // 
            this.FormStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OSRunTimeFixedLabel,
            this.OSRunTImeLabel,
            this.OSUseTimeFixedLabel,
            this.OSUseTimeLabel});
            this.FormStatus.Location = new System.Drawing.Point(0, 428);
            this.FormStatus.Name = "FormStatus";
            this.FormStatus.Size = new System.Drawing.Size(800, 22);
            this.FormStatus.TabIndex = 1;
            this.FormStatus.Text = "FormStatus";
            // 
            // OSRunTimeFixedLabel
            // 
            this.OSRunTimeFixedLabel.Name = "OSRunTimeFixedLabel";
            this.OSRunTimeFixedLabel.Size = new System.Drawing.Size(104, 17);
            this.OSRunTimeFixedLabel.Text = "System run time:";
            // 
            // OSRunTImeLabel
            // 
            this.OSRunTImeLabel.Name = "OSRunTImeLabel";
            this.OSRunTImeLabel.Size = new System.Drawing.Size(21, 17);
            this.OSRunTImeLabel.Text = "0s";
            // 
            // OSUseTimeFixedLabel
            // 
            this.OSUseTimeFixedLabel.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.OSUseTimeFixedLabel.Name = "OSUseTimeFixedLabel";
            this.OSUseTimeFixedLabel.Size = new System.Drawing.Size(105, 17);
            this.OSUseTimeFixedLabel.Text = "System use time:";
            // 
            // OSUseTimeLabel
            // 
            this.OSUseTimeLabel.Name = "OSUseTimeLabel";
            this.OSUseTimeLabel.Size = new System.Drawing.Size(21, 17);
            this.OSUseTimeLabel.Text = "0s";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "TimeWatcher";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // timeWatcherTab
            // 
            this.timeWatcherTab.Controls.Add(this.WatcherPage);
            this.timeWatcherTab.Controls.Add(this.HistoryPage);
            this.timeWatcherTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeWatcherTab.Font = new System.Drawing.Font("微软雅黑 Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timeWatcherTab.Location = new System.Drawing.Point(0, 0);
            this.timeWatcherTab.Name = "timeWatcherTab";
            this.timeWatcherTab.Padding = new System.Drawing.Point(10, 3);
            this.timeWatcherTab.SelectedIndex = 0;
            this.timeWatcherTab.Size = new System.Drawing.Size(800, 428);
            this.timeWatcherTab.TabIndex = 2;
            this.timeWatcherTab.Selected += new System.Windows.Forms.TabControlEventHandler(this.timeWatcherTab_Selected);
            // 
            // WatcherPage
            // 
            this.WatcherPage.Controls.Add(this.AppList);
            this.WatcherPage.Location = new System.Drawing.Point(4, 28);
            this.WatcherPage.Name = "WatcherPage";
            this.WatcherPage.Padding = new System.Windows.Forms.Padding(3);
            this.WatcherPage.Size = new System.Drawing.Size(792, 396);
            this.WatcherPage.TabIndex = 0;
            this.WatcherPage.Text = "Time watcher";
            this.WatcherPage.UseVisualStyleBackColor = true;
            // 
            // AppList
            // 
            this.AppList.BackColor = System.Drawing.SystemColors.Window;
            this.AppList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ApplicationName,
            this.ApplicationTimeRatio,
            this.ApplicationUseTime});
            this.AppList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AppList.Font = new System.Drawing.Font("微软雅黑 Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AppList.LargeImageList = this.AppSmallIconList;
            this.AppList.Location = new System.Drawing.Point(3, 3);
            this.AppList.Name = "AppList";
            this.AppList.Size = new System.Drawing.Size(786, 390);
            this.AppList.SmallImageList = this.AppSmallIconList;
            this.AppList.TabIndex = 1;
            this.AppList.UseCompatibleStateImageBehavior = false;
            this.AppList.View = System.Windows.Forms.View.Details;
            // 
            // ApplicationName
            // 
            this.ApplicationName.Text = "Name";
            this.ApplicationName.Width = 107;
            // 
            // ApplicationTimeRatio
            // 
            this.ApplicationTimeRatio.Text = "Time Ratio";
            this.ApplicationTimeRatio.Width = 344;
            // 
            // ApplicationUseTime
            // 
            this.ApplicationUseTime.Text = "Use Time";
            this.ApplicationUseTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ApplicationUseTime.Width = 90;
            // 
            // HistoryPage
            // 
            this.HistoryPage.Controls.Add(this.HistoryList);
            this.HistoryPage.Location = new System.Drawing.Point(4, 28);
            this.HistoryPage.Name = "HistoryPage";
            this.HistoryPage.Padding = new System.Windows.Forms.Padding(3);
            this.HistoryPage.Size = new System.Drawing.Size(792, 396);
            this.HistoryPage.TabIndex = 1;
            this.HistoryPage.Text = "History";
            this.HistoryPage.UseVisualStyleBackColor = true;
            // 
            // HistoryList
            // 
            this.HistoryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WindowName,
            this.StartTime,
            this.EndTime,
            this.TotalUseTime,
            this.AppPath});
            this.HistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistoryList.LargeImageList = this.AppSmallIconList;
            this.HistoryList.Location = new System.Drawing.Point(3, 3);
            this.HistoryList.Name = "HistoryList";
            this.HistoryList.Size = new System.Drawing.Size(786, 390);
            this.HistoryList.SmallImageList = this.AppSmallIconList;
            this.HistoryList.TabIndex = 0;
            this.HistoryList.UseCompatibleStateImageBehavior = false;
            this.HistoryList.View = System.Windows.Forms.View.Details;
            // 
            // WindowName
            // 
            this.WindowName.Text = "Window Name";
            this.WindowName.Width = 197;
            // 
            // StartTime
            // 
            this.StartTime.Text = "Start Time";
            this.StartTime.Width = 93;
            // 
            // EndTime
            // 
            this.EndTime.Text = "End Time";
            this.EndTime.Width = 79;
            // 
            // TotalUseTime
            // 
            this.TotalUseTime.Text = "Total Use Time";
            this.TotalUseTime.Width = 118;
            // 
            // AppPath
            // 
            this.AppPath.Text = "Application Path";
            this.AppPath.Width = 188;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.timeWatcherTab);
            this.Controls.Add(this.FormStatus);
            this.Name = "MainForm";
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.FormStatus.ResumeLayout(false);
            this.FormStatus.PerformLayout();
            this.timeWatcherTab.ResumeLayout(false);
            this.WatcherPage.ResumeLayout(false);
            this.HistoryPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList AppSmallIconList;
        private System.Windows.Forms.ToolStripStatusLabel OSRunTimeFixedLabel;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripStatusLabel OSUseTimeFixedLabel;
        public System.Windows.Forms.StatusStrip FormStatus;
        public System.Windows.Forms.ToolStripStatusLabel OSRunTImeLabel;
        public System.Windows.Forms.ToolStripStatusLabel OSUseTimeLabel;
        private System.Windows.Forms.TabControl timeWatcherTab;
        private System.Windows.Forms.TabPage WatcherPage;
        public System.Windows.Forms.ListView AppList;
        private System.Windows.Forms.ColumnHeader ApplicationName;
        private System.Windows.Forms.ColumnHeader ApplicationTimeRatio;
        private System.Windows.Forms.ColumnHeader ApplicationUseTime;
        private System.Windows.Forms.TabPage HistoryPage;
        private System.Windows.Forms.ListView HistoryList;
        private System.Windows.Forms.ColumnHeader WindowName;
        private System.Windows.Forms.ColumnHeader StartTime;
        private System.Windows.Forms.ColumnHeader EndTime;
        private System.Windows.Forms.ColumnHeader TotalUseTime;
        private System.Windows.Forms.ColumnHeader AppPath;
    }
}

