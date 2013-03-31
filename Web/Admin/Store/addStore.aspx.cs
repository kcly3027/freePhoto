using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin
{
    public partial class addStore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            StoreModel model = new StoreModel();
            model.StoreName = txt_shopname.Text;
            model.LoginPwd = txt_pwd.Text;
            model.LoginName = txt_username.Text;
            model.BaiduMap = hid_address.Value;
            model.Address = txt_address.Text;
            model.AddTime = DateTime.Now;

            bool result = StoreDAL.RegStoreInfo(model);
        }
    }
}