using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web
{
    public partial class oinfo : PageBase
    {
        protected string StoreAddress = "";
        protected OrderModel Model = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsChooseStore())
            {
                DataTable dt = StoreDAL.GetStoreDt();
                if (dt != null && dt.Rows.Count > 0)
                {
                    StoreAddress = "[";
                    foreach (DataRow dr in dt.Rows)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        string[] mapLocal = dr["BaiduMap"].ToString().Split('|');
                        dic.Add("StoreID", Convert.ToInt64(dr["StoreID"]));
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
            if (!IsLogin()) Response.Redirect("default.aspx", true);
            if (string.IsNullOrEmpty(Request["o"])) Response.Redirect("default.aspx", true);
            Model = OrderDAL.GetOrder(Request["o"].ToLower().Trim());
            if (Model == null) Response.Redirect("default.aspx", true);
        }
    }
}