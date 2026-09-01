<%@ WebHandler Language="C#" Class="JQGridMD_01_MDZModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;

public class JQGridMD_01_MDZModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "loadModule":
                this.loadModule(context);
                break;
            case "CA_01_CopyModule":
                this.CA_01_CopyModule(context);
                break;
            case "load_ad_modulecha":
                this.load_ad_modulecha(context);
                break;
            case "loadlist":
                this.LoadList(context);
                break;
            case "selectoption":
                this.selectoption(context);
                break;
            case "selectoption_module":
                this.selectoption_module(context);
                break;
            case "selectoption_double_click":
                this.selectoption_double_click(context);
                break;
            default:
                break;
        }
    }

    public void add(HttpContext context)
    {
        ADmin_JSON json = new ADmin_JSON();
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module_ = context.Request.QueryString["ma_module"];
        List<ad_module> lstAdModule = json.ad_moduleJSON();
        bool row_count = bool.Parse(context.Request.Form["row_count"]);
        string id = context.Request.Form["id_parent"];
        string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
        string ma_module = context.Request.Form["ma_module"];
        string ma_modulecha = context.Request.Form["ma_modulecha"].removeAllSpaceOrTrimText(false);
        if (ma_modulecha.Length > 32)
            ma_modulecha = ma_modulecha.Split('-')[0];
        string new_select = context.Request.Form["select_sql"];
        string new_from = context.Request.Form["from_sql"];
        string new_where = context.Request.Form["where_sql"];
        string new_orderby = context.Request.Form["orderby_sql"];
        string new_groupby = context.Request.Form["groupby_sql"];
        string loai_module = context.Request.Form["loai_module"];
        string url = context.Request.Form["url"];
        string thuake = context.Request.Form["thuake"];
        string header_grid = context.Request.Form["header_grid"];
        string double_click = context.Request.Form["double_click"].removeAllSpaceOrTrimText(false);
        if (double_click.Length > 32)
            double_click = double_click.Split('-')[0];
        bool mutil_select = bool.Parse(context.Request.Form["mutil_select"]);
        string ad_user_id = Security.id_taikhoan(context);
        if (thuake != null & thuake != "")
        {
            ad_module mod_tk = db.ad_module.Where(s => s.ad_module_id == thuake).Take(1).FirstOrDefault();
            if (header_grid == "" | header_grid == null)
            { header_grid = mod_tk.header_grid; }
            double_click = mod_tk.double_click;
            mutil_select = mod_tk.mutil_select.Value;
        }
        string loaithuake = context.Request.Form["loaithuake"];
        string bien_table = "", ten_table = "";
        try { ten_table = new_from.Split(' ')[0]; bien_table = new_from.Split(' ')[1]; }
        catch { }
        url = "View/Menu/Content/Module/" + ma_module + ".aspx";
        //#sort
        ad_menu mn = db.ad_menu.SingleOrDefault(p => p.ad_menu_id == (id));
        string capmodule = context.Request.Form["capmodule"];

        if (ma_module == "" | ma_module == null)
        {
            ma_module = "MD_0" + capmodule + "_" + DateTime.Now.ToString("ddMMyyyyhhmmssffftt");
        }
        else if (ma_module.Length < 6)
        {
            msg = "false#0Mã module phải bắt đầu bằng: " + "MD_0" + capmodule + "_";
        }
        else if (!ma_module.Substring(0, 6).Contains("MD_0" + capmodule + "_"))
        {
            msg = "false#1Mã menu phải bắt đầu bằng: " + "MD_0" + capmodule + "_";
        }

        if (db.ad_module.Where(p => p.ma_module == (ma_module)).Take(1).FirstOrDefault() != null)
        {
            msg = "false#Mã module này đã tồn tại";
        }
        else //check cap module
        {
            msg = check_ma_modulecha(int.Parse(capmodule), ma_modulecha, db);
        }

        if (msg.Length <= 0)
        {
            ad_module mod = new ad_module
            {
                ad_module_id = id_new,
                ma_menu = mn.ma_menu,
                ad_menu_id = mn.ad_menu_id,
                ma_module = ma_module,
                ten_module = context.Request.Form["ten_module"],
                url = url,
                ma_modulecha = ma_modulecha,
                capmodule = int.Parse(capmodule),
                sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10),
                loai_module = loai_module,
                thuake = thuake,
                loaithuake = string.IsNullOrEmpty(loaithuake) ? "" : loaithuake,
                row_count = row_count,
                soluong_mod = 0,

                header_grid = header_grid,
                mutil_select = mutil_select,
                double_click = double_click,
                select_sql = new_select,
                from_sql = new_from,
                Join_sql = "",
                where_sql = new_where,
                orderby_sql = new_orderby,
                groupby_sql = new_groupby,
                procedure_sql = context.Request.Form["procedure_sql"],

                nguoitao = ad_user_id,
                vaitrotao = Security.id_vaitro(context),
                bophantao = Security.id_phongban(context),
                nguoicapnhat = ad_user_id,
                vaitrocapnhat = Security.id_vaitro(context),
                bophancapnhat = Security.id_phongban(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now,
                mota = context.Request.Form["mota"],
                hoatdong = true
            };
            db.ad_module.Add(mod);
            lstAdModule.Add(mod);
            db.SaveChanges();

            ad_user tk = db.ad_user.Where(p => p.ad_user_id == ad_user_id).Take(1).FirstOrDefault();
            Module_TK mod_ = VNN_Config.get_ModuleKeThua(mod, 0, ma_module, "", "", db);
            Them_ChucNang_PhanQuyen(context, ma_module, id_new, tk, mn, mod_, db, json);

            if (loai_module == "JQG" | loai_module == "JQGS")
            {
                if (VNN_Function.Test_PrimaryKey(ten_table))
                {
                    msg = Admin_CreateBasicFile.CreateFileLoad_Modify(context, mod_, int.Parse(capmodule));
                    string sql = "sELeCt " + mod_.select_sql + " fRoM " + mod_.from_sql + " WHeRe 1=1 " + mod_.where_sql;
                    //End Xet truong hop la module thua ke
                    if (!string.IsNullOrEmpty(mod_.groupby_sql))
                        sql += " GrOuP bY " + mod_.groupby_sql;

                    if (!string.IsNullOrEmpty(mod_.orderby_sql))
                        sql += " OrDEr bY " + mod_.orderby_sql;

                    if (msg.Contains("\n Tạo grid thành công."))
                    {
                        Them_Column(context, mod, mod_, sql, bien_table, db);
                    }
                }
                else
                {
                    msg = "Không thể tạo grid do Table không tồn tại khóa chính.";
                }
            }
            else
            { msg = Admin_CreateBasicFile.CreateFileLoad_Modify(context, mod_, int.Parse(capmodule)); }

            VNN_Function.SortColumn("ad_module", sapxep, "ad_menu_id", id, "ma_module", ma_module, ma_modulecha);

            msg = "true#Tạo mới module thành công. " + msg;

            if (capmodule == "0" & sapxep == "0000000000")
            {
                mn.ma_module_count = mod.ma_module;
                db.SaveChanges();
            }

            string jsonData = JsonConvert.SerializeObject(lstAdModule, Formatting.Indented);
            json.urlData = typeof(ad_module).Name;
            json.WriteJson(jsonData);
            VNN_Function.loaddulieu_Auto(db, ma_module_);
            db.Dispose();
            msg += "#" + id_new;
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        ADmin_JSON json = new ADmin_JSON();
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module_ = context.Request.QueryString["ma_module"];
        bool row_count = bool.Parse(context.Request.Form["row_count"]);
        bool updateColumn = context.Request.Form["updateColumn"] == "1";
        string chuoi_capmodule = context.Request.Form["capmodule"];
        //sort
        string id = context.Request.Form["id_parent"];

        string id_ = context.Request.Form["id"];
        ad_module mod = db.ad_module.Where(p => p.ad_module_id == id_).Take(1).FirstOrDefault();

        ad_menu mn = db.ad_menu.Where(s => s.ad_menu_id == id).Take(1).FirstOrDefault();
        string id_taikhoan = Security.id_taikhoan(context);
        ad_user tk = db.ad_user.Where(p => p.ad_user_id == id_taikhoan).Take(1).FirstOrDefault();
        string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
        string ma_module = mod.ma_module;
        string ma_modulecha = context.Request.Form["ma_modulecha"].removeAllSpaceOrTrimText(false);
        if (ma_modulecha.Length > 32)
            ma_modulecha = ma_modulecha.Split('-')[0];
        //#sort
        //Reset Grid
        string updatetype = context.Request.Form["updatetype"];
        string old_select = mod.select_sql, new_select = context.Request.Form["select_sql"];
        string old_from = mod.from_sql, new_from = context.Request.Form["from_sql"];
        string old_where = mod.where_sql, new_where = context.Request.Form["where_sql"];
        string old_orderby = mod.orderby_sql, new_orderby = context.Request.Form["orderby_sql"];
        string old_groupby = mod.groupby_sql, new_groupby = context.Request.Form["groupby_sql"];
        string loai_module = mod.loai_module;
        string old_thuake = mod.thuake;
        string thuake = context.Request.Form["thuake"];
        string header_grid = context.Request.Form["header_grid"];
        string double_click = context.Request.Form["double_click"].removeAllSpaceOrTrimText(false);
        if (double_click.Length > 32)
            double_click = double_click.Split('-')[0];
        bool mutil_select = bool.Parse(context.Request.Form["mutil_select"]);
        bool hoatdong = bool.Parse(context.Request.Form["hoatdong"]);
        if (thuake != null & thuake != "")
        {
            ad_module mod_tk = db.ad_module.Where(s => s.ad_module_id == thuake).Take(1).FirstOrDefault();
            if (header_grid == "")
                header_grid = mod_tk.header_grid;

            double_click = mod_tk.double_click;
            mutil_select = mod_tk.mutil_select.Value;
        }
        string bien_table = "", ten_table = "";
        try { ten_table = new_from.Split(' ')[0]; bien_table = new_from.Split(' ')[1]; }
        catch { }

        msg = check_ma_modulecha(mod.capmodule.Value, ma_modulecha, db);

        if (mod != null & msg.Length <= 0)
        {
            if (mod.mutil_select != null)
            {
                if (mod.mutil_select.Value != mutil_select)
                {
                    updatetype = "update0";
                }
            }

            mod.ten_module = context.Request.Form["ten_module"];
            mod.url = context.Request.Form["url"];
            mod.ma_modulecha = ma_modulecha;
            mod.capmodule = int.Parse(chuoi_capmodule);
            mod.sapxep = sapxep;
            mod.header_grid = header_grid;
            mod.mutil_select = mutil_select;
            mod.double_click = double_click;
            mod.select_sql = new_select;
            mod.from_sql = new_from;
            mod.Join_sql = "";
            mod.where_sql = new_where;
            mod.orderby_sql = new_orderby;
            mod.groupby_sql = new_groupby;
            mod.procedure_sql = context.Request.Form["procedure_sql"];
            mod.row_count = row_count;
            if (thuake != null & thuake != "")
                mod.thuake = thuake;
            mod.nguoicapnhat = Security.id_taikhoan(context);
            mod.vaitrocapnhat = Security.id_vaitro(context);
            mod.bophancapnhat = Security.id_phongban(context);
            mod.ngaycapnhat = DateTime.Now;
            mod.mota = context.Request.Form["mota"];
            mod.hoatdong = hoatdong;

            string sql = "sELeCt " + new_select + " fRoM " + new_from + " WHeRe 1=1 " + new_where;

            if (!string.IsNullOrEmpty(new_groupby))
                sql += " GrOuP bY " + new_groupby;

            if (!string.IsNullOrEmpty(new_orderby))
                sql += " OrDEr bY " + new_orderby;

            Module_TK mod_ = VNN_Config.get_ModuleKeThua(mod, 0, ma_module, "", "", db);
            if (thuake != "" & thuake != null)
            {
                new_select = mod_.select_sql;
                sql = "sELeCt " + mod_.select_sql + " fRoM " + mod_.from_sql + " WHeRe 1=1 " + mod_.where_sql;

                if (!string.IsNullOrEmpty(mod_.groupby_sql))
                    sql += " GrOuP bY " + mod_.groupby_sql;

                if (!string.IsNullOrEmpty(mod_.orderby_sql))
                    sql += " OrDEr bY " + mod_.orderby_sql;
            }

            if (VNN_Function.TestSQL(sql) & (loai_module.Equals("JQG") | loai_module.Equals("JQGS")))
            {
                if (VNN_Function.Test_PrimaryKey(ten_table) == false & thuake == null)
                {
                    msg = "false#Table không tồn tại khóa chính";
                }
                else
                {
                    db.SaveChanges();
                    msg = Admin_CreateBasicFile.CreateFileLoad_Modify(context, mod_, int.Parse(chuoi_capmodule));
                    if (old_thuake != thuake & thuake != null & thuake != "")
                    {
                        Xoa_Case(mod.ad_module_id, db);
                        Them_ChucNang_PhanQuyen(context, ma_module, mod.ad_module_id, tk, mn, mod_, db, json);
                        Them_Column(context, mod, mod_, sql, bien_table, db);
                    }
                    else
                    {
                        new_select = new_select.Replace(" ", "");
                        old_select = old_select.Replace(" ", "");
                        if ((mod.thuake == null | mod.thuake == "") & new_select != old_select)
                        {
                            if (updateColumn)
                                Them_Column(context, mod, mod_, sql, bien_table, db);
                        }
                    }
                }
            }
            else if (loai_module.Equals("TC"))
            {
                msg = Admin_CreateBasicFile.CreateFileLoad_Modify(context, mod_, int.Parse(chuoi_capmodule));
                db.SaveChanges();
            }
            else
            {
                msg = "false#Cú pháp SQL không chính xác.\n" + sql;
            }

            if (!msg.Contains("false#"))
            {
                VNN_Function.SortColumn("ad_module", sapxep, "ad_menu_id", id, "ma_module", ma_module, ma_modulecha);
                msg = "true#Cập nhật thành công.";
                string restart = "";
                if (updatetype == "update0" & (loai_module.Equals("JQG") | loai_module.Equals("JQGS")))
                {
                    int dem = Admin_CreateBasicFile.Update_InterFace(context, mod_, db);
                    restart = "\nCó " + dem + " module thừa kế được cập nhật.";
                    foreach (ad_module mod_tk in db.ad_module.Where(s => s.thuake == mod.ad_module_id).ToList())
                    {
                        ad_case cn_tk = db.ad_case.Where(s => s.thuake == double_click.Replace(mod.ad_module_id, "")
                            & s.ad_module_id == mod_tk.ad_module_id).Take(1).FirstOrDefault();
                        mod_tk.double_click = cn_tk.ad_case_id + cn_tk.ad_module_id;
                        mod_tk.header_grid = header_grid;
                        mod_tk.mutil_select = mutil_select;

                        foreach (ad_column cot_tk in db.ad_column.Where(s => s.ma_module == mod_tk.ma_module).ToList())
                        {
                            ad_column cot = db.ad_column.Where(s => s.ma_module == mod.ma_module & s.ma_column == cot_tk.ma_column).Take(1).FirstOrDefault();
                            cot_tk.@fixed = cot.@fixed;
                            cot_tk.ma_column = cot.@ma_column;
                            cot_tk.ten_column = cot.@ten_column;
                            cot_tk.index_cot = cot.@index_cot;
                            cot_tk.width = cot.@width;
                            cot_tk.key_cot = cot.@key_cot;
                            cot_tk.formatter = cot.@formatter;
                            cot_tk.unformat = cot.@unformat;
                            cot_tk.align = cot.@align;
                            cot_tk.stype = cot.@stype;
                            cot_tk.searchoptions = cot.@searchoptions;
                            cot_tk.formoptions = cot.@formoptions;
                            cot_tk.label = cot.@label;
                            cot_tk.editrules = cot.@editrules;
                            cot_tk.ma_edittype = cot.@ma_edittype;
                            cot_tk.edittype = cot.@edittype;
                            cot_tk.editoptions = cot.@editoptions;
                            cot_tk.important = cot.@important;
                            cot_tk.colspan = cot.@colspan;
                            cot_tk.formatoptions = cot.@formatoptions;
                            cot_tk.reset_modify = cot.@reset_modify;
                            cot_tk.focus = cot.@focus;
                            cot_tk.disable_modify = cot.@disable_modify;
                            cot_tk.sopt = cot.sopt;
                            cot_tk.frozen = cot.frozen;
                            cot_tk.sapxep = cot.@sapxep;
                            cot_tk.nguoitao = cot.@nguoitao;
                            cot_tk.vaitrotao = cot.@vaitrotao;
                            cot_tk.bophantao = cot.@bophantao;
                            cot_tk.nguoicapnhat = cot.@nguoicapnhat;
                            cot_tk.vaitrocapnhat = cot.@vaitrocapnhat;
                            cot_tk.bophancapnhat = cot.@bophancapnhat;
                            cot_tk.ngaytao = cot.@ngaytao;
                            cot_tk.ngaycapnhat = cot.@ngaycapnhat;
                            cot_tk.mota = cot.@mota;
                            cot_tk.hoatdong = cot.hoatdong;
                        }
                    }
                    db.SaveChanges();
                }

                if (chuoi_capmodule == "0" & sapxep == "0000000000")
                {
                    mn.ma_module_count = mod.ma_module;
                    db.SaveChanges();
                }
                msg += restart;
            }


        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        ADmin_JSON json = new ADmin_JSON();
        EntityContext db = new EntityContext();
        List<ad_module> lstAdModule = json.ad_moduleJSON();
        List<ad_case> lstAdCase = json.ad_caseJSON();
        List<ad_column> lstAdColumn = json.ad_columnJSON();
        List<string> idsCaseDel = new List<string>();
        List<string> idsColumnDel = new List<string>();
        string msg = "";
        string ma_module_ = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_module mod = db.ad_module.Where(p => p.ad_module_id == id).FirstOrDefault();
            string ma_module = mod.ma_module;
            string url = mod.url;
            if (mod != null)
            {
                foreach (ad_case cn in db.ad_case.Where(s => s.ad_module_id == mod.ad_module_id).ToList())
                {
                    db.ad_case.Remove(cn);
                    idsCaseDel.Add(cn.ad_case_id);
                }
                lstAdCase = lstAdCase.Where(s => !idsCaseDel.Contains(s.ad_case_id)).ToList();

                foreach (ad_column cot in db.ad_column.Where(s => s.ad_module_id == mod.ad_module_id).ToList())
                {
                    db.ad_column.Remove(cot);
                    idsColumnDel.Add(cot.ad_column_id);
                }
                lstAdColumn = lstAdColumn.Where(s => !idsColumnDel.Contains(s.ad_column_id)).ToList();

                VNN_Function.XoaPhanQuyen(context, mod.ad_module_id, null, json);
                db.ad_module.Remove(mod);
                db.SaveChanges();
                lstAdModule = lstAdModule.Where(s => s.ad_module_id != mod.ad_module_id).ToList();
                Admin_CreateBasicFile.DeleteFileLoad_Modify(context, ma_module, url);
                msg = "true#Xóa module thành công!";

                string jsonData = JsonConvert.SerializeObject(lstAdModule, Formatting.Indented);
                json.urlData = typeof(ad_module).Name;
                json.WriteJson(jsonData);

                jsonData = JsonConvert.SerializeObject(lstAdCase, Formatting.Indented);
                json.urlData = typeof(ad_case).Name;
                json.WriteJson(jsonData);

                jsonData = JsonConvert.SerializeObject(lstAdColumn, Formatting.Indented);
                json.urlData = typeof(ad_column).Name;
                json.WriteJson(jsonData);
            }
            else
            {
                msg = "false#Lỗi: không tìm thấy đối tượng.";
            }
        }
        catch (Exception ex)
        {
            msg = "false#Lỗi: " + ex.Message;
        }
        context.Response.Write(msg);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void loadModule(HttpContext context)
    {
        ADmin_JSON json = new ADmin_JSON();
        var modules = json.ad_moduleJSON();
        var role_mmcs = json.ad_role_mmcJSON();
        var user_mmcs = json.ad_user_mmcJSON();
        string result_module = "";
        string ma_menu = context.Request.QueryString["ma_menu"];
        string url_menu = context.Request.QueryString["url_menu"];
        string ten_menu = context.Request.QueryString["ten_menu"];
        if (ma_menu == null)
            ma_menu = "";
        //menu cha
        string module_lv0 = "";
        string module_lv1 = "";
        string module_lv2 = "";
        string module_spanselect = " module_spanselect";
        string pointer = "cursor: pointer;user-select:none;";

        int dem = 0, dem_lv0 = 0;
        string loai_menu = "";
        foreach (var mod in modules.Where(p => p.ma_menu == ma_menu & p.capmodule == 0 & p.hoatdong == true).OrderBy(p => p.sapxep).ToList())
        {
            dem_lv0++;
            module_lv0 += "<span class='span_modulelv0_first far fa-folder'></span>";
            loai_menu = mod.loai_module;
            if (loai_menu != "JQGS")
            {
                ten_menu = mod.ten_module;
            }
            if (dem_lv0 != 1)
            {
                module_spanselect = "";
            }

            module_lv0 += "<span style='margin: 0 0 0 28px;" + pointer + "'" +
            " id='span_" + mod.ma_module + "' class='span_" + mod.ma_module + module_spanselect + "' " +
            " onclick=\"loadModule('" + mod.ma_module + "','" + mod.url + "','" + mod.capmodule + "','" + mod.ten_module + "')\">" + ten_menu + "</span>";
            module_lv0 += "<input id='input_tenmodule0' value='" + mod.ten_module + "' type='hidden' /> ";

            //module_lv0 += "&nbsp; &nbsp;";

            module_lv1 += "<span class=''></span><span style=' margin: 0 0 0 20;'></span>";
            if (loai_menu != "JQGS")
            {
                foreach (var mod_lv1 in modules.Where(p => p.capmodule == 1 & p.hoatdong == true
                    & (p.ma_modulecha.Equals(mod.ad_module_id) |
                    (modules.Where(s => s.ad_module_id == mod.thuake & p.ma_modulecha == s.ad_module_id).Take(1).Count() > 0)
                    )).OrderBy(p => p.sapxep).ToList())
                {
                    if (Security.PhanQuyen_Module(context, role_mmcs, user_mmcs, mod_lv1.ad_module_id))
                    {
                        dem++;
                        module_lv1 += "<span style='margin: -2px 0 0 -8px;' class='span_modulelv0_first fas fa-angle-double-right'></span>";
                        module_lv1 += "<span style='font-size: 10.5px !important'" +
                            " id='span_" + mod_lv1.ma_module + "' class='span_" + mod_lv1.ma_module + " span_unselected chuanhan danhan_" + mod.ma_module + "' " +
                            "onclick=\"loadModule('" + mod_lv1.ma_module + "','" + mod_lv1.url + "','" + mod_lv1.capmodule + "','" + mod_lv1.ten_module + "')\">" + mod_lv1.ten_module + "</span>";

                        module_lv1 += "<span class='cl_span_chiatach chuanhan danhan_" + mod.ma_module + "'></span>";

                        int i_module2 = 0;
                        foreach (var mod_lv2 in modules.Where(p => p.capmodule == 2 & p.hoatdong == true
                            & (p.ma_modulecha.Equals(mod_lv1.ad_module_id) |
                            (modules.Where(s => s.ad_module_id == mod_lv1.thuake & p.ma_modulecha == s.ad_module_id).Take(1).Count() > 0)
                            )).OrderBy(p => p.sapxep).ToList())
                        {
                            if (Security.PhanQuyen_Module(context, role_mmcs, user_mmcs, mod_lv2.ad_module_id))
                            {
                                if (i_module2 == 0)
                                {
                                    module_lv2 += "<span class='danhan_" + mod_lv1.ma_module + "'></span><span style=' margin: 0 0 0 33;' class='danhan_" + mod_lv1.ma_module + "'></span>";
                                    i_module2 = 1;
                                }
                                module_lv2 += "<span style='margin: -2px 0 0 -3px;' class='span_modulelv0_first fas fa-angle-right'></span>";
                                module_lv2 += "<span style='font-size: 10.5px !important' " +
                                " id='span_" + mod_lv2.ma_module + "' class='span_" + mod_lv2.ma_module + " span_unselected chuanhan danhan_" + mod_lv1.ma_module + "' " +
                                " onclick=\"loadModule('" + mod_lv2.ma_module + "','" + mod_lv2.url + "','" + mod_lv2.capmodule + "','" + mod_lv2.ten_module + "')\">" + mod_lv2.ten_module + "</span>";

                                module_lv2 += "<span class='cl_span_chiatach chuanhan danhan_" + mod_lv1.ma_module + "'></span>";
                            }
                        }
                    }
                }
            }
        }

        result_module += "<div class='div_module_background'>";
        result_module += "</div>";

        result_module += "<div class='div_modulelv0'>";
        result_module += module_lv0;
        result_module += "</div>";

        result_module += "<div class='div_modulelv1'>";
        result_module += "<div class='border_div_modulelv1'>";
        result_module += module_lv1;
        result_module += "</div>";
        result_module += "</div>";

        result_module += "<div class='div_modulelv2'>";
        result_module += "<div class='border_div_modulelv1'>";
        result_module += module_lv2;
        result_module += "</div>";
        result_module += "</div>";
        context.Response.Write(result_module);
    }

    public void load_ad_modulecha(HttpContext context)
    {
        EntityContext db = new EntityContext();
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);


        string str = "";
        str += "<select>";
        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        foreach (var mod in db.ad_module.Where(p => p.capmodule < 2).OrderBy(p => p.ten_module).ToList())
        {
            str += string.Format("<option value=\"{0}\">{1}</option>", mod.ma_module, "[Cấp:" + mod.capmodule + "] - " + mod.ma_module);
        }
        str += "</select>";
        context.Response.Write(str);
    }

    public void LoadList(HttpContext context)
    {
        string ad_menu_id = context.Request.QueryString["ad_menu_id"];
        ADmin_JSON json = new ADmin_JSON();
        var modules = json.ad_moduleJSON();
        var data = "";
        foreach (var mod in modules.Where(s => s.ad_menu_id == ad_menu_id).OrderBy(p => p.capmodule).ThenBy(s => s.sapxep))
        {
            data += "<a style='display:none'>" + mod.ad_module_id + "</a><a>" + mod.ten_module + "</a>ξ";
        }
        try { data = data.Remove(data.Length - 1); }
        catch { data = ""; }
        context.Response.Write(data);
    }

    //ky tu dac biet
    //(##) => ,
    public void Them_Column(HttpContext context, ad_module mod, Module_TK mod_, string sql, string bien_table, EntityContext db)
    {
        ADmin_JSON json = new ADmin_JSON();
        List<ad_column> LstAdColumn = json.ad_columnJSON();
        List<ad_module> lstAdModule = json.ad_moduleJSON();
        if (!string.IsNullOrEmpty(mod.thuake))
        {
            foreach (ad_column col_ in LstAdColumn.Where(p => p.ad_module_id == mod_.ad_module_id).ToList())
            {
                ad_column coltk_ = new ad_column
                {
                    ad_column_id = Helper.getNewId(),
                    ma_menu = mod.@ma_menu,
                    ma_module = mod.@ma_module,
                    @fixed = col_.@fixed,
                    ma_column = col_.@ma_column,
                    ten_column = col_.@ten_column,
                    index_cot = col_.@index_cot,
                    ad_module_id = mod.ad_module_id,
                    width = col_.@width,
                    key_cot = col_.@key_cot,
                    hidden = col_.@hidden,
                    formatter = col_.@formatter,
                    unformat = col_.@unformat,
                    align = col_.@align,
                    stype = col_.@stype,
                    searchoptions = col_.@searchoptions,
                    formoptions = col_.@formoptions,
                    label = col_.@label,
                    editable = col_.@editable,
                    editrules = col_.@editrules,
                    ma_edittype = col_.@ma_edittype,
                    edittype = col_.@edittype,
                    editoptions = col_.@editoptions,
                    important = col_.@important,
                    sapxep = col_.@sapxep,
                    colspan = col_.@colspan,
                    formatoptions = col_.@formatoptions,
                    sopt = col_.@sopt,
                    disable_modify = col_.@disable_modify,
                    focus = col_.@focus,
                    reset_modify = col_.@reset_modify,

                    nguoitao = col_.@nguoitao,
                    vaitrotao = col_.@vaitrotao,
                    bophantao = col_.@bophantao,
                    nguoicapnhat = col_.@nguoicapnhat,
                    vaitrocapnhat = col_.@vaitrocapnhat,
                    bophancapnhat = col_.@bophancapnhat,
                    ngaytao = col_.@ngaytao,
                    ngaycapnhat = col_.@ngaycapnhat,
                    mota = col_.@mota,
                    hoatdong = col_.hoatdong
                };
                db.ad_column.Add(coltk_);
                LstAdColumn.Add(coltk_);
            }
            db.SaveChanges();
        }
        else
        {
            int jj = mod.from_sql.Split(' ')[0].Length;
            string md_object = mod.from_sql.Substring(0, jj);
            System.Data.DataTable dt_column = Mbg.Data.SqlClient.SqlHelper.GetData(sql, "@start", 0, "@end", 10000);
            //Xoa cac cot khong nam trong sql
            List<string> idsColumn = new List<string>();
            foreach (ad_column col_ in LstAdColumn.Where(p => p.ad_module_id == mod.ad_module_id).ToList())
            {
                bool co = false;
                foreach (System.Data.DataColumn row_column in dt_column.Columns)
                {
                    if (col_.ma_column == row_column.ColumnName)
                    {
                        co = true;
                    }
                }

                if (co == false)
                {
                    if (col_ != null)
                    {
                        var col_db = db.ad_column.Where(s => s.ad_column_id == col_.ad_column_id).FirstOrDefault();
                        if (col_db != null)
                            db.ad_column.Remove(col_db);
                    }
                    idsColumn.Add(col_.ad_column_id);
                }
            }
            db.SaveChanges();
            LstAdColumn = LstAdColumn.Where(s => !idsColumn.Contains(s.ad_column_id)).ToList();

            //Kiem tra neu cot nam trong sql thi khong can them
            int i = 0;
            int j = sql.LastIndexOf("fRoM");
            string select = sql.Substring(0, j).Remove(0, 6);
            string[] select_array = null;
            try
            {
                select = select.Replace("/**/,/**/", "(##)");
                select_array = new string[select.Split(',').Count()];
                select_array = select.Split(',');
            }
            catch
            {

            }

            foreach (System.Data.DataColumn row_column in dt_column.Columns)
            {
                string index_cot = row_column.ColumnName;
                string mota = index_cot;
                if (select_array != null)
                {
                    if (select_array[i].LastIndexOf(" as ") > -1)
                    {
                        //Xet TH la cau sql don gian:select mn.ten_role as tenvaitro from ad_vaitro tk
                        int j_index = select_array[i].LastIndexOf(" as ");
                        index_cot = select_array[i].Substring(0, j_index);
                        mota = index_cot;
                        //Xet TH la cau sql trung nhau:
                        //select (select vtr.ten_role from ad_role vtr where vtr.ad_role_id = tk.ad_role_id) as nhan 
                        //from ad_user tk
                        if (index_cot.LastIndexOf("((") > -1 & index_cot.LastIndexOf("))") > -1)
                        {
                            mota = index_cot;
                            int j_index_mota = index_cot.IndexOf("(("), j_index_mota2 = index_cot.IndexOf("))");
                            index_cot = index_cot.Substring((j_index_mota + 2), (j_index_mota2 - j_index_mota - 2));

                        }
                    }
                    else
                    {
                        if (select_array[i].Contains("."))
                        {
                            index_cot = select_array[i];
                            mota = index_cot;
                        }
                    }
                }
                ad_column col_find = null;
                try
                {
                    col_find = db.ad_column.Where(p => p.ad_module_id == (mod.ad_module_id) & p.ma_column == (row_column.ColumnName)).FirstOrDefault();
                }
                catch { }

                string[] name_default = new string[100];
                string[] format_column = new string[100];
                index_cot = index_cot.Replace("\n", "").Replace("(##)", "/**/,/**/");
                mota = mota.Replace("\n", "").Replace("(##)", "/**/,/**/");
                if (col_find == null)
                {
                    name_default = VNN_Validate.check_NameColumn_default(row_column.ColumnName, index_cot, mota, bien_table);
                    format_column = VNN_Validate.check_FormatColumn(row_column.ColumnName, md_object, mod.ma_module);
                    col_find = new ad_column();
                    col_find.ad_column_id = Helper.getNewId();
                    col_find.ad_module_id = mod.ad_module_id;
                    col_find.ma_module = mod.ma_module;
                    col_find.ma_menu = mod.ma_menu;
                    col_find.colspan = "";
                    col_find.@fixed = "true";
                    col_find.sapxep = VNN_Config.load_number(i.ToString(), 10);
                    col_find.important = "false";
                    col_find.ma_column = row_column.ColumnName;
                    col_find.ten_column = name_default[0];
                    col_find.label = name_default[0];
                    col_find.mota = name_default[3];
                    col_find.index_cot = name_default[4];
                    col_find.width = name_default[5];
                    col_find.align = name_default[6];
                    col_find.reset_modify = true;
                    col_find.focus = false;
                    col_find.editrules = "1";
                    //--
                    col_find.ma_edittype = format_column[0];
                    col_find.edittype = format_column[1];
                    col_find.formatter = format_column[2];
                    col_find.formatoptions = format_column[3];
                    col_find.editoptions = format_column[4];
                    col_find.searchoptions = format_column[5];
                    col_find.stype = format_column[6];
                    col_find.sopt = format_column[7];
                    //--
                    col_find.nguoitao = Security.id_taikhoan(context);
                    col_find.vaitrotao = Security.id_vaitro(context);
                    col_find.bophantao = Security.id_phongban(context);
                    col_find.nguoicapnhat = Security.id_taikhoan(context);
                    col_find.vaitrocapnhat = Security.id_vaitro(context);
                    col_find.bophancapnhat = Security.id_phongban(context);
                    col_find.ngaytao = DateTime.Now;
                    col_find.ngaycapnhat = DateTime.Now;
                    col_find.hoatdong = true;
                    if (i == 0)
                    {
                        col_find.key_cot = "true";
                        col_find.hidden = "true";
                        col_find.editable = "false";
                    }
                    else
                    {
                        col_find.key_cot = "false";
                        col_find.hidden = name_default[1];
                        col_find.editable = name_default[2];
                    }
                    db.ad_column.Add(col_find);
                    LstAdColumn.Add(col_find);
                }
                else
                {
                    if (col_find.index_cot == null | col_find.index_cot == "")
                        col_find.index_cot = index_cot;
                    col_find.mota = mota;
                    col_find.nguoicapnhat = Security.id_taikhoan(context);
                    col_find.vaitrocapnhat = Security.id_vaitro(context);
                    col_find.bophancapnhat = Security.id_phongban(context);
                    col_find.ngaycapnhat = DateTime.Now;
                    col_find.sapxep = VNN_Config.load_number(i.ToString(), 10);
                    if (i == 0)
                    {
                        col_find.key_cot = "true";
                        col_find.hidden = "true";
                        col_find.editable = "false";
                    }
                    else
                    {
                        col_find.key_cot = "false";
                    }

                    LstAdColumn = LstAdColumn.Where(s => s.ad_column_id != col_find.ad_column_id).ToList();
                    LstAdColumn.Add(col_find);
                }
                i++;
                db.SaveChanges();
            }
        }

        string select_sql = VNN_Config.Select_sql(mod.ma_module, db);
        select_sql = select_sql.Remove(select_sql.Length - 1);
        mod.select_sql = select_sql;
        db.SaveChanges();

        string jsonData = JsonConvert.SerializeObject(LstAdColumn, Formatting.Indented);
        json.urlData = typeof(ad_column).Name;
        json.WriteJson(jsonData);

        lstAdModule = lstAdModule.Where(s => s.ad_module_id != mod.ad_module_id).ToList();
        lstAdModule.Add(mod);
        jsonData = JsonConvert.SerializeObject(lstAdModule, Formatting.Indented);
        json.urlData = typeof(ad_module).Name;
        json.WriteJson(jsonData);
    }

    public void Them_ChucNang_PhanQuyen(HttpContext context, string ma_module, string ad_module_id, ad_user tk, ad_menu mn, Module_TK mod_, EntityContext db, ADmin_JSON json)
    {
        var lstAdCase = json.ad_caseJSON();
        if (mod_.ma_moduletk != mod_.ma_module)
        {
            var count = 0;
            foreach (ad_case cn in db.ad_case.Where(s => s.ad_module_id == mod_.ad_module_id).ToList())
            {
                count++;
                string ad_case_id = Helper.getNewId();
                ad_case cntk_ = new ad_case
                {
                    ad_case_id = ad_case_id,
                    ad_module_id = ad_module_id,
                    ma_case = "CA_01_" + DateTime.Now.AddSeconds(count).ToString("ddMMyyyyhhmmssffftt"),
                    ma_module = ma_module,
                    ma_menu = mn.ma_menu,
                    thuake = cn.@ad_case_id,
                    sapxep = cn.@sapxep,
                    hidden_modify = cn.@hidden_modify,

                    ten_case = cn.@ten_case,
                    hamxuly = cn.@hamxuly,
                    logo = cn.@logo,
                    id_parent = cn.@id_parent,
                    isview = cn.@isview,
                    dodaiForm = cn.@dodaiForm,
                    docaoForm = cn.@docaoForm,
                    canhgiua = cn.@canhgiua,
                    tieude = cn.@tieude,
                    nguoitao = Security.id_taikhoan(context),
                    vaitrotao = Security.id_vaitro(context),
                    bophantao = Security.id_phongban(context),
                    nguoicapnhat = Security.id_taikhoan(context),
                    vaitrocapnhat = Security.id_vaitro(context),
                    bophancapnhat = Security.id_phongban(context),
                    ngaytao = DateTime.Now,
                    ngaycapnhat = DateTime.Now,
                    hoatdong = cn.@hoatdong
                };
                db.ad_case.Add(cntk_);
                lstAdCase.Add(cntk_);
            }

            string ad_case_id_ = Helper.getNewId();
            ad_case cn_ = new ad_case
            {
                ad_case_id = ad_case_id_,
                ad_module_id = ad_module_id,
                ma_case = "CA_01_" + DateTime.Now.AddSeconds(count).ToString("ddMMyyyyhhmmssffftt"),
                ma_module = ma_module,
                ma_menu = mn.ma_menu,
                thuake = "",
                sapxep = VNN_Config.load_number("1000", 10),
                hoatdong = true,
                hidden_modify = false,
                ten_case = "Hiển thị",
                hamxuly = "",
                logo = "",
                id_parent = true,
                isview = false,
                dodaiForm = "500",
                docaoForm = "",
                canhgiua = true,
                tieude = "",
                nguoitao = Security.id_taikhoan(context),
                vaitrotao = Security.id_vaitro(context),
                bophantao = Security.id_phongban(context),
                nguoicapnhat = Security.id_taikhoan(context),
                vaitrocapnhat = Security.id_vaitro(context),
                bophancapnhat = Security.id_phongban(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };
            db.ad_case.Add(cn_);
            lstAdCase.Add(cn_);
            db.SaveChanges();
            VNN_Function.ThemPhanQuyen(context, Security.id_vaitro(context), mn.ad_menu_id, ad_module_id, ad_case_id_);
        }
        else
        {
            VNN_Function.ThemChucNang(context, mn.ma_menu, ma_module, ad_module_id);
            VNN_Function.ThemPhanQuyen(context, Security.id_vaitro(context), mn.ad_menu_id, ad_module_id, null);
        }
    }

    public void CA_01_CopyModule(HttpContext context)
    {
        string msg = "";
        var db = new EntityContext();
        string id = context.Request.Form["id"];
        string id_copy = context.Request.Form["id_copy"];
        string ma_case_org = "", ma_case_copy = "", hamxuly_org = "", hamxuly_copy = "";
        ad_module mod_sel = db.ad_module.Where(s => s.ad_module_id == id).Take(1).FirstOrDefault();
        ad_module mod_copy = db.ad_module.Where(s => s.ad_module_id == id_copy).Take(1).FirstOrDefault();
        User_TK us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context));
        var json = new ADmin_JSON();
        var lstAdRoleMMC = json.ad_role_mmcJSON();
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                if (mod_copy == null)
                {
                    msg = "Không tồn tại module muốn copy";
                }
                else if (mod_sel == null)
                {
                    msg = "Không tồn tại module đã chọn";
                }
                else if (mod_copy.thuake != null & mod_copy.thuake != "")
                {
                    msg = "Module muốn copy không thể là module thừa kế.";
                }
                else if (mod_sel.thuake != null & mod_sel.thuake != "")
                {
                    msg = "Module bạn chọn đang thừa kế 1 module khác.";
                }

                if (msg.Length <= 0)
                {
                    mod_sel.ten_module = mod_copy.ten_module;
                    mod_sel.mutil_select = mod_copy.mutil_select;
                    mod_sel.double_click = mod_copy.double_click;
                    mod_sel.thuake = mod_copy.thuake;
                    mod_sel.header_grid = mod_copy.header_grid;
                    mod_sel.select_sql = mod_copy.select_sql;
                    mod_sel.from_sql = mod_copy.from_sql;
                    mod_sel.where_sql = mod_copy.where_sql;
                    mod_sel.groupby_sql = mod_copy.groupby_sql;
                    mod_sel.orderby_sql = mod_copy.orderby_sql;
                    mod_sel.row_count = mod_copy.row_count;
                    mod_sel.procedure_sql = mod_copy.procedure_sql;

                    Module_TK mod_ = VNN_Config.get_ModuleKeThua(mod_sel, 0, mod_sel.ma_module, "", "", db);
                    Admin_CreateBasicFile.CreateFileLoad_Modify(context, mod_, mod_sel.capmodule.Value);
                    db.ad_column.RemoveRange(db.ad_column.Where(s => s.ad_module_id == mod_sel.ad_module_id));
                    db.SaveChanges();

                    foreach (ad_column col in db.ad_column.Where(s => s.ad_module_id == mod_copy.ad_module_id).ToList())
                    {
                        ad_column col_cp = new ad_column
                        {
                            ad_column_id = Helper.getNewId(),
                            ma_menu = mod_sel.ma_menu,
                            ma_module = mod_sel.ma_module,
                            @fixed = col.@fixed,
                            ma_column = col.ma_column,
                            ten_column = col.ten_column,
                            index_cot = col.index_cot,
                            ad_module_id = mod_sel.ad_module_id,
                            width = col.width,
                            key_cot = col.key_cot,
                            hidden = col.hidden,
                            formatter = col.formatter,
                            unformat = col.unformat,
                            align = col.align,
                            stype = col.stype,
                            searchoptions = col.searchoptions,
                            formoptions = col.formoptions,
                            label = col.label,
                            editable = col.editable,
                            editrules = col.editrules,
                            ma_edittype = col.ma_edittype,
                            edittype = col.edittype,
                            editoptions = col.editoptions,
                            important = col.important,
                            sapxep = col.sapxep,
                            colspan = col.colspan,
                            formatoptions = col.formatoptions,
                            reset_modify = col.reset_modify,
                            focus = col.focus,
                            disable_modify = col.disable_modify,
                            sopt = col.sopt,
                            frozen = col.frozen,
                            nguoitao = us.ad_user_id,
                            vaitrotao = us.ad_role_id,
                            bophantao = us.md_phongban_id,
                            nguoicapnhat = us.ad_user_id,
                            vaitrocapnhat = us.ad_role_id,
                            bophancapnhat = us.md_phongban_id,
                            ngaytao = DateTime.Now,
                            ngaycapnhat = DateTime.Now,
                            mota = col.mota,
                            hoatdong = true
                        };
                        db.ad_column.Add(col_cp);
                    }

                    db.ad_case.RemoveRange(db.ad_case.Where(s => s.ad_module_id == mod_sel.ad_module_id));
                    db.ad_role_mmc.RemoveRange(db.ad_role_mmc.Where(s => s.ad_module_id == mod_sel.ad_module_id));
                    db.SaveChanges();
                    foreach (ad_case cn in db.ad_case.Where(s => s.ad_module_id == mod_copy.ad_module_id).ToList())
                    {
                        string hamxuly = cn.hamxuly, ma_case = cn.ma_case.Replace("_", "") + "_" + mod_sel.ma_module.Replace("_", "");
                        int last_ngoactrong = hamxuly.LastIndexOf("(");
                        if (hamxuly != "click_add(tengrid)" & hamxuly != "click_edit(tengrid)" &
                            hamxuly != "click_del(tengrid)" & hamxuly != "click_view(tengrid)")
                        {
                            hamxuly = hamxuly.Substring(0, last_ngoactrong).Replace("_", "") + "_" + mod_sel.ma_module.Replace("_", "") + hamxuly.Substring(last_ngoactrong);
                            ma_case_org += cn.ma_case + "ξ";
                            ma_case_copy += ma_case + "ξ";
                            hamxuly_org += cn.hamxuly + "ξ";
                            hamxuly_copy += hamxuly + "ξ";
                        }

                        ad_case cn_cp = new ad_case
                        {
                            ad_case_id = Helper.getNewId(),
                            ad_module_id = mod_sel.ad_module_id,
                            ma_case = ma_case,
                            ma_menu = mod_sel.ma_menu,
                            ma_module = mod_sel.ma_module,
                            thuake = cn.thuake,
                            sapxep = cn.sapxep,
                            ten_case = cn.ten_case,
                            hamxuly = hamxuly,
                            logo = cn.logo,
                            isview = cn.isview,
                            id_parent = cn.id_parent,
                            dodaiForm = cn.dodaiForm,
                            docaoForm = cn.docaoForm,
                            canhgiua = cn.canhgiua,
                            tieude = cn.tieude,
                            hidden_modify = cn.hidden_modify,
                            nguoitao = us.ad_user_id,
                            vaitrotao = us.ad_role_id,
                            bophantao = us.md_phongban_id,
                            nguoicapnhat = us.ad_user_id,
                            vaitrocapnhat = us.ad_role_id,
                            bophancapnhat = us.md_phongban_id,
                            ngaytao = DateTime.Now,
                            ngaycapnhat = DateTime.Now,
                            mota = cn.mota,
                            hoatdong = cn.hoatdong,
                        };
                        db.ad_case.Add(cn_cp);
                        db.SaveChanges();

                        foreach (ad_role_mmc pq in db.ad_role_mmc.Where(s => s.ad_module_id == mod_copy.ad_module_id & s.ad_menu_id == mod_copy.ad_menu_id & s.ad_case_id == cn.ad_case_id).ToList())
                        {
                            ad_role_mmc pq_cp = new ad_role_mmc
                            {
                                ad_role_mmc_id = Helper.getNewId(),
                                ad_menu_id = mod_sel.ad_menu_id,
                                ad_module_id = mod_sel.ad_module_id,
                                ad_case_id = cn_cp.ad_case_id,
                                ten_case = cn_cp.ten_case,
                                ad_role_id = pq.ad_role_id,
                                mota = null,

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
                                hoatdong = true
                            };
                            db.ad_role_mmc.Add(pq_cp);
                            lstAdRoleMMC.Add(pq_cp);
                        }
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                    VNN_Function.Copyfile_module(context, mod_copy.ma_module, mod_sel.ma_module, ma_case_org, ma_case_copy, hamxuly_org, hamxuly_copy);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                transaction.Commit();
                string jsonData = JsonConvert.SerializeObject(lstAdRoleMMC, Formatting.Indented);
                json.urlData = typeof(ad_role_mmc).Name;
                json.WriteJson(jsonData);
            }
            else
            {
                transaction.Rollback();
                msg = string.Format(@"<div style=""color:red"">{0}</div>", msg);
            }
        }
        context.Response.Write(msg);
    }

    public string check_ma_modulecha(int capmodule, string ma_modulecha, EntityContext db)
    {
        string kq = "";
        if (ma_modulecha == null) ma_modulecha = "";
        string capmdcha = db.ad_module.Where(s => s.ad_module_id == ma_modulecha).Select(s => s.capmodule).Take(1).FirstOrDefault().ToString();
        int capmdcha_ = -1;
        if (VNN_Validate.check_number(capmdcha, "int"))
            capmdcha_ = int.Parse(capmdcha);

        if (capmdcha_ == 0)
        {
            switch (capmodule)
            {
                case 0: kq = "false#Module cha không thể cùng cấp với module con"; break;
                case 2: kq = "false#Module cha của module cấp 2 phải là module cấp 1"; break;
            }
        }
        else if (capmdcha_ == 1)
        {
            switch (capmodule)
            {
                case 0: kq = "false#Module cha không thể dưới cấp module con"; break;
                case 1: kq = "false#Module cha không thể cùng cấp với module con"; break;
            }
        }
        return kq;
    }

    public void selectoption(HttpContext context)
    {
        EntityContext db = new EntityContext();
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        string sql = @"SELECT name FROM sys.all_objects WHERE (type = 'P') and is_ms_shipped=0  and ( name like '%admin_excute%' or name like '%user_%')";
        System.Data.DataTable dt_sql = Mbg.Data.SqlClient.SqlHelper.GetData(sql);

        string str = "";
        str += "<select>";
        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        for (int i = 0; i < dt_sql.Rows.Count; i++)
        {
            str += string.Format("<option value=\"{0}\">{1}</option>", dt_sql.Rows[i][0].ToString(), dt_sql.Rows[i][0].ToString());
        }
        str += "</select>";
        context.Response.Write(str);
    }

    public void selectoption_double_click(HttpContext context)
    {
        EntityContext db = new EntityContext();
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        string str = "";
        str += "<select>";

        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        foreach (var cn in db.ad_case.Where(s => s.hamxuly != null & s.hamxuly != "").ToList())
        {
            str += string.Format("<option value=\"{0}\">{1}</option>", cn.ad_case_id + cn.ad_module_id, cn.ten_case);
        }
        str += "</select>";
        context.Response.Write(str);
    }

    public void selectoption_module(HttpContext context)
    {
        EntityContext db = new EntityContext();
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        string str = "";
        string id = context.Request.QueryString["id"];
        str += firstnull ? string.Format("<option value=\"\"></option>") : "";
        foreach (var object_ in db.ad_module.Where(s => s.ad_module_id != id).OrderBy(s => s.ten_module).ToList())
        {
            str += string.Format("<option value=\"{0}\">{1}</option>", object_.ad_module_id, object_.ten_module + " - [ " + object_.ma_module + "]");
        }
        context.Response.Write(str);
    }

    public void Xoa_Case(string ad_module_id, EntityContext db)
    {
        ADmin_JSON json = new ADmin_JSON();
        List<ad_role_mmc> lstAdRoleMMC = new List<ad_role_mmc>();
        List<ad_case> lstAdCase = new List<ad_case>();
        List<string> idsAdRoleMMCDel = new List<string>();
        List<string> idsAdCaseDel = new List<string>();
        foreach (ad_case cn in db.ad_case.Where(s => s.ad_module_id.Equals(ad_module_id)).ToList())
        {
            foreach (ad_role_mmc pq in db.ad_role_mmc.Where(s => s.ad_module_id.Equals(ad_module_id) & s.ad_case_id.Equals(cn.ad_case_id)).ToList())
            {
                db.ad_role_mmc.Remove(pq);
                idsAdRoleMMCDel.Add(pq.ad_role_mmc_id);
            }
            db.SaveChanges();
            db.ad_case.Remove(cn);
            idsAdCaseDel.Add(cn.ad_case_id);
        }
        lstAdRoleMMC = lstAdRoleMMC.Where(s => !idsAdRoleMMCDel.Contains(s.ad_role_mmc_id)).ToList();
        lstAdCase = lstAdCase.Where(s => !idsAdCaseDel.Contains(s.ad_case_id)).ToList();
        db.SaveChanges();

        string jsonData = JsonConvert.SerializeObject(lstAdRoleMMC, Formatting.Indented);
        json.urlData = typeof(ad_role_mmc).Name;
        json.WriteJson(jsonData);

        jsonData = JsonConvert.SerializeObject(lstAdCase, Formatting.Indented);
        json.urlData = typeof(ad_case).Name;
        json.WriteJson(jsonData);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}