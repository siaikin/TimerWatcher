using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TimeWatcher
{
    /// <summary>
    /// 自定义应用程序窗口类
    /// </summary>
    public class IWindow
    {
        protected static int ProcessorCount = Environment.ProcessorCount;   // 处理器数量
        public Boolean isForeground = false;    // 应用窗口位于桌面顶层

        public int ProcessId;  // 应用对应线程id
        public String WindowTitle;  // 窗口标题文本
        public String AppFilePath;  // 应用主模块(可执行文件)文件路径
        public Icon AppIcon;   // 应用图标
        public TimeSpan ForegroundTimeSpen; // 应用在前台的时间
        public TimeSpan AppLiveTimeSpan; // 应用运行的总时间
        public long MemorySize; // 应用所占内存
        public float ProcessorTime; // 应用所占处理器时间比例(暂时忽略)

        public DateTime ToForegroundTime = DateTime.MinValue;   // 应用切换为活跃状态时的时间

        public IWindow(string windowTitle, string appFilePath, Icon appIcon)
        {
            WindowTitle = windowTitle;
            AppFilePath = appFilePath;
            AppIcon = appIcon;
        }

        public IWindow(string windowTitle, string appFilePath, IntPtr iconHandle)
            : this(windowTitle, appFilePath, Icon.FromHandle(iconHandle))
        {
            // 进程图标
            //IntPtr[] iconLarge = new IntPtr[10], iconSmall = new IntPtr[10];
            //DLLInvoke.ExtractIconEx(AppFilePath, 0, iconLarge, iconSmall, 1);
            //AppIcon = Icon.FromHandle(iconLarge[0]);
        }

        public IWindow(Process process)
        {
            ProcessId = process.Id;
            WindowTitle = process.ProcessName;
            AppFilePath = process.MainModule.FileName;
            // 进程图标
            AppIcon = Icon.ExtractAssociatedIcon(AppFilePath);

            ForegroundTimeSpen = TimeSpan.Zero;
            AppLiveTimeSpan = TimeSpan.Zero;
        }

        public IWindow(int processId)
            : this (Process.GetProcessById(processId))
        {
        }

        /// <summary>
        /// 更新窗口对象信息
        /// </summary>
        public void Update ()
        {
            if (ProcessId == -1)
                Console.WriteLine("进程Id未知, 窗口信息无法更新");
            Process CurrentProcess = null;
            try
            {
                CurrentProcess = Process.GetProcessById(ProcessId);
            } catch (ArgumentException e)
            {
                Console.WriteLine("id为[" + ProcessId + "]的进程未运行");
                return;
            } catch (InvalidOperationException e)
            {
                MessageBox.Show("未知错误");
            }

            AppLiveTimeSpan = DateTime.Now - CurrentProcess.StartTime;
            MemorySize = CurrentProcess.WorkingSet64;
            if (isForeground)
            {
                ForegroundTimeSpen += DateTime.Now - ToForegroundTime;
                isForeground = false;
            }
        }

        /// <summary>
        /// 当窗口切换到前台(最顶层)时调用, 重设isForeground, ToForefroundTime
        /// </summary> 
        public void SetForeground ()
        {
            if (!isForeground)
            {
                isForeground = true;
                ToForegroundTime = DateTime.Now;
            }
        }
    }
}
