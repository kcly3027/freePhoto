using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;

namespace freePhoto.Web
{
    public class PageBase : System.Web.UI.Page
    {
        HttpContext context = null;
        public PageBase()
        {
            context = HttpContext.Current;
        }

        public PageBase(HttpContext context)
        {
            this.context = context;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #region 基本数据

        private UserModel currentUser = null;
        public UserModel CurrentUser
        {
            get
            {
                if (IsLogin())
                {
                    if (currentUser == null)
                    {
                        string userid = freePhoto.Tools.Cookies.RequestCookies(CommonStr.USERCOOKIEKEY, CommonStr.USERCOOKIEKEY);
                        currentUser = UserDAL.GetModel(Convert.ToInt64(userid));
                    }
                    return currentUser;
                }
                return null;
            }
        }

        private StoreModel chooseStore = null;
        public StoreModel ChooseStore
        {
            get
            {
                if (IsChooseStore())
                {
                    if (chooseStore == null)
                    {
                        string storeid = freePhoto.Tools.Cookies.RequestCookies(CommonStr.USERCHOOSESTORECOOKIEKEY, CommonStr.USERCHOOSESTORECOOKIEKEY);
                        chooseStore = StoreDAL.GetModel(Convert.ToInt64(storeid));
                    }
                    return chooseStore;
                }
                return null;
            }
        }

        #endregion
        
        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            try
            {
                Int64 i = 0;
                string userid = freePhoto.Tools.Cookies.RequestCookies(CommonStr.USERCOOKIEKEY, CommonStr.USERCOOKIEKEY);
                return !string.IsNullOrEmpty(userid) && Int64.TryParse(userid, out i);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 检查是否选择店面
        /// </summary>
        /// <returns></returns>
        public bool IsChooseStore()
        {
            try
            {
                Int64 i = 0;
                string storeid = freePhoto.Tools.Cookies.RequestCookies(CommonStr.USERCHOOSESTORECOOKIEKEY, CommonStr.USERCHOOSESTORECOOKIEKEY);
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
            Response.End();
        }
    }
}