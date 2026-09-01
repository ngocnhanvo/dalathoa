<%@ WebHandler Language="C#" Class="JQGridMD_00_XuatBanThanhLyModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_XuatBanThanhLyModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA01XuatBan_MD00XuatBanThanhLy":
                this.CA01XuatBan_MD00XuatBanThanhLy(context);
                break;
            case "CA01CopytuDonHang_MD00XuatBanThanhLy":
                this.CA01CopytuDonHang_MD00XuatBanThanhLy(context);
                break;
            case "CA_01_XacNhanXuatKho":
                this.CA_01_XacNhanXuatKho(context);
                break;
            default:
                break;
        }
    }
    public void CA_01_XacNhanXuatKho(HttpContext context)
    {
        string md_xuatban_id = context.Request.QueryString["id"];
        string msg = "";
        var xb = db.md_xuatban.Where(s=>s.md_xuatban_id == md_xuatban_id).Take(1).FirstOrDefault();
        if(xb != null) {
            if (xb.phieuXNNK == null | xb.phieuXNNK == "")
            {
                xb.phieuXNNK = xb.sochungtu.Replace("PXKT","XNXK");
                db.SaveChanges();
                msg = "true";
            }
        }
        context.Response.Write(msg);
    }

    public void CA01CopytuDonHang_MD00XuatBanThanhLy(HttpContext context)
    {
        string sct_dsdh = context.Request.Form["value_donhang"];
        string sochungtu = VNN_VariablePublic.sochungtu(db, "PXKT", 1);
        string msg = "";
        var dsdh = db.c_danhsachdathang.Where(s => s.sochungtu == sct_dsdh).Take(1).FirstOrDefault();
        string id = Helper.getNewId();
        if (dsdh == null)
        {
            msg = "<div style='color:red'>Lỗi: Không tìm thấy đơn hàng \"" + sct_dsdh + "\"</div>";
        }
        else
        {
            string check_lsx_kho = db.md_xuatban.Where(s=>s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id & s.trangthai == "SOANTHAO").Select(s=>s.sochungtu).FirstOrDefault();
            if(check_lsx_kho != null & check_lsx_kho != "") {
                msg = "<div style='color:red'>Lỗi: Phiếu xuất kho \""+ check_lsx_kho +"\" chưa hiệu lực.</div>";
            }
            else {
                var don = db.c_dongdsdh.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).FirstOrDefault();
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == don.md_sanpham_id).Take(1).FirstOrDefault();
                var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == "ANCO TRADING").Take(1).FirstOrDefault();

                var xb = new md_xuatban();
                xb.md_xuatban_id = id;
                xb.sochungtu = sochungtu;
                xb.donhang_thamchieu = dsdh.so_po;
                xb.c_danhsachdathang_id = dsdh.c_danhsachdathang_id;
                xb.phieuXNNK = "";
                xb.ngaychuyen = DateTime.Now;
                xb.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
                xb.diachi = dtkd.diachi;
                xb.ngaydonhang = dsdh.hangiaohang_po;
                xb.trangthai = "SOANTHAO";
                xb.tukho = sp.khomacdinh;
                xb.sctdathang = dsdh.sochungtu;
                xb.nguoitao = userTK.ad_user_id;
                xb.vaitrotao = userTK.ad_role_id;
                xb.bophantao = userTK.md_phongban_id;
                xb.value_nguoitao = userTK.ma_user;
                xb.value_vaitrotao = userTK.ten_role;
                xb.value_bophantao = userTK.ten_phongban;

                xb.nguoicapnhat = userTK.ad_user_id;
                xb.vaitrocapnhat = userTK.ad_role_id;
                xb.bophancapnhat = userTK.md_phongban_id;
                xb.value_nguoicapnhat = userTK.ma_user;
                xb.value_vaitrocapnhat = userTK.ten_role;
                xb.value_bophancapnhat = userTK.ten_phongban;

                xb.ngaytao = DateTime.Now;
                xb.ngaycapnhat = DateTime.Now;
                xb.mota = "";
                xb.hoatdong = true;

                int count = 0;
                foreach (var ddsdh in db.c_dongdsdh.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id & s.sl_dathang > s.sl_hanngach).ToList())
                {
                    count++;
                    var sp1 = db.md_sanpham.Where(s => s.md_sanpham_id == ddsdh.md_sanpham_id).Take(1).FirstOrDefault();
                    var cdh_xb = new md_xuatban_cdh
                    {
                        md_xuatban_cdh_id = Helper.getNewId(),
                        md_xuatban_id = id,
                        md_sanpham_id = sp1.md_sanpham_id,
                        tong_sl_xuat = ddsdh.sl_dathang.Value - ddsdh.sl_hanngach.Value,
                        sl_daxuat = 0,
                        sl_xuat = 0,
                        md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id,
                        tenhang = sp1.mota_tiengviet,
                        nguoitao = userTK.ad_user_id,
                        vaitrotao = userTK.ad_role_id,
                        bophantao = userTK.md_phongban_id,
                        value_nguoitao = userTK.ma_user,
                        value_vaitrotao = userTK.ten_role,
                        value_bophantao = userTK.ten_phongban,

                        nguoicapnhat = userTK.ad_user_id,
                        vaitrocapnhat = userTK.ad_role_id,
                        bophancapnhat = userTK.md_phongban_id,
                        value_nguoicapnhat = userTK.ma_user,
                        value_vaitrocapnhat = userTK.ten_role,
                        value_bophancapnhat = userTK.ten_phongban,

                        ngaytao = DateTime.Now,
                        ngaycapnhat = DateTime.Now,
                        mota = "",
                        hoatdong = true
                    };
                    db.md_xuatban_cdh.Add(cdh_xb);
                }

                if(count > 0)
                    db.md_xuatban.Add(xb);
                else
                    msg = "<div style='color:red'>Lỗi: Đơn hàng \"" + sct_dsdh + "\" đã xuất hết tất cả dòng hàng.</div>";
            }
        }

        if (msg.Length <= 0)
        {
            db.SaveChanges();
            msg = id + "#<div style='color:blue'>Tạo phiếu xuất kho \"" + sochungtu + "\" thành công.</div>";
        }
        context.Response.Write(msg);
    }

    public void CA01XuatBan_MD00XuatBanThanhLy(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        string[] vnn = id.Split(',');
        int count_slxuat = 0;

        try
        {
            //Xu Ly nhap kho
            var px = db.md_xuatban.Where(s => vnn.Contains(s.md_xuatban_id) & s.xuat_thanhly == 1).FirstOrDefault();
            if(px == null)
            {
                msg = "Không tìm thấy phiếu xuất";
                goto EndEventHandler;
            }
            if (px.trangthai == Helper.HIEULUC)
            {
                msg = "Đã hiệu lực";
                goto EndEventHandler;
            }

            if (px.tukho == null | px.tukho == "")
            {
                msg = "Chưa chọn kho cần xuất";
                goto EndEventHandler;
            }

            var khoXuat = db.md_kho.Where(s => s.md_kho_id == px.tukho).FirstOrDefault();
            if(khoXuat == null)
            {
                msg = "Kho xuất không tồn tại";
                goto EndEventHandler;
            }

            string soluong_xuat = "0";
            var cdhs = db.md_xuatban_cdh.Where(s => s.md_xuatban_id == px.md_xuatban_id & (s.check_kho == null | s.check_kho == false)).ToList();
            foreach (var dh in cdhs)
            {
                //--
                var kho = db.md_kho_sanpham.Where(s => s.md_kho_id == px.tukho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();

                if (kho == null)
                {
                    if (dh.sl_xuat > 0)
                    {
                        msg += $@"""{sp.ma_sanpham}"" không có trong kho.<br>";
                        count_slxuat++;
                    }
                }
                else if (kho.soluong < dh.sl_xuat)
                {
                    msg += $@"Số lượng ""{sp.ma_sanpham}"" trong kho chỉ còn: ""{kho.soluong.Value.DropTrailingZeros()}"".<br>";
                }
                else if (dh.sl_xuat < 0)
                {
                    msg += $@"Số lượng xuất của ""{sp.ma_sanpham}"" phải lớn hơn 0.<br>";
                }

                if (msg.Length <= 0 & dh.sl_xuat > 0)
                {
                    dh.sl_tonkho = kho == null ? 0 : kho.soluong.GetValueOrDefault(0);
                    count_slxuat++;
                    soluong_xuat = "" + dh.sl_xuat.GetValueOrDefault(0).DropTrailingZeros();
                    //--cap nhat so luong xuat
                    edit_xuat(db, dh, userTK);
                    //--cap nhat vao kho
                    xuat_kho(db, dh, px, userTK);
                    //--tao lich su nhap kho
                    add_kho_ls(db, dh, px, userTK);
                }
            }

            if (msg.Length <= 0 & count_slxuat <= 0)
            {
                msg = $@"Phải có ít nhất 1 dòng hàng có số lượng lớn hơn 0.</div>";
            }

            if (msg.Length <= 0)
            {
                px.trangthai = Helper.HIEULUC;
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
            msg = $"<div style='color:blue'>Hiệu lực thành công</div>";
        }
        else
        {
            msg = $"<div style='color:red' error>Lỗi: {msg}</div>";
        }

        context.Response.Write(msg);
    }
    //--cap nhat so luong xuat
    public string edit_xuat(EntityContext db, md_xuatban_cdh dh, User_TK us)
    {
        string id_new = dh.md_xuatban_id;

        decimal sl_daxuat = dh.sl_daxuat.GetValueOrDefault(0) + dh.sl_xuat.GetValueOrDefault(0);
        if (dh.tong_sl_xuat.GetValueOrDefault(0) <= sl_daxuat)
        {
            sl_daxuat = dh.tong_sl_xuat.GetValueOrDefault(0);
            dh.check_kho = true;
        }

        dh.sl_daxuat = sl_daxuat;
        return id_new;
    }

    //xuat kho
    public string xuat_kho(EntityContext db, md_xuatban_cdh dh, md_xuatban px, User_TK us)
    {
        string id_new = dh.md_xuatban_id;
        var check_khospServer = db.md_kho_sanpham.Where(s => s.md_kho_id == px.tukho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
        var check_khosp = db.md_kho_sanpham.Local.Where(s => s.md_kho_id == px.tukho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
        if (check_khosp != null)
        {
            id_new = check_khosp.md_kho_sanpham_id;
            check_khosp.soluong = check_khosp.soluong.GetValueOrDefault(0) - dh.sl_xuat.GetValueOrDefault(0);
            check_khosp.ngaycapnhat = DateTime.Now;
        }
        return id_new;
    }
    //--tao lich su nhap kho
    public string add_kho_ls(EntityContext db, md_xuatban_cdh dh, md_xuatban px, User_TK us)
    {
        string id_new = Helper.getNewId();
        md_kho_giaodich giao = new md_kho_giaodich();
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

        giao.nguoitao = us.ad_user_id;
        giao.vaitrotao = us.ad_role_id;
        giao.bophantao = us.md_phongban_id;
        giao.value_nguoitao = us.ma_user;
        giao.value_vaitrotao = us.ten_role;
        giao.value_bophantao = us.ten_phongban;
        giao.nguoicapnhat = us.ad_user_id;
        giao.vaitrocapnhat = us.ad_role_id;
        giao.bophancapnhat = us.md_phongban_id;
        giao.value_nguoicapnhat = us.ma_user;
        giao.value_vaitrocapnhat = us.ten_role;
        giao.value_bophancapnhat = us.ten_phongban;
        giao.hoatdong = true;
        giao.ngaytao = DateTime.Now;
        giao.ngaycapnhat = DateTime.Now;
        db.md_kho_giaodich.Add(giao);

        return id_new;
    }

    public void add(HttpContext context)
    {
        context.Response.Write("");
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string md_dtk_id = context.Request.Form["md_doitackinhdoanh_id"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                //md_doitackinhdoanh dtk = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == md_dtk_id).FirstOrDefault();
                string id = context.Request.Form["id"];
                var object_ = db.md_xuatban.Where(p => p.md_xuatban_id == id).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (object_.trangthai == "HIEULUC")
                {
                    msg = "Lỗi:phiếu xuất kho đã Hiệu lực.";
                }
                else
                {
                    VNN_Function.SetFormValue(object_.nameof(s=>s.sochungtu), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.trangthai), "VNN_notpost");
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
                    object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#Cập nhật thành công.");
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = "false#" + msg;
                transaction.Rollback();
            }
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
                    var object_ = db.md_xuatban.Where(p => p.md_xuatban_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else if (object_.trangthai == "HIEULUC")
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Đã được ""Hiệu Lực"".", object_.sochungtu);
                    }
                    else
                    {
                        VNN_Function.Write_log(context, ma_module, null, oper, "PXKKT:" + object_.sochungtu, db);
                        object_.trangthai = "SOANTHAO";
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
                msg = @"true#Trả phiếu xuất kho đã chọn về ""Soạn Thảo"" thành công.";
                transaction.Commit();
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