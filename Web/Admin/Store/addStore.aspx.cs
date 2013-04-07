using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin
{
    public partial class addStore : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            StoreModel model = new StoreModel();
            if (!string.IsNullOrEmpty(txt_username.Text)) model.LoginName = txt_username.Text;
            if (!string.IsNullOrEmpty(txt_shopname.Text)) model.StoreName = txt_shopname.Text;
            if (!string.IsNullOrEmpty(hid_address.Value)) model.BaiduMap = hid_address.Value;
            if (!string.IsNullOrEmpty(txt_address.Text)) model.Address = txt_address.Text;
            if (!string.IsNullOrEmpty(txt_pwd.Text)) model.LoginPwd = txt_pwd.Text;
            model.AddTime = DateTime.Now;
            bool result = StoreDAL.RegStoreInfo(model);
            if (result)
                ShowAlert("店面添加成功！");
            else
                ShowAlert("店面添加失败！");
        }
    }
}