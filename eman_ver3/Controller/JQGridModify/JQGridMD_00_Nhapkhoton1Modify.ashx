<%@ WebHandler Language="C#" Class="JQGridMD_00_Nhapkhoton1Modify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_Nhapkhoton1Modify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA01NhapKho_MD00Nhapkhoton1":
                this.CA01NhapKho_MD00Nhapkhoton1(context);
                break;
            default:
                break;
        }
    }

    public void CA01NhapKho_MD00Nhapkhoton1(HttpContext context)
    {
        string msg = "", msg_success = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        string[] vnn = id.Split(',');
        int count_slnhap = 0;
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                //Xu Ly nhap kho

                foreach (var nk in db.md_nhapkhoton.Where(s => s.md_nhapkhoton_id == id).ToList())
                {
                    if (nk.trangthai == "HIEULUC")
                    {
                        msg = "<div style='color:red'>Lỗi: dòng \"" + nk.sochungtu + "\" đã hiệu lực.</div>";
                        break;
                    }
                    foreach (var dh in db.md_nhapkhoton_cdh.Where(s => s.md_nhapkhoton_id == nk.md_nhapkhoton_id).ToList())
                    {
                        var sp = db.md_sanpham.Where(s => s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
                        decimal nhanvoi = 1;
                        if (string.IsNullOrEmpty(nk.denkho) | nk.denkho == " ")
                        {
                            msg += "<div style='color:red'>Lỗi: dòng \"" + nk.sochungtu + "\" chưa có kho đến.</div>";
                        }
                        else if (dh.sl_nhap < 0)
                        {
                            msg += "<div style='color:red'>Lỗi: \"" + sp.ma_sanpham + "\" Số lượng nhập phải lớn hơn 0 .</div>";
                        }

                        if (msg.Length <= 0 & dh.sl_nhap > 0)
                        {
                            count_slnhap = 1;
                            decimal sl_nhap = dh.sl_nhap.Value;
                            //--cap nhat vao kho
                            nhap_kho(db, dh, nk, nhanvoi, userTK);
                            db.SaveChanges();
                            //--tao lich su nhap kho
                            add_kho_ls(db, dh, nk, nhanvoi, userTK);
                            db.SaveChanges();
                            msg_success += "<div style='color:blue'>Dòng \"" + nk.sochungtu + " của \"" + sp.ma_sanpham + "\" đã nhập kho thành công, số lượng nhập là: \"" + sl_nhap.DropTrailingZeros() + "\"</div>";
                        }
                    }

                    if (count_slnhap <= 0)
                    {
                        msg += "<div style='color:red'>Dòng \"" + nk.sochungtu + " phải có ít nhất 1 dòng hàng có số lượng lớn hơn 0.</div>";
                    }

                    //Kiem tra de chuyen thanh Hieu Luc
                    if (msg.Length <= 0)
                    {
                        nk.trangthai = "HIEULUC";
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "false#" + ex.Message;
            }

            if(msg.Length <= 0)
            {
                msg = msg_success;
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    //nhap kho
    public string nhap_kho(EntityContext db, md_nhapkhoton_cdh dh, md_nhapkhoton nk, decimal nhanvoi, User_TK us)
    {
        string id_new = Helper.getNewId();
        var spServer = db.md_kho_sanpham.Where(s => s.md_kho_id == nk.denkho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
        var sp = db.md_kho_sanpham.Local.Where(s => s.md_kho_id == nk.denkho & s.md_sanpham_id == dh.md_sanpham_id).FirstOrDefault();
        var add = sp == null;
        dh.sl_tonkho = spServer == null ? 0 : spServer.soluong.GetValueOrDefault(0);
        if (add)
        {
            sp = new md_kho_sanpham();
            sp.md_kho_sanpham_id = id_new;
            sp.soluong = (dh.sl_nhap * nhanvoi);
        }
        else
        {
            id_new = sp.md_kho_sanpham_id;
            sp.soluong += (dh.sl_nhap * nhanvoi);
        }
        sp.md_kho_id = nk.denkho;
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
    public string add_kho_ls(EntityContext db, md_nhapkhoton_cdh dh, md_nhapkhoton nk, decimal nhanvoi, User_TK us)
    {
        string id_new = Helper.getNewId();
        md_kho_giaodich giao = new md_kho_giaodich();
        giao.md_kho_giaodich_id = id_new;
        giao.md_kho_id = nk.denkho;
        giao.md_sanpham_id = dh.md_sanpham_id;
        giao.soluong_dichchuyen = dh.sl_nhap * nhanvoi;
        giao.ngaychuyen = nk.ngaychuyen;
        giao.kieuchuyen = "Nhập kho";
        giao.dongnhapxuat = nk.sochungtu;
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

        dh.sl_danhap = dh.sl_nhap;
        return "";
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
        string md_lenhsanxuat_id = context.Request.Form["md_lenhsanxuat_id"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id = context.Request.Form["id"];
                var object_ = db.md_nhapkhoton.Where(p => p.md_nhapkhoton_id == id).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (object_.trangthai == "HIEULUC")
                {
                    msg = "Lỗi:Đối tượng đã nhập kho";
                }
                else
                {
                    VNN_Function.SetFormValue(object_.nameof(s => s.md_lenhsanxuat_id), md_lenhsanxuat_id);
                    VNN_Function.SetFormValue(object_.nameof(s => s.sochungtu), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s => s.nhaptu), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s => s.trangthai), "VNN_notpost");
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
                msg = "true#Cập nhật thành công.";
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
                    var object_ = db.md_nhapkhoton.Where(p => p.md_nhapkhoton_id == id_del_).Take(1).FirstOrDefault();
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
                        VNN_Function.Write_log(context, ma_module, null, oper, "PNKKT:" + object_.sochungtu, db);
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
                transaction.Commit();
                msg = @"true#Trả phiếu nhập kho đã chọn về ""Soạn Thảo"" thành công.";
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