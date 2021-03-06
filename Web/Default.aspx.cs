﻿using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web
{
    public partial class _Default : PageBase
    {
        protected string StoreAddress = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = StoreDAL.GetStoreDt();
            if (dt != null && dt.Rows.Count > 0)
            {
                StoreAddress = "[";
                foreach (DataRow dr in dt.Rows)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    string[] mapLocal = dr["BaiduMap"].ToString().Split('|');
                    dic.Add("StoreID",Convert.ToInt64(dr["StoreID"]));
                    dic.Add("StoreName", dr["StoreName"].ToString());
                    dic.Add("Address", dr["Address"].ToString());
                    dic.Add("lat", mapLocal[0]);
                    dic.Add("lng", mapLocal[1]);
                    StoreAddress += ToJson(dic) + ",";
                }
                StoreAddress = StoreAddress.Substring(0, StoreAddress.Length - 1) + "]";
            }
            else
            {
                StoreAddress = "";
            }
        }
    }
}