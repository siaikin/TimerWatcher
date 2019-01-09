using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeWatcher
{
    public static class IWindowManager
    {
        // 系统使用时间
        public static TimeSpan OSUseTime = TimeSpan.Zero;

        public static Dictionary<String, IWindow> AppWindows = new Dictionary<string, IWindow>();
        public static IWindow CurrentIWindow = null;

        public static void UpdateAll ()
        {
            OSUseTime = TimeSpan.Zero;
            // 更新所有Window
            Dictionary<String, IWindow>.Enumerator enumerator = AppWindows.GetEnumerator();
            while(enumerator.MoveNext())
            {
                enumerator.Current.Value.Update();
                OSUseTime += enumerator.Current.Value.ForegroundTimeSpen;
            }
        }

        public static void Update (String key)
        {
            if (AppWindows.ContainsKey(key))
                AppWindows[key].Update();
        }

        public static void Update(Process process)
        {
            Update(process.MainModule.FileName);
        }

        public static Boolean SetForeground(String key)
        {
            if (AppWindows.ContainsKey(key))
            {
                AppWindows[key].SetForeground();
                CurrentIWindow = AppWindows[key];
                return true;
            }
            return false;
        }

        public static Boolean SetForeground(Process process)
        {
            return SetForeground(process.MainModule.FileName);
        }

        public static IWindow Add (Process process) 
        {
            String path = process.MainModule.FileName;
            if (AppWindows.ContainsKey(path))
                return null;
            AppWindows.Add(path, new IWindow(process));
            return AppWindows[path];
        }

        public static Boolean isContain (Process process)
        {
            return AppWindows.ContainsKey(process.MainModule.FileName);
        }

        public static Process GetForegroundWindowProcess()
        {
            int pid = GetForegroundWindowProcessId();
            Process process = null;
            if (pid < 0) return null;
            try
            {
                process = Process.GetProcessById(pid);
            } catch (ArgumentException e)
            {
                Console.WriteLine("id为[" + pid + "]的进程未运行");
                return null;
            }
            return process;
        }

        public static int GetForegroundWindowProcessId()
        {
            uint pid;
            IntPtr windowHandle = DLLInvoke.GetForegroundWindow();
            try
            {
                DLLInvoke.GetWindowThreadProcessId(windowHandle, out pid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //MessageBox.Show(e.Message);
                return -1;
            }
            return (int)pid;
        }
    }
}
