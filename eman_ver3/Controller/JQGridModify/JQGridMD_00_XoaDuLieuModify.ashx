<%@ WebHandler Language="C#" Class="JQGridMD_00_XoaDuLieuModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_XoaDuLieuModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_CapNhatHamXoa":
                this.CA_01_CapNhatHamXoa(context);
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
            string id = context.Request.QueryString["id"];
            if (msg.Length <= 0)
            {
                string action = context.Request.Params["oper"];
                string[] column_ex = { };
                string ten_table = "ad_remove";
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
            string id = context.Request.QueryString["id"];
            string object_ = db.ad_remove.Where(p => p.ad_remove_id == id).Select(s => s.ad_remove_id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }
            
            if (msg.Length <= 0)
            {
                string action = context.Request.Params["oper"];
                string ten_table = "ad_remove";
                string[] column_ex = { };
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
        string ma_module = context.Request.QueryString["ma_module"];
        string sql = @"select rm.ten_table from ad_remove rm where rm.ten_table not in 
            (SELECT vnn_col.TABLE_NAME
            FROM INFORMATION_SCHEMA.COLUMNS vnn_col
            left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE vnn_key
            on vnn_col.TABLE_NAME = vnn_key.TABLE_NAME and vnn_col.COLUMN_NAME = vnn_key.COLUMN_NAME
            left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS type_key
            on type_key.CONSTRAINT_NAME = vnn_key.CONSTRAINT_NAME
            where type_key.CONSTRAINT_TYPE = N'PRIMARY KEY')";

        System.Data.DataTable dt_sql = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        for (int i = 0; i < dt_sql.Rows.Count; i++)
        {
            sql = "delete from ad_remove where ten_table = '" + dt_sql.Rows[i][0] + "'";
            Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql);
        }
        VNN_Function.loaddulieu_Auto(db, ma_module);
    }

    public void CA_01_CapNhatHamXoa(HttpContext context)
    {
        EntityContext db = new EntityContext();
        ad_systemconfig ttc = db.ad_systemconfig.Where(s => s.ad_systemconfig_id != null).Take(1).FirstOrDefault();
        string ma_module = context.Request.QueryString["ma_module"];

        string notin = "";
        foreach (ad_remove rm in db.ad_remove.Where(s => s.hoatdong == true ).ToList())
        {
            notin += "'" + rm.ten_table + "',";
        }
        if (notin.Length > 0)
        {
            notin = "and ( vnn_col.TABLE_NAME not in (" + notin.Remove(notin.Length - 1) + "))";
        }
        
        string sql = @"SELECT vnn_col.TABLE_NAME,  vnn_col.COLUMN_NAME, type_key.CONSTRAINT_TYPE
            FROM INFORMATION_SCHEMA.COLUMNS vnn_col with (nolock)
            left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE vnn_key with (nolock)
            on vnn_col.TABLE_NAME = vnn_key.TABLE_NAME and vnn_col.COLUMN_NAME = vnn_key.COLUMN_NAME
            left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS type_key with (nolock)
            on type_key.CONSTRAINT_NAME = vnn_key.CONSTRAINT_NAME
            where type_key.CONSTRAINT_TYPE = N'PRIMARY KEY' "+ notin +" order by vnn_col.TABLE_NAME asc";

        System.Data.DataTable dt_sql = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        for (int i = 0; i < dt_sql.Rows.Count; i++)
        {
            string id_new = Helper.getNewId();
            string sapxep = VNN_Config.load_number(i.ToString(), 10);
            ad_remove rm = new ad_remove
            {
                ad_remove_id = id_new,
                ten_table = dt_sql.Rows[i][0].ToString(),
                ten_key = dt_sql.Rows[i][1].ToString(),
                sapxep = sapxep,
                nguoitao = Security.id_taikhoan(context),
                vaitrotao = Security.id_vaitro(context),
                bophantao = Security.id_phongban(context),
                nguoicapnhat = Security.id_taikhoan(context),
                vaitrocapnhat = Security.id_vaitro(context),
                bophancapnhat = Security.id_phongban(context),
                mota = "",
                hoatdong = true
            };
            db.ad_remove.Add(rm);
        }
		db.SaveChanges();
		
		sql = "select dbo.admin_rm_ad_remove()";
		dt_sql = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
		string table_db = dt_sql.Rows[0][0].ToString().Replace("'","''");
		sql = "exec [dbo].[admin_delete] N'ad_remove',N'and ten_table not in (" + table_db + ")'";	
		Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql);
        VNN_Function.loaddulieu_Auto(db, ma_module);
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
