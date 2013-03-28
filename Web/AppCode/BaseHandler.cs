using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace freePhoto.Web.AppCode
{
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        protected HttpRequest Request = null;
        protected HttpResponse Response = null;
        protected HttpContext Context = null;

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            Context = context; Request = context.Request; Response = context.Response;
            ProcessFunction();
        }

        protected virtual void ProcessFunction()
        {
            OutPut(ToJson(false, ""));
        }

        #region OutPut

        protected string ToJson(bool result, string message)
        {
            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(new JsonResult(result, message), writer);
            return writer.ToString();
        }

        protected string ToJson(bool result, Dictionary<string, object> _obj)
        {

            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(new JsonResult(result, "", _obj), writer);
            return writer.ToString();
        }

        protected string ToJson(JsonResult model)
        {

            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(model, writer);
            return writer.ToString();
        }

        protected void OutPut(string message)
        {
            Response.Clear();
            Response.Write(message);
            Response.End();
        }

        #endregion 

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}