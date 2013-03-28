using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web
{
    public partial class orders : PageBase
    {
        protected Int64 PSize = 5;
        protected Int64 Records = 0;
        protected string OrderNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["o"])) OrderNo = Request["o"].ToString().Trim();
            Bind1();
            Bind2();
            Bind3();
        }

        private void Bind1()
        {
            UInt64 p1 = 0;
            if (!UInt64.TryParse(Request["p1"], out p1) || p1 <= 0) p1 = 1;
            Repeater1.DataSource = OrderDAL.GetOrderList(CurrentUser.UserID, OrderNo, "", (Int64)p1, PSize, out Records);
            Repeater1.DataBind();
        }
        private void Bind2()
        {
            UInt64 p2 = 0;
            if (!UInt64.TryParse(Request["p2"], out p2) || p2 <= 0) p2 = 1;
            Repeater2.DataSource = OrderDAL.GetOrderList(CurrentUser.UserID, OrderNo, "未付款", (Int64)p2, PSize, out Records);
            Repeater2.DataBind();
        }
        private void Bind3()
        {
            UInt64 p3 = 0;
            if (!UInt64.TryParse(Request["p3"], out p3) || p3 <= 0) p3 = 1;
            Repeater3.DataSource = OrderDAL.GetOrderList(CurrentUser.UserID, OrderNo, "已付款", (Int64)p3, PSize, out Records);
            Repeater3.DataBind();
        }
    }
}