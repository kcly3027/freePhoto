using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Controls
{
    public partial class userli : System.Web.UI.UserControl
    {
        public Int64 i { get; set; }
        public DataTable DataSource { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Repeater1.DataSource = DataSource;
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