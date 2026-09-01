<%@ WebHandler Language="C#" Class="JQGridMD_00_KHMVTModify" %>
using System;
using System.Web;
using System.Linq;
using DataAcess;
using System.Collections.Generic;
using Newtonsoft.Json;

public class JQGridMD_00_KHMVTModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public EntityContext db = new EntityContext();
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
            case "load_banggia":
                this.load_banggia(context);
                break;
            case "CA_01_Taodomuavattu":
                this.CA_01_Taodomuavattu(context);
                break;
            case "load_khmvt":
                this.load_khmvt(context);
                break;
            case "CA_01_TongHopLHMVT":
                this.CA_01_TongHopLHMVT(context);
                break;
            case "CA_01_CopyKehoach":
                this.CA_01_CopyKehoach(context);
                break;
            case "CA_01_TraVeSoanThaoKHMHHVT":
                this.CA_01_TraVeSoanThaoKHMHHVT(context);
                break;
            case "CA_01_LamMoiDuLieuXuatKho":
                this.CA_01_LamMoiDuLieuXuatKho(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_LamMoiDuLieuXuatKho(HttpContext context)
    {
        string idparent = context.Request.Form["id"];
        string msg = "";

        try
        {
            var khmvt = db.c_kehoachmuavt.FirstOrDefault(s => s.c_kehoachmuavt_id == idparent);
            if (khmvt.md_trangthai_id == Helper.HIEULUC)
            {
                msg = string.Format(@"Kế hoach này đã ""Hiệu Lực"".");
            }
            else
            {
                foreach (var khmvt_cdh in db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_id == idparent).ToList())
                {
                    var slxk = db.md_kho_giaodich.Where(s =>
                        s.ngaychuyen >= khmvt.tungay
                        & s.ngaychuyen <= khmvt.denngay
                        & s.kieuchuyen == Helper.XuatKho
                        & !s.dongnhapxuat.StartsWith("PVC")
                        & s.md_sanpham_id == khmvt_cdh.md_sanpham_id)
                        .ToList().Sum(s => s.soluong_dichchuyen.GetValueOrDefault(0));
                    khmvt_cdh.sl_xuatkho = slxk;
                }
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
        {

        }
        else
        {
            msg = $@"<div style=""color:red"">Lỗi:{msg}</div>";
        }

        context.Response.Write(msg);
    }

    public void CA_01_TraVeSoanThaoKHMHHVT(HttpContext context)
    {
        string msg = "";
        var msgErrs = new List<string>();
        try
        {
            var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            var khmvts = db.c_kehoachmuavt.Where(p => ids.Contains(p.c_kehoachmuavt_id)).ToList();
            if(khmvts.Count <= 0)
            {
                msgErrs.Add("Lỗi:Không tìm thấy đối tượng");
                goto EndEventHandler;
            }

            foreach (var object_ in khmvts)
            {
                if (object_.md_trangthai_id != Helper.DANHAN)
                {
                    msgErrs.Add($@"Lỗi: {object_.sochungtu} không ở trạng thái ""Đã xác nhận""");
                }
                else
                {
                    object_.md_trangthai_id = Helper.SOANTHAO;
                }
            }

            if (msgErrs.Count <= 0)
                db.SaveChanges();
        }
        catch (Exception ex)
        {
            msgErrs.Add(ex.ToString());
        }

    EndEventHandler:;

        if (msgErrs.Count > 0)
        {
            msgErrs = msgErrs.Select(x => $@"<div error style='color:red'>{x}</div>").ToList();
            msg = string.Join("", msgErrs);
        }

        context.Response.Write(msg);
    }

    public void CA_01_TongHopLHMVT(HttpContext context)
    {
        var msg = "";
        var ids = context.Request.Form["id"].removeAllSpaceOrTrimText(false).Split(',').ToList();
        try
        {
            var khmvts = db.c_kehoachmuavt.Where(s => ids.Contains(s.c_kehoachmuavt_id)).ToList();

            foreach (var khmvt in khmvts)
            {
                if (khmvt == null)
                {
                    msg = $@"Kế hoạch không tìm thấy";
                    goto EndEventHandler;
                }

                if (khmvt.md_trangthai_id != Helper.SOANTHAO)
                {
                    msg = $@"Kế hoạch ở trạng thái khác ""Soạn Thảo""";
                    goto EndEventHandler;
                }

                var slyeucau = db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_id == khmvt.c_kehoachmuavt_id).ToList().Sum(s => s.sl_duyet.GetValueOrDefault(0));
                if (slyeucau <= 0)
                {
                    msg = $@"Phải có ít nhất 1 dòng hàng có số lượng đề nghị lớn hơn 0";
                    goto EndEventHandler;
                }

                khmvt.md_trangthai_id = Helper.DANHAN;
            }

            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
            msg = $@"<div style='color:blue'>Xác nhận thành công</div>";
        else
            msg = $@"<div style='color:red' error>{msg}</div>";

        context.Response.Write(msg);
    }

    public void CA_01_CopyKehoach(HttpContext context)
    {
        var msg = "";
        var ids = context.Request.Form["id"].removeAllSpaceOrTrimText(false).Split(',').ToList();
        try
        {
            var khmvts = db.c_kehoachmuavt.Where(s => ids.Contains(s.c_kehoachmuavt_id)).ToList();
            foreach (var khmvt in khmvts)
            {
                if (khmvt == null)
                {
                    msg = $@"Kế hoạch không tìm thấy";
                    goto EndEventHandler;
                }

                if (khmvt.md_trangthai_id != Helper.DANHAN)
                {
                    msg = $@"Kế hoạch ở trạng thái khác ""Đã xác nhận""";
                    goto EndEventHandler;
                }

                var slduyet = db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_id == khmvt.c_kehoachmuavt_id).ToList().Sum(s => s.sl_duyet2.GetValueOrDefault(0));
                if (slduyet <= 0)
                {
                    msg = $@"Phải có ít nhất 1 dòng hàng có số lượng duyệt lớn hơn 0";
                    goto EndEventHandler;
                }

                khmvt.md_trangthai_id = Helper.HIEULUC;
            }

            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
            msg = $@"<div style='color:blue'>Hiệu lực thành công</div>";
        else
            msg = $@"<div style='color:red' error>{msg}</div>";

        context.Response.Write(msg);
    }

    public void load_khmvt(HttpContext context)
    {
        var item = new Dictionary<string, object>();
        string str = "<option value=''></option>";
        foreach (c_kehoachmuavt khmvt in db.c_kehoachmuavt.Where(s => s.hoatdong == true & s.md_trangthai_id == "HIEULUC" & s.sochungtu.Contains("DTMVT")).ToList())
        {
            var dongMuaHang = db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_id == khmvt.c_kehoachmuavt_id).Sum(s => s.sl_duyet2 - s.sl_conlai);
            if (dongMuaHang > 0)
            {
                str += "<option value='" + khmvt.c_kehoachmuavt_id + "'>" + khmvt.ten_kehoach + "</option>";
                //var dtkd = from a in db.md_banggia
                //           join b in db.md_phienbangia on a.md_banggia_id equals b.md_banggia_id
                //           join c in db.md_giasanpham on b.md_phienbangia_id equals c.md_phienbangia_id
                //           where c.md_sanpham_id = 
                //           select a.lienket_bg;
            }
        }

        string value = db.ad_selectoption.Where(s => s.ma_selectoption == "<%=SL_eAncop_donvitinhsanpham%>").Select(s => s.value_selectoption).FirstOrDefault();
        item["khmvt"] = str;
        item["donvitinh"] = value;
        context.Response.Write(JsonConvert.SerializeObject(item));
    }

    public void load_banggia(HttpContext context)
    {
        string msg = "";
        msg += "<option value=''></option>";
        foreach (md_banggia bg in db.md_banggia.Where(s => s.banggiaban != true).OrderBy(s => s.ten_banggia))
        {
            msg += "<option value='" + bg.md_banggia_id + "'>" + bg.ten_banggia + "</option>";
        }
        context.Response.Write(msg);
    }

    public void CA_01_Taodomuavattu(HttpContext context)
    {
        string msg = "", msg_success = "";
        string[] vnn = context.Request.Form["id"].Split(',');
        string[] arr_id = context.Request.Form["arr_id"].Split(',');
        string[] arr_ncc = context.Request.Form["arr_ncc"].Split(',');
        string[] arr_sl = context.Request.Form["arr_sl"].Split(',');
        string[] arr_dvt = context.Request.Form["arr_dvt"].Split(',');
        string c_donmuahang_id = "";

        //using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var lst = new List<khmvt_hh>();
                for (int i = 0; i < arr_id.Length; i++)
                {
                    string arr_id_ = arr_id[i];
                    string arr_dvt_ = arr_dvt[i];
                    var khmvt_cdh = db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_cdh_id == arr_id_).FirstOrDefault();
                    if (khmvt_cdh != null)
                    {
                        decimal sl_semua = -1;
                        decimal sl_thaythe = sl_semua, saiso = 0;
                        var sp = db.md_sanpham.Where(s => s.md_sanpham_id == khmvt_cdh.md_sanpham_id).FirstOrDefault();
                        var dvt = db.md_donvitinhsanpham_cddv.
                            Where(s => s.md_sanpham_id == sp.ma_sanpham & s.md_donvitinhsanpham_id == arr_dvt_).FirstOrDefault();
                        try { sl_semua = decimal.Parse(arr_sl[i]); } catch { }
                        if (dvt != null)
                        {
                            sl_thaythe = dvt.nhanvoi.GetValueOrDefault(0) * sl_semua;
                            saiso = dvt.chiacho.GetValueOrDefault(0) * sl_semua;
                            if (saiso > dvt.saiso_toida.GetValueOrDefault(9999999))
                                saiso = dvt.saiso_toida.GetValueOrDefault(9999999);
                        }
                        else if (arr_dvt_ == sp.md_donvitinhsanpham_id)
                        {
                            sl_thaythe = sl_semua;
                            saiso = 0;
                        }
                        else
                        {
                            string tendvt = db.md_donvitinhsanpham.Where(s => s.md_donvitinhsanpham_id == arr_dvt_).Select(s => s.ten_dvt).FirstOrDefault();
                            msg += string.Format(@"
                        <div style='color:red'>
                            Lỗi: Mã sản phẩm ""{0}"" không có đơn vị tính ""{1}""
                        </div>",
                                sp.ma_sanpham,
                                tendvt);
                        }

                        if (sl_semua > -1 & (sl_thaythe - saiso) <= (khmvt_cdh.sl_duyet2.Value - khmvt_cdh.sl_conlai.Value))
                        {
                            string arr_ncc_ = arr_ncc[i];
                            var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == arr_ncc_).FirstOrDefault();
                            if (dtkd != null)
                            {
                                khmvt_cdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
                                khmvt_hh khm = new khmvt_hh
                                {
                                    md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id,
                                    md_donvitinhsanpham_id = arr_dvt_,
                                    md_sanpham_id = sp.md_sanpham_id,
                                    sl_duyet = khmvt_cdh.sl_duyet2.Value,
                                    sl_conlai = khmvt_cdh.sl_conlai.Value,
                                    sl_semua = sl_semua,
                                    sl_semua2 = sl_thaythe,
                                    saiso = saiso,
                                    md_thue_sanpham_id = sp.md_thue_sanpham_id,
                                    daidien = dtkd.daidien,
                                    ma_sanpham = sp.ma_sanpham,
                                    ma_dtkd = dtkd.ma_dtkd,
                                    md_banggia_id = db.md_banggia.Where(s => s.lienket_bg == dtkd.ma_dtkd & s.tuychon == Helper.MUAVT & s.hoatdong == true).Select(s => s.md_banggia_id).ToList()
                                };
                                lst.Add(khm);
                            }
                            else if (sl_semua > 0)
                            {
                                msg += "<div style='color:red'>Lỗi:NCC \"" + arr_ncc[i] + "\" không tồn tại</div>";
                            }
                        }
                        else
                        {
                            string ten_dvt = db.md_donvitinhsanpham.Where(s => s.md_donvitinhsanpham_id == sp.md_donvitinhsanpham_id).Select(s => s.ten_dvt).FirstOrDefault();
                            string msgDT = "";
                            msgDT += @"chỉ có thể nhập số lượng tối đa là: ""{0}""";
                            msgDT += @"<br> + Số lượng đang muốn nhập: ""{1}"" ({2})";
                            msgDT += @"<br> + Độ sai lệch cho phép: ""{3}""";
                            msgDT = string.Format(msgDT,
                                (khmvt_cdh.sl_duyet2.Value - khmvt_cdh.sl_conlai.GetValueOrDefault(0)).DropTrailingZeros(),
                                sl_thaythe.DropTrailingZeros(),
                                ten_dvt,
                                saiso.DropTrailingZeros());
                            msg += string.Format(@"
                        <div style='color:red'>
                            Lỗi:Mặt hàng ""{0}"" {1}
                        </div>", sp.ma_sanpham, msgDT);
                        }
                    }
                }

                if (msg.Length <= 0)
                {
                    foreach (var khmvt in db.c_kehoachmuavt.Where(s => vnn.Contains(s.c_kehoachmuavt_id)).ToList())
                    {
                        //Hieu luc tung don
                        int countx = 0;
                        int m = db.c_donmuahang.Where(s => s.c_kehoachmuavt_id == khmvt.c_kehoachmuavt_id & s.md_trangthai_id == "SOANTHAO" & s.sctkehoach == khmvt.sochungtu).Count();
                        countx += m;
                        string nhacungung = "123";
                        int dem_khmvt_cdh = 0;

                        var c_kehoachmuavt_cdhs = lst.Where(s => s.sl_semua > 0).OrderBy(s => s.md_doitackinhdoanh_id).ToList();

                        string id_new = Helper.getNewId(), ma_iso_tiente = "";
                        foreach (var khmvt_cdh in c_kehoachmuavt_cdhs)
                        {

                            var check_gia = (from a in db.md_banggia
                                             join b in db.md_phienbangia on a.md_banggia_id equals b.md_banggia_id
                                             join c in db.md_giasanpham on b.md_phienbangia_id equals c.md_phienbangia_id
                                             where
                                             c.md_sanpham_id == khmvt_cdh.md_sanpham_id
                                             //& c.md_donvitinhsanpham_id == khmvt_cdh.md_donvitinhsanpham_id
                                             & khmvt_cdh.md_banggia_id.Contains(a.md_banggia_id)
                                             & b.hoatdong == true
                                             & b.trangthai == Helper.HIEULUC
                                             & b.ngay_hieuluc <= DateTime.Now
                                             select new
                                             {
                                                 a.md_banggia_id,
                                                 c.gia,
                                                 a.md_dongtien_id,
                                                 c.md_phienbangia_id,
                                                 b.ngay_hieuluc
                                             }
                                            ).OrderByDescending(s => s.ngay_hieuluc).FirstOrDefault();

                            if (khmvt_cdh.sl_duyet == khmvt_cdh.sl_conlai)
                            {
                                //msg += "<div style='color:red'>Lỗi:Mã \""+ khmvt_cdh.ma_sanpham +"\" đã mua đủ số lượng.</div>";
                            }
                            else if (check_gia == null & khmvt_cdh.ma_dtkd != "MUA NGOAI")
                            {
                                msg += "<div style='color:red'>Lỗi:NCC \"" + khmvt_cdh.ma_dtkd + "\" không có giá của HH/VT \"" + khmvt_cdh.ma_sanpham + "\"</div>";
                            }
                            else
                            {
                                string md_dongtien_id = check_gia == null ? "" : check_gia.md_dongtien_id;
                                string md_phienbangia_id = check_gia == null ? "" : check_gia.md_phienbangia_id;
                                decimal? dongiamua = check_gia == null ? 0 : check_gia.gia;
                                var dmh = db.c_donmuahang.FirstOrDefault(s => s.c_donmuahang_id == id_new);
                                if (nhacungung != khmvt_cdh.md_doitackinhdoanh_id & dmh == null)
                                {
                                    nhacungung = khmvt_cdh.md_doitackinhdoanh_id;
                                    string sochungtu = VNN_VariablePublic.sochungtu(db, "DMH", 1);
                                    msg_success += "<div style='color:blue'>Tạo đơn mua vật tư \"" + sochungtu + "\" thành công.</div>";
                                    dem_khmvt_cdh = 1;
                                    id_new = Helper.getNewId();
                                    c_donmuahang_id += id_new + ",";
                                    ma_iso_tiente = db.md_dongtien.Where(s => s.md_dongtien_id == md_dongtien_id).Select(s => s.ma_iso).FirstOrDefault();
                                    dmh = new c_donmuahang();
                                    dmh.c_donmuahang_id = id_new;
                                    dmh.phieunhapkho = " ";
                                    dmh.c_kehoachmuavt_id = khmvt.c_kehoachmuavt_id;
                                    dmh.md_trangthai_id = "SOANTHAO";
                                    dmh.sochungtu = sochungtu;
                                    dmh.so_donmuahang = sochungtu;
                                    dmh.sctkehoach = khmvt.sochungtu;
                                    dmh.donhang_thamchieu = "";
                                    dmh.huongdan_lamhang = "";
                                    dmh.ngaydonhang = DateTime.Now;
                                    dmh.ngaygiaohang = DateTime.Now.AddDays(4);
                                    //dmh.diadiem_giaohang = db.md_kho.Where(s=>s.ma_kho == Helper.KHOVT).FirstOrDefault().md_kho_id;
                                    dmh.hinhthucthanhtoan = db.md_hinhthucthanhtoan.OrderBy(s => s.sapxep).FirstOrDefault().ten;
                                    dmh.md_doitackinhdoanh_id = khmvt_cdh.md_doitackinhdoanh_id;
                                    dmh.nguoilienhe = khmvt_cdh.daidien;
                                    dmh.md_banggia_id = check_gia == null ? "" : check_gia.md_banggia_id;
                                    //
                                    dmh.md_phienbangia_id = md_phienbangia_id;
                                    dmh.md_dongtien_id = md_dongtien_id;
                                    dmh.tong_tienhang = 0;
                                    dmh.tong_tatca = 0;
                                    dmh.chu_tong_tienhang = "Không " + ma_iso_tiente;
                                    dmh.chu_tong_tatca = "Không " + ma_iso_tiente;
                                    dmh.nguoitao = userTK.ad_user_id;
                                    dmh.vaitrotao = userTK.ad_role_id;
                                    dmh.bophantao = userTK.md_phongban_id;
                                    dmh.value_nguoitao = userTK.ma_user;
                                    dmh.value_vaitrotao = userTK.ten_role;
                                    dmh.value_bophantao = userTK.ten_phongban;

                                    dmh.nguoicapnhat = userTK.ad_user_id;
                                    dmh.vaitrocapnhat = userTK.ad_role_id;
                                    dmh.bophancapnhat = userTK.md_phongban_id;
                                    dmh.value_nguoicapnhat = userTK.ma_user;
                                    dmh.value_vaitrocapnhat = userTK.ten_role;
                                    dmh.value_bophancapnhat = userTK.ten_phongban;

                                    dmh.ngaytao = DateTime.Now;
                                    dmh.ngaycapnhat = DateTime.Now;
                                    dmh.mota = "";
                                    dmh.hoatdong = true;

                                    db.c_donmuahang.Add(dmh);
                                    if (khmvt.c_donmuavattu_id == " ")
                                        khmvt.c_donmuavattu_id = sochungtu;
                                    else
                                        khmvt.c_donmuavattu_id += "," + sochungtu;
                                }

                                var thue_sp = db.md_thue_sanpham.Where(s => s.md_thue_sanpham_id == khmvt_cdh.md_thue_sanpham_id).FirstOrDefault();
                                string md_thue_sanpham_id = "";
                                decimal gt_thue = 0;
                                if (thue_sp != null)
                                {
                                    md_thue_sanpham_id = thue_sp.md_thue_sanpham_id;
                                    gt_thue = thue_sp.giatri.Value;
                                }

                                var dmvt_cdmh = new c_donmuahang_cdmh();
                                dmvt_cdmh.c_donmuahang_cdmh_id = Helper.getNewId();
                                dmvt_cdmh.c_donmuahang_id = id_new;
                                dmvt_cdmh.md_sanpham_id = khmvt_cdh.md_sanpham_id;
                                dmvt_cdmh.md_donvitinhsanpham_id = khmvt_cdh.md_donvitinhsanpham_id;
                                dmvt_cdmh.sl_dadat = khmvt_cdh.sl_semua;
                                dmvt_cdmh.sl_dadat2 = khmvt_cdh.sl_semua2;
                                dmvt_cdmh.sl_hanngach = 0;
                                dmvt_cdmh.saiso = khmvt_cdh.saiso;
                                dmvt_cdmh.dongiamua = dongiamua;
                                dmvt_cdmh.giachuan = dongiamua;

                                dmvt_cdmh.thue = md_thue_sanpham_id;
                                dmvt_cdmh.thanhtien = Math.Floor(dongiamua.GetValueOrDefault(0) * khmvt_cdh.sl_semua);
                                dmvt_cdmh.thanhtienThue = dmvt_cdmh.thanhtien.GetValueOrDefault(0) * gt_thue / 100;
                                dmvt_cdmh.nguoitao = userTK.ad_user_id;
                                dmvt_cdmh.vaitrotao = userTK.ad_role_id;
                                dmvt_cdmh.bophantao = userTK.md_phongban_id;
                                dmvt_cdmh.value_nguoitao = userTK.ma_user;
                                dmvt_cdmh.value_vaitrotao = userTK.ten_role;
                                dmvt_cdmh.value_bophantao = userTK.ten_phongban;
                                dmvt_cdmh.md_donvitinhsanpham_id = khmvt_cdh.md_donvitinhsanpham_id;

                                dmvt_cdmh.nguoicapnhat = userTK.ad_user_id;
                                dmvt_cdmh.vaitrocapnhat = userTK.ad_role_id;
                                dmvt_cdmh.bophancapnhat = userTK.md_phongban_id;
                                dmvt_cdmh.value_nguoicapnhat = userTK.ma_user;
                                dmvt_cdmh.value_vaitrocapnhat = userTK.ten_role;
                                dmvt_cdmh.value_bophancapnhat = userTK.ten_phongban;

                                dmvt_cdmh.ngaytao = DateTime.Now;
                                dmvt_cdmh.ngaycapnhat = DateTime.Now;
                                dmvt_cdmh.mota = "";
                                dmvt_cdmh.hoatdong = true;
                                db.c_donmuahang_cdmh.Add(dmvt_cdmh);

                                string id_news = Helper.getNewId();
                                var thue = new c_donmuahang_thue();
                                //--
                                thue.tong_tien_thue = dmvt_cdmh.thanhtien * gt_thue / 100;
                                thue.tong_tien_chiu_thue = dmvt_cdmh.thanhtien + thue.tong_tien_thue;
                                //--
                                thue.c_donmuahang_thue_id = id_news;
                                thue.c_donmuahang_id = dmvt_cdmh.c_donmuahang_id;
                                thue.md_thue_sanpham_id = md_thue_sanpham_id;
                                thue.doanhnghiep = "";
                                thue.tochuc = "";

                                thue.nguoitao = userTK.ad_user_id;
                                thue.vaitrotao = userTK.ad_role_id;
                                thue.bophantao = userTK.md_phongban_id;
                                thue.value_nguoitao = userTK.ma_user;
                                thue.value_vaitrotao = userTK.ten_role;
                                thue.value_bophantao = userTK.ten_phongban;

                                thue.nguoicapnhat = userTK.ad_user_id;
                                thue.vaitrocapnhat = userTK.ad_role_id;
                                thue.bophancapnhat = userTK.md_phongban_id;
                                thue.value_nguoicapnhat = userTK.ma_user;
                                thue.value_vaitrocapnhat = userTK.ten_role;
                                thue.value_bophancapnhat = userTK.ten_phongban;

                                thue.ngaytao = DateTime.Now;
                                thue.ngaycapnhat = DateTime.Now;
                                thue.mota = "";
                                thue.hoatdong = true;

                                if (dmvt_cdmh.thue != "")
                                {
                                    db.c_donmuahang_thue.Add(thue);
                                }
                            }
                        }

                        if (dem_khmvt_cdh == 0 & msg == "")
                            msg += "<div style='color:red'>Lỗi: Dòng \"" + khmvt.sochungtu + "\" phải có ít nhất 1 mã hàng có số lượng duyệt lớn hơn 0<br>" +
                            " → Có thể kế hoạch đã được mua hết.</div>";
                    }
                }

                if (msg == "")
                {
                    //db.SaveChanges();
                    vnn = c_donmuahang_id.Split(',');
                    //Start cap nhat Thue trong don mua hang
                    foreach (var dmh in db.c_donmuahang.Local.Where(s => vnn.Contains(s.c_donmuahang_id)).ToList())
                    {
                        var c_donmuahang_thues = db.c_donmuahang_thue.Local.Where(s => s.c_donmuahang_id == dmh.c_donmuahang_id);
                        var id_notdel = new List<string>();
                        foreach (var thue_donhang_list in c_donmuahang_thues.Select(s => new { s.md_thue_sanpham_id, s.c_donmuahang_id }).Distinct().ToList())
                        {
                            var c_donmuahang_thueschild = c_donmuahang_thues.Where(s => s.md_thue_sanpham_id == thue_donhang_list.md_thue_sanpham_id);
                            var dmh_thue = c_donmuahang_thueschild.Take(1).FirstOrDefault();

                            dmh_thue.tong_tien_thue = c_donmuahang_thueschild.Select(s => s.tong_tien_thue).Sum();
                            dmh_thue.tong_tien_chiu_thue = c_donmuahang_thueschild.Select(s => s.tong_tien_chiu_thue).Sum();
                            id_notdel.Add(dmh_thue.c_donmuahang_thue_id);
                        }

                        foreach (var dong_thue in c_donmuahang_thues.Where(s => !id_notdel.Contains(s.c_donmuahang_thue_id)).ToList())
                        {
                            db.c_donmuahang_thue.Remove(dong_thue);
                        }
                    }

                    //db.SaveChanges();
                    //End cap nhat Thue trong don mua hang

                    //Start cap nhat don mua hang
                    foreach (var dmh in db.c_donmuahang.Local.Where(s => vnn.Contains(s.c_donmuahang_id)).ToList())
                    {
                        string ma_iso_tiente = db.md_dongtien.Where(s => s.md_dongtien_id == dmh.md_dongtien_id).Select(s => s.ma_iso).FirstOrDefault();
                        decimal tong_tienhang = decimal.Parse("0.00");
                        decimal tong_tienthue = decimal.Parse("0.00");
                        decimal tong_tatca = 0;
                        try
                        {
                            var cdmhs = db.c_donmuahang_cdmh.Local.Where(s => s.c_donmuahang_id == dmh.c_donmuahang_id).ToList();
                            tong_tienhang = cdmhs.Select(s => s.thanhtien.GetValueOrDefault(0)).Sum();
                            tong_tienthue = cdmhs.Select(s => s.thanhtienThue.GetValueOrDefault(0)).Sum();
                            tong_tatca = tong_tienhang + Math.Floor(tong_tienthue);
                        }
                        catch { }

                        var tth = VNN_ConvertMoney.convert((double)tong_tienhang, ma_iso_tiente).FirstOrDefault();
                        var ttc = VNN_ConvertMoney.convert((double)tong_tatca, ma_iso_tiente).FirstOrDefault();
                        dmh.tong_tienhang = (decimal)tth.Value;
                        dmh.tong_tatca = (decimal)ttc.Value;
                        dmh.chu_tong_tienhang = tth.Key;
                        dmh.chu_tong_tatca = ttc.Key;
                    }
                    db.SaveChanges();
                    //End cap nhat don mua hang
                }
            }
            catch (Exception ex)
            {
                msg = "<div style='color:red'>Lỗi: " + ex.Message + "</div>";
            }

            if (msg.Length <= 0)
            {
                msg = msg_success;
                //transaction.Commit();
            }
            else
            {
                //transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id = context.Request.Form["id"];
                if (msg.Length <= 0)
                {
                    var object_ = new c_kehoachmuavt();
                    object_.c_kehoachmuavt_id = id_new;
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
                    object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    db.c_kehoachmuavt.Add(object_);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#Thêm mới thành công#{0}", id_new);
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

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string md_trangthai_id = context.Request.Form["md_trangthai_id"];
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id = context.Request.Form["id"];
                var ngaykehoach = VNN_Config.setDateTime(context.Request.Form["ngaykehoach"]);
                var tungay = VNN_Config.setDateTime(context.Request.Form["tungay"]);
                var denngay = VNN_Config.setDateTime(context.Request.Form["denngay"]);
                var object_ = db.c_kehoachmuavt.Where(p => p.c_kehoachmuavt_id == id).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg = @"Lỗi:Không tìm thấy đối tượng cần sửa.";
                }
                else if (md_trangthai_id == "SOANTHAO" & object_.md_trangthai_id == "HIEULUC")
                {
                    msg = @"Lỗi:Không thể sửa trạng thái ""Hiệu lực"" thành ""Soạn Thảo"".";
                }
                else if (md_trangthai_id == "KETTHUC" & object_.md_trangthai_id == "SOANTHAO")
                {
                    msg = @"Lỗi:Không thể sửa trạng thái ""Soạn Thảo"" thành ""Kết Thúc"".";
                }
                else if (ngaykehoach == DateTime.MinValue | ngaykehoach == DateTime.MinValue.AddDays(1))
                {
                    msg = @"Lỗi:""Ngày kế hoạch"" có giá trị sai.";
                }
                else if (tungay == DateTime.MinValue | tungay == DateTime.MinValue.AddDays(1))
                {
                    msg = @"Lỗi:""Từ ngày"" có giá trị sai.";
                }
                else if (denngay == DateTime.MinValue | denngay == DateTime.MinValue.AddDays(1))
                {
                    msg = @"Lỗi:""Đến ngày"" có giá trị sai.";
                }

                if (msg.Length <= 0)
                {
                    object_.md_trangthai_id = md_trangthai_id;
                    object_.ngaykehoach = ngaykehoach;
                    object_.tungay = tungay;
                    object_.denngay = denngay;
                    object_.bophanyc = context.Request.Form["bophanyc"];
                    object_.bophanyc_value = db.ad_department.Where(s => s.md_phongban_id == object_.bophanyc).Select(s => s.ten_phongban).FirstOrDefault();

                    var cdvts = db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_id == object_.c_kehoachmuavt_id).ToList();
                    foreach (var cdvt in cdvts)
                    {
                        var slxk = db.md_kho_giaodich.Where(s =>
                        s.ngaychuyen >= object_.tungay
                        & s.ngaychuyen <= object_.denngay
                        & s.kieuchuyen == Helper.XuatKho
                        & !s.dongnhapxuat.StartsWith("PVC")
                        & s.md_sanpham_id == cdvt.md_sanpham_id)
                        .ToList().Sum(s => s.soluong_dichchuyen.GetValueOrDefault(0));
                        cdvt.sl_xuatkho = slxk;
                    }
                    db.SaveChanges();

                    //if (md_trangthai_id == "HIEULUC")
                    //{
                    //    var khohangVT = from a in db.md_kho
                    //                    join b in db.md_kho_sanpham on a.md_kho_id equals b.md_kho_id
                    //                    join c in db.md_sanpham on b.md_sanpham_id equals c.md_sanpham_id
                    //                    where a.vattu == true
                    //                    select new
                    //                    {
                    //                        a.md_kho_id,
                    //                        a.ten_kho,
                    //                        c.ma_sanpham,
                    //                        b.md_sanpham_id,
                    //                        c.md_donvitinhsanpham_id,
                    //                        c.mota_tiengviet,
                    //                        c.mota_tienganh,
                    //                        b.soluong,
                    //                        c.mota,
                    //                        c.sanpham
                    //                    };

                    //    var ncvtIds = db.c_yeucaumuavt.Where(s => s.c_kehoachmuavt_id == object_.sochungtu).Select(s => s.c_nhucauvattu_id).ToList();
                    //    var kh_ddhpx = db.c_nhucauvattu_dhpx.Where(s => ncvtIds.Contains(s.c_nhucauvattu_id)).Select(s=>s.md_dondathangphanxuong_id).ToList();

                    //    foreach (var dkh in db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_id == object_.c_kehoachmuavt_id).ToList())
                    //    {
                    //        var tht = khohangVT.Where(s =>
                    //                        s.md_sanpham_id == dkh.md_sanpham_id
                    //                    )
                    //                    .ToList()
                    //                    .Select(s =>
                    //                    (s.soluong -
                    //                    db.md_kho_giucho.Where(t =>
                    //                                        t.md_sanpham_id == s.md_sanpham_id
                    //                                        & t.md_kho_id == s.md_kho_id
                    //                                    )
                    //                                    .Select(t => t.soluong_giucho)
                    //                                    .Sum().GetValueOrDefault(0)).GetValueOrDefault(0).Set0WhenlessThan0()
                    //                    ).Sum();

                    //        if (dkh.sl_tonkho != tht.GetValueOrDefault(0))
                    //        {
                    //            msg += string.Format(@"
                    //            <div style='color:red'>
                    //                Lỗi: dòng ""{0}"" đã có sự thay đổi số lượng tồn kho.
                    //            </div>", object_.ten_kehoach);
                    //            break;
                    //        }

                    //        foreach (var dklht in db.c_kehoachmuavt_dklht.Where(s =>
                    //             s.c_kehoachmuavt_id == object_.c_kehoachmuavt_id
                    //             & s.md_sanpham_id == dkh.md_sanpham_id).ToList())
                    //        {
                    //            if (dklht.sl_layton > 0)
                    //            {
                    //                var giucho = new md_kho_giucho();
                    //                giucho.md_kho_giucho_id = Helper.getNewId();
                    //                giucho.md_kho_id = dklht.md_kho_id;
                    //                giucho.md_sanpham_id = dklht.md_sanpham_id;
                    //                giucho.md_donvitinhsanpham_id = dklht.md_donvitinhsanpham_id;
                    //                giucho.soluong_giucho = dklht.sl_layton;
                    //                giucho.sctlienquan = object_.sochungtu;
                    //                giucho.ngaygiu = DateTime.Now;
                    //                giucho.nguoitao = userTK.ad_user_id;
                    //                giucho.nguoicapnhat = userTK.ad_user_id;
                    //                giucho.ngaytao = DateTime.Now;
                    //                giucho.ngaycapnhat = DateTime.Now;
                    //                giucho.bophantao = userTK.md_phongban_id;
                    //                giucho.bophancapnhat = userTK.md_phongban_id;
                    //                giucho.vaitrotao = userTK.ad_role_id;
                    //                giucho.vaitrocapnhat = userTK.ad_role_id;
                    //                giucho.hoatdong = true;
                    //                giucho.mota = "";
                    //                giucho.lydo = "";
                    //                db.md_kho_giucho.Add(giucho);
                    //                db.SaveChanges();
                    //            }
                    //        }

                    //        decimal sl_duyet = dkh.sl_duyet.GetValueOrDefault(0) + dkh.sl_layton.GetValueOrDefault(0);
                    //        foreach (var dNCVT in db.c_kehoachdathang_dhcpx_vattu.Where(s =>
                    //            kh_ddhpx.Contains(s.c_kehoachdathang_dhcpx_id)
                    //            & s.md_sanpham_id == dkh.md_sanpham_id).ToList())
                    //        {
                    //            var sltd = dNCVT.soluong.GetValueOrDefault(0) - dNCVT.sl_giamhanngach.GetValueOrDefault(0) - dNCVT.sl_duyetmua.GetValueOrDefault(0);
                    //            dNCVT.sl_duyetmua = sl_duyet > sltd ? sltd : sl_duyet;
                    //            dNCVT.sl_giamhanngach = dNCVT.sl_giamhanngach.GetValueOrDefault(0);
                    //            sl_duyet = (sl_duyet - sltd).Set0WhenlessThan0().GetValueOrDefault(0);
                    //        }
                    //        db.SaveChanges();
                    //    }

                    //    if (msg.Length <= 0)
                    //    {
                    //        int count = db.c_kehoachmuavt_cdh.
                    //                Where(s => s.c_kehoachmuavt_id == object_.c_kehoachmuavt_id & s.sl_duyet2 == null).Count();
                    //        if (count > 0)
                    //            msg = @"Lỗi:Bạn chưa điều chỉnh số lượng duyệt (Nếu không mua hãy cho số lượng duyệt = 0).";
                    //    }

                    //    if (msg.Length <= 0)
                    //    {
                    //        var khogcs = db.md_kho_giucho.Where(s => s.sctlienquan == object_.sochungtu).ToList();
                    //        foreach (var khoId in khogcs.Select(s => s.md_kho_id).Distinct()) {
                    //            string sochungtu = VNN_VariablePublic.sochungtu(db, "PXKNB", 1);
                    //            var pxk = new md_xuatkhonb();
                    //            pxk.md_xuatkhonb_id = Helper.getNewId();
                    //            pxk.trangthai = "SOANTHAO";
                    //            pxk.bosung = 3;
                    //            pxk.sochungtu = sochungtu;
                    //            pxk.ngaychuyen = DateTime.Now;
                    //            pxk.ngaytao = DateTime.Now;
                    //            pxk.ngaycapnhat = DateTime.Now;
                    //            pxk.tukho = khoId;
                    //            pxk.kh_ddhpx = string.Join(",", kh_ddhpx);
                    //            db.md_xuatkhonb.Add(pxk);
                    //            if (object_.phieuhangton != null & object_.phieuhangton != "" & object_.phieuhangton != " ")
                    //            {
                    //                object_.phieuhangton += "<br>" + sochungtu;
                    //            }
                    //            else
                    //                object_.phieuhangton = sochungtu;

                    //            db.SaveChanges();

                    //            foreach (var giucho in khogcs.Where(s=>s.md_kho_id == khoId))
                    //            {
                    //                giucho.sctlienquan = pxk.sochungtu;
                    //                var cdvc = new md_xuatkhonb_cdh
                    //                {
                    //                    md_xuatkhonb_cdh_id = Helper.getNewId(),
                    //                    md_xuatkhonb_id = pxk.md_xuatkhonb_id,
                    //                    md_sanpham_id = giucho.md_sanpham_id,
                    //                    md_donvitinhsanpham_id = giucho.md_donvitinhsanpham_id,
                    //                    tong_sl_xuat = giucho.soluong_giucho,
                    //                    md_donvitinhsanpham_id2 = giucho.md_donvitinhsanpham_id,
                    //                    sl_daxuat = 0,
                    //                    saiso = 0,
                    //                    sl_xuat = giucho.soluong_giucho,
                    //                    sl_xuat2 = giucho.soluong_giucho,
                    //                    nguoitao = userTK.ad_user_id,
                    //                    vaitrotao = userTK.ad_role_id,
                    //                    bophantao = userTK.md_phongban_id,
                    //                    value_nguoitao = userTK.ma_user,
                    //                    value_vaitrotao = userTK.ten_role,
                    //                    value_bophantao = userTK.ten_phongban,
                    //                    nguoicapnhat = userTK.ad_user_id,
                    //                    vaitrocapnhat = userTK.ad_role_id,
                    //                    bophancapnhat = userTK.md_phongban_id,
                    //                    value_nguoicapnhat = userTK.ma_user,
                    //                    value_vaitrocapnhat = userTK.ten_role,
                    //                    value_bophancapnhat = userTK.ten_phongban,
                    //                    ngaytao = DateTime.Now,
                    //                    ngaycapnhat = DateTime.Now,
                    //                    hoatdong = true
                    //                };
                    //                db.md_xuatkhonb_cdh.Add(cdvc);
                    //            }
                    //            db.SaveChanges();
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = "true#Cập nhật thành công.";
                transaction.Commit();
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
                    var object_ = db.c_kehoachmuavt.Where(p => p.c_kehoachmuavt_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else if (!string.IsNullOrWhiteSpace(object_.c_donmuavattu_id))
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Đã tạo đơn mua vật tư.", object_.sochungtu);
                    }
                    else if (object_.md_trangthai_id != "SOANTHAO")
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Đã hiệu lực hoặc kết thúc.", object_.sochungtu);
                    }
                    else
                    {
                        string sct = object_.sochungtu;
                        foreach (var ycmvt in db.c_yeucaumuavt.Where(s => s.c_kehoachmuavt_id.Contains(sct)).ToList())
                        {
                            if (ycmvt != null)
                            {
                                ycmvt.c_kehoachmuavt_id = ycmvt.c_kehoachmuavt_id.Replace("," + sct, "").Replace(sct + ",", "").Replace(sct, "");
                                if (ycmvt.c_kehoachmuavt_id == "")
                                {
                                    ycmvt.c_kehoachmuavt_id = " ";
                                    ycmvt.md_trangthai_id = "SOANTHAO";
                                }

                                foreach (var ycmvt_cdh in db.c_yeucaumuavt_cdh.Where(s => s.c_yeucaumuavt_id == ycmvt.c_yeucaumuavt_id
                                 & s.sapxep == object_.c_kehoachmuavt_id))
                                {
                                    ycmvt_cdh.sapxep = null;
                                }
                            }
                        }

                        VNN_Function.Write_log(context, ma_module, null, oper, "KHMVT:" + object_.sochungtu, db);
                        db.c_kehoachmuavt.Remove(object_);
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
                msg = string.Format(@"true#Xóa kế hoạch mua mua vật tư đã chọn thành công");
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

public class khmvt_hh
{
    public string md_doitackinhdoanh_id;
    public string md_sanpham_id;
    public decimal sl_duyet;
    public decimal sl_conlai;
    public decimal sl_semua;
    public decimal sl_semua2;
    public decimal saiso;
    public string md_thue_sanpham_id;
    public string daidien;
    public string ma_sanpham;
    public string ma_dtkd;
    public List<string> md_banggia_id;
    public string md_donvitinhsanpham_id;
}
