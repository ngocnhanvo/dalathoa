<%@ WebHandler Language="C#" Class="JQGridMD_00_DonMuaHangModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;

public class JQGridMD_00_DonMuaHangModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public class Master
    {
        public string id { get; set; }
        public string md_doitackinhdoanh_id { get; set; }
        public string ngaydonhang { get; set; }
        public string ngaygiaohang { get; set; }
        public string ngaythanhtoan { get; set; }
        public string diadiem_giaohang { get; set; }
        public string hinhthucthanhtoan { get; set; }
        public string md_dieukienthanhtoan_id { get; set; }
        public decimal? giamgia { get; set; }
        public decimal? chiphi { get; set; }
        public string mota { get; set; }
    }

    public class Details
    {
        public string md_sanpham_id { get; set; }
        public decimal? sl { get; set; }
        public decimal? gia { get; set; }
    }
    public Public pub = new Public();
    public EntityContext db = new EntityContext();
    public HelperEntity helperEntity = new HelperEntity();
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
            case "CA_01_TPNKMG2":
                this.CA_01_TPNKMG2(context);
                break;
            case "CA_01_TPNKMG":
                this.CA_01_TPNKMG(context);
                break;
            case "CA_01_KetThucDonMuHang":
                this.CA_01_KetThucDonMuHang(context);
                break;
            case "CA_01_TraVeSoanThaoDMH":
                this.CA_01_TraVeSoanThaoDMH(context);
                break;
            case "addKOV":
                this.addKOV(context);
                break;
            case "loadEdit":
                this.loadEdit(context);
                break;
            case "editKOV":
                this.editKOV(context);
                break;
            default:
                break;
        }
    }

    public void editKOV(HttpContext context)
    {
        string msg = "", idnew = Helper.getNewId();
        bool ok = false;
        var details = JsonConvert.DeserializeObject<List<Details>>(context.Request.Form["details"].removeAllSpaceOrTrimText(true));
        var master = JsonConvert.DeserializeObject<Master>(context.Request.Form["master"].removeAllSpaceOrTrimText(true));
        var id = master.id;
        string ma_dtkd = master.md_doitackinhdoanh_id;
        var object_ = db.c_donmuahang.Where(s => s.c_donmuahang_id == id).FirstOrDefault();
        if (object_ == null)
        {
            msg = $@"Đơn mua hàng không tồn tại.";
            goto EndEventHandler;
        }

        var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == ma_dtkd).FirstOrDefault();
        if (dtkd == null)
        {
            msg = $@"Không tìm thấy nhà cung cấp có mã ""{ma_dtkd}""";
            goto EndEventHandler;
        }

        try
        {
            details = details.Where(s => s.sl > 0).ToList();
            if (details.Count <= 0)
            {
                msg = $@"Không có dòng hàng";
                goto EndEventHandler;
            }

            decimal tongtien = 0;
            var idNotDels = new List<string>();
            foreach (var dt in details)
            {
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == dt.md_sanpham_id).FirstOrDefault();
                if (sp != null)
                {
                    var cdhServer = db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == id & s.md_sanpham_id == sp.md_sanpham_id).FirstOrDefault();
                    var cdh = db.c_donmuahang_cdmh.Local.Where(s => s.c_donmuahang_id == id & s.md_sanpham_id == sp.md_sanpham_id).FirstOrDefault();
                    if (cdh == null)
                    {
                        cdh = new c_donmuahang_cdmh();
                        cdh.c_donmuahang_cdmh_id = Helper.getNewId();
                        cdh.c_donmuahang_id = id;
                        cdh.md_sanpham_id = sp.md_sanpham_id;
                        cdh.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id;
                        cdh.dongiamua = dt.gia.GetValueOrDefault(0);
                        cdh.sl_dadat = dt.sl.GetValueOrDefault(0);
                        cdh.thanhtien = cdh.dongiamua * cdh.sl_dadat;
                        cdh = Helper.setDefaultValueWhenInsertOrUpdate(cdh, userTK, false);
                        db.c_donmuahang_cdmh.Add(cdh);
                    }
                    else
                    {
                        cdh.dongiamua = dt.gia.GetValueOrDefault(0);
                        cdh.sl_dadat = dt.sl.GetValueOrDefault(0);
                        cdh.thanhtien = cdh.dongiamua * cdh.sl_dadat;
                        cdh = Helper.setDefaultValueWhenInsertOrUpdate(cdh, userTK, true);
                    }
                    tongtien += cdh.thanhtien.GetValueOrDefault(0);
                    idNotDels.Add(dt.md_sanpham_id);
                }
            }

            var objectDels = db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == id & !idNotDels.Contains(s.md_sanpham_id)).ToList();
            if (objectDels.Count > 0)
                db.c_donmuahang_cdmh.RemoveRange(objectDels);

            object_.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
            object_.ngaydonhang = master.ngaydonhang.ToNullableDateTime();
            object_.ngaygiaohang = master.ngaygiaohang.ToNullableDateTime();
            object_.ngaythanhtoan = master.ngaythanhtoan.ToNullableDateTime();
            object_.diadiem_giaohang = master.diadiem_giaohang.removeAllSpaceOrTrimText(true);
            object_.hinhthucthanhtoan = master.hinhthucthanhtoan.removeAllSpaceOrTrimText(true);
            object_.md_dieukienthanhtoan_id = master.md_dieukienthanhtoan_id.removeAllSpaceOrTrimText(true);
            object_.mota = master.mota.removeAllSpaceOrTrimText(true);
            object_.giamgia = master.giamgia.GetValueOrDefault(0);
            object_.chiphi = master.chiphi.GetValueOrDefault(0);
            object_.tong_tienhang = tongtien;
            object_.tong_tatca = tongtien - object_.giamgia + object_.chiphi;
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
            ok = true;
            msg = "Sửa thành công";
            idnew = object_.c_donmuahang_id;
        }

        var rs = new
        {
            idnew = idnew,
            ok = ok,
            msg = msg
        };
        context.Response.Write(JsonConvert.SerializeObject(rs));
    }

    public void loadEdit(HttpContext context)
    {
        string msg = "", ma_ncc = "", ten_ncc = "", sdt = "", diachi = "";
        bool ok = false;
        string id = context.Request.Form["id"].removeAllSpaceOrTrimText(false);
        var donmuahang = db.c_donmuahang.Where(s => s.c_donmuahang_id == id).FirstOrDefault();
        if (donmuahang == null)
        {
            msg = "Không tìm thấy đơn mua hàng đã chọn";
            goto EndEventHandler;
        }

        var ncc = db.md_doitackinhdoanh.Where(s => s.md_doitackinhdoanh_id == donmuahang.md_doitackinhdoanh_id).FirstOrDefault();

        if (ncc != null)
        {
            ma_ncc = ncc.ma_dtkd;
            ten_ncc = ncc.ten_dtkd;
            sdt = ncc.tel;
            diachi = ncc.diachi;
        }

    EndEventHandler:;
        dynamic rs = new Dictionary<string, object> { { "ok", ok }, { "msg", msg } };
        if (msg.Length <= 0)
        {
            var details = (from a in db.c_donmuahang_cdmh
                           join b in db.md_sanpham on a.md_sanpham_id equals b.md_sanpham_id
                           join c in db.md_donvitinhsanpham on b.md_donvitinhsanpham_id equals c.md_donvitinhsanpham_id
                           where a.c_donmuahang_id == id
                           orderby b.ma_sanpham
                           select new
                           {
                               b.md_sanpham_id,
                               b.ma_sanpham,
                               b.mota_tiengviet,
                               md_donvitinhsanpham_id = c.ten_dvt,
                               sl_dathang = a.sl_dadat,
                               gianhap = a.dongiamua,
                               a.thanhtien
                           }).ToList();
            rs["ok"] = true;
            rs["master"] = new
            {
                trangthai = donmuahang.md_trangthai_id,
                sochungtu = donmuahang.sochungtu,
                ma_ncc = ma_ncc,
                ten_ncc = ten_ncc,
                sdt = sdt,
                diachi = diachi,
                ngaydonhang = donmuahang.ngaydonhang == null ? "" : donmuahang.ngaydonhang.Value.ToString("dd-MM-yyyy HH:mm"),
                ngaygiaohang = donmuahang.ngaygiaohang == null ? "" : donmuahang.ngaygiaohang.Value.ToString("dd-MM-yyyy HH:mm"),
                ngaythanhtoan = donmuahang.ngaythanhtoan == null ? "" : donmuahang.ngaythanhtoan.Value.ToString("dd-MM-yyyy HH:mm"),
                diadiem_giaohang = donmuahang.diadiem_giaohang,
                hinhthucthanhtoan = donmuahang.hinhthucthanhtoan,
                md_dieukienthanhtoan_id = donmuahang.md_dieukienthanhtoan_id,
                mota = donmuahang.mota,
                tong_tienhang = donmuahang.tong_tienhang.GetValueOrDefault(0),
                giamgia = donmuahang.giamgia.GetValueOrDefault(0),
                chiphi = donmuahang.chiphi.GetValueOrDefault(0),
                tong_tatca = donmuahang.tong_tatca.GetValueOrDefault(0)
            };
            rs["details"] = details;
        }
        else
        {
            rs["msg"] = msg;
        }

        context.Response.Write(JsonConvert.SerializeObject(rs));
    }

    public void addKOV(HttpContext context)
    {
        string msg = "", idnew = Helper.getNewId();
        bool ok = false;
        var details = JsonConvert.DeserializeObject<List<Details>>(context.Request.Form["details"].removeAllSpaceOrTrimText(true));
        var master = JsonConvert.DeserializeObject<Master>(context.Request.Form["master"].removeAllSpaceOrTrimText(true));


        string sochungtu = VNN_VariablePublic.sochungtu(db, "DMH", 1, false);
        string ma_dtkd = master.md_doitackinhdoanh_id;
        var donmuahang = db.c_donmuahang.Where(s => s.sochungtu == sochungtu).FirstOrDefault();
        if (donmuahang != null)
        {
            msg = $@"Đơn mua hàng đã tồn tại số phiếu {sochungtu}.";
            goto EndEventHandler;
        }

        var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == ma_dtkd).FirstOrDefault();
        if (dtkd == null)
        {
            msg = $@"Không tìm thấy nhà cung cấp có mã ""{ma_dtkd}""";
            goto EndEventHandler;
        }

        try
        {
            details = details.Where(s => s.sl > 0).ToList();
            if (details.Count <= 0)
            {
                msg = $@"Không có dòng hàng";
                goto EndEventHandler;
            }

            decimal tongtien = 0;
            foreach (var dt in details)
            {
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == dt.md_sanpham_id).FirstOrDefault();
                if (sp != null)
                {
                    var cdh = new c_donmuahang_cdmh();
                    cdh.c_donmuahang_cdmh_id = Helper.getNewId();
                    cdh.c_donmuahang_id = idnew;
                    cdh.md_sanpham_id = sp.md_sanpham_id;
                    cdh.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id;
                    cdh.dongiamua = dt.gia.GetValueOrDefault(0);
                    cdh.sl_dadat = dt.sl.GetValueOrDefault(0);
                    cdh.thanhtien = cdh.dongiamua * cdh.sl_dadat;
                    cdh = Helper.setDefaultValueWhenInsertOrUpdate(cdh, userTK, false);
                    db.c_donmuahang_cdmh.Add(cdh);
                    tongtien += cdh.thanhtien.GetValueOrDefault(0);
                }
            }

            var object_ = new c_donmuahang();
            object_.c_donmuahang_id = idnew;
            object_.md_trangthai_id = Helper.SOANTHAO;
            object_.sochungtu = sochungtu;
            object_.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
            object_.ngaydonhang = master.ngaydonhang.ToNullableDateTime();
            object_.ngaygiaohang = master.ngaygiaohang.ToNullableDateTime();
            object_.ngaythanhtoan = master.ngaythanhtoan.ToNullableDateTime();
            object_.diadiem_giaohang = master.diadiem_giaohang.removeAllSpaceOrTrimText(true);
            object_.hinhthucthanhtoan = master.hinhthucthanhtoan.removeAllSpaceOrTrimText(true);
            object_.md_dieukienthanhtoan_id = master.md_dieukienthanhtoan_id.removeAllSpaceOrTrimText(true);
            object_.mota = master.mota.removeAllSpaceOrTrimText(true);
            object_.giamgia = master.giamgia.GetValueOrDefault(0);
            object_.chiphi = master.chiphi.GetValueOrDefault(0);
            object_.tong_tienhang = tongtien;
            object_.tong_tatca = tongtien - object_.giamgia + object_.chiphi;
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            db.c_donmuahang.Add(object_);

            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            ok = true;
            msg = "Tạo thành công";
        }

        var rs = new
        {
            idnew = idnew,
            ok = ok
        };
        context.Response.Write(JsonConvert.SerializeObject(rs));
    }

    public void CA_01_TraVeSoanThaoDMH(HttpContext context)
    {
        string id = context.Request.Form["id"];
        string msg = "";
        try
        {
            var dmh = db.c_donmuahang.Where(s => s.c_donmuahang_id == id).FirstOrDefault();
            if (dmh == null)
            {
                msg = $@"Không tìm thấy đơn mua hàng";
                goto EndEventHandler;
            }

            if (dmh.md_trangthai_id != Helper.DANHAN)
            {
                msg = $@"Đơn mua hàng không ở trạng thái ""Đã xác nhận""";
                goto EndEventHandler;
            }

            dmh.md_trangthai_id = Helper.SOANTHAO;
            //dmh.ngaycapnhat = DateTime.Now;

            var cdhs = db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == dmh.c_donmuahang_id & s.sl_tonkho != null).ToList();
            foreach (var cdh in cdhs)
                cdh.sl_tonkho = null;
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"<div style='color:blue'>Xác nhận đơn mua hàng thành công</div>";
        }
        else
        {
            msg = $@"<div style='color:red' error>{msg}</div>";
        }
        context.Response.Write(msg);
    }

    public void CA_01_KetThucDonMuHang(HttpContext context)
    {
        string id = context.Request.Form["id"];
        string msg = "";
        try
        {
            var dmh = db.c_donmuahang.Where(s => s.c_donmuahang_id == id).FirstOrDefault();
            if (dmh == null)
            {
                msg = $@"Không tìm thấy đơn mua hàng";
                goto EndEventHandler;
            }

            var tts = new string[] { Helper.HIEULUC, "CHUAXONG" };
            if (!tts.Contains(dmh.md_trangthai_id))
            {
                msg = $@"Đơn mua hàng không ở trạng thái ""Hiệu Lực hoặc Chưa nhập hết""";
                goto EndEventHandler;
            }

            dmh.md_trangthai_id = Helper.KETTHUC;
            //dmh.ngaycapnhat = DateTime.Now;
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"<div style='color:blue'>Xác nhận đơn mua hàng thành công</div>";
        }
        else
        {
            msg = $@"<div style='color:red' error>{msg}</div>";
        }
        context.Response.Write(msg);
    }

    public void CA_01_TPNKMG(HttpContext context)
    {
        string id = context.Request.Form["id"];
        string msg = "";
        try
        {
            var dmh = db.c_donmuahang.Where(s => s.c_donmuahang_id == id).FirstOrDefault();
            if (dmh == null)
            {
                msg = $@"Không tìm thấy đơn mua hàng";
                goto EndEventHandler;
            }

            if (dmh.md_trangthai_id != Helper.SOANTHAO)
            {
                msg = $@"Đơn mua hàng không ở trạng thái ""Soạn Thảo""";
                goto EndEventHandler;
            }

            dmh.md_trangthai_id = Helper.DANHAN;
            dmh.ngayxacnhan = DateTime.Now;
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"<div style='color:blue'>Xác nhận đơn mua hàng thành công</div>";
        }
        else
        {
            msg = $@"<div style='color:red' error>{msg}</div>";
        }
        context.Response.Write(msg);
    }

    public void CA_01_TPNKMG2(HttpContext context)
    {
        string id = context.Request.Form["id"];
        string diadiemgiaohang = context.Request.Form["diadiemgiaohang"].removeAllSpaceOrTrimText(true);
        string msg = "";
        try
        {
            var dmh = db.c_donmuahang.Where(s => s.c_donmuahang_id == id).FirstOrDefault();
            if (dmh == null)
            {
                msg = $@"Không tìm thấy đơn mua hàng";
                goto EndEventHandler;
            }

            if (dmh.md_trangthai_id != Helper.SOANTHAO)
            {
                msg = $@"Đơn mua hàng không ở trạng thái ""Soạn thảo""";
                goto EndEventHandler;
            }

            var ngaynhap = context.Request.Form["ngaynhap"].removeAllSpaceOrTrimText(true).ToNullableDateTime();
            if (ngaynhap == null)
            {
                msg = $@"Ngày nhập không thể bỏ trống";
                goto EndEventHandler;
            }

            if (!ngaynhap.Value.IsDate())
            {
                msg = $@"Ngày nhập có giá trị sai";
                goto EndEventHandler;
            }

            var dmhCDHs = db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == dmh.c_donmuahang_id).ToList();
            if (dmhCDHs.Count <= 0)
            {
                msg = $@"Đơn mua hàng chưa có dòng hàng";
                goto EndEventHandler;
            }

            var kho = db.md_kho.Where(s => s.hoatdong == true).FirstOrDefault();
            if (kho == null)
            {
                msg = $@"Không tìm thấy kho đang hoạt động";
                goto EndEventHandler;
            }

            dmh.ngaygiaohang = ngaynhap;
            dmh.diadiem_giaohang = diadiemgiaohang;
            dmh.md_trangthai_id = Helper.HIEULUC;
            dmh.ngayhieuluc = DateTime.Now;

            foreach (var cdh in dmhCDHs)
            {
                var slnhap = cdh.sl_dadat.GetValueOrDefault(0);
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == cdh.md_sanpham_id).FirstOrDefault();
                cdh.sl_tonkho = db.md_kho_sanpham.Where(s => s.md_sanpham_id == cdh.md_sanpham_id).ToList().Sum(s => s.soluong.GetValueOrDefault(0));
                var khospServer = db.md_kho_sanpham.Where(s => s.md_kho_id == kho.md_kho_id & s.md_sanpham_id == cdh.md_sanpham_id).FirstOrDefault();
                var khosp = db.md_kho_sanpham.Local.Where(s => s.md_kho_id == kho.md_kho_id & s.md_sanpham_id == cdh.md_sanpham_id).FirstOrDefault();
                if (khosp != null)
                {
                    khosp.soluong = khosp.soluong.GetValueOrDefault(0) + slnhap;
                }
                else
                {
                    khosp = new md_kho_sanpham();
                    khosp.md_kho_sanpham_id = Helper.getNewId();
                    khosp.md_kho_id = khosp.md_kho_id;
                    khosp.md_sanpham_id = cdh.md_sanpham_id;
                    khosp.soluong = slnhap;
                    Helper.setDefaultValueWhenInsertOrUpdate(khosp, userTK, false);
                    db.md_kho_sanpham.Add(khosp);
                }

                helperEntity.obj = new HelperEntity.objLSNX();
                helperEntity.obj.spId = cdh.md_sanpham_id;
                helperEntity.obj.dvtSpId = sp.md_donvitinhsanpham_id;
                helperEntity.obj.slDichChuyen = slnhap;
                helperEntity.obj.dongNhapXuat = dmh.sochungtu;
                helperEntity.obj.sctDonHang = dmh.sochungtu;
                helperEntity.obj.giaTriVND = cdh.dongiamua.GetValueOrDefault(0);
                helperEntity.obj.khoId = kho.md_kho_id;
                helperEntity.obj.kieuchuyen = helperEntity.kieuNhapKho;
                helperEntity.obj.ngayChuyen = dmh.ngaygiaohang;
                helperEntity.obj.theoKg = false;
                helperEntity.obj.laphieuKK = false;
                helperEntity.themHoacSuaLichSuNhapXuatKho(db, userTK);

                var sptksServer = db.md_kho_sanpham.Where(s => s.md_sanpham_id == sp.md_sanpham_id).ToList();
                var sptks = db.md_kho_sanpham.Local.Where(s => s.md_sanpham_id == sp.md_sanpham_id).ToList();
                sp.tonkho = sptks.Sum(s => s.soluong.GetValueOrDefault(0));
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
            msg = $@"<div style='color:blue'>Hiệu lực đơn mua hàng và nhập kho thành công</div>";
        }
        else
        {
            msg = $@"<div style='color:red' error>{msg}</div>";
        }
        context.Response.Write(msg);
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string ma_dtkd = context.Request.Form["md_doitackinhdoanh_id"].removeAllSpaceOrTrimText(true);
        try
        {
            string sochungtu = VNN_VariablePublic.sochungtu(db, "DMH", 1, false);
            var donmuahang = db.c_donmuahang.Where(s => s.sochungtu == sochungtu).FirstOrDefault();
            if (donmuahang != null)
            {
                msg = $@"Đơn mua hàng đã tồn tại số phiếu {sochungtu}.";
                goto EndEventHandler;
            }

            var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == ma_dtkd).FirstOrDefault();
            if (dtkd == null)
            {
                msg = $@"Không tìm thấy nhà cung cấp có mã ""{ma_dtkd}""";
                goto EndEventHandler;
            }

            var object_ = new c_donmuahang();
            object_.c_donmuahang_id = id_new;
            VNN_Function.SetFormValue(object_.nameof(s => s.sochungtu), sochungtu);
            VNN_Function.SetFormValue(object_.nameof(s => s.md_doitackinhdoanh_id), dtkd.md_doitackinhdoanh_id);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_.tong_tatca = object_.tong_tienhang.GetValueOrDefault(0) + object_.chiphi.GetValueOrDefault(0) - object_.giamgia.GetValueOrDefault(0);
            db.c_donmuahang.Add(object_);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
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

    //nht
    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string ma_dtkd = context.Request.Form["md_doitackinhdoanh_id"].removeAllSpaceOrTrimText(true);
        string id = context.Request.Form["id"].removeAllSpaceOrTrimText(true);
        try
        {
            var object_ = db.c_donmuahang.Where(s => s.c_donmuahang_id == id).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Không tìm thấy đơn mua hàng.";
                goto EndEventHandler;
            }

            var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == ma_dtkd).FirstOrDefault();
            if (dtkd == null)
            {
                msg = $@"Không tìm thấy nhà cung cấp có mã ""{ma_dtkd}""";
                goto EndEventHandler;
            }

            if (object_.md_trangthai_id == Helper.HIEULUC)
            {
                if (object_.md_doitackinhdoanh_id != dtkd.md_doitackinhdoanh_id)
                {
                    msg = $@"Không thể thay đổi nhà cung cấp khi đã Hiệu Lực";
                    goto EndEventHandler;
                }
            }

            VNN_Function.SetFormValue(object_.nameof(s => s.md_doitackinhdoanh_id), dtkd.md_doitackinhdoanh_id);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_.tong_tatca = object_.tong_tienhang.GetValueOrDefault(0) + object_.chiphi.GetValueOrDefault(0) - object_.giamgia.GetValueOrDefault(0);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"true#Sửa thành công";
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
            var object_s = db.c_donmuahang.Where(p => ids.Contains(p.c_donmuahang_id)).ToList();
            if (object_s.Count <= 0)
            {
                msg = $@"<br>Không tìm thấy đối tượng cần xóa";
                goto EndEventHandler;
            }

            var dsdhids = object_s.Select(s => s.c_donmuahang_id).ToList();
            var dbhDaHL = db.c_donmuahang.Where(s => dsdhids.Contains(s.c_donmuahang_id) & s.md_trangthai_id != Helper.SOANTHAO).Count() > 0;
            if (dbhDaHL)
            {
                msg = $@"<br>Đơn mua hàng này không ở trạng thái ""Soạn Thảo""";
                goto EndEventHandler;
            }

            foreach (var object_ in object_s)
            {
                //VNN_Function.Write_log(context, ma_module, null, oper, object_.c_danhsachdathang_id, db);
                db.c_donmuahang.Remove(object_);
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
            msg = $@"true#Xóa thành công";
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