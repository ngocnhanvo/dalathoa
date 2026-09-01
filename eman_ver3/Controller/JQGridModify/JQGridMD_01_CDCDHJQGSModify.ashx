<%@ WebHandler Language="C#" Class="JQGridMD_01_CDCDHJQGSModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_CDCDHJQGSModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            default:
                break;
        }
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
        string md_px_id = context.Request.Form["md_phanxuong_id"];
        string id = context.Request.Form["id"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var sl_phanphoi = decimal.Parse(context.Request.Form["sl_phanphoi"]);

                if (md_dtk_id != "")
                    md_dtk_id = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == md_dtk_id).Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();

                var object_ = db.c_kehoachdathang_cdhcd.Where(p => p.c_kehoachdathang_cdhcd_id == id).Take(1).FirstOrDefault();

                c_kehoachdathang kh = null;
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa.";
                }
                else if (sl_phanphoi < 0)
                {
                    msg = "Lỗi: Số lượng cần sản xuất không được âm.";
                }
                else if ((string.IsNullOrEmpty(md_dtk_id) | md_dtk_id == "") & (md_px_id == null | md_px_id == ""))
                {
                    msg = "Lỗi: Không thể bỏ trống cả 2 dữ liêu: Đối tác kinh doanh và phân xưởng sản xuất.";
                }
                else if (sl_phanphoi > object_.sl_candat)
                {
                    msg = "Lỗi: Số lượng cần đặt: " + object_.sl_candat + " (Lấy ít hơn hoặc bằng với số lượng này).";
                }
                else
                {
                    kh = db.c_kehoachdathang.FirstOrDefault(s => s.c_kehoachdathang_id == object_.c_kehoachdathang_id);
                    if (kh.trangthai != "SOANTHAO")
                        msg = "Lỗi: Dòng kế hoạch đặt hàng \"" + kh.ten_kh + "\" đã hiệu lực.";
                }

                if (msg.Length <= 0)
                {
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_doitackinhdoanh_id), md_dtk_id);
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_sanpham_id), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.sl_phanphoi), sl_phanphoi + "");
                    object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
                    db.SaveChanges();

                    //--sua sl_phanphoi va sl_candat
                    kh.xulykehoach = false;
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
        string ma_module = context.Request.QueryString["ma_module"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                foreach (var id_del_ in ids)
                {
                    var object_ = db.c_kehoachdathang_cdhcd.Where(p => p.c_kehoachdathang_cdhcd_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else
                    {
                        db.c_kehoachdathang_cdhcd.Remove(object_);
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
                msg = string.Format(@"true#Xóa thành công");
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
