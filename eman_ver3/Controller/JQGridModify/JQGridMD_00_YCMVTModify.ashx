<%@ WebHandler Language="C#" Class="JQGridMD_00_YCMVTModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
using System.Collections.Generic;

public class JQGridMD_00_YCMVTModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_TaoKeHoachmuaVT":
                this.CA_01_TaoKeHoachmuaVT(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_TaoKeHoachmuaVT(HttpContext context)
    {
        string[] vnn = context.Request.Form["id"].Split(',');
        string msg = "", msg_success = "";
        string sochungtu = "goc", id_new = Helper.getNewId();
        string ten_kehoach = context.Request.Form["ten_kh"].removeAllSpaceOrTrimText(true);
        string ngaylapStr = context.Request.Form["ngaylap"];
        string ngaycanStr = context.Request.Form["ngaycan"];
        DateTime? tungay = null;
        DateTime? denngay = null;

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var ycmvts = db.c_yeucaumuavt.Where(s => vnn.Contains(s.c_yeucaumuavt_id)).ToList();
                var ycmvtsTheoBom = ycmvts.Where(s => !string.IsNullOrEmpty(s.c_nhucauvattu_id)).ToList();
                var ycmvtsNgoaiBom = ycmvts.Where(s => string.IsNullOrEmpty(s.c_nhucauvattu_id)).ToList();
                if (ycmvtsTheoBom.Count > 0 & ycmvtsNgoaiBom.Count > 0)
                {
                    msg = $@"Các yêu cầu chỉ được chọn 1 trong 2 tiêu chí: ""theo BOM"" hoặc ""ngoài BOM""";
                    goto EndEventHandler;
                }

                var ngaylap = VNN_Config.setDateTime(ngaylapStr);
                if (!ngaylap.IsDate())
                {
                    msg = $@"Ngày lập có giá trị sai";
                    goto EndEventHandler;
                }


                var ngaycan = VNN_Config.setDateTime(ngaycanStr);
                if (!ngaycan.IsDate())
                {
                    if (string.IsNullOrWhiteSpace(ngaycanStr))
                    {
                        ngaycan = ycmvts.Min(s => s.ngaycan.Value);
                    }
                    else
                    {
                        msg = $@"Ngày cần có giá trị sai";
                        goto EndEventHandler;
                    }
                }

                var exist = db.c_kehoachmuavt.Where(s => s.ten_kehoach == ten_kehoach).Count() > 0;
                if (exist)
                {
                    msg = $@"Tên kế hoạch đã tồn tại";
                    goto EndEventHandler;
                }

                foreach (var ycmvt in ycmvts)
                {
                    if (ycmvt.ngaycan <= tungay | tungay == null)
                        tungay = ycmvt.ngaycan;

                    if (ycmvt.ngaycan >= denngay | denngay == null)
                        denngay = ycmvt.ngaycan;

                    if (db.c_yeucaumuavt_cdh.Where(s => s.sapxep == null & s.c_yeucaumuavt_id == ycmvt.c_yeucaumuavt_id).Count() <= 0)
                    {
                        msg = string.Format(@"Lỗi: Dòng ""{0}"" không thể tạo thêm kế hoạch mua vật tư.", ycmvt.sochungtu);
                        break;
                    }
                    else if (!string.IsNullOrEmpty(ycmvt.c_kehoachmuavt_id) & ycmvt.c_kehoachmuavt_id != " ")
                    {
                        msg = string.Format(@"Lỗi: Dòng ""{0}"" đã tạo kế hoạch mua vật tư.", ycmvt.sochungtu);
                        break;
                    }
                    else
                    {
                        ycmvt.c_kehoachmuavt_id = sochungtu;
                        ycmvt.khmvt_name = ten_kehoach;
                    }
                }

                if (msg.Length <= 0)
                {
                    var khmvt = new c_kehoachmuavt
                    {
                        c_kehoachmuavt_id = id_new,
                        c_donmuavattu_id = " ",
                        md_trangthai_id = Helper.SOANTHAO,
                        sochungtu = sochungtu,
                        ten_kehoach = ten_kehoach,
                        ngaykehoach = ngaylap,
                        ngaycan = ngaycan,
                        tungay = DateTime.Now.AddMonths(-3),
                        denngay = DateTime.Now,
                        tungayNCVT = ycmvts.Min(s=>s.tungay),
                        denngayNCVT = ycmvts.Max(s=>s.denngay),
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
                    db.c_kehoachmuavt.Add(khmvt);
                    db.SaveChanges();

                    foreach (var ycmvt_cdh in db.c_yeucaumuavt_cdh.Where(s => vnn.Contains(s.c_yeucaumuavt_id) & s.sapxep == null).ToList())
                    {
                        var sp = db.md_sanpham.Where(s => s.md_sanpham_id == ycmvt_cdh.md_sanpham_id).Take(1).FirstOrDefault();
                        var khmvt_cdh_chk = db.c_kehoachmuavt_cdh.Where(s => s.md_sanpham_id == ycmvt_cdh.md_sanpham_id &
                        s.c_kehoachmuavt_id == khmvt.c_kehoachmuavt_id).Take(1).FirstOrDefault();
                        c_kehoachmuavt_cdh khmvt_cdh = khmvt_cdh_chk;
                        if (khmvt_cdh_chk == null)
                        {
                            khmvt_cdh = new c_kehoachmuavt_cdh();
                            khmvt_cdh.c_kehoachmuavt_cdh_id = Helper.getNewId();
                            khmvt_cdh.c_kehoachmuavt_id = khmvt.c_kehoachmuavt_id;
                            khmvt_cdh.md_sanpham_id = ycmvt_cdh.md_sanpham_id;
                            khmvt_cdh.md_donvitinhsanpham_id = ycmvt_cdh.md_donvitinhsanpham_id;
                            string nhacungung = db.md_sanpham.Where(s => s.md_sanpham_id == ycmvt_cdh.md_sanpham_id).Select(s => s.nhacungung).FirstOrDefault();
                            if (string.IsNullOrEmpty(nhacungung))
                            {
                                nhacungung = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == "MUA NGOAI").Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();
                            }
                            khmvt_cdh.md_doitackinhdoanh_id = nhacungung;
                            khmvt_cdh.ngayphaico = tungay;
                            khmvt_cdh.sl_can = ycmvt_cdh.soluong_yeucau;
                            khmvt_cdh.sl_duyet = null;
                            var tonkho = (from a in db.md_kho_sanpham
                                          join b in db.md_kho on a.md_kho_id equals b.md_kho_id
                                          where b.vattu == true & a.md_sanpham_id == ycmvt_cdh.md_sanpham_id
                                          select new { a.md_sanpham_id, a.md_kho_id, a.soluong }).ToList();
                            decimal sl_tonkho = 0,
                                    sl_tonkhotoithieu = sp.sl_tonkho_toithieu.GetValueOrDefault(0),
                                    sl_khmvtCan = khmvt_cdh.sl_can.GetValueOrDefault(0);

                            if (tonkho.Count > 0)
                            {
                                foreach (var tonkho_ in tonkho)
                                {
                                    decimal sl_tonkho_ = tonkho_.soluong.GetValueOrDefault(0) - db.md_kho_giucho.Where(s =>
                                                    s.md_sanpham_id == tonkho_.md_sanpham_id &
                                                    s.md_kho_id == tonkho_.md_kho_id
                                                    ).Select(s => s.soluong_giucho).Sum()
                                                    .GetValueOrDefault(0).Set0WhenlessThan0().GetValueOrDefault(0);

                                    sl_tonkho += sl_tonkho_;
                                }
                            }
                            db.SaveChanges();


                            khmvt_cdh.sl_tonkho = sl_tonkho;
                            //khmvt_cdh.sl_layton = sl_tonkho > khmvt_cdh.sl_can ? khmvt_cdh.sl_can : sl_tonkho;
                            khmvt_cdh.sl_tonkho_toithieu = sp.sl_tonkho_toithieu.GetValueOrDefault(0);
                            //khmvt_cdh.sl_layton = (khmvt_cdh.sl_layton - khmvt_cdh.sl_tonkho_toithieu).GetValueOrDefault(0).Set0WhenlessThan0();
                            khmvt_cdh.sl_layton = 0;
                            var sldn = (khmvt_cdh.sl_can.GetValueOrDefault(0) - khmvt_cdh.sl_layton.GetValueOrDefault(0)
                                    + khmvt_cdh.sl_tonkho_toithieu.GetValueOrDefault(0) - khmvt_cdh.sl_tonkho.GetValueOrDefault(0)).Set0WhenlessThan0().GetValueOrDefault(0);
                            khmvt_cdh.sl_denghi = sldn <= 0 ? 0 : sldn;
                        }
                        else
                        {
                            khmvt_cdh.sl_can += ycmvt_cdh.soluong_yeucau;
                            //khmvt_cdh.sl_layton = khmvt_cdh.sl_tonkho > khmvt_cdh.sl_can ? khmvt_cdh.sl_can : khmvt_cdh.sl_tonkho;
                            //khmvt_cdh.sl_layton = (khmvt_cdh.sl_layton - khmvt_cdh.sl_tonkho_toithieu).GetValueOrDefault(0).Set0WhenlessThan0();
                            khmvt_cdh.sl_layton = 0;
                            var sldn = (khmvt_cdh.sl_can.GetValueOrDefault(0) - khmvt_cdh.sl_layton.GetValueOrDefault(0)
                                + khmvt_cdh.sl_tonkho_toithieu.GetValueOrDefault(0) - khmvt_cdh.sl_tonkho.GetValueOrDefault(0)).Set0WhenlessThan0().GetValueOrDefault(0);
                            khmvt_cdh.sl_denghi = sldn <= 0 ? 0 : sldn;
                        }

                        khmvt_cdh.sapxep = ycmvt_cdh.c_yeucaumuavt_cdh_id;
                        khmvt_cdh.sl_conlai = 0;
                        khmvt_cdh.nguoitao = userTK.ad_user_id;
                        khmvt_cdh.vaitrotao = userTK.ad_role_id;
                        khmvt_cdh.bophantao = userTK.md_phongban_id;
                        khmvt_cdh.value_nguoitao = userTK.ma_user;
                        khmvt_cdh.value_vaitrotao = userTK.ten_role;
                        khmvt_cdh.value_bophantao = userTK.ten_phongban;
                        khmvt_cdh.nguoicapnhat = userTK.ad_user_id;
                        khmvt_cdh.vaitrocapnhat = userTK.ad_role_id;
                        khmvt_cdh.bophancapnhat = userTK.md_phongban_id;
                        khmvt_cdh.value_nguoicapnhat = userTK.ma_user;
                        khmvt_cdh.value_vaitrocapnhat = userTK.ten_role;
                        khmvt_cdh.value_bophancapnhat = userTK.ten_phongban;
                        khmvt_cdh.ngaytao = DateTime.Now;
                        khmvt_cdh.ngaycapnhat = DateTime.Now;
                        khmvt_cdh.mota = "";
                        khmvt_cdh.hoatdong = true;
                        if (khmvt_cdh_chk == null)
                        {
                            db.c_kehoachmuavt_cdh.Add(khmvt_cdh);
                        }
                        db.SaveChanges();
                    }

                    //Tach KHMVT
                    var dem_nguoidung = from a in db.c_kehoachmuavt_cdh
                                        join b in db.md_sanpham on a.md_sanpham_id equals b.md_sanpham_id
                                        join c in db.md_chungloai_ql on b.md_nhomnangluc_id equals c.md_chungloai_id
                                        where a.c_kehoachmuavt_id == id_new & (c.ad_user_id != null & c.ad_user_id != "" & c.ad_user_id != " ")
                                        select new { c.ad_user_id };

                    var dem_ = dem_nguoidung.Distinct();
                    List<string> id_nd = new List<string>();
                    if (dem_.Count() > 0)
                    {
                        foreach (var dem in dem_.ToList())
                        {
                            id_nd.Add(dem.ad_user_id);
                        }
                    }

                    string id_new2 = "";
                    string sct_chung = "";

                    c_kehoachmuavt khdh = null;

                    if (id_nd.Count > 0)
                    {
                        for (int i = 0; i < id_nd.Count; i++)
                        {
                            id_new2 = Helper.getNewId();

                            string id_nd_ = id_nd[i];
                            ad_user user = db.ad_user.Where(s => s.ad_user_id == id_nd_).FirstOrDefault();
                            khdh = db.c_kehoachmuavt.Where(s => s.c_kehoachmuavt_id == id_new).FirstOrDefault();
                            if (khdh != null)
                            {
                                c_kehoachmuavt dsdh_new = khdh.Clone();
                                dsdh_new.c_kehoachmuavt_id = id_new2;

                                var san_pham = from a in db.c_kehoachmuavt_cdh
                                               join b in db.md_sanpham on a.md_sanpham_id equals b.md_sanpham_id
                                               where a.c_kehoachmuavt_id == id_new
                                               select new { a.c_kehoachmuavt_cdh_id, b.md_chungloai_id };
                                if (user != null)
                                {
                                    int m = 0;
                                    dsdh_new.ten_kehoach = ten_kehoach + " / " + user.ma_user;

                                    var san_pham2 = from a in san_pham
                                                    where (db.md_chungloai_ql.Where(s => s.md_chungloai_id == a.md_chungloai_id & s.ad_user_id == user.ad_user_id).Count() > 0)
                                                    select new { a.c_kehoachmuavt_cdh_id };

                                    foreach (var cdh_ in san_pham2.ToList())
                                    {
                                        c_kehoachmuavt_cdh cdh = db.c_kehoachmuavt_cdh.FirstOrDefault(s => s.c_kehoachmuavt_cdh_id == cdh_.c_kehoachmuavt_cdh_id);
                                        if (cdh != null)
                                        {
                                            c_yeucaumuavt_cdh ycmvt_cdh2 = db.c_yeucaumuavt_cdh.FirstOrDefault(s => s.c_yeucaumuavt_cdh_id == cdh.sapxep);
                                            if (ycmvt_cdh2 != null)
                                                ycmvt_cdh2.sapxep = id_new2;
                                            cdh.c_kehoachmuavt_id = id_new2;
                                            m = 1;
                                        }
                                    }
                                    if (m > 0)
                                    {
                                        string sct = VNN_VariablePublic.sochungtu(db, "DTMVT", 1);
                                        dsdh_new.sochungtu = sct;
                                        sct_chung += sct + ",";
                                        db.c_kehoachmuavt.Add(dsdh_new);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    int m = 0;
                                    var san_pham2 = from a in san_pham
                                                    where (db.md_chungloai_ql.Where(s => s.md_chungloai_id == a.md_chungloai_id).Count() <= 0)
                                                    select new { a.c_kehoachmuavt_cdh_id };

                                    dsdh_new.ten_kehoach = ten_kehoach + " / " + "Chung";

                                    foreach (var cdh_ in san_pham2.ToList())
                                    {
                                        c_kehoachmuavt_cdh cdh = db.c_kehoachmuavt_cdh.FirstOrDefault(s => s.c_kehoachmuavt_cdh_id == cdh_.c_kehoachmuavt_cdh_id);
                                        if (cdh != null)
                                        {
                                            c_yeucaumuavt_cdh ycmvt_cdh2 = db.c_yeucaumuavt_cdh.FirstOrDefault(s => s.c_yeucaumuavt_cdh_id == cdh.sapxep);
                                            if (ycmvt_cdh2 != null)
                                                ycmvt_cdh2.sapxep = id_new2;
                                            cdh.c_kehoachmuavt_id = id_new2;
                                            m = 1;
                                        }
                                    }
                                    if (m > 0)
                                    {
                                        string sct = VNN_VariablePublic.sochungtu(db, "DTMVT", 1);
                                        dsdh_new.sochungtu = sct;
                                        sct_chung += sct + ",";
                                        db.c_kehoachmuavt.Add(dsdh_new);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        c_kehoachmuavt nht = db.c_kehoachmuavt.Where(s => s.c_kehoachmuavt_id == id_new).FirstOrDefault();
                        string sct = VNN_VariablePublic.sochungtu(db, "DTMVT", 1);
                        nht.sochungtu = sct;
                        sct_chung = sct;
                        db.SaveChanges();
                    }

                    foreach (var ycmvt in db.c_yeucaumuavt.Where(s => vnn.Contains(s.c_yeucaumuavt_id)).ToList())
                    {
                        if (string.IsNullOrEmpty(sct_chung))
                            sct_chung = "";
                        foreach (var id_khmvt in db.c_yeucaumuavt_cdh.Where(s => s.c_yeucaumuavt_id == ycmvt.c_yeucaumuavt_id).Select(s => new { s.sapxep }).Distinct().ToList())
                        {
                            c_kehoachmuavt khmvt_sct = db.c_kehoachmuavt.FirstOrDefault(s => s.c_kehoachmuavt_id == id_khmvt.sapxep);
                            if (khmvt_sct != null)
                            {
                                sct_chung += khmvt_sct.sochungtu + ",";
                            }
                        }

                        if (sct_chung.LastIndexOf(",") > -1)
                            ycmvt.c_kehoachmuavt_id = sct_chung.Remove(sct_chung.Length - 1);
                        else if (!string.IsNullOrEmpty(sct_chung))
                            ycmvt.c_kehoachmuavt_id = sct_chung;
                        else
                            ycmvt.c_kehoachmuavt_id = " ";

                        ycmvt.md_trangthai_id = "HIEULUC";
                    }

                    var remove_kh = db.c_kehoachmuavt.Where(s => s.sochungtu == "goc").FirstOrDefault();
                    if (remove_kh != null)
                    {
                        db.c_kehoachmuavt.Remove(remove_kh);
                    }
                    db.SaveChanges();

                    msg_success = string.Format(@"Đã tạo kế hoạch mua vật tư ""{0}"" thành công.", sct_chung);
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }

        EndEventHandler:;

            if (msg.Length <= 0)
            {
                transaction.Commit();

                //string postData = "";
                //postData += "oper=CA_01_LamMoiHangTonKho";
                //postData += string.Format(@"&id={0}", id_new);
                //msg = VNN_VariablePublic.GetModule("Controller/JQGridModify/JQGridMD_01_CDKHVattuModify.ashx", postData);


                msg = string.Format("<div style='color:blue'>{0}</div>", msg_success);
            }
            else
            {
                msg = string.Format("<div style='color:red'>{0}</div>", msg);
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string masp = context.Request.Form["ma_sanpham"].removeAllSpaceOrTrimText(true);
        string id = context.Request.Form["id"];
        string ngaycanStr = context.Request.Form["ngaycan"].removeAllSpaceOrTrimText(true);
        string ncvt_name = context.Request.Form["ncvt_name"].removeAllSpaceOrTrimText(true);
        try
        {
            if(!VNN_Config.setDateTime(ngaycanStr).IsDate())
            {
                msg = $@"""Ngày cần"" có giá trị sai";
                goto EndEventHandler;
            }

            if (string.IsNullOrWhiteSpace(ncvt_name))
            {
                msg = $@"""Tên yêu cầu"" không thể bỏ trống";
                goto EndEventHandler;
            }

            var exist = db.c_yeucaumuavt.Where(s => s.ncvt_name == ncvt_name).Take(1).Count() > 0;
            if (exist)
            {
                msg = $@"""Tên yêu cầu"" đã được sử dụng trước đó";
                goto EndEventHandler;
            }

            string sochungtu = VNN_VariablePublic.sochungtu(db, "YCM", 1);
            var object_ = new c_yeucaumuavt();
            object_.c_yeucaumuavt_id = id_new;
            VNN_Function.SetFormValue(object_.nameof(s => s.sochungtu), sochungtu);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            db.c_yeucaumuavt.Add(object_);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = string.Format(@"true#Thêm mới thành công#{0}", id_new);
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg);
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string ngaycanStr = context.Request.Form["ngaycan"].removeAllSpaceOrTrimText(true);
        string ncvt_name = context.Request.Form["ncvt_name"].removeAllSpaceOrTrimText(true);
        string id = context.Request.Form["id"];

        try
        {
            var object_ = db.c_yeucaumuavt.Where(p => p.c_yeucaumuavt_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Không tìm thấy đối tượng cần sửa ";
                goto EndEventHandler;
            }

            if(!VNN_Config.setDateTime(ngaycanStr).IsDate())
            {
                msg = $@"""Ngày cần"" có giá trị sai";
                goto EndEventHandler;
            }

            if (string.IsNullOrWhiteSpace(ncvt_name))
            {
                msg = $@"""Tên yêu cầu"" không thể bỏ trống";
                goto EndEventHandler;
            }

            var exist = db.c_yeucaumuavt.Where(s => s.ncvt_name == ncvt_name & s.c_yeucaumuavt_id != object_.c_yeucaumuavt_id).Take(1).Count() > 0;
            if (exist)
            {
                msg = $@"""Tên yêu cầu"" đã được sử dụng trước đó";
                goto EndEventHandler;
            }

            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = string.Format(@"true#Cập nhật thành công");
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg);
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
                    var object_ = db.c_yeucaumuavt.Where(p => p.c_yeucaumuavt_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else if (!string.IsNullOrWhiteSpace(object_.c_kehoachmuavt_id))
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Đã tạo kế hoạch mua vật tư.", object_.sochungtu);
                    }
                    else if (object_.md_trangthai_id == "HIEULUC")
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Đã được hiệu lực.", object_.sochungtu);
                    }
                    else
                    {
                        var ncvt = db.c_nhucauvattu.Where(s => s.c_nhucauvattu_id == object_.c_nhucauvattu_id).Take(1).FirstOrDefault();
                        if (ncvt != null)
                        {
                            var khdhs = db.c_nhucauvattu_dhpx.Where(s => s.c_nhucauvattu_id == ncvt.c_nhucauvattu_id).ToList();
                            foreach (var khdh in khdhs)
                            {
                                var khdhLK = db.c_kehoachdathang.Where(s => s.c_kehoachdathang_id == khdh.c_kehoachdathang_id).FirstOrDefault();
                                if (khdhLK != null)
                                {
                                    khdhLK.xulyNCVT = false;
                                    khdhLK.tinhNCVT = false;
                                }
                            }
                            ncvt.c_yeucaumuavt_id = null;
                            db.c_nhucauvattu.Remove(ncvt);
                        }

                        foreach (var dhpx in db.md_dondathangphanxuong.Where(s => s.yeucaumuavattu == object_.sochungtu).ToList())
                        {
                            dhpx.yeucaumuavattu = " ";
                        }

                        VNN_Function.Write_log(context, ma_module, null, oper, "YCMVT:" + object_.sochungtu, db);
                        db.c_yeucaumuavt.Remove(object_);

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
                msg = string.Format(@"true#Xóa yêu cầu mua vật tư đã chọn thành công");
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
