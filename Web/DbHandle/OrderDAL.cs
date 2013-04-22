using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web;

namespace freePhoto.Web.DbHandle
{
    public class OrderModel
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 店面id
        /// </summary>
        public Int64 StoreID { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public Int64 UserID { get; set; }
        /// <summary>
        /// 文件key
        /// </summary>
        public string FileKey { get; set; }
        /// <summary>
        /// 文件类型 jpg,doc,docx
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// 文件原名
        /// </summary>
        public string FileOldName { get; set; }
        /// <summary>
        /// 文件页数
        /// </summary>
        public int FileCount { get; set; }
        /// <summary>
        /// 打印份数
        /// </summary>
        public int PrintNum { get; set; }
        /// <summary>
        /// 打印类型，照片纸，普通纸
        /// </summary>
        public string PrintType { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal Total_fee { get; set; }
        /// <summary>
        /// 取货人
        /// </summary>
        public string Person { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 订单创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 支付宝编号
        /// </summary>
        public string AlipayNo { get; set; }
        /// <summary>
        /// 支付日期
        /// </summary>
        public DateTime PayDate { get; set; }
        /// <summary>
        /// 免费张数
        /// </summary>
        public int FreeCount { get; set; }
        /// <summary>
        /// 收费张数
        /// </summary>
        public int PayCount { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 状态，未付款，已付款，已取件
        /// </summary>
        public string State { get; set; }
    }

    /*
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
    */

    public class OrderDAL : BaseDAL
    {
        /// <summary>
        /// 创建文件上传历史
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CreateUpfileHistory(string fileKey, string fileName, int filecount, string fileExt, string oldFileName)
        {
            string sqlStr = @"Insert Into UpFileHistory ([FileKey],[FileName],[FileCount],[FileExt],[FileOldName],[UpTime]) 
                              values(@FileKey,@FileName,@FileCount,@FileExt,@FileOldName,datetime('now','localtime'));";

            SQLiteParameter parameter1 = new SQLiteParameter("@FileKey", System.Data.DbType.String);
            parameter1.Value = fileKey;
            SQLiteParameter parameter2 = new SQLiteParameter("@FileName", System.Data.DbType.String);
            parameter2.Value = fileName;
            SQLiteParameter parameter3 = new SQLiteParameter("@FileCount", System.Data.DbType.Int32);
            parameter3.Value = filecount;
            SQLiteParameter parameter4 = new SQLiteParameter("@FileExt", System.Data.DbType.String);
            parameter4.Value = fileExt;
            SQLiteParameter parameter5 = new SQLiteParameter("@FileOldName", System.Data.DbType.String);
            parameter5.Value = oldFileName;
            bool result = ExecuteNonQuery(sqlStr, parameter1, parameter2, parameter3, parameter4, parameter5) > 0;
            return result;
        }

        /// <summary>
        /// 获取5分钟内未使用的文件
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUpFile5Min()
        {
            string sqlStr = @"select * from UpFileHistory where UpTime < datetime('now','-5 minute','localtime')";
            DataSet ds = ExecuteDataSet(sqlStr);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取昨天以前未使用的文件
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUpFileYesterday()
        {
            string sqlStr = @"select * from UpFileHistory where UpTime < datetime('now','-1 day','localtime')";
            DataSet ds = ExecuteDataSet(sqlStr);
            return ds.Tables[0];
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="fileKey"></param>
        /// <returns></returns>
        public static bool DelUpFile(string fileKey)
        {
            string sqlStr = @"delete from UpFileHistory where fileKey=@fileKey;";

            SQLiteParameter parameter1 = new SQLiteParameter("@fileKey");
            parameter1.Value = fileKey;
            parameter1.DbType = System.Data.DbType.String;
            return ExecuteNonQuery(sqlStr, parameter1) >0;
        }

        /// <summary>
        /// 获取文件页数
        /// </summary>
        /// <param name="fileKey"></param>
        /// <returns></returns>
        public static int GetFileCount(string fileKey)
        {
            string sqlStr = @"select FileCount from UpFileHistory where FileKey=@FileKey order by UpTime desc LIMIT 2;";
            SQLiteParameter parameter1 = new SQLiteParameter("@FileKey", System.Data.DbType.String);
            parameter1.Value = fileKey;
            object result = ExecuteScalar(sqlStr, parameter1);
            return result == null ? 0 : Convert.ToInt32(result);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool CreateOrder(OrderModel model)
        {
            try
            {
                string sqlStr = @"
                            Insert Into Orders ([OrderNo],[StoreID],[UserID],[FileKey],[FileType],[FileOldName],[FileCount],[PrintNum],[PrintType],[Total_fee],[Person],[Mobile],[Address],[CreateDate],[FreeCount],[PayCount],[Price],[State]) 
                                    Select @OrderNo,@StoreID,@UserID,@FileKey,FileExt,FileOldName,FileCount,@PrintNum,@PrintType,@Total_fee,@Person,@Mobile,@Address,datetime('now','localtime'),@FreeCount,@PayCount,@Price,@State
                                        From UpFileHistory Where FileKey=@FileKey;
                            Delete From UpFileHistory Where FileKey=@FileKey;";

                List<SQLiteParameter> list = GetEntityParas<OrderModel>(model);
                int result = ExecuteNonQuery(sqlStr, list.ToArray());
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderno"></param>
        public static void DelOrder(Int64 userid, string orderno)
        {
            string sqlStr = @"delete from orders where orderno=@orderno and userid=@userid;";

            SQLiteParameter parameter = new SQLiteParameter("@userid");
            parameter.Value = userid;
            parameter.DbType = System.Data.DbType.Int64;
            SQLiteParameter parameter1 = new SQLiteParameter("@orderno");
            parameter1.Value = orderno;
            parameter1.DbType = System.Data.DbType.String;
            ExecuteNonQuery(sqlStr, parameter1, parameter);
        }
        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderno"></param>
        public static bool DoneOrder(string orderno)
        {
            string sqlStr = @"update orders set [state]='已完成' where orderno=@orderno;";
            SQLiteParameter parameter1 = new SQLiteParameter("@orderno");
            parameter1.Value = orderno;
            parameter1.DbType = System.Data.DbType.String;
            return ExecuteNonQuery(sqlStr, parameter1) > 0;
        }

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public static OrderModel GetOrder(string orderno)
        {
            string sqlStr = @"select * from orders where orderno=@orderno order by CreateDate desc LIMIT 2;";
            SQLiteParameter parameter1 = new SQLiteParameter("@orderno",System.Data.DbType.String);
            parameter1.Value = orderno;
            return ConvertEntity<OrderModel>(ExecuteReader(sqlStr, parameter1), true);
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
            string sqlStr = @"Select * From Orders Where 1=1 {2} {0} {1} Order By CreateDate Desc limit @s offset @e ; Select Count(1) From Orders Where 1=1 {2} {0} {1}";
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
        public static DataTable GetOrderList1(Int64 userid, Int64 storeid, string orderNo, string state, Int64 pIndex, Int64 pSize, out Int64 record)
        {
            string s1 = "", s2 = "", s3 = "", s4 = "";
            string sqlStr = @"select Orders.*,Users.Email from Orders,Users
                            where Orders.userid=Users.UserID and 1=1 {2} {3} {0} {1}  Order By CreateDate Desc limit @s offset @e ; 
                            Select Count(1) From Orders Where 1=1 {2} {3} {0} {1}";
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
                s1 += " And Orders.orderNo like @OrderNo ";
                parameterList.Add(p2);
            }
            if (!string.IsNullOrEmpty(state))
            {
                SQLiteParameter p2 = new SQLiteParameter("@State", System.Data.DbType.String);
                p2.Value = state;
                s2 += " And Orders.State = @State ";
                parameterList.Add(p2);
            }
            if (userid > 0)
            {
                SQLiteParameter p1 = new SQLiteParameter("@UserID", System.Data.DbType.Int64);
                p1.Value = userid;
                parameterList.Add(p1);
                s3 += " And Orders.UserID=@UserID ";
            }
            if (storeid > 0)
            {
                SQLiteParameter p1 = new SQLiteParameter("@StoreID", System.Data.DbType.Int64);
                p1.Value = storeid;
                parameterList.Add(p1);
                s3 += " And Orders.StoreID=@StoreID ";
            }
            sqlStr = string.Format(sqlStr, s1, s2, s3, s4);
            DataSet ds = ExecuteDataSet(sqlStr, parameterList.ToArray());
            record = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取今日使用的免费纸张数量
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="printType">photo，normal</param>
        /// <returns></returns>
        public static int GetFreeCountTotal(Int64 userid, string printType)
        {
            string sqlStr = @"Select Sum(FreeCount) As FreeCount From Orders Where UserID=@UserID And PrintType=@PrintType 
            And datetime(CreateDate,'start of day','localtime') =  datetime('now','start of day','localtime');";
            SQLiteParameter parameter1 = new SQLiteParameter("@UserID", System.Data.DbType.Int64);
            parameter1.Value = userid;
            SQLiteParameter parameter2 = new SQLiteParameter("@PrintType", System.Data.DbType.String);
            parameter2.Value = printType;

            object result = ExecuteScalar(sqlStr, parameter1, parameter2);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// 获取订单统计
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOrderChart()
        {
            string sqlStr = @"select strftime('%H',CreateDate) as t,count(OrderNo) as c from Orders GROUP BY strftime('%H',CreateDate);";
            
            DataSet ds = ExecuteDataSet(sqlStr);
            return ds.Tables[0];
        }
    }

    public class OrderTools : BaseDAL
    {
        /// <summary>
        /// 获取预览路径
        /// </summary>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        public static string GetPreview(string fileExt, string filekey)
        {
            string[] imgTypes = new string[] { "jpg", "jpeg", "png", "gif" };
            string[] wordTypes = new string[] { "doc", "docx" };
            fileExt = fileExt.Substring(1);
            bool IsImage = Array.IndexOf(imgTypes, fileExt) != -1;
            bool IsWord = Array.IndexOf(wordTypes, fileExt) != -1;
            if (IsImage) return "/previewimg.aspx?file=" + filekey + "." + fileExt;
            if (IsWord) return "/previewpdf.aspx?file=" + filekey + ".pdf";
            return "";
        }

        public static bool IsImage(string fileExt)
        {
            string[] imgTypes = new string[] { "jpg", "jpeg", "png", "gif" };
            fileExt = fileExt.Substring(1);
            bool IsImage = Array.IndexOf(imgTypes, fileExt) != -1;
            return IsImage;
        }

        public static bool IsWord(string fileExt)
        {
            string[] wordTypes = new string[] { "doc", "docx" };
            fileExt = fileExt.Substring(1);
            bool IsWord = Array.IndexOf(wordTypes, fileExt) != -1;
            return IsWord;
        }
    }
}