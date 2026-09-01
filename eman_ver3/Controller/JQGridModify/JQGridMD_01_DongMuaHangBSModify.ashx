<%@ WebHandler Language="C#" Class="JQGridMD_01_DongMuaHangBSModify" %>
using System;
using System.Web;
using System.Linq;
using System.Transactions;
using DataAcess;
public class JQGridMD_01_DongMuaHangBSModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public JQGridMD_00_DonMuaHangClass classFunc = new JQGridMD_00_DonMuaHangClass();
    EntityContext db;
    EntityFunction entityFunc;
    User_TK userTK = null;

    public string oper = "vnn";
    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            db = new EntityContext();
            entityFunc = new EntityFunction();
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
        string ma_sanpham = context.Request.Form["md_sanpham_id"];
        string co_ngayhethan = context.Request.Form["co_ngayhethan"];
        string donvitinhId = context.Request.Form["md_donvitinhsanpham_id"];

        using (var transaction = new TransactionScope())
        {
            try
            {
                var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).FirstOrDefault();
                string id_parent = context.Request.Form["id_parent"];
                string thue = context.Request.Form["thue"];
                decimal dongiamua = decimal.Parse(context.Request.Form["dongiamua"]);
                decimal thanhtien = 0, sl_thaythe = 0, sl_thaythe2 = 0, saiso = 0;
                decimal sl_dadat = decimal.Parse(context.Request.Form["sl_dadat"]);
                decimal giachuan = 0;
                thanhtien = dongiamua * sl_dadat;
                DateTime? ngayhethan = null;
                var dmh = db.c_donmuahang.Where(s => s.c_donmuahang_id == id_parent & s.bosung == true).FirstOrDefault();

                if (dmh == null)
                {
                    msg = "false#Lỗi: Chưa có đơn mua hàng";
                }
                else if (sl_dadat <= 0)
                {
                    msg = "false#Lỗi: Số lượng đặt phải lớn hơn 0";
                }
                else
                {

                    var gsp = db.md_giasanpham.
                            Where(s =>
                                s.md_sanpham_id == sp.md_sanpham_id &
                                s.md_phienbangia_id == dmh.md_phienbangia_id &
                                s.md_donvitinhsanpham_id == donvitinhId).FirstOrDefault();
                    if (gsp != null)
                    {
                        if (dongiamua <= 0)
                            dongiamua = gsp.gia.GetValueOrDefault(0);

                        giachuan = gsp.gia.GetValueOrDefault(0);
                        thanhtien = dongiamua * sl_dadat;
                    }
                    else
                        dongiamua = -1;

                    if (dongiamua < 0)
                        msg = "false#Bảng giá của đơn mua hàng \"" + dmh.sochungtu + "\" không có giá cho sản phẩm \"" + sp.ma_sanpham + "\".";
                    else if (dongiamua > giachuan)
                        msg = string.Format(@"false#Giá mua không thể lớn hơn giá chuẩn. ({0} > {1})", dongiamua, giachuan);
                    var check = db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == dmh.c_donmuahang_id & s.md_sanpham_id == sp.md_sanpham_id).FirstOrDefault();
                    if (check != null)
                        msg = "false#Lỗi: Mã sản phẩm đã tồn tại";

                    var sp_obj = db.md_sanpham.Where(s => s.md_sanpham_id == sp.md_sanpham_id).FirstOrDefault();
                    if (sp_obj != null)
                    {
                        var dvt = db.md_donvitinhsanpham_cddv.
                            Where(s => s.md_sanpham_id == sp_obj.ma_sanpham & s.md_donvitinhsanpham_id == donvitinhId).FirstOrDefault();
                        if (dvt != null)
                        {
                            sl_thaythe = dvt.nhanvoi.GetValueOrDefault(0) * sl_dadat;
                            sl_thaythe2 = dvt.nhanvoi.GetValueOrDefault(0) * 0;
                            saiso = dvt.chiacho.GetValueOrDefault(0) * sl_dadat;
                            if (saiso > dvt.saiso_toida.GetValueOrDefault(9999999))
                                saiso = dvt.saiso_toida.GetValueOrDefault(9999999);
                        }
                        else if (donvitinhId == sp_obj.md_donvitinhsanpham_id)
                        {
                            sl_thaythe = sl_dadat;
                            sl_thaythe2 = 0;
                            saiso = 0;
                        }
                        else
                        {
                            string tendvt = db.md_donvitinhsanpham.Where(s => s.md_donvitinhsanpham_id == donvitinhId).Select(s => s.ten_dvt).FirstOrDefault();
                            msg = string.Format(@"false#Lỗi: Mã sản phẩm ""{0}"" không có đơn vị tính ""{1}""",
                                sp_obj.ma_sanpham,
                                tendvt);
                        }
                    }
                }

                if (msg.Length <= 0)
                {

                    //them Don dat hang
                    var cdmh = new c_donmuahang_cdmh();
                    cdmh.c_donmuahang_id = id_parent;
                    cdmh.c_donmuahang_cdmh_id = Helper.getNewId();
                    cdmh.md_sanpham_id = sp.md_sanpham_id;
                    cdmh.md_donvitinhsanpham_id = donvitinhId;
                    cdmh.sl_dadat = sl_dadat;
                    cdmh.sl_dadat2 = sl_thaythe;
                    cdmh.saiso = saiso;
                    cdmh.sl_hanngach = 0;
                    cdmh.dongiamua = dongiamua;
                    cdmh.giachuan = giachuan;
                    cdmh.thue = thue;
                    cdmh.thanhtien = thanhtien;
                    cdmh.ngay_hethan = ngayhethan;
                    cdmh.nguoitao = userTK.ad_user_id;
                    cdmh.vaitrotao = userTK.ad_role_id;
                    cdmh.bophantao = userTK.md_phongban_id;
                    cdmh.value_nguoitao = userTK.ma_user;
                    cdmh.value_vaitrotao = userTK.ten_role;
                    cdmh.value_bophantao = userTK.ten_phongban;

                    cdmh.nguoicapnhat = userTK.ad_user_id;
                    cdmh.vaitrocapnhat = userTK.ad_role_id;
                    cdmh.bophancapnhat = userTK.md_phongban_id;
                    cdmh.value_nguoicapnhat = userTK.ma_user;
                    cdmh.value_vaitrocapnhat = userTK.ten_role;
                    cdmh.value_bophancapnhat = userTK.ten_phongban;
                    cdmh.ngaytao = DateTime.Now;
                    cdmh.ngaycapnhat = DateTime.Now;
                    cdmh.mota = "";
                    cdmh.hoatdong = true;
                    db.c_donmuahang_cdmh.Add(cdmh);
                    db.SaveChanges();

                    classFunc.TinhThueDonHang(dmh, userTK, dmh.md_phienbangia_id, dmh.md_phienbangia_id, db);
                }
            }
            catch (Exception ex)
            {
                msg = "false#" + ex.Message;
            }

            if (msg.LastIndexOf("false#") <= -1)
            {
                msg = "true#Cập nhật thành công.";
                VNN_Function.loaddulieu_Auto(db, ma_module);
                transaction.Complete();
            }
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        using(var transaction = new TransactionScope(TransactionScopeOption.Required))
        {
            try
            {
                string id = context.Request.Form["id"];
                string thue = context.Request.Form["thue"];
                string co_ngayhethan = context.Request.Form["co_ngayhethan"];
                decimal dongiamua = decimal.Parse(context.Request.Form["dongiamua"]);
                decimal sl_dadat = decimal.Parse(context.Request.Form["sl_dadat"]);
                decimal thanhtien = 0, sl_thaythe = 0, sl_thaythe2 = 0, saiso = 0;
                var object_ = db.c_donmuahang_cdmh.Where(p => p.c_donmuahang_cdmh_id == id).Take(1).FirstOrDefault();
                c_donmuahang dmh = null;
                if (object_ == null)
                {
                    msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (dongiamua > object_.giachuan)
                {
                    msg = @"false#Giá mua không thể lớn hơn giá chuẩn.";
                }
                else
                {
                    dmh = db.c_donmuahang.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id).Take(1).FirstOrDefault();
                    if (dmh == null)
                    {
                        msg = "false#Không tìm thấy đơn mua hàng.";
                    }
                    else if (dmh.md_trangthai_id != "SOANTHAO")
                    {
                        msg = "false#Đơn mua hàng đã hiệu lực, không thể sửa.";
                    }

                    thanhtien = dongiamua * sl_dadat;

                    var sp_obj = db.md_sanpham.Where(s => s.md_sanpham_id == object_.md_sanpham_id).FirstOrDefault();
                    if (sp_obj != null)
                    {
                        var dvt = db.md_donvitinhsanpham_cddv.
                            Where(s => s.md_sanpham_id == sp_obj.ma_sanpham & s.md_donvitinhsanpham_id == object_.md_donvitinhsanpham_id).FirstOrDefault();
                        if (dvt != null)
                        {
                            sl_thaythe = dvt.nhanvoi.GetValueOrDefault(0) * sl_dadat;
                            sl_thaythe2 = dvt.nhanvoi.GetValueOrDefault(0) * 0;
                            saiso = dvt.chiacho.GetValueOrDefault(0) * sl_dadat;
                            if (saiso > dvt.saiso_toida.GetValueOrDefault(9999999))
                                saiso = dvt.saiso_toida.GetValueOrDefault(9999999);
                        }
                        else if (object_.md_donvitinhsanpham_id == sp_obj.md_donvitinhsanpham_id)
                        {
                            sl_thaythe = sl_dadat;
                            sl_thaythe2 = 0;
                            saiso = 0;
                        }
                        else
                        {
                            string tendvt = db.md_donvitinhsanpham.Where(s => s.md_donvitinhsanpham_id == object_.md_donvitinhsanpham_id).Select(s => s.ten_dvt).FirstOrDefault();
                            msg = string.Format(@"false#Lỗi: Mã sản phẩm ""{0}"" không có đơn vị tính ""{1}""",
                                sp_obj.ma_sanpham,
                                tendvt);
                        }
                    }
                }

                if (msg.Length <= 0)
                {
                    object_.sl_dadat2 = sl_thaythe;
                    VNN_Function.SetFormValue(object_.nameof(s=>s.thanhtien), thanhtien.ToString());
                    VNN_Function.SetFormValue(object_.nameof(s=>s.saiso), saiso.ToString());
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_sanpham_id), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_donvitinhsanpham_id), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.c_donmuahang_id), "VNN_notpost");
                    object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
                    db.SaveChanges();
                    classFunc.TinhThueDonHang(dmh, userTK, dmh.md_phienbangia_id, dmh.md_phienbangia_id, db);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.LastIndexOf("false#") <= -1)
            {
                msg = "true#Cập nhật thành công.";
                VNN_Function.loaddulieu_Auto(db, ma_module);
                transaction.Complete();
            }
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        using (var transaction = new TransactionScope())
        {
            try
            {
                var ids = context.Request.Form["id"].Split(',').Where(s=>!string.IsNullOrWhiteSpace(s)).ToList();

                foreach (var id_del_ in ids)
                {
                    var object_ = db.c_donmuahang_cdmh.Where(p => p.c_donmuahang_cdmh_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else
                    {
                        var dmh = db.c_donmuahang.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id).FirstOrDefault();
                        if (dmh.md_trangthai_id == "HIEULUC")
                        {
                            msg += string.Format(@"<br><b>{0}</b>: Đã ""Hiệu lực"".", dmh.sochungtu);
                        }
                        else if (dmh.md_trangthai_id == "HUYBO")
                        {
                            msg += string.Format(@"<br><b>{0}</b>: Đã ""Hủy"".", dmh.sochungtu);
                        }
                        else if (dmh.md_trangthai_id == "KETTHUC")
                        {
                            msg += string.Format(@"<br><b>{0}</b>: Đã ""Kết thúc"".", dmh.sochungtu);
                        }
                        else
                        {
                            db.c_donmuahang_cdmh.Remove(object_);
                            db.SaveChanges();

                            classFunc.TinhThueDonHang(dmh, userTK, dmh.md_phienbangia_id, dmh.md_phienbangia_id, db);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#Xóa thành công");
                VNN_Function.loaddulieu_Auto(ma_module);
                transaction.Complete();
            }
            else
            {
                msg = string.Format(@"false#{0}", msg.Substring(4));
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