using System;
using System.Collections.Generic;
using MobConcept.Toolkit;
using MobConcept.Model.Core;
using Web.UserCenter.VServices.alipay;
using MobConcept.UserCenter.App_Core.VServicesBLL;

namespace Web.UserCenter.VServices.alipay
{
    public partial class OpenDefault : System.Web.UI.Page
    {
        public int Number = 10;
        public string paymethod = "";
        public string defaultbank = "";
        public string tradeNo = "";
        public string PageError = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            int userID = 0;
            if (Int32.TryParse(Request["uid"], out userID))
            {
                if (Request["ispost"] != null)
                {
                    if (!string.IsNullOrEmpty(Request.Form["pay_bank"]))
                    {
                        if (Request.Form["pay_bank"] == "directPay")
                        {
                            paymethod = "directPay";
                        }
                        else
                        {
                            paymethod = "bankPay";
                            defaultbank = Request.Form["pay_bank"];
                        }
                    }

                    int number = 0;
                    if (CheckNumber(out number))
                    {
                        tradeNo = Web.UserCenter.VServices.alipay.IdentityGenerator.Instance.NextIdentity();
                        if (PayOrderBLL.AddPayOrder(userID, tradeNo, number))
                        {
                            if (paymethod == "directPay") defaultbank = "defaultbank";
                            //把请求参数打包成数组
                            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                            sParaTemp.Add("payment_type", "1");
                            sParaTemp.Add("out_trade_no", tradeNo);
                            sParaTemp.Add("subject", "魔云服务");
                            sParaTemp.Add("body", "魔云服务业务");//商品描述
                            sParaTemp.Add("total_fee", number.ToString());
                            sParaTemp.Add("paymethod", paymethod);
                            if (defaultbank != "" && paymethod != "directPay") sParaTemp.Add("defaultbank", defaultbank);
                            Service ali = new Service();
                            string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);
                            Response.Write("正在跳往支付宝。。。" + sHtmlText);
                        }
                        else
                        {
                            PageError = "未生成订单";
                        }
                    }
                    else
                    {
                        PageError = "请填入正确的充值金额";
                    }
                }
                else
                {
                    PageError = "未生成订单";
                }
                if (!string.IsNullOrEmpty(PageError))
                {
                    Response.Redirect("/service/OpenPay.aspx?error=" + Server.HtmlEncode(PageError));
                }
            }
            else
            {
                PageError = "用户ID不存在";
            }
        }

        public bool CheckNumber(out int tempNumber)
        {
            tempNumber = 0;
            if (string.IsNullOrEmpty(Request["TxtTotal_fee"])) return false;
            if (!Int32.TryParse(Request["TxtTotal_fee"], out tempNumber)) return false;
            if (tempNumber <= 0) return false;
            return true;
        }
    }
}