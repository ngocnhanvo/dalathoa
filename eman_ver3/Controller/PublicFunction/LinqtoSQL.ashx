<%@ WebHandler Language="C#" Class="LinqtoSQL" %>

using System;
using System.Web;
using System.Linq;
using DataAcess;

public class LinqtoSQL : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
        switch (oper)
        {
            case "updatelinq":
                this.updatelinq(context);
                break;
            default:
                break;
        }
    }

    public void updatelinq(HttpContext context)
    {
        try
        {
            //ADmin_UpdateLinq.Exec_UpdateLinq(context);
            context.Response.Write("thành công");
        }
        catch
        {
            context.Response.Write("thất bại");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}