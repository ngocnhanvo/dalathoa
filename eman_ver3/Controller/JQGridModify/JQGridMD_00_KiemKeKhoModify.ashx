<%@ WebHandler Language="C#" Class="JQGridMD_00_KiemKeKhoModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using DataAcess;
using System.Collections.Generic;

public class JQGridMD_00_KiemKeKhoModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    public HelperEntity helperEntity = new HelperEntity();
    public User_TK userTK = null;
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
            case "CA_01_KiemKe":
                this.CA_01_KiemKe(context);
                break;
            default:
                break;
        }
    }

    private string tudongThemDongHang(EntityContext db, md_kiemke kiemke, User_TK us)
    {
        var hangtho = kiemke.sapxep == "" | kiemke.sapxep == "0";
        var thanhpham = kiemke.sapxep == "" | kiemke.sapxep == "1";
        if (kiemke.tudong.GetValueOrDefault(false))
        {
            var cdhs = from a in db.md_kho_sanpham
                       join b in db.md_sanpham on a.md_sanpham_id equals b.md_sanpham_id
                       where a.md_kho_id == kiemke.md_kho_id & (a.soluong ?? 0) > 0
                       select new { a, b };

            var cdhsLst = new List<md_kho_sanpham>();
            if (hangtho == thanhpham)
            {
                cdhsLst = cdhs.Select(s => s.a).ToList();
            }
            else if (hangtho)
            {
                cdhsLst = cdhs.Where(s => s.b.ban_thanhpham == true).Select(s => s.a).ToList();
            }
            else if (thanhpham)
            {
                cdhsLst = cdhs.Where(s => s.b.sanpham == true).Select(s => s.a).ToList();
            }

            foreach (var cdh in cdhsLst)
            {
                var item = db.md_kiemke_cdh.Where(s => s.md_kiemke_id == kiemke.md_kiemke_id & s.md_sanpham_id == cdh.md_sanpham_id).FirstOrDefault();
                if (item == null)
                {
                    item = new md_kiemke_cdh();
                    item.md_kiemke_cdh_id = Helper.getNewId();
                    item.md_kiemke_id = kiemke.md_kiemke_id;
                    item.md_sanpham_id = cdh.md_sanpham_id;
                    item.md_kho_id = cdh.md_kho_id;
                    item.sl_demduoc = cdh.soluong.GetValueOrDefault(0);
                    item = Helper.setDefaultValueWhenInsertOrUpdate(item, us, false);
                    db.md_kiemke_cdh.Add(item);
                }
            }
        }

        return "";
    }

    public void CA_01_KiemKe(HttpContext context)
    {
        string msg = "";
        string id = context.Request.Form["id"];
        string ngay_dichchuyen = context.Request.Form["ngay_dichchuyen"];
        var msgErrs = new List<Public.BaoLoiKhiHieuLuc>();
        try
        {
            var nkk = VNN_Config.setDateTime(ngay_dichchuyen);
            var kk = db.md_kiemke.Where(s => s.md_kiemke_id == id).FirstOrDefault();
            if (kk == null)
            {
                msg = $@"Không tìm thấy phiếu kiểm kê đã chọn";
                goto EndEventHandler;
            }
            if (kk.ma_kiemke != Helper.SOANTHAO)
            {
                msg = $@"Phiếu phải ở trạng thái ""Soạn Thảo""";
                goto EndEventHandler;
            }
            if (!nkk.IsDate())
            {
                msg = "Ngày kiểm kê có giá trị sai";
                goto EndEventHandler;
            }
            var cdhs = db.md_kiemke_cdh.Where(s => s.md_kiemke_id == kk.md_kiemke_id & s.sl_sosach != null).ToList();
            if (cdhs.Count <= 0)
            {
                msg = $@"Không có dòng hàng cần kiểm kê";
                goto EndEventHandler;
            }

            //var msgErrs = new lis
            foreach (var cdh in cdhs)
            {
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == cdh.md_sanpham_id).FirstOrDefault();

                var kho_hh = db.md_kho_sanpham.Where(s =>
                    s.md_kho_id == kk.md_kho_id
                    & s.md_sanpham_id == sp.md_sanpham_id
                    ).FirstOrDefault();

                var chophepNhap0 = kho_hh == null;
                var slNX = cdh.sl_sosach.GetValueOrDefault(0) - cdh.sl_demduoc.GetValueOrDefault(0);
                if (!chophepNhap0)
                {
                    if (kho_hh.soluong.GetValueOrDefault(0) != cdh.sl_demduoc.GetValueOrDefault(0))
                    {
                        msg = $@"Hàng ""{sp.ma_sanpham}"" có số lượng trên máy khác với số lượng tồn kho hiện tại.";
                        msgErrs.Add(new Public.BaoLoiKhiHieuLuc()
                        {
                            msp = sp.ma_sanpham,
                            loi = $@"""SL trên máy đang có"" khác ""SL trên máy hiện tại"""
                        });
                    }
                    else
                    {
                        //Xuất SL hiện có
                        if (slNX < 0)
                        {
                            helperEntity.obj = new HelperEntity.objLSNX();
                            helperEntity.obj.spId = cdh.md_sanpham_id;
                            helperEntity.obj.dvtSpId = sp.md_donvitinhsanpham_id;
                            helperEntity.obj.slDichChuyen = 0 - slNX;
                            helperEntity.obj.dongNhapXuat = kk.sochungtu;
                            helperEntity.obj.sctDonHang = "";
                            helperEntity.obj.giaTriVND = 0;
                            helperEntity.obj.khoId = kk.md_kho_id;
                            helperEntity.obj.kieuchuyen = helperEntity.kieuXuatKho;
                            helperEntity.obj.ngayChuyen = nkk;
                            helperEntity.obj.theoKg = false;
                            helperEntity.obj.laphieuKK = true;
                            helperEntity.themHoacSuaLichSuNhapXuatKho(db, userTK);
                            kho_hh.soluong = kho_hh.soluong.GetValueOrDefault(0) + slNX;
                        }
                    }
                }

                if (msg.Length <= 0)
                {
                    //Nhập SL thực tế
                    if (slNX > 0 | chophepNhap0)
                    {
                        helperEntity.obj = new HelperEntity.objLSNX()
                        {
                            spId = cdh.md_sanpham_id,
                            dvtSpId = sp.md_donvitinhsanpham_id,
                            slDichChuyen = slNX,
                            dongNhapXuat = kk.sochungtu,
                            sctDonHang = "",
                            giaTriVND = 0,

                            khoId = kk.md_kho_id,
                            kieuchuyen = helperEntity.kieuNhapKho,
                            ngayChuyen = nkk,
                            theoKg = false,
                            laphieuKK = true
                        };
                        helperEntity.themHoacSuaLichSuNhapXuatKho(db, userTK);

                        if (chophepNhap0)
                        {
                            kho_hh = new md_kho_sanpham();
                            kho_hh.md_kho_sanpham_id = Helper.getNewId();
                            kho_hh.md_kho_id = kk.md_kho_id;
                            kho_hh.md_sanpham_id = sp.md_sanpham_id;
                            kho_hh.soluong = slNX;
                            kho_hh = Helper.setDefaultValueWhenInsertOrUpdate(kho_hh, userTK, false);
                            db.md_kho_sanpham.Add(kho_hh);
                        }
                        else
                        {
                            kho_hh.soluong = slNX;
                        }
                    }
                }

                var sptksServer = db.md_kho_sanpham.Where(s => s.md_sanpham_id == sp.md_sanpham_id).ToList();
                var sptks = db.md_kho_sanpham.Local.Where(s => s.md_sanpham_id == sp.md_sanpham_id).ToList();
                sp.tonkho = sptks.Sum(s => s.soluong.GetValueOrDefault(0));
            }

            if (msg.Length <= 0 & msgErrs.Count <= 0)
            {
                kk.ma_kiemke = Helper.HIEULUC;
                kk.ngayhieuluc = DateTime.Now;
                kk.ngay_kiemke = nkk;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0 & msgErrs.Count <= 0)
        {
            msg = $@"<div style='color:blue'>Hiệu lực phiếu kiểm kê thành công</div>";
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

            msg = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        context.Response.Write(msg);
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string kho = context.Request.Form["md_kho_id"];
        string id = context.Request.Form["id"];
        try
        {
            var ngay_kiemke = VNN_Config.setDateTime(context.Request.Form["ngay_kiemke"]);
            var kk = db.md_kiemke.Where(s => s.ma_kiemke != Helper.HIEULUC & s.md_kho_id == kho).OrderByDescending(p => p.ngay_kiemke).FirstOrDefault();
            if (kk != null)
            {
                msg = "false#Lỗi:Biên bản \"" + kk.ten_kiemke + "\" chưa thực thi.";
            }
            else if (db.md_kho.Where(s => s.md_kho_id == kho).Count() <= 0)
            {
                msg = string.Format("false#Lỗi: Kho đang chọn không tồn tại.");
            }
            else if (ngay_kiemke.IsDate() == false)
            {
                msg = string.Format("false#Lỗi: Ngày kiểm kê không đúng định dạng.");
            }

            if (msg.Length <= 0)
            {
                var kiemke = new md_kiemke();
                kiemke.md_kiemke_id = id_new;
                kiemke.sochungtu = VNN_VariablePublic.sochungtu(db, "BBKK", 1, false);
                kiemke.ma_kiemke = Helper.SOANTHAO;
                kiemke.ten_kiemke = context.Request.Form["ten_kiemke"];
                kiemke.md_kho_id = kho;
                kiemke.ngay_kiemke = ngay_kiemke;
                kiemke.sapxep = context.Request.Form["sapxep"];
                kiemke.mota = context.Request.Form["mota"];
                kiemke.tudong = context.Request.Form["tudong"].removeAllSpaceOrTrimText(false).ToNullableBool();
                kiemke = Helper.setDefaultValueWhenInsertOrUpdate(kiemke, userTK, false);
                db.md_kiemke.Add(kiemke);
                tudongThemDongHang(db, kiemke, userTK);

                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = string.Format(@"true#Thêm thành công.#{0}", id_new);
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
        string kho = context.Request.Form["md_kho_id"];
        string loaiHH = context.Request.Form["sapxep"];
        try
        {
            string id = context.Request.Form["id"];
            md_kiemke object_ = db.md_kiemke.Where(p => p.md_kiemke_id == id).Take(1).FirstOrDefault();
            var ngay_kiemke = VNN_Config.setDateTime(context.Request.Form["ngay_kiemke"]);
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }
            else if (object_.ma_kiemke != Helper.SOANTHAO)
            {
                msg = "false#Lỗi:Biên bản \"" + object_.ten_kiemke + "\" đã xác nhận hoặc hiệu lực.";
            }
            else if (db.md_kho.Where(s => s.md_kho_id == kho).Count() <= 0)
            {
                msg = string.Format("false#Lỗi: Kho đang chọn không tồn tại.");
            }
            else if (ngay_kiemke.IsDate() == false)
            {
                msg = string.Format("false#Lỗi: Ngày kiểm kê không đúng định dạng.");
            }

            if (msg.Length <= 0)
            {
                if (kho != object_.md_kho_id)
                {
                    db.md_kiemke_cdh.RemoveRange(db.md_kiemke_cdh.Where(s => s.md_kiemke_id == object_.md_kiemke_id));
                    db.SaveChanges();
                }

                object_.tudong = context.Request.Form["tudong"].removeAllSpaceOrTrimText(false).ToNullableBool();
                object_.ten_kiemke = context.Request.Form["ten_kiemke"];
                object_.ngay_kiemke = ngay_kiemke;
                object_.md_kho_id = kho;
                object_.sapxep = loaiHH;
                tudongThemDongHang(db, object_, userTK);
                db.SaveChanges();
                msg = "true#Cập nhật thành công.";
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string[] id_del = context.Request.Form["id"].Split(',');
            for (int i = 0; i < id_del.Length; i++)
            {
                var id_del_ = id_del[i];
                var object_ = db.md_kiemke.Where(p => p.md_kiemke_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg += string.Format(@"Lỗi dòng {0}:Không tìm thấy đối tượng cần xóa<br>", i + 1);
                }
                else if (object_.ma_kiemke != Helper.SOANTHAO)
                {
                    msg += string.Format(@"Lỗi dòng {0}:""{1}"" đã xác nhận hoặc hiệu lực<br>", i + 1, object_.ten_kiemke);
                }
                else
                {
                    db.md_kiemke.Remove(object_);
                }
            }
            if (msg.Length <= 0)
            {
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Xóa thành công.";
            }
            else
            {
                msg = "false#" + msg;
            }
        }
        catch (Exception ex)
        {
            msg = "false#Lỗi: " + ex.Message;
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