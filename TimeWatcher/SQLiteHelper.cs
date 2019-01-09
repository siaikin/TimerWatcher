using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Text.RegularExpressions;

namespace TimeWatcher
{
    class SQLiteHelper
    {
        public static readonly String DBDirectory = @"/sqliteDB/";
        public static readonly String DBConnectionString_prefix = "data source =";
        public static Dictionary<Type, DbType> ClassToSQLiteType = new Dictionary<Type, DbType>()
        {
            {typeof(String), DbType.String },
            {typeof(int), DbType.Int32 },
            {typeof(uint), DbType.UInt64 },
            {typeof(DateTime), DbType.DateTime }
        };

        public static SQLiteConnection connection = null;
        public static SQLiteCommand command = null;
        
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public static String CreateDB(String DBName)
        {
            String path = Environment.CurrentDirectory + DBDirectory;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            String DBPath = path + DBName;
            if (!File.Exists(DBPath))
            {
                SQLiteConnection.CreateFile(DBPath);
            }
            return DBPath;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="DBName"></param>
        public static void ConnectionDB (String DBName)
        {

            String connectionString = DBConnectionString_prefix
                + Environment.CurrentDirectory
                + DBDirectory
                + DBName;
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            command = connection.CreateCommand();
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static Boolean CreateTable (String sql)
        {
            if (ExecuteNotQuery(sql) == 1)
                return true;
            return false;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int Insert (String sql, Object[] values)
        {
            return ExecuteNotQuery(sql, values);
        }

        public static int Insert(String sql)
        {
            return ExecuteNotQuery(sql);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static SQLiteDataReader Query(String sql, Object[] values)
        {
            return ExecuteReader(sql, values);
        }
        public static SQLiteDataReader Query(String sql)
        {
            return ExecuteReader(sql);
        }

        /// <summary>
        /// 执行非查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static int ExecuteNotQuery(String sql)
        {
            if (sql == null) return -1;
            command.CommandText = sql;
            return command.ExecuteNonQuery();
        }

        private static int ExecuteNotQuery (String sql, Object[] values)
        {
            SQLiteParameter[] parameters = CreateSQLParameter(sql, values);
            if (parameters == null) return -1;
            command.CommandText = sql;
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static SQLiteDataReader ExecuteReader (String sql)
        {
            command.CommandText = sql;
            return command.ExecuteReader();
        }

        private static SQLiteDataReader ExecuteReader (String sql, Object[] values)
        {
            SQLiteParameter[] parameters = CreateSQLParameter(sql, values);
            if (parameters == null) return null;
            command.CommandText = sql;
            command.Parameters.AddRange(parameters);
            return command.ExecuteReader();
        }

        /// <summary>
        /// 创建带参sql语句的参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private static SQLiteParameter[] CreateSQLParameter (String sql, Object[] values)
        {
            if (connection == null || command == null) return null;
            command.CommandText = sql;
            // 正则提取字段名
            MatchCollection paramNames = Regex.Matches(sql, @"@\S*\b");
            SQLiteParameter[] parameters = new SQLiteParameter[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                Type type = values[i].GetType();
                parameters[i] = new SQLiteParameter(paramNames[i].Value, ClassToSQLiteType[type]);
                parameters[i].Value = values[i];
            }
            return parameters;
        }
    }
}
