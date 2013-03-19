using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace freePhoto.Web
{
    /// <summary>
    /// store 的摘要说明
    /// </summary>
    public class store : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            switch (action)
            {
                case "ChooseStore":
                    ChooseStore(context);
                    break;
                default:
                    context.Response.Write("{\"result\":false}");
                    break;
            }
        }

        private void ChooseStore(HttpContext context)
        {
            context.Session[CommonStr.USERCHOOSESTORE] = context.Request["storeid"].ToString();
            context.Response.Write("{\"result\":true}");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}