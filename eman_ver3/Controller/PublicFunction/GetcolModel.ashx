<%@ WebHandler Language="C#" Class="GetcolModel" %>

using System;
using System.Web;

public class GetcolModel : IHttpHandler {
    public void ProcessRequest (HttpContext context) {
        string ma_module = context.Request.QueryString["ma_module"];
        context.Response.Write(VNN_Config.get_colModel(context, ma_module)[0]);
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}