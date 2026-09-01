<%@ WebHandler Language="C#" Class="JQGridMD_00_DVTSPModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using DataAcess;
public class JQGridMD_00_DVTSPModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    User_TK userTK = null;

    public string oper = "vnn";
    public void ProcessRequest (HttpContext context) {
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
            default:
                break;
        }
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string maDVT = context.Request.Form["ma_edi"].removeAllSpaceOrTrimText(true);

        try
        {
            var dvt = db.md_donvitinhsanpham.Where(s => s.ma_edi == maDVT).FirstOrDefault();
            if (dvt != null)
            {
                msg = "Lỗi: Đơn vị tính đã tồn tại";
                goto EndEventHandler;
            }

            string id = context.Request.QueryString["id"];
            var object_ = new md_donvitinhsanpham();
            object_.md_donvitinhsanpham_id = id_new;
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            db.md_donvitinhsanpham.Add(object_);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"true#Thêm mới thành công#{id_new}";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = $@"false#{msg}";
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        string maDVT = context.Request.Form["ma_edi"].removeAllSpaceOrTrimText(true);

        try
        {
            var object_ = db.md_donvitinhsanpham.Where(p => p.md_donvitinhsanpham_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Lỗi:Không tìm thấy đối tượng cần sửa";
                goto EndEventHandler;
            }

            var dvt = db.md_donvitinhsanpham.Where(s => s.ma_edi == maDVT & s.md_donvitinhsanpham_id != object_.md_donvitinhsanpham_id).FirstOrDefault();
            if (dvt != null)
            {
                msg = "Lỗi: Đơn vị tính đã tồn tại";
                goto EndEventHandler;
            }

            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"true#Cập nhật thành công";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = $@"false#{msg}";
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id_del = context.Request.Form["id"];

        try
        {
            var object_ = db.md_donvitinhsanpham.Where(p => p.md_donvitinhsanpham_id == id_del).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Lỗi:Không tìm thấy đối tượng cần xóa";
                goto EndEventHandler;
            }

            var pbg001s = db.md_giasanpham.Where(s => s.md_sanpham_id == object_.md_donvitinhsanpham_id).Take(1).Count();
            var pbg002s = db.md_sanpham.Where(s => s.md_donvitinhsanpham_id == object_.md_donvitinhsanpham_id).Take(1).Count();
            var pbg003s = db.c_donmuahang_cdmh.Where(s => s.md_donvitinhsanpham_id == object_.md_donvitinhsanpham_id).Take(1).Count();

            if(pbg001s > 0)
                msg = "Lỗi:ĐVT đã được sử dụng trong bảng giá";
            else if(pbg002s > 0)
                msg = "Lỗi:ĐVT đã được sử dụng trong HHVT";
            else if(pbg003s > 0)
                msg = "Lỗi:ĐVT đã được sử dụng trong đơn mua HHVT";

            if (msg.Length <= 0)
            {
                VNN_Function.Write_log(context, ma_module, null, oper, "MĐVT:" + object_.ma_edi + ", TĐVT:" + object_.ten_dvt, db);
                db.md_donvitinhsanpham.Remove(object_);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if(msg.Length <= 0)
        {
            msg = $@"true#Xóa đơn vị tính thành công";
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