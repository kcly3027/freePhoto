using freePhoto.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web;

namespace freePhoto.Web.DbHandle
{
    public class UserModel
    {
        public Int64 UserID { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
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
            return ConvertEntity<UserModel>(ExecuteReader(sqlStr, parameter), true);
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
            return ConvertEntity<UserModel>(ExecuteReader(sqlStr, parameter), true);
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
            if (user == null) return false;
            bool isTrue = Md5.CheckPassword(pwd, user.Pwd, 32);
            return isTrue;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool RegUser(string email)
        {
            try
            {
                string pwd = Md5.MD5("123456");
                string sqlStr = "INSERT INTO USERS(EMAIL,PWD) VALUES(@EMAIL,@PWD);";
                SQLiteParameter parameter = new SQLiteParameter("@EMAIL", System.Data.DbType.String);
                parameter.Value = email;
                SQLiteParameter parameter1 = new SQLiteParameter("@PWD", System.Data.DbType.String);
                parameter1.Value = pwd;
                return ExecuteNonQuery(sqlStr, parameter, parameter1) > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool EditUser(UserModel model)
        {
            string sqlStr = "UPDATE USERS SET ADDRESS=@ADDRESS,SCHOOL=@SCHOOL,QQ=@QQ,MOBILE=@MOBILE WHERE USERID=@USERID;";
            SQLiteParameter[] parameters = new SQLiteParameter[5];
            parameters[0] = new SQLiteParameter("@ADDRESS", System.Data.DbType.String);
            parameters[0].Value = model.Address;
            parameters[1] = new SQLiteParameter("@SCHOOL", System.Data.DbType.String);
            parameters[1].Value = model.School;
            parameters[2] = new SQLiteParameter("@QQ", System.Data.DbType.String);
            parameters[2].Value = model.QQ;
            parameters[3] = new SQLiteParameter("@MOBILE", System.Data.DbType.String);
            parameters[3].Value = model.Mobile;
            parameters[4] = new SQLiteParameter("@USERID", System.Data.DbType.Int64);
            parameters[4].Value = model.UserID;

            return ExecuteNonQuery(sqlStr, parameters) > 0;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool EditPwd(Int64 userId, string pwd)
        {
            pwd = Md5.MD5(pwd);
            string sqlStr = "UPDATE USERS SET PWD=@PWD WHERE USERID=@USERID;";
            SQLiteParameter parameter = new SQLiteParameter("@USERID", System.Data.DbType.Int64);
            parameter.Value = userId;
            SQLiteParameter parameter1 = new SQLiteParameter("@PWD", System.Data.DbType.String);
            parameter1.Value = pwd;
            return ExecuteNonQuery(sqlStr, parameter, parameter1) > 0;
        }

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pSize"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public static DataTable GetUserList(string email,Int64 pIndex, Int64 pSize, out Int64 record)
        {
            string sqlStr = @"Select * From Users Where Email like @Email Order By UserID Desc limit @s offset @e ; Select Count(1) From Users  Where Email like @Email;";
            List<SQLiteParameter> parameterList = new List<SQLiteParameter>();
            SQLiteParameter s_email = new SQLiteParameter("@Email", System.Data.DbType.String);
            s_email.Value = string.Format("%{0}%", email);
            parameterList.Add(s_email);
            SQLiteParameter s = new SQLiteParameter("@s", System.Data.DbType.Int64);
            s.Value = pSize;
            parameterList.Add(s);
            SQLiteParameter e = new SQLiteParameter("@e", System.Data.DbType.Int64);
            e.Value = pSize * (pIndex - 1);
            parameterList.Add(e);
            DataSet ds = ExecuteDataSet(sqlStr, parameterList.ToArray());
            record = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
            return ds.Tables[0];
        }
    }
}