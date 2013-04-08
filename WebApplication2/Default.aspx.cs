using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConvertWord.ToPdf(Server.MapPath("0322.doc"), Server.MapPath("0322.pdf"));
        }
    }
}