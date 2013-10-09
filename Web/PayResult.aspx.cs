using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web
{
    public partial class PayResult : System.Web.UI.Page
    {
        protected string Link = "";
        protected bool IsSuccess = true;
        protected string ht = "";
        protected bool NoError = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Request["errorInfo"]) == false) { ht = Request["errorInfo"]; NoError = false; return; }
            if(string.IsNullOrEmpty(Request["result"]) == false) { IsSuccess = Request["result"] == "True"; }
            ht = IsSuccess ? "支付成功" : "支付失败";

            if(string.IsNullOrEmpty(Request["orderno"]) == false) { 
                string OrderNo = Request["OrderNo"];
                bool IsAdOrder = false;
                if(OrderNo.Substring(0, 2) == "2_") IsAdOrder = true;
                OrderNo = OrderNo.Substring(2, OrderNo.Length - 2);

                Link = IsAdOrder ? string.Format("adinfo.aspx?o={0}", OrderNo) : string.Format("oinfo.aspx?o={0}", OrderNo);
            }
        }
    }
}