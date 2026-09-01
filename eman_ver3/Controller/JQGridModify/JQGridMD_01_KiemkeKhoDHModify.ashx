<%@ WebHandler Language="C#" Class="JQGridMD_01_KiemkeKhoDHModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
using System.Collections.Generic;

public class JQGridMD_01_KiemkeKhoDHModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    EntityContext db;
    EntityFunction entityFunc;
    User_TK userTK = null;
    public string oper = "vnn";
    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            db = new EntityContext();
            entityFunc = new EntityFunction();
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
            case "CA_01_CapnhatSLDD":
                this.CA_01_CapnhatSLDD(context);
                break;
            case "CA_01_NhapLieuExcelBBKK2":
                this.CA_01_NhapLieuExcelBBKK2(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_NhapLieuExcelBBKK2(HttpContext context)
    {
        var msg = new List<string>();
        var jsonStr = context.Request.Form["json"];
        var id = context.Request.Form["id_parent"];
        var ma_module = context.Request.QueryString["ma_module"];
        try
        {
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonStr);
            var hasErr = false;
            foreach (var item in json)
            {
                string msp = item.ElementAt(0).Value.removeAllSpaceOrTrimText(true);
                string sld = item.ElementAt(1).Value.removeAllSpaceOrTrimText(true);
                string ghichu = item.ElementAt(2).Value.removeAllSpaceOrTrimText(true);
                string msgDT = add(context, ma_module, id, msp, sld, ghichu, false);
                if (!string.IsNullOrWhiteSpace(msgDT))
                {
                    if (msgDT.LastIndexOf("error") > -1)
                        hasErr = true;
                    msg.Add(msgDT);
                }
            }

            if (!hasErr)
            {
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg.Add(ex.Message);
        }

        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(msg));
    }

    public void CA_01_CapnhatSLDD(HttpContext context)
    {
        var db = new EntityContext();
        string sel_val = context.Request.Form["sel_val"];
        var ids = context.Request.Form["id"].removeAllSpaceOrTrimText(true).Split(',').ToList();
        string msg = "";

        try
        {
            decimal slvc_case = decimal.Parse(context.Request.Form["slvc_case"]);
            var cdhs = db.md_kiemke_cdh.Where(s => ids.Contains(s.md_kiemke_cdh_id)).ToList();
            var kkid = cdhs.Select(s => s.md_kiemke_id).First();
            var kk = db.md_kiemke.Where(s => s.md_kiemke_id == kkid).FirstOrDefault();
            if (kk.ma_kiemke != Helper.SOANTHAO)
            {
                msg = $@"Phiếu kiểm kê phải ở trạng thái ""Soạn Thảo""";
                goto EndEventHandler;
            }

            foreach (var cdh in cdhs)
            {
                if (sel_val == "0")
                {
                    cdh.sl_sosach = cdh.sl_demduoc;
                }
                else
                {
                    cdh.sl_sosach = slvc_case;
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
            msg = $@"<div style='color:blue'>Cập nhật số lượng thực tế thành công</div>";
        }
        else
        {
            msg = $@"<div error style='color:red'>{msg}</div>";
        }

        context.Response.Write(msg);
    }

    public string add(HttpContext context, string ma_module = "", string idpr = "", string ma_sanpham = "", string sldathang = "", string mota = "", bool updated = true)
    {
        string msg = "", id_new = Helper.getNewId();
        if (string.IsNullOrWhiteSpace(ma_module))
            ma_module = context.Request.QueryString["ma_module"];
        if (string.IsNullOrWhiteSpace(ma_sanpham))
            ma_sanpham = context.Request.Form["ma_sanpham"];
        if (string.IsNullOrWhiteSpace(idpr))
            idpr = context.Request.Form["id_parent"];
        if (string.IsNullOrWhiteSpace(sldathang))
            sldathang = context.Request.Form["sl_sosach"];
        if (string.IsNullOrWhiteSpace(mota))
            mota = context.Request.Form["mota"];
        try
        {
            var kk = db.md_kiemke.Where(s => s.md_kiemke_id == idpr).FirstOrDefault();
            if (kk == null)
            {
                msg = $@"Không tìm thấy phiếu kiểm kê";
                goto EndEventHandler;
            }
            if (kk.ma_kiemke != Helper.SOANTHAO)
            {
                msg = $@"Phiếu kiểm kê không ở trạng thái ""Soạn Thảo"".";
                goto EndEventHandler;
            }
            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).FirstOrDefault();
            if (sp == null)
            {
                msg = $@"Không tìm thấy HHVT có mã ""{ma_sanpham}"".";
                goto EndEventHandler;
            }
            var dh = db.md_kiemke_cdh.Where(s => s.md_sanpham_id == sp.md_sanpham_id & s.md_kiemke_id == kk.md_kiemke_id).FirstOrDefault();
            if (dh != null)
            {
                msg = $@"HHVT ""{ma_sanpham}"" đã tồn tại trong phiếu kiểm kê này.";
                goto EndEventHandler;
            }
            var hangtho = kk.sapxep == "0";
            var thanhpham = kk.sapxep == "1";
            var tatca = kk.sapxep == "";
            if (hangtho)
            {
                if (!sp.ban_thanhpham.GetValueOrDefault(false) | sp.vattu.GetValueOrDefault(true))
                {
                    msg = $@"HHVT ""{ma_sanpham}"" không phải hàng thô.";
                    goto EndEventHandler;
                }
            }
            if (thanhpham & !sp.sanpham.GetValueOrDefault(false))
            {
                msg = $@"HHVT ""{ma_sanpham}"" không phải hàng thành phẩm.";
                goto EndEventHandler;
            }

            dh = new md_kiemke_cdh();
            dh.md_kiemke_cdh_id = id_new;
            dh.md_kiemke_id = kk.md_kiemke_id;
            dh.md_sanpham_id = sp.md_sanpham_id;
            dh.mota = mota;
            dh.sl_demduoc = db.md_kho_sanpham
                    .Where(s => s.md_kho_id == kk.md_kho_id & s.md_sanpham_id == sp.md_sanpham_id)
                    .ToList()
                    .Sum(s => s.soluong.GetValueOrDefault(0));

            dh.sl_sosach = sldathang.removeAllSpaceOrTrimText(true).ToNullableDecimal();

            dh = Helper.setDefaultValueWhenInsertOrUpdate(dh, userTK, false);
            db.md_kiemke_cdh.Add(dh);

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
                msg = $@"<div msp='{ma_sanpham}' style='color:blue'>Ðã đạt</div>";
            }
        }
        else
        {
            if (updated)
                msg = $@"false#{msg}";
            else
                msg = $@"<div msp='{ma_sanpham}' style='color:red' error>{msg}</div>";
        }
        if (updated)
            context.Response.Write(msg);
        return msg;
    }

    public void edit(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string ma_sanpham = context.Request.Form["ma_sanpham"];
        string id = context.Request.Form["id"];
        try
        {
            var object_ = db.md_kiemke_cdh.Where(s => s.md_kiemke_cdh_id == id).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Không tìm thấy dòng đang chọn";
                goto EndEventHandler;
            }
            var kk = db.md_kiemke.Where(s => s.md_kiemke_id == object_.md_kiemke_id).FirstOrDefault();
            if (kk == null)
            {
                msg = $@"Không tìm thấy phiếu kiểm kê";
                goto EndEventHandler;
            }
            if (kk.ma_kiemke != Helper.SOANTHAO)
            {
                msg = $@"Phiếu kiểm kê không ở trạng thái ""Soạn Thảo"".";
                goto EndEventHandler;
            }
            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).FirstOrDefault();
            if (sp == null)
            {
                msg = $@"Không tìm thấy HHVT có mã ""{ma_sanpham}"".";
                goto EndEventHandler;
            }
            var exist = db.md_kiemke_cdh.Where(s =>
            s.md_sanpham_id == sp.md_sanpham_id
            & s.md_kiemke_id == kk.md_kiemke_id
            & s.md_kiemke_cdh_id != object_.md_kiemke_cdh_id).FirstOrDefault();
            if (exist != null)
            {
                msg = $@"HHVT ""{ma_sanpham}"" đã tồn tại trong phiếu kiểm kê này.";
                goto EndEventHandler;
            }

            object_.sl_sosach = context.Request.Form["sl_sosach"].removeAllSpaceOrTrimText(true).ToNullableDecimal();
            object_.mota = context.Request.Form["mota"];
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"true#Sửa dòng hàng thành công";
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
            var ids = context.Request.Form["id"].removeAllSpaceOrTrimText(false).Split(',').ToList();
            var cdhs = db.md_kiemke_cdh.Where(p => ids.Contains(p.md_kiemke_cdh_id)).ToList();
            var kkid = cdhs.Select(s => s.md_kiemke_id).Distinct().FirstOrDefault();
            var kk = db.md_kiemke.Where(s => s.md_kiemke_id == kkid).FirstOrDefault();
            if (kk == null)
            {
                msg = $@"Không tìm thấy biên bản kiểm kê";
                goto EndEventHandler;
            }
            if (kk.ma_kiemke != Helper.SOANTHAO)
            {
                msg = $@"Phiếu kiểm kê không ở trạng thái ""Soạn Thảo""";
                goto EndEventHandler;
            }

            foreach (var cdh in cdhs)
            {
                db.md_kiemke_cdh.Remove(cdh);
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
            msg = $@"true#Xóa dòng hàng thành công";
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
