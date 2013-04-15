using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web
{
    public partial class adinfo : PageBase
    {
        protected AdOrderModel Model = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin()) Response.Redirect("default.aspx", true);
            if (string.IsNullOrEmpty(Request["o"])) Response.Redirect("AdPut.aspx", true);
            Model = AdOrderDAL.GetOrder(Request["o"].ToLower().Trim());
            if (Model == null) Response.Redirect("AdPut.aspx", true);
            Repeater1.DataSource = StoreDAL.GetStoreDt(Model.AdStore);
            Repeater1.DataBind();
        }
    }
}