using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web;

namespace freePhoto.Web.DbHandle
{
    public class Orders
    {
        public string OrderNo { get; set; }
        public Int64 StoreID { get; set; }
        public Int64 UserID { get; set; }
        public string Person { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string AlipayNo {get;set;}
        public DateTime CreateDate { get; set; }
        public DateTime PayDate { get; set; }
    }

    public class OrderImgs
    {
        public string OrderNo { get; set; }
        public Int64 UserID { get; set; }
        public string ImgKey { get; set; }
        public string viewPortW { get; set; }
        public string viewPortH { get; set; }
        public string imageX { get; set; }
        public string imageY { get; set; }
        public string imageRotate { get; set; }
        public string imageW { get; set; }
        public string imageH { get; set; }
        public string selectorX { get; set; }
        public string selectorY { get; set; }
        public string selectorW { get; set; }
        public string selectorH { get; set; }

    }


    public class OrderDAL : BaseDAL
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool CreateOrder(Orders model,OrderImgs imgModel)
        {
            try
            {
                string sqlStr = @"Insert Into Orders ([OrderNo],[StoreID],[UserID],[Person],[Mobile],[Address],[State],[CreateDate])
                                    values(@OrderNo,@StoreID,@UserID,@Person,@Mobile,@Address,@State,@CreateDate);";

                List<SQLiteParameter> list = GetEntityParas<Orders>(model);
                bool result = ExecuteNonQuery(sqlStr, list.ToArray()) > 0;

                sqlStr = @"Insert Into orderimgs ([OrderNo],[UserID],[ImgKey],[viewPortW],[viewPortH],[imageX],[imageY],[imageRotate],[imageW],[imageH],[selectorX],[selectorY],[selectorW],[selectorH])                      
                      values(@OrderNo,@UserID,@ImgKey,@viewPortW,@viewPortH,@imageX,@imageY,@imageRotate,@imageW,@imageH,@selectorX,@selectorY,@selectorW,@selectorH)";
                List<SQLiteParameter> list2 = GetEntityParas<OrderImgs>(imgModel);
                bool result1 = ExecuteNonQuery(sqlStr, list2.ToArray()) > 0;
                if(result == false || result1 == false) DelOrder(model.UserID,model.OrderNo);
                return (result == true) && (result1 == true);
            }
            catch
            {
                DelOrder(model.UserID, model.OrderNo);
                return false;
            }
        }

        public static void DelOrder(Int64 userid, string orderno)
        {
            string sqlStr = @"delete from orders where orderno=@orderno and userid=@userid;
                                delete from orderimgs where orderno=@orderno and userid=@userid;";

            SQLiteParameter parameter = new SQLiteParameter("@userid");
            parameter.Value = userid;
            parameter.DbType = System.Data.DbType.Int64;
            SQLiteParameter parameter1 = new SQLiteParameter("@orderno");
            parameter1.Value = userid;
            parameter1.DbType = System.Data.DbType.String;
            ExecuteNonQuery(sqlStr, parameter1, parameter);
        }

        public static DataTable GetOrderList(Int64 userid, string orderNo, string state, Int64 pIndex, Int64 pSize, out Int64 record)
        {
            string s1 = "", s2 = "";
            string sqlStr = @"Select * From Orders Where UserID=@UserID {0} {1} Order By CreateDate Desc limit @s offset @e ; Select Count(1) From Orders Where UserID=@UserID {0} {1}";
            List<SQLiteParameter> parameterList = new List<SQLiteParameter>();
            SQLiteParameter p1 = new SQLiteParameter("@UserID", System.Data.DbType.Int64);
            p1.Value = userid;
            parameterList.Add(p1);
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
            sqlStr = string.Format(sqlStr, s1, s2);
            DataSet ds = ExecuteDataSet(sqlStr, parameterList.ToArray());
            record = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
            return ds.Tables[0];
        }
    }
}