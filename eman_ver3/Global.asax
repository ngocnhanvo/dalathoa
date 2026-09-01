<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new FilesystemReportStorageWebExtension(this.Context));
        DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.RegisterDataSourceWizardConfigFileConnectionStringsProvider();
    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        var response = Context.Response;
        var urlstr = Request.Url.ToString();
        var urlhost = Request.Url.Host;
        if (!urlhost.StartsWith("www") & !Request.Url.IsLoopback & !urlhost.StartsWith("1") & !urlhost.StartsWith(".local") & urlhost.IndexOf(".") < 2)
        {
            UriBuilder builder = new UriBuilder(Request.Url);
            builder.Host = "www." + Request.Url.Host;
            Response.StatusCode = 301;
            Response.AddHeader("Location", builder.ToString());
            Response.End();
            urlstr = builder.ToString();
        }
        //if (urlstr.Contains("http://") & !urlhost.StartsWith("192") & !urlhost.StartsWith("localhost"))
        //Response.Redirect(urlstr.Replace("http","https"));
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown 
    }

    void Application_Error(object sender, EventArgs e)
    {

    }

    void Session_Start(object sender, EventArgs e)
    {

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
