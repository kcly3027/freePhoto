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
        public DateTime ActiveTime { get; set; }
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
                string sqlStr = @"INSERT INTO USERS(EMAIL,PWD,RegTime) select @EMAIL,@PWD,DateTime('now','localtime') 
                where not exists(SELECT * FROM USERS WHERE EMAIL=@EMAIL);";
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
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool CheckUser(Int64 USERID)
        {
            string sqlStr = "UPDATE USERS SET ActiveTime=datetime('now','localtime'),IsCheck=1 WHERE USERID=@USERID;";
            SQLiteParameter[] parameters = new SQLiteParameter[1];
            parameters[0] = new SQLiteParameter("@USERID", System.Data.DbType.Int64);
            parameters[0].Value = USERID;

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

        /// <summary>
        /// 获得所有用户列表
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pSize"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public static DataTable GetUserList()
        {
            string sqlStr = @"select u.*,ifnull(sum(o.FreeCount+o.PayCount),0) as p,count(o.OrderNo) as p2,max(o.CreateDate) as mt from Users as u LEFT JOIN Orders o ON u.UserID = o.UserID group by u.UserID;";
            DataSet ds = ExecuteDataSet(sqlStr);
            return ds.Tables[0];
        }

        /// <summary>
        /// 添加登录记录
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool AddLoginHistory(Int64 userid)
        {
            string sqlStr = "INSERT INTO LoginHistory(UserID,LoginTime) Values(@USERID,datetime('now','localtime'));";
            SQLiteParameter parameter = new SQLiteParameter("@USERID", System.Data.DbType.Int64);
            parameter.Value = userid;
            return ExecuteNonQuery(sqlStr, parameter) > 0;
        }

        /// <summary>
        /// 检查是否连续登录7天
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool Is7Login(Int64 userid)
        {
            string sqlStr = @"select count(1) as logintimes from (
                                SELECT  datetime(logintime,'start of day','localtime') AS [day],COUNT(1) AS [logincount]
                                FROM loginhistory 
                                WHERE logintime BETWEEN datetime('now','-7 day','start of day','localtime')
                                AND datetime('now','start of day','localtime') and userid=@UserID
                                GROUP BY datetime(logintime,'start of day','localtime') ) as d";
            SQLiteParameter param1 = new SQLiteParameter("@UserID", System.Data.DbType.Int64);
            param1.Value = userid;
            int result = Convert.ToInt32(ExecuteScalar(sqlStr, param1));
            return result >= 7;
        }

        /// <summary>
        /// 检查是否连续登录3天
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool Is3Login(Int64 userid)
        {
            string sqlStr = @"select count(1) as logintimes from (
                                SELECT  datetime(logintime,'start of day','localtime') AS [day],COUNT(1) AS [logincount]
                                FROM loginhistory 
                                WHERE logintime BETWEEN datetime('now','-3 day','start of day','localtime')
                                AND datetime('now','start of day','localtime') and userid=@UserID
                                GROUP BY  datetime(logintime,'start of day','localtime') ) as d";
            SQLiteParameter param1 = new SQLiteParameter("@UserID", System.Data.DbType.Int64);
            param1.Value = userid;
            int result = Convert.ToInt32(ExecuteScalar(sqlStr, param1));
            return result >= 3;
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="donateType"></param>
        /// <returns></returns>
        public static bool AddDonate(Int64 userid,string donateType,int useNum)
        {
            string sqlStr = @"INSERT INTO Donate(UserID,DonateType,UseNum)  select @UserID,@DonateType,@UseNum 
                where not exists(SELECT 1 FROM Donate WHERE UserID=@UserID And DonateType=@DonateType);";
            SQLiteParameter parameter = new SQLiteParameter("@UserID", System.Data.DbType.Int64);
            parameter.Value = userid;
            SQLiteParameter parameter1 = new SQLiteParameter("@DonateType", System.Data.DbType.String);
            parameter1.Value = donateType;
            SQLiteParameter parameter2 = new SQLiteParameter("@UseNum", System.Data.DbType.Int32);
            parameter2.Value = useNum;
            return ExecuteNonQuery(sqlStr, parameter, parameter1, parameter2) > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="donateType"></param>
        /// <param name="useNum"></param>
        /// <returns></returns>
        public static bool UpdateDonate(Int64 userid, string donateType, int useNum)
        {
            string sqlStr = @"UPDATE Donate SET UseNum = UseNum + @UseNum WHERE UserID=@UserID And DonateType=@DonateType;";
            SQLiteParameter parameter = new SQLiteParameter("@UserID", System.Data.DbType.Int64);
            parameter.Value = userid;
            SQLiteParameter parameter1 = new SQLiteParameter("@DonateType", System.Data.DbType.String);
            parameter1.Value = donateType;
            SQLiteParameter parameter2 = new SQLiteParameter("@UseNum", System.Data.DbType.Int32);
            parameter2.Value = useNum;
            return ExecuteNonQuery(sqlStr, parameter, parameter1, parameter2) > 0;
        }

        /// <summary>
        /// 获取可用的赠送照片纸
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetDonateCount(Int64 userid, string donateType)
        {
            string sqlStr = @"SELECT Sum(UseNum) FROM Donate WHERE UserID=@UserID And DonateType = @DonateType;";
            SQLiteParameter parameter = new SQLiteParameter("@USERID", System.Data.DbType.Int64);
            parameter.Value = userid;
            SQLiteParameter parameter1 = new SQLiteParameter("@DonateType", System.Data.DbType.String);
            parameter1.Value = donateType;
            object result = ExecuteScalar(sqlStr, parameter, parameter1);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// 添加检测信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="donateType"></param>
        /// <returns></returns>
        public static bool AddCheckInfo(Int64 userid, string email, string randomNum, string numType)
        {
            string sqlStr = @"INSERT INTO tb_Checks([Email],[UserID],[RandomNum],[IsUse],[NumType]) Values(@Email,@UserID,@RandomNum,0,@NumType);";
            SQLiteParameter parameter1 = new SQLiteParameter("@Email", System.Data.DbType.String);
            parameter1.Value = email;
            SQLiteParameter parameter2 = new SQLiteParameter("@UserID", System.Data.DbType.Int64);
            parameter2.Value = userid;
            SQLiteParameter parameter3 = new SQLiteParameter("@RandomNum", System.Data.DbType.String);
            parameter3.Value = randomNum;
            SQLiteParameter parameter4 = new SQLiteParameter("@NumType", System.Data.DbType.String);
            parameter4.Value = numType;
            return ExecuteNonQuery(sqlStr, parameter1, parameter2, parameter3, parameter4) > 0;
        }

        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="randomNum"></param>
        /// <param name="numType"></param>
        /// <returns></returns>
        public static Int64 GetUserByCheck(string randomNum, string numType)
        {
            string sqlStr = @"SELECT UserID FROM tb_Checks WHERE RandomNum=@RandomNum And NumType = @NumType And IsUse=0;";
            SQLiteParameter parameter = new SQLiteParameter("@RandomNum", System.Data.DbType.String);
            parameter.Value = randomNum;
            SQLiteParameter parameter1 = new SQLiteParameter("@NumType", System.Data.DbType.String);
            parameter1.Value = numType;
            object result = ExecuteScalar(sqlStr, parameter, parameter1);
            return result != null ? Convert.ToInt64(result) : 0;
        }

        /// <summary>
        /// 更新使用
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="donateType"></param>
        /// <param name="useNum"></param>
        /// <returns></returns>
        public static bool UpdateUserByCheck(string randomNum, string numType)
        {
            string sqlStr = @"UPDATE tb_Checks SET IsUse = 1 Where RandomNum=@RandomNum And NumType = @NumType And IsUse=0;";
            SQLiteParameter parameter1 = new SQLiteParameter("@RandomNum", System.Data.DbType.String);
            parameter1.Value = randomNum;
            SQLiteParameter parameter2 = new SQLiteParameter("@NumType", System.Data.DbType.String);
            parameter2.Value = numType;
            return ExecuteNonQuery(sqlStr, parameter1, parameter2) > 0;
        }
    }
}