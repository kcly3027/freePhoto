using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                Login();
            }
        }
        private void Login()
        {
            if (string.IsNullOrEmpty(Request["$username"])) { OutPut(false, "请输入用户名"); return; }
            if (string.IsNullOrEmpty(Request["$pwd"])) { OutPut(false, "请输入密码"); return; }
            StoreModel model = null;
            bool result = StoreDAL.LoginModel(Request["$username"].ToLower().Trim(), Request["$pwd"].ToLower().Trim(), out model);
            if (result)
                freePhoto.Tools.Cookies.ResponseCookies(CommonStr.ADMINCOOKIEKEY, model.StoreID.ToString(), 0, CommonStr.ADMINCOOKIEKEY);
            OutPut(result, result ? "" : "用户名或密码错误");
        }

        protected void OutPut(bool result, string message)
        {
            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(new JsonResult(result, message), writer);
            Response.Clear();
            Response.Write(writer.ToString());
            Response.Flush();
            Response.End();
        }
    }
}