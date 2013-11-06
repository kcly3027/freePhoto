using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.Admin.set
{
    public partial class siteoption : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txt_left.Text = OptionDAL.GetOptionValue("index_left_1");
                txt_right.Text = OptionDAL.GetOptionValue("index_right_1");
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if(
            OptionDAL.InsertOption("index_left_1", txt_left.Text.Trim()) &&
            OptionDAL.InsertOption("index_right_1", txt_right.Text.Trim()))
                ShowAlert("保存成功！");
            else
                ShowAlert("保存失败！");
        }
    }
}