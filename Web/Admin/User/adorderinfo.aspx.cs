using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin.User
{
    public partial class adorderinfo : AdminBasePage
    {
        protected AdOrderModel Model = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["o"])) Response.Redirect("adlist.aspx", true);
            Model = AdOrderDAL.GetOrder(Request["o"].ToLower().Trim());
            if (Model == null) Response.Redirect("adlist.aspx", true);
            Repeater1.DataSource = StoreDAL.GetStoreDt(Model.AdStore);
            Repeater1.DataBind();
        }
    }
}