using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web
{
    public partial class loginout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string storeid = freePhoto.Tools.Cookies.RequestCookies(CommonStr.USERCOOKIEKEY, CommonStr.USERCOOKIEKEY);
            if (!string.IsNullOrEmpty(storeid)) freePhoto.Tools.Cookies.CleanCookies(CommonStr.USERCOOKIEKEY, CommonStr.COOKIEDOMAIN);
            Response.Redirect("default.aspx", true);
        }
    }
}