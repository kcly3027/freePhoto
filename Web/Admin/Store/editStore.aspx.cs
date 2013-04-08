using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin.Store
{
    public partial class editStore : AdminBasePage
    {
        protected int StoreID = 0;
        protected StoreModel Model = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request["d"], out StoreID)) Response.Redirect("list.aspx", true);
            Model = StoreDAL.GetModel(StoreID);
            if (Model == null) Response.Redirect("list.aspx", true);
            if (!IsPostBack)
            {
                txt_shopname.Text = Model.StoreName;
                txt_username.Text = Model.LoginName;
                hid_address.Value = Model.BaiduMap;
                txt_address.Text = Model.Address;
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_shopname.Text)) Model.StoreName = txt_shopname.Text;
            if (!string.IsNullOrEmpty(hid_address.Value)) Model.BaiduMap = hid_address.Value;
            if (!string.IsNullOrEmpty(txt_address.Text)) Model.Address = txt_address.Text;
            if (!string.IsNullOrEmpty(txt_pwd.Text)) Model.LoginPwd = txt_pwd.Text;
            else Model.LoginPwd = "";
            bool result = StoreDAL.EditStoreInfo(Model);
            if (result)
                ShowAlert("店面编辑成功！");
            else
                ShowAlert("店面编辑失败！");
        }
    }
}