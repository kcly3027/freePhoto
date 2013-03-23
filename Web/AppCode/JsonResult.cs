using System;
using System.Collections.Generic;
using System.Web;

namespace freePhoto.Web
{
    public class JsonResult
    {
        public bool result { get; set; }
        public string message { get; set; }
        public Dictionary<string,object> obj { get; set; }
        public JsonResult() { }
        public JsonResult(bool _result, string _message) : this(_result, _message, new Dictionary<string, object>()) { }
        public JsonResult(bool _result, string _message,Dictionary<string,object> _obj)
        {
            this.result = _result;
            this.message = _message;
            this.obj = _obj;
        }
        
    }
}