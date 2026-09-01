<%@ WebHandler Language="C#" Class="CacheAction" %>
using System;
using System.Web;
using System.Net;
public class CacheAction : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.Params["oper"];
        if (oper == "clear")
        {
            ADmin_JSON json = new ADmin_JSON();
            json.ClearCache(context);
        }
        else if (oper == "loginForCaching")
        {
            
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}