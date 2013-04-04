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
    public partial class list : AdminBasePage
    {
        protected Int64 i = 1;
        protected UInt64 p = 0;
        protected Int64 PSize = 20;
        protected string Email = "";
        protected Int64 Record = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UInt64.TryParse(Request["p"], out p) || p <= 0) p = 1;
            if (!string.IsNullOrEmpty(Request["email"])) Email = Request["email"].Trim();
            if (string.IsNullOrEmpty(Request["action"]))
            {
                Repeater1.DataSource = BindDt();
                Repeater1.DataBind();
            }
            else
            {
                OutPut(RenderView(BindDt()));
            }
        }

        private DataTable BindDt()
        {
            return UserDAL.GetUserList(Email, (Int64)p, PSize, out Record);
        }
        private string RenderView(DataTable dt)
        {
            StringWriter output = new StringWriter();
            Page page = new Page();
            userli li = (userli)page.LoadControl("~/Controls/userli.ascx");
            li.i = ((Int64) p - 1) * PSize + 1;
            li.DataSource = dt;
            page.Controls.Add(li);
            HttpContext.Current.Server.Execute(page, output, false);
            return output.ToString();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                i++;
            }
        }
    }
}