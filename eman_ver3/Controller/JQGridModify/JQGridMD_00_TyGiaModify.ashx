<%@ WebHandler Language="C#" Class="JQGridMD_00_TyGiaModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using DataAcess;
public class JQGridMD_00_TyGiaModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public string oper = "vnn";
    public void ProcessRequest(HttpContext context)
    {
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
            case "CA_01_UpdateTyGia":
                this.CA_01_UpdateTyGia(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_UpdateTyGia(HttpContext context)
    {
        EntityContext db = new EntityContext();
        String str = VNN_VariablePublic.connectString_Anco(db);
        SqlConnection cnn = new SqlConnection(str);
        SqlCommand cmd = new SqlCommand(@"select 
		  [md_tygia_id]
		  ,[ten_tygia]
		  ,[tu_dongtien_id]
		  ,[sang_dongtien_id]
		  ,[hieuluc_tungay]
		  ,[hieuluc_denngay]
		  ,[nhan_voi]
		  ,[chia_cho]
		  ,[ngaytao]
          ,[nguoitao]
		  ,[ngaycapnhat]
		  ,[nguoicapnhat]
		  ,[mota]
		  ,[hoatdong]
		from md_tygia order by md_tygia_id", cnn);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        int count_col = 0;
        string sql_column = "(";
        foreach (System.Data.DataColumn col in dt.Columns)
        {
            count_col++;
            sql_column += col.ColumnName + ",";
        }
        if (sql_column != "(") { sql_column = sql_column.Remove(sql_column.Length - 1) + ")"; }
        db.md_tygia.RemoveRange(db.md_tygia);
        db.SaveChanges();
        foreach (System.Data.DataRow row in dt.Rows)
        {

            string sql_insert = "insert into md_tygia" + sql_column + " values(";
            for (int i = 0; i < count_col; i++)
            {
                sql_insert += "N'" + row[i].ToString().Replace("'", "''") + "',";
            }
            sql_insert = sql_insert.Remove(sql_insert.Length - 1) + ")";
            Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql_insert);
        }

    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.QueryString["id"];

            string tu_dongtien_id = context.Request.Form["tu_dongtien_id"];
            string sang_dongtien_id = context.Request.Form["sang_dongtien_id"];



            if (tu_dongtien_id == "USD")
            {
                tu_dongtien_id = "385ec93024915838c98ef66e58b02e9b";
            }
            else
            {
                tu_dongtien_id = "964dd487f30e799f585cfab3ec5a178e";
            }

            if (sang_dongtien_id == "USD")
            {
                sang_dongtien_id = "385ec93024915838c98ef66e58b02e9b";
            }
            else
            {
                sang_dongtien_id = "964dd487f30e799f585cfab3ec5a178e";
            }

            if (msg.Length <= 0)
            {
                string action = "add";
                string[] column_ex = { "tu_dongtien_id", "sang_dongtien_id" };
                string ten_table = "md_tygia";
                VNN_Function.SetFormValue("tu_dongtien_id", tu_dongtien_id);
                VNN_Function.SetFormValue("sang_dongtien_id", sang_dongtien_id);
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
            string tu_dongtien_id = context.Request.Form["tu_dongtien_id"];
            string sang_dongtien_id = context.Request.Form["sang_dongtien_id"];

            if (tu_dongtien_id == "USD")
            {
                tu_dongtien_id = "385ec93024915838c98ef66e58b02e9b";
            }
            else
            {
                tu_dongtien_id = "964dd487f30e799f585cfab3ec5a178e";
            }

            if (sang_dongtien_id == "USD")
            {
                sang_dongtien_id = "385ec93024915838c98ef66e58b02e9b";
            }
            else
            {
                sang_dongtien_id = "964dd487f30e799f585cfab3ec5a178e";
            }

            string id = context.Request.Form["id"];
            string object_ = db.md_tygia.Where(p => p.md_tygia_id == id).Select(s => s.md_tygia_id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }

            if (msg.Length <= 0)
            {
                string action = "edit";
                string[] column_ex = { "tu_dongtien_id", "sang_dongtien_id" };
                string ten_table = "md_tygia";
                VNN_Function.SetFormValue("tu_dongtien_id", tu_dongtien_id);
                VNN_Function.SetFormValue("sang_dongtien_id", sang_dongtien_id);
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
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                foreach (var id_del_ in ids)
                {
                    var object_ = db.md_tygia.Where(p => p.md_tygia_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else
                    {
                        VNN_Function.Write_log(context, ma_module, null, oper, "TG:" + object_.ten_tygia, db);
                        db.md_tygia.Remove(object_);
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
                msg = @"true#Xóa tỷ giá đã chọn thành công.";
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
