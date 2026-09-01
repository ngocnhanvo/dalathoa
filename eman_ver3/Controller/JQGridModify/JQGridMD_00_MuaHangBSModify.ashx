<%@ WebHandler Language="C#" Class="JQGridMD_00_DonMuaHangModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
public class JQGridMD_00_DonMuaHangModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            default:
                break;
        }
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];

        string ma_dtkd = context.Request.Form["md_doitackinhdoanh_id"];
        string md_banggia_id = context.Request.Form["md_banggia_id"];
        string md_dongtien_id = "";
        string i = "1";
        string ngaydonhang = context.Request.Form["ngaydonhang"];
        string ngaygiaohang = context.Request.Form["ngaygiaohang"];
        string sctdathang = context.Request.Form["sctdathang"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string sochungtu = VNN_VariablePublic.sochungtu(db, "DMH", 1);
                var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == ma_dtkd).FirstOrDefault();
                var bg = db.md_banggia.Where(s => s.md_banggia_id == md_banggia_id).FirstOrDefault();
                if (bg == null & ma_dtkd != "MUA NGOAI")
                    msg = @"Không tìm thấy bảng giá";
                if (dtkd == null)
                    msg = @"Không tìm thấy đối tác kinh doanh";
                else if (db.c_danhsachdathang.Where(s => s.sochungtu == sctdathang).FirstOrDefault() == null)
                    msg = @"""SCT đơn hàng"" không tồn tại";
                else if (string.IsNullOrEmpty(ngaydonhang))
                    msg = @"""Ngày đơn hàng"" không thể để trống";
                else if (string.IsNullOrEmpty(ngaygiaohang))
                    msg = @"""Ngày giao hàng"" không thể để trống";
                string id = context.Request.Form["id"];
                if (msg.Length <= 0)
                {
                    if (ma_dtkd == "MUA NGOAI")
                        md_dongtien_id = db.md_dongtien.Where(s => s.ma_iso == "VND").Select(s => s.md_dongtien_id).FirstOrDefault();
                    else
                        md_dongtien_id = bg.md_dongtien_id;

                    var object_ = new c_donmuahang();
                    object_.c_donmuahang_id = id_new;
                    object_.bosung = true;
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_doitackinhdoanh_id), dtkd.md_doitackinhdoanh_id);
                    VNN_Function.SetFormValue(object_.nameof(s=>s.c_kehoachdathang_dhncc_id), i);
                    VNN_Function.SetFormValue(object_.nameof(s=>s.sochungtu), sochungtu);
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_dongtien_id), md_dongtien_id);
                    VNN_Function.SetFormValue(object_.nameof(s=>s.tygiaVND), Extension.TyGiaVND(md_dongtien_id, VNN_Config.setDateTime(ngaydonhang), db).ToString());
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
                    object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    db.c_donmuahang.Add(object_);
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
        string msg = "", msg_er = "", msg_err = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string md_trangthai_id = context.Request.Form["md_trangthai_id"];
        string ma_dtkd = context.Request.Form["md_doitackinhdoanh_id"];
        string md_banggia_id = context.Request.Form["md_banggia_id"];
        string md_phienbangia_id = context.Request.Form["md_phienbangia_id"];
        string md_phienbangia_id2 = md_phienbangia_id;
        string md_doitackinhdoanh_id = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == ma_dtkd).Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id_parent = context.Request.Form["id_parent"];
                string id = context.Request.Form["id"];
                var object_ = db.c_donmuahang.Where(p => p.c_donmuahang_id == id).Take(1).FirstOrDefault();
                int count = db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id).Count();

                if (object_ == null)
                {
                    msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (ma_dtkd == "MUA NGOAI" & md_trangthai_id == "HIEULUC")
                {
                    msg = string.Format(@"false#Lỗi:Không thể ""Hiệu Lực"" khi đối tác kinh doanh là ""{0}"".", ma_dtkd);
                }
                else if (count <= 0 & md_trangthai_id == "HIEULUC")
                {
                    msg = "false#Lỗi:Đối tượng chưa có mặt hàng cần mua.";
                }
                else if (md_doitackinhdoanh_id == null)
                {
                    msg = "false#Lỗi:Không tồn tại đối tác kinh doanh \"" + ma_dtkd + "\".";
                }
                else if (!new string[] { "SOANTHAO", "HIEULUC" }.Contains(md_trangthai_id) & object_.md_trangthai_id == "SOANTHAO")
                {
                    msg = "false#Lỗi:Đối tượng chỉ có thể 'Hiệu Lực'.";
                }

                object_.mota = context.Request.Form["mota"];
                if (object_.md_trangthai_id == "HIEULUC")
                {
                    var ngaythanhtoan = VNN_Config.setDateTime(context.Request.Form["ngaythanhtoan"]);
                    if (ngaythanhtoan.IsDate() == true)
                        object_.ngaythanhtoan = ngaythanhtoan;
                    else
                        msg = "false#Lỗi: Ngày thanh toán không đúng định dạng.";
                    object_.diadiem_giaohang = context.Request.Form["diadiem_giaohang"];

                    if (msg.Length <= 0)
                        msg = @"true#Đã cập nhật ""Ngày thành toán"" và ""địa điểm giao hàng""";

                    db.SaveChanges();
                }
                else if (object_.md_trangthai_id == "SOANTHAO")
                {
                    foreach (var dmh_st in db.c_donmuahang.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id & s.c_kehoachmuavt_id == object_.c_kehoachmuavt_id).ToList())
                    {
                        foreach (var cdh_st in db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == dmh_st.c_donmuahang_id).ToList())
                        {
                            var cdh_obj = db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_id == dmh_st.c_kehoachmuavt_id & s.md_sanpham_id == cdh_st.md_sanpham_id).FirstOrDefault();
                            if (cdh_obj != null)
                            {
                                var sp_obj = db.md_sanpham.Where(s => s.md_sanpham_id == cdh_obj.md_sanpham_id).FirstOrDefault();
                                if (
                                    (cdh_obj.sl_conlai + cdh_st.sl_dadat2.GetValueOrDefault(0) - cdh_st.saiso.GetValueOrDefault(0))
                                    > cdh_obj.sl_duyet
                                )
                                {
                                    decimal check_sl = cdh_obj.sl_duyet.Value - cdh_obj.sl_conlai.Value;
                                    msg_err += string.Format(@"Mã sản phẩm ""{0}"" vượt quá SL cho phép (SL còn lại:""{1}"") <br>",
                                        sp_obj.ma_sanpham, check_sl);
                                }
                            }
                        }
                    }

                    if (msg_err.Length > 0)
                        msg = "false#Lỗi:" + msg_err;

                    if (msg.Length <= 0)
                    {
                        md_phienbangia_id2 = object_.md_phienbangia_id;
                        md_banggia bg = db.md_banggia.Where(s => s.md_banggia_id == md_banggia_id).Take(1).FirstOrDefault();
                        md_phienbangia pbg = db.md_phienbangia.Where(s => s.md_banggia_id == bg.md_banggia_id & s.md_phienbangia_id == md_phienbangia_id).FirstOrDefault();

                        if (bg == null)
                        {
                            msg += "false#Lỗi:Nhà cung cấp \"" + ma_dtkd + "\" không có bảng giá.";
                        }
                        else if (pbg == null)
                        {
                            md_phienbangia pbg1 = db.md_phienbangia.Where(s => s.md_phienbangia_id == md_phienbangia_id).FirstOrDefault();
                            msg += "false#Lỗi:Nhà cung cấp \"" + ma_dtkd + "\" không có phiên bảng giá \"" + pbg1.ten_phienbangia + "\" .";
                        }

                        var c_donmuahang_cdmhs = from a in db.c_donmuahang_cdmh
                                                 join c in db.md_sanpham on a.md_sanpham_id equals c.md_sanpham_id
                                                 where a.c_donmuahang_id == object_.c_donmuahang_id
                                                 select new { a.c_donmuahang_cdmh_id, a.md_sanpham_id, c.ma_sanpham };

                        if (msg.Length <= 0)
                        {
                            foreach (var cdmh in c_donmuahang_cdmhs.OrderBy(s => s.md_sanpham_id).ToList())
                            {
                                var gsp = db.md_giasanpham.Where(s => s.md_sanpham_id == cdmh.md_sanpham_id & s.md_phienbangia_id == pbg.md_phienbangia_id).FirstOrDefault();
                                if (gsp == null)
                                {
                                    msg_er += "không có giá của sản phẩm \"" + cdmh.ma_sanpham + "\"<br>";
                                }
                                else { }
                            }

                            if (msg_er.Length > 0)
                            {
                                msg = "false#Lỗi:Nhà cung cấp \"" + ma_dtkd + "\"<br>" + msg_er;
                            }
                        }

                        if (msg.Length <= 0)
                        {
                            object_.md_trangthai_id = context.Request.Form["md_trangthai_id"];
                            object_.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
                            object_.ngaydonhang = VNN_Config.setDateTime(context.Request.Form["ngaydonhang"]);
                            object_.ngaygiaohang = VNN_Config.setDateTime(context.Request.Form["ngaygiaohang"]);
                            object_.huongdan_lamhang = context.Request.Form["huongdan_lamhang"];
                            object_.diadiem_giaohang = context.Request.Form["diadiem_giaohang"];
                            object_.md_banggia_id = context.Request.Form["md_banggia_id"];
                            object_.md_phienbangia_id = context.Request.Form["md_phienbangia_id"];
                            object_.md_dongtien_id = bg.md_dongtien_id;
                            object_.tygiaVND = Extension.TyGiaVND(bg.md_dongtien_id, object_.ngaydonhang.Value, db);
                            object_.mota = context.Request.Form["mota"];
                            object_.hinhthucthanhtoan = context.Request.Form["hinhthucthanhtoan"];
                            db.c_donmuahang_thue.RemoveRange(db.c_donmuahang_thue.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id));
                            db.SaveChanges();

                            classFunc.TinhThueDonHang(object_, userTK, md_phienbangia_id, md_phienbangia_id2, db);
                            // cap nhat ke hoach mua vat tu
                            var kh = db.c_kehoachmuavt.Where(s => s.c_kehoachmuavt_id == object_.c_kehoachmuavt_id & s.c_donmuavattu_id.Contains(object_.sochungtu)).FirstOrDefault();
                            if (kh != null & md_trangthai_id == "HIEULUC")
                            {
                                foreach (var cdh in db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id).ToList())
                                {
                                    var cd = db.c_kehoachmuavt_cdh.Where(s => s.md_sanpham_id == cdh.md_sanpham_id & s.c_kehoachmuavt_id == kh.c_kehoachmuavt_id).FirstOrDefault();
                                    cd.sl_conlai += cdh.sl_dadat2;
                                }
                            }
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "false#" + ex;
            }

            if (msg.Length <= 0 | msg.StartsWith("true#")) {
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
                transaction.Rollback();
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
                    var object_ = db.c_donmuahang.Where(p => p.c_donmuahang_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else if (object_.md_trangthai_id == "HIEULUC")
                    {
                        msg += string.Format(@"<br><b>{0} ({1})</b>: Không thể xóa khi đẫ ""Hiệu Lực"".", object_.sochungtu, object_.so_donmuahang);
                    }
                    else if (!string.IsNullOrWhiteSpace(object_.phieunhapkho))
                    {
                        msg += string.Format(@"<br><b>{0} ({1})</b>: Đã tạo phiếu nhập kho.", object_.sochungtu, object_.so_donmuahang);
                    }
                    else
                    {
                        VNN_Function.Write_log(context, ma_module, null, oper, "MĐMH:" + object_.sochungtu + ", TĐMH:" + object_.so_donmuahang, db);
                        db.c_donmuahang.Remove(object_);
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
                msg = string.Format(@"true#Xóa các đơn mua hàng đã chọn thành công");
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