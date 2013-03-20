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
            if (Session[CommonStr.USERSESSIONKEY] != null)
                Session.Remove(CommonStr.USERSESSIONKEY);
            Response.Redirect("default.aspx", true);
        }
    }
}