<%@ WebHandler Language="C#" Class="JQGridMD_01_PhienBangGiaJQGSModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using DataAcess;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
public class JQGridMD_01_PhienBangGiaJQGSModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "select_phienbangia":
                this.select_phienbangia(context);
                break;
            case "CA_01_CapNhatLaiGSP":
                this.CA_01_CapNhatLaiGSP(context);
                break;
            case "CA_01_HieuLucPBG":
                this.CA_01_HieuLucPBG(context);
                break;
            case "CA_01_XoaPBGDaHL":
                this.CA_01_XoaPBGDaHL(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_XoaPBGDaHL(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id_del = context.Request.Form["id"];
        try
        {
            var object_ = db.md_phienbangia.Where(p => p.md_phienbangia_id == id_del).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Không tìm thấy đối tượng cần xóa";
                goto EndEventHandler;
            }

            if (object_.trangthai != Helper.HIEULUC)
            {
                msg = $@"PBG không ở trạng thái ""Hiệu Lực""";
                goto EndEventHandler;
            }

            if (msg.Length <= 0)
            {
                string noidung = $@"{object_.ten_phienbangia}";
                VNN_Function.Write_log(context, ma_module, null, oper, noidung, db);
                db.md_phienbangia.Remove(object_);
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
            msg = $@"<div style=""color:blue"">Xóa thành công</div>";
        }
        else
        {
            msg = $@"<div style=""color:red"" error>Lỗi: {msg}</div>";
        }

        context.Response.Write(msg);
    }

    public void CA_01_HieuLucPBG(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];

        try
        {
            var pbg = db.md_phienbangia.Where(s => s.md_phienbangia_id == id).FirstOrDefault();
            if (pbg == null)
            {
                msg = $@"Phiên bản giá đang chọn không tồn tại";
                goto EndEventHandler;
            }

            if (pbg.trangthai != Helper.SOANTHAO)
            {
                msg = $@"Phiên bản giá đang chọn đã ""Hiệu Lực""";
                goto EndEventHandler;
            }

            if (pbg.ten_phienbangia.removeAllSpaceOrTrimText(false).Length <= 0)
            {
                msg = $@"Phiên bản giá đang chọn chưa có tên";
                goto EndEventHandler;
            }

            if (pbg.ngay_hieuluc == null)
            {
                msg = $@"Phiên bản giá đang chọn chưa có ngày hiệu lực";
                goto EndEventHandler;
            }

            var khongCoGiaSP = db.md_giasanpham.Where(s => s.md_phienbangia_id == pbg.md_phienbangia_id).Take(1).Count() <= 0;
            if (khongCoGiaSP)
            {
                msg = $@"Chưa có giá HHVT nào được thêm vào PBG này";
                goto EndEventHandler;
            }

            var bg = db.md_banggia.Where(s => s.md_banggia_id == pbg.md_banggia_id).FirstOrDefault();
            if (bg == null)
            {
                msg = $@"Không tìm thấy bảng giá của phiên bản giá đang chọn";
                goto EndEventHandler;
            }

            if (bg.ten_banggia.removeAllSpaceOrTrimText(false).Length <= 0)
            {
                msg = $@"Bảng giá chưa có <b>tên</b>";
                goto EndEventHandler;
            }

            if (bg.tuychon.removeAllSpaceOrTrimText(false).Length <= 0)
            {
                msg = $@"Bảng giá chưa có <b>loại</b>";
                goto EndEventHandler;
            }

            var canChonDTKD = new string[] { Helper.MUAVT, Helper.MUATP, Helper.BANTP }.Contains(bg.tuychon);
            if (canChonDTKD & bg.lienket_bg.removeAllSpaceOrTrimText(false).Length <= 0)
            {
                msg = $@"Bảng giá chưa chọn <b>đối tác kinh doanh</b>";
                goto EndEventHandler;
            }

            pbg.trangthai = Helper.HIEULUC;
            pbg.ngayHL = DateTime.Now;
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"<div id=""rs_pbgHL"" style=""color:blue"" ttpbg=""{Helper.HIEULUC}"" ttbg=""{Helper.DADUYET}"">Hiệu lực thành công</div>";
        }
        else
        {
            msg = $@"<div id=""rs_pbgHL"" style=""color:red"" error ttpbg=""{Helper.SOANTHAO}"" ttbg=""{Helper.CHODUYET}"">Lỗi: {msg}</div>";
        }

        context.Response.Write(msg);
    }

    public void CA_01_CapNhatLaiGSP(HttpContext context)
    {
        string msg = "";
        string type = context.Request.Form["type"];
        var scts = context.Request.Form["scts"].Split(';');
        var doiPBGMoiNhat = context.Request.Form["doiPBGMoiNhat"];
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id = context.Request.Form["id"];
                var pbg = db.md_phienbangia.Where(s => s.md_phienbangia_id == id).FirstOrDefault();
                md_phienbangia pbgMoiNhat = null;


                if (pbg == null)
                    msg = "Không tìm thấy phiên bản giá";
                else if (pbg.hoatdong == false & doiPBGMoiNhat != "1")
                {
                    msg = "Phiên bản giá không hoạt động";
                }
                else if (
                    db.md_banggia.Where(s =>
                        s.md_banggia_id == pbg.md_banggia_id
                        & s.tuychon == "NHANCONG"
                        & !string.IsNullOrEmpty(s.md_to_id)).Count() <= 0)
                    msg = "Phiên bản giá không thuộc bảng giá nhân công";
                else
                {
                    if (doiPBGMoiNhat == "1")
                    {
                        pbgMoiNhat = db.md_phienbangia.Where(s =>
                            s.md_banggia_id == pbg.md_banggia_id
                            & s.hoatdong == true
                            & s.md_phienbangia_id != pbg.md_phienbangia_id
                            ).OrderByDescending(s => s.ngay_hieuluc).FirstOrDefault();
                        if (pbgMoiNhat == null)
                            msg = "Không tìm thấy phiên bản giá nào mới hơn phiên bản giá đang chọn";
                    }
                }

                if (msg.Length <= 0)
                {
                    var ngaygoihan = DateTime.Now.AddDays(-120);
                    var xnbss = db.md_xuatkhonb.Where(s => s.phienbangiaNC == id);
                    if (type == "2")
                        xnbss = xnbss.Where(s => scts.Contains(s.sochungtu));
                    else
                        xnbss = xnbss.Where(s => s.ngaytao >= ngaygoihan);
                    var xnbs = xnbss.ToList();

                    foreach (var xnb in xnbs)
                    {
                        if (doiPBGMoiNhat == "1")
                            xnb.phienbangiaNC = pbgMoiNhat.md_phienbangia_id;

                        foreach (var cdh in db.md_xuatkhonb_cdh.Where(s =>
                            s.md_xuatkhonb_id == xnb.md_xuatkhonb_id
                            & db.c_danhsachdathang.Where(t => t.so_po == s.tenhang & t.trangthai != "KETTHUC").Count() > 0
                        ).ToList())
                        {
                            var gsp = db.md_giasanpham
                                    .Where(s =>
                                        s.md_phienbangia_id == xnb.phienbangiaNC
                                        & s.md_sanpham_id == cdh.md_sanpham_id
                                        & s.md_donvitinhsanpham_id == cdh.md_donvitinhsanpham_id
                                        & s.gia != cdh.gianhancong)
                                    .FirstOrDefault();

                            if (gsp != null)
                            {
                                cdh.gianhancong = gsp.gia.GetValueOrDefault(0);
                                var lsx = db.md_lenhsanxuat.Where(s => cdh.lsx_to.StartsWith(s.sochungtu)).FirstOrDefault();
                                if (lsx != null)
                                {
                                    var tsx = db.md_lenhsanxuat_tosx.Where(s =>
                                        s.md_lenhsanxuat_id == lsx.md_lenhsanxuat_id
                                        & s.md_phanxuong_to_id == cdh.tuto).FirstOrDefault();
                                    if (tsx != null)
                                    {
                                        var cdhLSX = db.md_lenhsanxuat_tosx_cdh.Where(s =>
                                            s.md_lenhsanxuat_tosx_id == tsx.md_lenhsanxuat_tosx_id
                                            & s.md_sanpham_id == cdh.md_sanpham_id
                                        ).FirstOrDefault();
                                        if (cdhLSX != null)
                                            cdhLSX.gianhancong = cdh.gianhancong;
                                    }
                                }
                            }
                        }

                        foreach (var cdh in db.md_kho_giaodich.Where(s =>
                            s.dongnhapxuat == xnb.sochungtu
                        ).ToList())
                        {
                            var gsp = db.md_giasanpham
                                    .Where(s =>
                                        s.md_phienbangia_id == xnb.phienbangiaNC
                                        & s.md_sanpham_id == cdh.md_sanpham_id
                                        & s.md_donvitinhsanpham_id == cdh.md_donvitinhsanpham_id
                                        & s.gia != cdh.gianhancong)
                                    .FirstOrDefault();

                            if (gsp != null)
                            {
                                cdh.gianhancong = gsp.gia.GetValueOrDefault(0);
                            }
                        }
                    }

                    var vcnbss = db.md_vanchuyennoibo
                            .Where(s => s.phienbangiaNC == id);
                    if (type == "2")
                        vcnbss = vcnbss.Where(s => scts.Contains(s.sochungtu));
                    else
                        vcnbss = vcnbss.Where(s => s.ngaytao >= ngaygoihan);
                    var vcnbs = vcnbss.ToList();

                    foreach (var xnb in vcnbs)
                    {
                        if (doiPBGMoiNhat == "1")
                            xnb.phienbangiaNC = pbgMoiNhat.md_phienbangia_id;

                        foreach (var cdh in db.md_vanchuyennoibo_cdvc.Where(s =>
                            s.md_vanchuyennoibo_id == xnb.md_vanchuyennoibo_id
                        ).ToList())
                        {
                            var gsp = db.md_giasanpham
                                    .Where(s =>
                                        s.md_phienbangia_id == xnb.phienbangiaNC
                                        & s.md_sanpham_id == cdh.md_sanpham_id
                                        & s.md_donvitinhsanpham_id == cdh.md_donvitinhsanpham_id
                                        & s.gia != cdh.gianhancong)
                                    .FirstOrDefault();

                            if (gsp != null)
                            {
                                cdh.gianhancong = gsp.gia.GetValueOrDefault(0);
                            }
                        }

                        foreach (var cdh in db.md_vanchuyennoibo_dalayton.Where(s =>
                            s.md_vanchuyennoibo_id == xnb.md_vanchuyennoibo_id
                        ).ToList())
                        {
                            var gsp = db.md_giasanpham
                                    .Where(s =>
                                        s.md_phienbangia_id == xnb.phienbangiaNC
                                        & s.md_sanpham_id == cdh.md_sanpham_id
                                        & s.md_donvitinhsanpham_id == cdh.md_donvitinhsanpham_id
                                        & s.gia != cdh.gianhancong)
                                    .FirstOrDefault();

                            if (gsp != null)
                            {
                                cdh.gianhancong = gsp.gia.GetValueOrDefault(0);
                            }
                        }

                        foreach (var cdh in db.md_kho_giaodich.Where(s =>
                            s.dongvanchuyen == xnb.sochungtu
                        ).ToList())
                        {
                            var gsp = db.md_giasanpham
                                    .Where(s =>
                                        s.md_phienbangia_id == xnb.phienbangiaNC
                                        & s.md_sanpham_id == cdh.md_sanpham_id
                                        & s.md_donvitinhsanpham_id == cdh.md_donvitinhsanpham_id
                                        & s.gia != cdh.gianhancong)
                                    .FirstOrDefault();

                            if (gsp != null)
                            {
                                cdh.gianhancong = gsp.gia.GetValueOrDefault(0);
                            }
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex + "";
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"<div style='color:blue'>Cập nhật giá cho phiếu XK thành công</div>");
                transaction.Commit();
            }
            else
            {
                msg = string.Format(@"<div style='color:red' class='error'>Lỗi {0}</div>", msg);
                transaction.Rollback();
            }
        }

        context.Response.Write(msg);
    }

    public void select_phienbangia(HttpContext context)
    {
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        string str = "";
        string id = context.Request.QueryString["id"];
        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        var lstPBG = new List<string>();
        foreach (var object_ in db.md_phienbangia.OrderByDescending(s => s.ngay_hieuluc).ToList())
        {
            if (object_.hoatdong == true)
            {
                str += string.Format("<option bg='{2}' value='{0}'>{1}</option>", object_.md_phienbangia_id, object_.ten_phienbangia, object_.md_banggia_id);
                lstPBG.Add(object_.md_banggia_id);
            }
            else
                str += string.Format("<option bg='{2}' class='notsel_option' value='{0}'>{1}</option>", object_.md_phienbangia_id, object_.ten_phienbangia, object_.md_banggia_id);
        }
        context.Response.Write("<select>" + str + "</select>");
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string md_trangthai_id = context.Request.Form["md_trangthai_id"];
        string id = context.Request.Form["id_parent"];
        string ten_pbg = context.Request.Form["ten_phienbangia"];

        try
        {
            if (db.md_phienbangia.Where(s => s.ten_phienbangia == ten_pbg).Count() > 0)
            {
                msg = $@"Đã tồn tại phiên bản giá ""{ten_pbg}""";
            }
            else
            {
                var object_ = new md_phienbangia();
                object_.md_phienbangia_id = id_new;
                object_.md_banggia_id = id;
                object_.trangthai = Helper.SOANTHAO;
                object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
                object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                db.md_phienbangia.Add(object_);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

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
        string md_trangthai_id = context.Request.Form["md_trangthai_id"];
        string id = context.Request.Form["id"];
        string ten_pbg = context.Request.Form["ten_phienbangia"];

        try
        {
            var object_ = db.md_phienbangia.Where(p => p.md_phienbangia_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Không tìm thấy đối tượng cần sửa ";
                goto EndEventHandler;
            }

            if (object_.trangthai != Helper.SOANTHAO)
            {
                var suaPBGHL = Security.PhanQuyen_ChucNang(context, ma_module, "CA_01_SuaPBGHL");
                if (!suaPBGHL)
                {
                    msg = $@"PBG đã Hiệu Lực";
                    goto EndEventHandler;
                }
            }

            if (db.md_phienbangia.Where(s => s.ten_phienbangia == ten_pbg).Count() > 0 & object_.ten_phienbangia != ten_pbg)
            {
                msg = $@"Đã tồn tại phiên bản giá ""{ten_pbg}""";
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

        try
        {
            string id_del = context.Request.Form["id"];

            var object_ = db.md_phienbangia.Where(p => p.md_phienbangia_id == id_del).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Không tìm thấy đối tượng cần xóa";
            }
            else if (object_.trangthai != Helper.SOANTHAO)
            {
                msg = $@"PBG đã Hiệu Lực";
            }
            else
            {
                var pbg001s = db.md_vanchuyennoibo.Where(s => s.phienbangiaNC == object_.md_phienbangia_id).Take(1).Count();
                var pbg002s = db.md_xuatkhonb.Where(s => s.phienbangiaNC == object_.md_phienbangia_id).Take(1).Count();
                var pbg003s = db.c_donmuahang.Where(s => s.md_phienbangia_id == object_.md_phienbangia_id).Take(1).Count();
                if (pbg001s > 0)
                    msg = $@"Phiên bảng giá đã được áp dụng cho phiếu chuyển kho";
                else if (pbg002s > 0)
                    msg = $@"Phiên bảng giá đã được áp dụng cho phiếu xuất kho nội bộ";
                else if (pbg003s > 0)
                    msg = $@"Phiên bảng giá đã được áp dụng cho đơn mua hàng hóa vật tư";
            }

            if (msg.Length <= 0)
            {
                VNN_Function.Write_log(context, ma_module, null, oper, object_.ten_phienbangia, db);
                db.md_phienbangia.Remove(object_);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
        {
            msg = $@"true#Xóa phiên bảng giá thành công";
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
