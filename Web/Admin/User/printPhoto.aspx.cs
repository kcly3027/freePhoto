using freePhoto.Web.Controls;
using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin.User
{
    public partial class printPhoto : AdminBasePage
    {
        protected Int64 PIndex = 0;
        protected Int64 PSize = 6;
        protected Int64 Record = 0;
        protected string OrderNo = "";
        protected string OType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Int64.TryParse(Request["p"], out PIndex)) PIndex = 1;
            if (!string.IsNullOrEmpty(Request["order"])) OrderNo = Request["order"].Trim();
            if (!string.IsNullOrEmpty(Request["type"])) OType = Request["type"].Trim();
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"].ToLower().Trim())
                {
                    case "get":
                        OutPut(RenderView(BindRep()));
                        break;
                }
            }
            else
            {
                Repeater1.DataSource = BindRep();
                Repeater1.DataBind();
            }
        }

        private DataTable BindRep()
        {
            return OrderDAL.GetOrderList1(0, IsMain ? 0 : CurrentStore.StoreID, OrderNo, OType, (Int64)PIndex, PSize, out Record);
        }
        private string RenderView(DataTable dt)
        {
            StringWriter output = new StringWriter();
            Page page = new Page();
            orderli1 li = (orderli1)page.LoadControl("~/Controls/orderli1.ascx");
            li.DataSource = dt;
            page.Controls.Add(li);
            HttpContext.Current.Server.Execute(page, output, false);
            return output.ToString();
        }
    }
}