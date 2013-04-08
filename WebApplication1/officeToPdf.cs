using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace WebApplication1
{
    public class officeToPdf
    {
        public void ToPdf(string path, string sPath)
        {
            string tempPath = System.IO.Path.GetTempPath();

            //   if (Directory.Exists(tempPath))
            //   {
            //       Directory.CreateDirectory(tempPath);
            //   }
            object tempFileName = path;
            object savePath = sPath;
            FileInfo fi = new FileInfo(path);
            string astdt = fi.Extension;
            object strFileName = fi.Name;
            object flg = false;
            object oMissing = System.Reflection.Missing.Value;
            switch (astdt.ToLower())
            {
                case ".doc":
                case ".docx":
                    astdt = "pdf";
                    Microsoft.Office.Interop.Word._Application oWord;
                    Microsoft.Office.Interop.Word._Document oDoc;
                    oWord = new Microsoft.Office.Interop.Word.Application();
                    //oWord.Visible = true;
                    //oDoc = oWord.Documents.Open(ref tempFileName,
                    //ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    //ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    //ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    oDoc = oWord.Documents.Open(ref tempFileName);
                    XLog.XTrace.WriteLine(oWord == null ? "1" : "2");
                    XLog.XTrace.WriteLine(oDoc == null ? "1" : "2");
                    XLog.XTrace.WriteLine(tempFileName.ToString());
                    try
                    {
                        // 计算Word文档页数
                        Microsoft.Office.Interop.Word.WdStatistic stat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
                        int num = oDoc.ComputeStatistics(stat, ref oMissing);
                        // if (!Directory.Exists(savePath.ToString()))
                        // {
                        //     Directory.CreateDirectory(savePath.ToString());
                        // }
                        savePath = sPath;//+ strFileName + "." + astdt;
                        object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                        oDoc.SaveAs(ref savePath, ref format,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    }
                    catch (Exception ex)
                    {
                        if (oDoc == null) XLog.XTrace.WriteLine(ex.Message);
                        throw (ex);
                    }
                    finally
                    {
                        oDoc.Close(ref flg, ref oMissing, ref oMissing);
                        oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                    }
                    break;
                case ".xls":
                case ".xlsx":
                    astdt = "pdf";
                    Microsoft.Office.Interop.Excel._Application oExcel;
                    oExcel = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook book = oExcel.Workbooks.Open(tempFileName.ToString(),
                    oMissing, true, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing, true, oMissing,
                    oMissing, oMissing, oMissing, oMissing);
                    try
                    {
                        //oExcel.Visible = true; 
                        Microsoft.Office.Interop.Excel.Sheets m_objSheets = (Microsoft.Office.Interop.Excel.Sheets)book.Worksheets;
                        Microsoft.Office.Interop.Excel.Worksheet m_objSheet = (Microsoft.Office.Interop.Excel.Worksheet)(m_objSheets.get_Item(1));

                        Microsoft.Office.Interop.Excel.Range range = m_objSheet.get_Range("A1", oMissing);

                        range = range.get_Resize(1, 1);
                        object objValue = range.get_Value(oMissing);

                        if (objValue == null)
                        {
                            m_objSheet.Cells[1, 1] = " ";
                        }

                        //if (!Directory.Exists(savePath.ToString()))
                        //{
                        //    Directory.CreateDirectory(savePath.ToString());
                        //}
                        savePath = sPath;// +strFileName + "." + astdt;
                        book.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF,
                                savePath, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                        book.Close(flg, oMissing, oMissing);
                        oExcel.Quit();
                    }
                    catch (Exception ex)
                    {
                        book.Close(flg, oMissing, oMissing);
                        oExcel.Quit();
                        throw (ex);
                    }
                    break;
                case ".ppt":
                case ".pptx":
                    astdt = "pdf";
                    Microsoft.Office.Interop.PowerPoint.Application ppApp = new Microsoft.Office.Interop.PowerPoint.Application();
                    try
                    {
                        Microsoft.Office.Interop.PowerPoint.Presentation presentation = ppApp.Presentations.Open(tempFileName.ToString(),
                                                        Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse,
                                                        Microsoft.Office.Core.MsoTriState.msoFalse);
                        if (presentation.Slides.Count < 1)
                        {
                            presentation.Slides.Add(1, Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank);
                        }
                        if (ppApp.Presentations.Count < 0)
                        {
                            ppApp.Quit();
                            return;
                        }
                        if (ppApp.Presentations[ppApp.Presentations.Count].Slides.Count < 1)
                        {
                            return;
                        }

                        //if (!Directory.Exists(savePath.ToString()))
                        //{
                        //    Directory.CreateDirectory(savePath.ToString());
                        //}
                        savePath = sPath;// +strFileName + "." + astdt;
                        ppApp.Presentations[ppApp.Presentations.Count].SaveAs(savePath.ToString(), Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF, Microsoft.Office.Core.MsoTriState.msoTrue);
                        ppApp.Presentations[ppApp.Presentations.Count].Close();
                        ppApp.Quit();
                    }
                    catch (Exception ex)
                    {
                        ppApp.Presentations[ppApp.Presentations.Count].Close();
                        ppApp.Quit();
                        throw (ex);
                    }
                    break;
                default:
                    break;
            }

        }

        public void ToSwf(string pdfPath, string swfPath, int page)
        {
            try
            {
                string exe = HttpContext.Current.Server.MapPath("PDF2SWF/pdf2swf.exe");
                if (!File.Exists(exe))
                {
                    throw new ApplicationException("Can not find: " + exe);
                }
                StringBuilder sb = new StringBuilder();

                sb.Append(" -o \"" + swfPath + "\"");//output

                sb.Append(" -z");

                sb.Append(" -s flashversion=9");//flash version

                sb.Append(" -s disablelinks");//禁止PDF里面的链接

                sb.Append(" -p " + "1" + "-" + page);//page range

                sb.Append(" -j 100");//Set quality of embedded jpeg pictures to quality. 0 is worst (small), 100 is best (big). (default:85)

                sb.Append(" \"" + pdfPath + "\"");//input

                System.Diagnostics.Process proc = new System.Diagnostics.Process();

                proc.StartInfo.FileName = exe;

                proc.StartInfo.Arguments = sb.ToString();

                proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                proc.Start();

                proc.WaitForExit();

                proc.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetPageCount(string pdfPath)
        {
            try
            {
                byte[] buffer = File.ReadAllBytes(pdfPath);

                int length = buffer.Length;

                if (buffer == null)

                    return -1;

                if (buffer.Length <= 0)

                    return -1;

                string pdfText = Encoding.Default.GetString(buffer);

                System.Text.RegularExpressions.Regex rx1 = new System.Text.RegularExpressions.Regex(@"/Type\s*/Page[^s]");

                System.Text.RegularExpressions.MatchCollection matches = rx1.Matches(pdfText);

                return matches.Count;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
