using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin.Store
{
    public partial class list : AdminBasePage
    {
        protected int i = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            Repeater1.DataSource = StoreDAL.GetStoreDt();
            Repeater1.DataBind();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                i++;
            }
        }
    }
}