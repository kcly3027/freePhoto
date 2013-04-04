using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin.User
{
    public partial class orderinfo : AdminBasePage
    {
        protected string OrderNo = "";
        protected UserModel Model = null;
        protected Orders OrderModel = null;
        protected OrderImgs ImgModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["o"]))
            {
                OrderNo = Request["o"].ToLower().ToLower();
                OrderModel = OrderDAL.GetOrder(OrderNo);
                if (OrderModel == null) Response.Redirect("printPhoto.aspx", true);
                ImgModel = OrderDAL.GetOrderImgs(OrderNo);
                if (OrderModel == null) Response.Redirect("printPhoto.aspx", true);
                Model = UserDAL.GetModel(OrderModel.UserID);
                if (Model == null) Response.Redirect("printPhoto.aspx", true);
            }
            else
            {
                Response.Redirect("printPhoto.aspx", true);
            }
        }
    }
}