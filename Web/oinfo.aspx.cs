using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web
{
    public partial class oinfo : PageBase
    {
        protected string SelectAddress = "";
        protected string StoreAddress = "";
        protected OrderModel Model = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin() || !IsChooseStore()) Response.Redirect("default.aspx", true);
            if (string.IsNullOrEmpty(Request["o"])) Response.Redirect("default.aspx", true);
            Model = OrderDAL.GetOrder(Request["o"].ToLower().Trim());
            if (Model == null) Response.Redirect("default.aspx", true);
            SelectAddress = StoreDAL.GetModel(Model.StoreID).Address;
        }
    }
}