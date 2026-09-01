using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace OfficeToPDF
{
    /// <summary>
    /// Summary description for Objects
    /// </summary>
    public class WordExportData
    {
        public string title { get; set; }
        public string fileNameTemp { get; set; }
        public string DisplayName { get; set; }
        public List<string> sqls { get; set; }
        public List<DataTable> tbls { get; set; }
        public DevExpress.XtraReports.Web.ReportViewer viewer { get; set; }

        public List<AvariablePrj.lstImage> lstImage = new List<AvariablePrj.lstImage>();
        public List<AvariablePrj.lstTextReplace> lstTextReplace = new List<AvariablePrj.lstTextReplace>();
        public List<AvariablePrj.lstFormula> lstFormula = new List<AvariablePrj.lstFormula>();
        public List<AvariablePrj.lstFontSize> lstFontSize = new List<AvariablePrj.lstFontSize>();
        public List<ICell> lstRemoveComment = new List<ICell>();
        public List<int> lstAutoSizeColumn = new List<int>();
        public List<int> tablePos = new List<int>();
        public List<int> tableDetailPos = new List<int>();
        public WordExportData()
        {

        }

        public void exec(string type)
        {
            var printCf = new PrintAnco2();
            printCf.isPDF = type == "pdf";

            var context = HttpContext.Current;
            var sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 0);
            sothapphan = PrintAnco2.Replace0ToHyphen2(sothapphan);
            var config = PrintAnco2.GetInfoPrint();
            string url = ExcuteSignalRStatic.mapPathSignalR("~/" + PrintAnco2.GetStoreNotApp(2, fileNameTemp));
            var urlFontPDF = url.Substring(0, url.LastIndexOf(".")) + ".pdf.doc";

            if (File.Exists(urlFontPDF) & type == "pdf")
                url = urlFontPDF;

            title = url.Substring(url.LastIndexOf("\\") + 1);

            var dt = tbls.FirstOrDefault();
            foreach (DataColumn column in dt.Columns)
            {
                DisplayName = DisplayName.Replace("{" + column.ColumnName + "}", dt.Rows[0][column.ColumnName].ToString());

                if (!column.ColumnName.StartsWith("a"))
                {
                    lstTextReplace.Add(new AvariablePrj.lstTextReplace()
                    {
                        oldT = "{" + column.ColumnName + "}",
                        newT = dt.Rows[0][column.ColumnName].ToString()
                    });
                }
            }

            var filename = Guid.NewGuid().ToString();
            filename = System.Text.RegularExpressions.Regex.Replace(filename, @"[^0-9a-zA-Z]+", "-");
            var urlWord = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/FileUpload/{0}.doc", filename));
            var urlWord2 = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/FileUpload/{0}.doc", Guid.NewGuid().ToString()));
            var urlPDF = urlWord.Substring(0, urlWord.LastIndexOf(".") + 1) + "pdf";
            File.Copy(url, urlWord);
            var wordConvert = new WordConverter();
            wordConvert.lstTextReplace = lstTextReplace;
            wordConvert.tablePos = tablePos;
            wordConvert.tableDetailPos = tableDetailPos;
            wordConvert.tbls = tbls;
            wordConvert.isPDF = printCf.isPDF.GetValueOrDefault(false);
            var msg = wordConvert.Convert(urlWord, printCf.isPDF.GetValueOrDefault(false) ? urlPDF : urlWord2);

            if (msg.Length <= 0)
            {
                if (printCf.isPDF.GetValueOrDefault(false))
                {
                    viewer.Attributes["linkExcel"] = urlPDF;
                    viewer.Attributes["linkViewExcel"] = string.Format(Helper.EncodeHTML_VNN(Security.UrlBase() + "FileUpload/{0}.pdf"), filename);
                    viewer.Attributes["nameExcel"] = filename;
                    Helper.viewFile(viewer);
                    File.Delete(urlWord);
                    //context.Response.Redirect(string.Format("../../../ViewPDFPublic/index.aspx?urlpdf=../FileUpload/{0}.pdf&zoomprint=0.999&zoom=page-actual&remove=true&namedown={1}", filename, DisplayName));
                }
                else
                {
                    var xfileRead = new FileStream(urlWord2, FileMode.Open, FileAccess.ReadWrite);
                    var memoryStream = new MemoryStream();
                    xfileRead.CopyTo(memoryStream);
                    xfileRead.Close();
                    xfileRead.Dispose();
                    File.Delete(urlWord);
                    File.Delete(urlWord2);

                    context.Response.ContentType = "application/vnd.ms-word";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.doc", DisplayName));
                    context.Response.Clear();
                    context.Response.BinaryWrite(memoryStream.ToArray());
                    context.Response.End();
                }
            }
            else
            {
                context.Response.Write(msg);
            }
        }
    }
}