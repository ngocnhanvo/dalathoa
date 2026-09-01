using System;
using System.IO;
using System.Web;
public partial class PrintControllers_MD_00_QuanLyMauInExcel_Default : System.Web.UI.Page
{
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var fileNameTemp = Request.QueryString["fileNameTemp"];
        var isBackup = Request.QueryString["type"] == "backup";
        var convert = new OfficeToPDF.ExcelConverter();
        var storeStr = isBackup ? PrintAnco2.GetStoreBackUp(true, fileNameTemp) : PrintAnco2.GetStore(true, fileNameTemp);
        string url = Server.MapPath(storeStr);
        if (!File.Exists(url))
            msg = "Không có tập tin";
        else
        {
            var filename = Guid.NewGuid().ToString();
            var urlExcel = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/upload/{0}.xls", filename));
            var urlPDF = urlExcel.Substring(0, urlExcel.LastIndexOf(".") + 1) + "pdf";

            File.Copy(url, urlExcel, true);

            var taskDelay = new System.Threading.Tasks.Task(() => { });
            taskDelay.delayTask(1000);

            msg = convert.ConvertInterop(urlExcel, urlPDF, null);

            if (msg.Length <= 0)
            {
                Response.Redirect(string.Format("../../../ViewPDFPublic/index.aspx?urlpdf=../upload/{0}.pdf&zoomprint=0.999&zoom=page-width&remove=true", filename));
            }
            else
            {
                Response.Write(msg);
            }
        }
    }
}