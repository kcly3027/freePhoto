using System;
using System.Collections.Generic;
using System.Web;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.service.alipay
{
    /// <summary>
    /// return_url 的摘要说明
    /// </summary>
    public class return_url : IHttpHandler
    {
        HttpRequest Request = null;
        HttpResponse Response = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Request = context.Request;
            Response = context.Response;
            Receive(context);
            //跳转页面
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
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    string trade_no = Request.QueryString["trade_no"];              //支付宝交易号
                    string order_no = Request.QueryString["out_trade_no"];	        //获取订单号
                    string total_fee = Request.QueryString["total_fee"];            //获取总金额
                    string subject = Request.QueryString["subject"];                //商品名称、订单名称
                    string body = Request.QueryString["body"];                      //商品描述、订单备注、描述
                    string buyer_email = Request.QueryString["buyer_email"];        //买家支付宝账号
                    string trade_status = Request.QueryString["trade_status"];      //交易状态

                    object model = null;
                    if(order_no.Substring(0, 2) == "2_")
                    {
                        model = AdOrderDAL.GetOrder(order_no.Substring(2, order_no.Length - 2));
                    }
                    else
                    {
                        model = OrderDAL.GetOrder(order_no.Substring(2, order_no.Length - 2));
                    }
                     
                    if(model == null) Response.Redirect(string.Format("PayResult.aspx?errorInfo={0}", HttpUtility.UrlEncode("未找到对应的订单编号")), true);
                    //if (model.Rows[0]["AlipayStatue"].ToString() == "success") Response.Redirect("PayResult.aspx?info=true", true);
                    //if (model.Rows[0]["AlipayStatue"].ToString() == "fail") Response.Redirect("PayResult.aspx?info=false", true);
                    Response.Redirect(string.Format("~/PayResult.aspx?orderno={1}&result={0}", IsSuccess(trade_status.ToString()).ToString(), order_no), true);
                }
            }
        }
        private bool IsSuccess(string trade_status)
        {
            return trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS";
        }
        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public System.Collections.Generic.SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            System.Collections.Specialized.NameValueCollection coll;
            coll = Request.QueryString;

            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
    }
}