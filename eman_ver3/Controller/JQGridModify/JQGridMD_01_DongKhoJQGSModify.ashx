<%@ WebHandler Language="C#" Class="JQGridMD_01_DongKhoModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_DongKhoModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            string id_parent = context.Request.Form["id_parent"];
			string ma_sanpham = context.Request.Form["md_sanpham_id"];
			md_sanpham sp = db.md_sanpham.Where(s=>s.ma_sanpham == ma_sanpham).Take(1).FirstOrDefault();
			string ngaytinh_tonkhocuoi = context.Request.Form["ngaytinh_tonkhocuoi"];
			DateTime nttkc = VNN_Config.setDateTime(ngaytinh_tonkhocuoi);

			
			if(nttkc == DateTime.MinValue.AddDays(1)){
				msg = "false#Lỗi: Ngày \"" + ngaytinh_tonkhocuoi + "\" không đúng định dạng.";
			}
            else if(sp == null){
				msg = "false#Lỗi: Sản phâm \"" + ma_sanpham + "\" không tồn tại." + nttkc;
			}
			else{
				string chk_sp = db.md_kho_sanpham.Where(s=>s.md_sanpham_id == sp.md_sanpham_id & s.md_kho_id == id).Select(s=>s.md_sanpham_id).Take(1).FirstOrDefault();
				if(chk_sp != null & chk_sp != ""){
					msg = "false#Lỗi: Mã \"" + ma_sanpham + "\" đã có trong kho này.";
				}
			}
			
			if (msg.Length <= 0)
            {
				if(nttkc == DateTime.MinValue){ VNN_Function.SetFormValue("ngaytinh_tonkhocuoi", null); }
                string action = "add";
                string[] column_ex = { "md_kho_id" };
                string ten_table = "md_kho_sanpham";
				VNN_Function.SetFormValue("md_kho_id", id);
				VNN_Function.SetFormValue("md_sanpham_id", sp.md_sanpham_id);
				VNN_Function.SetFormValue("md_kho_id", id_parent);
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
			string ma_sanpham = context.Request.Form["md_sanpham_id"];
            md_kho_sanpham object_ = db.md_kho_sanpham.Where(p => p.md_kho_sanpham_id == id).Take(1).FirstOrDefault();
            md_sanpham sp = db.md_sanpham.Where(s=>s.ma_sanpham == ma_sanpham).Take(1).FirstOrDefault();
			//string ngaytinh_tonkhocuoi = context.Request.Form["ngaytinh_tonkhocuoi"];
			//DateTime nttkc = VNN_Config.setDateTime(ngaytinh_tonkhocuoi);
			
			if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }
			else if(sp == null){
				msg = "false#Lỗi: Sản phâm \"" + ma_sanpham + "\" không tồn tại.";
			}
			else{
				string chk_sp = db.md_kho_sanpham.Where(s=>s.md_sanpham_id == sp.md_sanpham_id & s.md_kho_id == id & s.md_sanpham_id != object_.md_sanpham_id).Select(s=>s.md_sanpham_id).Take(1).FirstOrDefault();
				if(chk_sp != null & chk_sp != ""){
					msg = "false#Lỗi: Mã \"" + ma_sanpham + "\" đã có trong kho này.";
				}
			}
			
            if (msg.Length <= 0)
            {
                string action = "edit";
                string[] column_ex = { };
                string ten_table = "md_kho_sanpham";		
				VNN_Function.SetFormValue("md_sanpham_id", sp.md_sanpham_id);
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
            string ten_table = "md_kho_sanpham";
            int count = context.Request.Form["id"].Split(',').Count();
            string[] id_del = new string[count];
            id_del = context.Request.Form["id"].Split(',');
            for (int i = 0; i < count; i++)
            {
                 msg_del = ""; var id_del_ = id_del[i];
string object_ = db.md_kho_sanpham.Where(p => p.md_kho_sanpham_id == id_del_).Select(s => s.md_kho_sanpham_id).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg_del = "Lỗi:Không tìm thấy đối tượng cần xóa " + i;
                    msg += msg_del + "\n";
                }
                if (msg_del.Length <= 0)
                {
                    string action = "del";
                    string[] column_ex = { };
                    VNN_Function.SetFormValue("id", object_);
                    VNN_Function.Set_DefaultvalueColumn(context, action);
                    VNN_Function.Modify_Function(context, ma_module, null, ten_table , action, column_ex, db);
                }
            }
            VNN_Function.loaddulieu_Auto(db, ma_module);
            if (msg.Length <= 0)
            {
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
