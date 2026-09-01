<%@ WebHandler Language="C#" Class="ConfigSystem" %>

using System;
using System.Web;
using System.Data;
using System.Linq;
using System.IO;
using DataAcess;
using Newtonsoft.Json;

public class ConfigSystem : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];

        switch (oper)
        {
            case "configSystem":
                this.configSystem(context);
                break;
            case "changeColor":
                this.changeColor(context);
                break;
            case "loadColor":
                this.loadColor(context);
                break;
            case "FindOrigination":
                this.FindOrigination(context);
                break;
            case "count_module2":
                this.count_module2(context);
                break;
            case "load_select":
                this.load_select(context);
                break;
            case "update_config":
                this.update_config(context);
                break;
            case "loadData":
                this.loadData(context);
                break;
            case "clean":
                this.cleanData(context);
                break;
            default:
                break;
        }
    }

    public void cleanData(HttpContext context)
    {
        string msg = "";
        try
        {
            var d = new DirectoryInfo(Helper.pathReport("")["link"]);
            var files = d.GetFiles("*.*");
            foreach (var file in files)
            {
                file.Delete();
            }
            msg += "Đã xóa rác report thành công\n";
        }
        catch (Exception ex)
        {
            msg = "Lỗi xóa rác report:" + ex.Message + "\n";
        }

        try
        {
            var d = new DirectoryInfo(Helper.pathImport("")["link"]);
            var files = d.GetFiles("*.*");
            foreach (var file in files)
            {
                file.Delete();
            }
            msg += "Đã xóa rác import thành công\n";
        }
        catch (Exception ex)
        {
            msg = "Lỗi xóa rác import:" + ex.Message + "\n";
        }

        context.Response.Write(msg);
    }

    public void update_config(HttpContext context)
    {
        string msg = "";
        try
        {
            string tencongty = context.Request.Form["tencongty"];
            EntityContext db = new EntityContext();
            ad_systemconfig ttc = db.ad_systemconfig.FirstOrDefault();
            ttc.tencongty = tencongty;
            db.SaveChanges();
            msg = "true(##)" + tencongty;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        context.Response.Write(msg);
    }

    public void load_select(HttpContext context)
    {
        string type = context.Request.QueryString["type"];
        string firstnull = context.Request.QueryString["firstnull"];
        string selected = context.Request.QueryString["selected"];
        string msg = "";
        EntityContext db = new EntityContext();
        if (firstnull == "") { msg = "<option value=\"\"></option>"; }
        ad_selectoption sel = db.ad_selectoption.Where(s => s.ma_selectoption == type).FirstOrDefault();
        if (sel != null)
        {
            string id_count = sel.select_sql.Split(',')[0];
            id_count = id_count.Replace("distinct", "");
            if (id_count.Contains(" as "))
            {
                int j_index = id_count.IndexOf(" as ");
                id_count = id_count.Substring(0, j_index);
            }

            string orderby = id_count;
            if (sel.orderby_sql != null)
            {
                if (sel.orderby_sql.Replace(" ", "").Length > 0)
                {
                    orderby = sel.orderby_sql;
                }
            }

            string sql_select =
            " sELeCt {0}" +
            " fRoM {1} " +
            " WHeRe 1=1 {2} {3}";

            string orderby_ = "";
            if (sel.orderby_sql != null)
            {
                if (sel.orderby_sql != "")
                    orderby_ = "order by " + sel.orderby_sql;
            }
            string sql = string.Format(sql_select, sel.select_sql, sel.from_sql, sel.where_sql, orderby_);
            string[] id_cop = sel.display_member.Replace("'", "").Split(':');

            System.Data.DataTable dt = Mbg.Data.SqlClient.SqlHelper.GetData(sql);

            foreach (DataRow row in dt.Rows)
            {
                //Khởi tạo option với giá trị không đổi
                string str = "<option value=\"" + id_cop[0] + "\">" + id_cop[1] + "</option>";
                if (selected == row[0].ToString())
                    str = "<option selected value=\"" + id_cop[0] + "\">" + id_cop[1] + "</option>";
                //Thay đổi giá trị của option
                foreach (System.Data.DataColumn col in dt.Columns)
                {
                    if (sel.display_member.Contains(col.ColumnName.ToString()))
                    {
                        if (row[col].ToString() != null)
                        {
                            str = str.Replace(col.ColumnName.ToString(), row[col].ToString());
                        }
                    }
                }
                msg += str;
            }
        }
        context.Response.Write(msg);
    }

    public void count_module2(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.QueryString["id"];
        if (id == "null" | id == "undefined") { id = null; }
        ad_module mod0 = db.ad_module.Where(s => s.ma_module == ma_module).FirstOrDefault();

        string groupby = "";
        if (mod0.groupby_sql != null & mod0.groupby_sql != "")
            groupby = $"group by {mod0.groupby_sql}";
        string sql_count = $@"select 1 from {mod0.from_sql} where 1=1 {mod0.where_sql} {groupby}";
        sql_count = ADmin_ConvertStringToCode.Avariable(context, sql_count, "", id, null, db);
        var dt_count = Mbg.Data.SqlClient.SqlHelper.GetData(sql_count);

        if (dt_count.Rows.Count > 0)
        {
            msg = dt_count.Rows.Count.ToString();
        }
        context.Response.Write(msg);
    }

    public void changeColor(HttpContext context)
    {
        var db = new EntityContext();
        string msg = "";

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var userId = Security.id_taikhoan(context);
                var user = db.ad_user.Where(s => s.ad_user_id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.mauBackground = context.Request.Form["mausac"];
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }


            if (msg.Length <= 0)
                transaction.Commit();
            else
                transaction.Rollback();
        }

        context.Response.Write(msg);
    }

    public void loadColor(HttpContext context)
    {
        EntityContext db = new EntityContext();
        ad_systemconfig ttc = db.ad_systemconfig.FirstOrDefault();
        if (ttc != null)
            context.Response.Write(ttc.mausac);
    }

    public void configSystem(HttpContext context)
    {
        EntityContext db = new EntityContext();
        ad_systemconfig ttc = db.ad_systemconfig.FirstOrDefault();
        ad_systemconfig ttc_edit = null;
        string msg = "";
        string path = "upload";
        string url_base = Security.UrlBase();
        if (context.Request.Form["txt_tencongty"] != "" & context.Request.Form["txt_email"] != ""
            & context.Request.Form["txt_emailserver"] != "" & context.Request.Form["txt_port"] != ""
            & context.Request.Form["txt_matkhau"] != "")
        {

            if (VNN_Validate.check_number(context.Request.Form["txt_port"], "int") == false)
            {
                msg = "Trường dữ liệu \"Port\" phải là kiểu số nguyên !!!";
            }

            if (msg.Length <= 0)
            {
                if (ttc != null)
                {
                    ttc_edit = ttc;
                }
                else
                {
                    ttc_edit = new ad_systemconfig();
                    ttc_edit.ad_systemconfig_id = Helper.getNewId();
                }

                string url = context.Server.MapPath(url_base + path).Replace("Controller", "");
                if (!Directory.Exists(url))
                {
                    Directory.CreateDirectory(url);
                }

                try
                {
                    HttpPostedFile f = context.Request.Files["txt_Logo"];
                    if (f.ContentType.Equals("image/png") | f.ContentType.Equals("image/jpeg"))
                    {
                        int j = f.FileName.LastIndexOf('.');

                        string fileSave = url + "/" + ttc.ad_systemconfig_id + f.FileName.Substring(j);
                        f.SaveAs(fileSave);

                        string fileSaveDB = path + "/" + ttc.ad_systemconfig_id + f.FileName.Substring(j);
                        ttc_edit.logo = fileSaveDB;
                    }
                    else
                    {
                        msg = "\nLogo phải là tập tin có dạng .png hoặc .jpeg" + path;
                    }
                }
                catch
                {

                }

                try
                {
                    HttpPostedFile f_tc = context.Request.Files["txt_Logo_tc"];

                    if (f_tc.ContentType.Equals("image/png") | f_tc.ContentType.Equals("image/jpeg"))
                    {
                        int j = f_tc.FileName.LastIndexOf('.');
                        string fileSave = url + "/" + ttc.ad_systemconfig_id + "1" + f_tc.FileName.Substring(j);
                        f_tc.SaveAs(fileSave);

                        string fileSaveDB = path + "/" + ttc.ad_systemconfig_id + "1" + f_tc.FileName.Substring(j);
                        ttc_edit.logo_trangchu = fileSaveDB;
                    }
                    else
                    {
                        msg = "\nLogo trang chủ phải là tập tin có dạng .png hoặc .jpeg";
                    }
                }
                catch
                {

                }


                if (msg.Length <= 0)
                {
                    ttc_edit.ten_canhbao = context.Request.Params["txt_tencanhbao"];

                    try
                    {
                        //ttc_edit.soluong_grid = int.Parse(context.Request.Params["txt_soluong_grid"]);
                    }
                    catch
                    {
                        //ttc_edit.soluong_grid = 15;
                    }

                    //ttc_edit.soluong_grid_2 = context.Request.Params["txt_soluong_grid_2"];
                    ttc_edit.tencongty = context.Request.Params["txt_tencongty"];
                    ttc_edit.website = context.Request.Params["txt_website"];
                    ttc_edit.taikhoanemail = context.Request.Params["txt_email"];
                    ttc_edit.port = context.Request.Params["txt_port"];
                    ttc_edit.phone = context.Request.Params["txt_dienthoai"];
                    ttc_edit.passemail = context.Request.Params["txt_matkhau"];
                    ttc_edit.diachi = context.Request.Params["txt_diachi"];
                    ttc_edit.emailserver = context.Request.Params["txt_emailserver"];
                    ttc_edit.fax = context.Request.Params["txt_Fax"];
                    ttc_edit.url_linq = context.Request.Params["txt_url_linq"];
                    ttc_edit.ten_db = context.Request.Params["txt_ten_db"];
                    ttc_edit.ten_linq = context.Request.Params["txt_ten_linq"];
                    ttc_edit.ten_connectstring = context.Request.Params["txt_ten_connectstring"];
                    ttc_edit.format_ngay = context.Request.Params["txt_format_ngay"];
                    ttc_edit.format_so = context.Request.Params["txt_format_so"];
                    ttc_edit.connectstring_anco = context.Request.Params["txt_connectstring_anco"];
                    ttc_edit.domain = context.Request.Params["txt_domain"];
                    ttc_edit.email_hotro = context.Request.Params["txt_email_hotro"];
                    if (ttc == null)
                    {
                        db.ad_systemconfig.Add(ttc_edit);
                    }
                    db.SaveChanges();
                    msg = "Cấu hình thành công, nhấn F5 để hiệu lực thông tin vừa cấu hình !!!";
                }
            }
        }
        else
        {
            msg = "Các trường dữ liệu có dấu (*) là bắt buộc nhập !!!";
        }

        context.Response.Write(msg);
    }

    public void FindOrigination(HttpContext context)
    {
        if (VNN_VariablePublic.view_origination == false)
            VNN_VariablePublic.view_origination = true;
        else
            VNN_VariablePublic.view_origination = false;

        context.Response.Write(VNN_VariablePublic.view_origination);
    }

    public void loadData(HttpContext context)
    {
        var db = new EntityContext();
        var ttc = db.ad_systemconfig.FirstOrDefault();
        context.Response.Write(
            ttc.tencongty + "#" +
            ttc.diachi + "#" +
            ttc.phone + "#" +
            ttc.fax + "#" +
            ttc.website + "#" +
            ttc.emailserver + "#" +
            ttc.port + "#" +
            ttc.taikhoanemail + "#" +
            ttc.passemail + "#" +
            ttc.logo + "#" +
            ttc.ten_canhbao + "#" +
            ttc.soluong_grid + "#" +
            ttc.soluong_grid_2 + "#" +
            ttc.logo_trangchu + "#" +
            ttc.url_linq + "#" +
            ttc.ten_db + "#" +
            ttc.ten_linq + "#" +
            ttc.ten_connectstring + "#" +
            ttc.format_ngay + "#" +
            ttc.format_so + "#" +
            ttc.connectstring_anco + "#" +
            ttc.domain + "#" +
            ttc.email_hotro
        );
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}