<%@ WebHandler Language="C#" Class="JQGridMD_00_NhapkhotuNCCModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
public class JQGridMD_00_NhapkhotuNCCModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public class khdh_lsx
    {
        public string khdhid { get; set; }
        public string lsxid { get; set; }
    }
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
            case "CA_01_XLNKNCC":
                this.CA_01_XLNKNCC(context);
                break;
            case "CA_01_XacNhanNKNCC":
                this.CA_01_XacNhanNKNCC(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_XacNhanNKNCC(HttpContext context)
    {
        string msg = "";
        context.Response.Write(msg);
    }

    public void CA_01_XLNKNCC(HttpContext context)
    {
        string msg = "", msg_success = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        int count_slnhap = 0;


        //Xu Ly nhap kho
        var px = db.md_nhapkho_ncc.Where(s => s.md_nhapkho_ncc_id == id).FirstOrDefault();
        if (px.trangthai == Helper.HIEULUC)
        {
            msg = $@"Lỗi: Phiếu ""{px.sochungtu}"" đã hiệu lực.</div>";
            goto EndEventHandler;
        }

        var khoNhap = db.md_kho.Where(s => s.md_kho_id == px.kho).FirstOrDefault();
        if (khoNhap == null)
        {
            msg = $@"Lỗi: Phiếu ""{px.sochungtu}"" chưa chọn kho.";
            goto EndEventHandler;
        }

        if (px.trangthai != Helper.DANHAN)
        {
            msg = $@"Lỗi: Phiếu ""{px.sochungtu}"" cần ở trạng thái ""Đã Xác Nhận"".";
            goto EndEventHandler;
        }

        string ngayNhapStr = context.Request.Form["ngayNhap"];
        var ngayNhap = VNN_Config.setDateTime(ngayNhapStr);
        if (!ngayNhap.IsDate())
        {
            msg = $@"Lỗi: Giá trị ngày nhập kho bị sai.";
            goto EndEventHandler;
        }
        px.ngaychuyen = ngayNhap;

        var md_nhapkho_ncc_dhs = db.md_nhapkho_ncc_dh
                .Where(s => s.md_nhapkho_ncc_id == px.md_nhapkho_ncc_id & s.sl_nhap != null & s.sl_nhap > 0)
                .OrderBy(s => s.so_dmh).ToList();

        c_donmuahang dmh = null;
        var tsxIds = new List<string>();
        var tsxVTIds = new List<string>();
        var khlsxIds = new List<khdh_lsx>();
        var dsdhIds = new List<string>();
        foreach (var dh in md_nhapkho_ncc_dhs)
        {
            var sp = db.md_sanpham.Where(s => s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();

            var xetDK = false;
            if (dmh == null)
            {
                dmh = db.c_donmuahang.Where(s => s.sochungtu == dh.so_dmh).FirstOrDefault();
                xetDK = true;
            }
            else if (dmh.sochungtu != dh.so_dmh)
            {
                dmh = db.c_donmuahang.Where(s => s.sochungtu == dh.so_dmh).FirstOrDefault();
                xetDK = true;
            }

            if (dmh != null & xetDK)
            {
                var stts = new int?[] { 9998, 9999 };
                var kht = db.c_kehoachdathangtong.Where(s => s.c_kehoachdathangtong_id == dmh.c_kehoachdathang_dhncc_id).FirstOrDefault();
                if (kht == null)
                {
                    var kh = db.c_kehoachdathang.Where(s => s.c_kehoachdathang_id == dmh.c_kehoachdathang_dhncc_id).FirstOrDefault();
                    if (kh != null)
                    {
                        //var dhpxSCT = db.c_kehoachdathang_dhcpx.Where(s =>
                        //    s.c_kehoachdathang_id == kh.c_kehoachdathang_id
                        //    & s.md_phanxuong_id == sp.md_phanxuong_id
                        //    ).Select(s => s.dongdathang).FirstOrDefault();

                        //var dhpxIds = db.md_dondathangphanxuong.Where(s => dhpxSCT.Contains(s.sochungtu)).Select(s => s.md_dondathangphanxuong_id).ToList();

                        var lsxs = db.md_lenhsanxuat.Where(s => s.c_kehoachdathang_id == kh.c_kehoachdathang_id).OrderBy(s => s.ngayketthuc).ToList();
                        var lsxIds = lsxs.Select(s => s.md_lenhsanxuat_id).ToList();
                        var tsxs = db.md_lenhsanxuat_tosx.Where(s => lsxIds.Contains(s.md_lenhsanxuat_id) & stts.Contains(s.stt)).ToList();
                        tsxIds = tsxs.Select(s => s.md_lenhsanxuat_tosx_id).ToList();
                        //var tsxVTs = db.md_lenhsanxuat_tosx.Where(s => lsxIds.Contains(s.md_lenhsanxuat_id) & s.stt == 9999).ToList();
                        //tsxVTIds = tsxVTs.Select(s => s.md_lenhsanxuat_tosx_id).ToList();
                        foreach (var lsx in lsxs.Where(s=>(s.sxton ?? false) == true).ToList())
                            khlsxIds.Add(new khdh_lsx() { khdhid = lsx.c_kehoachdathang_id, lsxid = lsx.md_lenhsanxuat_id });
                    }
                }
                else
                {
                    var lsxs = db.md_lenhsanxuat.Where(s => s.nhomKH == kht.ten_kh).OrderBy(s => s.ngayketthuc).ToList();
                    var lsxIds = lsxs.Select(s => s.md_lenhsanxuat_id).ToList();
                    var tsxs = db.md_lenhsanxuat_tosx.Where(s => lsxIds.Contains(s.md_lenhsanxuat_id) & stts.Contains(s.stt)).ToList();
                    tsxIds = tsxs.Select(s => s.md_lenhsanxuat_tosx_id).ToList();
                    //var tsxVTs = db.md_lenhsanxuat_tosx.Where(s => lsxIds.Contains(s.md_lenhsanxuat_id) & s.stt == 9999).ToList();
                    //tsxVTIds = tsxVTs.Select(s => s.md_lenhsanxuat_tosx_id).ToList();
                    foreach (var lsx in lsxs.Where(s => (s.sxton ?? false) == true).ToList())
                        khlsxIds.Add(new khdh_lsx() { khdhid = lsx.c_kehoachdathang_id, lsxid = lsx.md_lenhsanxuat_id });
                }
            }

            decimal sltn = dh.sl_nhap.GetValueOrDefault(0);
            var cdhsServer = db.md_lenhsanxuat_tosx_cdh.Where(s =>
                tsxIds.Contains(s.md_lenhsanxuat_tosx_id)
                & s.sl_datncc > 0
                & s.md_sanpham_id == dh.md_sanpham_id).ToList();

            var cdhs = db.md_lenhsanxuat_tosx_cdh.Local.Where(s =>
                tsxIds.Contains(s.md_lenhsanxuat_tosx_id)
                & s.sl_datncc > 0
                & s.md_sanpham_id == dh.md_sanpham_id).ToList();

            foreach (var cdh in cdhs)
            {
                if (sltn > 0)
                {
                    var sltd = sltn > cdh.sl_datncc.GetValueOrDefault(0) ? cdh.sl_datncc.GetValueOrDefault(0) : sltn;
                    cdh.sl_dahoanthanh = cdh.sl_dahoanthanh.GetValueOrDefault(0) + sltd;
                    sltn = sltn - sltd;

                    var khdhId = khlsxIds.Where(s => s.lsxid == cdh.md_lenhsanxuat_id).Select(s => s.khdhid).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(khdhId))
                    {
                        var kh = db.c_kehoachdathang.Where(s => s.c_kehoachdathang_id == khdhId).FirstOrDefault();
                        if (kh != null)
                        {
                            if (kh.sanxuatton.GetValueOrDefault(true))
                            {
                                var ddsdhServer = db.c_dongdsdh.Where(s => s.c_danhsachdathang_id == kh.c_danhsachdathang_id & s.md_sanpham_id == cdh.md_sanpham_id).FirstOrDefault();
                                var ddsdh = db.c_dongdsdh.Local.Where(s => s.c_danhsachdathang_id == kh.c_danhsachdathang_id & s.md_sanpham_id == cdh.md_sanpham_id).FirstOrDefault();
                                if(ddsdh != null)
                                {
                                    ddsdh.sl_nhaphang = ddsdh.sl_nhaphang.GetValueOrDefault(0) + sltd;
                                    ddsdh.sl_conlai = ddsdh.sl_conlai.GetValueOrDefault(0) - sltd;
                                }
                                dsdhIds.Add(kh.c_danhsachdathang_id);
                            }
                        }
                    }
                }
            }

            //decimal sltnVT = dh.sl_nhap.GetValueOrDefault(0);
            //var cdhVTsServer = db.md_lenhsanxuat_tosx_vattu.Where(s =>
            //    tsxVTIds.Contains(s.md_lenhsanxuat_tosx_id)
            //    & s.md_sanpham_id == dh.md_sanpham_id).ToList();

            //var cdhVTs = db.md_lenhsanxuat_tosx_vattu.Local.Where(s =>
            //    tsxVTIds.Contains(s.md_lenhsanxuat_tosx_id)
            //    & s.md_sanpham_id == dh.md_sanpham_id).ToList();

            //foreach(var cdhVT in cdhVTs)
            //{
            //    if (sltnVT > 0)
            //    {
            //        var sldn = cdhVT.soluong.GetValueOrDefault(0) - cdhVT.sl_hanngach.GetValueOrDefault(0);
            //        var sltd = sltnVT > sldn ? sldn : sltnVT;
            //        cdhVT.sl_hanngach = cdhVT.sl_hanngach.GetValueOrDefault(0) + sltd;
            //        sltnVT = sltnVT - sltd;
            //    }
            //}

            try
            {
                count_slnhap++;
                decimal soluong_nhap = dh.sl_nhap.Value;
                //--cap nhat so luong nhap
                edit_nhap(db, dh, px, userTK);
                //--cap nhat vao kho
                string khospId = add_kho(db, dh, px, userTK);
                //--tao lich su nhap kho
                add_kho_ls(db, dh, px, userTK, dmh);

                msg_success += $@"<div>HHVT ""{sp.ma_sanpham}"" đã nhập kho ""{soluong_nhap.DropTrailingZeros()}""</div>";

            }
            catch (Exception ex)
            {
                msg = $@"<div style='color:red'>Lỗi: {ex.Message}</div>";
                goto EndEventHandler;
            }
        }

        if (count_slnhap <= 0)
        {
            msg = $@"Phiếu ""{px.sochungtu}"" phải có ít nhất 1 dòng hàng có số lượng lớn hơn 0.";
            goto EndEventHandler;
        }

        //Kiem tra de chuyen thanh Hieu Luc
        if (msg.Length <= 0)
        {
            px.trangthai = Helper.HIEULUC;

            var dmhs = db.c_donmuahang.Where(s => px.c_donmuahang_id.Contains(s.c_donmuahang_id)).ToList();
            foreach (var dmh1 in dmhs)
            {
                var cdhsServer = db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == dmh1.c_donmuahang_id).Where(s => s.sl_dadat > s.sl_hanngach).ToList();
                var cdhs = db.c_donmuahang_cdmh.Local.Where(s => s.c_donmuahang_id == dmh1.c_donmuahang_id).Where(s => s.sl_dadat > s.sl_hanngach).ToList().Count;
                if (cdhs <= 0)
                    dmh1.md_trangthai_id = "DAXONG";
                else
                    dmh1.md_trangthai_id = "CHUAXONG";
            }

            if(dsdhIds.Count > 0)
            {
                dsdhIds = dsdhIds.Distinct().ToList();
                foreach(var dsdhId in dsdhIds)
                {
                    var dsdh = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == dsdhId).FirstOrDefault();
                    var donghangsServer = db.c_dongdsdh.Where(s => s.c_danhsachdathang_id == dsdhId).ToList();
                    var donghangs = db.c_dongdsdh.Local.Where(s => s.c_danhsachdathang_id == dsdhId).ToList();
                    var daHT = donghangs.Where(s => s.sl_dathang.GetValueOrDefault(0) - s.sl_nhaphang.GetValueOrDefault(0) - s.sl_giamhanngach.GetValueOrDefault(0) > 0).Count() <= 0;
                    if (daHT & donghangs.Count > 0)
                    {
                        dsdh.trangthai = Helper.KETTHUC;
                        dsdh.md_trangthai_id = Helper.ChoDG;
                    }
                }
            }
            db.SaveChanges();
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

    //--cap nhat so luong nhap
    public string edit_nhap(EntityContext db, md_nhapkho_ncc_dh dh, md_nhapkho_ncc nk, User_TK us)
    {
        string id_new = dh.md_nhapkho_ncc_id;
        var dmh_cdmh = db.c_donmuahang_cdmh.Where(s =>
            s.c_donmuahang_id == dh.c_donmuahang_id
            & s.md_sanpham_id == dh.md_sanpham_id
        ).FirstOrDefault();

        if (dmh_cdmh != null)
            dmh_cdmh.sl_hanngach = dmh_cdmh.sl_hanngach.GetValueOrDefault(0) + dh.sl_nhap.GetValueOrDefault(0);

        dh.sl_danhap = dh.sl_nhap.GetValueOrDefault(0);
        nk.ngaycapnhat = DateTime.Now;

        foreach (var check_nk in db.md_nhapkho_ncc.
                Where(s => s.md_nhapkho_ncc_id != nk.md_nhapkho_ncc_id
                    & s.c_donmuahang_id.Contains(dh.c_donmuahang_id)
                    & new string[] { "SOANTHAO", "DANHAN" }.Contains(s.trangthai)).ToList())
        {
            var ck_dh = db.md_nhapkho_ncc_dh.
                    Where(s =>
                        s.md_nhapkho_ncc_id == check_nk.md_nhapkho_ncc_id
                        & s.c_donmuahang_id == dh.c_donmuahang_id
                        & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();

            if (ck_dh != null)
            {
                var tsld = (ck_dh.tong_sl_dat.GetValueOrDefault(0) - dh.sl_nhap.GetValueOrDefault(0)).Set0WhenlessThan0();
                ck_dh.tong_sl_dat = tsld;
                var slmn = ck_dh.sl_muonnhap.GetValueOrDefault(0);
                ck_dh.sl_muonnhap = slmn > tsld ? tsld : slmn;
                var sltn = ck_dh.sl_nhap.GetValueOrDefault(0);
                slmn = ck_dh.sl_muonnhap.GetValueOrDefault(0);
                ck_dh.sl_nhap = sltn > slmn ? slmn : sltn;
            }
        }
        return id_new;
    }

    //--cap nhat vao kho
    public string add_kho(EntityContext db, md_nhapkho_ncc_dh dh, md_nhapkho_ncc px, User_TK us)
    {
        string id_new = Helper.getNewId();
        var sp = db.md_kho_sanpham.Where(s => s.md_kho_id == px.kho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
        dh.sl_tonkho = sp == null ? 0 : sp.soluong.GetValueOrDefault(0);
        var add = sp == null;
        if (add)
        {
            sp = new md_kho_sanpham();
            sp.md_kho_sanpham_id = id_new;
            sp.soluong = dh.sl_nhap;
        }
        else
        {
            id_new = sp.md_kho_sanpham_id;
            sp.soluong = dh.sl_nhap + sp.soluong;
        }
        sp.md_kho_id = px.kho;
        sp.md_sanpham_id = dh.md_sanpham_id;

        sp.nguoitao = us.ad_user_id;
        sp.vaitrotao = us.ad_role_id;
        sp.bophantao = us.md_phongban_id;
        sp.value_nguoitao = us.ma_user;
        sp.value_vaitrotao = us.ten_role;
        sp.value_bophantao = us.ten_phongban;
        sp.nguoicapnhat = us.ad_user_id;
        sp.vaitrocapnhat = us.ad_role_id;
        sp.bophancapnhat = us.md_phongban_id;
        sp.value_nguoicapnhat = us.ma_user;
        sp.value_vaitrocapnhat = us.ten_role;
        sp.value_bophancapnhat = us.ten_phongban;
        sp.hoatdong = true;
        if (add)
        {
            sp.ngaytao = DateTime.Now;
            sp.ngaycapnhat = DateTime.Now;
            db.md_kho_sanpham.Add(sp);
        }
        else
        {
            sp.ngaycapnhat = DateTime.Now;
        }
        return id_new;
    }

    //--tao lich su nhap kho
    public string add_kho_ls(EntityContext db, md_nhapkho_ncc_dh dh, md_nhapkho_ncc px, User_TK us, c_donmuahang dmh = null)
    {
        string id_new = Helper.getNewId();
        string abc = "";
        if (dh.sl_nhap == 0)
        {
            abc = "abc";
        }
        else
        {
            var dvt = db.md_sanpham.Where(s => s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
            var giao = new md_kho_giaodich();
            giao.md_kho_giaodich_id = id_new;
            giao.md_kho_id = px.kho;
            giao.md_sanpham_id = dh.md_sanpham_id;
            giao.soluong_dichchuyen = dh.sl_nhap;
            giao.ngaychuyen = px.ngaychuyen;
            giao.kieuchuyen = Helper.NhapKho;
            giao.dongnhapxuat = px.sochungtu;
            giao.dongkiemkho = px.sochungtu;
            giao.dongvanchuyen = px.sochungtu;
            giao.dongsanxuat = px.sochungtu;
            giao.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id;
            giao.mota = dh.so_dmh;
            giao.donhang = dmh == null ? "" : dmh.donhang_thamchieu;

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
            // dh.sl_nhap = 0;
        }
        return abc;
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
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id = context.Request.Form["id"];
                var object_ = db.md_nhapkho_ncc.Where(p => p.md_nhapkho_ncc_id == id).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (!new string[] { "SOANTHAO", "DANHAN" }.Contains(object_.trangthai))
                {
                    msg = "Lỗi:Đã nhập kho";
                }
                else
                {
                    VNN_Function.SetFormValue(object_.nameof(s => s.trangthai), "VNN_notpost");
                    object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
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

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                foreach (var id_del_ in ids)
                {
                    var object_ = db.md_nhapkho_ncc.Where(p => p.md_nhapkho_ncc_id == id_del_).Take(1).FirstOrDefault();
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
                        VNN_Function.Write_log(context, ma_module, null, oper, "PNKNCC:" + object_.sochungtu, db);
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
                msg = @"true#Trả phiếu nhập kho đã chọn về ""Soạn Thảo"" thành công.";
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