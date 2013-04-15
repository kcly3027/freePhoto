using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using freePhoto.Web.AppCode;
using freePhoto.Web.DbHandle;

namespace freePhoto.Web
{
    public partial class AdPut : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                AddAd();
            }
            else
            {
                if (!IsPostBack)
                {
                    Repeater1.DataSource = StoreDAL.GetStoreDt();
                    Repeater1.DataBind();
                }
            }
        }

        protected void AddAd()
        {
            if (!IsLogin()) { OutPut(false, "login"); }
            int _PrintNum = 0; DateTime _AdBeginTime = DateTime.Now; DateTime _AdEndTime = DateTime.Now;
            string AdKeyWord = Request["AdKeyWord"]; if (string.IsNullOrEmpty(AdKeyWord)) goto CheckFail;
            string FileKey = Request["FileKey"]; if (string.IsNullOrEmpty(FileKey)) goto CheckFail;
            string AdBeginTime = Request["AdBeginTime"]; if (!DateTime.TryParse(AdBeginTime, out _AdBeginTime)) goto CheckFail;
            string AdEndTime = Request["AdEndTime"]; if (!DateTime.TryParse(AdEndTime, out _AdEndTime)) goto CheckFail;
            string AdStore = Request["AdStore"]; if (string.IsNullOrEmpty(AdStore)) goto CheckFail;
            string NanNvBL = Request["NanNvBL"]; if (string.IsNullOrEmpty(NanNvBL)) goto CheckFail;
            string AdName = Request["AdName"]; if (string.IsNullOrEmpty(AdName)) goto CheckFail;
            string PrintNum = Request["PrintNum"]; if (!int.TryParse(PrintNum, out _PrintNum)) goto CheckFail;
            string PrintType = Request["PrintType"]; if (string.IsNullOrEmpty(PrintType)) goto CheckFail;

            Random ran = new Random();
            int RandKey = ran.Next(10000000, 90000000);

            string orderid = CurrentUser.UserID.ToString().Insert(1, RandKey.ToString());
            int fileCount = OrderDAL.GetFileCount(FileKey);
            AdOrderModel model = new AdOrderModel();
            model.AdBeginTime = _AdBeginTime;
            model.AdEndTime = _AdEndTime;
            model.AdKeyWord = AdKeyWord;
            model.AdName = AdName;
            model.AdStore = AdStore;
            model.FileKey = FileKey;
            model.NanNvBL = NanNvBL;
            model.OrderNo = orderid;
            model.Price = GetPrice(PrintType);
            model.PrintNum = _PrintNum;
            model.PrintType = PrintType;
            model.State = "未付款";
            model.Total_fee = model.PrintNum * model.Price * fileCount;
            model.UserID = CurrentUser.UserID;

            bool result = AdOrderDAL.CreateOrder(model);
            OutPut(result, result ? orderid : "订单创建失败");

        CheckFail:
            OutPut(false, "信息不完整，订单添加失败");
        }


        private decimal GetPrice(string printType)
        {
            switch (printType.Trim().ToLower())
            {
                case "pthb":
                    return ConstData.PTHBPaper;
                case "ptcs":
                    return ConstData.PTCSPaper;
                default:
                    return ConstData.XPCSPaper;
            }
        }
    }
}