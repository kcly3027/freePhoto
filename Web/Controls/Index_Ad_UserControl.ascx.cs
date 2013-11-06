using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.Controls
{
    public partial class Index_Ad_UserControl : System.Web.UI.UserControl
    {
        protected string left, right;
        protected void Page_Load(object sender, EventArgs e)
        {
            left = OptionDAL.GetOptionValue("index_left_1");
            right = OptionDAL.GetOptionValue("index_right_1");
        }
    }
}