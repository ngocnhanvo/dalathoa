<%@ WebHandler Language="C#" Class="JqGridQuocGiaModify" %>

using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;

public class JqGridQuocGiaModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
        
        switch (oper)
        {
            case "add":
                this.add(context);
                break;
            case "edit":
                this.edit(context);
                break;
            case "del":
                this.del(context);
                break;
            case "selectoption":
                this.SelectOption(context);
                break;
            default:
                break;
        }
    }


    public void add(HttpContext context)
    {
        try
        {
            EntityContext db = new EntityContext();
			string ma_quocgia= context.Request.Form["ma_quocgia"];;
			if(db.md_quocgia.SingleOrDefault(p => p.ma_quocgia.Equals(ma_quocgia)) != null)
			{
				context.Response.Write("false#Mã quốc gia này đã tồn tại!");
			}
			else
			{
				md_quocgia qg = new md_quocgia();
				qg.md_quocgia_id = Helper.getNewId();
				qg.ma_quocgia = ma_quocgia;
				qg.ten_quocgia = context.Request.Form["ten_quocgia"];

				qg.ngaytao = DateTime.Now;
				qg.nguoitao = Security.id_taikhoan(context);
				qg.ngaycapnhat = DateTime.Now;
				qg.nguoicapnhat = Security.id_taikhoan(context);
				qg.mota = context.Request.Form["mota"];
				qg.hoatdong = true;

				db.md_quocgia.Add(qg);
				db.SaveChanges();
				context.Response.Write("true#Thêm thành công!");
			}
        }
        catch (Exception ex)
        {
            context.Response.Write("false#Lỗi: " + ex.Message);
        }
    }


    public void edit(HttpContext context)
    {
        try
        {
            EntityContext db = new EntityContext();
            string id = context.Request.Form["id"];
            md_quocgia qg = db.md_quocgia.SingleOrDefault(p => p.md_quocgia_id == id);
            if (qg != null)
            {
                qg.ma_quocgia = context.Request.Form["ma_quocgia"];
                qg.ten_quocgia = context.Request.Form["ten_quocgia"];

                qg.ngaycapnhat = DateTime.Now;
                qg.nguoicapnhat = Security.id_taikhoan(context);
                qg.mota = context.Request.Form["mota"];
                db.SaveChanges();
                context.Response.Write("true#Cập Nhật Thành công!");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("false#Lỗi: " + ex.Message);
        }
    }

    public void del(HttpContext context)
    {
        try
        {
            EntityContext db = new EntityContext();
            string id = context.Request.Form["id"];
            md_quocgia qg = db.md_quocgia.SingleOrDefault(p => p.md_quocgia_id == id);
            if (qg != null)
            {
                db.md_quocgia.Remove(qg);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToUpper().Contains("REFERENCE"))
            {
                context.Response.Write("false#Lỗi: Đang được sử dụng, không thể xóa");
            }
            else
            {
                context.Response.Write("false#Lỗi: " + ex.Message);
            }
        }
    }

    public void SelectOption(HttpContext context)
    {
        EntityContext db = new EntityContext();
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        var qgs = from qg in db.md_quocgia where qg.hoatdong.Equals(true) orderby qg.ten_quocgia select qg;
        string str = "";
        str += "<select>";
        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        foreach (var qg in qgs)
        {
            str += string.Format("<option value=\"{0}\">{1}</option>", qg.md_quocgia_id, qg.ten_quocgia);
        }
        str += "</select>";
        context.Response.Write(str);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}