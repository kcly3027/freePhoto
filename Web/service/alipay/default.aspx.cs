using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.service.alipay
{
    public partial class _default : System.Web.UI.Page
    {
        public string tradeNo = "";
        public string PageError = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["ispost"] != null)
            {
                string OrderNo = Request["OrderNo"];
                bool IsAdOrder = false, IsExists = true;
                if(OrderNo.Substring(0, 2) == "2_") IsAdOrder = true;
                        OrderNo = OrderNo.Substring(2, OrderNo.Length - 2);


                decimal number = 0;
                if(IsAdOrder)
                {
                    AdOrderModel model = AdOrderDAL.GetOrder(OrderNo);
                    if(model == null) IsExists = false;
                    if(model != null) number = model.Total_fee;
                }
                else
                {
                    OrderModel model = OrderDAL.GetOrder(OrderNo);
                    if(model == null) IsExists = false;
                    if(model != null) number = model.Total_fee;
                }


                if (IsExists &&  number > 0)
                {
                    //把请求参数打包成数组
                    SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                    sParaTemp.Add("payment_type", "1");
                    sParaTemp.Add("out_trade_no", (IsAdOrder ? "2_" : "1_") + OrderNo);
                    sParaTemp.Add("subject", "喷嚏客打印");
                    sParaTemp.Add("body", "喷嚏客打印");//商品描述
                    sParaTemp.Add("total_fee", number.ToString());
                    sParaTemp.Add("paymethod", "directPay");
                    Service ali = new Service();
                    string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);
                    PageError = "正在跳往支付宝。。。" + sHtmlText;
                }
                else
                {
                    PageError = "订单不存在！";
                }
            }
        }
    }
}