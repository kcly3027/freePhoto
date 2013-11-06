using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Web;

namespace freePhoto.Web.DbHandle
{
    public class OptionDAL : BaseDAL
    {
        /// <summary>
        /// 是否存在表SiteOption
        /// </summary>
        /// <returns></returns>
        public static bool IsExistsTable()
        {
            string sqlStr = @"SELECT COUNT(*) as TabCount FROM sqlite_master where type='table' and name='SiteOption';";
            return Convert.ToInt16(ExecuteScalar(sqlStr)) > 0;
        }
        /// <summary>
        /// 创建表SiteOption
        /// </summary>
        /// <returns></returns>
        public static bool CreateTable()
        {
            string sqlStr = @"create table SiteOption(OptionKey text,OptionValue text);";
            return ExecuteNonQuery(sqlStr) > 0;
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="optionkey"></param>
        /// <returns></returns>
        public static string GetOptionValue(string optionkey)
        {
            string sqlStr = @"select optionvalue from SiteOption where optionkey=@optionkey;";
            SQLiteParameter parameter1 = new SQLiteParameter("@optionkey");
            parameter1.Value = optionkey;
            parameter1.DbType = System.Data.DbType.String;
            object obj = ExecuteScalar(sqlStr, parameter1);
            return obj == null ? "" : obj.ToString();
        }
        public static bool InsertOption(string optionkey, string optionvalue)
        {
            string sqlStr = @"delete from SiteOption where OptionKey=@OptionKey; 
            Insert Into SiteOption([OptionKey],[OptionValue]) values(@OptionKey,@OptionValue) ;";
            List<SQLiteParameter> parameterList = new List<SQLiteParameter>();
            SQLiteParameter e1 = new SQLiteParameter("@OptionKey", System.Data.DbType.String);
            e1.Value = optionkey ?? "";
            parameterList.Add(e1);
            SQLiteParameter e2 = new SQLiteParameter("@OptionValue", System.Data.DbType.String);
            e2.Value = optionvalue ?? "";
            parameterList.Add(e2);
            return ExecuteNonQuery(sqlStr, parameterList.ToArray()) > 0;
        }
    }
}