using System;
using System.Collections.Generic;
using System.Web;

namespace freePhoto.Web
{
    public class CommonStr
    {
        /// <summary>
        /// 管理页面seessionkey
        /// </summary>
        public const string ADMINSESSIONKEY = "A_Store";
        /// <summary>
        /// 管理页面跳转页面
        /// </summary>
        public const string ADMINLOGINURL = "/Admin/Login.aspx";
        /// <summary>
        /// 用户登录key
        /// </summary>
        public const string USERSESSIONKEY = "U_Store";
        /// <summary>
        /// 用户选择店面
        /// </summary>
        public const string USERCHOOSESTORE = "C_Store";
    }
}