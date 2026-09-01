<%@ WebHandler Language="C#" Class="JQGridMD_00_NhaplieuPMModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using ExcelLibrary.SpreadSheet;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using Mbg.Data.SqlClient;

public class JQGridMD_00_NhaplieuPMModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
        switch (oper)
        {
            case "importDB":
                this.importDB(context);
                break;
            case "import_data":
                this.import_data(context);
                break;
            default:
                break;
        }
    }

    public void finalFunctionAfterImportEnd(string select_ddpm)
    {
        var db = new EntityContext();
        var ip = db.ad_import.Where(s => s.ad_import_id == select_ddpm).FirstOrDefault();
        if (ip == null)
        {
            goto end;
        }

        if(ip.ma_import == "IP_BOM_01")
        {
            foreach(var vt in db.md_sanpham_bom_vattu.Where(s => s.soluong == null).ToList())
            {
                db.md_sanpham_bom_vattu.Remove(vt);
            }
        }
        else if(ip.ma_import == "IP_BOM_01")
        {
            foreach(var vt in db.md_sanpham_bom_vattu.Where(s => s.soluong == null).ToList())
            {
                db.md_sanpham_bom_vattu.Remove(vt);
            }
        }

        VNN_Function.loaddulieu_Auto(db, "MD_00_NhaplieuPM");

    end:;
    }

    public string upload(HttpContext context)
    {
        string msg = "";
        HttpFileCollection files = context.Request.Files;
        string path = Security.UrlBase() + "FileUpload";
        if (files.Count > 0)
        {
            if (!System.IO.File.Exists(context.Server.MapPath(path)))
            {
                msg = path + "/" + files[0].FileName;
                files[0].SaveAs(context.Server.MapPath(msg));
                int j = msg.LastIndexOf(".");
                if (msg.Substring(j) != ".xls")
                {
                    ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(context.Server.MapPath(msg));
                    msg = msg.Replace(".xlsx", ".xls");
                }
            }
            else
            {
                msg = "false";
            }
        }
        else
        {
            msg = "false#2";
        }
        return msg;
    }

    public void del_file(HttpContext context, string path)
    {
        try
        {
            if (System.IO.File.Exists(context.Server.MapPath(path)))
            {
                System.IO.File.Delete(context.Server.MapPath(path));
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void importDB(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string select_ddpm = context.Request.Form["select_ddpm"];
        string skipCheckColumnCount = context.Request.Form["skipCheckColumnCount"].removeAllSpaceOrTrimText(false);
        string file_path = upload(context);
        if (select_ddpm == "")
        {
            msg = "false#0";
        }
        else if (file_path == "false#2")
        {
            msg = "false#2";
        }
        else if (file_path == "false")
        {
            msg = "false";
        }
        else
        {
            var ip = db.ad_import.Where(s => s.ad_import_id == select_ddpm).FirstOrDefault();
            string file = context.Server.MapPath(file_path);
            using (var stream = File.Open(file, FileMode.Open))
            {
                try
                {
                    Workbook wb = null;
                    var task = Task.Run(() =>
                    {
                        wb = Workbook.Load(stream);
                        context.Session["worksheets"] = wb.Worksheets[0].Cells;
                    });

                    if (!task.Wait(TimeSpan.FromSeconds(30)))
                        msg = "false#Tập tin này quá lớn và phức tạp để hệ thống có thể đọc hiểu.";
                    else if (wb != null)
                    {
                        var kiemTraCot = skipCheckColumnCount.ToLower() == "false";
                        var activeWS = wb.Worksheets[0];
                        var ipDetails = db.ad_import_column.
                                Where(s => s.imported == "true" & s.ad_import_id == ip.ad_import_id).
                                OrderBy(s => s.sapxep).ToList();
                        var tblTable = "";
                        if (kiemTraCot)
                        {
                            var tblRow = "";
                            var tblTd = "";
                            foreach (var cell in ipDetails)
                            {
                                tblTd += string.Format(@"<th>{0}</th>", cell.ten_import_column);
                            }

                            if (ip.value_header != 999 & ip.value_row != 999)
                            {
                                for (var cell = ipDetails.Count; cell <= activeWS.Cells.LastColIndex; cell++)
                                {
                                    var val = activeWS.Cells.Rows[0].GetCell(cell);
                                    var str = val.StringValue;
                                    str = str.changeScientificNotation_Decimal();
                                    tblTd += string.Format(@"<th>{0}</th>", str);
                                }
                            }

                            tblRow += string.Format(@"<tr>{0}</tr>", tblTd);

                            for (var row = 1; row < activeWS.Cells.Rows.Count; row++)
                            {
                                tblTd = "";
                                var tdOrTh = row == 0 ? "th" : "td";
                                for (var cell = 0; cell <= activeWS.Cells.LastColIndex; cell++)
                                {
                                    var val = activeWS.Cells.Rows[row].GetCell(cell);
                                    var str = val.StringValue;
                                    str = str.changeScientificNotation_Decimal();
                                    tblTd += string.Format(@"<{0}>{1}</{0}>", tdOrTh, str);
                                }
                                tblRow += string.Format(@"<tr>{0}</tr>", tblTd);

                                if (row > 2 & activeWS.Cells.Rows.Count > 2)
                                {
                                    tblRow += string.Format(@"<tr><td colspan='{0}' style='background-color:rgb(240, 248, 255);text-align:left;'><b>{1} dòng đã được ẩn...</b></td></tr>",
                                        activeWS.Cells.LastColIndex + 1,
                                        activeWS.Cells.Rows.Count - row
                                        );
                                    break;
                                }
                            }
                            tblTable += string.Format(@"<table class='tblCheckColumnInReport'>{0}</table>", tblRow);

                            string input = "<input class='continueImport' onclick='importDB(true)' type='button' value='ĐỒNG Ý NHẬP LIỆU'/>";
                            msg = string.Format(@"<br>false#Tập tin đã chọn ({0} cột), Định dạng đã chọn ({1} cột).
                                <div style='padding: 5px;'>Xem lại trước khi nhập liệu</div>{2}<br>{3}",
                                activeWS.Cells.LastColIndex + 1,
                                ipDetails.Count,
                                tblTable,
                                input
                            );
                        }
                        else
                            msg = this.NewFromCellCollection(context, activeWS.Cells, ip, file, db);
                    }
                    else
                    {
                        msg = "false#1";
                    }
                }
                catch (Exception ex)
                {
                    msg = "false#" + ex;
                }
                stream.Close();
                stream.Dispose();
            }
        }
        context.Response.Write(msg);
    }

    public string NewFromCellCollection(HttpContext context, CellCollection cellCollection, ad_import ip, string file_path, EntityContext db)
    {
        string type = context.Request.Form["type"];
        string ten_table = ip.ten_table;
        int count_column = db.ad_import_column.Where(s => s.ad_import_id == ip.ad_import_id & s.imported == "true").Count();
        string sql_del = "";
        context.Session["sql_del"] = "";
        if (ip.column_del != null & type == "fast")
        {
            if (ip.column_del.Replace(" ", "") != "")
            {
                ten_table = "cpip_" + DateTime.Now.ToString("ddMMyyyyhhmmssffftt");
                string sql_createtbl = "SELECT * INTO " + ten_table + " FROM " + ip.ten_table + " WHERE 1=2";
                SqlHelper.ExcuteNonQuery(sql_createtbl);
                sql_del = "delete top(1000) from " + ip.ten_table + " where " + ip.column_del + " {in} {0}";
            }
        }
        string sql_insert = "insert into " + ten_table, sql_columns = "(", sql_values = "(";
        string sql_update = "update " + ten_table, sql_sets = "", sql_wheres = "";
        string check_sql_primary = "select top 1 hoatdong from " + ten_table + " where 1=1";
        string str_check = "";
        User_TK us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context));
        context.Session["us_import"] = us;
        foreach (ad_import_column col in db.ad_import_column.Where(s => s.ad_import_id == ip.ad_import_id).OrderBy(s => s.sapxep).ToList())
        {
            //insert
            sql_columns += col.ma_import_column + ",";
            string sql_select = col.select_sql;
            if (sql_select == "") { sql_select = "NULL"; }
            sql_values += sql_select + ",";
            //update
            if (col.imported == "True")
            {
                if (col.primary_key == "True")
                {
                    sql_wheres += " and " + col.ma_import_column + " = " + sql_select;
                    str_check += col.select_sql_cp + "^";
                }
                sql_sets += col.ma_import_column + "=" + sql_select + ",";
            }
            else if (VNN_Validate.check_column_default(col.ma_import_column, "edit", null))
            {
                sql_sets += col.ma_import_column + "=" + sql_select + ",";
            }
            //del
            if (ip.column_del == col.ma_import_column & type == "fast")
            {
                sql_del = sql_del.Replace("{0}", sql_select);
            }
        }
        //--
        string str_row_ex = "", str_row_value = "";
        foreach (ad_import_ex ex_row in db.ad_import_ex.Where(s => s.ad_import_id == ip.ad_import_id).OrderBy(s => s.row_ex).ToList())
        {
            str_row_ex += ex_row.row_ex + ",";
            str_row_value += ex_row.value_replace + ",";
        }
        if (str_row_ex != "")
        {
            str_row_ex = str_row_ex.Substring(0, str_row_ex.Length - 1);
            context.Session["arr_exrow"] = str_row_ex.Split(',');
        }
        else
        {
            context.Session["arr_exrow"] = null;
        }
        if (str_row_value != "")
        {
            str_row_value = str_row_value.Substring(0, str_row_value.Length - 1);
            context.Session["arr_value"] = str_row_value.Split(',');
        }
        else
        {
            context.Session["arr_value"] = null;
        }
        //--
        string ex_sql = "", ex_avariable = "";
        foreach (ad_import_ava ex_ava in db.ad_import_ava.Where(s => s.ad_import_id == ip.ad_import_id).OrderBy(s => s.ngaytao).ToList())
        {
            ex_sql += ex_ava.select_sql + "ξ";
            ex_avariable += ex_ava.ava_name + "ξ";
        }
        if (ex_sql != "")
        {
            ex_sql = ex_sql.Substring(0, ex_sql.Length - 1);
            context.Session["ex_sql"] = ex_sql.Split('ξ');
            context.Session["ex_ava"] = ex_avariable.Split('ξ');
        }
        else
        {
            context.Session["ex_sql"] = null;
            context.Session["ex_ava"] = null;
        }
        //--
        string[] str_check_arr = str_check.Split('^');
        //insert
        if (sql_columns != "(") { sql_columns = sql_columns.Remove(sql_columns.Length - 1) + ")"; }
        if (sql_values != "(") { sql_values = sql_values.Remove(sql_values.Length - 1) + ")"; }
        sql_insert += sql_columns + " values " + sql_values;
        //update
        if (sql_sets != "") { sql_sets = sql_sets.Remove(sql_sets.Length - 1); }
        sql_update += " set " + sql_sets + " where 1=1 " + sql_wheres;
        //check sql
        check_sql_primary += sql_wheres;
        context.Session["str_check_arr"] = str_check_arr;
        Row row = cellCollection.Rows[0];
        int count_column_all = 0;
        for (int col_index = 0; col_index < 200; col_index++)
        {
            count_column_all++;
            string row_sel = row.GetCell(col_index) + "";
            if (string.IsNullOrEmpty(row_sel))
            {
                count_column_all--;
                break;
            }
        }
        count_column_all = count_column_all - count_column;
        return
        //0
        cellCollection.Rows.Count + "ξ" +
        //1
        check_sql_primary + "ξ" +
        //2
        sql_insert + "ξ" +
        //3
        sql_update + "ξ" +
        //4
        count_column + "ξ" +
        //5
        file_path + "ξ" +
        //6
        count_column_all + "ξ" +
        //7
        ip.value_header + "ξ" +
        //8
        ip.value_row + "ξ" +
        //9
        sql_del + "ξ" +
        //10
        ip.ten_table + "ξ" +
        //11
        ten_table;
    }

    public void import_data(HttpContext context)
    {
        string msg = "";
        try
        {
            string select_ddpm = context.Request.Form["select_ddpm"];
            int end = int.Parse(context.Request.Form["end"]);
            int total = int.Parse(context.Request.Form["total"]);
            int value_header = int.Parse(context.Request.Form["value_header"]);
            int value_row = int.Parse(context.Request.Form["value_row"]);
            int start = int.Parse(context.Request.Form["start"]);

            int count_column = int.Parse(context.Request.Form["count_column"]);
            int count_column_all = int.Parse(context.Request.Form["count_column_all"]);
            int count_row_null = 0;
            int count_check_arr = 0;
            int count_ex_sql = 0;
            string sql_insert = context.Request.Form["sql_insert"];
            string sql_update = context.Request.Form["sql_update"];
            string sql_del = context.Request.Form["sql_del"];
            string ten_table = context.Request.Form["ten_table"];
            string ten_tablecp = context.Request.Form["ten_tablecp"];
            string check_sql_primary = context.Request.Form["check_sql_primary"];
            string[] ex_sql = context.Session["ex_sql"] as string[];
            string[] ex_ava = context.Session["ex_ava"] as string[];
            string[] str_check_arr = context.Session["str_check_arr"] as string[];
            CellCollection cellCollection = context.Session["worksheets"] as CellCollection;
            User_TK us = context.Session["us_import"] as User_TK;
            try
            {


                if (ex_sql != null) { count_ex_sql = ex_sql.Count(); }
                if (str_check_arr != null) { count_check_arr = str_check_arr.Count(); }

                if (end >= total) { end = total; }
                for (int i = start; i < end; i++)
                {
                    string sql_insert_row = sql_insert, check_sql_row = check_sql_primary, sql_update_row = sql_update, sql_del_row = sql_del;
                    string sql_insert_row2 = sql_insert, check_sql_row2 = check_sql_primary, sql_update_row2 = sql_update, sql_del_row2 = sql_del;
                    string[] ex_sql_row = null, ex_sql_row2 = null;
                    if (count_ex_sql > 0) { ex_sql_row = (string[])ex_sql.Clone(); ex_sql_row2 = (string[])ex_sql.Clone(); }
                    try
                    {
                        Row row = cellCollection.Rows[i];
                        if (row.GetCell(0).ToString() == null | row.GetCell(0).ToString() == "")
                        {
                            count_row_null++;
                        }
                        else
                        {
                            try
                            {
                                for (int j = 0; j < count_column; j++)
                                {
                                    string row_sel = row.GetCell(j).ToString();
                                    row_sel = row_sel.changeScientificNotation_Decimal();

                                    if (row_sel == null | row_sel == "")
                                    {
                                        sql_insert_row = sql_insert_row.Replace("'{" + j + "}'", "NULL");
                                        check_sql_row = check_sql_row.Replace("'= {" + j + "}'", "is null");
                                        sql_update_row = sql_update_row.Replace("'{" + j + "}'", "NULL");
                                        sql_del_row = sql_del_row.Replace("'{" + j + "}'", "NULL");
                                        for (int k = 0; k < count_ex_sql; k++)
                                        {
                                            ex_sql_row[k] = ex_sql_row[k].Replace("'{" + j + "}'", "NULL");
                                        }
                                    }
                                    else
                                    {
                                        row_sel = "N'" + row_sel.Replace("'", "''") + "'";
                                        sql_insert_row = sql_insert_row.Replace("'{" + j + "}'", row_sel);
                                        check_sql_row = check_sql_row.Replace("'{" + j + "}'", row_sel);
                                        sql_update_row = sql_update_row.Replace("'{" + j + "}'", row_sel);
                                        sql_del_row = sql_del_row.Replace("'{" + j + "}'", row_sel);
                                        for (int k = 0; k < count_ex_sql; k++)
                                        {
                                            ex_sql_row[k] = ex_sql_row[k].Replace("'{" + j + "}'", row_sel);
                                        }
                                    }

                                    //--
                                    if (j != value_header & j != value_row & (value_header != 999 | value_row != 999))
                                    {
                                        if (row_sel == null | row_sel == "")
                                        {
                                            sql_insert_row2 = sql_insert_row2.Replace("'{" + j + "}'", "NULL");
                                            check_sql_row2 = check_sql_row2.Replace("'= {" + j + "}'", "is null");
                                            sql_update_row2 = sql_update_row2.Replace("'{" + j + "}'", "NULL");
                                            sql_del_row2 = sql_del_row2.Replace("'{" + j + "}'", "NULL");
                                            for (int k = 0; k < count_ex_sql; k++)
                                            {
                                                ex_sql_row2[k] = ex_sql_row2[k].Replace("'{" + j + "}'", "NULL");
                                            }
                                        }
                                        else
                                        {
                                            sql_insert_row2 = sql_insert_row2.Replace("'{" + j + "}'", row_sel);
                                            check_sql_row2 = check_sql_row2.Replace("'{" + j + "}'", row_sel);
                                            sql_update_row2 = sql_update_row2.Replace("'{" + j + "}'", row_sel);
                                            sql_del_row2 = sql_del_row2.Replace("'{" + j + "}'", row_sel);
                                            for (int k = 0; k < count_ex_sql; k++)
                                            {
                                                ex_sql_row2[k] = ex_sql_row2[k].Replace("'{" + j + "}'", row_sel);
                                            }
                                        }
                                    }
                                    //--
                                }



                                string ava_1 = "";
                                for (int ii = 0; ii < count_check_arr - 1; ii++)
                                {
                                    int ava = int.Parse(str_check_arr[ii].Replace("'{", "").Replace("}'", ""));
                                    ava_1 += row.GetCell(ava) + " → ";
                                }


                                msg += excute_import(context, check_sql_row, sql_update_row, sql_insert_row, sql_del_row,
                                ava_1, i, count_row_null, 0, ex_sql_row, ex_ava, count_ex_sql, us);
                                if (value_header != 999 & value_row != 999)
                                {
                                    msg += check_column_ex(context, count_column_all, count_column, sql_insert_row2,
                                    check_sql_row2, sql_update_row2, sql_del_row2, str_check_arr, row, i, value_header,
                                    value_row, count_row_null, ex_sql_row2, ex_ava, count_ex_sql, cellCollection, us);
                                }
                            }
                            catch (Exception ex)
                            {
                                msg += string.Format("\n<div style=\"color:red; padding: 0px 0 5px 0px;\">Dòng {0} xảy ra lỗi: {1}</div>", (i - count_row_null), ex.Message);


                            }
                        }



                    }

                    catch (Exception ex)
                    {
                        msg += string.Format(@"<div style=""color:red; padding: 0px 0 5px 0px;"">{0}</div>", ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                msg += string.Format(@"<div style=""color:red; padding: 0px 0 5px 0px;"">{0}</div>", ex.Message);
            }

            try
            {
                if (end >= total)
                {
                    string file = context.Request.Form["file"];
                    del_file(context, file);

                    if (ten_table != ten_tablecp)
                    {
                        msg = copy_data_checked(context, ten_table, ten_tablecp, msg);
                    }
                    finalFunctionAfterImportEnd(select_ddpm);
                    context.Session.Remove("us_import");
                    context.Session.Remove("str_check_arr");
                    context.Session.Remove("ex_sql");
                    context.Session.Remove("ex_ava");
                    context.Session.Remove("worksheets");
                    context.Session.Remove("arr_exrow");
                    context.Session.Remove("arr_value");
                    context.Session.Remove("sql_del");
                }
                else
                {
                    context.Session["us_import"] = context.Session["us_import"];
                    context.Session["str_check_arr"] = context.Session["str_check_arr"];
                    context.Session["ex_sql"] = context.Session["ex_sql"];
                    context.Session["ex_ava"] = context.Session["ex_ava"];
                    context.Session["worksheets"] = context.Session["worksheets"];
                    context.Session["arr_exrow"] = context.Session["arr_exrow"];
                    context.Session["arr_value"] = context.Session["arr_value"];
                    context.Session["sql_del"] = context.Session["sql_del"];
                }
            }
            catch (Exception ex)
            {
                msg += string.Format(@"<div style=""color:red; padding: 0px 0 5px 0px;"">Lỗi kết thúc Import: {0}</div>", ex.Message);
            }
        }
        catch (Exception ex)
        {
            msg += string.Format(@"<div style=""color:red; padding: 0px 0 5px 0px;"">Lỗi: {0}</div>", ex.Message);
        }
        context.Response.Write(msg);
    }

    public string excute_import(HttpContext context, string check_sql_row, string sql_update_row, string sql_insert_row, string sql_del_row,
    string ava_1, int i, int count_row_null, int type, string[] ex_sql, string[] ex_ava, int count_ex_sql, User_TK us)
    {
        string msg = "", color = "color:blue;", char_spec = "→";
        sql_insert_row = change_avariable(sql_insert_row, us);
        sql_update_row = change_avariable(sql_update_row, us);
        for (int j = 0; j < count_ex_sql; j++)
        {
            DataTable dt_add = SqlHelper.GetData(ex_sql[j]);
            string[] ex_ava_value = ex_ava[j].Split(',');
            for (int k = 0; k < dt_add.Columns.Count; k++)
            {
                try
                {
                    string value_rep = dt_add.Rows[0][k].ToString(), value_org = ex_ava_value[k];
                    check_sql_row = check_sql_row.Replace(value_org, value_rep);
                    sql_update_row = sql_update_row.Replace(value_org, value_rep);
                    sql_insert_row = sql_insert_row.Replace(value_org, value_rep);
                    sql_del_row = sql_del_row.Replace(value_org, value_rep);
                }
                catch
                {
                    string value_org = ex_ava_value[k];
                    check_sql_row = check_sql_row.Replace("N'" + value_org + "'", "NULL");
                    sql_update_row = sql_update_row.Replace("N'" + value_org + "'", "NULL");
                    sql_insert_row = sql_insert_row.Replace("N'" + value_org + "'", "NULL");
                    sql_del_row = sql_del_row.Replace("N'" + value_org + "'", "NULL");
                    check_sql_row = check_sql_row.Replace("'" + value_org + "'", "NULL");
                    sql_update_row = sql_update_row.Replace("'" + value_org + "'", "NULL");
                    sql_insert_row = sql_insert_row.Replace("'" + value_org + "'", "NULL");
                    sql_del_row = sql_del_row.Replace("'" + value_org + "'", "NULL");
                }
            }
        }
        if (sql_del_row != "" & sql_del_row != null)
        {
            //sql_del_row = sql_del_row.Replace("'","''").Replace("(@@)","'");
            context.Session["sql_del"] = context.Session["sql_del"].ToString() + "<br>" + sql_del_row;
        }

        DataTable dt = SqlHelper.GetData(check_sql_row);

        if (type == 1) { color = "color:#5454C3;"; char_spec = "+++"; }
        if (ava_1 != "(")
        {
            ava_1 = ava_1.Remove(ava_1.Length - 3);
        }
        else
        {
            ava_1 += "Không xác định";
        }

        if (dt.Rows.Count > 0)
        {
            string ex = SqlHelper.ExcuteNonQuery2(sql_update_row);
            if (string.IsNullOrEmpty(ex))
            {
                msg += string.Format("\n<div style=\"" + color + " padding: 0px 0 5px 0px;\"> " + char_spec + " Dòng {0} ({1}) đã được cập nhật.</div>", (i - count_row_null), ava_1);
            }
            else
            {
                msg += string.Format("\n<div class='err_import' style=\"color:red; padding: 0px 0 5px 0px;\"> " + char_spec + " Dòng {0} ({1}) cập nhật thất bại<br>Lỗi:{2}.</div>", (i - count_row_null), ava_1, ex);
                msg += string.Format("\n<div style=\"display:none\"> " + char_spec + " Dòng {0}: {1}</div>", i - count_row_null, sql_update_row);
            }
        }
        else
        {
            string ex = SqlHelper.ExcuteNonQuery2(sql_insert_row);
            if (string.IsNullOrEmpty(ex))
            {
                if (!string.IsNullOrEmpty(context.Session["sql_del"] + ""))
                    msg += string.Format("\n<div style=\"" + color + " padding: 0px 0 5px 0px;\"> " + char_spec + " Dòng {0} ({1}) có thể import.</div>", (i - count_row_null), ava_1);
                else
                    msg += string.Format("\n<div style=\"" + color + " padding: 0px 0 5px 0px;\"> " + char_spec + " Dòng {0} ({1}) đã được thêm mới.</div>", (i - count_row_null), ava_1);
            }
            else
            {
                msg += string.Format("\n<div class='err_import' style=\"color:red; padding: 0px 0 5px 0px;\"> " + char_spec + " Dòng {0} ({1}) thêm mới thất bại.<br>Lỗi:{2}</div>", i - count_row_null, ava_1, ex);
                msg += string.Format("\n<div style=\"display:none\"> " + char_spec + " Dòng {0}: {1}</div>", i - count_row_null, sql_insert_row);
            }
        }
        return msg;
    }

    public string check_column_ex(HttpContext context, int count_column_all, int count_column, string sql_insert_row2, string check_sql_row2,
    string sql_update_row2, string sql_del_row2, string[] str_check_arr, Row row, int i, int value_header, int value_row, int count_row_null, string[] ex_sql,
    string[] ex_ava, int count_ex_sql, CellCollection cellCollection, User_TK us)
    {
        string[] arr_exrow = context.Session["arr_exrow"] as string[];
        string[] arr_value = context.Session["arr_value"] as string[];
        string msg = "";
        int count_arr_exrow = 0;
        if (arr_exrow != null) { count_arr_exrow = arr_exrow.Count(); }
        for (int i_add = 0; i_add < count_column_all; i_add++)
        {
            string sql_insert_row3 = sql_insert_row2, check_sql_row3 = check_sql_row2, sql_update_row3 = sql_update_row2, sql_del_row3 = sql_del_row2;
            if (ex_sql != null)
            {
                string[] ex_sql_row3 = (string[])ex_sql.Clone();
                int col_index = count_column + i_add;
                //-- Thay the tieu de cot ngoai le
                string row_sel1 = cellCollection.Rows[0].GetCell(col_index).ToString();
                row_sel1 = "N'" + row_sel1.Replace("'", "''") + "'";
                sql_insert_row3 = sql_insert_row3.Replace("'{" + value_header + "}'", row_sel1);
                check_sql_row3 = check_sql_row3.Replace("'{" + value_header + "}'", row_sel1);
                sql_update_row3 = sql_update_row3.Replace("'{" + value_header + "}'", row_sel1);
                sql_del_row3 = sql_del_row3.Replace("'{" + value_header + "}'", row_sel1);
                for (int j = 0; j < count_ex_sql; j++)
                {
                    ex_sql_row3[j] = ex_sql_row3[j].Replace("'{" + value_header + "}'", row_sel1);
                }
                //-- Import hang ngoai le
                for (int i_ex = 0; i_ex < count_arr_exrow; i_ex++)
                {
                    string row_sel1_1 = cellCollection.Rows[int.Parse(arr_exrow[i_ex])].GetCell(col_index).ToString();
                    row_sel1_1 = "N'" + row_sel1_1.Replace("'", "''") + "'";
                    sql_insert_row3 = sql_insert_row3.Replace("'{" + arr_value[i_ex] + "}'", row_sel1_1);
                    check_sql_row3 = check_sql_row3.Replace("'{" + arr_value[i_ex] + "}'", row_sel1_1);
                    sql_update_row3 = sql_update_row3.Replace("'{" + arr_value[i_ex] + "}'", row_sel1_1);
                    sql_del_row3 = sql_del_row3.Replace("'{" + arr_value[i_ex] + "}'", row_sel1_1);
                    for (int j = 0; j < count_ex_sql; j++)
                    {
                        ex_sql_row3[j] = ex_sql_row3[j].Replace("'{" + arr_value[i_ex] + "}'", row_sel1_1);
                    }
                }
                //-- Thay the gia tri hang ngoai le
                string row_sel2 = row.GetCell(col_index).ToString();
                if (row_sel2 == null | row_sel2 == "")
                {
                    sql_insert_row3 = sql_insert_row3.Replace("'{" + value_row + "}'", "NULL");
                    check_sql_row3 = check_sql_row3.Replace("'{" + value_row + "}'", "is null");
                    sql_update_row3 = sql_update_row3.Replace("'{" + value_row + "}'", "NULL");
                    sql_del_row3 = sql_del_row3.Replace("'{" + value_row + "}'", "NULL");
                    for (int j = 0; j < count_ex_sql; j++)
                    {
                        ex_sql_row3[j] = ex_sql_row3[j].Replace("'{" + value_row + "}'", "NULL");
                    }
                }
                else
                {
                    row_sel2 = "N'" + row_sel2.Replace("'", "''") + "'";
                    sql_insert_row3 = sql_insert_row3.Replace("'{" + value_row + "}'", row_sel2);
                    check_sql_row3 = check_sql_row3.Replace("'{" + value_row + "}'", row_sel2);
                    sql_update_row3 = sql_update_row3.Replace("'{" + value_row + "}'", row_sel2);
                    sql_del_row3 = sql_del_row3.Replace("'{" + value_row + "}'", row_sel2);
                    for (int j = 0; j < count_ex_sql; j++)
                    {
                        ex_sql_row3[j] = ex_sql_row3[j].Replace("'{" + value_row + "}'", row_sel2);
                    }
                }
                //-- tao thong bao
                string ava_1 = "";
                for (int ii = 0; ii < str_check_arr.Count() - 1; ii++)
                {
                    if (str_check_arr[ii] == "'{" + value_header.ToString() + "}'")
                    {
                        ava_1 += cellCollection.Rows[0].GetCell(col_index) + " → ";
                    }
                    else
                    {
                        int ava = int.Parse(str_check_arr[ii].Replace("'{", "").Replace("}'", ""));
                        ava_1 += row.GetCell(ava) + " → ";
                    }
                }
                //-- import du lieu
                msg += excute_import(context, check_sql_row3, sql_update_row3, sql_insert_row3, "", ava_1, i, count_row_null, 1,
                ex_sql_row3, ex_ava, count_ex_sql, us);
            }
        }
        return msg;
    }

    public string change_avariable(string sql, User_TK us)
    {
        string kq = sql.Replace("'@ad_user_id'", string.Format(@"N'{0}'", us.ad_user_id));
        kq = kq.Replace("'@ad_role_id'", string.Format(@"N'{0}'", us.ad_role_id));
        kq = kq.Replace("'@md_phongban_id'", string.Format(@"N'{0}'", us.md_phongban_id));
        kq = kq.Replace("'@ma_user'", string.Format(@"N'{0}'", us.ma_user));
        kq = kq.Replace("'@ma_role'", string.Format(@"N'{0}'", us.ma_role));
        kq = kq.Replace("'@ten_role'", string.Format(@"N'{0}'", us.ten_role));
        kq = kq.Replace("'@ten_phongban'", string.Format(@"N'{0}'", us.ten_phongban));

        kq = sql.Replace("@ad_user_id", us.ad_user_id).Replace("@ad_role_id", us.ad_role_id)
        .Replace("@md_phongban_id", us.md_phongban_id).Replace("@ma_user", us.ma_user)
        .Replace("@ma_role", us.ma_role).Replace("@ten_role", us.ten_role).Replace("@ten_phongban", us.ten_phongban);

        return kq;
    }

    public string copy_data_checked(HttpContext context, string ten_table, string ten_tablecp, string msg)
    {
        string sql_del = context.Session["sql_del"] == null ? "" : context.Session["sql_del"].ToString();
        string sql_copyval = "INSERT INTO " + ten_table + " SELECT * FROM " + ten_tablecp;
        string sql_delcopy = "DROP TABLE [dbo]." + ten_tablecp;
        string strDel = "", strDelEXE = "";
        if (!msg.Contains("err_import"))
        {
            bool next = true;
            try
            {
                string[] arrDel = sql_del.Split(new string[] { "<br>" }, StringSplitOptions.None);
                foreach (string a in arrDel.Where(s => !string.IsNullOrEmpty(s)))
                {
                    try
                    {
                        string[] arrA = a.Split(new string[] { "{in}" }, StringSplitOptions.None);
                        if (!string.IsNullOrEmpty(arrA[1]))
                        {
                            strDel += string.Format(@"{0},", arrA[1]);
                            if (string.IsNullOrEmpty(strDelEXE))
                                strDelEXE = string.Format(@"{0}", arrA[0]);
                        }
                    }
                    catch { }
                }

                if (!string.IsNullOrEmpty(strDelEXE) & !string.IsNullOrEmpty(strDel))
                {
                    strDel = strDel.Substring(0, strDel.Length - 1);
                    strDelEXE = string.Format(@"
                    WHILE (1=1)
                    BEGIN
                        {0} in ({1}) 
                        IF @@ROWCOUNT < 1 BREAK
                    END", strDelEXE, strDel);
                    SqlHelper.ExcuteNonQuery(strDelEXE);
                }
            }
            catch (Exception ex)
            {
                msg += string.Format(@"<div style='font-size:14px !important; color:red'>Có lỗi khi xóa dữ liệu cũ, {0}.</div>", strDelEXE + ex.Message);
                next = false;
            }

            if (next == true)
            {
                try
                {
                    SqlHelper.ExcuteNonQuery(sql_copyval);
                }
                catch (Exception ex)
                {
                    msg += string.Format(@"<div style='font-size:14px !important; color:red'>Có lỗi khi thêm dữ liệu mới, {0}.</div>", ex.Message);
                }
                msg += "<div style='font-size:14px !important; color:blue'>Đã import tất cả dữ liệu trên.</div>";
            }
        }
        else
        {
            msg += "<div style='font-size:14px !important; color:red'>Có lỗi, không thể import dữ liệu.</div>";
        }
        if (ten_tablecp.Contains("cpip_"))
        {
            try
            {
                SqlHelper.ExcuteNonQuery(sql_delcopy);
            }
            catch { }
        }
        return msg;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
