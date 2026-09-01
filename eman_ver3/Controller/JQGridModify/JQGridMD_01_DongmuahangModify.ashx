<%@ WebHandler Language="C#" Class="JQGridMD_01_DongmuahangModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_DongmuahangModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    public JQGridMD_00_DonMuaHangClass classFunc = new JQGridMD_00_DonMuaHangClass();
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

    public string add(HttpContext context, string ma_module = "", string id = "", string ma_sanpham = "", string sldathang = "", string giamua = "", string mota = "", string thue = "", bool updated = true)
    {
        string msg = "", id_new = Helper.getNewId();
        if (string.IsNullOrWhiteSpace(ma_module))
            ma_module = context.Request.QueryString["ma_module"];
        if (string.IsNullOrWhiteSpace(ma_sanpham))
            ma_sanpham = context.Request.Form["md_sanpham_id"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(id))
            id = context.Request.Form["id_parent"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(sldathang))
            sldathang = context.Request.Form["sl_dadat"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(giamua))
            giamua = context.Request.Form["dongiamua"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(mota))
            mota = context.Request.Form["mota"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(thue))
            thue = context.Request.Form["thue"].removeAllSpaceOrTrimText(true);
        try
        {
            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).FirstOrDefault();
            if (sp == null)
            {
                msg = $"Mã <b>{ma_sanpham}</b> không tồn tại.";
                goto EndEventHandler;
            }

            var dsdhServer = db.c_donmuahang.Where(s => s.c_donmuahang_id == id).FirstOrDefault();
            var dsdh = db.c_donmuahang.Local.Where(s => s.c_donmuahang_id == id).FirstOrDefault();
            if (dsdh == null)
            {
                msg = $"Không tìm thấy đơn mua hàng.";
                goto EndEventHandler;
            }
            if (dsdh.md_trangthai_id != Helper.SOANTHAO)
            {
                msg = $@"Ðơn mua hàng ""{dsdh.sochungtu}"" không ở trạng thái Soạn Thảo.";
                goto EndEventHandler;
            }
            int ddsdh = db.c_donmuahang_cdmh.Where(s => s.md_sanpham_id == sp.md_sanpham_id & s.c_donmuahang_id == dsdh.c_donmuahang_id).Count();
            if (ddsdh > 0)
            {
                msg = $"Đã được thêm trước đó.";
                goto EndEventHandler;
            }
            var sldathang_Val = sldathang.ToNullableDecimal();
            if (sldathang_Val == null)
            {
                msg = "SL đặt hàng không thể bỏ trống";
                goto EndEventHandler;
            }
            if (sldathang_Val <= 0)
            {
                msg = $"Số lượng đặt hàng phải lớn hơn 0";
                goto EndEventHandler;
            }
            var giamua_Val = giamua.removeAllSpaceOrTrimText(false).ToNullableDecimal();
            if (giamua_Val == null)
            {
                msg = $"Giá mua phải có giá trị";
                goto EndEventHandler;
            }
            if (giamua_Val < 0)
            {
                msg = $"Giá mua không thể có giá trị âm";
                goto EndEventHandler;
            }

            var thuesp = db.md_thue_sanpham.Where(s => s.md_thue_sanpham_id == thue).FirstOrDefault();
            if (thuesp == null)
            {
                msg = $@"Không tìm thấy thuế đã chọn.";
                goto EndEventHandler;
            }

            var object_ = new c_donmuahang_cdmh();
            object_.c_donmuahang_cdmh_id = id_new;
            object_.c_donmuahang_id = id;
            VNN_Function.SetFormValue(object_.nameof(s => s.sl_dadat), sldathang_Val.ToString());
            VNN_Function.SetFormValue(object_.nameof(s => s.dongiamua), giamua_Val.ToString());
            VNN_Function.SetFormValue(object_.nameof(s => s.md_sanpham_id), sp.md_sanpham_id);
            VNN_Function.SetFormValue(object_.nameof(s => s.mota), mota);
            VNN_Function.SetFormValue(object_.nameof(s => s.thue), thue);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_.thanhtien = Math.Floor(object_.sl_dadat.GetValueOrDefault(0) * object_.dongiamua.GetValueOrDefault(0));
            object_.thanhtienThue = object_.thanhtien * thuesp.giatri / 100;
            db.c_donmuahang_cdmh.Add(object_);
            //classFunc.TinhThueDonMuaHang(dsdh, userTK, db);
            if (updated)
                db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;
        if (msg.Length <= 0)
        {
            if (updated)
            {
                msg = $@"true#Thêm thành công#{id_new}";
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = $"<div msp='{ma_sanpham}' style='color:blue'>Ðã đạt</div>";
            }
        }
        else
        {
            if (updated)
                msg = $@"false#{msg}";
            else
                msg = $"<div msp='{ma_sanpham}' style='color:red' error>{msg}</div>";
        }

        if (updated)
            context.Response.Write(msg);
        return msg;
    }

    public string edit(HttpContext context, string ma_module = "", string id = "", string id_parent = "", string ma_sanpham = "", string sldathang = "", string giamua = "", string mota = "", string thue = "", bool updated = true)
    {
        string msg = "", id_new = Helper.getNewId();
        if (string.IsNullOrWhiteSpace(ma_module))
            ma_module = context.Request.QueryString["ma_module"];
        if (string.IsNullOrWhiteSpace(ma_sanpham))
            ma_sanpham = context.Request.Form["md_sanpham_id"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(id))
            id = context.Request.Form["id"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(sldathang))
            sldathang = context.Request.Form["sl_dadat"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(giamua))
            giamua = context.Request.Form["dongiamua"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(mota))
            mota = context.Request.Form["mota"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(thue))
            thue = context.Request.Form["thue"].removeAllSpaceOrTrimText(true);

        try
        {
            var object_ = db.c_donmuahang_cdmh.Where(p => p.c_donmuahang_cdmh_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Lỗi:Không tìm thấy đối tượng cần sửa ";
                goto EndEventHandler;
            }

            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).FirstOrDefault();
            if (sp == null)
            {
                msg = $"Mã <b>{ma_sanpham}</b> không tồn tại.";
                goto EndEventHandler;
            }

            var dsdhServer = db.c_donmuahang.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id).FirstOrDefault();
            var dsdh = db.c_donmuahang.Local.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id).FirstOrDefault();
            if (dsdh == null)
            {
                msg = $"Không tìm thấy đơn mua hàng.";
                goto EndEventHandler;
            }
            if (dsdh.md_trangthai_id != Helper.SOANTHAO)
            {
                msg = $@"Ðơn mua hàng ""{dsdh.sochungtu}"" không ở trạng thái Soạn Thảo.";
                goto EndEventHandler;
            }
            int ddsdh = db.c_donmuahang_cdmh.Where(s =>
                s.md_sanpham_id == sp.md_sanpham_id &
                s.c_donmuahang_id == dsdh.c_donmuahang_id &
                s.c_donmuahang_cdmh_id != object_.c_donmuahang_cdmh_id).Count();
            if (ddsdh > 0)
            {
                msg = $"Đã được thêm trước đó.";
                goto EndEventHandler;
            }
            var sldathang_Val = sldathang.ToNullableDecimal();
            if (sldathang_Val == null)
            {
                msg = "SL đặt hàng không thể bỏ trống";
                goto EndEventHandler;
            }
            if (sldathang_Val <= 0)
            {
                msg = $"Số lượng đặt hàng phải lớn hơn 0";
                goto EndEventHandler;
            }
            var giamua_Val = giamua.removeAllSpaceOrTrimText(false).ToNullableDecimal();
            if (giamua_Val == null)
            {
                msg = $"Giá mua phải có giá trị";
                goto EndEventHandler;
            }
            if (giamua_Val < 0)
            {
                msg = $"Giá mua không thể có giá trị âm";
                goto EndEventHandler;
            }

            var thuesp = db.md_thue_sanpham.Where(s => s.md_thue_sanpham_id == thue).FirstOrDefault();
            if (thuesp == null)
            {
                msg = $@"Không tìm thấy thuế đã chọn.";
                goto EndEventHandler;
            }

            VNN_Function.SetFormValue(object_.nameof(s => s.sl_dadat), sldathang_Val.ToString());
            VNN_Function.SetFormValue(object_.nameof(s => s.dongiamua), giamua_Val.ToString());
            VNN_Function.SetFormValue(object_.nameof(s => s.md_sanpham_id), sp.md_sanpham_id);
            VNN_Function.SetFormValue(object_.nameof(s => s.mota), mota);
            VNN_Function.SetFormValue(object_.nameof(s => s.thue), thue);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_.thanhtien = Math.Floor(object_.sl_dadat.GetValueOrDefault(0) * object_.dongiamua.GetValueOrDefault(0));
            object_.thanhtienThue = object_.thanhtien * thuesp.giatri / 100;
            //classFunc.TinhThueDonMuaHang(dsdh, userTK, db);
            if (updated)
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

        if (updated)
            context.Response.Write(msg);
        return msg;
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        c_donmuahang dsdh = null;
        try
        {
            var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            foreach (var id_del_ in ids)
            {
                var object_ = db.c_donmuahang_cdmh.Where(p => p.c_donmuahang_cdmh_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy dòng cần xóa.", id_del_);
                }
                else
                {
                    dsdh = db.c_donmuahang.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id).FirstOrDefault();
                    if (dsdh.md_trangthai_id == Helper.HIEULUC)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Ðã xử lý.", dsdh.sochungtu);
                    }
                    else
                    {
                        db.c_donmuahang_cdmh.Remove(object_);
                        db.SaveChanges();
                    }
                }
            }

            //classFunc.TinhThueDonMuaHang(dsdh, userTK, db);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
        {
            msg = string.Format(@"true#Xóa thành công");
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