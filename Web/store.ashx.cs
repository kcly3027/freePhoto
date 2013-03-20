using freePhoto.Tools;
using freePhoto.Web.DbHandle;
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
                case "RegUser":
                    RegUser(context);
                    break;
                case "Login":
                    Login(context);
                    break;
                default:
                    context.Response.Write("{\"result\":false}");
                    break;
            }
        }

        private void Login(HttpContext context)
        {
            string email = context.Request["Email"];
            string pwd = context.Request["Pwd"];
            if (!string.IsNullOrEmpty(email) && Regxp.IsEmail(email))
            {
                UserModel model = null;
                if (UserDAL.LoginModel(email, pwd, out model))
                {
                    context.Session[CommonStr.USERSESSIONKEY] = model;
                    context.Response.Write("{\"result\":true,\"message\":\"登录成功\"}");
                }
                else
                {
                    context.Response.Write("{\"result\":false,\"message\":\"登录失败，请输入正确的邮箱和密码\"}");
                }
            }
            else
            {
                context.Response.Write("{\"result\":false,\"message\":\"请输入正确的邮箱！\"}");
            } 
        }

        private void ChooseStore(HttpContext context)
        {
            context.Session[CommonStr.USERCHOOSESTORE] = context.Request["storeid"].ToString();
            context.Response.Write("{\"result\":true}");
        }

        private void RegUser(HttpContext context)
        {
            string email = context.Request["Email"];
            if (!string.IsNullOrEmpty(email) && Regxp.IsEmail(email))
            {
                if (UserDAL.RegUser(email))
                {
                    UserModel model = UserDAL.GetModel(email);
                    context.Session[CommonStr.USERSESSIONKEY] = model;
                    context.Response.Write("{\"result\":true,\"message\":\"注册成功\"}");
                }
                else
                {
                    context.Response.Write("{\"result\":false,\"message\":\"注册失败\"}");
                }   
            }
            else
            {
                context.Response.Write("{\"result\":false,\"message\":\"请输入正确的邮箱！\"}");
            }            
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