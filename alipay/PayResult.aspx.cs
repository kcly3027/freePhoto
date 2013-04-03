using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserCenter.VServices.alipay
{
    public partial class PayResult : System.Web.UI.Page
    {
        protected string ErrorInfo = "";
        protected string Info = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["errorInfo"]))
            {
                ErrorInfo = Server.HtmlDecode(Request["errorInfo"]);
                Info = "";
            }
            else
            {
                if (!string.IsNullOrEmpty(Request["info"]))
                {
                    Info = Server.HtmlDecode(Request["info"]);
                    ErrorInfo = "";
                }
            }
        }
    }
}