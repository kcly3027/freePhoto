﻿using System;
using System.Collections.Generic;
using System.Web;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.Admin
{
    public class AdminBasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (IsLogin() == false)
            {
                GoToLogin();
            }
            else
            {
                base.OnInit(e);
            }
        }

        private StoreModel currentStore = null;

        /// <summary>
        /// 当前店面
        /// </summary>
        public StoreModel CurrentStore
        {
            get
            {
                if (IsLogin() == false)
                {
                    GoToLogin();
                }
                else
                {
                    if (currentStore == null)
                    {
                        string storeid = freePhoto.Tools.Cookies.RequestCookies(CommonStr.ADMINCOOKIEKEY, CommonStr.ADMINCOOKIEKEY);
                        currentStore = StoreDAL.GetModel(Convert.ToInt64(storeid));
                        if (currentStore == null) GoToLogin();
                    }
                    return currentStore;
                }
                return null;
            }
        }

        /// <summary>
        /// 跳转到登陆页面
        /// </summary>
        public void GoToLogin()
        {
            string storeid = freePhoto.Tools.Cookies.RequestCookies(CommonStr.ADMINCOOKIEKEY, CommonStr.ADMINCOOKIEKEY);
            if (!string.IsNullOrEmpty(storeid)) freePhoto.Tools.Cookies.CleanCookies(CommonStr.ADMINCOOKIEKEY, CommonStr.COOKIEDOMAIN);

            Response.Redirect(CommonStr.ADMINLOGINURL, true);
            Response.End();
        }

        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            try
            {
                Int64 i = 0;
                string storeid = freePhoto.Tools.Cookies.RequestCookies(CommonStr.ADMINCOOKIEKEY, CommonStr.ADMINCOOKIEKEY);
                return !string.IsNullOrEmpty(storeid) && Int64.TryParse(storeid, out i);
            }
            catch
            {
                return false;
            }
        }

        protected void OutPut(string message)
        {
            Response.Clear();
            Response.Write(message);
            Response.Flush();
            Response.End();
        }
    }
}