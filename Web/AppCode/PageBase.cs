using freePhoto.Web.AppCode;
using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace freePhoto.Web
{
    public class PageBase : BasePage
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

        private bool IsGet1 = false, IsGet2 = false, IsGet3 = false, IsGet4 = false;
        private int _count1 = 0, _count2 = 0, _count3 = 0, _count4 = 0;

        /// <summary>
        /// 免费相纸
        /// </summary>
        public int FreePhoto
        {
            get
            {
                if (IsGet1 == false)
                {
                    IsGet1 = true;
                    _count1 = UserDAL.GetDonateCount(CurrentUser.UserID, "FreePhoto");
                }
                return _count1;
            }
        }
        /// <summary>
        /// 今天使用的免费相纸
        /// </summary>
        public int FreePhotoNow
        {
            get
            {
                if (IsGet2 == false)
                {
                    IsGet2 = true;
                    _count2 = OrderDAL.GetFreeCountTotal(CurrentUser.UserID, "photo");
                }
                return _count2;
            }
        }

        /// <summary>
        /// 免费相纸
        /// </summary>
        public int NormalCount
        {
            get
            {
                if (IsGet3 == false)
                {
                    IsGet3 = true;
                    _count3 = UserDAL.Is7Login(CurrentUser.UserID) ? ConstData.Donate_Login7 : (UserDAL.Is3Login(CurrentUser.UserID) ? ConstData.Donate_Login3 : 0);
                    _count3 += ConstData.Donate_Login;
                }
                return _count3;
            }
        }
        /// <summary>
        /// 今天使用的普通纸
        /// </summary>
        public int NormalCountNow
        {
            get
            {
                if (IsGet4 == false)
                {
                    IsGet4 = true;
                    _count4 = OrderDAL.GetFreeCountTotal(CurrentUser.UserID, "normal");
                }
                return _count4;
            }
        }

        #endregion

        #region 其他

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

        #endregion
    }
}