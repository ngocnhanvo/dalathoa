<%@ WebHandler Language="C#" Class="JQGridMD_01_CDHDXuatKModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_CDHDXuatKModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA01CapnhatSLXBan_MD01CacDongThanhLy":
                this.CA01CapnhatSLXBan_MD01CacDongThanhLy(context);
                break;
            default:
                break;
        }
    }

    public void CA01CapnhatSLXBan_MD01CacDongThanhLy(HttpContext context)
    {
        string sel_val = context.Request.Form["sel_val"];
        decimal slvc_case = decimal.Parse(context.Request.Form["slvc_case"]);
        string[] vnn = context.Request.Form["id"].Split(',');
        string msg = "";
        foreach (var xb_cdh in db.md_xuatban_cdh.Where(s => vnn.Contains(s.md_xuatban_cdh_id)).ToList())
        {
            var vcnb = db.md_xuatban.Where(s => s.md_xuatban_id == xb_cdh.md_xuatban_id).FirstOrDefault();
            if(vcnb.trangthai == "HIEULUC")
            {
                msg = "<div style='color:red'>Lỗi:Dòng " + vcnb.sochungtu + " đã Hiệu lực.</div>";
                break;
            }
            else {
                decimal sltd = xb_cdh.tong_sl_xuat.Value - xb_cdh.sl_daxuat.Value;
                if (sel_val == "0")
                {
                    xb_cdh.sl_xuat = sltd;
                }
                else {
                    if (slvc_case <= sltd)
                    {
                        xb_cdh.sl_xuat = slvc_case;
                    }
                    else
                    {
                        var sp = db.md_sanpham.Where(s => s.md_sanpham_id == xb_cdh.md_sanpham_id).Take(1).FirstOrDefault();
                        msg = "<div style='color:red'>Lỗi: Dòng \"" + sp.ma_sanpham + "\" chỉ có thể chuyển tối đa là: " + sltd.DropTrailingZeros() + "</div>";
                        break;
                    }
                }
            }
        }
        if (msg == "")
        {
            msg = "<div style='color:blue'>Cập nhật số lượng dự kiến xuất kho thành công</div>";
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
        string md_donvitinhsanpham_id = context.Request.Form["md_donvitinhsanpham_id"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                decimal tong_sl_xuat = decimal.Parse(context.Request.Form["tong_sl_xuat"]);
                decimal sl_xuat = decimal.Parse(context.Request.Form["sl_xuat"]);
                string id = context.Request.Form["id"];
                var object_ = db.md_xuatban_cdh.Where(p => p.md_xuatban_cdh_id == id).Take(1).FirstOrDefault();
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == object_.md_sanpham_id).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (sl_xuat <= 0)
                {
                    msg = "Số lượng xuất phải lớn hớn 0";
                }
                else if (sl_xuat > object_.tong_sl_xuat)
                {
                    msg = "Chỉ có thể xuất tối đa là " + object_.tong_sl_xuat.GetValueOrDefault(0).DropTrailingZeros();
                }

                foreach (var ncc in db.md_xuatban.Where(s => s.md_xuatban_id == object_.md_xuatban_id).ToList())
                {
                    if (ncc.trangthai == "HIEULUC")
                    {
                        msg = "Phiếu xuất kho đã hiệu lực !";
                    }
                }

                if (msg.Length <= 0)
                {
                    VNN_Function.SetFormValue(object_.nameof(s=>s.ghichu_donvi2), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_sanpham_id), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.tenhang), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.tong_sl_xuat), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.sl_xuat), sl_xuat.ToString());
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