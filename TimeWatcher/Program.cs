using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TimeWatcher
{
    static class Program
    {
        public static MainForm mainForm;
        public static int IProcessId;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IProcessId = Process.GetCurrentProcess().Id;

            DataPersistence.Init();

            mainForm = new MainForm();
            Application.Run(mainForm);
        }
    }
}
