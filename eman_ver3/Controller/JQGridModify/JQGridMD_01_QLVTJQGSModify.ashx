<%@ WebHandler Language="C#" Class="JQGridMD_01_QLVTJQGSModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_QLVTJQGSModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
		string ad_user_id = context.Request.Form["ad_user_id"];
        try
        {
            string id = context.Request.Form["id"];
			md_chungloai_ql ql = db.md_chungloai_ql.Where(s => s.md_chungloai_id == id & s.ad_user_id == ad_user_id).FirstOrDefault();
			if(ql != null)
			{
				msg = "false#Tài khoản đã tồn tại.";
			}
            if (msg.Length <= 0)
            {
                string action = "add";
                string[] column_ex = { "md_chungloai_id" };
                string ten_table = "md_chungloai_ql";
				VNN_Function.SetFormValue("md_chungloai_id", id);
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
		string ad_user_id = context.Request.Form["ad_user_id"];
        try
        {
            string id = context.Request.Form["id"];
            md_chungloai_ql object_ = db.md_chungloai_ql.Where(p => p.md_chungloai_ql_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }
			
			md_chungloai_ql ql = db.md_chungloai_ql.Where(s => s.md_chungloai_ql_id != object_.md_chungloai_ql_id & s.md_chungloai_id == id & s.ad_user_id == ad_user_id).FirstOrDefault();
			if(ql != null)
			{
				msg = "false#Tài khoản đã tồn tại.";
			}
			
            
            if (msg.Length <= 0)
            {
                string action = "edit";
                string[] column_ex = { "md_chungloai_id" };
                string ten_table = "md_chungloai_ql";
				VNN_Function.SetFormValue("md_chungloai_id", id);
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
        try
        {
            string ten_table = "md_chungloai_ql";
            int count = context.Request.Form["id"].Split(',').Count();
            string[] id_del = new string[count];
            id_del = context.Request.Form["id"].Split(',');
            for (int i = 0; i < count; i++)
            {
                 msg_del = ""; var id_del_ = id_del[i];
                md_chungloai_ql object_ = db.md_chungloai_ql.Where(p => p.md_chungloai_ql_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg_del = "Lỗi:Không tìm thấy đối tượng cần xóa " + i;
                    msg += msg_del + "\n";
                }
                if (msg_del.Length <= 0)
                {
                     db.md_chungloai_ql.Remove(object_);
                }
            }
            if (msg.Length <= 0)
            {
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
