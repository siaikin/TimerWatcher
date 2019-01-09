using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Timers;
using System.Runtime.InteropServices;

namespace TimeWatcher
{
    class InputWatcher
    {
        // 默认间隔毫秒数
        public static readonly double DEFAULT_INTERVAL = 5 * 1000;

        public DLLInvoke.LastInputInfo LastInputInfo;
        public Timer Timer;

        public InputWatcher ()
            :this(DEFAULT_INTERVAL)
        {
        }

        public InputWatcher (double interval)
        {
            // 必须将结构体的cbSize设置为结构体的大小(sizeof(LastInputInfo))
            // https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-taglastinputinfo
            LastInputInfo = new DLLInvoke.LastInputInfo();
            LastInputInfo.cbSize = Marshal.SizeOf(LastInputInfo);
            Timer = new Timer
            {
                Interval = interval,
                AutoReset = true
            };
        }

        public void Start ()
        {
            Timer.Start();
        }

        public void Stop ()
        {
            Timer.Stop();
        }

        /// <summary>
        /// 重置定时器
        /// </summary>
        public void Restart ()
        {
        }

        /// <summary>
        /// 获取最后一次输入距现在的毫秒值
        /// </summary>
        /// <param name="lastInputInfo"></param>
        /// <returns></returns>
        public static uint GetLastInputTimeInterval(ref DLLInvoke.LastInputInfo lastInputInfo)
        {
            uint inputInterval = 0;
            if (DLLInvoke.GetLastInputInfo(ref lastInputInfo) != 0)
            {
                inputInterval = DLLInvoke.GetTickCount() - lastInputInfo.dwTime;
                if (inputInterval < 0)
                    inputInterval = 0;
            }
            return inputInterval;
        }
    }
}
