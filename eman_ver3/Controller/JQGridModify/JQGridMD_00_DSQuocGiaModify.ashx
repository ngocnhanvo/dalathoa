<%@ WebHandler Language="C#" Class="JQGridMD_00_DSQuocGiaModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using DataAcess;
public class JQGridMD_00_DSQuocGiaModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_UpdateQG":
                this.CA_01_UpdateQG(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_UpdateQG(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string str = VNN_VariablePublic.connectString_Anco(db);
        string start = context.Request.Form["start"];
        string end = context.Request.Form["end"];
        SqlConnection cnn = new SqlConnection(str);

        SqlCommand cmd = new SqlCommand(@"select * from md_quocgia where hoatdong = 1", cnn);
        cmd.CommandTimeout = 50000;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        int count_col = 0;
        string sql_column = "(";
        foreach (System.Data.DataColumn col in dt.Columns)
        {
            if (col.ColumnName != "rownum")
            {
                count_col++;
                sql_column += col.ColumnName + ",";
            }
        }

        if (sql_column != "(") { sql_column += "anco_check" + ")"; }

        foreach (System.Data.DataRow row in dt.Rows)
        {
            string md_quocgia2 = row[0].ToString();
            md_quocgia spid = db.md_quocgia.Where(s => s.md_quocgia_id == md_quocgia2).FirstOrDefault();
            if (spid == null)
            {
                string sql_insert = "insert into md_quocgia" + sql_column + " values(";
                for (int i = 0; i < count_col; i++)
                {
                    string cell_value = row[i].ToString();
                    if (cell_value == null | cell_value == "")
                    {
                        sql_insert += "NULL,";
                    }
                    else
                    {
                        sql_insert += "N'" + cell_value.Replace("'", "''") + "',";
                    }
                }
                sql_insert += "1)";
                Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql_insert);
            }
        }
    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            if (msg.Length <= 0)
            {
                string action = "add";
                string[] column_ex = { };
                string ten_table = "md_quocgia";
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
            string id = context.Request.Form["id"];
            string object_ = db.md_quocgia.Where(p => p.md_quocgia_id == id).Select(s => s.md_quocgia_id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }

            if (msg.Length <= 0)
            {
                string action = "edit";
                string[] column_ex = { };
                string ten_table = "md_quocgia";
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
                string id_del = context.Request.Form["id"];

                var object_ = db.md_quocgia.Where(p => p.md_quocgia_id == id_del).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần xóa";
                }
                else
                {
                    var pbg001s = db.md_doitackinhdoanh.Where(s => s.md_quocgia_id == object_.md_quocgia_id).Take(1).Count();

                    if(pbg001s > 0)
                        msg = "Lỗi:Quốc gia đã được sử dụng trong đối tác kinh doanh";
                }

                if (msg.Length <= 0)
                {
                    VNN_Function.Write_log(context, ma_module, null, oper, "MQG:" + object_.ma_quocgia + ", TQG:" + object_.ten_quocgia, db);
                    db.md_quocgia.Remove(object_);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if(msg.Length <= 0)
            {
                transaction.Commit();
                msg = string.Format("true#{0}", "Xóa quốc gia thành công");
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = string.Format("false#{0}", msg);
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
