using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;

namespace freePhoto.Web
{
    public class PageBase : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        public UserModel CurrentUser
        {
            get
            {
                if (IsLogin() == false)
                {
                    return (UserModel)Session[CommonStr.USERSESSIONKEY];
                }
                return null;
            }
        }

        public StoreModel ChooseStore
        {
            get
            {
                if (IsChooseStore())
                {
                    Int64 storeId = (Int64)Session[CommonStr.USERCHOOSESTORE];
                    return StoreDAL.GetModel(storeId);
                }
                return null;
            }
        }


        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            try
            {
                return Session[CommonStr.USERSESSIONKEY] != null;
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
                return Session[CommonStr.USERCHOOSESTORE] != null;
            }
            catch
            {
                return false;
            }
        }
    }
}