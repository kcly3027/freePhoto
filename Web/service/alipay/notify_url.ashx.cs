using System;
using System.Collections.Generic;
using System.Web;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.service.alipay
{
    /// <summary>
    /// notify_url 的摘要说明
    /// </summary>
    public class notify_url : IHttpHandler
    {
        HttpRequest Request = null;
        HttpResponse Response = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Request = context.Request;
            Response = context.Response;
            try
            {
                Receive(context);
            }
            catch (Exception ex)
            {
                XLog.XTrace.WriteLine(ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        //接受处理方法
        public void Receive(HttpContext context)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    string trade_no = Request.Form["trade_no"];         //支付宝交易号
                    string order_no = Request.Form["out_trade_no"];     //获取订单号
                    string total_fee = Request.Form["total_fee"];       //获取总金额
                    string subject = Request.Form["subject"];           //商品名称、订单名称
                    string body = Request.Form["body"];                 //商品描述、订单备注、描述
                    string buyer_email = Request.Form["buyer_email"];   //买家支付宝账号
                    string trade_status = Request.Form["trade_status"]; //交易状态
                    bool IsAdOrder = false;
                    try
                    {
                        if(order_no.Substring(0, 2) == "2_") IsAdOrder = true;
                        order_no = order_no.Substring(2, order_no.Length - 2);
                        if(IsAdOrder == false)
                        {
                            OrderModel model = OrderDAL.GetOrder(order_no);
                            if(model == null) Response.Write("success");
                            else
                            {
                                if(trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                                {
                                    int fee = Convert.ToInt32(Convert.ToDecimal(total_fee));
                                    bool result = OrderDAL.PayOrder(order_no, trade_no);
                                }
                            }
                        }
                        else
                        {
                            AdOrderModel model = AdOrderDAL.GetOrder(order_no);
                            if(model == null) Response.Write("success");
                            else
                            {
                                if(trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                                {
                                    int fee = Convert.ToInt32(Convert.ToDecimal(total_fee));
                                    bool result = OrderDAL.PayOrder(order_no, trade_no);
                                }
                            }
                        }
                        XLog.XTrace.WriteLine(string.Format("内部订单：【" + (IsAdOrder ? "普通订单" : "广告订单") + "】【{2}】,支付宝订单：【{0}】,返回结果：【{1}】,充值金额：【{3}】", trade_no, trade_status, order_no, total_fee));
                        Response.Write("success");
                    }
                    catch (Exception ex)
                    {
                        XLog.XTrace.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Response.Write("fail");
                }
            }
            Response.Write("fail");
        }
        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            System.Collections.Specialized.NameValueCollection coll;
            coll = Request.Form;

            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}