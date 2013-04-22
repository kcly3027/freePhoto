using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.Admin.set
{
    public partial class chart : System.Web.UI.Page
    {
        protected string ChartStr = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable Dt = OrderDAL.GetOrderChart();
            if (Dt != null && Dt.Rows.Count > 0)
            {
                ChartStr = "";
                for (int i = 0; i < 24; i++)
                {
                    DataRow[] drs = Dt.Select("t=" + i.ToString());
                    if (drs != null && drs.Length == 1)
                    {
                        ChartStr += drs[0]["c"].ToString() + ",";
                    }
                    else
                    {
                        ChartStr += "0,";
                    }
                }
                ChartStr = ChartStr.Substring(0, ChartStr.Length - 1);
            }
        }
    }
}