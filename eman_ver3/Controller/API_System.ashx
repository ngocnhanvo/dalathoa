<%@ WebHandler Language="C#" Class="API_System" %>

using System;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

public class API_System : IHttpHandler
{
    public JavaScriptSerializer js = new JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.QueryString["oper"];
        switch (oper)
        {
            case "loadPDF":
                this.loadPDF(context);
                break;
            case "loadImage":
                this.loadImage(context);
                break;
        }
    }
    public void loadPDF(HttpContext context)
    {
        var urlPDF = context.Request.QueryString["urlPDF"];
        var removePDF = context.Request.QueryString["remove"];
        urlPDF = urlPDF.Substring(0, urlPDF.LastIndexOf("?") <= 0 ? urlPDF.Length : urlPDF.LastIndexOf("?"));
        var url = context.Server.MapPath(urlPDF);
        var lastIndex = url.LastIndexOf(".");
        var mimeType = url.Substring(lastIndex + 1).ToLower();
        lastIndex = url.LastIndexOf("/");
        var fileName = url.Substring(lastIndex + 1).ToLower();

        using (var fileStream = new FileStream(url, FileMode.Open))
        {
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                context.Response.ContentType = mimeType == "pdf" ? "application/pdf" : "image/jpeg";
                context.Response.BinaryWrite(memoryStream.ToArray());
            }
        }

        if (File.Exists(url) & removePDF == "true")
        {
            //File.Delete(url);
        }
    }

    public void loadImage(HttpContext context)
    {
        var code = context.Request.QueryString["code"];
        var type = context.Request.QueryString["type"];

        var typeVal = string.IsNullOrWhiteSpace(type) ? 0 : int.Parse(type);

        var values = Enum.GetValues(typeof(eNumPB.PathImage));

        string value = values.GetValue(typeVal).ToString();

        string folder = Extension.ParseEnum<eNumPB.PathImage>(value).Description();

        var url = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/{0}/{1}.jpg", folder, code));


        if (!File.Exists(url))
        {
            url = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/{0}", eNumPB.PathImage.ImageNotFound.Description()));
        }
        var lastIndex = url.LastIndexOf(".");
        var mimeType = url.Substring(lastIndex + 1).ToLower();
        context.Response.ContentType = mimeType == "jpg" ? "image/jpeg" : (mimeType == "png" ? "image/png" : (mimeType == "gif" ? "image/gif" : (mimeType == "svg" ? "image/svg+xml" : "application/octet-stream")));
        context.Response.TransmitFile(url);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}