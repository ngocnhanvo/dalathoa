<%@ WebHandler Language="C#" Class="JQGridMD_01_DDHPXModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_DDHPXModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
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
            default:
                break;
        }
    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "",id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            if (msg.Length <= 0)
            {
                string action = "add";
                string[] column_ex = { };
                string ten_table = "c_nhucauvattu_ddhpx";
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, id_new, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Thêm thành công." + "#" + id_new;
            }
        }
        catch(Exception ex)
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
            string object_ = db.c_nhucauvattu_ddhpx.Where(p => p.c_nhucauvattu_ddhpx_id == id).Select(s => s.c_nhucauvattu_ddhpx_id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }
            
            if (msg.Length <= 0)
            {
                string action = "edit";
                string[] column_ex = { };
                string ten_table = "c_nhucauvattu_ddhpx";
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Cập nhật thành công.";
            }
        }
        catch(Exception ex)
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
		string id = "";
        try
        {
            string ten_table = "c_nhucauvattu_dhpx";
            int count = context.Request.Form["id"].Split(',').Count();
            string[] id_del = new string[count];
            id_del = context.Request.Form["id"].Split(',');
			
            for (int i = 0; i < count; i++)
            {
                 msg_del = ""; var id_del_ = id_del[i];
                c_nhucauvattu_dhpx object_ = db.c_nhucauvattu_dhpx.Where(p => p.c_nhucauvattu_dhpx_id == id_del_).Take(1).FirstOrDefault();
				c_nhucauvattu nc = db.c_nhucauvattu.FirstOrDefault(s => s.c_nhucauvattu_id == object_.c_nhucauvattu_id);
                if (object_ == null)
                {
                    msg_del = "Lỗi:Không tìm thấy đối tượng cần xóa " + i;
                    msg += msg_del + "\n";
                }
				else if(nc.c_yeucaumuavt_id  != null & nc.c_yeucaumuavt_id  != "" & nc.c_yeucaumuavt_id  != " ")
				{
					msg_del = "Lỗi:Đã tạo yêu cầu : \"" + nc.c_yeucaumuavt_id +"\" không thể xóa " + i;
                    msg += msg_del + "\n";
				}
                if (msg_del.Length <= 0)
                {
					id = object_.c_nhucauvattu_id;
                    db.c_nhucauvattu_dhpx.Remove(object_);
                }
            }
           
			c_nhucauvattu ncvt = db.c_nhucauvattu.Where(s=>s.c_nhucauvattu_id == id).Take(1).FirstOrDefault();
			if(ncvt == null & msg == "")
				msg = "Lỗi:Không tìm thấy nhu cầu vật tư.";
            
			if (msg.Length <= 0)
            {
				db.c_nhucauvattu_ddhpx.RemoveRange(db.c_nhucauvattu_ddhpx.Where(s=>s.c_nhucauvattu_id == id));
				db.c_nhucauvattu_ycmvt.RemoveRange(db.c_nhucauvattu_ycmvt.Where(s=>s.c_nhucauvattu_id == id));
				ncvt.datinh_nhucau = false;
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
