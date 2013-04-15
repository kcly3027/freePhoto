using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Reflection;
using System.Data;
using System.Web;
using System.Configuration;

namespace freePhoto.Web.DbHandle
{
    public class BaseDAL
    {
        static BaseDAL()
        {
            string _connectionStrings = "Data Source={0};Version=3;UTF8Encoding=True";

            _DbPath = HttpContext.Current.Server.MapPath(@"\DB\" + ConfigurationManager.AppSettings["Data"]);
            //_DbPath = ConfigurationManager.AppSettings["Data"];
            _ConnectionStrings = string.Format(_connectionStrings, DbPath);
        }

        private static readonly string _DbPath = "";
        /// <summary>
        /// 数据地址
        /// </summary>
        protected internal static string DbPath
        {
            get { return _DbPath; }
        }
        private static readonly string _ConnectionStrings = "";
        protected internal static string ConnectionStrings
        {
            get { return _ConnectionStrings; }
        }

        /// <remarks>
        /// 使用示例：
        /// int result = ExecuteNonQuery("PublishOrders",  CommandType.StoredProcedure, new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <summary>
        /// 执行一个不需要返回值的SqlCommand命令
        /// </summary>
        /// <param name="sqlText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个数值表示此SqlCommand命令执行后影响的行数
        /// </returns>
        protected internal static int ExecuteNonQuery(string sqlText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionStrings))
            {
                //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                PrepareCommand(cmd, conn, null, cmdType, sqlText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 执行一个不需要返回值的SqlCommand命令
        /// </summary>
        /// <param name="sqlText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns></returns>
        protected internal static int ExecuteNonQuery(string sqlText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteNonQuery(sqlText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// 执行一条返回结果集的SqlCommand命令
        /// </summary>
        /// <param name="sqlText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个包含结果的SqlDataReader
        /// </returns>
        protected internal static DataSet ExecuteDataSet(string sqlText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection conn = new SQLiteConnection(ConnectionStrings);

            // 在这里使用try/catch处理是因为如果方法出现异常，则SqlDataReader就不存在，
            //CommandBehavior.CloseConnection的语句就不会执行，触发的异常由catch捕获。
            //关闭数据库连接，并通过throw再次引发捕捉到的异常。
            try
            {
                DataSet ds = new DataSet();
                PrepareCommand(cmd, conn, null, cmdType, sqlText, commandParameters);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(ds);
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// 执行一条返回结果集的SqlCommand命令
        /// </summary>
        /// <param name="sqlText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个包含结果的SqlDataReader
        /// </returns>
        protected internal static DataSet ExecuteDataSet(string sqlText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteDataSet(sqlText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// 执行一条返回结果集的SqlCommand命令
        /// </summary>
        /// <param name="sqlText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个包含结果的SqlDataReader
        /// </returns>
        protected internal static IDataReader ExecuteReader(string sqlText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection conn = new SQLiteConnection(ConnectionStrings);

            // 在这里使用try/catch处理是因为如果方法出现异常，则SqlDataReader就不存在，
            //CommandBehavior.CloseConnection的语句就不会执行，触发的异常由catch捕获。
            //关闭数据库连接，并通过throw再次引发捕捉到的异常。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, sqlText, commandParameters);
                SQLiteDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行一条返回结果集的SqlCommand命令
        /// </summary>
        /// <param name="sqlText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个包含结果的SqlDataReader
        /// </returns>
        protected internal static IDataReader ExecuteReader(string sqlText,  params SQLiteParameter[] commandParameters)
        {
            return ExecuteReader(sqlText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// 执行一条返回结果集的SqlCommand命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个包含结果的SqlDataReader
        /// </returns>
        protected internal static List<T> ExecuteReader<T>(string sqlText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            List<T> list = new List<T>();
            using (IDataReader reader = ExecuteReader(sqlText, cmdType, commandParameters))
            {
                while (reader.Read()) list.Add(ConvertEntity<T>(reader, false));
                reader.Close();
            }
            return list;
        }

        /// <summary>
        /// 执行一条返回结果集的SqlCommand命令
        /// </summary>
        /// <param name="sqlText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个包含结果的SqlDataReader
        /// </returns>
        protected internal static List<T> ExecuteReader<T>(string sqlText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteReader<T>(sqlText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// 执行一条返回第一条记录第一列的SqlCommand命令
        /// </summary>
        /// <param name="sqlText">The SQL text.</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个object类型的数据，可以通过 Convert.To{Type}方法转换类型
        /// </returns>
        protected internal static object ExecuteScalar(string sqlText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionStrings))
            {
                PrepareCommand(cmd, connection, null, cmdType, sqlText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val != System.DBNull.Value ? val : null;
            }
        }

        /// <summary>
        /// 执行一条返回第一条记录第一列的SqlCommand命令
        /// </summary>
        /// <param name="sqlText">The SQL text.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个object类型的数据，可以通过 Convert.To{Type}方法转换类型
        /// </returns>
        protected internal static object ExecuteScalar(string sqlText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteScalar(sqlText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// 添加 Database 参数信息
        /// </summary>
        /// <typeparam name="T">从何类型获取参数属性</typeparam>
        /// <param name="db">Database</param>
        /// <param name="cmd">DbCommand</param>
        /// <param name="t">参数属性取值来源</param>
        protected internal static List<SQLiteParameter> GetEntityParas<T>(T t)
        {
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            Type type = typeof(T);
            PropertyInfo[] proinfos = type.GetProperties();
            foreach (PropertyInfo p in proinfos)
            {
                if (p.CanRead)
                {
                    var _temp = new SQLiteParameter("@" + p.Name, BuildDbType(p.PropertyType));
                    if (p.PropertyType == DateTime.Now.GetType()) _temp.Value = Convert.ToDateTime(p.GetValue(t, null)).ToString("s");
                    _temp.Value = p.GetValue(t, null);
                    if(_temp.Value != null)list.Add(_temp);
                }
            }
            return list;
        }

        /// <summary>
        /// 反转数据行到实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="idr">IDataReader</param>
        /// <param name="t">结果</param>
        protected internal static T ConvertEntity<T>(IDataReader idr,bool close)
        {
            try
            {
                if (idr.Read())
                {
                    Type type = typeof(T);
                    //对值类型检查
                    if (type.IsValueType || type.Name == "String")
                    {
                        if (idr.FieldCount > 0)
                        {
                            return (T)Convert.ChangeType(idr[0], type);
                        }
                        else return default(T);
                    }

                    T t = (T)type.GetConstructor(new Type[0]).Invoke(new Object[0]);
                    PropertyInfo[] proinfos = type.GetProperties();
                    foreach (PropertyInfo p in proinfos)
                    {
                        string pname = p.Name;
                        if (p.CanWrite && CheckFieldContains(idr, pname))//如果可写，并且在IDataReader字段内存在
                        {
                            object obj = idr[p.Name];
                            if (obj != null && obj != DBNull.Value)
                                p.SetValue(t, obj, null);
                        }
                    }
                    if (close) idr.Close();
                    return t;
                }
                else
                {
                    return default(T);
                }
            }
            catch
            {
                idr.Close();
                return default(T);
            }
        }

        /// <summary>
        /// 创建 DbType 类型
        /// </summary>
        /// <param name="t">System数据类型</param>
        /// <returns></returns>
        protected internal static DbType BuildDbType(Type t)
        {
            //DbType.Currency            
            switch (t.Name)
            {
                case "Byte":
                    return DbType.Byte;
                case "Byte[]":
                    return DbType.Binary;
                //return DbType.Byte;
                case "Int32":
                    return DbType.Int32;
                case "Int64":
                    return DbType.Int64;
                case "UInt16":
                    return DbType.UInt16;
                case "UInt32":
                    return DbType.UInt32;
                case "UInt64":
                    return DbType.UInt64;
                case "Decimal":
                    return DbType.Decimal;
                case "Double":
                    return DbType.Double;
                case "Guid":
                    return DbType.Guid;
                case "Xml":
                    return DbType.Xml;
                case "Object":
                    return DbType.Binary;
                case "Boolean":
                    return DbType.Boolean;
                case "String":
                    return DbType.String;
                case "DateTime":
                    return DbType.String;
                default:
                    return DbType.String;
            }
        }

        protected internal static string BuildUpdateString<T>(T t)
        {
            string sqlStr = "";
            PropertyInfo[] infos = t.GetType().GetProperties();
            for (int i = 0; i < infos.Length; i++)
            {
                if (i != 0) sqlStr += ",";
                sqlStr += string.Format("{0}=@{0} ", infos[i].Name);
            }
            return sqlStr;
        }

        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">SqlCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-SQL语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, CommandType cmdType, string cmdText, SQLiteParameter[] cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //判断是否需要事物处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                {
                    if (parm == null) continue;
                    if (parm.DbType == DbType.DateTime)
                    {
                        parm.DbType = DbType.String;
                        parm.Value = ((DateTime)parm.Value).ToString("s");
                    }
                    cmd.Parameters.Add(parm);
                }
            }
        }

        /// <summary>
        ///  检查 属性值是否包括在 IDataReader字段内
        /// </summary>
        private static bool CheckFieldContains(IDataReader idr, string propertyName)
        {
            bool isContains = false;
            for (int i = 0, rowCount = idr.FieldCount; i < rowCount; i++)
            {
                if (idr.GetName(i).ToLower() == propertyName.ToLower())
                {
                    isContains = true;
                    break;
                }
            }
            return isContains;
        }
    }
}
