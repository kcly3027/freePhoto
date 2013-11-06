using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using freePhoto.Web.Controls;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.Admin.User
{
    public partial class feedbacklist : AdminBasePage
    {
        protected Int64 i = 1;
        protected Int64 PIndex = 0;
        protected Int64 PSize = 10;
        protected Int64 Record = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Int64.TryParse(Request["p"], out PIndex)) PIndex = 1;
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"].ToLower().Trim())
                {
                    case "get":
                        OutPut(RenderView(BindRep()));
                        break;
                    case "del":
                        OutPut(Del());
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
            return FeedbackDAL.GetFeedbackList((Int64)PIndex, PSize, out Record);
        }
        private string Del()
        {
            Int64 fid = 0;
            if(Int64.TryParse(Request["FID"],out fid) == false) fid = 0;
            return FeedbackDAL.Del(fid) ? "success" : "fail";
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                i++;
            }
        }

        private string RenderView(DataTable dt)
        {
            StringWriter output = new StringWriter();
            Page page = new Page();
            feedbackli li = (feedbackli)page.LoadControl("~/Controls/feedbackli.ascx");
            li.DataSource = dt;
            li.i = ((Int64) PIndex - 1) * PSize + 1;
            page.Controls.Add(li);
            HttpContext.Current.Server.Execute(page, output, false);
            return output.ToString();
        }
    }
}