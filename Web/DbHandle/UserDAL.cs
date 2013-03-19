using freePhoto.Tools;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Web;

namespace freePhoto.Web.DbHandle
{
    public class UserModel
    {
        public Int64 UserID { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string School { get; set; }
        public string QQ { get; set; }
        public string Mobile { get; set; }
        public DateTime RegTime { get; set; }
        public string ActiveTime { get; set; }
        public bool IsCheck { get; set; }
    }

    public class UserDAL : BaseDAL
    {
        /// <summary>
        /// 根据邮箱获取实体
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        public static UserModel GetModel(string email)
        {
            string sqlStr = "SELECT * FROM USERS WHERE EMAIL=@EMAIL";
            SQLiteParameter parameter = new SQLiteParameter("@EMAIL");
            parameter.Value = email.ToLower();
            parameter.DbType = System.Data.DbType.String;
            return ConvertEntity<UserModel>(ExecuteReader(sqlStr, parameter));
        }

        /// <summary>
        /// 根据邮箱获取实体
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        public static UserModel GetModel(Int64 userId)
        {
            string sqlStr = "SELECT * FROM USERS WHERE USERID=@USERID";
            SQLiteParameter parameter = new SQLiteParameter("@USERID");
            parameter.Value = userId;
            parameter.DbType = System.Data.DbType.Int64;
            return ConvertEntity<UserModel>(ExecuteReader(sqlStr, parameter));
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool LoginModel(string email, string pwd, out UserModel user)
        {
            user = GetModel(email);
            bool isTrue = Md5.CheckPassword(pwd, pwd, 32);
            return isTrue;
        }
    }
}