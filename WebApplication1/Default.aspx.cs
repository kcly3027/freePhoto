using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            officeToPdf op = new officeToPdf();
            string path = Server.MapPath("1.doc");
            string spath = Server.MapPath("1.pdf");
            op.ToPdf(path, spath);
            //op.ToSwf(spath, Server.MapPath("1.swf"), op.GetPageCount(spath));
        }
    }
}