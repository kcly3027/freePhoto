using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web;

namespace freePhoto.Web.DbHandle
{
    public class AdOrderModel
    {
        public Int64 AdID { get; set; }
        public string OrderNo { get; set; }
        public Int64 UserID { get; set; }
        public string AdKeyWord { get; set; }
        public string FileKey { get; set; }
        public string FileType { get; set; }
        public string FileOldName { get; set; }
        public int FileCount { get; set; }
        public DateTime AdBeginTime { get; set; }
        public DateTime AdEndTime { get; set; }
        public string AdStore { get; set; }
        public string NanNvBL { get; set; }
        public string AdName { get; set; }
        public int PrintNum { get; set; }
        public string PrintType { get; set; }
        public decimal Total_fee { get; set; }
        public DateTime CreateDate { get; set; }
        public string AlipayNo { get; set; }
        public DateTime PayDate { get; set; }
        public decimal Price { get; set; }
        public string State { get; set; }
    }

    public class AdOrderDAL : BaseDAL
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool CreateOrder(AdOrderModel model)
        {
            try
            {
                string sqlStr = @"
                            Insert Into AdOrders ([OrderNo],[UserID],[AdKeyWord],[FileKey],[FileType],[FileOldName],[FileCount],[AdBeginTime],[AdEndTime],[AdStore],[NanNvBL],[AdName],[PrintNum],[PrintType],[Total_fee],[CreateDate],[Price],[State]) 
                                    Select @OrderNo,@UserID,@AdKeyWord,@FileKey,FileExt,FileOldName,FileCount,@CAdBeginTime,@CAdEndTime,@AdStore,@NanNvBL,@AdName,@PrintNum,@PrintType,@Total_fee,datetime('now','localtime'),@Price,@State
                                        From UpFileHistory Where FileKey=@FileKey;
                            Delete From UpFileHistory Where FileKey=@FileKey;";

                List<SQLiteParameter> list = GetEntityParas<AdOrderModel>(model);
                list.Add(new SQLiteParameter("@CAdBeginTime", model.AdBeginTime.ToString("s")));
                list.Add(new SQLiteParameter("@CAdEndTime", model.AdEndTime.ToString("s")));
                int result = ExecuteNonQuery(sqlStr, list.ToArray());
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public static AdOrderModel GetOrder(string orderno)
        {
            string sqlStr = @"select * from AdOrders where orderno=@orderno order by CreateDate desc LIMIT 2;";
            SQLiteParameter parameter1 = new SQLiteParameter("@orderno", orderno);
            return ConvertEntity<AdOrderModel>(ExecuteReader(sqlStr, parameter1), true);
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderno"></param>
        public static bool DoneOrder(string orderno)
        {
            string sqlStr = @"update AdOrders set [state]='已完成' where orderno=@orderno;";
            SQLiteParameter parameter1 = new SQLiteParameter("@orderno");
            parameter1.Value = orderno;
            parameter1.DbType = System.Data.DbType.String;
            return ExecuteNonQuery(sqlStr, parameter1) > 0;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderNo"></param>
        /// <param name="state"></param>
        /// <param name="pIndex"></param>
        /// <param name="pSize"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public static DataTable GetOrderList(Int64 userid, string orderNo, string state, Int64 pIndex, Int64 pSize, out Int64 record)
        {
            string s1 = "", s2 = "", s3 = "";
            string sqlStr = @"Select * From AdOrders Where 1=1 {2} {0} {1} Order By CreateDate Desc limit @s offset @e ; Select Count(1) From AdOrders Where 1=1 {2} {0} {1}";
            List<SQLiteParameter> parameterList = new List<SQLiteParameter>();
            SQLiteParameter s = new SQLiteParameter("@s", System.Data.DbType.Int64);
            s.Value = pSize;
            parameterList.Add(s);
            SQLiteParameter e = new SQLiteParameter("@e", System.Data.DbType.Int64);
            e.Value = pSize * (pIndex - 1);
            parameterList.Add(e);

            if (!string.IsNullOrEmpty(orderNo))
            {
                SQLiteParameter p2 = new SQLiteParameter("@OrderNo", System.Data.DbType.String);
                p2.Value = string.Format("%{0}%", orderNo);
                s1 += " And orderNo like @OrderNo ";
                parameterList.Add(p2);
            }
            if (!string.IsNullOrEmpty(state))
            {
                SQLiteParameter p2 = new SQLiteParameter("@State", System.Data.DbType.String);
                p2.Value = state;
                s2 += " And State = @State ";
                parameterList.Add(p2);
            }
            if (userid > 0)
            {
                SQLiteParameter p1 = new SQLiteParameter("@UserID", System.Data.DbType.Int64);
                p1.Value = userid;
                parameterList.Add(p1);
                s3 += " And UserID=@UserID ";
            }
            sqlStr = string.Format(sqlStr, s1, s2, s3);
            DataSet ds = ExecuteDataSet(sqlStr, parameterList.ToArray());
            record = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
            return ds.Tables[0];
        }
    }
}