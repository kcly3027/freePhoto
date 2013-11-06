using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web
{
    public partial class check : PageBase
    {
        string cmd = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            cmd = Request["cmd"];
            string key = Request["key"];
            if (string.IsNullOrEmpty(cmd) || string.IsNullOrEmpty(key)) Response.Redirect("default.aspx", true);
            if (cmd == "restpwd") restpwd(key.Trim());
            if (cmd == "activeuser") activeuser(key.Trim());
        }

        private void restpwd(string key)
        {
            Int64 userid = UserDAL.GetUserByCheck(key, "restpwd");
            if (userid > 0) { UserDAL.EditPwd(userid, "123456"); UserDAL.UpdateUserByCheck(key, "restpwd"); ShowRedirect("密码已经重置为123456，请重新登录后修改密码", "/default.aspx"); }
            else
            {
                ShowRedirect("链接无效，请登录", "/default.aspx");
            }
        }

        private void activeuser(string key)
        {
            Int64 userid = UserDAL.GetUserByCheck(key, "activeuser");
            if (userid > 0) { UserDAL.CheckUser(userid); UserDAL.UpdateUserByCheck(key, "activeuser"); ShowRedirect("账户已激活，谢谢您的合作", "/default.aspx"); }
            else
            {
                ShowRedirect("链接无效，请登录", "/default.aspx");
            }
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        private void ShowRedirect(string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("location.href='{0}'", url);
            Builder.Append("</script>");
            //page.RegisterStartupScript("message", Builder.ToString());
            Response.Write(Builder.ToString());

        }
    }
}