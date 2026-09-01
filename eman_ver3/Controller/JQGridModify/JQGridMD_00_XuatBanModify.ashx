<%@ WebHandler Language="C#" Class="JQGridMD_00_XuatBanModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;

public class JQGridMD_00_XuatBanModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_XuatBan":
                this.CA_01_XuatBan(context);
                break;
            case "CA_01_CopytuDonHang":
                this.CA_01_CopytuDonHang(context);
                break;
            case "CA_01_XacNhanXuatKho":
                this.CA_01_XacNhanXuatKho(context);
                break;
            case "CA_01_XuatBanGuiMail":
                this.CA_01_XuatBanGuiMail(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_XuatBanGuiMail(HttpContext context)
    {
        string msg = "", id = context.Request.Form["id"];
        string mailFrom = userTK.email.removeAllSpaceOrTrimText(true);
        string mailTo = context.Request.Form["mailTo"].removeAllSpaceOrTrimText(true);

        try
        {
            var px = db.md_xuatban.Where(s => s.md_xuatban_id == id).FirstOrDefault();

            if (px == null)
            {
                msg = $@"Không tìm thấy phiếu xuất bán";
                goto EndEventHandler;
            }
            if (px.trangthai != Helper.HIEULUC)
            {
                msg = $@"Lỗi: dòng ""{px.sochungtu}"" cần ở trạng thái ""Hiệu lực"".";
                goto EndEventHandler;
            }
            if(!Security.IsValidEmail(mailFrom))
            {
                msg = $@"Lỗi: Email của người gửi ""{mailFrom}"" không đúng định dạng.";
                goto EndEventHandler;
            }
            if(!Security.IsValidEmail(mailTo))
            {
                msg = $@"Lỗi: Email của người nhận ""{mailTo}"" không đúng định dạng.";
                goto EndEventHandler;
            }

            var gm = new GoogleMail(null);
            var json = new ADmin_JSON();
            var ttc = json.ad_systemconfigJSON().FirstOrDefault();
            gm.Email = ttc.taikhoanemail;
            gm.Password = ttc.passemail;
            gm.Smtp = ttc.emailserver;
            gm.Port = ttc.port.ToNullableInt().GetValueOrDefault(25);
            gm.SSL = ttc.website == "1";
            string loaicont = db.md_loaicont.Where(s => s.md_loaicont_id == px.loai_cont).Select(s => s.ten_cont).FirstOrDefault();
            string tieude = $"Đơn hàng {px.donhang_thamchieu} đã được xuất tại xưởng, số phiếu là {px.sochungtu}";
            string noidung = $"";
            noidung += $"Số cont: {px.so_cont}<br>";
            noidung += $"Số seal: {px.so_seal}<br>";
            noidung += $"Loại cont: {loaicont}<br>";
            noidung += $"Ngày xuất: {px.ngaychuyen.Value.ToString("dd/MM/yyyy")}";
            string linkConnect = $@"View/Print/MD_00_XuatBan/yeuCauXuatHang.aspx?id={px.md_xuatban_id}&inPDF=2&download=1";
            var msgDT = VNN_VariablePublic.GetModule(linkConnect, $"");
            gm.Attachments = new List<System.Net.Mail.Attachment>();
            var ata = new System.Net.Mail.Attachment(msgDT);
            ata.ContentDisposition.FileName = $"Yêu cầu xuất hàng {px.ngaychuyen.Value.ToString("ddMMyy")}.xls";
            gm.Attachments.Add(ata);
            gm.Send2(gm.Email, mailTo, mailFrom, "", tieude, noidung, "");
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"<div style='color:blue'>Gửi mail thành công</div>";
        }
        else
        {
            msg = $@"<div style='color:red'>{msg}</div>";
        }
        context.Response.Write(msg);
    }

    public void CA_01_XacNhanXuatKho(HttpContext context)
    {
        string md_xuatban_id = context.Request.QueryString["id"];
        string msg = "";
        var xb = db.md_xuatban.Where(s => s.md_xuatban_id == md_xuatban_id).Take(1).FirstOrDefault();
        if (xb != null)
        {
            if (xb.phieuXNNK == null | xb.phieuXNNK == "")
            {
                xb.phieuXNNK = xb.sochungtu.Replace("PXKXB", "XNXK");
                db.SaveChanges();
                msg = "true";
            }
        }
        context.Response.Write(msg);
    }

    public void CA_01_CopytuDonHang(HttpContext context)
    {
        string msg = "";
        context.Response.Write(msg);
    }

    public void CA_01_XuatBan(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        var ids = context.Request.Form["id"].removeAllSpaceOrTrimText(false).Split(',').ToList();
        int count_slxuat = 0;
        var rows = context.Request.Form["rows"];
        var msgErrs = new List<Public.BaoLoiKhiHieuLuc>();
        try
        {
            var px = db.md_xuatban.Where(s => ids.Contains(s.md_xuatban_id)).FirstOrDefault();

            if (px == null)
            {
                msg = $@"Không tìm thấy phiếu xuất bán";
                goto EndEventHandler;
            }
            if (px.trangthai != Helper.DANHAN)
            {
                msg = $@"Lỗi: dòng ""{px.sochungtu}"" cần ở trạng thái ""Đã Xác Nhận"".";
                goto EndEventHandler;
            }
            var khoXuat = db.md_kho.Where(s => s.md_kho_id == px.tukho).FirstOrDefault();
            if(khoXuat == null)
            {
                msg = $@"Lỗi: không tìm thấy ""Kho xuất"" đã chọn.";
                goto EndEventHandler;
            }
            var xuatDen = db.md_doitackinhdoanh.Where(s => s.md_doitackinhdoanh_id == px.md_doitackinhdoanh_id).FirstOrDefault();
            if(xuatDen == null)
            {
                msg = $@"Lỗi: không tìm thấy ""Khách hàng"".";
                goto EndEventHandler;
            }
            string ngayXuatStr = context.Request.Form["ngayXuat"];
            var ngayXuat = VNN_Config.setDateTime(ngayXuatStr);
            if (!ngayXuat.IsDate())
            {
                msg = $@"Lỗi: Giá trị ngày xuất kho bị sai.";
                goto EndEventHandler;
            }
            px.ngaychuyen = ngayXuat;
            var cdvcs = db.md_xuatban_cdh.Where(s => s.md_xuatban_id == px.md_xuatban_id).ToList();
            var dongHangs = JsonConvert.DeserializeObject<List<md_xuatban_cdh>>(rows);
            foreach (var dh in cdvcs)
            {
                var dongHang = dongHangs.Where(s => s.md_xuatban_cdh_id == dh.md_xuatban_cdh_id).FirstOrDefault();
                if (dongHang != null)
                {
                    dh.sl_xuat = dongHang.sl_xuat;
                    dh.sl_inner = dongHang.sl_inner;
                    dh.sl_outer = dongHang.sl_outer;
                    dh.tldg = dongHang.tldg;
                    dh.nw = dongHang.nw;
                    dh.gw = dongHang.gw;
                    dh.cbm = dongHang.cbm;
                }
            }

            foreach (var dh in cdvcs.Where(s => s.sl_xuat > 0).ToList())
            {
                var khosp = db.md_kho_sanpham.Where(s => s.md_kho_id == px.tukho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();

                if (khosp == null)
                {
                    msg += $@"<br>Lỗi: ""{sp.ma_sanpham}"" không có trong kho.";
                    msgErrs.Add(new Public.BaoLoiKhiHieuLuc()
                    {
                        msp = sp.ma_sanpham,
                        loi = $@"không có trong kho"
                    });
                }
                else if (khosp.soluong < dh.sl_xuat)
                {
                    msg += $@"<br>Lỗi: Số lượng ""{sp.ma_sanpham}"" trong kho chỉ còn: ""{khosp.soluong.Value.DropTrailingZeros()}""";
                    msgErrs.Add(new Public.BaoLoiKhiHieuLuc()
                    {
                        msp = sp.ma_sanpham,
                        loi = $@"Trong kho chỉ còn ""{khosp.soluong.Value.DropTrailingZeros()}"""
                    });
                }

                var ddsdh = db.c_dongdsdh.Where(s => s.c_danhsachdathang_id == px.c_danhsachdathang_id & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
                if (ddsdh == null)
                {
                    msg += $@"<br>Lỗi: ""{sp.ma_sanpham}"" không có trong đơn hàng ""{px.donhang_thamchieu}"".";
                    msgErrs.Add(new Public.BaoLoiKhiHieuLuc()
                    {
                        msp = sp.ma_sanpham,
                        loi = $@"không có trong đơn hàng ""{px.donhang_thamchieu}"""
                    });
                }
                else
                {
                    var sltd = ddsdh.sl_donggoiTP.GetValueOrDefault(0) -
                        ddsdh.sl_dagiao.GetValueOrDefault(0) - ddsdh.sl_thuhoi.GetValueOrDefault(0);

                    var sltdDH = ddsdh.sl_dathang.GetValueOrDefault(0) - ddsdh.sl_giamhanngach.GetValueOrDefault(0);
                    sltd = sltd > sltdDH ? sltdDH : sltd;
                    if(dh.sl_xuat > sltd)
                    {
                        msg += $@"<br>Lỗi: ""{sp.ma_sanpham}"" vượt quá hạn mức được xuất ""{sltd.DropTrailingZeros()}"".";
                        msgErrs.Add(new Public.BaoLoiKhiHieuLuc()
                        {
                            msp = sp.ma_sanpham,
                            loi = $@"vượt quá hạn mức được xuất ""{sltd.DropTrailingZeros()}"""
                        });
                    }
                }

                if (msg.Length <= 0 & dh.sl_xuat.GetValueOrDefault(0) > 0)
                {
                    dh.sl_tonkho = khosp == null ? 0 : khosp.soluong.GetValueOrDefault(0);
                    count_slxuat++;
                    //--cap nhat so luong xuat
                    edit_xuat(db, dh, px, userTK);
                    //--cap nhat vao kho
                    xuat_kho(db, dh, px, khosp, userTK);
                    //--tao lich su nhap kho
                    add_kho_ls(db, dh, px, userTK);
                }
            }

            if (msg.Length <= 0 & count_slxuat <= 0)
            {
                msg += $@"<br>Lỗi: ""{px.sochungtu}"" phải có ít nhất 1 dòng hàng có số lượng lớn hơn 0.";
            }

            if (msg.Length <= 0)
            {
                var dsdh = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == px.c_danhsachdathang_id).FirstOrDefault();
                if (dsdh != null)
                {
                    var checkKTServer = db.c_dongdsdh.Where(s =>
                        s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id &
                        (s.sl_hanngach ?? 0) + (s.sl_thuhoi ?? 0) < s.sl_dathang - (s.sl_giamhanngach ?? 0)
                        ).ToList();

                    var checkKT = db.c_dongdsdh.Local.Where(s =>
                        s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id &
                        (s.sl_hanngach ?? 0) + (s.sl_thuhoi ?? 0) < s.sl_dathang - (s.sl_giamhanngach ?? 0)
                        ).ToList();

                    if (checkKT.Count <= 0)
                    {
                        dsdh.trangthai = Helper.KETTHUC;
                        dsdh.md_trangthai_id = Helper.DaXuatHang;
                    }
                }

                px.trangthai = Helper.HIEULUC;
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
            msg = $@"<div style='color:blue'>Hiệu lực phiếu xuất bán thành công</div>";
        }
        else
        {
            if (msgErrs.Count > 0)
                msg = "Thiếu thông tin";

            if (msg.StartsWith("<br>"))
                msg = msg.Substring(4);

            var result = new
            {
                msg = $@"<div error style='color:red'>{msg}</div>",
                json = msgErrs
            };

            msg = JsonConvert.SerializeObject(result);
        }

        context.Response.Write(msg);
    }

    //--cap nhat so luong xuat
    public string edit_xuat(EntityContext db, md_xuatban_cdh dh, md_xuatban xk, User_TK us)
    {
        decimal sl_daxuat = dh.sl_daxuat.Value + dh.sl_xuat.Value;
        if (dh.tong_sl_xuat.Value <= sl_daxuat)
        {
            sl_daxuat = dh.tong_sl_xuat.Value;
            dh.check_kho = true;
        }

        dh.sl_daxuat = dh.sl_xuat + dh.sl_daxuat;
        xk.ngaycapnhat = DateTime.Now;
        return null;
    }

    //xuat kho
    public string xuat_kho(EntityContext db, md_xuatban_cdh dh, md_xuatban px, md_kho_sanpham sp, User_TK us)
    {
        sp.soluong = sp.soluong.GetValueOrDefault(0) - dh.sl_xuat;
        sp.ngaycapnhat = DateTime.Now;
        var dsdh = db.c_dongdsdh.Where(s => s.c_danhsachdathang_id == px.c_danhsachdathang_id & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
        if (dsdh != null)
        {
            dsdh.sl_hanngach = dsdh.sl_hanngach.GetValueOrDefault(0) + dh.sl_xuat.GetValueOrDefault(0);
            dsdh.sl_dagiao = dsdh.sl_dagiao.GetValueOrDefault(0) + dh.sl_xuat;
            dsdh.sl_conlai = dsdh.sl_conlai.GetValueOrDefault(0) - dh.sl_xuat;
        }
        return null;
    }
    //--tao lich su nhap kho
    public string add_kho_ls(EntityContext db, md_xuatban_cdh dh, md_xuatban px, User_TK us)
    {
        string id_new = Helper.getNewId();
        var giao = new md_kho_giaodich();
        giao.md_kho_giaodich_id = id_new;
        giao.md_kho_id = px.tukho;
        giao.md_sanpham_id = dh.md_sanpham_id;
        giao.soluong_dichchuyen = dh.sl_xuat;
        giao.ngaychuyen = px.ngaychuyen;
        giao.kieuchuyen = Helper.XuatKho;
        giao.dongnhapxuat = px.sochungtu;
        giao.dongkiemkho = px.sochungtu;
        giao.dongvanchuyen = px.sochungtu;
        giao.dongsanxuat = px.sochungtu;
        giao.md_donvitinhsanpham_id = dh.md_donvitinhsanpham_id;
        giao.mota = px.donhang_thamchieu;
        giao.donhang = px.donhang_thamchieu;
        giao = Helper.setDefaultValueWhenInsertOrUpdate(giao, userTK, false);
        giao.hoatdong = true;
        giao.ngaytao = DateTime.Now;
        giao.ngaycapnhat = DateTime.Now;
        db.md_kho_giaodich.Add(giao);
        return id_new;
    }

    public void add(HttpContext context)
    {
        string msg = "";
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string md_dtk_id = context.Request.Form["md_doitackinhdoanh_id"];
        string diachi = context.Request.Form["diachi"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var dtk = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == md_dtk_id).FirstOrDefault();
                string id = context.Request.Form["id"];
                var object_ = db.md_xuatban.Where(p => p.md_xuatban_id == id).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (object_.trangthai == "HIEULUC")
                {
                    msg = string.Format(@"Lỗi:Phiếu xuất kho đã ""Hiệu Lực"".");
                }
                else if (dtk == null)
                {
                    msg = "Lỗi:Mã đối tác \"" + md_dtk_id + "\" không tồn tại.";
                }

                if (msg.Length <= 0)
                {
                    VNN_Function.SetFormValue(object_.nameof(s => s.md_doitackinhdoanh_id), dtk.md_doitackinhdoanh_id);
                    VNN_Function.SetFormValue(object_.nameof(s => s.diachi), dtk.diachi);
                    VNN_Function.SetFormValue(object_.nameof(s => s.sochungtu), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s => s.trangthai), "VNN_notpost");
                    entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#Cập nhật thành công");
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = string.Format(@"false#{0}", msg);
                transaction.Rollback();
            }
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
                var object_ = db.md_xuatban.Where(p => p.md_xuatban_id == id_del_).Take(1).FirstOrDefault();
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
                    VNN_Function.Write_log(context, ma_module, null, oper, "PXB:" + object_.sochungtu, db);
                    object_.trangthai = Helper.SOANTHAO;

                    var cdhs = db.md_xuatban_cdh.Where(s => s.md_xuatban_id == object_.md_xuatban_id & s.sl_xuat != null).ToList();
                    foreach(var cdh in cdhs)
                    {
                        cdh.sl_xuat = null;
                    }
                    db.SaveChanges();
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if(msg.Length <= 0)
        {
            msg = @"true#Trả phiếu xuất bán đã chọn về ""Soạn Thảo"" thành công.";
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