using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

/// <summary>
/// DLL调用
/// </summary>
namespace TimeWatcher
{
    class DLLInvoke
    {
        // https://docs.microsoft.com/zh-cn/windows/desktop/WinAuto/event-constants
        public static readonly uint EVENT_SYSTEM_FOREGROUND = 0x0003;
        public static readonly uint EVENT_SYSTEM_MINIMIZESTART = 0x0016;
        public static readonly uint EVENT_SYSTEM_MINIMIZEEND = 0x0017;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        // 获取窗口标题文本
        // GetWindowTextW, GetWindowTextA W和A表示在ANSI环境或Unicode编码环境下使用
        // 窗口标题过长会产生以下异常, 应传入创建的字符串的长度而非标题长度, GetWindowTextW会将标题超出部分自动截断
        // “System.AccessViolationException”类型的未经处理的异常在 未知模块。 中发生尝试读取或写入受保护的内存。这通常指示其他内存已损坏。
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindowTextW(IntPtr intPtr, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetWindowTextLengthW(IntPtr intPtr);

        /// <summary>
        /// 用窗口句柄获取进程,线程id
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern UInt16 GetWindowThreadProcessId(IntPtr intPtr, out uint lpdwProcessId);

        /// <summary>
        /// 对windows事件添加钩子函数
        /// </summary>
        /// <param name="eventMin"></param>
        /// <param name="eventMax"></param>
        /// <param name="hmodeWinEventProc"></param>
        /// <param name="pfnWinEventProc"></param>
        /// <param name="idProcess"></param>
        /// <param name="idThread"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetWinEventHook(Int32 eventMin, Int32 eventMax, IntPtr hmodeWinEventProc, WinEventDelegate pfnWinEventProc, Int32 idProcess, Int32 idThread, Int32 dwFlags);

        public delegate void WinEventDelegate(IntPtr hWinEventHook, Int32 eventType, IntPtr hwnd, long idObject, long idChlid, Int32 dwEventThread, Int32 dwmsEventTime);

        public struct LastInputInfo {
            public int cbSize;
            public uint dwTime;
        }

        /// <summary>
        /// 返回上次输入的时间
        /// 收到最后一个输入事件时的TickCount(TickCount: 系统运行时间)
        /// 实际返回的是最后一个输入事件时的系统运行时间
        /// </summary>
        /// <param name="lastInputInfo"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetLastInputInfo(ref LastInputInfo lastInputInfo);

        /// <summary>
        /// 从可执行文件, dll获取icon
        /// </summary>
        /// <param name="IpszFile"></param>
        /// <param name="nIconIndex"></param>
        /// <param name="phIconLarge"></param>
        /// <param name="phIconSmall"></param>
        /// <param name="nIcons"></param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern int ExtractIconEx(String IpszFile, int nIconIndex, IntPtr[] phIconLarge, IntPtr[] phIconSmall, uint nIcons);

        [DllImport("shell32.dll")]
        public static extern IntPtr ExtractIcon(IntPtr hInst, String pszExeFileName, uint nIconIndex);

        [DllImport("shell32.dll")]
        public static extern IntPtr ExtractAssociatedIconW(IntPtr hInst, String pszIconPath, ref Int32 piIcon);

        /// <summary>
        /// 获取系统运行时间
        /// 经过的时间存储为DWORD()值。因此，如果系统连续运行49.7天，时间将回绕到零。
        /// 要避免此问题，请使用GetTickCount64函数。
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern UInt32 GetTickCount();

        /// <summary>
        /// 获取系统运行时间
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern UInt64 GetTickCount64();
    }
}
