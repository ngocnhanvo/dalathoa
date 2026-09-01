<%@ WebHandler Language="C#" Class="FileTest" %>

using System;
using System.Web;
using System.Data;
using System.Linq;

public class FileTest : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
        switch (oper)
        {
			case "xuly_License":
                this.xuly_License(context);
                break;
            default:
                break;
        }
    }
    
    public void xuly_License(HttpContext context)
    {
       string license = context.Request.Form["txt_license"];
	   //context.Response.Write(Key_License_Timer.Key_License_Timer.xuly_License(license, System.Web.Configuration.WebConfigurationManager.ConnectionStrings["edoc2014ConnectionString"].ConnectionString)); 
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}