<%@ WebHandler Language="C#" Class="JQGridMD_00_NamKyKTModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_NamKyKTModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_Tao12kychoNam":
                this.CA_01_Tao12kychoNam(context);
                break;
            default:
                break;
        }
    }

    public string CA_01_Tao12kychoNam(HttpContext context, md_namtaichinh ntc = null)
    {
        string msg = "", id = context.Request.Form["id"];
        if (ntc == null)
            ntc = db.md_namtaichinh.FirstOrDefault(s => s.md_namtaichinh_id == id);

        if (ntc == null)
        {
            msg = "Lỗi: Không tìm thấy năm tài chính đã chọn.";
            goto EndEventHandler;
        }

        for (int i = 1; i <= 12; i++)
        {
            string iStr = i > 10 ? i.ToString() : $"0{i}";
            string ngaybatdau = $@"01/{iStr}/{ntc.giatri} 00:00";
            string ngayketthuc = $@"{DateTime.DaysInMonth(ntc.giatri.Value, i)}/{iStr}/{ntc.giatri} 23:59";

            var ntc_ky = new md_namtaichinh_ky
            {
                md_namtaichinh_ky_id = Helper.getNewId(),
                md_namtaichinh_id = ntc.md_namtaichinh_id,
                ma_ky = $@"Ky{i}-{ntc.giatri}",
                ten_ky = $@"Kỳ {i}",
                soky = i,
                loaiky = "CHUAN",
                ngaybatdau = VNN_Config.setDateTime(ngaybatdau),
                ngayketthuc = VNN_Config.setDateTime(ngayketthuc),
                hoatdong = true
            };
            ntc_ky = Helper.setDefaultValueWhenInsertOrUpdate(ntc_ky, userTK, false);
            db.md_namtaichinh_ky.Add(ntc_ky);
            db.SaveChanges();
        }
    EndEventHandler:;
        return msg;
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        string ten_namtaichinh = context.Request.Form["ten_namtaichinh"];
        string giatri = context.Request.Form["giatri"].removeAllSpaceOrTrimText(false);

        try
        {
            var giatriInt = giatri.ToNullableInt();
            if (giatriInt == null)
            {
                msg = $@"Giá trị phải là 1 số nguyên";
                goto EndEventHandler;
            }

            var exist = db.md_namtaichinh.Where(s => s.giatri == giatriInt).FirstOrDefault();
            if (exist != null)
            {
                msg = $@"Năm ""{giatri}"" đã tồn tại";
                goto EndEventHandler;
            }

            var object_ = new md_namtaichinh();
            object_.md_namtaichinh_id = id_new;
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            db.md_namtaichinh.Add(object_);

            CA_01_Tao12kychoNam(context, object_);
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
            var object_ = db.md_namtaichinh.Where(p => p.md_namtaichinh_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                goto EndEventHandler;
            }

            VNN_Function.SetFormValue(object_.nameof(s => s.giatri), Helper.VNN_notpost);
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

        try
        {
            var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            foreach (var id_del_ in ids)
            {
                var object_ = db.md_namtaichinh.Where(p => p.md_namtaichinh_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                }
                else
                {
                    var checkUse = db.md_modongky.Where(s => s.md_namtaichinh_id == object_.md_namtaichinh_id).Count();
                    if (checkUse > 0)
                        msg += string.Format(@"<br><b>{0}</b>: Có liên kết với Mở Đóng Kỳ.", object_.ten_namtaichinh);
                    else
                    {
                        VNN_Function.Write_log(context, ma_module, null, oper, "NTC:" + object_.ten_namtaichinh, db);
                        db.md_namtaichinh.Remove(object_);
                        db.SaveChanges();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
        {
            msg = @"true#Xóa năm tài chính đã chọn thành công.";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg.Substring(4));
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
