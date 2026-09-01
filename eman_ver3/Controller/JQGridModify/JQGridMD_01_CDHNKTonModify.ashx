<%@ WebHandler Language="C#" Class="JQGridMD_01_DHDNhapModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_DHDNhapModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA01CapnhatSLNKNB_MD01CDHNKTon":
                this.CA01CapnhatSLNKNB_MD01CDHNKTon(context);
                break;
            default:
                break;
        }
    }

    public void CA01CapnhatSLNKNB_MD01CDHNKTon(HttpContext context)
    {
        string sel_val = context.Request.Form["sel_val"];
        decimal slvc_case = decimal.Parse(context.Request.Form["slvc_case"]);
        string[] vnn = context.Request.Form["id"].Split(',');
        string msg = "";
        foreach (var nknb_cdh in db.md_nhapkhoton_cdh.Where(s => vnn.Contains(s.md_nhapkhoton_cdh_id)).ToList())
        {
            var vcnb = db.md_nhapkhoton.Where(s => s.md_nhapkhoton_id == nknb_cdh.md_nhapkhoton_id).FirstOrDefault();
            if(vcnb.trangthai == "HIEULUC")
            {
                msg = "<div style='color:red'>Lỗi:Dòng " + vcnb.sochungtu + " đã Hiệu lực.</div>";
                break;
            }
            else {
                decimal sltd = nknb_cdh.tong_sl_dat.Value - nknb_cdh.sl_danhap.Value;
                if (sel_val == "0")
                {
                    nknb_cdh.sl_nhap = sltd;
                }
                else {
                    if (slvc_case <= sltd)
                    {
                        nknb_cdh.sl_nhap = slvc_case;
                    }
                    else
                    {
                        var sp = db.md_sanpham.Where(s => s.md_sanpham_id == nknb_cdh.md_sanpham_id).Take(1).FirstOrDefault();
                        msg = "<div style='color:red'>Lỗi: Dòng \"" + sp.ma_sanpham + "\" chỉ có thể chuyển tối đa là: " + VNN_VariablePublic.autoRound(sltd, 4) + "</div>";
                        break;
                    }
                }
            }
        }
        if (msg == "")
        {
            msg = "<div style='color:blue'>Cập nhật số lượng dự kiến nhập kho thành công</div>";
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
        string md_donvitinhsanpham_id = context.Request.Form["md_donvitinhsanpham_id"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id = context.Request.Form["id"];
                var object_ = db.md_nhapkhoton_cdh.Where(p => p.md_nhapkhoton_cdh_id == id).Take(1).FirstOrDefault();
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == object_.md_sanpham_id).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (sl_nhap < 0)
                {
                    msg = "Số lượng xuất phải lớn hớn 0";
                }
                else if (sl_nhap > object_.tong_sl_dat)
                {
                    msg = "Chỉ có thể nhập tối đa là " + object_.tong_sl_dat.GetValueOrDefault(0).DropTrailingZeros();
                }

                foreach (var ncc in db.md_nhapkhoton.Where(s => s.md_nhapkhoton_id == object_.md_nhapkhoton_id).ToList())
                {
                    if (ncc.trangthai == "HIEULUC")
                    {
                        msg = "Đơn hàng đã hiệu lực !";
                    }
                }

                if (msg.Length <= 0)
                {
                    VNN_Function.SetFormValue(object_.nameof(s => s.md_sanpham_id), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s => s.tong_sl_dat), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s => s.sl_nhap), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s => s.md_donvitinhsanpham_id), "VNN_notpost");
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