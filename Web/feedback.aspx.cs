using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web
{
    public partial class feedback : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Context.Request["action"];
            if(action == "To")
            {
                if(IsLogin() == false) { OutPut("{\"result\":false,\"message\":\"login\"}"); return; }
                string content = Request["content"];
                string code = Request["code"];
                if(string.IsNullOrEmpty(code) || string.IsNullOrEmpty(content)) { OutPut("{\"result\":false,\"message\":\"内容不完整\"}"); return; }
                if(Session["ValidateCode"] != null && code.ToLower() == Session["ValidateCode"].ToString().ToLower())
                {
                    bool result = FeedbackDAL.InsertFeedback(CurrentUser.UserID, "", content.Replace("<", "&lt;").Replace(">", "&rt;"), "", "", "");
                    OutPut("{\"result\":" + (result ? "true" : "false") + ",\"message\":\"" + (result ? "反馈成功" : "反馈失败") + "\"}");
                }
                else
                {
                    OutPut("{\"result\":false,\"message\":\"请输入验证码\"}");
                    return;
                }
            }
        }

    }
}