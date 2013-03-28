using System;
using System.Collections.Generic;
using System.Web;
using freePhoto.Tools;
using System.Data.SQLite;

namespace freePhoto.Web.DbHandle
{
    public class StoreModel
    {
        public int StoreID { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string BaiduMap { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime LastLoginTime { get; set; }
    }

    public class StoreDAL : BaseDAL
    {
        /// <summary>
        /// 根据邮箱获取实体
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        public static StoreModel GetModel(string email)
        {
            string sqlStr = "SELECT * FROM STORES WHERE LOGINNAME=@LOGINNAME";
            SQLiteParameter parameter = new SQLiteParameter("@LOGINNAME");
            parameter.Value = email.ToLower();
            parameter.DbType = System.Data.DbType.String;
            return ConvertEntity<StoreModel>(ExecuteReader(sqlStr, parameter), true);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="storeId">店面id</param>
        /// <returns></returns>
        public static StoreModel GetModel(Int64 storeId)
        {
            string sqlStr = "SELECT * FROM STORES WHERE STOREID=@STOREID";
            SQLiteParameter parameter = new SQLiteParameter("@STOREID");
            parameter.Value = storeId;
            parameter.DbType = System.Data.DbType.Int64;
            return ConvertEntity<StoreModel>(ExecuteReader(sqlStr, parameter), true);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <param name="store"></param>
        /// <returns></returns>
        public bool LoginModel(string email, string pwd, out StoreModel store)
        {
            store = GetModel(email);
            bool isTrue = Md5.CheckPassword(pwd, pwd, 32);
            return isTrue;
        }
    }
}