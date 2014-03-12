using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin
{
    public partial class editPwd : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (!string.IsNullOrEmpty(action))
            {
                EditPwd();
            }
        }

        private void EditPwd()
        {
            string oldpwd = Request["oldpwd"],newpwd = Request["newpwd"];
            if(string.IsNullOrEmpty(oldpwd)) OutPut(false,"请输入旧密码");
            if(string.IsNullOrEmpty(newpwd)) OutPut(false,"请输入新密码");

            StoreModel model = null;
            if(StoreDAL.LoginModel(CurrentStore.LoginName,oldpwd,out model))
            {
                bool result = StoreDAL.EditPwd(CurrentStore.StoreID, newpwd);
                OutPut(result, result ? "" : "密码修改失败");
            }
            else{
                OutPut(false, "旧密码不正确");
            }
        }
    }
}