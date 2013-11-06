using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web
{
    public partial class addorder : PageBase
    {
        protected OrderModel OrderModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsChooseStore() == false) Response.Redirect("~/default.aspx", true);
            if(IsLogin()) OrderModel = OrderDAL.GetLastOrder(CurrentUser.UserID);
        }
    }
}