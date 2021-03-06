﻿using System;
using System.Collections.Generic;
using System.Web;
using freePhoto.Tools;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.AppCode
{
    public class SmtpHelper
    {
        private static bool SmtpClientSend(MailMessage mail)
        {
            string FromName = fun.getapp("FromName");
            string SmtpServer = fun.getapp("SmtpServer");
            string SmtpUserName = fun.getapp("SmtpUserName");
            string SmtpPwd = fun.getapp("SmtpPwd");

            mail.From = SmtpUserName;
            mail.FromName = FromName;
            SmtpClient client = new SmtpClient();
            client.SmtpServer = SmtpServer;
            return client.Send(mail, SmtpUserName, SmtpPwd);
        }

        public static bool SendActiveMail(Int64 userid,string email)
        {
            string identity = IdentityGenerator.Instance.NextIdentity();
            MailMessage mail = new MailMessage();
            mail.Body = FileHelper.GetFileContent("RegHtml");
            mail.Body = mail.Body.Replace("$(UserName)", "用户名称");
            mail.Body = mail.Body.Replace("$(Identity)", identity);
            mail.AddRecipients(email);
            mail.Subject = "激活邮件";
            bool result = SmtpClientSend(mail);
            if (result) UserDAL.AddCheckInfo(userid, email, identity, "activeuser");
            return result;
        }
        public static bool SendCPwdMail(string email)
        {
            UserModel model = UserDAL.GetModel(email);
            if (model != null)
            {
                string identity = IdentityGenerator.Instance.NextIdentity();
                MailMessage mail = new MailMessage();
                mail.Body = FileHelper.GetFileContent("CPwdHtml");
                mail.Body = mail.Body.Replace("$(UserName)", "用户名称");
                mail.Body = mail.Body.Replace("$(Identity)", identity);
                mail.AddRecipients(model.Email);
                mail.Subject = "重置密码";
                bool result = SmtpClientSend(mail);
                if (result) UserDAL.AddCheckInfo(model.UserID, email, identity, "restpwd");
                return result;
            }
            return false;
        }
        public static bool SendSys_CPwdMail(UserModel model)
        {
            string identity = IdentityGenerator.Instance.NextIdentity();
            MailMessage mail = new MailMessage();
            mail.Body = FileHelper.GetFileContent("Sys_CPwdHtml");
            mail.Body = mail.Body.Replace("$(UserName)", "用户名称");
            mail.Body = mail.Body.Replace("$(Identity)", identity);
            mail.AddRecipients(model.Email);
            mail.Subject = "重置密码";
            bool result = SmtpClientSend(mail);
            if (result) UserDAL.AddCheckInfo(model.UserID, model.Email, identity, "restpwd");
            return result;
        }
    }
}