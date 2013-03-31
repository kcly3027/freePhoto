using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin
{
    public partial class loginout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string storeid = freePhoto.Tools.Cookies.RequestCookies(CommonStr.ADMINCOOKIEKEY, CommonStr.ADMINCOOKIEKEY);
            if (!string.IsNullOrEmpty(storeid)) freePhoto.Tools.Cookies.CleanCookies(CommonStr.ADMINCOOKIEKEY, CommonStr.ADMINCOOKIEKEY);
            //Response.Redirect("default.aspx", true);
        }
    }
}