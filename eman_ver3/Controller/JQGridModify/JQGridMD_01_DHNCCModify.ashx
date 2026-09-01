<%@ WebHandler Language="C#" Class="JQGridMD_01_DHNCCModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_DHNCCModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_CapnhatSLNKNCC":
                this.CA_01_CapnhatSLNKNCC(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_CapnhatSLNKNCC(HttpContext context)
    {
        string sel_val = context.Request.Form["sel_val"];
        decimal slvc_case = decimal.Parse(context.Request.Form["slvc_case"]);
        string[] vnn = context.Request.Form["id"].Split(',');
        string msg = "";
        foreach (var nk_ncc_dh in db.md_nhapkho_ncc_dh.Where(s => vnn.Contains(s.md_nhapkho_ncc_dh_id)).ToList())
        {
            var vcnb = db.md_nhapkho_ncc.Where(s => s.md_nhapkho_ncc_id == nk_ncc_dh.md_nhapkho_ncc_id).FirstOrDefault();
            var sp = db.md_sanpham.Where(s => s.md_sanpham_id == nk_ncc_dh.md_sanpham_id).Take(1).FirstOrDefault();
            if (vcnb.trangthai != "DANHAN")
            {
                msg = string.Format(@"<div style='color:red'>Lỗi:Dòng ""{0}"" không ở trạng thái ""Đã xác nhận"".</div>", vcnb.sochungtu);
                break;
            }
            else
            {
                decimal sltd = nk_ncc_dh.sl_muonnhap.GetValueOrDefault(0);
                decimal tile = 1;
                if (sp.md_donvitinhsanpham_id != nk_ncc_dh.md_donvitinhsanpham_id)
                {
                    string idDMH = db.md_nhapkho_ncc.Where(s => s.md_nhapkho_ncc_id == nk_ncc_dh.md_nhapkho_ncc_id).Select(s => s.c_donmuahang_id).FirstOrDefault();
                    var donghang = db.c_donmuahang_cdmh.Where(s =>
                        s.c_donmuahang_id == idDMH &
                        s.md_sanpham_id == sp.md_sanpham_id).FirstOrDefault();
                    if (donghang != null)
                    {
                        tile = donghang.sl_dadat2.GetValueOrDefault(0) / donghang.sl_dadat.Value;
                    }
                    else
                    {
                        msg = "false#Không tìm thấy dòng mua hàng.";
                    }
                }

                if (sel_val == "0")
                {
                    nk_ncc_dh.sl_nhap = sltd;
                    //nk_ncc_dh.sl_muonnhap = sltd;
                    nk_ncc_dh.sl_nhap2 = nk_ncc_dh.sl_nhap * tile;
                }
                else
                {
                    if (slvc_case <= sltd)
                    {
                        nk_ncc_dh.sl_nhap = slvc_case;
                        //nk_ncc_dh.sl_muonnhap = slvc_case;
                        nk_ncc_dh.sl_nhap2 = nk_ncc_dh.sl_nhap * tile;
                    }
                    else
                    {
                        msg = "<div style='color:red'>Lỗi: Dòng \"" + sp.ma_sanpham + "\" chỉ có thể chuyển tối đa là: " + sltd.DropTrailingZeros() + "</div>";
                        break;
                    }
                }
            }
        }
        if (msg == "")
        {
            msg = "<div style='color:blue'>Cập nhật số lượng nhập kho thành công</div>";
            db.SaveChanges();
        }
        context.Response.Write(msg);
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
        decimal tong_sl_dat = decimal.Parse(context.Request.Form["tong_sl_dat"]);
        decimal sl_nhap = decimal.Parse(context.Request.Form["sl_nhap"]);
        decimal sl_nhap2 = sl_nhap;

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id = context.Request.Form["id"];
                var object_ = db.md_nhapkho_ncc_dh.Where(p => p.md_nhapkho_ncc_dh_id == id).Take(1).FirstOrDefault();
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == object_.md_sanpham_id).FirstOrDefault();
                var vcnb = db.md_nhapkho_ncc.Where(s => s.md_nhapkho_ncc_id == object_.md_nhapkho_ncc_id).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (vcnb.trangthai != "DANHAN")
                {
                    msg = string.Format(@"Phiếu nhập kho không ở trạng thái ""Đã xác nhận""");
                }
                else if (sl_nhap < 0)
                {
                    msg = "Số lượng nhập không được âm";
                }
                else if (sl_nhap > tong_sl_dat)
                {
                    msg = "Số lượng thực nhập không được lớn hơn tổng số lượng";
                }
                else
                {
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_sanpham_id), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_donvitinhsanpham_id), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.sl_muonnhap), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.sl_nhap), sl_nhap.ToString());
                    VNN_Function.SetFormValue(object_.nameof(s=>s.sl_nhap2), sl_nhap2.ToString());
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