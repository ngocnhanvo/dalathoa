<%@ WebHandler Language="C#" Class="JQGridMD_00_XuatnoiboModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;

public class JQGridMD_00_XuatnoiboModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "edit":
                this.edit(context);
                break;
            case "del":
                this.del(context);
                break;
            case "CA_01_XuatKhoNB":
                this.CA_01_XuatKhoNB(context);
                break;
            case "CA_01_TaodongxuatkhotuPNK":
                this.CA_01_TaodongxuatkhotuPNK(context);
                break;
            case "CA_01_XacNhanPXKNB":
                this.CA_01_XacNhanPXKNB(context);
                break;
            default:
                break;
        }

        db.Dispose();
    }

    public void CA_01_XacNhanPXKNB(HttpContext context)
    {
        string msg = "Không có chức năng này";
        context.Response.Write(msg);
    }

    public void CA_01_TaodongxuatkhotuPNK(HttpContext context)
    {
        string msg = "Không có chức năng này";
        context.Response.Write(msg);
    }

    public void CA_01_XuatKhoNB(HttpContext context)
    {
        string msg = "", msg_success = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        var ngayxuat = context.Request.Form["ngayxuat"];
        var ktSL = (context.Request.Form["ktSL"] + "").ToLower() == "true";
        int count_slxuat = 0;
        var rows = context.Request.Form["rows"];
        var msgErrs = new List<Public.BaoLoiKhiHieuLuc>();
        var pub = new Public();
        var spsghino = new List<string>();
        try
        {
            var xk = db.md_xuatkhonb.Where(s => s.md_xuatkhonb_id == id).FirstOrDefault();
            if (xk == null)
            {
                msg = $@"Lỗi: Không tìm thấy phiếu xuất kho.";
                goto EndEventHandler;
            }

            if (xk.trangthai != Helper.DANHAN)
            {
                msg = $@"Lỗi: dòng ""{xk.sochungtu}"" không ở trạng thái ""Đã xác nhận"".";
                goto EndEventHandler;
            }

            var ngaychuyen = VNN_Config.setDateTime(ngayxuat);
            if (!ngaychuyen.IsDate())
            {
                msg = $@"Lỗi: dòng ""{xk.sochungtu}"" có giá trị ngày xuất kho bị sai.";
                goto EndEventHandler;
            }

            var lsx2 = db.md_lenhsanxuat2.Where(s => s.sochungtu == xk.chungtu_lenhsx).FirstOrDefault();
            if(lsx2 == null & xk.bosung != 1)
            {
                msg = $@"Lỗi: dòng ""{xk.sochungtu}"" không xác định được ""Lệnh sản xuất"".";
                goto EndEventHandler;
            }

            xk.ngaychuyen = ngaychuyen;
            var dongHangs = JsonConvert.DeserializeObject<List<md_xuatkhonb_cdh>>(rows);
            var xknbDHs = dongHangs.Where(s => s.sl_thucxuat != null).Select(s => s.md_xuatkhonb_cdh_id).ToList();
            dongHangs = dongHangs.Where(s => s.sl_thucxuat != null).ToList();
            //var md_xuatkhonb_cdhs = .ToList();

            var kho = db.md_kho.Where(s => s.md_kho_id == xk.tukho).FirstOrDefault();
            if (kho == null)
            {
                msg = $@"Lỗi: dòng ""{xk.sochungtu}"" không có kho xuất.";
                goto EndEventHandler;
            }

            var md_xuatkhonb_cdhs = (from a in db.md_xuatkhonb_cdh
                                     join b in db.md_sanpham on a.md_sanpham_id equals b.md_sanpham_id
                                     where a.md_xuatkhonb_id == xk.md_xuatkhonb_id & xknbDHs.Contains(a.md_xuatkhonb_cdh_id)
                                     orderby b.ma_sanpham ascending
                                     select new { a, b }
                                     ).ToList();

            foreach (var dhsp in md_xuatkhonb_cdhs)
            {
                var dh = dhsp.a;
                var sp = dhsp.b;
                var dongHang = dongHangs.Where(s => s.md_xuatkhonb_cdh_id == dh.md_xuatkhonb_cdh_id).FirstOrDefault();
                if (dongHang != null)
                {
                    dh.sl_thucxuat = dongHang.sl_thucxuat;
                    dh.ghino = dongHang.ghino;
                    if(string.IsNullOrWhiteSpace(dh.tenhang))
                        dh.tenhang = xk.donhang_thamchieu;
                }

                if (sp == null)
                {
                    msg = $@"Lỗi: ""{dh.md_sanpham_id}"" không tìm thấy mã HHVT từ ID này.";
                    goto EndEventHandler;
                }

                var khospServer = db.md_kho_sanpham.Where(s => s.md_kho_id == xk.tukho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
                var khosp = db.md_kho_sanpham.Local.Where(s => s.md_kho_id == xk.tukho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();

                if (khosp == null)
                {
                    if (dh.sl_thucxuat > 0)
                    {
                        msgErrs.Add(new Public.BaoLoiKhiHieuLuc()
                        {
                            msp = sp.ma_sanpham,
                            loi = "Không có trong kho"
                        });
                    }
                }
                else if (khosp.soluong < dh.sl_thucxuat)
                {
                    string tendvt = db.md_donvitinhsanpham.
                            Where(s => s.md_donvitinhsanpham_id == sp.md_donvitinhsanpham_id).Select(s => s.ten_dvt).FirstOrDefault();
                    msgErrs.Add(new Public.BaoLoiKhiHieuLuc()
                    {
                        msp = sp.ma_sanpham,
                        loi = $@"Số lượng trong kho chỉ còn: ""{khosp.soluong.Value.DropTrailingZeros()} {tendvt}"""
                    });
                }

                dh.sl_tonkho = khosp == null ? 0 : khosp.soluong.GetValueOrDefault(0);

                if (dh.ghino > 0)
                {
                    var ghinoChoXuongServer = db.md_kho_ghino.Where(s =>
                        s.md_phanxuong_id == xk.xuatden
                        & s.md_sanpham_id == dh.md_sanpham_id
                        & s.soluong_no > 0).FirstOrDefault();

                    var ghinoChoXuong = db.md_kho_ghino.Local.Where(s =>
                        s.md_phanxuong_id == xk.xuatden
                        & s.md_sanpham_id == dh.md_sanpham_id
                        & s.soluong_no > 0).FirstOrDefault();

                    var ghino = (dh.ghino.GetValueOrDefault(0)).Set0WhenlessThan0();
                    if (ghinoChoXuong == null)
                    {
                        ghinoChoXuong = new md_kho_ghino();
                        ghinoChoXuong.md_kho_ghino_id = Helper.getNewId();
                        ghinoChoXuong.md_phanxuong_id = xk.xuatden;
                        ghinoChoXuong.md_sanpham_id = dh.md_sanpham_id;
                        ghinoChoXuong.soluong_no = ghino;
                        ghinoChoXuong.sctlienquan = xk.chungtu_lenhsx;
                        ghinoChoXuong.lsx_to = sp.ma_sanpham;
                        ghinoChoXuong.ngayno = DateTime.Now;
                        db.md_kho_ghino.Add(ghinoChoXuong);
                    }
                    else
                    {
                        if (!ghinoChoXuong.sctlienquan.Contains(xk.chungtu_lenhsx))
                            ghinoChoXuong.sctlienquan += "," + xk.chungtu_lenhsx;
                        ghinoChoXuong.soluong_no = ghinoChoXuong.soluong_no.GetValueOrDefault(0) + ghino;
                    }

                    spsghino.Add(dh.md_sanpham_id);
                }

                decimal sl_xuat = dh.sl_thucxuat.GetValueOrDefault(0);
                if (sl_xuat > 0 & msgErrs.Count <= 0)
                {
                    decimal sl_xuatVT = dh.sl_thucxuat.GetValueOrDefault(0);

                    if (lsx2 != null)
                    {
                        var tsxvtids = dh.lsx_to.Split(',').ToList();
                        var vtsServer = db.md_lenhsanxuat_tosx_vattu.Where(s => tsxvtids.Contains(s.md_lenhsanxuat_tosx_vattu_id)).ToList();
                        var vts = db.md_lenhsanxuat_tosx_vattu.Local.Where(s => tsxvtids.Contains(s.md_lenhsanxuat_tosx_vattu_id)).ToList();
                        md_lenhsanxuat_tosx_vattu vtcc = null;
                        foreach (var vt in vts)
                        {
                            var tsx = db.md_lenhsanxuat_tosx.Where(s => s.md_lenhsanxuat_tosx_id == vt.md_lenhsanxuat_tosx_id).FirstOrDefault();
                            var stt = tsx == null ? 0 : tsx.stt;
                            var mathaydoi = stt == 9998 ? vt.sp3 : vt.sp2;
                            var sltd = vt.soluong.GetValueOrDefault(0) - vt.sl_hanngach.GetValueOrDefault(0);
                            if (sltd > 0)
                            {
                                var slx = sl_xuatVT > sltd ? sltd : sl_xuatVT;
                                sl_xuatVT = sl_xuatVT - slx;
                                vt.sl_hanngach = vt.sl_hanngach.GetValueOrDefault(0) + slx;

                                vtcc = vt;

                                var cdhTUServer = db.md_lenhsanxuat_tosx_cdh.Where(s =>
                                    s.md_sanpham_id == vt.md_sanpham_id
                                    & s.macuoi == vt.sp1
                                    & s.mathaydoi == mathaydoi
                                    & s.lsxTen == lsx2.donhang
                                    & s.xuongChinh == lsx2.xuongPhu
                                    & s.md_lenhsanxuat_id == vt.md_lenhsanxuat_id
                                    ).FirstOrDefault();

                                var cdhTU = db.md_lenhsanxuat_tosx_cdh.Local.Where(s =>
                                    s.md_sanpham_id == vt.md_sanpham_id
                                    & s.macuoi == vt.sp1
                                    & s.mathaydoi == mathaydoi
                                    & s.lsxTen == lsx2.donhang
                                    & s.xuongChinh == lsx2.xuongPhu
                                    & s.md_lenhsanxuat_id == vt.md_lenhsanxuat_id
                                    ).FirstOrDefault();
                                if (cdhTU != null)
                                {
                                    cdhTU.sl_dagiao = cdhTU.sl_dagiao.GetValueOrDefault(0) + slx;
                                }
                            }
                        }

                        if (sl_xuatVT > 0 & vtcc != null)
                        {
                            vtcc.sl_hanngach = vtcc.sl_hanngach.GetValueOrDefault(0) + sl_xuatVT;
                        }
                    }

                    khosp.soluong = khosp.soluong.GetValueOrDefault(0) - sl_xuat;
                    var lsXK = new md_kho_giaodich
                    {
                        md_kho_giaodich_id = Helper.getNewId(),
                        md_kho_id = khosp.md_kho_id,
                        md_sanpham_id = khosp.md_sanpham_id,
                        soluong_dichchuyen = sl_xuat,
                        md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id,
                        ngaychuyen = xk.ngaychuyen,
                        kieuchuyen = Helper.XuatKho,
                        dongnhapxuat = xk.sochungtu,
                        dongkiemkho = xk.sochungtu,
                        dongvanchuyen = xk.sochungtu,
                        dongsanxuat = xk.sochungtu,
                        sapxep = "",
                        sanxuat = 0,
                        mota = xk.donhang_thamchieu,
                        donhang = xk.donhang_thamchieu,
                        hoatdong = true
                    };
                    lsXK = Helper.setDefaultValueWhenInsertOrUpdate(lsXK, userTK, false);
                    db.md_kho_giaodich.Add(lsXK);
                    count_slxuat = 1;
                }
            }

            if (ktSL == true & msgErrs.Count <= 0)
            {
                if (count_slxuat <= 0 & msg == "")
                {
                    msg += $@"Lỗi: Dòng ""{xk.sochungtu}"" phải có ít nhất 1 dòng hàng có số lượng lớn hơn 0";
                    goto EndEventHandler;
                }
            }

            //Kiem tra de chuyen thanh Hieu Luc
            if (msgErrs.Count <= 0)
            {
                msg = msg_success + msg;
                xk.trangthai = Helper.HIEULUC;
                xk.ngayhieuluc = DateTime.Now;
                xk.nguoiHL = userTK.ad_user_id;
                db.SaveChanges();

                if (spsghino.Count > 0)
                {
                    var pxknbs = db.md_xuatkhonb.Where(s => s.bosung == 0 & s.xuatden == xk.xuatden & s.trangthai != Helper.HIEULUC).ToList();
                    foreach (var pxknb in pxknbs)
                    {
                        pub.ghiNoChoSX(db, null, pxknb.chungtu_lenhsx, spsghino);
                        var pxknbDHs = db.md_xuatkhonb_cdh.Where(s =>
                            s.md_xuatkhonb_id == pxknb.md_xuatkhonb_id
                            & spsghino.Contains(s.md_sanpham_id)).ToList();
                        foreach (var dh in pxknbDHs)
                        {
                            var trunoServer = db.md_kho_ghino.Where(s =>
                                    s.md_phanxuong_id == pxknb.xuatden
                                    & s.md_sanpham_id == dh.md_sanpham_id
                                    & s.sctlienquan == pxknb.chungtu_lenhsx
                                    & s.soluong_no < 0).ToList().Sum(s => s.soluong_no.GetValueOrDefault(0));
                            var truno = db.md_kho_ghino.Local.Where(s =>
                                s.md_phanxuong_id == pxknb.xuatden
                                & s.md_sanpham_id == dh.md_sanpham_id
                                & s.sctlienquan == pxknb.chungtu_lenhsx
                                & s.soluong_no < 0).ToList().Sum(s => s.soluong_no.GetValueOrDefault(0));

                            dh.truno = 0 - truno;
                            var sltd = dh.tong_sl_xuat.GetValueOrDefault(0) - dh.truno.GetValueOrDefault(0);
                            if (dh.sl_muonxuat > sltd)
                                dh.sl_muonxuat = sltd;
                        }
                        db.SaveChanges();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;

        if (msg.Length <= 0 & msgErrs.Count <= 0)
        {
            msg = $@"<div style='color:blue'>Hiệu lực phiếu xuất thành công</div>";
        }
        else
        {
            if (msgErrs.Count > 0)
                msg = $@"Thiếu thông tin";

            var result = new
            {
                msg = $@"<div error style='color:red'>{msg}</div>",
                json = msgErrs
            };

            msg = JsonConvert.SerializeObject(result);
        }

        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string bosung = context.Request.Form["bosung"];
        var xuatVTtheoBOM = bosung == "0";
        var xuatVTboSung = bosung == "1";
        var xuatHHTron = bosung == "2";
        var xuatHangTho = bosung == "3";
        string tukho = context.Request.Form["tukho"];
        string xuatden = context.Request.Form["xuatden"];
        string id = context.Request.Form["id"];
        try
        {
            DateTime ngaychuyen = VNN_Config.setDateTime(context.Request.Form["ngaychuyen"]);

            var object_ = db.md_xuatkhonb.Where(p => p.md_xuatkhonb_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Lỗi:Không tìm thấy đối tượng cần sửa";
                goto EndEventHandler;
            }
            if (object_.trangthai != Helper.DANHAN)
            {
                msg = @"Lỗi:Chỉ có thể sửa phiếu đang ""Đã xác nhận""";
                goto EndEventHandler;
            }
            if (ngaychuyen.IsDate() == false)
            {
                msg = "Lỗi: Ngày xuất kho không đúng định dạng";
                goto EndEventHandler;
            }

            var khoXuat = db.md_kho.Where(s => s.md_kho_id == tukho).FirstOrDefault();
            if (khoXuat == null)
            {
                msg = $@"Lỗi: Kho xuất không tồn tại.";
                goto EndEventHandler;
            }

            var xuatChoBP = db.ad_department.Where(s => s.md_phongban_id == xuatden).FirstOrDefault();
            if (xuatVTboSung)
            {
                if (xuatChoBP == null)
                {
                    msg = $@"Lỗi: Bộ phận cần xuất vật tư không tồn tại.";
                    goto EndEventHandler;
                }
                object_.xuatden = xuatChoBP.md_phongban_id;
            }
            object_.tukho = khoXuat.md_kho_id;
            object_.mota = context.Request.Form["mota"];
            object_.ngaychuyen = ngaychuyen;
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = "true#Cập nhật thành công.";
        }
        else
        {
            msg = "false#" + msg;
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
                    var object_ = db.md_xuatkhonb.Where(p => p.md_xuatkhonb_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần trả.", id_del_);
                    }
                    else if (object_.trangthai == Helper.HIEULUC)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Đã được ""Hiệu Lực"".", object_.sochungtu);
                    }
                    else
                    {
                        VNN_Function.Write_log(context, ma_module, null, oper, "PXKNB(ST):" + object_.sochungtu, db);
                        object_.trangthai = "SOANTHAO";
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
                transaction.Commit();
                msg = @"true#Trả phiếu xuất kho dã chọn về ""Soạn Thảo"" thành công.";
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