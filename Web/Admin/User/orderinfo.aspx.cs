using freePhoto.Web.DbHandle;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace freePhoto.Web.Admin.User
{
    public partial class orderinfo : AdminBasePage
    {
        protected string OrderNo = "";
        protected UserModel Model = null;
        protected OrderModel OrderModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["o"]))
            {
                OrderNo = Request["o"].ToLower().ToLower();
                OrderModel = OrderDAL.GetOrder(OrderNo);
                if (OrderModel == null) Response.Redirect("printPhoto.aspx", true);
                Model = UserDAL.GetModel(OrderModel.UserID);
                if (Model == null) Response.Redirect("printPhoto.aspx", true);
            }
            else
            {
                Response.Redirect("printPhoto.aspx", true);
            }
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                bool result = OrderDAL.DoneOrder(OrderNo);
                OutPut(ToJson(new JsonResult(result, result ? "该订单已经完成" : "操作失败")));
            }
        }

        protected void DelFile(string fileKey)
        {
            string pdf_path = Server.MapPath("/convertpdf");
            string o_path = Server.MapPath("/upfile");
            string img_path = Server.MapPath("/convertimg");
            DelFile(pdf_path, fileKey);
            DelFile(o_path, fileKey);
            DelFile(img_path, fileKey);
        }

        private bool DelFile(string dir, string fileKey)
        {
            try
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(dir);
                System.IO.FileInfo[] files = directoryInfo.GetFiles(fileKey + ".*", System.IO.SearchOption.AllDirectories);
                foreach (System.IO.FileInfo fi in files)
                {
                    fi.Delete();
                }
                return true;
            }
            catch{
                return false;
            }
        }
    }
}