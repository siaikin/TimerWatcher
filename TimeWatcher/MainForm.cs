using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Timers;
namespace TimeWatcher
{
    public partial class MainForm : Form
    {
        // 系统总运行时间
        public static TimeSpan OSRunTime = new TimeSpan(Environment.TickCount * 1000000);
        // 空闲检测的时间间隔
        public static uint IdleDetectionTimeInterval = 5 * 1000;
        // 繁忙检测的时间间隔
        public static uint BusyDetectionTimeInterval = 1 * 1000;
        // 自身进程id
        public static int IProcessId;

        Dictionary<String, IWindow> AppWindows;

        WindowWatcher WindowForegroundWatcher = null;
        WindowWatcher WindowMinimizeWatcher = null;
        InputWatcher IdleWatcher = null;

        public MainForm()
        {
            InitializeComponent();
            AppWindows = IWindowManager.AppWindows;
            IProcessId = Program.IProcessId;
            ApplicationFilter.AddProcessId(IProcessId);
            ApplicationFilter.AddAppPath(@"C:\WINDOWS\");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 监控窗口切换
            WindowForegroundWatcher = new WindowWatcher();
            WindowForegroundWatcher.windowSwitchEvent += WindowSwitch;
            WindowForegroundWatcher.Init();
            // 监控窗口从最小化恢复
            WindowMinimizeWatcher = new WindowWatcher((int)DLLInvoke.EVENT_SYSTEM_MINIMIZEEND, (int)DLLInvoke.EVENT_SYSTEM_MINIMIZEEND, 0, 0);
            WindowMinimizeWatcher.windowSwitchEvent += WindowSwitch;
            WindowMinimizeWatcher.Init();
            // 系统输入监听
            IdleWatcher = new InputWatcher(IdleDetectionTimeInterval);
            IdleWatcher.Timer.Elapsed += IdleMonitor;
            IdleWatcher.Start();
            // 设置排序方法
            ListViewSort listViewSort = new ListViewSort(ListViewSort.DESCENDING);
            listViewSort.CompareMethod += GetCompareValue;
            AppList.ListViewItemSorter = listViewSort;
        }

        private void AppList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            foreach (ListViewItem item in AppList.Items)
            {
                IProgressBar progressBar = (IProgressBar)item.SubItems[1].Tag;
                Rectangle rectangle = default(Rectangle);
                rectangle = item.SubItems[1].Bounds;
                rectangle.Width = AppList.Columns[1].Width;
                progressBar.SetBounds(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal && this.Visible == true)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
            else if (this.WindowState == FormWindowState.Minimized && this.Visible == false)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
        }

        public String GetCompareValue(Object item)
        {
            return ((ListViewItem)item).SubItems[2].Text;
        }

        /// <summary>
        /// 活动窗口切换时触发, 传入当前活跃窗口进程
        /// </summary>
        /// <param name="processId"></param>
        public void WindowSwitch(Process process)
        {
            OSRunTime = TimeSpan.FromMilliseconds(Environment.TickCount);

            if (ApplicationFilter.isContain(process.Id) || ApplicationFilter.isMatch(process.MainModule.FileName))
            {
                // 更新所有IWindow信息
                IWindowManager.UpdateAll();

                // 更新窗体界面
                UpdateView();
                return;
            }

            // 更新数据: 
            // (1) 添加新IWindow
            IWindow window = null;
            if (!IWindowManager.isContain(process))
            {
                window = IWindowManager.Add(process);
                AddViewItem(window);
            }
            // (2) 更新所有IWindow信息
            IWindowManager.UpdateAll();

            // 更新窗体界面
            UpdateView();
            // 保存时间片信息
            if (IWindowManager.CurrentIWindow != null)
                DataPersistence.InsertTimeRange(IWindowManager.CurrentIWindow);
            IWindowManager.SetForeground(process);
        }

        /// <summary>
        /// 添加当前活动窗口
        /// </summary>
        /// <param name="process"></param>
        public void AddViewItem (IWindow window)
        {
            AppSmallIconList.Images.Add(window.AppIcon);
            AppList.BeginUpdate();
            ListViewItem item = new ListViewItem(window.WindowTitle);
            item.Tag = window;
            item.ImageIndex = AppSmallIconList.Images.Count - 1;
            item.SubItems.Add("");
            item.SubItems.Add(window.ForegroundTimeSpen.ToString(@"hh\:mm\:ss"));
            AppList.Items.Add(item);

            // 添加进度条
            Rectangle rectangle = default(Rectangle);
            rectangle = item.SubItems[1].Bounds;
            IProgressBar progressBar = new IProgressBar(rectangle)
            {
                Value = 0,
                Visible = true,
                Maximum = 100
            };
            item.SubItems[1].Tag = progressBar;
            progressBar.Parent = AppList;
            AppList.EndUpdate();
        }

        /// <summary>
        /// 更新窗口视图
        /// </summary>
        public void UpdateView ()
        {
            // 更新FormStatus内容
            OSRunTImeLabel.Text = OSRunTime.ToString(@"%h'hou. '%m'min. '%s'sec.'");
            OSUseTimeLabel.Text = IWindowManager.OSUseTime.ToString(@"%h'hou. '%m'min. '%s'sec.'");

            AppList.BeginUpdate();
            foreach (ListViewItem item in AppList.Items)
            {
                IWindow window = (IWindow)item.Tag;
                item.SubItems[2].Text = window.ForegroundTimeSpen.ToString(@"%h'h. '%m'm. '%s's.'");
            }

            AppList.Sort();

            foreach (ListViewItem item in AppList.Items)
            {
                IWindow window = (IWindow)item.Tag;

                if (window.ForegroundTimeSpen > IWindowManager.OSUseTime)
                {
                    Console.WriteLine("错误:应用时间大于系统使用时间");
                    return;
                }
                Rectangle rectangle = item.SubItems[1].Bounds;
                ((IProgressBar)item.SubItems[1].Tag).SetBounds(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                if (IWindowManager.OSUseTime == TimeSpan.Zero)
                   ((IProgressBar)item.SubItems[1].Tag).Value = 0;
                else
                    ((IProgressBar)item.SubItems[1].Tag).Value = (int)(window.ForegroundTimeSpen.TotalSeconds * 100 / IWindowManager.OSUseTime.TotalSeconds);
            }
            AppList.EndUpdate();
        }

        /// <summary>
        /// 更新历史记录页面
        /// </summary>
        public void UpdateHistory ()
        {
            List<Object[]> result = DataPersistence.QueryTimeRange();
            HistoryList.BeginUpdate();
            HistoryList.Items.Clear();
            foreach (Object[] row in result)
            {
                ListViewItem item = new ListViewItem((String)row[3]);
                item.SubItems.Add((((DateTime)row[0]).ToLongTimeString()));
                item.SubItems.Add((((DateTime)row[1]).ToLongTimeString()));
                item.SubItems.Add(((Int64)row[4]).ToString());
                item.SubItems.Add(((String)row[2]));
                HistoryList.Items.Add(item);
            }
            HistoryList.EndUpdate();
        }

        Boolean isActive = true;
        /// <summary>
        /// 活跃状态进行空闲检测，非活跃状态进行繁忙检测
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void IdleMonitor(Object source, ElapsedEventArgs e)
        {
            // 无操作毫秒数
            uint interval = InputWatcher.GetLastInputTimeInterval(ref IdleWatcher.LastInputInfo);
            System.Timers.Timer timer = (System.Timers.Timer)source;
            
            if (!isActive)
            {
                Console.WriteLine("Idle status");
                // 非活跃状态下
                // 判断无操作时间是否小于繁忙检测时间间隔
                // 如小于则认定系统处于活跃状态
                isActive = interval < BusyDetectionTimeInterval ? true : true;
                if (isActive)
                {
                    Process process = IWindowManager.GetForegroundWindowProcess();
                    if (process != null && AppWindows.ContainsKey(process.MainModule.FileName))
                        AppWindows[process.MainModule.FileName].SetForeground();
                    timer.Interval = IdleDetectionTimeInterval;
                }
            } else
            {
                Console.WriteLine("Busy status");
                // 活跃状态下
                // 判断无操作时间是否小于空闲检测时间间隔
                // 如小于则认定系统处于活跃状态
                isActive = interval < IdleDetectionTimeInterval ? true : false;
                if (isActive)
                {
                    timer.Interval = IdleDetectionTimeInterval - interval;
                } else
                {
                    Process process = IWindowManager.GetForegroundWindowProcess();
                    if (process != null && AppWindows.ContainsKey(process.MainModule.FileName))
                        AppWindows[process.MainModule.FileName].Update();

                    timer.Interval = BusyDetectionTimeInterval;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
        }

        private void timeWatcherTab_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1)
            {
                UpdateHistory();
            }
        }
    }
}
