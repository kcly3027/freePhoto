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
                case "EditUser":
                    EditUser(context);
                    break;
                case "EditPwd":
                    EditPwd(context);
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
                    freePhoto.Tools.Cookies.ResponseCookies(CommonStr.USERCOOKIEKEY, model.UserID.ToString(), 0, CommonStr.USERCOOKIEKEY);
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
            freePhoto.Tools.Cookies.ResponseCookies(CommonStr.USERCHOOSESTORECOOKIEKEY, context.Request["storeid"].ToString(), 0, CommonStr.USERCHOOSESTORECOOKIEKEY);
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
                    freePhoto.Tools.Cookies.ResponseCookies(CommonStr.USERCOOKIEKEY, model.UserID.ToString(), 0, CommonStr.USERCOOKIEKEY);
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

        private void EditUser(HttpContext context)
        {
            PageBase pageBase = new PageBase(context);
            string Address = context.Request["Address"];
            string Mobile = context.Request["Mobile"];
            string QQ = context.Request["QQ"];

            UserModel model = new UserModel();
            model.Address = Address;
            model.Mobile = Mobile;
            model.QQ = QQ;
            model.UserID = pageBase.CurrentUser.UserID;
            if (UserDAL.EditUser(model))
            {
                context.Response.Write("{\"result\":true,\"message\":\"信息设定成功\"}");
            }
            else
            {
                context.Response.Write("{\"result\":false,\"message\":\"信息设定失败\"}");
            }
        }

        private void EditPwd(HttpContext context)
        {
            PageBase pageBase = new PageBase(context);
            string Pwd = context.Request["Pwd"];

            if (!string.IsNullOrEmpty(Pwd))
            {
                if (UserDAL.EditPwd(pageBase.CurrentUser.UserID, Pwd))
                {
                    context.Response.Write("{\"result\":true,\"message\":\"密码修改成功\"}");
                }
                else
                {
                    context.Response.Write("{\"result\":false,\"message\":\"密码修改失败\"}");
                }
            }
            else
            {
                context.Response.Write("{\"result\":false,\"message\":\"密码修改失败\"}");
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