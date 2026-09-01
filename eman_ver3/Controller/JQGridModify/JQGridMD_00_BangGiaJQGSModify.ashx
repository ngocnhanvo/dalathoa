<%@ WebHandler Language="C#" Class="JQGridMD_00_BangGiaModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using DataAcess;

public class JQGridMD_00_BangGiaModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    User_TK userTK = null;

    public string oper = "vnn";
    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
            userTK = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context), db);
        }

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
            case "CA_01_vnntest_MD_00_BangGiaJQGS":
                this.CA_01_vnntest_MD_00_BangGiaJQGS(context);
                break;
            case "selectoption":
                this.selectoption(context);
                break;
            case "select_bangia":
                this.select_bangia(context);
                break;
            case "select_bangiaNhanCong":
                this.select_bangiaNhanCong(context);
                break;
            default:
                break;
        }
    }

    public void select_bangia(HttpContext context)
    {
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        string str = "";
        string id = context.Request.QueryString["id"];
        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        foreach (var object_ in db.md_banggia.Where(s => s.hoatdong == true & (s.tuychon == Helper.MUAVT | s.tuychon == Helper.MUATP)).OrderBy(s => s.ten_banggia).ToList())
        {
            str += string.Format(@"<option dtkd='{2}' style='display:none' value='{0}'>{1}</option>", object_.md_banggia_id, object_.ten_banggia, object_.lienket_bg);
        }
        context.Response.Write("<select>" + str + "</select>");
    }

    public void select_bangiaNhanCong(HttpContext context)
    {
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        string str = "";
        string id = context.Request.QueryString["id"];
        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        foreach (var object_ in db.md_banggia.Where(s => s.hoatdong == true & s.tuychon == Helper.NHANCONG).OrderBy(s => s.ten_banggia).ToList())
        {
            str += string.Format(@"<option kho='{2}' style='display:none' value='{0}'>{1}</option>", object_.md_banggia_id, object_.ten_banggia, "");
        }
        context.Response.Write("<select>" + str + "</select>");
    }

    public void selectoption(HttpContext context)
    {
        string str = "<select>";
        foreach (var cn in db.md_banggia.Where(s => s.md_banggia_id != null & s.md_banggia_id != "").ToList())
        {
            str += string.Format("<option value=\"{0}\" dongtien_id=\"{2}\">{1}</option>", cn.md_banggia_id, cn.ten_banggia, cn.md_dongtien_id);
        }
        str += "</select>";
        context.Response.Write(str);
    }

    public void CA_01_vnntest_MD_00_BangGiaJQGS(HttpContext context)
    {
        context.Response.Write("vnn");
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string pxId = context.Request.Form["md_phanxuong_id"];
        string toId = context.Request.Form["md_to_id"];
        string lienket_bg = context.Request.Form["lienket_bg"];
        string tuychon = context.Request.Form["tuychon"];
        
        var bgExist = db.md_banggia.Where(s => s.lienket_bg == lienket_bg & s.tuychon == tuychon & s.hoatdong == true).FirstOrDefault();
        if (bgExist != null)
        {
            msg = $@"Đã có bảng giá cùng ""loại"" cùng ""đối tác"" tồn tại trước đó";
            goto EndEventHandler;
        }

        try
        {
            var pxTrongQT = db.ad_department.Where(s => s.md_phongban_id == toId).FirstOrDefault();
            var object_ = new md_banggia();
            object_.md_banggia_id = id_new;
            //object_.trangthai = Helper.CHODUYET;
            object_.phongbanId = pxTrongQT == null ? "" : pxTrongQT.md_phongban_id;
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            db.md_banggia.Add(object_);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = string.Format(@"true#Thêm mới thành công#{0}", id_new);
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg);
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string pxId = context.Request.Form["md_phanxuong_id"];
        string toId = context.Request.Form["md_to_id"];
        string lienket_bg = context.Request.Form["lienket_bg"];
        string tuychon = context.Request.Form["tuychon"];

        try
        {
            string id = context.Request.Form["id"];
            var object_ = db.md_banggia.Where(p => p.md_banggia_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Không tìm thấy đối tượng cần sửa ";
                goto EndEventHandler;
            }

            var pbgs = db.md_phienbangia.Where(s => s.md_banggia_id == object_.md_banggia_id & s.trangthai == Helper.HIEULUC).Count();
            if (pbgs > 0)
            {
                msg = "Bảng giá đã tồn tại phiên bản giá Hiệu Lực";
                goto EndEventHandler;
            }

            var bgExist = db.md_banggia.Where(s => s.md_banggia_id != object_.md_banggia_id & s.lienket_bg == lienket_bg & s.tuychon == tuychon & s.hoatdong == true).FirstOrDefault();
            if (bgExist != null)
            {
                msg = $@"Đã có bảng giá cùng ""loại"" cùng ""đối tác"" tồn tại trước đó";
                goto EndEventHandler;
            }

            var pxTrongQT = db.ad_department.Where(s => s.md_phongban_id == toId).FirstOrDefault();
            object_.phongbanId = pxTrongQT == null ? "" : pxTrongQT.md_phongban_id;
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = string.Format(@"true#Cập nhật thành công");
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg);
        }

        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            string id_del = context.Request.Form["id"];

            var object_ = db.md_banggia.Where(p => p.md_banggia_id == id_del).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Lỗi:Không tìm thấy đối tượng cần xóa";
            }
            else
            {
                var pbgs = db.md_phienbangia.Where(s => s.md_banggia_id == object_.md_banggia_id).Count();
                if (pbgs > 0)
                    msg = "Lỗi:Bảng giá đã tồn tại phiên bản giá";
            }

            if (msg.Length <= 0)
            {
                VNN_Function.Write_log(context, ma_module, null, oper, object_.ten_banggia, db);
                db.md_banggia.Remove(object_);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
        {
            msg = $@"true#Xóa bảng giá thành công";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = $@"false#{msg}";
        }

        context.Response.Write(msg);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}