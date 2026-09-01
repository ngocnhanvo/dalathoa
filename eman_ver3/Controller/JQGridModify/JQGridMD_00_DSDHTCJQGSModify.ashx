<%@ WebHandler Language="C#" Class="JQGridMD_00_DSDHTCJQGSModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using DataAcess;
using System.Collections.Generic;
using Newtonsoft.Json;
public class JQGridMD_00_DSDHTCJQGSModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public class Master
    {
        // Thông tin đơn hàng
        public string thongtinnhanhang { get; set; }
        public string ngaygiao { get; set; }
        public string thongtinsanpham { get; set; }
        public string thongtinthanhtoan { get; set; }
        public string thongtinxuathoadon { get; set; }
        public string trangthaithanhtoan { get; set; }
        public string trangthaigiaohang { get; set; }
        public string trangthaihoadon { get; set; }
        public string trangthaicam { get; set; }
    }
    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    User_TK userTK = null;
    public Dictionary<string, string> arrTrangThai = new Dictionary<string, string>() {
        { Helper.SOANTHAO, "Soạn Thảo" },
        { Helper.HIEULUC, "Hiệu Lực" },
        { Helper.HUYBO, "Hủy Bỏ" },
        { Helper.KETTHUC, "Kết thúc" }
    };

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
            case "CA01DCHT_MD00DSDHTCJQGS": // Hieu Luc
                this.CA01DCHT_MD00DSDHTCJQGS(context);
                break;
            case "CA_01_SuaDonHang":
                this.CA_01_SuaDonHang(context);
                break;
            case "CA_01_HuyBoDonHangTN":
                this.CA_01_HuyBoDonHangTN(context);
                break;
            case "CA_01_HoanThanhDonHang":
                this.CA_01_HoanThanhDonHang(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_HuyBoDonHangTN(HttpContext context)
    {
        string msg = "", msg_success = "";
        string id = context.Request.Form["id"];
        string ma_module = context.Request.QueryString["ma_module"];
        string check_1 = context.Request.Form["check"];
        string[] vnn = id.Split(',');
        try
        {
            var dsdh = db.c_danhsachdathang.Where(s => vnn.Contains(s.c_danhsachdathang_id)).FirstOrDefault();
            if (dsdh == null)
            {
                msg = $@"Không tìm thấy đơn hàng cần thao tác";
                goto EndEventHandler;
            }

            string trangThaiDangCo = dsdh.trangthai;
            if (trangThaiDangCo == Helper.DANHAN | trangThaiDangCo == Helper.SOANTHAO)
            {
                msg = "Đơn hàng chưa hiệu lực";
                goto EndEventHandler;
            }

            if (check_1 == "1")
            {
                dsdh.trangthai = Helper.HUYBO;
                var khdh = db.c_kehoachdathang.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).FirstOrDefault();
                if (khdh != null)
                {
                    khdh.trangthaiSav = khdh.trangthai;
                    khdh.trangthai = Helper.KETTHUC;
                }
            }
            else if (check_1 == "2")
            {
                dsdh.trangthai = Helper.KETTHUC;
                var khdh = db.c_kehoachdathang.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).FirstOrDefault();
                if (khdh != null)
                {
                    khdh.trangthaiSav = khdh.trangthai;
                    khdh.trangthai = Helper.KETTHUC;
                }
            }
            else if (check_1 == "3")
            {
                dsdh.trangthai = Helper.HIEULUC;
                var khdh = db.c_kehoachdathang.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).FirstOrDefault();
                if (khdh != null)
                {
                    if (!string.IsNullOrWhiteSpace(khdh.trangthaiSav))
                        khdh.trangthai = khdh.trangthaiSav;
                }
            }

            if (ma_module == "MD_00_DSDHJQGS")
                oper = "CA_01_HuyBoDonHangAnco";

            msg_success = $@"""{dsdh.so_po}"" đã đổi trạng thái từ ""{arrTrangThai[trangThaiDangCo]}"" sang ""{arrTrangThai[dsdh.trangthai]}""";
            VNN_Function.Write_log(context, ma_module, null, oper, msg_success, db);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"<div style='color:blue'>{msg_success}</div>";
        }
        else
        {
            msg = $@"<div style='color:red'>{msg}</div>";
        }

        context.Response.Write(msg);
    }

    public void CA_01_SuaDonHang(HttpContext context)
    {
        string msg = "";
        bool ok = false;
        string idSel = context.Request.Form["id"].removeAllSpaceOrTrimText(true);

        try
        {
            var master = JsonConvert.DeserializeObject<Master>(context.Request.Form["master"].removeAllSpaceOrTrimText(true));
            var object_ = db.c_hoadonbanhang.Where(s => s.c_hoadonbanhang_id == idSel).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Hóa đơn bán không tồn tại.";
                goto EndEventHandler;
            }

            object_.thongtinnhanhang = master.thongtinnhanhang;
            object_.thongtinsanpham = master.thongtinsanpham;
            object_.thongtinthanhtoan = master.thongtinthanhtoan;
            object_.thongtinxuathoadon = master.thongtinxuathoadon;
            object_.trangthaicam = master.trangthaicam;
            object_.trangthaigiaohang = master.trangthaigiaohang;
            object_.trangthaithanhtoan = master.trangthaithanhtoan;
            object_.trangthaihoadon = master.trangthaihoadon;
            object_.ngaygiao = master.ngaygiao.ToNullableDateTime();
            Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            db.SaveChanges();
            ok = true;
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;

        var rs = new
        {
            idnew = idSel,
            ok,
            msg
        };

        context.Response.Write(JsonConvert.SerializeObject(rs));
    }

    public void CA01DCHT_MD00DSDHTCJQGS(HttpContext context)
    {
        string msg = "", msg_success = "";
        string id = context.Request.Form["id"];
        string[] vnn = id.Split(',');
        var dsdhIds = new List<string>();
        var msgBLHLs = new List<Public.BaoLoiKhiHieuLuc>();
        var c_dongdsdhs = new List<c_dongdsdh>();
        var pub = new Public();
        try
        {
            var dsdh = db.c_danhsachdathang.Where(s => vnn.Contains(s.c_danhsachdathang_id)).FirstOrDefault();
            if (dsdh == null)
            {
                msg = $@"Đơn hàng không tồn tại";
            }
            else if (dsdh.trangthai == Helper.HIEULUC)
            {
                msg = $"Lỗi:Đơn hàng <b>{dsdh.sochungtu}</b> đã hiệu lực.";
            }
            else if (dsdh.trangthai == Helper.KETTHUC)
            {
                msg = $@"Lỗi:Đơn hàng <b>{dsdh.sochungtu}</b> đã kết thúc.";
            }

            if (msg.Length > 0)
                goto EndEventHandler;

            foreach (var dh in db.c_dongdsdh.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).ToList())
            {
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();

                var blhl = new Public.BaoLoiKhiHieuLuc();
                blhl.msp = sp == null ? dh.md_sanpham_id : sp.ma_sanpham;
                blhl.loi = "";
                if (sp == null)
                {
                    blhl.loi = "Không tồn tại";
                }
                else if (sp.trangthai == "NHD")
                {
                    blhl.loi = "đã ngưng hoạt động";
                }
                else
                {
                    c_dongdsdhs.Add(dh);
                }
                msgBLHLs.Add(blhl);
            }

            if (msgBLHLs.Where(s => !string.IsNullOrWhiteSpace(s.loi)).Count() > 0)
            {
                msg = $@"Lỗi: Đơn hàng ""{dsdh.sochungtu}"" thiếu số liệu";
                goto EndEventHandler;
            }
            else if (c_dongdsdhs.Count <= 0)
            {
                msg = $@"Lỗi: Đơn hàng ""{dsdh.sochungtu}"" không có dòng hàng.";
                goto EndEventHandler;
            }

            dsdh.trangthai = Helper.HIEULUC;
            dsdh.dg_nangluc = true;
            dsdh.ngayhieuluc = DateTime.Now;
            db.SaveChanges();
            msg_success += $@"<div class='nhan-thanhcong'>Triển khai đơn hàng ""{dsdh.sochungtu}"" thành công.</div>";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length > 0)
        {
            msg = $"<div error style='color:red'>{msg}</div>";
        }

        if (msg.Length <= 0)
        {
            msg = msg_success;
        }

        var result = new Dictionary<string, object>();
        result["msg"] = msg;
        result["json"] = msgBLHLs.Where(s => !string.IsNullOrWhiteSpace(s.loi)).OrderBy(s => s.msp).ToList();
        context.Response.Write(JsonConvert.SerializeObject(result));
    }

    public void CA_01_HoanThanhDonHang(HttpContext context)
    {
        string msg = "", msg_success = "";
        string id = context.Request.Form["id"];
        string[] vnn = id.Split(',');
        var dsdhIds = new List<string>();
        var msgBLHLs = new List<Public.BaoLoiKhiHieuLuc>();
        var c_dongdsdhs = new List<c_dongdsdh>();
        var pub = new Public();
        try
        {
            var dsdh = db.c_danhsachdathang.Where(s => vnn.Contains(s.c_danhsachdathang_id)).FirstOrDefault();
            if (dsdh == null)
            {
                msg = $@"Đơn hàng không tồn tại";
            }
            else if (dsdh.trangthai != Helper.HIEULUC)
            {
                msg = $"Lỗi:Đơn hàng <b>{dsdh.sochungtu}</b> không ở trạng thái hiệu lực.";
            }

            if (msg.Length > 0)
                goto EndEventHandler;

            dsdh.trangthai = Helper.KETTHUC;
            db.SaveChanges();
            msg_success += $@"<div class='nhan-thanhcong'>Đơn hàng ""{dsdh.sochungtu}"" hoàn thành.</div>";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length > 0)
        {
            msg = $"<div error style='color:red'>{msg}</div>";
        }

        if (msg.Length <= 0)
        {
            msg = msg_success;
        }

        var result = new Dictionary<string, object>();
        result["msg"] = msg;
        result["json"] = msgBLHLs.Where(s => !string.IsNullOrWhiteSpace(s.loi)).OrderBy(s => s.msp).ToList();
        context.Response.Write(JsonConvert.SerializeObject(result));
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string ma_khachhang = context.Request.Form["khachhang"];
        string id = context.Request.QueryString["id"];

        try
        {
            var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == ma_khachhang).FirstOrDefault();
            if (dtkd == null)
            {
                msg = $@"Không tìm thấy khách hàng có mã ""{ma_khachhang}""";
                goto EndEventHandler;
            }
            string sochungtu = VNN_VariablePublic.sochungtu(db, "DHB", 1, false);
            var donhang = db.c_danhsachdathang.Where(s => s.sochungtu == sochungtu).FirstOrDefault();
            if (donhang != null)
            {
                msg = $@"Lỗi:Số chứng từ ""{sochungtu}"" đã tồn tại.";
                goto EndEventHandler;
            }

            var object_ = new c_danhsachdathang();
            object_.c_danhsachdathang_id = id_new;
            object_.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
            object_.sochungtu = sochungtu;
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            db.c_danhsachdathang.Add(object_);
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

    public void edit(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string ma_khachhang = context.Request.Form["khachhang"];
        string id = context.Request.Form["id"];

        try
        {
            var object_ = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == id).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Không tìm đơn hàng đã chọn";
                goto EndEventHandler;
            }
            var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == ma_khachhang).FirstOrDefault();
            if (dtkd == null)
            {
                msg = $@"Không tìm thấy khách hàng có mã ""{ma_khachhang}""";
                goto EndEventHandler;
            }
            object_.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
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

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            foreach (var id_del_ in ids)
            {
                var object_ = db.c_danhsachdathang.Where(p => p.c_danhsachdathang_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                }
                else if (new string[] { "HIEULUC", "HUYBO", "KETTHUC" }.Contains(object_.trangthai))
                {
                    msg += string.Format(@"<br><b>{0} ({1})</b>: Không thể xóa khi đang trong trạng thái ""Hiệu Lực"", ""Hủy"" hoặc ""Kết Thúc"".", object_.sochungtu, object_.so_po);
                }
                else if (!new string[] { "HIEULUC", "SOANTHAO" }.Contains(object_.md_trangthai_id))
                {
                    msg += string.Format(@"<br><b>{0} ({1})</b>: Không thể xóa khi đang triển khai.", object_.sochungtu, object_.so_po);
                }
                else if (db.md_hanngach.Where(s => s.c_danhsachdathang_id == object_.c_danhsachdathang_id).Count() > 0)
                {
                    msg += string.Format(@"<br><b>{0} ({1})</b>: Đã tạo phiếu giảm hạn ngạch.", object_.sochungtu, object_.so_po);
                }
                else
                {
                    var taptins = db.md_taptin.Where(s => s.lienket == object_.c_danhsachdathang_id).ToList();
                    foreach (var taptin in taptins)
                    {
                        var path = ExcuteSignalRStatic.mapPathSignalR($@"~/{taptin.path}");
                        Helper.removeFileWithPath(path);
                        db.md_taptin.Remove(taptin);
                    }

                    VNN_Function.Write_log(context, ma_module, null, oper, "MĐH:" + object_.sochungtu + ", TĐH:" + object_.so_po, db);
                    db.c_danhsachdathang.Remove(object_);
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
            msg = string.Format(@"true#Xóa các đơn hàng đã chọn thành công");
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