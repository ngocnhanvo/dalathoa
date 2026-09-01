<%@ WebHandler Language="C#" Class="JQGridMD_00_NhapkhotuNCCModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_NhapkhotuNCCModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
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
            case "CA01XLNKNCC_MD01DonMuaHangNKNCC":
                this.CA01XLNKNCC_MD01DonMuaHangNKNCC(context);
                break;
            case "CA_01_XacNhanNhapkho":
                this.CA_01_XacNhanNhapkho(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_XacNhanNhapkho(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string md_nhapkho_ncc_id = context.Request.QueryString["id"];
        md_nhapkho_ncc nk_ncc = db.md_nhapkho_ncc.Where(s => s.md_nhapkho_ncc_id == md_nhapkho_ncc_id).Take(1).FirstOrDefault();
        if (nk_ncc != null)
        {
            if (nk_ncc.phieuXNNK == null | nk_ncc.phieuXNNK == "")
            {
                nk_ncc.phieuXNNK = nk_ncc.sochungtu.Replace("PNK", "XNNK");
                db.SaveChanges();
            }
        }
    }

    public void CA01XLNKNCC_MD01DonMuaHangNKNCC(HttpContext context)
    {
        EntityContext db = new EntityContext();
        User_TK us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context));
        string msg = "", msg_success = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        bool check_hieuluc = false;
        int count_slnhap = 0;
        bool check = false;
        try
        {
            //Xu Ly nhap kho
            foreach (md_nhapkho_ncc px in db.md_nhapkho_ncc.Where(s => s.md_nhapkho_ncc_id == id))
            {
                if (px.trangthai == "HIEULUC")
                {
                    msg += "<div style='color:red'>Lỗi: dòng \"" + px.sochungtu + "\" đã hiệu lực.</div>";
                }
                else if (px.phieuXNNK == null | px.phieuXNNK == "")
                {
                    msg += "<div style='color:red'>Lỗi: dòng \"" + px.sochungtu + "\" chưa in phiếu xác nhận nhập hàng.</div>";
                }
                else {
                    var check_kho = db.md_nhapkho_ncc_dh.Where(s => s.check_kho == true & s.md_nhapkho_ncc_id == px.md_nhapkho_ncc_id);
                    var check_ma = db.md_nhapkho_ncc_dh.Where(s => s.md_sanpham_id != null & s.md_nhapkho_ncc_id == px.md_nhapkho_ncc_id);

                    var md_nhapkho_ncc_dhs = db.md_nhapkho_ncc_dh.Where(s => s.md_nhapkho_ncc_id == px.md_nhapkho_ncc_id & (s.check_kho == null | s.check_kho == false));
                    foreach (md_nhapkho_ncc_dh dh in md_nhapkho_ncc_dhs)
                    {
                        md_sanpham sp = db.md_sanpham.Where(s => s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
                        if (dh.check_kho != true)
                        {
                            if (px.kho == null & px.kho == " ")
                            {
                                msg += "<div style='color:red'>Lỗi: dòng \"" + px.sochungtu + "\" chưa có kho đến.</div>";
                            }
                            else if (dh.sl_nhap < 0)
                            {
                                msg += "<div style='color:red'>Lỗi: Số lượng nhập của \"" + sp.ma_sanpham + "\" phải lớn hơn 0 .</div>";
                            }
                            else if (dh.sl_nhap == 0)
                            {
                                count_slnhap++;
                            }
                            else
                            {
                                check = true;
                            }
                        }
                        if (check == true)
                        {
                            if (dh.check_kho != true)
                            {
                                decimal soluong_nhap = dh.sl_nhap.Value;
                                //--cap nhat so luong nhap
                                edit_nhap(db, dh, us);
                                //--cap nhat vao kho
                                add_kho(db, dh, px, us);
                                //--tao lich su nhap kho
                                string abc = add_kho_ls(db, dh, px, us);
                                if (abc.Length <= 0)
                                {
                                    msg_success += "<div style='color:blue'>Dòng \"" + px.sochungtu + " của \"" + sp.ma_sanpham + "\" đã nhập kho thành công, số lượng nhập là: \"" +
                                    VNN_VariablePublic.autoRound(soluong_nhap, 4) + "\"</div>";
                                }
                            }
                        }
                    }
                    int dem_dh = md_nhapkho_ncc_dhs.Count();
                    if (dem_dh <= count_slnhap & dem_dh > 0 & count_slnhap > 0)
                    {
                        msg_success += "<div style='color:red'>Dòng \"" + px.sochungtu + " phải có ít nhất 1 dòng hàng có số lượng lớn hơn 0.</div>";
                    }
                    //Kiem tra de chuyen thanh Hieu Luc
                    if (msg.Length <= 0)
                    {
                        px.trangthai = "HIEULUC";
                        db.SaveChanges();
                    }
                    msg = msg_success + msg;
                }
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    //--cap nhat so luong nhap
    public string edit_nhap(EntityContext db, md_nhapkho_ncc_dh dh, User_TK us)
    {
        string id_new = dh.md_nhapkho_ncc_id;
        md_nhapkho_ncc nk = db.md_nhapkho_ncc.Where(s => s.md_nhapkho_ncc_id == dh.md_nhapkho_ncc_id).FirstOrDefault();
        if (dh.tong_sl_dat == (decimal)dh.sl_danhap + (decimal)dh.sl_nhap)
        {
            dh.check_kho = true;
        }

        dh.sl_danhap = dh.sl_nhap + dh.sl_danhap;
        nk.ngaycapnhat = DateTime.Now;
        foreach(md_nhapkho_ncc check_nk in db.md_nhapkho_ncc.Where(s => s.md_nhapkho_ncc_id != nk.md_nhapkho_ncc_id & s.c_donmuahang_id == nk.c_donmuahang_id & s.trangthai == "SOANTHAO"))
        {
            foreach(md_nhapkho_ncc_dh ck_dh in db.md_nhapkho_ncc_dh.Where(s => s.md_nhapkho_ncc_id ==check_nk.md_nhapkho_ncc_id & s.md_sanpham_id == dh.md_sanpham_id))
            {
                ck_dh.tong_sl_dat = ck_dh.tong_sl_dat - dh.sl_danhap;
            }
        }
        return id_new;
    }

    //--cap nhat vao kho
    public string add_kho(EntityContext db, md_nhapkho_ncc_dh dh, md_nhapkho_ncc px, User_TK us)
    {
        string id_new = Helper.getNewId();
        md_kho_sanpham check_khosp = db.md_kho_sanpham.Where(s => s.md_kho_id == px.kho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
        md_kho_sanpham sp = check_khosp;
        if (sp == null)
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

        //Kiem tra
        if (check_khosp == null)
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
    public string add_kho_ls(EntityContext db, md_nhapkho_ncc_dh dh, md_nhapkho_ncc px, User_TK us)
    {
        string id_new = Helper.getNewId();
        string abc = "";
        if (dh.sl_nhap == 0)
        {
            abc = "abc";
        }
        else
        {
            md_kho_giaodich giao = new md_kho_giaodich();
            giao.md_kho_giaodich_id = id_new;
            giao.md_kho_id = px.kho;
            giao.md_sanpham_id = dh.md_sanpham_id;
            giao.soluong_dichchuyen = dh.sl_nhap;
            giao.ngaychuyen = DateTime.Now;
            giao.kieuchuyen = "Nhập kho";
            //giao.dongnhapnhap = px.sochungtu;
            //giao.dongkiemkho = "";
            //giao.dongvanchuyen = "";
            giao.dongnhapxuat = px.sochungtu;

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
            dh.sl_nhap = 0;
        }
        return abc;
    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            if (msg.Length <= 0)
            {
                string action = "add";
                string[] column_ex = { };
                string ten_table = "md_nhapkho_ncc";
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, id_new, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Thêm thành công." + "#" + id_new;
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
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            md_nhapkho_ncc object_ = db.md_nhapkho_ncc.Where(p => p.md_nhapkho_ncc_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }
            else if (object_.trangthai != "SOANTHAO")
            {
                msg = "false#Lỗi:Đã nhập kho";
            }

            if (msg.Length <= 0)
            {
                string action = "edit";
                string[] column_ex = { };
                string ten_table = "md_nhapkho_ncc";
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
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
        string msg = "",  msg_del = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string ten_table = "md_nhapkho_ncc";
            int count = context.Request.Form["id"].Split(',').Count();
            string[] id_del = new string[count];
            id_del = context.Request.Form["id"].Split(',');
            string c_donmuahang_id = "";
            for (int i = 0; i < count; i++)
            {
                 msg_del = ""; var id_del_ = id_del[i];
                md_nhapkho_ncc object_ = db.md_nhapkho_ncc.Where(p => p.md_nhapkho_ncc_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg_del = "Lỗi:Không tìm thấy đối tượng cần xóa " + i;
                    msg += msg_del + "\n";
                }
                if (object_.phieuXNNK != null & object_.phieuXNNK != "")
                {
                    msg_del = "Lỗi:Phiếu nhập kho \"" + object_.sochungtu + "\" đã in xác nhận nhập hàng, không thể chỉnh sửa.";
                    msg += msg_del + "\n";
                }
                if (msg_del.Length <= 0)
                {
                    c_donmuahang_id += object_.c_donmuahang_id + ",";
                    foreach (md_nhapkho_ncc_dh nk_ncc_dh in db.md_nhapkho_ncc_dh.Where(s => s.md_nhapkho_ncc_id == object_.md_nhapkho_ncc_id))
                    {
                        c_donmuahang_cdmh dmh_cdmh = db.c_donmuahang_cdmh.Where(s => s.md_sanpham_id == nk_ncc_dh.md_sanpham_id & s.c_donmuahang_id == object_.c_donmuahang_id).Take(1).FirstOrDefault();
                        if (dmh_cdmh != null)
                            dmh_cdmh.sl_hanngach += nk_ncc_dh.tong_sl_dat;
                    }
                    db.md_nhapkho_ncc.Remove(object_);
                }
            }

            if (msg.Length <= 0)
            {
                db.SaveChanges();
                string[] vnn_donhang_id = c_donmuahang_id.Split(',');
                foreach (c_donmuahang dmh in db.c_donmuahang.Where(s => vnn_donhang_id.Contains(s.c_donmuahang_id)))
                {
                    if (db.md_nhapkho_ncc.Where(s => s.c_donmuahang_id == dmh.c_donmuahang_id).Count() <= 0)
                        dmh.phieunhapkho = " ";
                }
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
            if (ex.Message.ToUpper().Contains("REFERENCE"))
            {
                msg = "false#Lỗi: Đang được sử dụng, không thể xóa";
            }
            else
            {
                msg = "false#Lỗi: " + ex.Message;
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