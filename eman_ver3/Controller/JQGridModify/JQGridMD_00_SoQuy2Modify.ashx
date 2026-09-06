<%@ WebHandler Language="C#" Class="JQGridMD_00_SoQuy2Modify" %>

using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using DataAcess;

public class JQGridMD_00_SoQuy2Modify :
    IHttpHandler,
    System.Web.SessionState.IRequiresSessionState
{
    private const string NGUON_HOADON = "HOADON_BANHANG";

    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    User_TK userTK = null;
    public string oper = "vnn";

    private class SoQuyMaster
    {
        public string loai_giaodich { get; set; }
        public string ngay_giaodich { get; set; }
        public string md_loaithuchi_id { get; set; }
        public string md_loaidtkd_id { get; set; }
        public string md_doitackinhdoanh_id { get; set; }
        public string nguoi_nop_nhan { get; set; }
        public string nguoi_thuchi { get; set; }
        public string phuongthucthanhtoan { get; set; }
        public string sotien { get; set; }
        public string diengiai { get; set; }
    }

    private class PhanBoInput
    {
        public string c_hoadonbanhang_id { get; set; }
        public decimal? sotien_phanbo { get; set; }
    }

    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            oper = context.Request.QueryString["oper"]
                ?? context.Request.Form["oper"]
                ?? "";

            userTK = VNN_Function.get_user(
                Security.id_taikhoan(context),
                Security.id_vaitro(context),
                Security.id_phongban(context),
                db
            );
        }

        switch (oper)
        {
            case "add":
                add(context);
                break;
            case "edit":
                edit(context);
                break;
            case "del":
                del(context);
                break;
            case "loadEdit":
                loadEdit(context);
                break;
            case "loadHoaDonPhanBo":
                loadHoaDonPhanBo(context);
                break;
            default:
                context.Response.Write("false#Thao tác không hợp lệ.");
                break;
        }
    }

    private SoQuyMaster getMaster(HttpContext context)
    {
        string json = context.Request.Form["master"];
        if (string.IsNullOrWhiteSpace(json)) return null;

        return new JavaScriptSerializer()
            .Deserialize<SoQuyMaster>(json);
    }

    private List<PhanBoInput> getPhanBo(HttpContext context)
    {
        string json = context.Request.Form["allocations"];
        if (string.IsNullOrWhiteSpace(json))
            return new List<PhanBoInput>();

        try
        {
            return new JavaScriptSerializer()
                .Deserialize<List<PhanBoInput>>(json)
                ?? new List<PhanBoInput>();
        }
        catch
        {
            return new List<PhanBoInput>();
        }
    }

    private decimal tongDaThuHoaDon(string hoaDonId, string loaiTruSoQuyId)
    {
        decimal tuDetail =
            (from pb in db.c_soquy_hoadon
             join sq in db.c_soquy
                on pb.c_soquy_id equals sq.c_soquy_id
             where pb.c_hoadonbanhang_id == hoaDonId
                && pb.hoatdong != false
                && sq.trangthai != Helper.HUYBO
                && (string.IsNullOrEmpty(loaiTruSoQuyId)
                    || sq.c_soquy_id != loaiTruSoQuyId)
             select (decimal?)pb.sotien_phanbo)
            .Sum()
            .GetValueOrDefault(0);

        // Tương thích dữ liệu cũ chưa có dòng c_soquy_hoadon.
        decimal tuLegacy =
            db.c_soquy
            .Where(s =>
                s.c_hoadonbanhang_id == hoaDonId
                && s.trangthai != Helper.HUYBO
                && (string.IsNullOrEmpty(loaiTruSoQuyId)
                    || s.c_soquy_id != loaiTruSoQuyId)
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

    // khachthanhtoan_kov từ đây mang nghĩa tổng số tiền thực tế đã thu của hóa đơn.
    // Không đụng trangthaithanhtoan vì trạng thái vẫn do người dùng quyết định độc lập.
    private void dongBoThanhToanHoaDon(IEnumerable<string> hoaDonIds)
    {
        var ids = (hoaDonIds ?? Enumerable.Empty<string>())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct()
            .ToList();

        foreach (var hoaDonId in ids)
        {
            var hd = db.c_hoadonbanhang.FirstOrDefault(h =>
                h.c_hoadonbanhang_id == hoaDonId);

            if (hd == null)
                continue;

            decimal daThanhToan = tongDaThuHoaDon(hoaDonId, null);
            decimal khachCanTra = hd.khachcantra_kov.GetValueOrDefault(0);

            hd.khachthanhtoan_kov = daThanhToan;
            hd.ghino_kov = Math.Max(0, khachCanTra - daThanhToan);

            Helper.setDefaultValueWhenInsertOrUpdate(hd, userTK, true);
        }
    }

    private decimal phanBoCuaPhieu(string soQuyId, string hoaDonId)
    {
        decimal detail = db.c_soquy_hoadon
            .Where(pb =>
                pb.c_soquy_id == soQuyId
                && pb.c_hoadonbanhang_id == hoaDonId
                && pb.hoatdong != false)
            .Select(pb => (decimal?)pb.sotien_phanbo)
            .Sum()
            .GetValueOrDefault(0);

        if (detail > 0) return detail;

        var sq = db.c_soquy.FirstOrDefault(s => s.c_soquy_id == soQuyId);
        if (sq != null
            && sq.trangthai != Helper.HUYBO
            && sq.c_hoadonbanhang_id == hoaDonId)
        {
            return sq.sotien;
        }

        return 0;
    }

    private string dongBoPhanBo(
        c_soquy soQuy,
        List<PhanBoInput> input,
        bool khoaTheoHoaDonNguon
    )
    {
        if (soQuy == null) return "Không tìm thấy phiếu để phân bổ.";

        input = (input ?? new List<PhanBoInput>())
            .Where(x =>
                x != null
                && !string.IsNullOrWhiteSpace(x.c_hoadonbanhang_id)
                && x.sotien_phanbo.GetValueOrDefault(0) > 0)
            .GroupBy(x => x.c_hoadonbanhang_id.Trim())
            .Select(g => new PhanBoInput
            {
                c_hoadonbanhang_id = g.Key,
                sotien_phanbo = g.Sum(x => x.sotien_phanbo.GetValueOrDefault(0))
            })
            .ToList();

        if (khoaTheoHoaDonNguon)
        {
            if (string.IsNullOrWhiteSpace(soQuy.c_hoadonbanhang_id))
                return "Phiếu thu hóa đơn không có hóa đơn nguồn.";

            input = new List<PhanBoInput>
            {
                new PhanBoInput
                {
                    c_hoadonbanhang_id = soQuy.c_hoadonbanhang_id,
                    sotien_phanbo = soQuy.sotien
                }
            };
        }

        if (input.Count > 0 && soQuy.loai_giaodich != "THU")
            return "Chỉ phiếu thu mới được phân bổ vào hóa đơn bán hàng.";

        decimal tongPhanBo = input.Sum(x => x.sotien_phanbo.GetValueOrDefault(0));
        if (tongPhanBo > soQuy.sotien)
            return "Tổng tiền phân bổ không được lớn hơn số tiền phiếu thu.";

        foreach (var item in input)
        {
            var hd = db.c_hoadonbanhang.FirstOrDefault(h =>
                h.c_hoadonbanhang_id == item.c_hoadonbanhang_id);

            if (hd == null)
                return "Không tìm thấy hóa đơn được phân bổ.";

            if (hd.trangthai == Helper.HUYBO || hd.hoatdong == false)
                return "Không thể phân bổ vào hóa đơn đã hủy.";

            if (!string.IsNullOrWhiteSpace(soQuy.md_doitackinhdoanh_id)
                && hd.nguoimuaid_kov != soQuy.md_doitackinhdoanh_id)
            {
                return "Các hóa đơn phân bổ phải thuộc cùng khách hàng của phiếu thu.";
            }

            decimal giaTriHoaDon = hd.khachcantra_kov.GetValueOrDefault(0);
            decimal daThuKhac = tongDaThuHoaDon(
                hd.c_hoadonbanhang_id,
                soQuy.c_soquy_id
            );

            decimal conLai = Math.Max(0, giaTriHoaDon - daThuKhac);
            decimal phanBo = item.sotien_phanbo.GetValueOrDefault(0);

            if (phanBo > conLai)
            {
                return "Số tiền phân bổ cho hóa đơn "
                    + hd.sochungtu
                    + " vượt số tiền còn phải thu.";
            }
        }

        var oldRows = db.c_soquy_hoadon
            .Where(x => x.c_soquy_id == soQuy.c_soquy_id)
            .ToList();

        var inputIds = input
            .Select(x => x.c_hoadonbanhang_id)
            .ToList();

        foreach (var old in oldRows)
        {
            if (!inputIds.Contains(old.c_hoadonbanhang_id))
            {
                old.hoatdong = false;
                Helper.setDefaultValueWhenInsertOrUpdate(old, userTK, true);
            }
        }

        foreach (var item in input)
        {
            var rows = oldRows
                .Where(x => x.c_hoadonbanhang_id == item.c_hoadonbanhang_id)
                .ToList();

            var row = rows.FirstOrDefault();
            if (row == null)
            {
                row = new c_soquy_hoadon();
                row.c_soquy_hoadon_id = Helper.getNewId();
                row.c_soquy_id = soQuy.c_soquy_id;
                row.c_hoadonbanhang_id = item.c_hoadonbanhang_id;
                row.sotien_phanbo = item.sotien_phanbo.GetValueOrDefault(0);
                row.hoatdong = true;
                row = Helper.setDefaultValueWhenInsertOrUpdate(row, userTK, false);
                db.c_soquy_hoadon.Add(row);
            }
            else
            {
                row.sotien_phanbo = item.sotien_phanbo.GetValueOrDefault(0);
                row.hoatdong = true;
                row = Helper.setDefaultValueWhenInsertOrUpdate(row, userTK, true);

                foreach (var duplicate in rows.Skip(1))
                {
                    duplicate.hoatdong = false;
                    Helper.setDefaultValueWhenInsertOrUpdate(duplicate, userTK, true);
                }
            }
        }

        return "";
    }

    private object taoDongPhanBo(c_soquy sq, c_hoadonbanhang hd)
    {
        decimal giaTri = hd.khachcantra_kov.GetValueOrDefault(0);
        decimal daThuTruoc = tongDaThuHoaDon(hd.c_hoadonbanhang_id, sq.c_soquy_id);
        decimal phanBo = phanBoCuaPhieu(sq.c_soquy_id, hd.c_hoadonbanhang_id);
        decimal conLai = Math.Max(0, giaTri - daThuTruoc - phanBo);

        return new
        {
            c_hoadonbanhang_id = hd.c_hoadonbanhang_id,
            ma_phieu = hd.sochungtu,
            ngay = hd.ngay_kov.HasValue
                ? hd.ngay_kov.Value.ToString(VNN_Config.get_FormatDate())
                : "",
            gia_tri_phieu = giaTri,
            da_thu_truoc = daThuTruoc,
            sotien_phanbo = phanBo,
            con_lai = conLai,
            trangthai = conLai <= 0 ? "Đã thanh toán" : "Còn nợ"
        };
    }

    public void loadEdit(HttpContext context)
    {
        try
        {
            string id = context.Request.Form["id"];
            if (string.IsNullOrWhiteSpace(id))
            {
                writeJson(context, new { error = "Không có ID phiếu cần sửa." });
                return;
            }

            var object_ = db.c_soquy.FirstOrDefault(p => p.c_soquy_id == id);
            if (object_ == null)
            {
                writeJson(context, new { error = "Không tìm thấy phiếu." });
                return;
            }

            var ltc = db.md_loaithuchi.FirstOrDefault(p =>
                p.md_loaithuchi_id == object_.md_loaithuchi_id);

            var dtkd = db.md_doitackinhdoanh.FirstOrDefault(p =>
                p.md_doitackinhdoanh_id == object_.md_doitackinhdoanh_id);

            var master = new
            {
                c_soquy_id = object_.c_soquy_id,
                ma_phieu = object_.ma_phieu,
                loai_giaodich = object_.loai_giaodich,
                ngay_giaodich = object_.ngay_giaodich.ToString(VNN_Config.get_FormatDate()),
                md_loaithuchi_id = ltc != null ? ltc.ma_loaithuchi : "",
                md_doitackinhdoanh_id = object_.md_doitackinhdoanh_id,
                md_loaidtkd_id = dtkd != null ? dtkd.md_loaidtkd_id : "",
                nguoi_thuchi = object_.nguoi_thuchi,
                nguoi_nop_nhan = object_.nguoi_nop_nhan,
                sotien = object_.sotien,
                phuongthucthanhtoan = object_.phuongthucthanhtoan,
                diengiai = object_.diengiai,
                nguon_nghiepvu = object_.nguon_nghiepvu,
                c_hoadonbanhang_id = object_.c_hoadonbanhang_id,
                value_nguoitao = object_.nguoitao
            };

            var hoaDonIds = db.c_soquy_hoadon
                .Where(x => x.c_soquy_id == id && x.hoatdong != false)
                .Select(x => x.c_hoadonbanhang_id)
                .Distinct()
                .ToList();

            if (hoaDonIds.Count == 0
                && !string.IsNullOrWhiteSpace(object_.c_hoadonbanhang_id))
            {
                hoaDonIds.Add(object_.c_hoadonbanhang_id);
            }

            var hoaDons = db.c_hoadonbanhang
                .Where(h => hoaDonIds.Contains(h.c_hoadonbanhang_id))
                .ToList();

            var allocations = hoaDons
                .Select(h => taoDongPhanBo(object_, h))
                .ToList();

            writeJson(context, new
            {
                master = master,
                allocations = allocations
            });
        }
        catch (Exception ex)
        {
            writeJson(context, new { error = ex.Message });
        }
    }

    public void loadHoaDonPhanBo(HttpContext context)
    {
        try
        {
            string doiTacId = (context.Request.Form["md_doitackinhdoanh_id"] ?? "").Trim();
            string soQuyId = (context.Request.Form["c_soquy_id"] ?? "").Trim();

            if (string.IsNullOrWhiteSpace(doiTacId))
            {
                writeJson(context, new { ok = true, rows = new object[0] });
                return;
            }

            var sq = !string.IsNullOrWhiteSpace(soQuyId)
                ? db.c_soquy.FirstOrDefault(s => s.c_soquy_id == soQuyId)
                : null;

            var hdList = db.c_hoadonbanhang
                .Where(h =>
                    h.nguoimuaid_kov == doiTacId
                    && h.trangthai != Helper.HUYBO
                    && h.hoatdong != false)
                .OrderBy(h => h.ngay_kov)
                .ToList();

            var rows = new List<object>();
            foreach (var hd in hdList)
            {
                decimal giaTri = hd.khachcantra_kov.GetValueOrDefault(0);
                decimal daThuTruoc = tongDaThuHoaDon(hd.c_hoadonbanhang_id, soQuyId);
                decimal phanBoHienTai = sq != null
                    ? phanBoCuaPhieu(soQuyId, hd.c_hoadonbanhang_id)
                    : 0;
                decimal conLaiTruocPhieu = Math.Max(0, giaTri - daThuTruoc);

                if (conLaiTruocPhieu <= 0 && phanBoHienTai <= 0)
                    continue;

                rows.Add(new
                {
                    c_hoadonbanhang_id = hd.c_hoadonbanhang_id,
                    ma_phieu = hd.sochungtu,
                    ngay = hd.ngay_kov.HasValue
                        ? hd.ngay_kov.Value.ToString(VNN_Config.get_FormatDate())
                        : "",
                    gia_tri_phieu = giaTri,
                    da_thu_truoc = daThuTruoc,
                    sotien_phanbo = phanBoHienTai,
                    toi_da_phan_bo = conLaiTruocPhieu,
                    trangthai = Math.Max(0, giaTri - daThuTruoc - phanBoHienTai) <= 0
                        ? "Đã thanh toán"
                        : "Còn nợ"
                });
            }

            writeJson(context, new { ok = true, rows = rows });
        }
        catch (Exception ex)
        {
            writeJson(context, new { ok = false, error = ex.Message });
        }
    }

    private void writeJson(HttpContext context, object data)
    {
        context.Response.ContentType = "application/json";
        context.Response.Write(new JavaScriptSerializer().Serialize(data));
    }

    public void add(HttpContext context)
    {
        string msg = "";
        string id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            SoQuyMaster master = getMaster(context);
            if (master == null)
            {
                msg = "Không nhận được dữ liệu phiếu.";
                goto EndEventHandler;
            }

            string loaiGiaoDich = (master.loai_giaodich ?? "").Trim().ToUpper();
            if (loaiGiaoDich != "THU" && loaiGiaoDich != "CHI")
            {
                msg = "Loại giao dịch không hợp lệ.";
                goto EndEventHandler;
            }

            var ngayGiaoDich = VNN_Config.setDateTime(master.ngay_giaodich);
            if (!ngayGiaoDich.IsDate())
            {
                msg = "Thời gian giao dịch không hợp lệ.";
                goto EndEventHandler;
            }

            var soTien = master.sotien.ToNullableDecimal();
            if (soTien.GetValueOrDefault(0) <= 0)
            {
                msg = "Số tiền phải lớn hơn 0.";
                goto EndEventHandler;
            }

            string maLoaiThuChi = (master.md_loaithuchi_id ?? "").Trim();
            if (string.IsNullOrWhiteSpace(maLoaiThuChi))
            {
                msg = loaiGiaoDich == "THU"
                    ? "Vui lòng chọn loại thu."
                    : "Vui lòng chọn loại chi.";
                goto EndEventHandler;
            }

            var loaiThuChi = db.md_loaithuchi.FirstOrDefault(p =>
                p.ma_loaithuchi == maLoaiThuChi);
            if (loaiThuChi == null)
            {
                msg = "Không tìm thấy loại thu/chi.";
                goto EndEventHandler;
            }

            if (!string.IsNullOrWhiteSpace(loaiThuChi.loai_giaodich)
                && !string.Equals(
                    loaiThuChi.loai_giaodich.Trim(),
                    loaiGiaoDich,
                    StringComparison.OrdinalIgnoreCase))
            {
                msg = loaiGiaoDich == "THU"
                    ? "Loại thu đã chọn không hợp lệ."
                    : "Loại chi đã chọn không hợp lệ.";
                goto EndEventHandler;
            }

            string doiTacId = (master.md_doitackinhdoanh_id ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(doiTacId))
            {
                var doiTac = db.md_doitackinhdoanh.FirstOrDefault(p =>
                    p.md_doitackinhdoanh_id == doiTacId);

                if (doiTac == null)
                {
                    msg = "Đối tượng nộp/nhận không tồn tại.";
                    goto EndEventHandler;
                }

                string loaiDoiTacId = (master.md_loaidtkd_id ?? "").Trim();
                if (!string.IsNullOrWhiteSpace(loaiDoiTacId)
                    && doiTac.md_loaidtkd_id != loaiDoiTacId)
                {
                    msg = "Đối tượng nộp/nhận không thuộc loại đối tượng đã chọn.";
                    goto EndEventHandler;
                }
            }

            string nguoiThuChi = (master.nguoi_thuchi ?? "").Trim();
            if (string.IsNullOrWhiteSpace(nguoiThuChi))
            {
                msg = loaiGiaoDich == "THU"
                    ? "Vui lòng chọn người thu."
                    : "Vui lòng chọn người chi.";
                goto EndEventHandler;
            }

            string maChungTu = loaiGiaoDich == "THU" ? "PT" : "PC";
            string soChungTu = VNN_VariablePublic.sochungtu(db, maChungTu, 1, false);
            if (string.IsNullOrWhiteSpace(soChungTu))
            {
                msg = "Không thể sinh số phiếu.";
                goto EndEventHandler;
            }

            if (db.c_soquy.Any(p => p.ma_phieu == soChungTu))
            {
                msg = "Sổ quỹ đã tồn tại số phiếu " + soChungTu + ".";
                goto EndEventHandler;
            }

            var object_ = new c_soquy();
            object_.c_soquy_id = id_new;
            object_.ma_phieu = soChungTu;
            object_.loai_giaodich = loaiGiaoDich;
            object_.ngay_giaodich = ngayGiaoDich;
            object_.trangthai = Helper.HIEULUC;
            object_.md_loaithuchi_id = loaiThuChi.md_loaithuchi_id;
            object_.nguoi_thuchi = nguoiThuChi;
            object_.md_doitackinhdoanh_id = string.IsNullOrWhiteSpace(doiTacId) ? null : doiTacId;
            object_.nguoi_nop_nhan = (master.nguoi_nop_nhan ?? "").Trim();
            object_.sotien = soTien.GetValueOrDefault(0);
            object_.phuongthucthanhtoan = master.phuongthucthanhtoan;
            object_.diengiai = (master.diengiai ?? "").Trim();
            object_.nguon_nghiepvu = "THU_CHI_THUCONG";
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            db.c_soquy.Add(object_);

            var phanBo = getPhanBo(context);
            msg = dongBoPhanBo(object_, phanBo, false);
            if (!string.IsNullOrWhiteSpace(msg))
                goto EndEventHandler;

            // Save allocation trước để phép SUM bên dưới luôn đọc dữ liệu chuẩn từ DB.
            db.SaveChanges();

            dongBoThanhToanHoaDon(
                phanBo.Select(x => x.c_hoadonbanhang_id)
            );
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;
        if (msg.Length <= 0)
        {
            msg = "true#Thêm mới thành công#" + id_new;
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = "false#" + msg;
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];

        try
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                msg = "Không có ID phiếu cần sửa.";
                goto EndEventHandler;
            }

            var object_ = db.c_soquy.FirstOrDefault(p => p.c_soquy_id == id);
            if (object_ == null)
            {
                msg = "Không tìm thấy phiếu cần sửa.";
                goto EndEventHandler;
            }

            if (object_.trangthai == Helper.HUYBO)
            {
                msg = "Phiếu đã hủy, không thể sửa.";
                goto EndEventHandler;
            }

            SoQuyMaster master = getMaster(context);
            if (master == null)
            {
                msg = "Không nhận được dữ liệu phiếu.";
                goto EndEventHandler;
            }

            var hoaDonBiAnhHuong = db.c_soquy_hoadon
                .Where(x => x.c_soquy_id == id && x.hoatdong != false)
                .Select(x => x.c_hoadonbanhang_id)
                .Distinct()
                .ToList();

            if (!string.IsNullOrWhiteSpace(object_.c_hoadonbanhang_id))
                hoaDonBiAnhHuong.Add(object_.c_hoadonbanhang_id);

            string maPhieuCu = object_.ma_phieu;
            string loaiGiaoDichCu = (object_.loai_giaodich ?? "").Trim().ToUpper();
            string nguonNghiepVuCu = object_.nguon_nghiepvu;
            string hoaDonNguonCu = object_.c_hoadonbanhang_id;

            string loaiClient = (master.loai_giaodich ?? "").Trim().ToUpper();
            if (!string.IsNullOrWhiteSpace(loaiClient) && loaiClient != loaiGiaoDichCu)
            {
                msg = "Không thể đổi loại phiếu Thu sang Chi hoặc ngược lại.";
                goto EndEventHandler;
            }

            var ngayGiaoDich = VNN_Config.setDateTime(master.ngay_giaodich);
            if (!ngayGiaoDich.IsDate())
            {
                msg = "Thời gian giao dịch không hợp lệ.";
                goto EndEventHandler;
            }

            var soTien = master.sotien.ToNullableDecimal();
            if (soTien.GetValueOrDefault(0) <= 0)
            {
                msg = "Số tiền phải lớn hơn 0.";
                goto EndEventHandler;
            }

            string maLoaiThuChi = (master.md_loaithuchi_id ?? "").Trim();
            var loaiThuChi = db.md_loaithuchi.FirstOrDefault(p =>
                p.ma_loaithuchi == maLoaiThuChi);
            if (loaiThuChi == null)
            {
                msg = "Không tìm thấy loại thu/chi.";
                goto EndEventHandler;
            }

            if (!string.IsNullOrWhiteSpace(loaiThuChi.loai_giaodich)
                && !string.Equals(
                    loaiThuChi.loai_giaodich.Trim(),
                    loaiGiaoDichCu,
                    StringComparison.OrdinalIgnoreCase))
            {
                msg = "Loại thu/chi đã chọn không hợp lệ.";
                goto EndEventHandler;
            }

            string doiTacId = (master.md_doitackinhdoanh_id ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(doiTacId)
                && !db.md_doitackinhdoanh.Any(p => p.md_doitackinhdoanh_id == doiTacId))
            {
                msg = "Đối tượng nộp/nhận không tồn tại.";
                goto EndEventHandler;
            }

            object_.phuongthucthanhtoan = master.phuongthucthanhtoan;
            object_.ngay_giaodich = ngayGiaoDich;
            object_.md_loaithuchi_id = loaiThuChi.md_loaithuchi_id;
            object_.nguoi_thuchi = master.nguoi_thuchi;
            object_.md_doitackinhdoanh_id = string.IsNullOrWhiteSpace(doiTacId) ? null : doiTacId;
            object_.nguoi_nop_nhan = (master.nguoi_nop_nhan ?? "").Trim();
            object_.sotien = soTien.GetValueOrDefault(0);
            object_.diengiai = (master.diengiai ?? "").Trim();

            object_.ma_phieu = maPhieuCu;
            object_.loai_giaodich = loaiGiaoDichCu;
            object_.nguon_nghiepvu = nguonNghiepVuCu;
            object_.c_hoadonbanhang_id = hoaDonNguonCu;

            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);

            bool khoaTheoHoaDonNguon =
                object_.nguon_nghiepvu == NGUON_HOADON
                && !string.IsNullOrWhiteSpace(object_.c_hoadonbanhang_id);

            var phanBo = getPhanBo(context);
            msg = dongBoPhanBo(object_, phanBo, khoaTheoHoaDonNguon);
            if (!string.IsNullOrWhiteSpace(msg))
                goto EndEventHandler;

            hoaDonBiAnhHuong.AddRange(
                phanBo.Select(x => x.c_hoadonbanhang_id)
            );

            if (khoaTheoHoaDonNguon)
                hoaDonBiAnhHuong.Add(object_.c_hoadonbanhang_id);

            db.SaveChanges();

            dongBoThanhToanHoaDon(hoaDonBiAnhHuong);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;
        if (msg.Length <= 0)
        {
            msg = "true#Cập nhật thành công#" + id;
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = "false#" + msg;
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            string id = context.Request.Form["id"];
            if (string.IsNullOrWhiteSpace(id))
            {
                msg = "Không có phiếu cần hủy.";
                goto EndEventHandler;
            }

            var ids = id.Split(',')
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Trim())
                .Distinct()
                .ToList();

            var objects = db.c_soquy
                .Where(p => ids.Contains(p.c_soquy_id))
                .ToList();

            if (objects.Count <= 0)
            {
                msg = "Không tìm thấy phiếu cần hủy.";
                goto EndEventHandler;
            }

            var hoaDonBiAnhHuong = new List<string>();

            foreach (var object_ in objects)
            {
                if (object_.trangthai == Helper.HUYBO)
                    continue;

                var allocations = db.c_soquy_hoadon
                    .Where(x =>
                        x.c_soquy_id == object_.c_soquy_id
                        && x.hoatdong != false)
                    .ToList();

                hoaDonBiAnhHuong.AddRange(
                    allocations.Select(x => x.c_hoadonbanhang_id)
                );

                if (!string.IsNullOrWhiteSpace(object_.c_hoadonbanhang_id))
                    hoaDonBiAnhHuong.Add(object_.c_hoadonbanhang_id);

                object_.trangthai = Helper.HUYBO;
                Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);

                foreach (var allocation in allocations)
                {
                    allocation.hoatdong = false;
                    Helper.setDefaultValueWhenInsertOrUpdate(allocation, userTK, true);
                }
            }

            db.SaveChanges();

            dongBoThanhToanHoaDon(hoaDonBiAnhHuong);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;
        if (msg.Length <= 0)
        {
            msg = "true#Hủy phiếu thành công";
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
