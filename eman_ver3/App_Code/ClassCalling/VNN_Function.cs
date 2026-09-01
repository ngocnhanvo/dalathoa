using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DataAcess;
using Newtonsoft.Json;

public static class VNN_Function
{
    public static ad_user ad_user_infor(System.Web.HttpContext context)
    {
        EntityContext db = new EntityContext();
        string id_taikhoan = Security.id_taikhoan(context);
        ad_user tk = db.ad_user.Where(s => s.ad_user_id == id_taikhoan).Take(1).FirstOrDefault();
        return tk;
    }

    public static User_TK get_user(string ad_user_id, string ad_role_id, string md_phongban_id)
    {
        EntityContext db = new EntityContext();
        ad_user tk = db.ad_user.Where(s => s.ad_user_id == ad_user_id).Take(1).FirstOrDefault();
        ad_role vtr = db.ad_role.Where(s => s.ad_role_id == ad_role_id).Take(1).FirstOrDefault();
        ad_department pb = db.ad_department.Where(s => s.md_phongban_id == md_phongban_id).Take(1).FirstOrDefault();
        User_TK user = new User_TK
        {
            ad_user_id = tk.ad_user_id,
            ad_role_id = vtr.ad_role_id,
            md_phongban_id = pb.md_phongban_id,
            ma_user = tk.ma_user,
            hoten = tk.hoten,
            ma_role = vtr.ma_role,
            ten_role = vtr.ten_role,
            ten_phongban = pb.ten_phongban,
            ma_phongban = pb.ma_phongban,
            mauBackground = string.IsNullOrWhiteSpace(tk.mauBackground) ? "mau_01" : tk.mauBackground,
            chuyenCachInBTSangPDF = tk.chuyenCachInBTSangPDF,
            tuDongNhanDienCachIn = tk.tuDongNhanDienCachIn
        };
        return user;
    }

    public static User_TK get_user(string ad_user_id, string ad_role_id, string md_phongban_id, EntityContext db)
    {
        var tk = db.ad_user.FirstOrDefault(s => s.ad_user_id == ad_user_id);
        var vtr = db.ad_role.Where(s => s.ad_role_id == ad_role_id).Take(1).FirstOrDefault();
        var pb = db.ad_department.Where(s => s.md_phongban_id == md_phongban_id).Take(1).FirstOrDefault();
        var user = new User_TK();
        user.ad_user_id = tk.ad_user_id;
        user.ad_role_id = vtr.ad_role_id;
        user.md_phongban_id = pb.md_phongban_id;
        user.hoten = tk.hoten;
        user.ma_user = tk.ma_user;
        user.ma_role = vtr.ma_role;
        user.ten_role = vtr.ten_role;
        user.ten_phongban = pb.ten_phongban;
        user.ma_phongban = pb.ma_phongban;
        user.mauBackground = string.IsNullOrWhiteSpace(tk.mauBackground) ? "mau_01" : tk.mauBackground;
        user.chuyenCachInBTSangPDF = tk.chuyenCachInBTSangPDF;
        user.tuDongNhanDienCachIn = tk.tuDongNhanDienCachIn;
        user.btnDongMenuTuDong = tk.btnDongMenuTuDong;
        user.btnDongMenuConTuDong = tk.btnDongMenuConTuDong;
        user.chinhanh = pb.md_doitackinhdoanh_id;
        user.ma_nhanvien = tk.ma_nhanvien;
        user.email = tk.email;
        user.email_pass = tk.email_pass;
        return user;
    }

    public static string ADGetColumn_SQL(string ten_table, string ma_module, string where_ex)
    {
        string hoatdong = "";
        if (ten_table != "" & ten_table != null)
        {
            ten_table = "and vnn_col.TABLE_NAME in ('" + ten_table + "')";
        }

        if (ma_module != "" & ma_module != null)
        {
            ma_module = "and ma_module = '" + ma_module + "'";
            hoatdong = "and col.hoatdong = 1";
        }

        string kq = string.Format(@"SELECT vnn_col.COLUMN_NAME,col.ma_column, col.editable, col.key_cot, col.sapxep, col.hoatdong,
            vnn_col.IS_NULLABLE, vnn_col.DATA_TYPE, vnn_col.CHARACTER_MAXIMUM_LENGTH, 
            OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName, type_key.CONSTRAINT_TYPE
            FROM INFORMATION_SCHEMA.COLUMNS vnn_col
            left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE vnn_key
            on vnn_col.TABLE_NAME = vnn_key.TABLE_NAME and vnn_col.COLUMN_NAME = vnn_key.COLUMN_NAME
            left join sys.foreign_keys f
            on f.name = vnn_key.CONSTRAINT_NAME
            left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS type_key
			on type_key.CONSTRAINT_NAME = vnn_key.CONSTRAINT_NAME
			left join ad_column col on col.ma_column = vnn_col.COLUMN_NAME {0}
            WHERE 1=1 {1} {2} {3} ORDER BY col.sapxep", ma_module, ten_table, hoatdong, where_ex);
        return kq;
    }

    public static string ADUpdateSelect(string sql, string display_member)
    {
        string kq = "value: {'':'',";
        System.Data.DataTable dt = Mbg.Data.SqlClient.SqlHelper.GetData(sql, "@start", 0, "@end", 999999999);

        foreach (System.Data.DataRow row in dt.Rows)
        {
            string gt = display_member;
            foreach (System.Data.DataColumn col in dt.Columns)
            {
                if (display_member.Contains(col.ColumnName.ToString()))
                {
                    gt = gt.Replace(col.ColumnName.ToString(), row[col].ToString().Replace("'", "\\'"));
                }
            }
            kq += gt + ",";
        }

        if (kq.Equals("value: '{'':'',"))
            kq = "falsefalse#Display member không có giá trị để hiển thị.";
        else
            kq = kq.Remove(kq.Length - 1) + "}";
        return kq;
    }

    public static string ADUpdateSelect_Auto(System.Web.HttpContext context)
    {
        EntityContext db = new EntityContext();
        ADmin_JSON json = new ADmin_JSON();
        json.urlData = typeof(ad_selectoption).Name;
        json.ClearCache(context, json.urlData);
        var selectoptions = json.ad_selectoptionJSON();
        string table = "";
        var ad_selectop_saves = db.ad_selectop_save.Select(s => new { s.ten_table }).Distinct().ToList();
        foreach (var sel_save in ad_selectop_saves)
        {
            if (sel_save.ten_table != table)
            {
                table = sel_save.ten_table;
                foreach (var sel in db.ad_selectoption.Where(s => s.from_sql.Contains(table + " ")).ToList())
                {
                    string id_count = sel.select_sql.Split(',')[0];
                    id_count = id_count.Replace("distinct", "");
                    id_count = Regex.Replace(id_count, "top [0-9999]", "", RegexOptions.IgnoreCase);
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
                    string sql = string.Format(@"sELeCt * fRoM ( 
                    sELeCt " + sel.select_sql +
                    @", ROW_NUMBER() OVER ( ORDER BY {0}) as RowNum " +
                    @"fRoM {1}
                    WHeRe 1=1 {2} {3} ) P WHeRe 1=1 order by RowNum asc",
                    orderby, sel.from_sql, sel.where_sql, "", id_count, "");
                    sql = ADmin_ConvertStringToCode.Avariable(context, sql, id_count, null, null, db).Replace("'", "''");
                    sql = string.Format("exec [dbo].[{3}] '{0}','{1}',{2}", sql, "", 1, "admin_excutesql");
                    sel.value_selectoption = ADUpdateSelect(sql, sel.display_member);
                    selectoptions = selectoptions.Where(s => s.ad_selectoption_id != sel.ad_selectoption_id).ToList();
                    selectoptions.Add(sel);
                }
            }
            db.ad_selectop_save.RemoveRange(db.ad_selectop_save.Where(s => s.ten_table == sel_save.ten_table));
        }
        db.SaveChanges();
        string jsonData = JsonConvert.SerializeObject(selectoptions, Formatting.Indented);
        if (jsonData != null)
        {
            if (jsonData.Replace("\n", "").Replace(" ", "").Length > 0)
            {
                json.urlData = typeof(ad_selectoption).Name;
                json.WriteJson(jsonData);
            }
        }
        return "";
    }

    public static string ADUpdateSelect_Auto(System.Web.HttpContext context, EntityContext db, ADmin_JSON json)
    {
        if (db == null)
            db = new EntityContext();
        if (json == null)
            json = new ADmin_JSON();

        json.urlData = typeof(ad_selectoption).Name;
        json.ClearCache(context, json.urlData);
        var selectoptions = json.ad_selectoptionJSON();
        string table = "";
        var ad_selectop_saves = db.ad_selectop_save.Select(s => new { s.ten_table }).Distinct().ToList();
        foreach (var sel_save in ad_selectop_saves)
        {
            if (sel_save.ten_table != table)
            {
                table = sel_save.ten_table;
                foreach (var sel in db.ad_selectoption.Where(s => s.from_sql.Contains(table + " ")).ToList())
                {
                    string id_count = sel.select_sql.Split(',')[0];
                    id_count = id_count.Replace("distinct", "");
                    id_count = Regex.Replace(id_count, "top [0-9999]", "", RegexOptions.IgnoreCase);
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
                    string sql = string.Format(@"sELeCt * fRoM ( 
                    sELeCt " + sel.select_sql +
                    @", ROW_NUMBER() OVER ( ORDER BY {0}) as RowNum " +
                    @"fRoM {1}
                    WHeRe 1=1 {2} {3} ) P WHeRe 1=1 order by RowNum asc",
                    orderby, sel.from_sql, sel.where_sql, "", id_count, "");
                    sql = ADmin_ConvertStringToCode.Avariable(context, sql, id_count, null, null, db).Replace("'", "''");
                    sql = string.Format("exec [dbo].[{3}] '{0}','{1}',{2}", sql, "", 1, "admin_excutesql");
                    sel.value_selectoption = ADUpdateSelect(sql, sel.display_member);
                    selectoptions = selectoptions.Where(s => s.ad_selectoption_id != sel.ad_selectoption_id).ToList();
                    selectoptions.Add(sel);
                }
            }
            db.ad_selectop_save.RemoveRange(db.ad_selectop_save.Where(s => s.ten_table == sel_save.ten_table));
        }
        db.SaveChanges();
        string jsonData = JsonConvert.SerializeObject(selectoptions, Formatting.Indented);
        if (jsonData != null)
        {
            if (jsonData.Replace("\n", "").Replace(" ", "").Length > 0)
            {
                json.urlData = typeof(ad_selectoption).Name;
                json.WriteJson(jsonData);
            }
        }
        return "";
    }

    public static void create_Trigger(string ten_db)
    {
        string sql_table = "USE " + ten_db + " SELECT * FROM INFORMATION_SCHEMA.tables";
        System.Data.DataTable dt_table = Mbg.Data.SqlClient.SqlHelper.GetData(sql_table, "@start", 0, "@end", 10000);
        foreach (System.Data.DataRow row_table in dt_table.Rows)
        {
            if (row_table[2].ToString() != "ad_selectop_save" & row_table[2].ToString() != "md_dbbiendong" & row_table[2].ToString() != "ad_selectoption")
            {
                string sql_trigger = "";
                sql_trigger = string.Format(
                "USE [" + ten_db + "] " +
                "IF OBJECT_ID ('update_selectoption_" + row_table[2] + "', 'TR') IS NOT NULL DROP TRIGGER update_selectoption_" + row_table[2] + ";");
                Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql_trigger);

                sql_trigger = string.Format("CREATE TRIGGER update_selectoption_" + row_table[2] +
                " ON  [" + row_table[1] + "].[" + row_table[2] + "]" +
                "   after UPDATE, insert, delete" +
                " AS" +
                " BEGIN" +
                "    exec [dbo].[admin_updateSelection] '" + row_table[2] + "'" +
                " end");
                Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql_trigger);
            }
        }
    }

    public static void create_Trigger_Del(string ten_db)
    {
        EntityContext db = new EntityContext();
        foreach (ad_remove rm in db.ad_remove.Where(s => s.hoatdong == true).OrderBy(s => s.sapxep).ToList())
        {
            string sql_trigger = "";
            sql_trigger = string.Format(
            "USE [" + ten_db + "] " +
            "IF OBJECT_ID ('update_remove_" + rm.ten_table + "', 'TR') IS NOT NULL DROP TRIGGER update_remove_" + rm.ten_table + ";");
            Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql_trigger);

            sql_trigger = "CREATE TRIGGER update_remove_" + rm.ten_table +
            " ON  [dbo].[" + rm.ten_table + "]" +
            " INSTEAD OF DELETE " +
            "\n AS" +
            "\n BEGIN" +
            "\n SET NOCOUNT ON;";
            foreach (ad_removeline rml in db.ad_removeline.Where(s => s.ad_remove_id == rm.ad_remove_id).OrderBy(s => s.sapxep).ToList())
            {
                sql_trigger += string.Format(@"
                    WHILE (1=1)
                    BEGIN
                        delete top (1000) from {0} where {1} in (SELECT {2} from Deleted)
                        IF @@ROWCOUNT < 1 BREAK
                    END
                ", rml.ten_table, rm.ten_key, rm.ten_key);
                //sql_trigger += "\n delete from "+ rml.ten_table +" where " + rm.ten_key + " in (SELECT " + rm.ten_key + " from Deleted)"; 
            }

            sql_trigger += string.Format(@"
                    WHILE (1=1)
                    BEGIN
                        delete top (1000) from {0} where {1} in (SELECT {2} from Deleted)
                        IF @@ROWCOUNT < 1 BREAK
                    END
                ", rm.ten_table, rm.ten_key, rm.ten_key);
            sql_trigger += "\n SET NOCOUNT OFF;";
            sql_trigger += "\n END";
            Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql_trigger);
        }
    }
    /*SortColumn()
     -- Tự động đánh số để sắp xếp các dòng dựa theo tiêu chí truyền vào --
     * tablename: Tên table trong csdl muốn đánh số
     * sapxep: Giá trị sắp xếp đã cập nhật từ thao tác add hoặc edit
     * name_id: tên id cha của table (ví dụ: sử dụng hàm trên ad_module thì name_id là ad_menu_id)
     * value_id: giá trị của id cha ở trên
     * name_code: tên mã (ví dụ: sử dụng hàm trên ad_module thì name_code là ma_module)
     * value_code: giá trị của mã ở trên
     * value_parentcode: giá trị của mã cha (ví dụ: sử dụng hàm trên ad_module thì value_parentcode là giá trị của column ma_module)
     * Giá trị nào không có thì set giá trị đó là null
    #SortColumn*/
    public static void SortColumn(string tablename, string sapxep, string name_id, string value_id, string name_code, string value_code, string value_parentcode)
    {
        string sql_value = "SELECT {0} FROM " + tablename + " where 1=1";
        string sql_update = "UPDATE " + tablename + " set sapxep = '{0}' where 1=1 {1} ";
        if (value_id != null & value_id != "") { sql_value += " and " + name_id + " = '" + value_id + "'"; sql_update += " and " + name_id + " = '" + value_id + "'"; }

        string sql_column = String.Format(@"sELECT vnn_col.COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS vnn_col
            left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE vnn_key
            on vnn_col.TABLE_NAME = vnn_key.TABLE_NAME and vnn_col.COLUMN_NAME = vnn_key.COLUMN_NAME
            left join sys.foreign_keys f
            on f.name = vnn_key.CONSTRAINT_NAME
            left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS type_key
			on type_key.CONSTRAINT_NAME = vnn_key.CONSTRAINT_NAME
            WHERE vnn_col.TABLE_NAME = '{0}'", tablename);
        //--
        string sql_findid = sql_column + " and type_key.CONSTRAINT_TYPE = 'PRIMARY KEY'";
        System.Data.DataTable dt_findid = Mbg.Data.SqlClient.SqlHelper.GetData(sql_findid, "@start", 0, "@end", 10000);
        string ten_id = dt_findid.Rows[0][0].ToString();
        //--
        System.Data.DataTable dt_column = Mbg.Data.SqlClient.SqlHelper.GetData(sql_column, "@start", 0, "@end", 10000);
        foreach (System.Data.DataRow row_column in dt_column.Rows)
        {
            if (row_column[0].ToString() == "ma_modulecha")
            {
                if (value_parentcode.Equals(null) | value_parentcode.Equals(""))
                {
                    sql_value += " and (ma_modulecha = '' or ma_modulecha is null)";
                    sql_update += " and (ma_modulecha = '' or ma_modulecha is null)";
                }
                else
                {
                    sql_value += " and ma_modulecha = N'" + value_parentcode + "'";
                    sql_update += " and ma_modulecha = N'" + value_parentcode + "'";
                }
                break;
            }
            else if (row_column[0].ToString() == "ma_menucha")
            {
                if (value_parentcode.Equals(null) | value_parentcode.Equals(""))
                {
                    sql_value += " and (ma_menucha = '' or ma_menucha is null)";
                    sql_update += " and (ma_menucha = '' or ma_menucha is null)";
                }
                else
                {
                    sql_value += " and ma_menucha = N'" + value_parentcode + "'";
                    sql_update += " and ma_menucha = N'" + value_parentcode + "'";
                }
                break;
            }
        }
        //--
        System.Data.DataTable dt_value = Mbg.Data.SqlClient.SqlHelper.GetData(string.Format(sql_value, "*") + " order by sapxep", "@start", 0, "@end", 10000000000);
        foreach (System.Data.DataRow row_value in dt_value.Rows)
        {
            if (row_value["sapxep"].ToString() != "" & row_value["sapxep"].ToString() != null & VNN_Validate.check_number(row_value["sapxep"].ToString(), "int"))
            {
                if (row_value[name_code].ToString() != value_code & row_value["sapxep"].ToString() == sapxep)
                {
                    sapxep = VNN_Config.load_number((int.Parse(sapxep) + 1).ToString(), 10);
                    string update = string.Format(sql_update, sapxep, " and " + ten_id + " = '" + row_value[ten_id] + "'");
                    Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(update);
                }
            }
        }
    }

    public static void XoaPhanQuyen(System.Web.HttpContext context, string ad_module_id, string ad_case_id)
    {
        EntityContext db = new EntityContext();
        if (ad_case_id == null | ad_case_id == "")
        {
            foreach (var pq in db.ad_role_mmc.Where(s => s.ad_module_id == ad_module_id).ToList())
            {
                db.ad_role_mmc.Remove(pq);
            }
        }
        else
        {
            foreach (var pq in db.ad_role_mmc.Where(s => s.ad_module_id == ad_module_id & s.ad_case_id == ad_case_id).ToList())
            {
                db.ad_role_mmc.Remove(pq);
            }
        }
        db.SaveChanges();
    }

    public static void XoaPhanQuyen(System.Web.HttpContext context, string ad_module_id, string ad_case_id, ADmin_JSON json, EntityContext db = null)
    {
        if (db == null)
            db = new EntityContext();

        List<ad_role_mmc> lstAdRoleMMC = json.ad_role_mmcJSON();
        List<ad_role_mmcol> lstAdRoleMMCol = json.ad_role_mmcolJSON();
        List<string> idsDelRoleMMC = new List<string>();
        List<string> idsDelRoleMMCol = new List<string>();
        var xoaModule = string.IsNullOrWhiteSpace(ad_case_id);
        if (xoaModule)
        {
            foreach (var pq in db.ad_role_mmc.Where(s => s.ad_module_id == ad_module_id).ToList())
            {
                db.ad_role_mmc.Remove(pq);
                idsDelRoleMMC.Add(pq.ad_role_mmc_id);
            }

            foreach (var pq in db.ad_role_mmcol.Where(s => s.ad_module_id == ad_module_id).ToList())
            {
                db.ad_role_mmcol.Remove(pq);
                idsDelRoleMMCol.Add(pq.ad_role_mmcol_id);
            }

            foreach (var pq in db.ad_role_where.Where(s => s.ad_module_id == ad_module_id).ToList())
            {
                db.ad_role_where.Remove(pq);
            }

            foreach (var pq in db.ad_role_mmvalue.Where(s => s.ad_module_id == ad_module_id).ToList())
            {
                db.ad_role_mmvalue.Remove(pq);
            }
        }
        else
        {
            foreach (ad_role_mmc pq in db.ad_role_mmc.Where(s => s.ad_module_id == ad_module_id & s.ad_case_id == ad_case_id).ToList())
            {
                db.ad_role_mmc.Remove(pq);
                idsDelRoleMMC.Add(pq.ad_role_mmc_id);
            }
        }
        db.SaveChanges();

        lstAdRoleMMC = lstAdRoleMMC.Where(s => !idsDelRoleMMC.Contains(s.ad_role_mmc_id)).ToList();
        string jsonData = JsonConvert.SerializeObject(lstAdRoleMMC, Formatting.Indented);
        json.urlData = typeof(ad_role_mmc).Name;
        json.WriteJson(jsonData);

        if (xoaModule)
        {
            lstAdRoleMMCol = lstAdRoleMMCol.Where(s => !idsDelRoleMMCol.Contains(s.ad_role_mmcol_id)).ToList();
            jsonData = JsonConvert.SerializeObject(lstAdRoleMMCol, Formatting.Indented);
            json.urlData = typeof(ad_role_mmcol).Name;
            json.WriteJson(jsonData);
        }
    }

    public static ad_role_mmc ThemPhanQuyen(System.Web.HttpContext context, string ad_role_id, string ad_menu_id, string ad_module_id, string ad_case_id)
    {
        ADmin_JSON json = new ADmin_JSON();
        List<ad_role_mmc> lstAdRoleMMC = json.ad_role_mmcJSON();
        EntityContext db = new EntityContext();
        string id_taikhoan = Security.id_taikhoan(context);
        string id_vaitro = Security.id_vaitro(context);
        string id_phongban = Security.id_phongban(context);
        ad_user tk = db.ad_user.Where(s => s.ad_user_id == id_taikhoan).Take(1).FirstOrDefault();
        ad_role vtr = db.ad_role.Where(s => s.ad_role_id == id_vaitro).Take(1).FirstOrDefault();
        ad_department pb = db.ad_department.Where(s => s.md_phongban_id == id_phongban).Take(1).FirstOrDefault();
        ad_role_mmc pqBB = null;

        if (ad_case_id == null | ad_case_id == "")
        {
            foreach (ad_case cn in db.ad_case.Where(s => s.ad_module_id == ad_module_id).ToList())
            {
                ad_role_mmc pq = new ad_role_mmc();
                pq.ad_role_mmc_id = Helper.getNewId();
                pq.ad_menu_id = ad_menu_id;
                pq.ad_module_id = ad_module_id;
                pq.ad_case_id = cn.ad_case_id;
                pq.ten_case = cn.ten_case;
                pq.ad_role_id = ad_role_id;
                pq.mota = null;
                pq.nguoitao = tk.ad_user_id;
                pq.vaitrotao = vtr.ad_role_id;
                pq.bophantao = pb.md_phongban_id;
                pq.value_nguoitao = tk.ma_user;
                pq.value_vaitrotao = vtr.ten_role;
                pq.value_bophantao = pb.ten_phongban;
                pq.nguoicapnhat = tk.ad_user_id;
                pq.vaitrocapnhat = vtr.ad_role_id;
                pq.bophancapnhat = pb.md_phongban_id;
                pq.value_nguoicapnhat = tk.ma_user;
                pq.value_vaitrocapnhat = vtr.ten_role;
                pq.value_bophancapnhat = pb.ten_phongban;
                pq.ngaytao = DateTime.Now;
                pq.ngaycapnhat = DateTime.Now;
                pq.hoatdong = true;
                db.ad_role_mmc.Add(pq);
                lstAdRoleMMC.Add(pq);
            }
        }
        else
        {
            foreach (ad_case cn in db.ad_case.Where(s => s.ad_module_id == ad_module_id & s.ad_case_id == ad_case_id).ToList())
            {
                pqBB = new ad_role_mmc
                {
                    ad_role_mmc_id = Helper.getNewId(),
                    ad_menu_id = ad_menu_id,
                    ad_module_id = ad_module_id,
                    ad_case_id = cn.ad_case_id,
                    ten_case = cn.ten_case,
                    ad_role_id = ad_role_id,
                    mota = null,

                    nguoitao = tk.ad_user_id,
                    vaitrotao = vtr.ad_role_id,
                    bophantao = pb.md_phongban_id,
                    value_nguoitao = tk.ma_user,
                    value_vaitrotao = vtr.ten_role,
                    value_bophantao = pb.ten_phongban,

                    nguoicapnhat = tk.ad_user_id,
                    vaitrocapnhat = vtr.ad_role_id,
                    bophancapnhat = pb.md_phongban_id,
                    value_nguoicapnhat = tk.ma_user,
                    value_vaitrocapnhat = vtr.ten_role,
                    value_bophancapnhat = pb.ten_phongban,

                    ngaytao = DateTime.Now,
                    ngaycapnhat = DateTime.Now,
                    hoatdong = true
                };
                db.ad_role_mmc.Add(pqBB);
                lstAdRoleMMC.Add(pqBB);
            }
        }
        db.SaveChanges();

        string jsonData = JsonConvert.SerializeObject(lstAdRoleMMC, Formatting.Indented);
        json.urlData = typeof(ad_role_mmc).Name;
        json.WriteJson(jsonData);
        return pqBB;
    }

    public static ad_role_mmc ThemPhanQuyenChucNang(System.Web.HttpContext context, string ad_role_id, string ad_menu_id, string ad_module_id, string caseId, EntityContext db)
    {
        string id_taikhoan = Security.id_taikhoan(context);
        string id_vaitro = Security.id_vaitro(context);
        string id_phongban = Security.id_phongban(context);
        var tk = db.ad_user.Where(s => s.ad_user_id == id_taikhoan).Take(1).FirstOrDefault();
        var vtr = db.ad_role.Where(s => s.ad_role_id == id_vaitro).Take(1).FirstOrDefault();
        var pb = db.ad_department.Where(s => s.md_phongban_id == id_phongban).Take(1).FirstOrDefault();
        ad_role_mmc pqBB = null;

        foreach (var cn in db.ad_case.Where(s => s.ad_module_id == ad_module_id & s.ad_case_id == caseId).ToList())
        {
            pqBB = new ad_role_mmc
            {
                ad_role_mmc_id = Helper.getNewId(),
                ad_menu_id = ad_menu_id,
                ad_module_id = ad_module_id,
                ad_case_id = cn.ad_case_id,
                ten_case = cn.ten_case,
                ad_role_id = ad_role_id,
                mota = null,

                nguoitao = tk.ad_user_id,
                vaitrotao = vtr.ad_role_id,
                bophantao = pb.md_phongban_id,
                value_nguoitao = tk.ma_user,
                value_vaitrotao = vtr.ten_role,
                value_bophantao = pb.ten_phongban,

                nguoicapnhat = tk.ad_user_id,
                vaitrocapnhat = vtr.ad_role_id,
                bophancapnhat = pb.md_phongban_id,
                value_nguoicapnhat = tk.ma_user,
                value_vaitrocapnhat = vtr.ten_role,
                value_bophancapnhat = pb.ten_phongban,

                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now,
                hoatdong = true
            };
        }

        return pqBB;
    }

    public static ad_role_mmcol ThemPhanQuyenCot(System.Web.HttpContext context, string ad_role_id, string ad_menu_id, string ad_module_id, string columnId, bool edit, bool view, EntityContext db)
    {
        string id_taikhoan = Security.id_taikhoan(context);
        string id_vaitro = Security.id_vaitro(context);
        string id_phongban = Security.id_phongban(context);
        var tk = db.ad_user.Where(s => s.ad_user_id == id_taikhoan).Take(1).FirstOrDefault();
        var vtr = db.ad_role.Where(s => s.ad_role_id == id_vaitro).Take(1).FirstOrDefault();
        var pb = db.ad_department.Where(s => s.md_phongban_id == id_phongban).Take(1).FirstOrDefault();
        ad_role_mmcol pqBB = null;

        foreach (var cn in db.ad_column.Where(s => s.ad_module_id == ad_module_id & s.ad_column_id == columnId).ToList())
        {
            pqBB = new ad_role_mmcol
            {
                ad_role_mmcol_id = Helper.getNewId(),
                ad_menu_id = ad_menu_id,
                ad_module_id = ad_module_id,
                ad_column_id = cn.ad_column_id,
                ten_column = cn.ten_column,
                ad_role_id = ad_role_id,
                disableEdit = edit,
                disableView = view,
                mota = null,

                nguoitao = tk.ad_user_id,
                vaitrotao = vtr.ad_role_id,
                bophantao = pb.md_phongban_id,
                value_nguoitao = tk.ma_user,
                value_vaitrotao = vtr.ten_role,
                value_bophantao = pb.ten_phongban,

                nguoicapnhat = tk.ad_user_id,
                vaitrocapnhat = vtr.ad_role_id,
                bophancapnhat = pb.md_phongban_id,
                value_nguoicapnhat = tk.ma_user,
                value_vaitrocapnhat = vtr.ten_role,
                value_bophancapnhat = pb.ten_phongban,

                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now,
                hoatdong = true
            };
        }

        return pqBB;
    }

    public static void ThemChucNang(System.Web.HttpContext context, string ma_menu, string ma_module, string ad_module_id)
    {
        ADmin_JSON json = new ADmin_JSON();
        List<ad_case> lstAdCase = json.ad_caseJSON();
        EntityContext db = new EntityContext();
        string id_taikhoan = Security.id_taikhoan(context);
        string id_vaitro = Security.id_vaitro(context);
        string id_phongban = Security.id_phongban(context);
        ad_user tk = db.ad_user.Where(s => s.ad_user_id == id_taikhoan).Take(1).FirstOrDefault();
        ad_role vtr = db.ad_role.Where(s => s.ad_role_id == id_vaitro).Take(1).FirstOrDefault();
        ad_department pb = db.ad_department.Where(s => s.md_phongban_id == id_phongban).Take(1).FirstOrDefault();
        string ten_case = "Thêm mới 1 dòng:Sửa dòng đã chọn:Xóa dòng đã chọn:Xem chi tiết";
        for (int i = 0; i < ten_case.Split(':').Count(); i++)
        {
            string hamxuly = "click_add(tengrid)", width = "440";
            bool isview = false, hidden_modify = false;
            string logo = "glyphicon glyphicon-plus-sign";
            if (i == 1)
            {
                hamxuly = "click_edit(tengrid)";
                logo = "glyphicon glyphicon-edit";
                hidden_modify = true;
            }
            else if (i == 2)
            {
                hamxuly = "click_del(tengrid)";
                logo = "glyphicon glyphicon-trash";
                width = "300";
                hidden_modify = true;
            }
            else if (i == 3)
            {
                hamxuly = "click_view(tengrid)"; isview = true;
                logo = "glyphicon glyphicon-eye-open";
                hidden_modify = false;
            }
            ad_case cn = new ad_case
            {
                ad_case_id = Helper.getNewId(),
                ma_case = "CA_00_" + DateTime.Now.AddSeconds(i).ToString("ddMMyyyyhhmmssffftt"),
                ten_case = ten_case.Split(':')[i],
                ma_menu = ma_menu,
                ma_module = ma_module,
                ad_module_id = ad_module_id,
                hamxuly = hamxuly,
                isview = false,
                dodaiForm = width,
                id_parent = true,
                sapxep = VNN_Config.load_number(i.ToString(), 10),
                canhgiua = true,
                logo = logo,
                hidden_modify = hidden_modify,

                nguoitao = tk.ad_user_id,
                vaitrotao = vtr.ad_role_id,
                bophantao = pb.md_phongban_id,
                value_nguoitao = tk.ma_user,
                value_vaitrotao = vtr.ten_role,
                value_bophantao = pb.ten_phongban,

                nguoicapnhat = tk.ad_user_id,
                vaitrocapnhat = vtr.ad_role_id,
                bophancapnhat = pb.md_phongban_id,
                value_nguoicapnhat = tk.ma_user,
                value_vaitrocapnhat = vtr.ten_role,
                value_bophancapnhat = pb.ten_phongban,

                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now,
                mota = "",
                hoatdong = true
            };
            db.ad_case.Add(cn);
            lstAdCase.Add(cn);
        }
        db.SaveChanges();

        string jsonData = JsonConvert.SerializeObject(lstAdCase, Formatting.Indented);
        json.urlData = typeof(ad_case).Name;
        json.WriteJson(jsonData);
    }

    public static bool TestSQL(string sql)
    {
        bool sql_ok = false;
        try { object sql_select = Mbg.Data.SqlClient.SqlHelper.GetData(sql); sql_ok = true; }
        catch { sql_ok = false; }
        return sql_ok;
    }

    public static bool Test_PrimaryKey(string ten_table)
    {
        if (ADmin_UpdateLinq.get_KeyOfTable(ten_table) == null)
            return false;
        else
            return true;
    }

    //public static string loaddulieu_Auto(LinqDataContext db, string ma_module)
    //{
    //    string kq = "";
    //    db.SubmitChanges();
    //    ad_autoload_mmc au_mod = db.ad_autoload_mmcs.Where(s => s.ma_module == ma_module).Take(1).FirstOrDefault();
    //    if (au_mod != null)
    //    {
    //        ad_autoload au_load = db.ad_autoloads.Where(s => s.ad_autoload_id == au_mod.ad_autoload_id).Take(1).FirstOrDefault();
    //        int stt = int.Parse(au_load.sapxep);
    //        kq = au_load.tanso = Helper.getNewId();
    //        VNN_VariablePublic.session_bd[stt] = kq;
    //        db.SubmitChanges();
    //    }
    //    return kq;
    //}

    public static string loaddulieu_Auto(string ma_module)
    {
        string kq = "";
        ADUpdateSelect_Auto(System.Web.HttpContext.Current, null, null);
        return kq;
    }

    public static string loaddulieu_Auto(EntityContext db, string ma_module)
    {
        string kq = "";
        db.SaveChanges();
        ADUpdateSelect_Auto(System.Web.HttpContext.Current, db, null);
        return kq;
    }

    public static string loaddulieu_Auto(EntityContext db, ADmin_JSON json, string ma_module)
    {
        string kq = "";
        db.SaveChanges();
        ADUpdateSelect_Auto(System.Web.HttpContext.Current, db, json);
        return kq;
    }

    public static string FindString(string strSource, string strStart, string strEnd)
    {
        int Start, End;
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }
        else
        {
            return "";
        }
    }

    public static string IsNull(string s, string value)
    {
        if (String.IsNullOrEmpty(s))
            return value;
        else
            return s;
    }

    public static string set_Icon(string logo, string class_, string function, string id, string title)
    {
        string kq = "";
        if (logo.Contains("/"))
        {
            kq = "<img class=\"" + class_ + "\" onclick=\"" + function + "\" id=\"" + id + "\" title=\"" + title + "\" alt=\"\" src=\"" + logo + "\"/>";
        }
        else
        {
            kq = "<span class=\"" + class_ + " " + logo + "\" onclick=\"" + function + "\" id=\"" + id + "\" title=\"" + title + "\"></span>";
        }
        return kq;
    }

    public static string setFunction_AutoLoad(EntityContext db)
    {
        string kq = "";
        kq += "\n\nfunction Load_Auto(i_dem, loadgrid_at) {";
        foreach (var au_load in db.ad_autoload.Where(s => s.hoatdong == true).ToList())
        {
            int dem = int.Parse(au_load.sapxep);
            kq += "\nif(i_dem == " + dem + ") {";
            foreach (var au_mod in db.ad_autoload_mmc.Where(s => s.hoatdong == true & s.ad_autoload_id == au_load.ad_autoload_id).ToList())
            {
                kq += "\ntry { loadclick('grid" + au_mod.ma_module + "', 'edit', i_dem); } catch (r) { }";
            }
            kq += "\nif(loadgrid_at == true) { dem_Records(); }";
            kq += "\nif(id_action[i_dem] != null) { id_action[i_dem] = null; }";
            kq += "\nif (id_oper[i_dem] != null) { id_oper[i_dem] = null; }";
            kq += "\n}";
        }
        kq += "\n}";
        return kq;
    }

    public static void SetFormValue(string key, string value)
    {
        var collection = System.Web.HttpContext.Current.Request.Form;

        // Get the "IsReadOnly" protected instance property.
        var propInfo = collection.GetType().GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

        // Mark the collection as NOT "IsReadOnly"
        propInfo.SetValue(collection, false, new object[] { });

        // Change the value of the key.
        if (collection.AllKeys.Contains(key))
            collection[key] = value;
        else
        {
            collection.Add(key, value);
        }

        // Mark the collection back as "IsReadOnly"     
        propInfo.SetValue(collection, true, new object[] { });
    }

    public static void Set_DefaultvalueColumn(System.Web.HttpContext context, string action)
    {
        string formatdate = VNN_Config.get_FormatDate();
        EntityContext db = new EntityContext();
        string id_taikhoan = Security.id_taikhoan(context);
        string id_vaitro = Security.id_vaitro(context);
        string id_phongban = Security.id_phongban(context);
        ad_user tk = db.ad_user.Where(s => s.ad_user_id == id_taikhoan).Take(1).FirstOrDefault();
        ad_role vtr = db.ad_role.Where(s => s.ad_role_id == id_vaitro).Take(1).FirstOrDefault();
        ad_department pb = db.ad_department.Where(s => s.md_phongban_id == id_phongban).Take(1).FirstOrDefault();
        if (action == "edit")
        {
            SetFormValue("value_nguoicapnhat", tk.ma_user);
            SetFormValue("value_vaitrocapnhat", vtr.ten_role);
            SetFormValue("value_bophancapnhat", pb.ten_phongban);
            SetFormValue("nguoicapnhat", tk.ad_user_id);
            SetFormValue("vaitrocapnhat", vtr.ad_role_id);
            SetFormValue("bophancapnhat", pb.md_phongban_id);

            SetFormValue("ngaycapnhat", DateTime.Now.ToString(formatdate).Replace("SA", "AM").Replace("CH", "PM"));
            if (context.Request.Form["hoatdong"] == null)
            {
                SetFormValue("hoatdong", "True");
            }
        }
        else
        {
            SetFormValue("nguoitao", tk.ad_user_id);
            SetFormValue("vaitrotao", vtr.ad_role_id);
            SetFormValue("bophantao", pb.md_phongban_id);
            SetFormValue("value_nguoitao", tk.ma_user);
            SetFormValue("value_vaitrotao", vtr.ten_role);
            SetFormValue("value_bophantao", pb.ten_phongban);

            SetFormValue("nguoicapnhat", tk.ad_user_id);
            SetFormValue("vaitrocapnhat", vtr.ad_role_id);
            SetFormValue("bophancapnhat", pb.md_phongban_id);
            SetFormValue("value_nguoicapnhat", tk.ma_user);
            SetFormValue("value_vaitrocapnhat", vtr.ten_role);
            SetFormValue("value_bophancapnhat", pb.ten_phongban);

            SetFormValue("ngaytao", DateTime.Now.ToString(formatdate).Replace("SA", "AM").Replace("CH", "PM"));
            SetFormValue("ngaycapnhat", DateTime.Now.ToString(formatdate).Replace("SA", "AM").Replace("CH", "PM"));
            if (context.Request.Form["hoatdong"] == null)
            {
                SetFormValue("hoatdong", "True");
            }
        }
    }

    public static string Modify_Function(System.Web.HttpContext context, string ma_module, string id_new, string table_name, string action, string[] column_ex, EntityContext db)
    {
        string kq = "";
        string sql = ADGetColumn_SQL(table_name, ma_module, "");
        System.Data.DataTable dt = Mbg.Data.SqlClient.SqlHelper.GetData(sql, "@start", 0, "@end", 100000000);
        if (action == "edit")
        {
            string update_sql = " Update " + table_name + " ";
            string values_sql = " Set ";
            string where_sql = " Where 1=1 ";
            VNN_Function.Set_DefaultvalueColumn(context, "edit");
            foreach (System.Data.DataRow row_column in dt.Rows)
            {
                if (row_column["key_cot"].ToString() == "true")
                {
                    VNN_Function.SetFormValue(row_column["ma_column"].ToString(), null);
                    where_sql += " and " + row_column["ma_column"].ToString() + " = '" + context.Request.Form["id"] + "'";
                }

                if (row_column["editable"].ToString() == "true" |
                    VNN_Validate.check_column_default(row_column["ma_column"].ToString(), "edit", column_ex))
                {
                    string row_ma_column = row_column["ma_column"].ToString(), FormValue = context.Request.Form[row_ma_column];
                    if (FormValue != "VNN_notpost")
                    {
                        values_sql += row_column["ma_column"].ToString() + " = " + VNN_Validate.check_ValueForm(row_ma_column, action, FormValue, row_column["data_type"].ToString()) + ",";
                    }
                }
            }
            values_sql = values_sql.Remove(values_sql.Length - 1);
            kq = update_sql + values_sql + where_sql;
            Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(update_sql + values_sql + where_sql);
        }
        else if (action == "add")
        {
            string insert_sql = "Insert into " + table_name + "(";
            string values_sql = "Values (";
            foreach (System.Data.DataRow row_column in dt.Rows)
            {
                if (row_column["key_cot"].ToString() == "true")
                    VNN_Function.SetFormValue(row_column["ma_column"].ToString(), id_new);

                if (row_column["editable"].ToString() == "true" | row_column["key_cot"].ToString() == "true" |
                    VNN_Validate.check_column_default(row_column["ma_column"].ToString(), "add", column_ex))
                {
                    insert_sql += row_column["ma_column"].ToString() + ",";
                    values_sql += VNN_Validate.check_ValueForm(row_column["ma_column"].ToString(), action, context.Request.Form[row_column["ma_column"].ToString()], row_column["data_type"].ToString()) + ",";
                }
            }
            insert_sql = insert_sql.Remove(insert_sql.Length - 1) + ")";
            values_sql = values_sql.Remove(values_sql.Length - 1) + ")";
            kq = insert_sql + values_sql;
            Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(insert_sql + values_sql);
        }
        else if (action == "del")
        {
            string del_sql = "delete " + table_name;
            string where_sql = " Where 1=1 ";

            foreach (System.Data.DataRow row_column in dt.Rows)
            {
                if (row_column["key_cot"].ToString() == "true")
                {
                    where_sql += " and " + row_column["ma_column"].ToString() + " = '" + context.Request.Form["id"] + "'";
                }
                else
                {
                    break;
                }
            }
            Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(del_sql + where_sql);
        }
        return kq;
    }

    public static string Write_log(System.Web.HttpContext context, string ma_module, string ma_modulebs, string oper, string noidung, EntityContext db, bool? nosubmit = false, User_TK us = null)
    {
        string kq = "";
        ad_case cn = null;

        if (us == null)
            us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context), db);

        if (oper != "add" & oper != "edit" & oper != "del")
        {
            if (ma_modulebs != null & ma_modulebs != "")
                cn = db.ad_case.Where(s => s.ma_module == ma_modulebs & s.ma_case.Equals(oper)).Take(1).FirstOrDefault();
            else
                cn = db.ad_case.Where(s => s.ma_module == ma_module & s.ma_case.Equals(oper)).Take(1).FirstOrDefault();
        }
        else
        {
            if (ma_modulebs != null & ma_modulebs != "")
                cn = db.ad_case.Where(s => s.ma_module == ma_modulebs & s.hamxuly.Equals("click_" + oper + "(tengrid)")).Take(1).FirstOrDefault();
            else
                cn = db.ad_case.Where(s => s.ma_module == ma_module & s.hamxuly.Equals("click_" + oper + "(tengrid)")).Take(1).FirstOrDefault();
        }

        var log = new ad_log
        {
            ad_log_id = Helper.getNewId(),
            ad_case_id = cn.ad_case_id,
            ad_module_id = cn.ad_module_id,
            mota = noidung,
            hoatdong = true
        };
        log = Helper.setDefaultValueWhenInsertOrUpdate(log, us, false);
        db.ad_log.Add(log);

        if (!nosubmit.GetValueOrDefault(false))
            db.SaveChanges();

        return kq;
    }

    public static void WriteSCTDaXoa(System.Web.HttpContext context, User_TK us, string sct, EntityContext db)
    {
        if (us == null)
            us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context));

        var sctDaXoa = new md_sochungtudaxoa
        {
            md_sochungtudaxoa_id = Helper.getNewId(),
            value = sct,
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

            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now,
            mota = "",
            hoatdong = true
        };
        db.md_sochungtudaxoa.Add(sctDaXoa);
    }

    public static string BytesToString(long byteCount, int dec)
    {
        string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
        if (byteCount == 0)
            return "0" + suf[0];
        long bytes = Math.Abs(byteCount);
        int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
        double num = Math.Round(bytes / Math.Pow(1024, place), dec);
        return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
    }

    public static void Copyfile_module(System.Web.HttpContext context, string ma_module_org, string ma_module_cp, string ma_case_org, string ma_case_copy, string hamxuly_org, string hamxuly_copy)
    {
        string[] arr_ma_case_org = ma_case_org.Split('ξ');
        string[] arr_ma_case_copy = ma_case_copy.Split('ξ');
        string[] arr_hamxuly_org = hamxuly_org.Split('ξ');
        string[] arr_hamxuly_copy = hamxuly_copy.Split('ξ');
        //copy modify file
        string filepath_org = Security.UrlBase() + "Controller/JQGridModify/JQGrid" + ma_module_org + "Modify.ashx";
        string filepath_cp = Security.UrlBase() + "Controller/JQGridModify/JQGrid" + ma_module_cp + "Modify.ashx";
        filepath_org = context.Server.MapPath(filepath_org);
        filepath_cp = context.Server.MapPath(filepath_cp);

        string w_org = System.IO.File.ReadAllText(filepath_org);
        string w_cp = System.IO.File.ReadAllText(filepath_cp);
        w_cp = w_cp.Replace(w_cp, w_org);
        w_cp = w_cp.Replace("JQGrid" + ma_module_org + "Modify", "JQGrid" + ma_module_cp + "Modify");
        for (int i = 0; i < arr_ma_case_org.Count() - 1; i++)
        {
            w_cp = w_cp.Replace("\"" + arr_ma_case_org[i] + "\":", "\"" + arr_ma_case_copy[i] + "\":");
            w_cp = w_cp.Replace("this." + arr_ma_case_org[i] + "(", "this." + arr_ma_case_copy[i] + "(");
            w_cp = w_cp.Replace(arr_ma_case_org[i] + "(HttpContext context", arr_ma_case_copy[i] + "(HttpContext context");
        }
        System.IO.File.WriteAllText(filepath_cp, w_cp, System.Text.Encoding.Unicode);
        System.Threading.Thread.Sleep(200);
        //copy load file
        filepath_org = Security.UrlBase() + "Controller/JQGrid/JQGrid" + ma_module_org + "Load.ashx";
        filepath_cp = Security.UrlBase() + "Controller/JQGrid/JQGrid" + ma_module_cp + "Load.ashx";
        filepath_org = context.Server.MapPath(filepath_org);
        filepath_cp = context.Server.MapPath(filepath_cp);

        w_org = System.IO.File.ReadAllText(filepath_org);
        w_cp = System.IO.File.ReadAllText(filepath_cp);
        w_cp = w_cp.Replace(w_cp, w_org);
        w_cp = w_cp.Replace("JQGrid" + ma_module_org + "Load", "JQGrid" + ma_module_cp + "Load");
        System.IO.File.WriteAllText(filepath_cp, w_cp, System.Text.Encoding.Unicode);
        System.Threading.Thread.Sleep(200);

        //copy aspx file
        filepath_org = Security.UrlBase() + "View/Menu/Content/Module/" + ma_module_org + ".aspx";
        filepath_cp = Security.UrlBase() + "View/Menu/Content/Module/" + ma_module_cp + ".aspx";
        filepath_org = context.Server.MapPath(filepath_org);
        filepath_cp = context.Server.MapPath(filepath_cp);

        w_org = System.IO.File.ReadAllText(filepath_org);
        w_cp = System.IO.File.ReadAllText(filepath_cp);
        w_cp = w_cp.Replace(w_cp, w_org);
        w_cp = w_cp.Replace(ma_module_org, ma_module_cp);
        System.IO.File.WriteAllText(filepath_cp, w_cp, System.Text.Encoding.Unicode);
        System.Threading.Thread.Sleep(200);

        //copy script file
        filepath_org = Security.UrlBase() + "js/Module_script/" + ma_module_org + ".js";
        filepath_cp = Security.UrlBase() + "js/Module_script/" + ma_module_cp + ".js";
        filepath_org = context.Server.MapPath(filepath_org);
        filepath_cp = context.Server.MapPath(filepath_cp);
        w_org = System.IO.File.ReadAllText(filepath_org, System.Text.Encoding.Unicode);
        w_cp = System.IO.File.ReadAllText(filepath_cp, System.Text.Encoding.Unicode);
        w_cp = w_cp.Replace(w_cp, w_org);
        for (int i = 0; i < arr_ma_case_org.Count() - 1; i++)
        {
            w_cp = w_cp.Replace(arr_hamxuly_org[i], arr_hamxuly_copy[i]);
            w_cp = w_cp.Replace("//start " + arr_ma_case_org[i], "//start " + arr_ma_case_copy[i]);
            w_cp = w_cp.Replace("//end " + arr_ma_case_org[i], "//end " + arr_ma_case_copy[i]);
            w_cp = w_cp.Replace("?oper=" + arr_ma_case_org[i], "?oper=" + arr_ma_case_copy[i]);
        }
        w_cp = w_cp.Replace("JQGrid" + ma_module_org + "Modify.ashx", "JQGrid" + ma_module_cp + "Modify.ashx");
        System.IO.File.WriteAllText(filepath_cp, w_cp, System.Text.Encoding.Unicode);
    }
    public static string count_Module(System.Web.HttpContext context, ad_module mod0, string ma_module, EntityContext db)
    {
        string msg = "", from_sql = "", where_sql = "", header_grid = "", where_ex = "";
        if (mod0 == null)
        {
            mod0 = db.ad_module.FirstOrDefault(s => s.ma_module == ma_module);
        }

        /*if (mod0.mod_lienket != null)
        {
            ad_module modlk = db.ad_module.FirstOrDefault(s => s.ad_module_id == mod0.mod_lienket);
            if (modlk != null)
            {
                from_sql = modlk.from_sql;
                where_sql = modlk.where_sql + " " + mod0.where_sql;
                header_grid = modlk.header_grid + " " + mod0.header_grid;
            }
            else
            {
                from_sql = mod0.from_sql;
                where_sql = mod0.where_sql;
                header_grid = mod0.header_grid;
            }
        }
        else*/
        {
            from_sql = mod0.from_sql;
            where_sql = mod0.where_sql;
            header_grid = mod0.header_grid;
        }
        string sql_count = string.Format(@"select count(1) from {0} where 1=1 {1} {2}", from_sql, where_sql, header_grid);
        sql_count = ADmin_ConvertStringToCode.Avariable(context, sql_count, "", "", null, db);
        try
        {
            System.Data.DataTable dt_count = Mbg.Data.SqlClient.SqlHelper.GetData(sql_count);

            if (dt_count.Rows.Count > 0)
            {
                int count = int.Parse(dt_count.Rows[0][0].ToString());
                if (count > 0)
                    msg += "<span class=\"esc_count_" + mod0.ma_module + " badge badge-primary\">" + count + "</span>";
                else
                    msg += "<span class=\"esc_count_" + mod0.ma_module + " esc_display badge badge-primary\">" + count + "</span>";
            }
        }
        catch { }
        return msg;
    }
}
