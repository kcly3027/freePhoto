using freePhoto.Web.AppCode;
using System;
using System.Collections.Generic;
using System.Web;
using freePhoto.Web.DbHandle;
using System.Data;

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
                    case "ClearFile":
                        result = ClearFile(Context);
                        break;
                    case "ClearFile1":
                        result = ClearFile1(Context);
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

        /// <summary>
        /// 清理文件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string ClearFile(HttpContext context)
        {
            try
            {
                DataTable dt = OrderDAL.GetUpFile5Min();
                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        DelFile(PDFFilePath, item["FileKey"].ToString());
                        DelFile(OriginalFilePath, item["FileKey"].ToString());
                        DelFile(ImgFilePath, item["FileKey"].ToString());
                        OrderDAL.DelUpFile(item["FileKey"].ToString());
                    }
                    return ToJson(new JsonResult(true, "清理完成"));
                }
                return ToJson(new JsonResult(true, "清理完成"));
            }
            catch
            {
                return ToJson(new JsonResult(false, "清理失败"));
            }
        }
        /// <summary>
        /// 清理文件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string ClearFile1(HttpContext context)
        {
            try
            {
                DataTable dt = OrderDAL.GetUpFileYesterday();
                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        DelFile(PDFFilePath, item["FileKey"].ToString());
                        DelFile(OriginalFilePath, item["FileKey"].ToString());
                        DelFile(ImgFilePath, item["FileKey"].ToString());
                        OrderDAL.DelUpFile(item["FileKey"].ToString());
                    }
                    return ToJson(new JsonResult(true, "清理完成"));
                }
                return ToJson(new JsonResult(true, "清理完成"));
            }
            catch
            {
                return ToJson(new JsonResult(false, "清理失败"));
            }
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