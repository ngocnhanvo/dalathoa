<%@ WebHandler Language="C#" Class="JQGridMD_00_HoaDonModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using DataAcess;
using Newtonsoft.Json;
using System.Collections.Generic;

public class JQGridMD_00_HoaDonModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private const string NGUON_SOQUY_HOADON = "HOADON_BANHANG";

    public class Master
    {
        public string loai { get; set; }
        public string nhanvien { get; set; }
        public string ngay { get; set; }
        public string nguoimuaid { get; set; }
        public string nguoimua { get; set; }
        public decimal? tongsoluong { get; set; }
        public decimal? tongtienhang { get; set; }
        public decimal? giamgia { get; set; }
        public string loaithu { get; set; }
        public decimal? thuthue { get; set; }
        public decimal? thuvanchuyen { get; set; }
        public decimal? thukhac { get; set; }
        public string hinhthucthanhtoan { get; set; }
        public decimal? khachcantra { get; set; }
        public decimal? khachthanhtoan { get; set; }
        public decimal? tienthua { get; set; }
        public string mota { get; set; }
        public string nguoinhan { get; set; }
        public string sdt_nguoinhan { get; set; }
        public string diachi_nguoinhan { get; set; }
        public bool? thuho_cod { get; set; }
        public decimal? cod { get; set; }
        public string thongtinnhanhang { get; set; }
        public string ngay_kov { get; set; }
        public string ngaygiao { get; set; }
        public string thongtinsanpham { get; set; }
        public string thongtinthanhtoan { get; set; }
        public string thongtinxuathoadon { get; set; }
        public string trangthaithanhtoan { get; set; }
        public string trangthaigiaohang { get; set; }
        public string trangthaihoadon { get; set; }
        public string trangthaicam { get; set; }
    }

    public class Details
    {
        public int? stt { get; set; }
        public string key_id { get; set; }
        public string md_sanpham_id { get; set; }
        public string md_sanpham_pr_id { get; set; }
        public string mahang { get; set; }
        public string tenhang { get; set; }
        public string dvt { get; set; }
        public decimal? sl { get; set; }
        public decimal? gia { get; set; }
        public decimal? thanhtien { get; set; }
    }

    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    public HelperEntity helperEntity = new HelperEntity();
    User_TK userTK = null;

    public string oper = "vnn";

    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            oper = context.Request.QueryString["oper"] == null
                ? context.Request.Form["oper"]
                : context.Request.QueryString["oper"];

            userTK = VNN_Function.get_user(
                Security.id_taikhoan(context),
                Security.id_vaitro(context),
                Security.id_phongban(context),
                db
            );
        }

        switch (oper)
        {
            case "CA_01_HieuLucHoaDon":
                this.CA_01_HieuLucHoaDon(context);
                break;
            case "loadEdit":
                this.loadEdit(context);
                break;
            case "CA_01_SuaHoaDon":
                this.CA_01_SuaHoaDon(context);
                break;
            case "CA_01_THemMoiHoaDon":
                this.CA_01_THemMoiHoaDon(context);
                break;
            case "del":
                this.del(context);
                break;
            default:
                break;
        }
    }

    // Tổng tiền thực thu của hóa đơn lấy từ các allocation còn hiệu lực.
    // Giữ fallback legacy để dữ liệu cũ chưa có c_soquy_hoadon vẫn đọc đúng.
    private decimal tongDaThuHoaDon(string hoaDonId)
    {
        decimal tuDetail =
            (from pb in db.c_soquy_hoadon
             join sq in db.c_soquy
                on pb.c_soquy_id equals sq.c_soquy_id
             where pb.c_hoadonbanhang_id == hoaDonId
                && pb.hoatdong != false
                && sq.trangthai != Helper.HUYBO
             select (decimal?)pb.sotien_phanbo)
            .Sum()
            .GetValueOrDefault(0);

        decimal tuLegacy = db.c_soquy
            .Where(s =>
                s.c_hoadonbanhang_id == hoaDonId
                && s.trangthai != Helper.HUYBO
                && !db.c_soquy_hoadon.Any(pb =>
                    pb.c_soquy_id == s.c_soquy_id
                    && pb.c_hoadonbanhang_id == hoaDonId
                    && pb.hoatdong != false)
            )
            .Select(s => (decimal?)s.sotien)
            .Sum()
            .GetValueOrDefault(0);

        return tuDetail + tuLegacy;
    }

    // khachthanhtoan_kov được tận dụng làm snapshot "đã thanh toán".
    // trangthaithanhtoan vẫn độc lập, tuyệt đối không tự đổi tại đây.
    private void dongBoThanhToanHoaDon(string hoaDonId)
    {
        if (string.IsNullOrWhiteSpace(hoaDonId))
            return;

        var hd = db.c_hoadonbanhang.FirstOrDefault(h =>
            h.c_hoadonbanhang_id == hoaDonId);

        if (hd == null)
            return;

        decimal daThanhToan = tongDaThuHoaDon(hoaDonId);
        decimal khachCanTra = hd.khachcantra_kov.GetValueOrDefault(0);

        hd.khachthanhtoan_kov = daThanhToan;
        hd.ghino_kov = Math.Max(0, khachCanTra - daThanhToan);
        Helper.setDefaultValueWhenInsertOrUpdate(hd, userTK, true);
    }

    // Phiếu thu sinh trực tiếp lúc lập hóa đơn vẫn dùng c_soquy.c_hoadonbanhang_id
    // để tương thích code cũ, nhưng quan hệ tiền chuẩn được ghi thêm vào c_soquy_hoadon.
    // Khi sửa hóa đơn, không lấy khachthanhtoan_kov hiện tại để sửa số tiền phiếu nguồn,
    // vì cột này lúc đó đã là tổng đã thanh toán (có thể gồm nhiều phiếu thu khác).
    private string dongBoSoQuyHoaDon(
        c_hoadonbanhang hoadon,
        md_doitackinhdoanh khachhang,
        DateTime ngayGiaoDich,
        decimal soTienThuLucLap,
        bool actionEdit
    )
    {
        if (hoadon == null)
            return "Không tìm thấy hóa đơn để đồng bộ sổ quỹ.";

        if (string.IsNullOrWhiteSpace(hoadon.c_hoadonbanhang_id))
            return "Hóa đơn chưa có ID.";

        // Query DB để hydrate entity, sau đó ưu tiên Local đúng convention của project.
        var soQuyServer = db.c_soquy.FirstOrDefault(s =>
            s.c_hoadonbanhang_id == hoadon.c_hoadonbanhang_id
            && s.nguon_nghiepvu == NGUON_SOQUY_HOADON
            && s.trangthai != Helper.HUYBO);

        var soQuy = db.c_soquy.Local.FirstOrDefault(s =>
            s.c_hoadonbanhang_id == hoadon.c_hoadonbanhang_id
            && s.nguon_nghiepvu == NGUON_SOQUY_HOADON
            && s.trangthai != Helper.HUYBO)
            ?? soQuyServer;

        decimal soTienThu;

        if (actionEdit)
        {
            // Hóa đơn đã có: chỉ đồng bộ metadata của phiếu nguồn hiện hữu.
            // Không tạo phiếu mới từ khachthanhtoan_kov vì đó đã là tổng lũy kế.
            if (soQuy == null)
                return "";

            soTienThu = soQuy.sotien;
        }
        else
        {
            soTienThu = Math.Max(0, soTienThuLucLap);
        }

        if (soTienThu <= 0)
        {
            if (soQuy != null)
            {
                soQuy.trangthai = Helper.HUYBO;
                Helper.setDefaultValueWhenInsertOrUpdate(soQuy, userTK, true);

                var oldAllocations = db.c_soquy_hoadon
                    .Where(x =>
                        x.c_soquy_id == soQuy.c_soquy_id
                        && x.c_hoadonbanhang_id == hoadon.c_hoadonbanhang_id
                        && x.hoatdong != false)
                    .ToList();

                foreach (var allocation2 in oldAllocations)
                {
                    allocation2.hoatdong = false;
                    Helper.setDefaultValueWhenInsertOrUpdate(allocation2, userTK, true);
                }
            }

            return "";
        }

        var loaiThu = db.md_loaithuchi.FirstOrDefault(p =>
            p.ma_loaithuchi == NGUON_SOQUY_HOADON
            && p.loai_giaodich == "THU");

        if (loaiThu == null)
            return $@"Không tìm thấy loại thu ""{NGUON_SOQUY_HOADON}""";

        bool taoMoi = soQuy == null;

        if (taoMoi)
        {
            string soChungTu = VNN_VariablePublic.sochungtu(db, "PT", 1, false);
            if (string.IsNullOrWhiteSpace(soChungTu))
                return "Không thể sinh số phiếu thu.";

            if (db.c_soquy.Any(s => s.ma_phieu == soChungTu))
                return "Sổ quỹ đã tồn tại số phiếu " + soChungTu + ".";

            soQuy = new c_soquy();
            soQuy.c_soquy_id = Helper.getNewId();
            soQuy.trangthai = Helper.HIEULUC;
            soQuy.ma_phieu = soChungTu;
            soQuy.loai_giaodich = "THU";
            soQuy.nguon_nghiepvu = NGUON_SOQUY_HOADON;
            soQuy.c_hoadonbanhang_id = hoadon.c_hoadonbanhang_id;
        }

        soQuy.ngay_giaodich = ngayGiaoDich;
        soQuy.md_loaithuchi_id = loaiThu.md_loaithuchi_id;
        soQuy.md_doitackinhdoanh_id = hoadon.nguoimuaid_kov;
        soQuy.nguoi_nop_nhan = !string.IsNullOrWhiteSpace(hoadon.nguoimua_kov)
            ? hoadon.nguoimua_kov
            : (khachhang != null ? khachhang.ten_dtkd : "");
        soQuy.nguoi_thuchi = hoadon.nhanvien_kov;
        soQuy.phuongthucthanhtoan = hoadon.hinhthucthanhtoan_kov;
        soQuy.sotien = soTienThu;
        soQuy.diengiai = "Thu tiền hóa đơn " + hoadon.sochungtu;

        if (taoMoi)
        {
            soQuy = Helper.setDefaultValueWhenInsertOrUpdate(soQuy, userTK, false);
            db.c_soquy.Add(soQuy);
        }
        else
        {
            soQuy = Helper.setDefaultValueWhenInsertOrUpdate(soQuy, userTK, true);
        }

        // Upsert allocation của phiếu nguồn vào hóa đơn.
        var allocationRows = db.c_soquy_hoadon
            .Where(x =>
                x.c_soquy_id == soQuy.c_soquy_id
                && x.c_hoadonbanhang_id == hoadon.c_hoadonbanhang_id)
            .ToList();

        var allocation = allocationRows.FirstOrDefault();
        if (allocation == null)
        {
            allocation = new c_soquy_hoadon();
            allocation.c_soquy_hoadon_id = Helper.getNewId();
            allocation.c_soquy_id = soQuy.c_soquy_id;
            allocation.c_hoadonbanhang_id = hoadon.c_hoadonbanhang_id;
            allocation.sotien_phanbo = soQuy.sotien;
            allocation.hoatdong = true;
            allocation = Helper.setDefaultValueWhenInsertOrUpdate(allocation, userTK, false);
            db.c_soquy_hoadon.Add(allocation);
        }
        else
        {
            allocation.sotien_phanbo = soQuy.sotien;
            allocation.hoatdong = true;
            allocation = Helper.setDefaultValueWhenInsertOrUpdate(allocation, userTK, true);

            foreach (var duplicate in allocationRows.Skip(1))
            {
                duplicate.hoatdong = false;
                Helper.setDefaultValueWhenInsertOrUpdate(duplicate, userTK, true);
            }
        }

        return "";
    }

    public void CA_01_HieuLucHoaDon(HttpContext context)
    {
        string id = context.Request.Form["id"];
        string msg = "";
        try
        {
            var hoadon = db.c_hoadonbanhang.FirstOrDefault(s => s.c_hoadonbanhang_id == id);
            if (hoadon == null)
            {
                msg = "Không tìm thấy hóa đơn bán hàng";
                goto EndEventHandler;
            }

            if (hoadon.trangthai != Helper.SOANTHAO)
            {
                msg = $@"Hóa đơn không ở trạng thái ""Soạn thảo""";
                goto EndEventHandler;
            }

            var dmhCDHs = db.c_hoadonbanhang_cdmh
                .Where(s => s.c_hoadonbanhang_id == hoadon.c_hoadonbanhang_id)
                .ToList();

            if (dmhCDHs.Count <= 0)
            {
                msg = "Hóa đơn chưa có dòng hàng";
                goto EndEventHandler;
            }

            hoadon.trangthai = Helper.KETTHUC;
            hoadon.ngayhieuluc = DateTime.Now;
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;
        if (msg.Length <= 0)
            msg = "<div style='color:blue'>Hiệu lực đơn mua hàng và nhập kho thành công</div>";
        else
            msg = $@"<div style='color:red' error>{msg}</div>";

        context.Response.Write(msg);
    }

    public void loadEdit(HttpContext context)
    {
        string msg = "", sctdonhang = "", trangthaidonhang = "", trangthaithanhtoan = "",
            hoten_nguoimua = "", sdt_nguoimua = "", diachi_nguoimua = "";
        bool ok = false;
        string id = context.Request.Form["id"].removeAllSpaceOrTrimText(false);
        var hoadon = db.c_hoadonbanhang.FirstOrDefault(s => s.c_hoadonbanhang_id == id);

        if (hoadon == null)
        {
            msg = "Không tìm thấy hóa đơn bán hàng đã chọn";
            goto EndEventHandler;
        }

        var ncc = db.c_danhsachdathang.FirstOrDefault(s =>
            s.c_danhsachdathang_id == hoadon.c_danhsachdathang_id);

        if (ncc != null)
        {
            var nguoimua = db.md_doitackinhdoanh.FirstOrDefault(s =>
                s.md_doitackinhdoanh_id == ncc.md_doitackinhdoanh_id);

            sctdonhang = ncc.sochungtu;
            trangthaidonhang = ncc.trangthai;
            trangthaithanhtoan = ncc.trangthaithanhtoan;

            if (nguoimua != null)
            {
                hoten_nguoimua = nguoimua.ten_dtkd;
                sdt_nguoimua = nguoimua.tel;
                diachi_nguoimua = nguoimua.diachi;
            }
        }

    EndEventHandler:;
        dynamic rs = new Dictionary<string, object> { { "ok", ok }, { "msg", msg } };
        if (msg.Length <= 0)
        {
            var details = (from a in db.c_hoadonbanhang_cdmh
                           join b in db.md_sanpham on a.md_sanpham_id equals b.md_sanpham_id
                           join c in db.md_donvitinhsanpham on b.md_donvitinhsanpham_id equals c.md_donvitinhsanpham_id
                           where a.c_hoadonbanhang_id == id
                           orderby b.ma_sanpham
                           select new
                           {
                               a.stt,
                               md_sanpham_id = a.key_id,
                               parent_id = a.md_sanpham_pr_id,
                               ma_sanpham = (string.IsNullOrEmpty(a.md_sanpham_pr_id) ? b.ma_sanpham : ""),
                               mota_tiengviet = (string.IsNullOrEmpty(a.md_sanpham_pr_id) ? b.mota_tiengviet : ""),
                               md_donvitinhsanpham_id = (string.IsNullOrEmpty(a.md_sanpham_pr_id) ? c.ten_dvt : ""),
                               sl_dathang = a.sl_dadat,
                               gianhap = a.dongiamua,
                               a.thanhtien,
                               b.tonkho,
                               b.dathang
                           }).ToList();

            rs["ok"] = true;
            rs["id"] = id;
            rs["master"] = hoadon;
            rs["details"] = details;
        }
        else
        {
            rs["msg"] = msg;
        }

        context.Response.Write(JsonConvert.SerializeObject(rs));
    }

    public string modifyHoaDon(HttpContext context, string msg, string idnew, string sochungtu, c_hoadonbanhang hdbh = null)
    {
        if (msg.Length > 0)
            return msg;

        var actionEdit = hdbh != null;
        var details = JsonConvert.DeserializeObject<List<Details>>(
            context.Request.Form["details"].removeAllSpaceOrTrimText(true));
        var master = JsonConvert.DeserializeObject<Master>(
            context.Request.Form["master"].removeAllSpaceOrTrimText(true));

        try
        {
            details = details.Where(s => s.sl > 0).ToList();
            int stt = 0;
            foreach (var detail in details)
            {
                stt++;
                var id = string.IsNullOrWhiteSpace(detail.md_sanpham_pr_id)
                    ? detail.key_id
                    : detail.md_sanpham_pr_id;
                detail.md_sanpham_id = id;
                detail.stt = stt;
            }

            if (details.Count <= 0)
            {
                msg = "Không có dòng hàng";
                goto EndEventHandler;
            }

            var khachhang = db.md_doitackinhdoanh.FirstOrDefault(s =>
                s.md_doitackinhdoanh_id == master.nguoimuaid);
            if (khachhang == null)
            {
                msg = "Bạn chưa chọn khách hàng.";
                goto EndEventHandler;
            }

            var giaNull = details.FirstOrDefault(s => s.gia == null);
            if (giaNull != null)
            {
                msg = $@"Đơn giá của mã hàng ""{giaNull.mahang}"" không thể bỏ trống";
                goto EndEventHandler;
            }

            var groupedDetails = details
                .GroupBy(d => d.md_sanpham_id)
                .Select(g => new
                {
                    md_sanpham_id = g.Key,
                    mahang = g.First().mahang,
                    TongSoLuongDat = g.Sum(x => x.sl.GetValueOrDefault(0))
                })
                .ToList();

            var sanPhamIds = groupedDetails.Select(g => g.md_sanpham_id).ToList();

            var slCuDict = new Dictionary<string, decimal>();
            if (actionEdit)
            {
                slCuDict = db.c_hoadonbanhang_cdmh
                    .Where(cd =>
                        cd.c_hoadonbanhang_id == hdbh.c_hoadonbanhang_id
                        && sanPhamIds.Contains(cd.md_sanpham_id))
                    .GroupBy(cd => cd.md_sanpham_id)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Sum(x => x.sl_dadat.GetValueOrDefault(0))
                    );
            }

            var tonKhoDict = db.md_sanpham
                .Where(s => sanPhamIds.Contains(s.md_sanpham_id))
                .Select(s => new
                {
                    s.md_sanpham_id,
                    TonCoTheLay = (s.tonkho ?? 0) - (s.dathang ?? 0)
                })
                .ToDictionary(s => s.md_sanpham_id, s => s.TonCoTheLay);

            var spVuotTonKho = groupedDetails
                .Select(g =>
                {
                    decimal tonHienTai = tonKhoDict.TryGetValue(g.md_sanpham_id, out decimal val) ? val : 0;
                    decimal slCu = slCuDict.TryGetValue(g.md_sanpham_id, out decimal oldVal) ? oldVal : 0;
                    decimal tonKhaDungThucTe = actionEdit ? (tonHienTai + slCu) : tonHienTai;

                    return new
                    {
                        g.md_sanpham_id,
                        g.mahang,
                        g.TongSoLuongDat,
                        TonCoTheLay = tonKhaDungThucTe,
                        IsVuot = g.TongSoLuongDat > tonKhaDungThucTe
                    };
                })
                .Where(x => x.IsVuot)
                .ToList();

            if (spVuotTonKho.Any())
            {
                var dsMaHang = string.Join(", ",
                    spVuotTonKho
                        .Select(x => x.mahang + $" ({x.TonCoTheLay.DropTrailingZeros()})")
                        .Distinct()
                        .OrderBy(s => s));
                msg = $@"Các mã hàng sau vượt quá số lượng tồn kho cho phép: {dsMaHang}";
                goto EndEventHandler;
            }

            var defaultKho = db.md_kho.FirstOrDefault(k => k.hoatdong == true)
                ?? db.md_kho.FirstOrDefault();
            string khoId = defaultKho != null ? defaultKho.md_kho_id : "";
            string maSoChungTu = string.IsNullOrEmpty(sochungtu)
                ? (actionEdit ? hdbh.sochungtu : "")
                : sochungtu;

            if (actionEdit)
            {
                string loaiCu = hdbh.loai_kov;
                var oldDetailsList = db.c_hoadonbanhang_cdmh
                    .Where(s => s.c_hoadonbanhang_id == hdbh.c_hoadonbanhang_id)
                    .ToList();

                if (loaiCu == "01")
                {
                    foreach (var oldItem in oldDetailsList)
                    {
                        var sp = db.md_sanpham.FirstOrDefault(s =>
                            s.md_sanpham_id == oldItem.md_sanpham_id);
                        if (sp != null)
                            sp.dathang = Math.Max(0, (sp.dathang ?? 0) - (oldItem.sl_dadat ?? 0));
                    }
                }
                else if (loaiCu == "00")
                {
                    foreach (var oldItem in oldDetailsList)
                    {
                        var sp = db.md_sanpham.FirstOrDefault(s =>
                            s.md_sanpham_id == oldItem.md_sanpham_id);
                        if (sp != null)
                            sp.tonkho = (sp.tonkho ?? 0) + (oldItem.sl_dadat ?? 0);

                        if (!string.IsNullOrEmpty(khoId))
                        {
                            var khoSp = db.md_kho_sanpham.FirstOrDefault(k =>
                                k.md_kho_id == khoId
                                && k.md_sanpham_id == oldItem.md_sanpham_id);
                            if (khoSp != null)
                                khoSp.soluong = (khoSp.soluong ?? 0) + (oldItem.sl_dadat ?? 0);
                        }
                    }

                    var oldGiaoDich = db.md_kho_giaodich
                        .Where(g =>
                            g.donhang == hdbh.c_hoadonbanhang_id
                            || g.dongnhapxuat == maSoChungTu)
                        .ToList();
                    if (oldGiaoDich.Any())
                        db.md_kho_giaodich.RemoveRange(oldGiaoDich);
                }

                db.c_hoadonbanhang_cdmh.RemoveRange(oldDetailsList);
            }

            decimal tongtien = 0, tongsoluong = 0;
            string loaiMoi = master.loai;

            foreach (var dt in details)
            {
                var sp = db.md_sanpham.FirstOrDefault(s =>
                    s.md_sanpham_id == dt.md_sanpham_id);

                if (sp == null)
                    continue;

                decimal slMoi = dt.sl.GetValueOrDefault(0);

                var cdh = new c_hoadonbanhang_cdmh();
                cdh.c_hoadonbanhang_cdmh_id = Helper.getNewId();
                cdh.c_hoadonbanhang_id = idnew;
                cdh.stt = dt.stt;
                cdh.key_id = dt.key_id;
                cdh.md_sanpham_id = dt.md_sanpham_id;
                cdh.md_sanpham_pr_id = dt.md_sanpham_pr_id;
                cdh.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id;
                cdh.dongiamua = dt.gia.GetValueOrDefault(0);
                cdh.sl_dadat = slMoi;
                cdh.thanhtien = cdh.dongiamua * cdh.sl_dadat;
                cdh = Helper.setDefaultValueWhenInsertOrUpdate(cdh, userTK, false);
                db.c_hoadonbanhang_cdmh.Add(cdh);

                tongtien += cdh.thanhtien.GetValueOrDefault(0);
                tongsoluong += cdh.sl_dadat.GetValueOrDefault(0);

                if (loaiMoi == "01")
                {
                    sp.dathang = (sp.dathang ?? 0) + slMoi;
                }
                else if (loaiMoi == "00")
                {
                    sp.tonkho = (sp.tonkho ?? 0) - slMoi;

                    if (!string.IsNullOrEmpty(khoId))
                    {
                        var khoSp = db.md_kho_sanpham.FirstOrDefault(k =>
                            k.md_kho_id == khoId
                            && k.md_sanpham_id == dt.md_sanpham_id);

                        if (khoSp == null)
                        {
                            khoSp = new md_kho_sanpham
                            {
                                md_kho_sanpham_id = Helper.getNewId(),
                                md_kho_id = khoId,
                                md_sanpham_id = dt.md_sanpham_id,
                                soluong = 0 - slMoi,
                                hoatdong = true
                            };
                            khoSp = Helper.setDefaultValueWhenInsertOrUpdate(khoSp, userTK, false);
                            db.md_kho_sanpham.Add(khoSp);
                        }
                        else
                        {
                            khoSp.soluong = (khoSp.soluong ?? 0) - slMoi;
                            khoSp = Helper.setDefaultValueWhenInsertOrUpdate(khoSp, userTK, true);
                        }

                        helperEntity.obj = new HelperEntity.objLSNX();
                        helperEntity.obj.spId = dt.md_sanpham_id;
                        helperEntity.obj.dvtSpId = sp.md_donvitinhsanpham_id;
                        helperEntity.obj.slDichChuyen = 0 - slMoi;
                        helperEntity.obj.dongNhapXuat = maSoChungTu;
                        helperEntity.obj.sctDonHang = maSoChungTu;
                        helperEntity.obj.giaTriVND = cdh.thanhtien.GetValueOrDefault(0);
                        helperEntity.obj.khoId = khoId;
                        helperEntity.obj.kieuchuyen = helperEntity.kieuXuatKho;
                        helperEntity.obj.ngayChuyen = master.ngay.ToNullableDateTime();
                        helperEntity.obj.theoKg = false;
                        helperEntity.obj.laphieuKK = false;
                        helperEntity.obj.mota = "Xuất kho bán nhanh";
                        helperEntity.themHoacSuaLichSuNhapXuatKho(db, userTK);
                    }
                }
            }

            c_hoadonbanhang object_;
            if (actionEdit)
            {
                object_ = hdbh;
            }
            else
            {
                object_ = new c_hoadonbanhang();
                object_.c_hoadonbanhang_id = idnew;
                object_.sochungtu = sochungtu;
                object_.trangthai = master.loai == "00" ? Helper.KETTHUC : Helper.HIEULUC;
            }

            object_.loai_kov = master.loai;
            object_.nhanvien_kov = master.nhanvien;
            object_.ngay_kov = master.ngay_kov.ToNullableDateTime();
            object_.nguoimuaid_kov = master.nguoimuaid;
            object_.nguoimua_kov = master.nguoimua;
            object_.nguoinhan_kov = master.nguoinhan;
            object_.sdt_nguoinhan_kov = master.sdt_nguoinhan;
            object_.diachi_nguoinhan_kov = master.diachi_nguoinhan;
            object_.hinhthucthanhtoan_kov = master.hinhthucthanhtoan.removeAllSpaceOrTrimText(true);
            object_.giamgia_kov = master.giamgia.GetValueOrDefault(0);
            object_.thuvanchuyen_kov = master.thuvanchuyen.GetValueOrDefault(0);
            object_.thuthue_kov = master.thuthue.GetValueOrDefault(0);
            object_.thukhac_kov = master.thukhac.GetValueOrDefault(0);
            object_.loaithu_kov = master.loaithu.removeAllSpaceOrTrimText(true);
            object_.tongtienhang_kov = tongtien;
            object_.tongsoluong_kov = tongsoluong;
            object_.khachcantra_kov = tongtien - object_.giamgia_kov + object_.thukhac_kov;
            object_.thuho_cod_kov = master.thuho_cod;
            object_.cod_kov = master.cod;
            object_.thongtinnhanhang = master.thongtinnhanhang;
            object_.thongtinsanpham = master.thongtinsanpham;
            object_.thongtinthanhtoan = master.thongtinthanhtoan;
            object_.thongtinxuathoadon = master.thongtinxuathoadon;
            object_.trangthaicam = master.trangthaicam;
            object_.trangthaigiaohang = master.trangthaigiaohang;
            object_.trangthaithanhtoan = master.trangthaithanhtoan;
            object_.trangthaihoadon = master.trangthaihoadon;
            object_.ngaygiao = master.ngaygiao.ToNullableDateTime();

            decimal tienKhachDuaLucLap = master.khachthanhtoan.GetValueOrDefault(0);
            decimal tienThuaLucLap = Math.Max(
                0,
                tienKhachDuaLucLap - object_.khachcantra_kov.GetValueOrDefault(0)
            );
            decimal soTienThuLucLap = Math.Max(0, tienKhachDuaLucLap - tienThuaLucLap);

            if (!actionEdit)
            {
                // Lúc thêm, master.khachthanhtoan chỉ là input để tạo phiếu thu đầu tiên.
                // Giá trị lưu trên hóa đơn chuyển sang nghĩa "đã thanh toán" thực tế.
                object_.tienthua_kov = tienThuaLucLap;
                object_.khachthanhtoan_kov = soTienThuLucLap;
                object_.ghino_kov = Math.Max(
                    0,
                    object_.khachcantra_kov.GetValueOrDefault(0) - soTienThuLucLap
                );
            }
            // Lúc sửa: không ghi master.khachthanhtoan trở lại object_.khachthanhtoan_kov.
            // Cột này sẽ được SUM lại từ c_soquy_hoadon sau khi SaveChanges.

            object_.mota_kov = master.mota.removeAllSpaceOrTrimText(true);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, actionEdit);

            if (!actionEdit)
                db.c_hoadonbanhang.Add(object_);

            DateTime ngayGiaoDich = master.ngay.ToNullableDateTime()
                .GetValueOrDefault(DateTime.Now);

            string msgSoQuy = dongBoSoQuyHoaDon(
                object_,
                khachhang,
                ngayGiaoDich,
                soTienThuLucLap,
                actionEdit
            );

            if (!string.IsNullOrWhiteSpace(msgSoQuy))
            {
                msg = msgSoQuy;
                goto EndEventHandler;
            }

            // Save phiếu + allocation trước, sau đó lấy allocation làm source of truth
            // để cập nhật snapshot khachthanhtoan_kov/ghino_kov.
            db.SaveChanges();
            dongBoThanhToanHoaDon(object_.c_hoadonbanhang_id);
            db.SaveChanges();

            msg = "";
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;
        return msg;
    }

    public void CA_01_SuaHoaDon(HttpContext context)
    {
        string msg = "";
        string idSel = context.Request.Form["id"];
        bool ok = false;
        var hdbh = db.c_hoadonbanhang.FirstOrDefault(s => s.c_hoadonbanhang_id == idSel);

        if (hdbh == null)
            msg = "Hóa đơn bán không tồn tại.";

        msg = modifyHoaDon(context, msg, idSel, null, hdbh);

        if (msg.Length <= 0)
        {
            ok = true;
            msg = "Sửa hóa đơn thành công";
        }

        var rs = new { idnew = idSel, ok, msg };
        context.Response.Write(JsonConvert.SerializeObject(rs));
    }

    public void CA_01_THemMoiHoaDon(HttpContext context)
    {
        string msg = "", idnew = Helper.getNewId();
        bool ok = false;
        string sochungtu = VNN_VariablePublic.sochungtu(db, "HDB", 1, false);
        var hdbh = db.c_hoadonbanhang.FirstOrDefault(s => s.sochungtu == sochungtu);

        if (hdbh != null)
            msg = $@"Hóa đơn bán đã tồn tại số phiếu {sochungtu}.";

        msg = modifyHoaDon(context, msg, idnew, sochungtu);

        if (msg.Length <= 0)
        {
            ok = true;
            msg = "Tạo hóa đơn thành công";
        }

        var rs = new { idnew, ok, msg };
        context.Response.Write(JsonConvert.SerializeObject(rs));
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            string idRaw = context.Request.Form["id"];
            if (string.IsNullOrWhiteSpace(idRaw))
            {
                msg = "Không có hóa đơn cần hủy.";
                goto EndEventHandler;
            }

            var ids = idRaw
                .Split(',')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .Distinct()
                .ToList();

            var object_s = db.c_hoadonbanhang
                .Where(p => ids.Contains(p.c_hoadonbanhang_id))
                .ToList();

            if (object_s.Count <= 0)
            {
                msg = "Không tìm thấy hóa đơn cần hủy.";
                goto EndEventHandler;
            }

            var hoaDonDaHuy = object_s
                .Where(s => s.trangthai == Helper.HUYBO)
                .ToList();

            if (hoaDonDaHuy.Any())
            {
                string dsSoChungTu = string.Join(", ", hoaDonDaHuy.Select(s => s.sochungtu));
                msg = "Hóa đơn đã hủy trước đó: " + dsSoChungTu + ".";
                goto EndEventHandler;
            }

            var defaultKho = db.md_kho.FirstOrDefault(k => k.hoatdong == true)
                ?? db.md_kho.FirstOrDefault();
            string khoId = defaultKho != null ? defaultKho.md_kho_id : "";

            var hoaDonCanTinhLai = new List<string>();

            foreach (var object_ in object_s)
            {
                string hdbhId = object_.c_hoadonbanhang_id;
                string loaiHD = object_.loai_kov;
                string soChungTu = object_.sochungtu;

                var details = db.c_hoadonbanhang_cdmh
                    .Where(s => s.c_hoadonbanhang_id == hdbhId)
                    .ToList();

                if (loaiHD == "01")
                {
                    foreach (var dt in details)
                    {
                        var spServer = db.md_sanpham.FirstOrDefault(s =>
                            s.md_sanpham_id == dt.md_sanpham_id);
                        var sp = db.md_sanpham.Local.FirstOrDefault(s =>
                            s.md_sanpham_id == dt.md_sanpham_id) ?? spServer;

                        if (sp != null)
                        {
                            decimal sl = dt.sl_dadat.GetValueOrDefault(0);
                            sp.dathang = Math.Max(0, (sp.dathang ?? 0) - sl);
                        }
                    }
                }
                else if (loaiHD == "00")
                {
                    foreach (var dt in details)
                    {
                        decimal sl = dt.sl_dadat.GetValueOrDefault(0);

                        var spServer = db.md_sanpham.FirstOrDefault(s =>
                            s.md_sanpham_id == dt.md_sanpham_id);
                        var sp = db.md_sanpham.Local.FirstOrDefault(s =>
                            s.md_sanpham_id == dt.md_sanpham_id) ?? spServer;

                        if (sp != null)
                            sp.tonkho = (sp.tonkho ?? 0) + sl;

                        if (!string.IsNullOrWhiteSpace(khoId))
                        {
                            var khoSp = db.md_kho_sanpham.FirstOrDefault(k =>
                                k.md_kho_id == khoId
                                && k.md_sanpham_id == dt.md_sanpham_id);

                            if (khoSp != null)
                            {
                                khoSp.soluong = (khoSp.soluong ?? 0) + sl;
                                khoSp = Helper.setDefaultValueWhenInsertOrUpdate(khoSp, userTK, true);
                            }
                        }
                    }

                    var giaoDichKho = db.md_kho_giaodich
                        .Where(g =>
                            g.donhang == hdbhId
                            || g.donhang == soChungTu
                            || g.dongnhapxuat == soChungTu)
                        .ToList();

                    foreach (var gd in giaoDichKho)
                    {
                        gd.hoatdong = false;
                        Helper.setDefaultValueWhenInsertOrUpdate(gd, userTK, true);
                    }
                }

                // Hủy riêng allocation của hóa đơn đang hủy.
                // Không hủy các phiếu thu thủ công nếu chúng còn/được giữ như một khoản tiền chưa phân bổ.
                var phanBoHoaDon = db.c_soquy_hoadon
                    .Where(x =>
                        x.c_hoadonbanhang_id == hdbhId
                        && x.hoatdong != false)
                    .ToList();

                var soQuyIds = phanBoHoaDon
                    .Select(x => x.c_soquy_id)
                    .Distinct()
                    .ToList();

                foreach (var pb in phanBoHoaDon)
                {
                    pb.hoatdong = false;
                    Helper.setDefaultValueWhenInsertOrUpdate(pb, userTK, true);
                }

                // Phiếu tự sinh từ hóa đơn: chỉ hủy phiếu nếu sau khi bỏ allocation
                // của hóa đơn này nó không còn phân bổ active cho hóa đơn khác.
                var dsSoQuyNguon = db.c_soquy
                    .Where(s =>
                        s.c_hoadonbanhang_id == hdbhId
                        && s.nguon_nghiepvu == NGUON_SOQUY_HOADON
                        && s.trangthai != Helper.HUYBO)
                    .ToList();

                foreach (var sq in dsSoQuyNguon)
                {
                    var allAllocations = db.c_soquy_hoadon
                        .Where(x => x.c_soquy_id == sq.c_soquy_id)
                        .ToList();

                    bool conPhanBoKhac = allAllocations.Any(x =>
                        x.hoatdong != false
                        && x.c_hoadonbanhang_id != hdbhId);

                    if (!conPhanBoKhac)
                    {
                        sq.trangthai = Helper.HUYBO;
                        Helper.setDefaultValueWhenInsertOrUpdate(sq, userTK, true);
                    }
                }

                foreach (var dt in details)
                {
                    dt.hoatdong = false;
                    Helper.setDefaultValueWhenInsertOrUpdate(dt, userTK, true);
                }

                object_.trangthai = Helper.HUYBO;
                object_.hoatdong = false;
                Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);

                hoaDonCanTinhLai.Add(hdbhId);
            }

            db.SaveChanges();

            foreach (var hoaDonId in hoaDonCanTinhLai.Distinct())
                dongBoThanhToanHoaDon(hoaDonId);

            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;
        if (msg.Length <= 0)
        {
            msg = "true#Hủy hóa đơn thành công";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = "false#" + msg;
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(msg);
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
