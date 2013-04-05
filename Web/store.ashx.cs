using freePhoto.Tools;
using freePhoto.Web.AppCode;
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
    public class store : BaseHandler
    {
        protected override void ProcessFunction()
        {
            string action = Context.Request["action"];
            string result = "";
            switch (action)
            {
                case "ChooseStore":
                    result = ChooseStore(Context);
                    break;
                case "RegUser":
                    result = RegUser(Context);
                    break;
                case "Login":
                    result = Login(Context);
                    break;
                case "EditUser":
                    result = EditUser(Context);
                    break;
                case "EditPwd":
                    result = EditPwd(Context);
                    break;
                case "IsLogin":
                    result = IsLogin(Context);
                    break;
                case "CreateOrder":
                    result = CreateOrder(Context);
                    break;
            }
            OutPut(result);
        }

        private string Login(HttpContext context)
        {
            string email = context.Request["Email"];
            string pwd = context.Request["Pwd"];
            if (!string.IsNullOrEmpty(email) && Regxp.IsEmail(email))
            {
                UserModel model = null;
                if (UserDAL.LoginModel(email, pwd, out model))
                {
                    freePhoto.Tools.Cookies.ResponseCookies(CommonStr.USERCOOKIEKEY, model.UserID.ToString(), 0, CommonStr.USERCOOKIEKEY);
                    return "{\"result\":true,\"message\":\"登录成功\"}";
                }
                else
                {
                    return "{\"result\":false,\"message\":\"登录失败，请输入正确的邮箱和密码\"}";
                }
            }
            else
            {
                return "{\"result\":false,\"message\":\"请输入正确的邮箱！\"}";
            } 
        }

        private string ChooseStore(HttpContext context)
        {
            freePhoto.Tools.Cookies.ResponseCookies(CommonStr.USERCHOOSESTORECOOKIEKEY, context.Request["storeid"].ToString(), 0, CommonStr.USERCHOOSESTORECOOKIEKEY);
            return "{\"result\":true}";
        }

        private string RegUser(HttpContext context)
        {
            string email = context.Request["Email"];
            if (!string.IsNullOrEmpty(email) && Regxp.IsEmail(email))
            {
                if (UserDAL.RegUser(email))
                {
                    UserModel model = UserDAL.GetModel(email);
                    freePhoto.Tools.Cookies.ResponseCookies(CommonStr.USERCOOKIEKEY, model.UserID.ToString(), 0, CommonStr.USERCOOKIEKEY);
                    return "{\"result\":true,\"message\":\"注册成功\"}";
                }
                else
                {
                    return "{\"result\":false,\"message\":\"注册失败\"}";
                }   
            }
            else
            {
                return "{\"result\":false,\"message\":\"请输入正确的邮箱！\"}";
            }            
        }

        private string EditUser(HttpContext context)
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
                return "{\"result\":true,\"message\":\"信息设定成功\"}";
            }
            else
            {
                return "{\"result\":false,\"message\":\"信息设定失败\"}";
            }
        }

        private string EditPwd(HttpContext context)
        {
            PageBase pageBase = new PageBase(context);
            string Pwd = context.Request["Pwd"];

            if (!string.IsNullOrEmpty(Pwd))
            {
                if (UserDAL.EditPwd(pageBase.CurrentUser.UserID, Pwd))
                {
                    return "{\"result\":true,\"message\":\"密码修改成功\"}";
                }
                else
                {
                    return "{\"result\":false,\"message\":\"密码修改失败\"}";
                }
            }
            else
            {
                return "{\"result\":false,\"message\":\"密码修改失败\"}";
            }
        }

        private string IsLogin(HttpContext context)
        {
            PageBase pageBase = new PageBase(context);
            return pageBase.IsLogin() ? "{\"result\":true,\"message\":\"\"}" : "{\"result\":false,\"message\":\"\"}";
        }

        private string CreateOrder(HttpContext context)
        {
            PageBase pageBase = new PageBase(context);
            if (!pageBase.IsLogin()) return ToJson(false, "login"); ; int printnum = 0;
            string fileKey = Request["fileKey"]; if (string.IsNullOrEmpty(fileKey)) goto CheckFail;
            string printtype = Request["printtype"]; if (string.IsNullOrEmpty(printtype)) goto CheckFail;
            string person = Request["person"]; if (string.IsNullOrEmpty(person)) goto CheckFail;
            string mobile = Request["mobile"]; if (string.IsNullOrEmpty(mobile)) goto CheckFail;
            string address = Request["address"]; if (string.IsNullOrEmpty(address)) goto CheckFail;
            string _printnum = Request["printnum"]; if (string.IsNullOrEmpty(_printnum) || !int.TryParse(_printnum, out printnum)) goto CheckFail;

            Random ran=new Random();
            int RandKey=ran.Next(10000000,90000000);

            string orderid = RandKey.ToString() + pageBase.ChooseStore.StoreID.ToString();
            int fileCount = OrderDAL.GetFileCount(fileKey);
            OrderModel model = new OrderModel();
            model.OrderNo = orderid;
            model.StoreID = pageBase.ChooseStore.StoreID;
            model.UserID = pageBase.CurrentUser.UserID;
            model.Person = person;
            model.Mobile = mobile;
            model.Address = address;
            model.FileKey = fileKey;
            model.PrintType = printtype;
            model.Total_fee = 0;
            model.CreateDate = DateTime.Now;
            model.FreeCount = 0;
            model.PayCount = fileCount * printnum;
            model.Price = printtype == "normal" ? Convert.ToDecimal(0.43) : Convert.ToDecimal(0.7);
            model.State = "未付款";

            bool result = OrderDAL.CreateOrder(model);
            return ToJson(result, result ? orderid : "");

        CheckFail:
            return ToJson(false, "信息不完整，订单添加失败");
        }
    }
}