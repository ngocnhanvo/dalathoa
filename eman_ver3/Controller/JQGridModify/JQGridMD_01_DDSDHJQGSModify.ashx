<%@ WebHandler Language="C#" Class="JQGridMD_01_DDSDHModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using DataAcess;
public class JQGridMD_01_DDSDHModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA01UpdateDDSAncotrading_MD01DDSDHJQGS":
                this.CA01UpdateDDSAncotrading_MD01DDSDHJQGS(context);
                break;

            default:
                break;
        }
    }

    public void CA01UpdateDDSAncotrading_MD01DDSDHJQGS(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string str = VNN_VariablePublic.connectString_Anco(db);
        string start = context.Request.Form["start"];
        string end = context.Request.Form["end"];
        SqlConnection cnn = new SqlConnection(str);

        SqlCommand cmd = new SqlCommand(@"select * from (select *, ROW_NUMBER() over (order by ngaytao) as rownum 
		from c_dongdsdh where hoatdong = 1 and md_doitackinhdoanh_id = 'e36c38e0982d1b0372708eaa0c6162ed') P where P.rownum >= " + start + " and P.rownum <= " + end, cnn);
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
            string c_dongdsdh_id = row[0].ToString();
            c_dongdsdh dsdh = db.c_dongdsdh.Where(s => s.c_dongdsdh_id == c_dongdsdh_id).FirstOrDefault();
            if (dsdh == null)
            {
                string sql_insert = "insert into c_dongdsdh" + sql_column + " values(";
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
                string[] column_ex = { "c_danhsachdathang_id" };
                VNN_Function.SetFormValue("c_danhsachdathang_id", id);
                VNN_Function.SetFormValue("v2", "VNN_notpost");
                string ten_table = "c_dongdsdh";
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, id_new, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Thêm thành công Hello." + "#" + id_new;
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
        string ten_phienban = context.Request.Form["md_sanpham_bom_id"];
        string masp = context.Request.Form["md_sanpham_id"];
        try
        {


            string id = context.Request.Form["id"];
            var object_ = db.c_dongdsdh.Where(p => p.c_dongdsdh_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }
            else
            {
                var bom_sp = db.md_sanpham_bom.Where(s =>
                s.md_sanpham_id == object_.md_sanpham_id &
                s.ten_phienban == ten_phienban &
                string.IsNullOrEmpty(s.md_phanxuong_id) &
                string.IsNullOrEmpty(s.md_to_id)).FirstOrDefault();
                if (bom_sp == null)
                    msg = string.Format(@"false#HH ""{0}"" không có BOM", masp);
                else
                    ten_phienban = bom_sp.md_sanpham_bom_id;
            }

            if (msg.Length <= 0)
            {
                string action = "edit";
                string[] column_ex = { };
                string ten_table = "c_dongdsdh";
                VNN_Function.SetFormValue("v2", "VNN_notpost");
                VNN_Function.SetFormValue("md_sanpham_bom_id", ten_phienban);
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
        string msg = "",  msg_del = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string ten_table = "c_dongdsdh";
            int count = context.Request.Form["id"].Split(',').Count();
            string[] id_del = new string[count];
            id_del = context.Request.Form["id"].Split(',');
            for (int i = 0; i < count; i++)
            {
                msg_del = ""; var id_del_ = id_del[i];
                string object_ = db.c_dongdsdh.Where(p => p.c_dongdsdh_id == id_del_).Select(s => s.c_dongdsdh_id).Take(1).FirstOrDefault();
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}