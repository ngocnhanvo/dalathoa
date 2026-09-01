<%@ WebHandler Language="C#" Class="Send_hotro" %>

using System;
using System.Web;

public class Send_hotro : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        { 
            string title = context.Request.Params["tieude"];
            string nd= context.Request.Params["noidung"];
            string email= context.Request.Params["email"];

            if (title != "" & nd != "" & email != "")
            {
                GoogleMail gm = new GoogleMail();
                gm.Send(email, title, nd,"");
                context.Response.Write(1);
            }
            else
            {
                context.Response.Write(2);
            }
        }
        catch
        {
            context.Response.Write(0);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}