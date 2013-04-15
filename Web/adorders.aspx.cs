using freePhoto.Web.Controls;
using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web
{
    public partial class adorders : PageBase
    {
        protected Int64 PSize = 5;
        protected string OrderNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["o"])) OrderNo = Request["o"].ToString().Trim();
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"].ToLower().Trim())
                {
                    case "getp1":
                        OutPut(RenderView(Bind1()));
                        break;
                    case "getp2":
                        OutPut(RenderView(Bind2()));
                        break;
                    case "getp3":
                        OutPut(RenderView(Bind3()));
                        break;
                }
            }
            else
            {
                Repeater1.DataSource = Bind1();
                Repeater1.DataBind();

                Repeater2.DataSource = Bind2();
                Repeater2.DataBind();

                Repeater3.DataSource = Bind3();
                Repeater3.DataBind();
            }
        }

        protected Int64 Record1 = 0;
        private DataTable Bind1()
        {
            UInt64 p1 = 0;
            if (!UInt64.TryParse(Request["p1"], out p1) || p1 <= 0) p1 = 1;
            return AdOrderDAL.GetOrderList(CurrentUser.UserID, OrderNo, "", (Int64)p1, PSize, out Record1);
        }
        protected Int64 Record2 = 0;
        private DataTable Bind2()
        {
            UInt64 p2 = 0;
            if (!UInt64.TryParse(Request["p2"], out p2) || p2 <= 0) p2 = 1;
            return AdOrderDAL.GetOrderList(CurrentUser.UserID, OrderNo, "未付款", (Int64)p2, PSize, out Record2);
        }
        protected Int64 Record3 = 0;
        private DataTable Bind3()
        {
            UInt64 p3 = 0;
            if (!UInt64.TryParse(Request["p3"], out p3) || p3 <= 0) p3 = 1;
            return AdOrderDAL.GetOrderList(CurrentUser.UserID, OrderNo, "已付款", (Int64)p3, PSize, out Record3);
        }
        private string RenderView(DataTable dt)
        {
            StringWriter output = new StringWriter();
            Page page = new Page();
            adorderli li = (adorderli)page.LoadControl("~/Controls/adorderli.ascx");
            li.DataSource = dt;
            page.Controls.Add(li);
            HttpContext.Current.Server.Execute(page, output, false);
            return output.ToString();
        }
    }
}