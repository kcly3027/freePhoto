using freePhoto.Web.AppCode;
using System;
using System.Collections.Generic;
using System.Web;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web.Admin
{
    /// <summary>
    /// admin 的摘要说明
    /// </summary>
    public class admin : BaseHandler
    {
        string PDFFilePath = string.Empty;
        string OriginalFilePath = string.Empty;
        string ImgFilePath = string.Empty;
        AdminBasePage AdminPage = new AdminBasePage();
        protected override void ProcessFunction()
        {
            PDFFilePath = Context.Server.MapPath("/convertpdf");
            OriginalFilePath = Context.Server.MapPath("/upfile");
            ImgFilePath = Context.Server.MapPath("/convertimg");

            string action = Context.Request["action"];
            if (string.IsNullOrEmpty(action)) { OutPut(ToJson(new JsonResult(false, ""))); return; }
            if (AdminPage.IsLogin())
            {
                string result = "";
                switch (action)
                {
                    case "DoneOrder":
                        result = DoneOrder(Context);
                        break;
                    case "DoneAdOrder":
                        result = DoneAdOrder(Context);
                        break;
                }
                OutPut(result);
            }
            else
            {
                OutPut(ToJson(new JsonResult(false, "login")));
            }
        }
        private string DoneOrder(HttpContext context)
        {
            if (string.IsNullOrEmpty(Request["o"])) return ToJson(new JsonResult(false, "操作失败"));
            string OrderNo = Request["o"].ToLower().ToLower();
            bool result = OrderDAL.DoneOrder(OrderNo);
            if(result){
                OrderModel model = OrderDAL.GetOrder(OrderNo);
                DelFile(PDFFilePath, model.FileKey);
                DelFile(OriginalFilePath, model.FileKey);
                DelFile(ImgFilePath, model.FileKey);
            }
            return ToJson(new JsonResult(result, result ? "该订单已经完成" : "操作失败"));
        }
        private string DoneAdOrder(HttpContext context)
        {
            if (string.IsNullOrEmpty(Request["o"])) return ToJson(new JsonResult(false, "操作失败"));
            string OrderNo = Request["o"].ToLower().ToLower();
            bool result = AdOrderDAL.DoneOrder(OrderNo);
            if (result)
            {
                AdOrderModel model = AdOrderDAL.GetOrder(OrderNo);
                DelFile(PDFFilePath, model.FileKey);
                DelFile(OriginalFilePath, model.FileKey);
                DelFile(ImgFilePath, model.FileKey);
            }
            return ToJson(new JsonResult(result, result ? "该订单已经完成" : "操作失败"));
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
            catch
            {
                return false;
            }
        }
    }
}