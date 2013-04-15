﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace freePhoto.Web.AppCode
{
    public class BasePage : System.Web.UI.Page
    {
        #region 消息提示对话框

        #region 显示消息提示对话框
        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="msg">提示信息</param>
        public void ShowAlert(string msg)
        {
            OutPut("<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        #endregion

        #region 显示消息提示对话框，并进行页面跳转
        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public void ShowAndRedirect(string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("location.href='{0}'", url);
            Builder.Append("</script>");
            //page.RegisterStartupScript("message", Builder.ToString());
            OutPut(Builder.ToString());

        }

        public void ShowAndRedirect(string msg, string url, bool top)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            if (top == true)
            {
                Builder.AppendFormat("top.location.href='{0}'", url);
            }
            else
            {
                Builder.AppendFormat("location.href='{0}'", url);
            }
            Builder.Append("</script>");
            // page.RegisterStartupScript("message", Builder.ToString());
            OutPut(Builder.ToString());

        }

        #endregion

        #region 输出自定义脚本信息
        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public void ResponseScript(string script)
        {
            //page.RegisterStartupScript("message", "<script language='javascript' defer>" + script + "</script>");
            OutPut("<script language='javascript' defer>" + script + "</script>");
        }

        #endregion

        #endregion

        protected void OutPut(string message)
        {
            Response.Clear();
            Response.Write(message);
            Response.Flush();
            Response.End();
        }

        protected void OutPut(bool result, string message)
        {
            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(new JsonResult(result, message), writer);
            OutPut(writer.ToString());
        }

        protected string ToJson(object obj)
        {
            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(obj, writer);
            return writer.ToString();
        }
    }
}