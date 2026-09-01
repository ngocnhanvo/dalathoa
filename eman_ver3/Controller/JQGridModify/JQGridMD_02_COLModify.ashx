
<%@ WebHandler Language="C#" Class="JQGridMD_02_COLModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;

public class JQGridMD_02_COLModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private class ColumnRole
    {
        public string text { get; set; }
        public bool? selEdit { get; set; }
        public bool? selView { get; set; }
        public int? index { get; set; }
    }

    private class ColumnSort
    {
        public string maCot { get; set; }
        public int? colpos { get; set; }
        public int? rowpos { get; set; }
        public int? colspan { get; set; }
    }

    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    User_TK userTK = null;

    public string oper = "vnn";

    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
            userTK = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context), db);
        }

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
            case "sel_edittype":
                this.sel_edittype(context);
                break;
            case "loadlist":
                this.LoadList(context);
                break;
            case "loadFields":
                this.loadFields(context);
                break;
            case "CA_01_SapXepTruongDuLieu":
                this.CA_01_SapXepTruongDuLieu(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_SapXepTruongDuLieu(HttpContext context)
    {
        var id = context.Request.Form["id"];
        var data = context.Request.Form["data"];
        string msg = "";
        try
        {
            var json = new ADmin_JSON();
            var lstAdColumn = json.ad_columnJSON();

            var jsonData = JsonConvert.DeserializeObject<List<ColumnSort>>(data);
            foreach (var item in jsonData)
            {
                var formoptions = string.Format(@"'',colpos:{0},rowpos:{1}", item.colpos, item.rowpos);
                var column = db.ad_column.Where(s => s.ad_module_id == id & s.ma_column == item.maCot).FirstOrDefault();
                if (column != null)
                {
                    column.formoptions = formoptions;

                    if (item.colspan != null)
                        column.colspan = item.colspan.ToString();
                    else
                        column.colspan = " ";

                    lstAdColumn = lstAdColumn.Where(s => s.ad_column_id != column.ad_column_id).ToList();
                    lstAdColumn.Add(column);
                }
            }
            db.SaveChanges();


            string jsonStr = JsonConvert.SerializeObject(lstAdColumn, Formatting.Indented);
            json.urlData = typeof(ad_column).Name;
            json.WriteJson(jsonStr);
        }
        catch(Exception ex)
        {
            msg = ex.Message;
        }

        context.Response.Write(msg);
    }

    public void loadFields(HttpContext context)
    {
        var id = context.Request.QueryString["id"];
        var json = db.ad_column.Where(s => s.ad_module_id == id & s.editable == "true").OrderBy(s=>s.sapxep).ToList();
        context.Response.Write(JsonConvert.SerializeObject(json));
    }

    public void LoadList(HttpContext context)
    {
        var json = new ADmin_JSON();
        var columns = json.ad_columnJSON();
        var role_mmcols = json.ad_role_mmcolJSON();
        var dataJS = new List<ColumnRole>();
        dataJS.Add(new ColumnRole() {
            text = "Tất cả",
            selEdit = false,
            selView = false,
            index = 0
        });

        string id = context.Request.QueryString["id"];
        string ad_module_id = context.Request.QueryString["ad_module_id"];

        int i = 1;
        foreach (var cn in columns.Where(s =>
            s.ad_module_id == ad_module_id
            & s.hoatdong == true
            & s.key_cot != "true"
            ).OrderBy(p => p.sapxep))
        {
            var itemCas = new ColumnRole();
            itemCas.text = $@"<a style='display:none'>{cn.ad_column_id}</a><a>{cn.ten_column}</a>";
            itemCas.selEdit = false;
            itemCas.selView = false;
            itemCas.index = i;
            foreach (var pq in role_mmcols.Where(p => p.ad_module_id == cn.ad_module_id & p.ad_role_id == id & p.ad_column_id == cn.ad_column_id).ToList())
            {
                itemCas.selEdit = pq.disableEdit.GetValueOrDefault(false);
                itemCas.selView = pq.disableView.GetValueOrDefault(false);
            }
            dataJS.Add(itemCas);
            i++;
        }
        context.Response.Write(JsonConvert.SerializeObject(dataJS));
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id_parent"];
            ad_module mod = db.ad_module.Where(p => p.ad_module_id == (id)).Take(1).FirstOrDefault();
            //sort
            string ad_module_id = mod.ad_module_id;
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string ma_column = context.Request.Form["ma_column"];
            string index_cot = context.Request.Form["index_cot"];
            bool focus = bool.Parse(context.Request.Form["focus"]);
            bool frozen = bool.Parse(context.Request.Form["frozen"]);
            string mota = context.Request.Form["mota"];
            string sopt = context.Request.Form["sopt"];
            string edittype = context.Request.Form["edittype"];
            var edit_type = db.ad_editstyle.Where(s => s.ma_editstyle == edittype).Select(s => new { s.ma_editstyle, s.value_editstyle }).FirstOrDefault();
            string select_sql = "select ";
            select_sql += VNN_Config.Select_sql(mod.ma_module, db);
            if (mota != null & mota != "")
            {
                select_sql += mota + " as " + ma_column;
            }
            else
            {
                if (index_cot != null & index_cot != "")
                    select_sql += index_cot + " as " + ma_column;
                else
                    select_sql += ma_column;
            }

            string sql = "";
            sql += select_sql + " from " + mod.from_sql
                + " where 1=1 " + mod.where_sql;

            if (!string.IsNullOrEmpty(mod.groupby_sql))
                sql += " group by " + mod.groupby_sql;

            if (!string.IsNullOrEmpty(mod.orderby_sql))
                sql += " order by " + mod.orderby_sql;


            if (db.ad_column.Where(p => p.ma_column == (ma_column) & p.ad_module_id == (id)).Take(1).FirstOrDefault() != null)
            {
                msg = "false#Mã cột này đã tồn tại trong module " + mod.ma_module;
            }
            else if (mod.thuake != null & mod.thuake != "")
            {
                var mod_tk = db.ad_module.Where(s => s.ad_module_id == (mod.thuake)).Select(s => new { s.ten_module, s.ma_module }).Take(1).FirstOrDefault();
                msg = "false#Không thể tạo mới, Module này đang thừa kế từ module: " + mod_tk.ten_module + " - (" + mod_tk.ma_module + ")";
            }
            else if (VNN_Function.TestSQL(sql) == false)
            {
                msg = "false#Cú pháp SQL không chính xác.\n" + sql;
            }

            if (sopt == null | sopt == "") { sopt = "bw"; }
            if (msg.Length <= 0)
            {
                ad_column cot = new ad_column
                {
                    ad_column_id = id_new,
                    ma_menu = mod.ma_menu,
                    ma_module = mod.ma_module,
                    @fixed = context.Request.Form["fixed"],
                    ma_column = ma_column,
                    ten_column = context.Request.Form["ten_column"],
                    index_cot = context.Request.Form["index_cot"],
                    ad_module_id = id,
                    width = context.Request.Form["width"],
                    key_cot = context.Request.Form["key_cot"],
                    hidden = context.Request.Form["hidden"],
                    formatter = context.Request.Form["formatter"],
                    unformat = context.Request.Form["unformat"],
                    align = context.Request.Form["align"],
                    stype = context.Request.Form["stype"],
                    searchoptions = context.Request.Form["searchoptions"],
                    formoptions = context.Request.Form["formoptions"],
                    label = VNN_VariablePublic.DecodeHTML(context.Request.Form["label"]),
                    editable = context.Request.Form["editable"],
                    editrules = context.Request.Form["editrules"],
                    ma_edittype = edit_type.ma_editstyle,
                    edittype = edit_type.value_editstyle,
                    editoptions = context.Request.Form["editoptions"],
                    important = context.Request.Form["important"],
                    sapxep = sapxep,
                    colspan = context.Request.Form["colspan"],
                    formatoptions = context.Request.Form["formatoptions"],
                    reset_modify = bool.Parse(context.Request.Form["reset_modify"]),
                    focus = focus,
                    disable_modify = context.Request.Form["disable_modify"],
                    sopt = sopt,
                    frozen = frozen,
                    nguoitao = Security.id_taikhoan(context),
                    vaitrotao = Security.id_vaitro(context),
                    bophantao = Security.id_phongban(context),
                    nguoicapnhat = Security.id_taikhoan(context),
                    vaitrocapnhat = Security.id_vaitro(context),
                    bophancapnhat = Security.id_phongban(context),
                    ngaytao = DateTime.Now,
                    ngaycapnhat = DateTime.Now,
                    mota = context.Request.Form["mota"],
                    hoatdong = true
                };
                db.ad_column.Add(cot);
                db.SaveChanges();

                if (focus == true)
                {
                    foreach (ad_column col in db.ad_column.Where(s => s.ma_module == mod.ma_module & s.ma_column != ma_column).ToList())
                    {
                        col.focus = false;
                    }
                    db.SaveChanges();
                }
                VNN_Function.SortColumn("ad_column", sapxep, "ad_module_id", id, "ma_column", ma_column, null);
                msg += "true#Thêm cột thành công" + "#" + id_new;
                System.Threading.Thread.Sleep(500);
                select_sql = VNN_Config.Select_sql(mod.ma_module, db);
                select_sql = select_sql.Remove(select_sql.Length - 1);
                mod.select_sql = select_sql;

                System.Threading.Thread.Sleep(1000);
                Them_Update_Column(context, mod, cot, null, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
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
        ADmin_JSON json = new ADmin_JSON();
        List<ad_column> lstAdColumn = json.ad_columnJSON();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id_ = context.Request.Form["id"];
            ad_column cot = db.ad_column.Where(s => s.ad_column_id == id_).Take(1).FirstOrDefault();
            //sort
            string id = cot.ad_module_id;
            ad_module mod = db.ad_module.Where(s => s.ad_module_id == (id)).Take(1).FirstOrDefault();
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string ma_column = context.Request.Form["ma_column"];
            bool focus = bool.Parse(context.Request.Form["focus"]);
            bool frozen = bool.Parse(context.Request.Form["frozen"]);
            bool not_order = string.IsNullOrWhiteSpace(context.Request.Form["not_order"]) ? false : bool.Parse(context.Request.Form["not_order"]);
            string edittype_ = context.Request.Form["edittype"];
            ad_editstyle edit_type = db.ad_editstyle.Where(s => s.ma_editstyle == edittype_).FirstOrDefault();
            string ma_editstyle = "", value_editstyle = "";
            if (edit_type != null)
            {
                ma_editstyle = edit_type.ma_editstyle;
                value_editstyle = edit_type.value_editstyle;
            }

            string old_ma_column = cot.ma_column;
            string old_index_cot = cot.index_cot;
            string old_mota = cot.mota;
            string sopt = context.Request.Form["sopt"];
            if (sopt == null | sopt == "") { sopt = "bw"; }
            if (cot != null)
            {
                cot.ma_column = ma_column;
                cot.ten_column = context.Request.Form["ten_column"];
                cot.index_cot = context.Request.Form["index_cot"];
                if (mod.thuake == null | mod.thuake == "")
                {
                    cot.key_cot = context.Request.Form["key_cot"];
                    cot.formatter = context.Request.Form["formatter"];
                    cot.unformat = context.Request.Form["unformat"];
                    cot.align = context.Request.Form["align"];
                    cot.stype = context.Request.Form["stype"];
                    cot.searchoptions = context.Request.Form["searchoptions"];
                    cot.formoptions = context.Request.Form["formoptions"];
                    cot.label = VNN_VariablePublic.DecodeHTML(context.Request.Form["label"]);
                    cot.editrules = context.Request.Form["editrules"];
                    cot.ma_edittype = ma_editstyle;
                    cot.edittype = value_editstyle;
                    cot.editoptions = context.Request.Form["editoptions"];
                    cot.important = context.Request.Form["important"];
                    cot.sapxep = sapxep;
                    cot.colspan = context.Request.Form["colspan"];
                    cot.formatoptions = context.Request.Form["formatoptions"];
                    cot.width = context.Request.Form["width"];
                    cot.@fixed = context.Request.Form["fixed"];
                    cot.reset_modify = bool.Parse(context.Request.Form["reset_modify"]);
                    cot.focus = bool.Parse(context.Request.Form["focus"]);
                    cot.disable_modify = context.Request.Form["disable_modify"];
                    cot.sopt = sopt;
                    cot.frozen = frozen;
                    cot.not_order = not_order;

                    cot.nguoicapnhat = Security.id_taikhoan(context);
                    cot.vaitrocapnhat = Security.id_vaitro(context);
                    cot.bophancapnhat = Security.id_phongban(context);
                    cot.ngaycapnhat = DateTime.Now;
                    cot.mota = context.Request.Form["mota"];
                    cot.hoatdong = true;
                }
                cot.editable = context.Request.Form["editable"];
                cot.hidden = context.Request.Form["hidden"];
                db.SaveChanges();
                lstAdColumn = lstAdColumn.Where(s => s.ad_column_id != cot.ad_column_id).ToList();
                lstAdColumn.Add(cot);
                if (focus == true)
                {
                    List<ad_column> cols = new List<ad_column>();
                    foreach (ad_column col in db.ad_column.Where(s => s.ma_module == mod.ma_module & s.ma_column != ma_column))
                    {
                        col.focus = false;
                        cols.Add(col);
                    }
                    db.SaveChanges();
                    if (cols.Count > 0)
                    {
                        lstAdColumn = lstAdColumn.Where(s => !cols.Select(t=>t.ad_column_id).Contains(s.ad_column_id)).ToList();
                        lstAdColumn.AddRange(cols);
                    }
                }

                string jsonData = JsonConvert.SerializeObject(lstAdColumn, Formatting.Indented);
                json.urlData = typeof(ad_column).Name;
                json.WriteJson(jsonData);
                if (mod.thuake != null & mod.thuake != "")
                {
                    VNN_Function.SortColumn("ad_column", sapxep, "ad_module_id", id, "ma_column", ma_column, null);
                    msg = "true#Cập nhật thành công.";
                }
                else
                {
                    string select_sql = VNN_Config.Select_sql(mod.ma_module, db);
                    select_sql = select_sql.Remove(select_sql.Length - 1);
                    string sql = "";
                    sql += "select " + select_sql + " from " + mod.from_sql
                        + " where 1=1 " + mod.where_sql;

                    if (!string.IsNullOrEmpty(mod.groupby_sql))
                        sql += " group by " + mod.groupby_sql;

                    if (!string.IsNullOrEmpty(mod.orderby_sql))
                        sql += " order by " + mod.orderby_sql;

                    if (VNN_Function.TestSQL(sql))
                    {
                        mod.select_sql = select_sql;
                        db.SaveChanges();
                        VNN_Function.SortColumn("ad_column", sapxep, "ad_module_id", id, "ma_column", ma_column, null);
                        msg = "true#Cập nhật thành công.";
                        Them_Update_Column(context, mod, cot, old_ma_column, db);
                    }
                    else
                    {
                        msg = "false#Sai cú pháp SQL." + sql;
                        cot.ma_column = old_ma_column;
                        cot.index_cot = old_index_cot;
                        cot.mota = old_mota;
                        db.SaveChanges();
                    }
                }
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = "false#Lỗi: Không tìm thấy đối tượng.";
            }
        }
        catch (Exception ex)
        {
            msg += "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        ADmin_JSON json = new ADmin_JSON();
        List<ad_column> LstAdColumn = json.ad_columnJSON();
        List<ad_module> LstAdModule = json.ad_moduleJSON();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_column cot = db.ad_column.Where(p => p.ad_column_id == (id)).Take(1).FirstOrDefault();
            ad_module mod = db.ad_module.Where(p => p.ad_module_id == (cot.ad_module_id)).Take(1).FirstOrDefault();
            if (cot != null)
            {
                if (mod.thuake != null & mod.thuake != "")
                {
                    var mod_tk = db.ad_module.Where(s => s.ad_module_id == (mod.thuake)).Select(s => new { s.ten_module, s.ma_module }).Take(1).FirstOrDefault();
                    msg = "false#Không thể xóa, Module này đang thừa kế từ module: " + mod_tk.ten_module + " - (" + mod_tk.ma_module + ")";
                }

                if (msg.Length <= 0)
                {
                    foreach (var pq in db.ad_role_mmcol.Where(s => s.ad_module_id == mod.ad_module_id & s.ad_column_id == cot.ad_column_id).ToList())
                    {
                        db.ad_role_mmcol.Remove(pq);
                    }
                    db.ad_column.Remove(cot);
                    db.SaveChanges();
                    LstAdColumn = LstAdColumn.Where(s => s.ad_column_id != cot.ad_column_id).ToList();

                    string select_sql = VNN_Config.Select_sql(mod.ma_module, db);
                    select_sql = select_sql.Remove(select_sql.Length - 1);
                    mod.select_sql = select_sql;
                    db.SaveChanges();
                    LstAdModule = LstAdModule.Where(s => s.ad_module_id != mod.ad_module_id).ToList();
                    LstAdModule.Add(mod);
                    VNN_Function.loaddulieu_Auto(db, ma_module);
                    msg = "true#Xóa column thành công.";
                }
            }
            else
            {
                msg = "false#Lỗi: Không tìm thấy column.";
            }
        }
        catch (Exception ex)
        {
            msg = "false#Lỗi: " + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void sel_edittype(HttpContext context)
    {
        //khai bao
        string id = context.Request.QueryString["id"];
        string kq = "";
        ad_editstyle ed = db.ad_editstyle.Where(p => p.ma_editstyle == (id)).Take(1).FirstOrDefault();
        kq += ed.ma_editstyle + "(##)";
        kq += ed.value_editoption + "(##)";
        kq += ed.value_formatoptions + "(##)";
        kq += ed.value_formatter + "(##)";
        context.Response.Write(kq);
    }

    public void Them_Update_Column(HttpContext context, ad_module mod, ad_column cot, string old_ma_column, EntityContext db)
    {
        ADmin_JSON json = new ADmin_JSON();
        List<ad_module> LstAdModule = json.ad_moduleJSON();

        if (old_ma_column == null)
        {
            foreach (ad_module mod_tk in LstAdModule.Where(s => s.thuake == mod.ad_module_id).ToList())
            {
                ad_column cot_tk = new ad_column
                {
                    ad_column_id = Helper.getNewId(),
                    ma_menu = mod_tk.@ma_menu,
                    ma_module = mod_tk.@ma_module,
                    @fixed = cot.@fixed,
                    ma_column = cot.@ma_column,
                    ten_column = cot.@ten_column,
                    index_cot = cot.@index_cot,
                    ad_module_id = mod_tk.ad_module_id,
                    width = cot.@width,
                    key_cot = cot.@key_cot,
                    hidden = cot.@hidden,
                    formatter = cot.@formatter,
                    unformat = cot.@unformat,
                    align = cot.@align,
                    stype = cot.@stype,
                    searchoptions = cot.@searchoptions,
                    formoptions = cot.@formoptions,
                    label = cot.@label,
                    editable = cot.@editable,
                    editrules = cot.@editrules,
                    ma_edittype = cot.@ma_edittype,
                    edittype = cot.@edittype,
                    editoptions = cot.@editoptions,
                    important = cot.@important,
                    sapxep = cot.@sapxep,
                    colspan = cot.@colspan,
                    formatoptions = cot.@formatoptions,
                    reset_modify = cot.@reset_modify,
                    focus = cot.@focus,
                    disable_modify = cot.@disable_modify,
                    sopt = cot.@sopt,
                    frozen = cot.@frozen,
                    not_order = cot.@not_order,
                    nguoitao = cot.@nguoitao,
                    vaitrotao = cot.@vaitrotao,
                    bophantao = cot.@bophantao,
                    nguoicapnhat = cot.@nguoicapnhat,
                    vaitrocapnhat = cot.@vaitrocapnhat,
                    bophancapnhat = cot.@bophancapnhat,
                    ngaytao = cot.@ngaytao,
                    ngaycapnhat = cot.@ngaycapnhat,
                    mota = cot.@mota,
                    hoatdong = true
                };
                db.ad_column.Add(cot_tk);
                db.SaveChanges();
                VNN_Function.SortColumn("ad_column", cot.@sapxep, "ad_module_id", mod_tk.ad_module_id, "ma_column", cot.@ma_column, null);
            }
        }
        else
        {
            foreach (ad_module mod_tk in LstAdModule.Where(s => s.thuake == (mod.ad_module_id)).ToList())
            {
                foreach (ad_column cot_tk in db.ad_column.Where(s => s.ad_module_id == (mod_tk.ad_module_id) & s.ma_column == (old_ma_column)).ToList())
                {
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
                    cot_tk.not_order = cot.not_order;
                    cot_tk.sapxep = cot.@sapxep;
                    if (context.Request.Form["loaicapnhat"] == "updatehidden")
                    {
                        cot_tk.hidden = cot.@hidden;
                    }
                    else if (context.Request.Form["loaicapnhat"] == "updateeditable")
                    {
                        cot_tk.editable = cot.@editable;
                    }
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
                db.SaveChanges();
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
