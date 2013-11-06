using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web;

namespace freePhoto.Web.DbHandle
{
    public class FeedbackDAL : BaseDAL
    {
        /// <summary>
        /// 是否存在表Feedback
        /// </summary>
        /// <returns></returns>
        public static bool IsExistsTable()
        {
            string sqlStr = @"SELECT COUNT(*) as TabCount FROM sqlite_master where type='table' and name='Feedback';";
            return Convert.ToInt16(ExecuteScalar(sqlStr)) > 0;
        }
        /// <summary>
        /// 创建表Feedback
        /// </summary>
        /// <returns></returns>
        public static bool CreateTable()
        {
            string sqlStr = @"create table Feedback(FID integer PRIMARY KEY autoincrement,UserID integer,FUserName text,FContent text,FTel text,FEmail text,FQQ text,FTime datetime);";
            return ExecuteNonQuery(sqlStr) > 0;
        }

        /// <summary>
        /// 获得反馈列表
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pSize"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public static DataTable GetFeedbackList(Int64 pIndex, Int64 pSize, out Int64 record)
        {
            string sqlStr = @"Select a.*,b.EMAIL From Feedback as a,USERS as b where a.UserID=b.UserID Order By FTime Desc limit @s offset @e ; Select Count(1) From Feedback;";
            List<SQLiteParameter> parameterList = new List<SQLiteParameter>();
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

        public static bool Del(Int64 FID)
        {
            string sqlStr = @"DELEte from Feedback where fid=@fid";
            List<SQLiteParameter> parameterList = new List<SQLiteParameter>();
            SQLiteParameter s = new SQLiteParameter("@fid", System.Data.DbType.Int64);
            s.Value = FID;
            parameterList.Add(s);
            return ExecuteNonQuery(sqlStr,parameterList.ToArray()) > 0;
        }

        public static bool InsertFeedback(Int64 userid, string fusername, string fcontent, string ftel, string femail, string fqq)
        {
            string sqlStr = @"Insert Into Feedback([UserID],[FUserName],[FContent],[FTel],[FEmail],[FQQ],[FTime]) values(@userid,@fusername,@fcontent,@ftel,@femail,@fqq,datetime('now','localtime')) ;";
            List<SQLiteParameter> parameterList = new List<SQLiteParameter>();
            SQLiteParameter s = new SQLiteParameter("@userid", System.Data.DbType.Int64);
            s.Value = userid;
            parameterList.Add(s);
            SQLiteParameter e1 = new SQLiteParameter("@fusername", System.Data.DbType.String);
            e1.Value = fusername ?? "";
            parameterList.Add(e1);
            SQLiteParameter e2 = new SQLiteParameter("@fcontent", System.Data.DbType.String);
            e2.Value = fcontent ?? "";
            parameterList.Add(e2);
            SQLiteParameter e3 = new SQLiteParameter("@ftel", System.Data.DbType.String);
            e3.Value = ftel ?? "";
            parameterList.Add(e3);
            SQLiteParameter e4 = new SQLiteParameter("@femail", System.Data.DbType.String);
            e4.Value = femail ?? "";
            parameterList.Add(e4);
            SQLiteParameter e5 = new SQLiteParameter("@fqq", System.Data.DbType.String);
            e5.Value = fqq ?? "";
            parameterList.Add(e5);
            return ExecuteNonQuery(sqlStr,parameterList.ToArray()) > 0;
        }
    }
}