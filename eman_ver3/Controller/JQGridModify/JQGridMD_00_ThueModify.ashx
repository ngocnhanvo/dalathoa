<%@ WebHandler Language="C#" Class="JQGridMD_00_ThueModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_ThueModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        EntityContext db = new EntityContext();
        string msg = "",id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            var object_ = new md_thue_sanpham();
            object_.md_thue_sanpham_id = id_new;
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            db.md_thue_sanpham.Add(object_);
            db.SaveChanges();
        }
        catch(Exception ex)
        {
            msg = "false#" + ex.Message;
        }

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
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            var object_ = db.md_thue_sanpham.Where(p => p.md_thue_sanpham_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
            }
            else
            {
                object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

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
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                foreach (var id_del_ in ids)
                {
                    var object_ = db.md_thue_sanpham.Where(p => p.md_thue_sanpham_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else
                    {
                        var checkUse = db.md_sanpham.Where(s => s.md_thue_sanpham_id == object_.md_thue_sanpham_id).Count();
                        var checkUse1 = db.c_donmuahang_cdmh.Where(s => s.thue == object_.md_thue_sanpham_id).Count();

                        if (checkUse > 0)
                            msg += string.Format(@"<br><b>{0}</b>: Có liên kết với Hàng Hóa Vật Tư.", object_.ten_thue_sanpham);
                        else if (checkUse1 > 0)
                            msg += string.Format(@"<br><b>{0}</b>: Có liên kết với Đơn Mua Hàng.", object_.ten_thue_sanpham);
                        else
                        {
                            VNN_Function.Write_log(context, ma_module, null, oper, "Thuế:" + object_.ten_thue_sanpham, db);
                            db.md_thue_sanpham.Remove(object_);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if(msg.Length <= 0)
            {
                msg = @"true#Xóa thuế đã chọn thành công.";
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = string.Format(@"false#{0}", msg.Substring(4));
                transaction.Rollback();
            }
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
