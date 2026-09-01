<%@ WebHandler Language="C#" Class="JQGridMD_00_QLSochungtuModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_QLSochungtuModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_CapNhatNamHienTai":
                this.CA_01_CapNhatNamHienTai(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_CapNhatNamHienTai(HttpContext context)
    {
        var msg = "";
        using (var transaction = db.Database.BeginTransaction())
        {
            var checkFinally = true;
            try
            {
                var scts = db.md_sochungtu.ToList();
                foreach (var sct in scts)
                {
                    if (sct.namnay >= DateTime.Now.Year)
                    {
                        msg += string.Format(@"<div class='nhan-loi'>Lỗi: <b>{0}</b> Cập nhật thất bại, năm ""{1}"" đã từng được cập nhật</div>.", sct.ma_sochungtu, DateTime.Now.Year);
                    }
                    else
                    {
                        sct.namtruoc = sct.namnay;
                        sct.namnay = DateTime.Now.Year;
                        sct.giatri_namtruoc = sct.giatri_thaydoi;

                        var giatri_thaydoi = sct.giatri_thaydoi;
                        int count_gttd = giatri_thaydoi.Length;
                        sct.giatri_thaydoi = VNN_Config.load_number("0", count_gttd);
                        sct.khuonmau = sct.khuonmau.Replace(giatri_thaydoi, sct.giatri_thaydoi);
                        VNN_Function.Write_log(context, "MD_00_QLSochungtu", null, oper, "SCT:" + sct.ma_sochungtu + ", Năm HT:" + DateTime.Now.Year, db);
                        db.SaveChanges();

                        msg += string.Format(@"<div class='nhan-thanhcong'><b>{0}</b>: Cập nhật thành công cho năm ""{1}""</div>.", sct.ma_sochungtu, DateTime.Now.Year);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = string.Format(@"<div class='nhan-loi'>Lỗi: {0}</div>", ex.Message);
                checkFinally = false;
            }

            if (checkFinally)
                transaction.Commit();
            else
                transaction.Rollback();
        }

        context.Response.Write(msg);
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string md_trangthai_id = context.Request.Form["md_trangthai_id"];
        string ma_sochungtu = context.Request.Form["ma_sochungtu"];
        try
        {
            string id = context.Request.Form["id"];
            if (msg.Length <= 0)
            {
                var object_ = new md_sochungtu();
                object_.md_sochungtu_id = id_new;
                object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
                object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                object_.hoatdong = true;
                db.md_sochungtu.Add(object_);
                if (md_trangthai_id == Helper.HIEULUC)
                {
                    var sctsCungLoai = db.md_sochungtu.Where(s => s.md_sochungtu_id != id_new & s.md_trangthai_id == Helper.HIEULUC & s.ma_sochungtu == ma_sochungtu).ToList();
                    foreach (md_sochungtu sct in sctsCungLoai)
                    {
                        sct.md_trangthai_id = Helper.SOANTHAO;
                    }
                }
                db.SaveChanges();
                msg = $"true#Thêm thành công.#{id_new}";
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string md_trangthai_id = context.Request.Form["md_trangthai_id"];
        string ma_sochungtu = context.Request.Form["ma_sochungtu"];
        try
        {
            string id = context.Request.Form["id"];
            md_sochungtu object_ = db.md_sochungtu.Where(p => p.md_sochungtu_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Lỗi:Không tìm thấy đối tượng cần sửa";
                goto EndEventHandler;
            }

            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            if (md_trangthai_id == Helper.HIEULUC)
            {
                var sctsCungLoai = db.md_sochungtu.Where(s => s.md_sochungtu_id != object_.md_sochungtu_id & s.md_trangthai_id == Helper.HIEULUC & s.ma_sochungtu == ma_sochungtu).ToList();
                foreach (md_sochungtu sct in sctsCungLoai)
                {
                    sct.md_trangthai_id = Helper.SOANTHAO;
                }
            }
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $"true#Sửa thành công";
        }
        else
        {
            msg = $"false#{msg}";
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                foreach (var id_del_ in ids)
                {
                    var object_ = db.md_sochungtu.Where(p => p.md_sochungtu_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else if (object_.md_trangthai_id == "HIEULUC")
                    {
                        msg += string.Format(@"<br><b>{0} ({1})</b>: Đã được ""Hiệu Lực"".", object_.ma_sochungtu, object_.ten_sochungtu);
                    }
                    else
                    {
                        VNN_Function.Write_log(context, ma_module, null, oper, "SCT:" + object_.ma_sochungtu + ", Tên:" + object_.ten_sochungtu, db);
                        db.md_sochungtu.Remove(object_);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = @"true#Xóa số chứng từ đã chọn thành công.";
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
