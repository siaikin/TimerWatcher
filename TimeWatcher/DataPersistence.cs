using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TimeWatcher
{
    public static class DataPersistence
    {
        private static readonly String Create_Table_TimeRange = "CREATE TABLE IF NOT EXISTS time_range (" +
            "srart_time DATETIME, " +
            "end_time DATETIME, " +
            "app_path TEXT," +
            "window_name TEXT, " +
            "total_use_time BIGUINT" +
            ")";
        private static readonly String Insert_TimeRange = "INSERT INTO time_range VALUES (@start_time, @end_time, @app_path, @window_name, @total_use_time)";
        private static readonly String Query_TimeRange = "SELECT * FROM time_range";

        public static void Init ()
        {
            SQLiteHelper.CreateDB("timeHistory.db");
            SQLiteHelper.ConnectionDB("timeHistory.db");
            SQLiteHelper.CreateTable(Create_Table_TimeRange);
        }

        /// <summary>
        /// 插入使用应用的时间信息到数据库
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="appPath"></param>
        /// <param name="windowName"></param>
        /// <param name="totalRunTime"></param>
        /// <returns></returns>
        public static int InsertTimeRange(DateTime startTime, DateTime endTime, String appPath, String windowName, uint totalUseTime)
        {
            return SQLiteHelper.Insert(Insert_TimeRange, new object[] { startTime, endTime, appPath, windowName, totalUseTime });
        }

        public static int InsertTimeRange(IWindow window)
        {
            if (window.ToForegroundTime == DateTime.MinValue) window.ToForegroundTime = DateTime.Now;
            return InsertTimeRange(window.ToForegroundTime, DateTime.Now, window.AppFilePath, window.WindowTitle, (uint)window.ForegroundTimeSpen.TotalSeconds);
        }
        /// <summary>
        /// 查询数据库中所有的时间片
        /// </summary>
        /// <returns></returns>
        public static List<Object[]> QueryTimeRange()
        {
            SQLiteDataReader reader = SQLiteHelper.Query(Query_TimeRange);
            Console.WriteLine(reader);
            List<Object[]> result = new List<object[]>();
            while (reader.Read())
            {
                Object[] row = new object[reader.FieldCount];
                row[0] = reader.GetDateTime(0);
                row[1] = reader.GetDateTime(1);
                row[2] = reader.GetString(2);
                row[3] = reader.GetString(3);
                row[4] = reader.GetInt64(4);
                result.Add(row);
            }
            reader.Close();
            return result;
        }
    }
}
