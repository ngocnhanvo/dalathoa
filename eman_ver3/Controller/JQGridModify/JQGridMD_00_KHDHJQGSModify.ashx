<%@ WebHandler Language="C#" Class="JQGridMD_00_KHDHJQGSModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_KHDHJQGSModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_TYeuCau":
                this.CA_01_TYeuCau(context);
                break;
            case "CA_01_KH_ThayDoiTT":
                this.CA_01_KH_ThayDoiTT(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_KH_ThayDoiTT(HttpContext context)
    {
        string msg = "", msg_success = "";
        string id = context.Request.Form["id"];
        string check_1 = context.Request.Form["check"];
        string[] vnn = id.Split(',');

        try
        {
            foreach (var dsdh in db.c_kehoachdathang.Where(s => vnn.Contains(s.c_kehoachdathang_id)).ToList())
            {
                if (dsdh.trangthai != "DAHET")
                {
                    if (check_1 == "1")
                    {
                        dsdh.trangthai = "DAHET";

                    }
                    else if (check_1 == "2")
                    {
                        dsdh.trangthai = "KETTHUC";
                    }
                    msg_success = "<div style='color:blue'> Chuyển đổi kế hoạch thành công.</div>";
                }
                else if (dsdh.trangthai != "KETTHUC")
                {
                    dsdh.trangthai = "SOANTHAO";
                    msg_success = "<div style='color:blue'> Chuyển đổi kế hoạch thành công.</div>";
                }
                else
                {
                    msg = "<div style='color:red'> Chuyển đổi thất bại, kế hoạch này đã kết thúc</div>";
                }
            }

            if (msg.Length <= 0)
            {
                db.SaveChanges();
                msg = msg_success;
            }
        }
        catch (Exception ex)
        {
            msg = string.Format(@"<div style='color:red'>{0}</div>", ex.Message);
        }
        context.Response.Write(msg);
    }

    private void CA_01_TYeuCau(HttpContext context)
    {
        string msg = "";
        context.Response.Write(msg);
    }

    //Them cac dong hang cho dat hang nha cung cap 
    public string add_mmuangoai_chd(EntityContext db, string c_kehoachdathang_id, string c_kehoachdathang_dhncc_id, string md_sanpham_id, string macuoi, string md_donvitinhsanpham_id, decimal sl_candat, DateTime ngayhoanthanh, string mota, decimal sl_phanphoi, int i, User_TK us)
    {
        string id_new = Helper.getNewId();
        c_kehoachdathang_dhncc_cdh check_px = db.c_kehoachdathang_dhncc_cdh.FirstOrDefault(s => s.md_sanpham_id == md_sanpham_id & s.c_kehoachdathang_dhncc_id == c_kehoachdathang_dhncc_id);
        c_kehoachdathang_dhncc_cdh dhncc = check_px;
        if (check_px == null)
        {
            dhncc = new c_kehoachdathang_dhncc_cdh();
            dhncc.c_kehoachdathang_dhncc_cdh_id = Helper.getNewId();
        }
        else
        {
            id_new = dhncc.c_kehoachdathang_dhncc_cdh_id;
        }

        dhncc.c_kehoachdathang_id = c_kehoachdathang_id;
        dhncc.c_kehoachdathang_dhncc_id = c_kehoachdathang_dhncc_id;
        dhncc.md_sanpham_id = md_sanpham_id;
        dhncc.macuoi = macuoi;
        dhncc.md_donvitinhsanpham_id = md_donvitinhsanpham_id;
        dhncc.mota = mota;
        if (sl_candat - sl_phanphoi == 0)
        {
            dhncc.soluong = sl_candat;
        }
        else
        {
            dhncc.soluong = sl_candat - sl_phanphoi;
        }
        dhncc.ngaycan = ngayhoanthanh;

        dhncc.nguoitao = us.ad_user_id;
        dhncc.vaitrotao = us.ad_role_id;
        dhncc.bophantao = us.md_phongban_id;
        dhncc.value_nguoitao = us.ma_user;
        dhncc.value_vaitrotao = us.ten_role;
        dhncc.value_bophantao = us.ten_phongban;
        dhncc.nguoicapnhat = us.ad_user_id;
        dhncc.vaitrocapnhat = us.ad_role_id;
        dhncc.bophancapnhat = us.md_phongban_id;
        dhncc.value_nguoicapnhat = us.ma_user;
        dhncc.value_vaitrocapnhat = us.ten_role;
        dhncc.value_bophancapnhat = us.ten_phongban;
        dhncc.ngaytao = DateTime.Now;
        dhncc.ngaycapnhat = DateTime.Now;
        dhncc.hoatdong = true;

        //Kiem tra
        if (check_px == null)
        {
            dhncc.ngaytao = DateTime.Now;
            dhncc.ngaycapnhat = DateTime.Now;
            db.c_kehoachdathang_dhncc_cdh.Add(dhncc);
        }
        else
        {
            dhncc.ngaycapnhat = DateTime.Now;
        }
        db.SaveChanges();
        return id_new;
    }

    //them cac dong mua hang nha cung cap
    public string add_mmuangoai(EntityContext db, string c_kehoachdathang_id, string md_doitackinhdoanh_id, string donhang_thamchieu, DateTime ngayhoanthanh, string mota, string sodonhang, string thoigianht, int i, User_TK us)
    {
        string id_new = Helper.getNewId();
        c_kehoachdathang_dhncc check_px = db.c_kehoachdathang_dhncc.FirstOrDefault(s => s.md_doitackinhdoanh_id == md_doitackinhdoanh_id & s.c_kehoachdathang_id == c_kehoachdathang_id);
        c_kehoachdathang_dhncc dhcpx = check_px;
        if (check_px == null)
        {
            dhcpx = new c_kehoachdathang_dhncc();
            dhcpx.c_kehoachdathang_dhncc_id = Helper.getNewId();
            dhcpx.chungtu = " ";
        }
        else
        {
            id_new = dhcpx.c_kehoachdathang_dhncc_id;
        }

        dhcpx.c_kehoachdathang_id = c_kehoachdathang_id;
        dhcpx.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
        dhcpx.donhang = donhang_thamchieu;
        dhcpx.mota = mota;
        md_doitackinhdoanh dtkd = db.md_doitackinhdoanh.Where(s => s.md_doitackinhdoanh_id == md_doitackinhdoanh_id).FirstOrDefault();
        dhcpx.banggia = dtkd.md_banggia_id;
        dhcpx.sctdathang = sodonhang;
        dhcpx.thoihan_giaohang = ngayhoanthanh - TimeSpan.Parse(thoigianht);

        dhcpx.nguoitao = us.ad_user_id;
        dhcpx.vaitrotao = us.ad_role_id;
        dhcpx.bophantao = us.md_phongban_id;
        dhcpx.value_nguoitao = us.ma_user;
        dhcpx.value_vaitrotao = us.ten_role;
        dhcpx.value_bophantao = us.ten_phongban;
        dhcpx.nguoicapnhat = us.ad_user_id;
        dhcpx.vaitrocapnhat = us.ad_role_id;
        dhcpx.bophancapnhat = us.md_phongban_id;
        dhcpx.value_nguoicapnhat = us.ma_user;
        dhcpx.value_vaitrocapnhat = us.ten_role;
        dhcpx.value_bophancapnhat = us.ten_phongban;
        dhcpx.ngaytao = DateTime.Now;
        dhcpx.ngaycapnhat = DateTime.Now;
        dhcpx.hoatdong = true;

        //Kiem tra
        if (check_px == null)
        {
            dhcpx.ngaytao = DateTime.Now;
            dhcpx.ngaycapnhat = DateTime.Now;
            db.c_kehoachdathang_dhncc.Add(dhcpx);
        }
        else
        {
            dhcpx.ngaycapnhat = DateTime.Now;
        }
        db.SaveChanges();
        return id_new;
    }


    //them cac dong hang cua phan xuong
    public string add_phanxuong_cdh(EntityContext db, string c_kehoachdathang_id, string c_kehoachdathang_dhcpx_id, string md_sanpham_id, string macuoi,
    string md_donvitinhsanpham_id, decimal sl_phanphoi, string mota, string noigiaohang, int i, User_TK us, string sodonhang)
    {
        //lay bom san pham vat tu
        string dsdhId = db.c_danhsachdathang.Where(s => s.sochungtu == sodonhang).Select(s => s.c_danhsachdathang_id).FirstOrDefault();
        string spBomId = db.c_dongdsdh.Where(s =>
                            s.md_sanpham_id == md_sanpham_id &
                            s.c_danhsachdathang_id == dsdhId)
                            .Select(s => s.md_sanpham_bom_id).FirstOrDefault();

        string id_new = Helper.getNewId();
        var check_px = db.c_kehoachdathang_dhcpx_cdh.FirstOrDefault(s => s.md_sanpham_id == md_sanpham_id & s.c_kehoachdathang_dhcpx_id == c_kehoachdathang_dhcpx_id);

        c_kehoachdathang_dhcpx_cdh dhcpx = check_px;
        if (check_px == null)
        {
            dhcpx = new c_kehoachdathang_dhcpx_cdh();
            dhcpx.c_kehoachdathang_dhcpx_cdh_id = Helper.getNewId();
        }
        else
        {
            id_new = dhcpx.c_kehoachdathang_dhcpx_cdh_id;
        }

        dhcpx.c_kehoachdathang_id = c_kehoachdathang_id;
        dhcpx.c_kehoachdathang_dhcpx_id = c_kehoachdathang_dhcpx_id;
        dhcpx.md_sanpham_id = md_sanpham_id;
        dhcpx.macuoi = macuoi;
        dhcpx.md_donvitinhsanpham_id = md_donvitinhsanpham_id;
        dhcpx.noigiaohang = noigiaohang;
        dhcpx.mota = mota;
        dhcpx.soluong = sl_phanphoi;
        dhcpx.md_sanpham_bom_id = spBomId;
        dhcpx.nguoitao = us.ad_user_id;
        dhcpx.vaitrotao = us.ad_role_id;
        dhcpx.bophantao = us.md_phongban_id;
        dhcpx.value_nguoitao = us.ma_user;
        dhcpx.value_vaitrotao = us.ten_role;
        dhcpx.value_bophantao = us.ten_phongban;
        dhcpx.nguoicapnhat = us.ad_user_id;
        dhcpx.vaitrocapnhat = us.ad_role_id;
        dhcpx.bophancapnhat = us.md_phongban_id;
        dhcpx.value_nguoicapnhat = us.ma_user;
        dhcpx.value_vaitrocapnhat = us.ten_role;
        dhcpx.value_bophancapnhat = us.ten_phongban;
        dhcpx.ngaytao = DateTime.Now;
        dhcpx.ngaycapnhat = DateTime.Now;
        dhcpx.hoatdong = true;

        //Kiem tra
        if (check_px == null)
        {
            dhcpx.ngaytao = DateTime.Now;
            dhcpx.ngaycapnhat = DateTime.Now;
            db.c_kehoachdathang_dhcpx_cdh.Add(dhcpx);
        }
        else
        {
            dhcpx.ngaycapnhat = DateTime.Now;
        }
        db.SaveChanges();
        return id_new;
    }
    //them dat hang cho phan xuong
    public string add_phanxuong(EntityContext db, string c_kehoachdathang_id, string md_phanxuong_id, string donhang_thamchieu, DateTime ngayhoanthanh, string mota, string sodonhang, string thoigianht, int i, User_TK us)
    {
        string id_new = Helper.getNewId();
        c_kehoachdathang_dhcpx check_px = db.c_kehoachdathang_dhcpx.FirstOrDefault(s => s.md_phanxuong_id == md_phanxuong_id & s.c_kehoachdathang_id == c_kehoachdathang_id);
        c_kehoachdathang_dhcpx dhcpx = check_px;
        if (check_px == null)
        {
            dhcpx = new c_kehoachdathang_dhcpx();
            dhcpx.c_kehoachdathang_dhcpx_id = Helper.getNewId();
            dhcpx.dongdathang = " ";
        }
        else
        {
            id_new = dhcpx.c_kehoachdathang_dhcpx_id;
        }

        dhcpx.md_phanxuong_id = md_phanxuong_id;
        dhcpx.c_kehoachdathang_id = c_kehoachdathang_id;
        dhcpx.donhang = donhang_thamchieu;
        dhcpx.ngayHTcham = ngayhoanthanh;
        dhcpx.sctdathang = sodonhang;
        var dsdh = db.c_danhsachdathang.Where(s => s.sochungtu == dhcpx.sctdathang).FirstOrDefault();
        dhcpx.hdlh = dsdh.huongdanlamhang;
        dhcpx.hdlhchung = dsdh.huongdanlamhangchung;
        dhcpx.c_danhsachdathang_id = dsdh.c_danhsachdathang_id;
        dhcpx.tinh_ncvt = false;

        dhcpx.nguoitao = us.ad_user_id;
        dhcpx.vaitrotao = us.ad_role_id;
        dhcpx.bophantao = us.md_phongban_id;
        dhcpx.value_nguoitao = us.ma_user;
        dhcpx.value_vaitrotao = us.ten_role;
        dhcpx.value_bophantao = us.ten_phongban;
        dhcpx.nguoicapnhat = us.ad_user_id;
        dhcpx.vaitrocapnhat = us.ad_role_id;
        dhcpx.bophancapnhat = us.md_phongban_id;
        dhcpx.value_nguoicapnhat = us.ma_user;
        dhcpx.value_vaitrocapnhat = us.ten_role;
        dhcpx.value_bophancapnhat = us.ten_phongban;
        dhcpx.ngaytao = DateTime.Now;
        dhcpx.ngaycapnhat = DateTime.Now;
        dhcpx.hoatdong = true;

        //Kiem tra
        if (check_px == null)
        {
            dhcpx.ngaytao = DateTime.Now;
            dhcpx.ngaycapnhat = DateTime.Now;
            db.c_kehoachdathang_dhcpx.Add(dhcpx);
        }
        else
        {
            dhcpx.ngaycapnhat = DateTime.Now;
        }
        db.SaveChanges();
        return id_new;
    }

    //Xoa cac du lieu lien quan den lap ke hoach de doi chieu lai 
    public void del_kehoachdathang(EntityContext db, string kehoachdathang_id)
    {
        db.c_kehoachdathang_dhcpx.RemoveRange(db.c_kehoachdathang_dhcpx.Where(s => s.c_kehoachdathang_id == kehoachdathang_id));
        db.c_kehoachdathang_dhncc.RemoveRange(db.c_kehoachdathang_dhncc.Where(s => s.c_kehoachdathang_id == kehoachdathang_id));
        db.SaveChanges();
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
                var object_ = db.c_kehoachdathang.Where(p => p.c_kehoachdathang_id == id).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else
                {
                    object_.mota = context.Request.Form["mota"];
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
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
            var object_s = db.c_kehoachdathang.Where(p => ids.Contains(p.c_kehoachdathang_id)).ToList();
            if (object_s.Count <= 0)
            {
                msg = $@"<br>Không tìm thấy đối tượng cần xóa.";
                goto EndEventHandler;
            }

            foreach (var object_ in object_s)
            {
                if (object_.trangthai != Helper.SOANTHAO)
                {
                    msg += string.Format(@"<br><b>{0}</b>: chỉ xóa được khi ở trạng thái Soạn Thảo.", object_.ten_kh);
                }
                else if (!string.IsNullOrWhiteSpace(object_.nhomKH))
                {
                    msg += string.Format(@"<br>Kế hoạch này đang thuộc <b>kế hoạch tổng</b> có tên là <b>""{0}""</b>", object_.nhomKH);
                }
                else
                {
                    var ghn = db.md_hanngach.Where(s => s.c_danhsachdathang_id == object_.c_danhsachdathang_id).FirstOrDefault();
                    if (ghn != null)
                    {
                        msg += $@"<br>Đơn hàng <b>{object_.donhang_thamchieu}</b> đã lập phiếu Giảm Hạn Ngạch <b>{ghn.sochungtu}</b>";
                    }
                    else
                    {
                        var dhpxOlds = db.md_dondathangphanxuong.Where(s => s.c_kehoachdathang_id == object_.c_kehoachdathang_id).ToList();
                        var dhpxIds = dhpxOlds.Select(s => s.md_dondathangphanxuong_id).ToList();
                        var lsxOlds = db.md_lenhsanxuat.Where(s => dhpxIds.Contains(s.md_dondathangphanxuong_id)).ToList();

                        foreach (var lsxOld in lsxOlds)
                        {
                            db.md_lenhsanxuat.Remove(lsxOld);
                        }

                        foreach (var dhpxOld in dhpxOlds)
                        {
                            db.md_dondathangphanxuong.Remove(dhpxOld);
                        }

                        var ds = db.c_danhsachdathang.FirstOrDefault(s => s.so_po == object_.donhang_thamchieu);
                        if (ds != null)
                        {
                            ds.md_trangthai_id = Helper.HIEULUC;
                            ds.trangthai = Helper.SOANTHAO;
                        }

                        VNN_Function.Write_log(context, ma_module, null, oper, "TKHĐH:" + object_.ten_kh, db);
                        db.c_kehoachdathang.Remove(object_);
                    }
                }
            }

            if (msg.Length <= 0)
                db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = $"Lỗi: {ex.Message}";
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"true#Xóa kế hoạch đặt hàng đã chọn thành công";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = $@"false#{msg.Substring(4)}";
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