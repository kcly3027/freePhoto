using System;
using System.Collections.Generic;
using System.Web;
using freePhoto.Tools;
using System.Data.SQLite;
using System.Data;

namespace freePhoto.Web.DbHandle
{
    public class StoreModel
    {
        public Int64 StoreID { get; set; }
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
        public static StoreModel GetModel(string username)
        {
            string sqlStr = "SELECT * FROM STORES WHERE LOGINNAME=@LOGINNAME";
            SQLiteParameter parameter = new SQLiteParameter("@LOGINNAME");
            parameter.Value = username.Trim().ToLower();
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
        public static bool LoginModel(string email, string pwd, out StoreModel store)
        {
            store = GetModel(email);
            bool isTrue = Md5.CheckPassword(pwd.Trim().ToLower(), store.LoginPwd, 32);
            return isTrue;
        }

        /// <summary>
        /// 注册店面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool RegStoreInfo(StoreModel model)
        {
            model.LoginPwd = Md5.MD5(model.LoginPwd.Trim().ToLower());
            string sqlStr = @"
                    Insert Into Stores ([LoginName],[LoginPwd],[StoreName],[Address],[BaiduMap],[AddTime])
                    select @LoginName,@LoginPwd,@StoreName,@Address,@BaiduMap,datetime('now','localtime')  where not exists(select 1 from stores where loginname='kcly')
            ";
            List<SQLiteParameter> list = GetEntityParas(model);
            return ExecuteNonQuery(sqlStr, list.ToArray()) > 0;
        }

        /// <summary>
        /// 编辑店面信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool EditStoreInfo(StoreModel model)//[LoginPwd]
        {
            string sqlStr = @"Update Stores Set [StoreName]=@StoreName,[Address]=@Address,[BaiduMap]=@BaiduMap {0} WHERE STOREID=@STOREID;  ";
            if (!string.IsNullOrEmpty(model.LoginPwd))
            {
                sqlStr = string.Format(sqlStr, ",[LoginPwd]=@LoginPwd ");
                model.LoginPwd = Md5.MD5(model.LoginPwd.Trim().ToLower());
            }
            else { sqlStr = string.Format(sqlStr, ""); }
            List<SQLiteParameter> list = GetEntityParas(model);
            return ExecuteNonQuery(sqlStr, list.ToArray()) > 0;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool EditPwd(Int64 storeid, string pwd)
        {
            string sqlStr = @"Update Stores Set [LoginPwd]=@LoginPwd WHERE STOREID=@STOREID;  ";
            pwd = Md5.MD5(pwd.Trim().ToLower());
            SQLiteParameter parameter = new SQLiteParameter("@STOREID", System.Data.DbType.Int64);
            parameter.Value = storeid;
            SQLiteParameter parameter1 = new SQLiteParameter("@LoginPwd", System.Data.DbType.String);
            parameter1.Value = pwd;
            return ExecuteNonQuery(sqlStr, parameter, parameter1) > 0;
        }

        /// <summary>
        /// 检查用户名
        /// </summary>
        /// <param name="loginname"></param>
        /// <returns></returns>
        public static bool CheckUserName(string loginname)
        {
            string sqlStr = @"select count(1) from stores where loginname=@LoginName";
            SQLiteParameter parameter = new SQLiteParameter("@LoginName", System.Data.DbType.String);
            parameter.Value = loginname.Trim().ToLower();
            return (int)ExecuteScalar(sqlStr, parameter) > 0;
        }

        public static DataTable GetStoreDt()
        {
            string sqlStr = @"select * from  Stores";
            return ExecuteDataSet(sqlStr).Tables[0];
        }

        public static DataTable GetStoreDt(string storeids)
        {
            string sqlStr = @"SELECT * FROM Stores WHERE StoreID IN (" + storeids + ")";
            return ExecuteDataSet(sqlStr).Tables[0];
        }
    }
}