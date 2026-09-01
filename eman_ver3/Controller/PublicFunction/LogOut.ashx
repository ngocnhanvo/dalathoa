<%@ WebHandler Language="C#" Class="LogOut" %>

using System;
using System.Web;
using System.Web.Security;
public class LogOut : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest (HttpContext context) {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.Web.Security.FormsAuthentication.SignOut();

        //// 2. Xóa thủ công Cookie phụ (nếu bạn muốn xóa luôn tên hiển thị khi logout)
        //string cookiePhu = $"UserPrefs_{FormsAuthentication.FormsCookieName}";
        //if (context.Request.Cookies[cookiePhu] != null)
        //{
        //    var userPref = new HttpCookie(cookiePhu);
        //    userPref.Expires = DateTime.Now.AddDays(-1);
        //    context.Response.Cookies.Add(userPref);
        //}
        context.Response.Redirect(Security.UrlBase() + "Login.aspx");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}