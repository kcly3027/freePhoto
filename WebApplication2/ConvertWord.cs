using System;
using System.Collections.Generic;
using System.Web;
using PDFMAKERAPILib;

namespace WebApplication2
{
    public class ConvertWord
    {
        public static bool ToPdf(string sPath,string tPath)
        {
            object missing = System.Type.Missing;
            PDFMakerApp app = new PDFMakerApp();
            int result = app.CreatePDF(sPath, tPath, PDFMakerSettings.kConvertAllPages, false, true, true, missing);
            
            return false;
        }
    }
}