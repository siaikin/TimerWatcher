using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeWatcher
{
    public static class ApplicationFilter
    {
        static HashSet<int> ExcludeProcess = new HashSet<int>();
        static HashSet<String> ExcludeApplicationPath = new HashSet<string>();

        public static void AddProcessId (int processId)
        {
            ExcludeProcess.Add(processId);
        }

        public static void AddAppPath (String appPath)
        {
            ExcludeApplicationPath.Add(appPath);
        }

        public static Boolean isContain (int processId)
        {
            return ExcludeProcess.Contains(processId);
        }

        public static Boolean isMatch (String appPath)
        {
            foreach(String mathString in ExcludeApplicationPath)
            {
                if (appPath.Contains(mathString))
                    return true;
            }
            return false;
        }
    }
}
