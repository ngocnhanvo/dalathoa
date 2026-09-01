<%@ WebHandler Language="C#" Class="JQGridMD_01_ColumnImportModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;

public class JQGridMD_01_ColumnImportModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
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
            case "CA_01_CapnhatColumn":
                this.CA_01_CapnhatColumn(context);
                break;
            case "CA_01_ImportColumn":
                this.CA_01_ImportColumn(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_ImportColumn(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string id = context.Request.Form["id"];
        string ip_column = context.Request.Form["ip_column"];
        string id_parent = context.Request.Form["id_parent"];
        if (msg.Length <= 0)
        {
            string[] id_per = id.Split(',');
            for (int nht = 0; nht < id_per.Count(); nht++)
            {
                string cur_id = id_per[nht];
                if (ip_column == "rdi_addip")
                {
                    foreach (ad_import_column col in db.ad_import_column.Where(s => s.ad_import_id == id_parent & s.ad_import_column_id == cur_id).ToList())
                    {
                        col.imported = "True";
                    }
                }
                else if (ip_column == "rdi_delip")
                {
                    foreach (ad_import_column col in db.ad_import_column.Where(s => s.ad_import_id == id_parent & s.imported == "true" & s.ad_import_column_id == cur_id).ToList())
                    {
                        col.imported = "False";
                        col.select_sql = "";
                    }
                }
                db.SaveChanges();

                int i = 0;
                foreach (ad_import_column col in db.ad_import_column.Where(s => s.ad_import_id == id_parent & s.imported == "true").OrderBy(s => s.sapxep).ToList())
                {
                    if (col.select_sql == "" | col.select_sql == null)
                    {
                        col.select_sql = "'{" + i + "}'";
                        col.select_sql_cp = "'{" + i + "}'";
                    }
                    else
                    {
                        if (col.select_sql_cp.Length > 0)
                        {
                            col.select_sql = col.select_sql.Replace(col.select_sql_cp, "'{" + i + "}'");
                            col.select_sql_cp = "'{" + i + "}'";
                        }
                    }
                    i++;
                }
            }
            db.SaveChanges();
        }
        context.Response.Write(msg);
    }

    public void CA_01_CapnhatColumn(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string id = context.Request.Form["id"];
        User_TK us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context));
        ad_import ip = db.ad_import.Where(s => s.ad_import_id == id).Take(1).FirstOrDefault();
        if (ip != null)
        {
            string sql = string.Format(@"SELECT vnn_col.COLUMN_NAME, type_key.CONSTRAINT_TYPE
            FROM INFORMATION_SCHEMA.COLUMNS vnn_col
            left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE vnn_key
            on vnn_col.TABLE_NAME = vnn_key.TABLE_NAME and vnn_col.COLUMN_NAME = vnn_key.COLUMN_NAME
            left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS type_key
			on type_key.CONSTRAINT_NAME = vnn_key.CONSTRAINT_NAME
            WHERE vnn_col.TABLE_NAME = '{0}'", ip.ten_table);
            System.Data.DataTable dt = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
            int i = 0;
            string ma_column = "";
            foreach (System.Data.DataRow row in dt.Rows)
            {
                ma_column += row[0].ToString() + ";";
                string ma_import = row[0].ToString();
                ad_import_column chk_ip_col = db.ad_import_column.Where(s => s.ad_import_id == ip.ad_import_id & s.ma_import_column == ma_import).Take(1).FirstOrDefault();
                if (chk_ip_col == null)
                {
                    string select_sql = "";
                    if (row[1].ToString() == "PRIMARY KEY")
                    {
                        select_sql = "Replace(newid(),'-','')";
                    }
                    else if (row[0].ToString() == "nguoitao" | row[0].ToString() == "nguoicapnhat")
                    {
                        select_sql = "'@ad_user_id'";
                    }
                    else if (row[0].ToString() == "vaitrotao" | row[0].ToString() == "vaitrocapnhat")
                    {
                        select_sql = "'@ad_role_id'";
                    }
                    else if (row[0].ToString() == "bophantao" | row[0].ToString() == "bophancapnhat")
                    {
                        select_sql = "'@ad_role_id'";
                    }
                    else if (row[0].ToString() == "value_nguoitao" | row[0].ToString() == "value_nguoicapnhat")
                    {
                        select_sql = "'@ma_user'";
                    }
                    else if (row[0].ToString() == "value_vaitrotao" | row[0].ToString() == "value_vaitrocapnhat")
                    {
                        select_sql = "'@ten_role'";
                    }
                    else if (row[0].ToString() == "value_bophantao" | row[0].ToString() == "value_bophancapnhat")
                    {
                        select_sql = "'@ten_phongban'";
                    }
                    else if (row[0].ToString() == "ngaytao" | row[0].ToString() == "ngaycapnhat")
                    {
                        select_sql = "getdate()";
                    }
                    else if (row[0].ToString() == "hoatdong")
                    {
                        select_sql = "1";
                    }

                    ad_import_column ip_col = new ad_import_column
                    {
                        ad_import_column_id = Helper.getNewId(),
                        ad_import_id = ip.ad_import_id,
                        ma_import_column = row[0].ToString(),
                        ten_import_column = "",
                        select_sql = select_sql,
                        select_sql_cp = "",
                        sapxep = VNN_Config.load_number(i.ToString(), 10),
                        imported = "False",
                        primary_key = "False",
                        nguoitao = us.ad_user_id,
                        vaitrotao = us.ad_role_id,
                        bophantao = us.md_phongban_id,
                        value_nguoitao = us.ma_user,
                        value_vaitrotao = us.ten_role,
                        value_bophantao = us.ten_phongban,

                        nguoicapnhat = us.ad_user_id,
                        vaitrocapnhat = us.ad_role_id,
                        bophancapnhat = us.md_phongban_id,
                        value_nguoicapnhat = us.ma_user,
                        value_vaitrocapnhat = us.ten_role,
                        value_bophancapnhat = us.ten_phongban,
                        mota = "",
                        hoatdong = true
                    };
                    db.ad_import_column.Add(ip_col);
                }
                i++;
            }
            db.SaveChanges();
            foreach (ad_import_column col in db.ad_import_column.Where(s => s.ad_import_id == ip.ad_import_id).ToList().Where(s => !ma_column.Contains(s.ma_import_column)))
            {
                db.ad_import_column.Remove(col);
            }
            db.SaveChanges();
        }
    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id_parent"];

            if (msg.Length <= 0)
            {
                string action = "add";
                string[] column_ex = { };
                string ten_table = "ad_import_column";
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
        //try
        {
            string id = context.Request.Form["id_parent"];
            string id_ = context.Request.Form["id"];
            string select_sql = context.Request.Form["select_sql"];
            string ma_import_column = context.Request.Form["ma_import_column"];
            ad_import ip = db.ad_import.Where(s => s.ad_import_id == id).Take(1).FirstOrDefault();
            string object_ = db.ad_import_column.Where(p => p.ad_import_column_id == id_).Select(s => s.ad_import_column_id).Take(1).FirstOrDefault();
            string check_sql = check_SQL(ip.ten_table, select_sql, ma_import_column);
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string checkSQL = context.Request.Form["checksql"];
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa.";
            }
            else if (ma_import_column.Trim() == "")
            {
                msg = "false#Lỗi: Mã column không được bỏ trống.";
            }
            else if (check_sql != "" & checkSQL != "0")
            {
                msg = "false#Cú pháp sql sai: " + check_sql;
            }

            if (msg.Length <= 0)
            {
                string action = "edit";
                string[] column_ex = { };
                string ten_table = "ad_import_column";

                VNN_Function.SetFormValue("sapxep", sapxep);
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Cập nhật thành công.";
                VNN_Function.SortColumn(ten_table, sapxep, "ad_import_id", id, "ma_import_column", ma_import_column, "");
            }
        }
        //catch(Exception ex)
        {
            // msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", msg_del = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string ten_table = "ad_import_column";
            int count = context.Request.Form["id"].Split(',').Count();
            string[] id_del = new string[count];
            id_del = context.Request.Form["id"].Split(',');
            for (int i = 0; i < count; i++)
            {
                msg_del = "";
                string id_delI = id_del[i];
                string object_ = db.ad_import_column.Where(p => p.ad_import_column_id.Equals(id_delI)).Select(s => s.ad_import_column_id).Take(1).FirstOrDefault();
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
                    VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);
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

    public string check_SQL(string ten_table, string select_sql, string ma_import_column)
    {
        string kq = "";
        if (select_sql.Replace(" ", "") == "") { select_sql = "''"; }
        string sql = "select " + select_sql + " as " + ma_import_column + " from " + ten_table;
        if (VNN_Function.TestSQL(sql) == false)
        {
            kq = sql;
        }
        return kq;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
