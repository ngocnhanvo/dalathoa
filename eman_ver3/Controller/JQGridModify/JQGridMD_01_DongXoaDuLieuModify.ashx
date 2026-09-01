
<%@ WebHandler Language="C#" Class="JQGridMD_01_DongXoaDuLieuModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_DongXoaDuLieuModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "selectoption":
                this.selectoption(context);
                break;
            case "selectoptionline":
                this.selectoptionline(context);
                break;
            default:
                break;
        }
    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string id_parent = context.Request.Form["id_parent"];
        string ten_tab = context.Request.Form["ten_table"];
        try
        {
            var rm = db.ad_remove.Where(s => s.ad_remove_id == id_parent).FirstOrDefault();
            if (rm == null)
            {
                msg = $"Không tìm thấy table của lưới cha";
                goto EndEventHandler;
            }

            var rml = db.ad_removeline.Where(s => s.ad_remove_id == rm.ad_remove_id & s.ten_table == ten_tab).Take(1).FirstOrDefault();
            if (rml != null)
            {
                msg = "Tên table con đã được thêm trước đó.";
                goto EndEventHandler;
            }


            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string ten_table_ = context.Request.Form["ten_table"];
            string action = context.Request.Params["oper"];
            string[] column_ex = { "ad_remove_id" };
            string ten_table = "ad_removeline";
            VNN_Function.SetFormValue("ad_remove_id", id_parent);
            VNN_Function.SetFormValue("sapxep", sapxep);
            VNN_Function.Set_DefaultvalueColumn(context, action);
            VNN_Function.Modify_Function(context, ma_module, id_new, ten_table, action, column_ex, db);
            VNN_Function.loaddulieu_Auto(db, ma_module);
            VNN_Function.SortColumn(ten_table, sapxep, "ad_remove_id", id_parent, "ten_table", ten_table_, null);

            if (context.Request.Form["update_0"] == "update_0")
            {
                var ttc = Helper.getInfoDB();
                VNN_Function.create_Trigger_Del(ttc["database"]);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if(msg.Length <= 0)
        {
            msg = $"true#Thêm thành công#{id_new}";
        }
        else
        {
            msg = $"false#{msg}";
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
            string object_ = db.ad_removeline.Where(p => p.ad_removeline_id == id).Select(s => s.ad_removeline_id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }

            if (msg.Length <= 0)
            {
                string action = context.Request.Params["oper"];
                string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
                string ten_table_ = context.Request.Form["ten_table"];
                string ten_table = "ad_removeline";
                string[] column_ex = { };
                VNN_Function.SetFormValue("sapxep", sapxep);
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                VNN_Function.SortColumn(ten_table, sapxep, "ad_remove_id", id, "ten_table", ten_table_, null);
                msg = "true#Cập nhật thành công.";

                if (context.Request.Form["update_0"] == "update_0")
                {
                    var ttc = Helper.getInfoDB();
                    VNN_Function.create_Trigger_Del(ttc["database"]);
                }
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
        try
        {
            string id = context.Request.Form["id"];
            string object_ = db.ad_removeline.Where(p => p.ad_removeline_id == id).Select(s => s.ad_removeline_id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần xóa ";
            }

            if (msg.Length <= 0)
            {
                string action = context.Request.Params["oper"];
                string ten_table = "ad_removeline";
                string[] column_ex = { };
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "Xóa#Cập nhật thành công.";
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
                msg = "false#" + ex.Message;
            }
        }
        context.Response.Write(msg);
    }

    public void selectoption(HttpContext context)
    {
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        string sql = $@"
            SELECT distinct 
                vnn_col.TABLE_NAME, vnn_col.COLUMN_NAME
            FROM 
                INFORMATION_SCHEMA.COLUMNS vnn_col  with (nolock)
                left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE vnn_key on vnn_col.TABLE_NAME = vnn_key.TABLE_NAME and vnn_col.COLUMN_NAME = vnn_key.COLUMN_NAME
                left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS type_key on type_key.CONSTRAINT_NAME = vnn_key.CONSTRAINT_NAME
            where 
                (
                    type_key.CONSTRAINT_TYPE != N'PRIMARY KEY' or 
                    type_key.CONSTRAINT_TYPE is null
                ) 
                and vnn_col.COLUMN_NAME like '%_id'
            order by 
                vnn_col.TABLE_NAME asc
        ";

        var dt_sql = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        string str = "";
        str += "<select>";
        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        for (int i = 0; i < dt_sql.Rows.Count; i++)
        {
            string name = dt_sql.Rows[i][0].ToString(), keyF = dt_sql.Rows[i][1].ToString();
            str += $"<option keyF='{keyF}' value='{name}'>{name}</option>";
        }
        str += "</select>";
        context.Response.Write(str);
    }
    public void selectoptionline(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string id = context.Request.QueryString["id"];
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        var ten_table = db.ad_removeline.Where(s => s.ad_remove_id == id);
        string str = "";
        str = "<select>";
        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        foreach (ad_removeline rml in ten_table)
        {
            str += string.Format("<option value=\"{0}\">{1}</option>", rml.ten_table, rml.ten_table);
        }
        str += "</select>";
        context.Response.Write(str);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}