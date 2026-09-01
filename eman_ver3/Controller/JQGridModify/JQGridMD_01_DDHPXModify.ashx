<%@ WebHandler Language="C#" Class="JQGridMD_01_DDHPXModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_DDHPXModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        string msg = "", msg_del = "", msg_success = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {

            var iddels = context.Request.Form["id"].Split(',');
            var object_s = db.c_nhucauvattu_dhpx.Where(p => iddels.Contains(p.c_nhucauvattu_dhpx_id)).ToList();
            if(object_s.Count <= 0)
            {
                msg = $"Không tìm thấy các đơn hàng đã chọn";
                goto EndEventHandler;
            }
            var ncvtid = object_s.Select(s => s.c_nhucauvattu_id).FirstOrDefault();
            var ncvt = db.c_nhucauvattu.Where(s => s.c_nhucauvattu_id == ncvtid).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(ncvt.c_yeucaumuavt_id))
            {
                msg = $"Lỗi: Đã tạo yêu cầu mua vật tư";
                goto EndEventHandler;
            }

            var ncvtDHPXs = db.c_nhucauvattu_dhpx.Where(s => s.c_nhucauvattu_id == ncvt.c_nhucauvattu_id).ToList();
            if(ncvtDHPXs.Count <= 1)
            {
                msg = $"NCVT phải có ít nhất 1 đơn hàng";
                goto EndEventHandler;
            }

            foreach (var object_ in object_s)
            {
                db.c_nhucauvattu_dhpx.Remove(object_);
                var khdh = db.c_kehoachdathang.Where(s =>
                    s.c_kehoachdathang_id == object_.c_kehoachdathang_id).FirstOrDefault();
                if (khdh != null)
                    khdh.tinhNCVT = false;
            }

            if (msg.Length <= 0)
            {
                db.c_nhucauvattu_ddhpx.RemoveRange(db.c_nhucauvattu_ddhpx.Where(s => s.c_nhucauvattu_id == ncvt.c_nhucauvattu_id));
                db.c_nhucauvattu_ycmvt.RemoveRange(db.c_nhucauvattu_ycmvt.Where(s => s.c_nhucauvattu_id == ncvt.c_nhucauvattu_id));
                ncvt.datinh_nhucau = false;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = "true#Xóa thành công";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
            msg = $"false#Lỗi: {msg}";

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
