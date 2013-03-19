using System;
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
                    return (StoreModel)Session[CommonStr.ADMINSESSIONKEY];
                }
                return null;
            }
        }

        /// <summary>
        /// 跳转到登陆页面
        /// </summary>
        public void GoToLogin()
        {
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
                return Session[CommonStr.ADMINSESSIONKEY] == null || ((StoreModel)Session[CommonStr.ADMINSESSIONKEY]).StoreID <= 0;
            }
            catch
            {
                return false;
            }
        }
    }
}