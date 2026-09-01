<%@ WebHandler Language="C#" Class="JQGrid__________Modify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using DataAcess;

public class JQGrid__________Modify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            default:
                break;
        }
    }


    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            var exist = db.md_dbbiendong.Where(s => s.md_dbbiendong_id == id_new).FirstOrDefault();
            if(exist != null)
            {
                msg = $@"Đã nhập trước đó";
                goto EndEventHandler;
            }

            var object_ = new md_dbbiendong();
            object_.md_dbbiendong_id = id_new;
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            db.md_dbbiendong.Add(object_);
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

        try
        {
            var object_ = db.md_dbbiendong.Where(p => p.md_dbbiendong_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Lỗi:Không tìm thấy đối tượng cần sửa ";
                goto EndEventHandler;
            }

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

        try
        {
            var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            var object_s = db.md_dbbiendong.Where(p => ids.Contains(p.md_dbbiendong_id)).ToList();
            if(object_s.Count <= 0)
            {
                msg = "<br>Lỗi:Không tìm thấy đối tượng cần xóa";
                goto EndEventHandler;
            }

            foreach (var object_ in object_s)
            {
                //VNN_Function.Write_log(context, ma_module, null, oper, object_.md_dbbiendong_id, db);
                db.md_dbbiendong.Remove(object_);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"true#Xóa bảng giá thành công";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = $@"false#{msg.Substring(4)}";
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