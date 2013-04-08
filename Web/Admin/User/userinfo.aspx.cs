﻿using freePhoto.Web.Controls;
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
    public partial class userinfo : AdminBasePage
    {
        protected Int64 UserID = 0;
        protected UserModel Model = null;
        protected Int64 PSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Int64.TryParse(Request["u"], out UserID)) Response.Redirect("list.aspx", true);
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"].ToLower().Trim())
                {
                    case "getp1":
                        OutPut(RenderView(Bind1()));
                        break;
                }
            }
            else
            {
                Model = UserDAL.GetModel(UserID);
                if (Model == null) Response.Redirect("list.aspx", true);
                Repeater1.DataSource = Bind1();
                Repeater1.DataBind();
            }
        }

        protected Int64 Record1 = 0;
        private DataTable Bind1()
        {
            UInt64 p1 = 0;
            if (!UInt64.TryParse(Request["p1"], out p1) || p1 <= 0) p1 = 1;
            return OrderDAL.GetOrderList1(UserID, IsMain ? 0 : CurrentStore.StoreID, "", "", (Int64)p1, PSize, out Record1);
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