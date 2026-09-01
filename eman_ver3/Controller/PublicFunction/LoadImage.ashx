<%@ WebHandler Language="C#" Class="LoadImage" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class LoadImage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];

        switch (oper)
        {
            case "load_img":
                this.load_img(context);
                break;
            default:
                break;
        }
    }

    public void load_img(HttpContext context)
    {
        string kq = "";
        string check = context.Request.QueryString["check"];
        string elem = context.Request.QueryString["elem"];
        if (check == "img_Class")
        {
            string filepath = Security.UrlBase() + "css/bootstrap.css";
            filepath = context.Server.MapPath(filepath);
            string[] lines = System.IO.File.ReadAllLines(filepath);
            foreach (string line in lines)
            {
                string class_ = VNN_Function.FindString(line, ".glyphicon-", ":before");
                if (class_.Replace(" ", "") != "")
                {
                    kq += "\n\n" + "<div onclick=\"get_image('" + elem + "','glyphicon glyphicon-" + class_ + "')\" class=\"nhan_divclass\"><span class=\"nhan_spanclass glyphicon glyphicon-" + class_ + "\"></span></div>";
                }
            }
        }
        else
        {
            string filepath = Security.UrlBase() + "images/Menu";
            string hamxuly = "";
            string[] items = System.IO.Directory.GetFiles(context.Server.MapPath(filepath));
            foreach (string item in items)
            {
                hamxuly = "get_image('" + elem + "', 'images/Menu/" + System.IO.Path.GetFileName(item) + "')";
                kq += "\n\n" + "<div onclick=\"" + hamxuly + "\" class=\"nhan_divclass\">" + "<img class=\"nhan_spanclass\" src=\"images/Menu/" + System.IO.Path.GetFileName(item) + "\" />" + "</div>";
            }

            filepath = Security.UrlBase() + "images/Content";
            items = System.IO.Directory.GetFiles(context.Server.MapPath(filepath));
            foreach (string item in items)
            {
                hamxuly = "get_image('" + elem + "', 'images/Content/" + System.IO.Path.GetFileName(item) + "')";
                kq += "\n\n" + "<div onclick=\"" + hamxuly + "\" class=\"nhan_divclass\">" + "<img class=\"nhan_spanclass\" src=\"images/Content/" + System.IO.Path.GetFileName(item) + "\" />" + "</div>";
            }

            filepath = Security.UrlBase() + "images/mime";
            items = System.IO.Directory.GetFiles(context.Server.MapPath(filepath));
            foreach (string item in items)
            {
                hamxuly = "get_image('" + elem + "', 'images/mime/" + System.IO.Path.GetFileName(item) + "')";
                kq += "\n\n" + "<div onclick=\"" + hamxuly + "\" class=\"nhan_divclass\">" + "<img class=\"nhan_spanclass\" src=\"images/mime/" + System.IO.Path.GetFileName(item) + "\" />" + "</div>";
            }

            filepath = Security.UrlBase() + "images/icon";
            items = System.IO.Directory.GetFiles(context.Server.MapPath(filepath));
            foreach (string item in items)
            {
                hamxuly = "get_image('" + elem + "', 'images/icon/" + System.IO.Path.GetFileName(item) + "')";
                kq += "\n\n" + "<div onclick=\"" + hamxuly + "\" class=\"nhan_divclass\">" + "<img class=\"nhan_spanclass\" src=\"images/icon/" + System.IO.Path.GetFileName(item) + "\" />" + "</div>";
            }

            filepath = Security.UrlBase() + "images/loading";
            items = System.IO.Directory.GetFiles(context.Server.MapPath(filepath));
            foreach (string item in items)
            {
                hamxuly = "get_image('" + elem + "', 'images/loading/" + System.IO.Path.GetFileName(item) + "')";
                kq += "\n\n" + "<div onclick=\"" + hamxuly + "\" class=\"nhan_divclass\">" + "<img class=\"nhan_spanclass\" src=\"images/loading/" + System.IO.Path.GetFileName(item) + "\" />" + "</div>";
            }
        }
        context.Response.Write(kq);
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}