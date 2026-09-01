using System;
using System.Threading;
public partial class _Default : System.Web.UI.Page
{
    public string link = "";
    protected void Page_Load(object sender, EventArgs e)
    {
		string id = Request.QueryString["id"];
		Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-BE");
        var rp = new VNN_report();
        link = Server.MapPath(Security.UrlBase() + "ReportsStorage/"+ id +".repx");
        rp.LoadLayout(link);
        reportDesigner.OpenReport(rp);
        
    }
}