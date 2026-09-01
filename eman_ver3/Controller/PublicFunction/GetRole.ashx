<%@ WebHandler Language="C#" Class="GetRole" %>

using System;
using System.Web;
using System.Linq;
public class GetRole : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
        
        switch (oper)
        {
            case "DinhkemTTCV":
                this.DinhkemTTCV(context);
                break;
            default:
                break;
        }
    }

    public void DinhkemTTCV(HttpContext context)
    {
        string ma_module = "MD_01_TTCongVan";
        bool uploadTT = Security.PhanQuyen_ChucNang(context, ma_module, "CA_01_TTUpload");
        bool xoaTT = Security.PhanQuyen_ChucNang(context, ma_module, "CA_01_TTRemove");
        string kq = uploadTT + "(##)" + xoaTT;
        context.Response.Write(kq.ToLower());
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}