using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

/// <summary>
/// 窗口监听
/// </summary>
namespace TimeWatcher
{
    class WindowWatcher
    {
        public delegate void ActiveWindowSwitch(Process process);

        // SetWinEventHook所需参数
        public int eventMin;
        public int eventMax;
        public IntPtr hmodeWinEventProc;
        public DLLInvoke.WinEventDelegate winEventDelegate;
        public int idProcess;
        public int idThread;
        public int dwFlags;

        public ActiveWindowSwitch windowSwitchEvent;

        // HWINEVENTHOOK value that identifies this event hook instance. Applications save this return value to use it with the UnhookWinEvent function.
        // If unsuccessful, returns zero.
        // 返回值用于标识钩子函数实例，用于之后调用UnhookWinEvent方法撤销该函数. 执行失败，返回0(IntPtr.zero)
        private IntPtr HookHandle;

        public WindowWatcher(int eventMin, int eventMax, IntPtr hmodeWinEventProc, int idProcess, int idThread, int dwFlags)
        {
            this.eventMin = eventMin;
            this.eventMax = eventMax;
            this.hmodeWinEventProc = hmodeWinEventProc;
            this.winEventDelegate += WindowBaseEvent;
            this.idProcess = idProcess;
            this.idThread = idThread;
            this.dwFlags = dwFlags;
        }

        public WindowWatcher(int eventMin, int eventMax, int idProcess, int idThread)
            : this(eventMin, eventMax, IntPtr.Zero, idProcess, idThread, 0)
        {
        }

        public WindowWatcher(int eventMin, int eventMax, DLLInvoke.WinEventDelegate winEventDelegate)
            : this(eventMin, eventMax, IntPtr.Zero, 0, 0, 0)
        {
        }

        public WindowWatcher(int idProcess, int idThread)
            : this((int)DLLInvoke.EVENT_SYSTEM_FOREGROUND, (int)DLLInvoke.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, idProcess, idThread, 0)
        {
        }

        public WindowWatcher()
            : this((int)DLLInvoke.EVENT_SYSTEM_FOREGROUND, (int)DLLInvoke.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, 0, 0, 0)
        {
        }

        /// <summary>
        /// 向系统添加钩子函数
        /// </summary>
        public void Init()
        {
            if (this.windowSwitchEvent.GetInvocationList() == null ||this.windowSwitchEvent.GetInvocationList().Length <= 0)
            {
                Console.WriteLine("回调函数列表为空");
                return;
            }
            HookHandle = DLLInvoke.SetWinEventHook(eventMin, eventMax, hmodeWinEventProc, winEventDelegate, idProcess, idThread, dwFlags);
        }

        // 上一个活动窗口的进程id,  当前进程id
        uint preProcessId = 0, processId;

        /// <summary>
        /// 切换窗口时的回调函数（同一进程下的线程窗口切换也会触发）
        /// </summary>
        /// <param name="hWinEventHook"></param>
        /// <param name="eventType"></param>
        /// <param name="hwnd"></param>
        /// <param name="idObject"></param>
        /// <param name="idChlid"></param>
        /// <param name="dwEventThread"></param>
        /// <param name="dwmsEventTime"></param>
        public void WindowBaseEvent(IntPtr hWinEventHook, Int32 eventType, IntPtr hwnd, long idObject, long idChlid, Int32 dwEventThread, Int32 dwmsEventTime)
        {
            // 获取窗口句柄所代表的进程id, 该函数返回值为窗口代表线程id
            UInt16 threadId = DLLInvoke.GetWindowThreadProcessId(hwnd, out processId);

            // 过滤同一进程下切换线程的情况
            if (preProcessId == processId) return;

            preProcessId = processId;

            Process process = null;
            // 过滤掉已终止的进程
            try
            {
                process = Process.GetProcessById((int)processId);
            } catch (ArgumentException e)
            {
                Console.WriteLine("id为[" + processId + "]的进程未运行");
                return;
            } catch (InvalidOperationException e)
            {
                MessageBox.Show("未知错误");
            }

            String modulePath = null;
            // 过滤某些系统应用无法获取到主模块路径的情况
            try
            {
                modulePath = process.MainModule.FileName;
            }
            catch (Exception e)
            {
                Console.WriteLine("无权限访问当前应用的文件路径 [" + process.ProcessName + "]");
                Console.WriteLine(e.Message);
                //MessageBox.Show("无权限访问当前应用的文件路径 [" + process.ProcessName + "]");
                return;
            }
            Console.WriteLine("PID: " + processId);
            Console.WriteLine("进程主模块名: " + process.MainModule.ModuleName);
            Console.WriteLine("进程主模块路径: " + process.MainModule.FileName);

            windowSwitchEvent.Invoke(process);
        }

    }
}
