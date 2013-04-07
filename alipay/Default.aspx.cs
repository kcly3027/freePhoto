using System;
using System.Collections.Generic;
using MobConcept.Toolkit;
using Web.UserCenter.VServices.alipay;
using MobConcept.Model.Core;
using MobConcept.UserCenter.App_Core.VServicesBLL;

namespace Web.UserCenter.VServices.alipay
{
    /// <summary>
    ///  生成唯一主键
    /// </summary>
    public class IdentityGenerator
    {
        static long lastIdentity = 0;
        static IdentityGenerator o = new IdentityGenerator();
        /// <summary>
        ///  实例化
        /// </summary>
        public static IdentityGenerator Instance
        {
            get
            {
                if (o != null)
                    return o;
                else
                    return new IdentityGenerator();
            }
        }
        /// <summary>
        ///  下一个主键值
        /// </summary>
        public string NextIdentity()
        {
            long idint = DateTime.Now.Ticks - new DateTime(2000, 1, 1).Ticks;
            while (lastIdentity >= idint) idint = lastIdentity + 1;
            lastIdentity = idint;
            return idint.ToString();
        }
    }//class end
    public partial class Default : Pagebase
    {
        public int Number = 10;
        public string paymethod = "";
        public string defaultbank = "";
        public string tradeNo = "";
        public string PageError = "";

        protected void Page_Load(object sender, EventArgs e)
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
                    tradeNo = IdentityGenerator.Instance.NextIdentity();
                    if (PayOrderBLL.AddPayOrder(CurrentUser.UserID, tradeNo, number))
                    {
                        if (paymethod == "directPay") defaultbank = "defaultbank";
                        /* 测试支付功能
                        PayOrder model = PayOrderBLL.GetPayOrder(tradeNo);
                        if (model == null) Response.Redirect(string.Format("PayResult.aspx?errorInfo={0}", Server.UrlEncode("未找到对应的订单编号")), true);
                        if (model.State == "success") Response.Redirect("PayResult.aspx?info=true", true);
                        if (model.State == "fail") Response.Redirect("PayResult.aspx?info=false", true);

                        if (PayOrderBLL.PayOrder(model.UserId, "success", tradeNo, tradeNo))
                        {
                            Response.Redirect("PayResult.aspx?info=true", true);
                        }
                        else
                        {
                            Response.Redirect(string.Format("PayResult.aspx?errorInfo={0}", Server.UrlEncode("订单处理失败!")), true);
                        }
                        */
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
            if (!string.IsNullOrEmpty(PageError))
            {
                Response.Redirect("/service/Pay.aspx?error=" + Server.HtmlEncode(PageError));
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