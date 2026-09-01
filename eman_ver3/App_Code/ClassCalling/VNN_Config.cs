using DataAcess;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class VNN_Config
{
    public static string get_ten_column(string ma_module, string ma_column)
    {
        EntityContext db = new EntityContext();
        ad_column cot = db.ad_column.Where(p => p.ma_module == (ma_module) & p.ma_column == (ma_column) & p.hoatdong == (true)).Take(1).FirstOrDefault();
        return cot.ten_column;
    }

    public static string[] get_records()
    {
        ADmin_JSON json = new ADmin_JSON();
        var items = json.ad_systemconfigJSON();
        var ttc = items.FirstOrDefault();
        return new string[2] { ttc.soluong_grid.ToString(), "[" + ttc.soluong_grid_2 + "]" };
    }

    public static string get_FormatDate()
    {
        string kq = "";
        ADmin_JSON json = new ADmin_JSON();
        var items = json.ad_systemconfigJSON();
        var ttc = items.FirstOrDefault();
        if (ttc != null)
        {
            if (ttc.format_ngay != null)
            {
                string format = ttc.format_ngay;
                if (!ttc.format_ngay.Contains("tt") & !ttc.format_ngay.Contains("TT"))
                {
                    format = ttc.format_ngay.Replace("hh", "HH");
                }
                else
                {
                    format = ttc.format_ngay.Replace("HH", "hh");
                }
                kq = format;
            }
        }
        return kq;
    }

    //Neu ket qua la DateTime.MinValue (NGay` bi bo trong)
    //Neu ket qua la kq = DateTime.MinValue.AddDays(1) (NGay` bi bo trong)
    public static DateTime setDateTime(string date, string fmt = "")
    {
        DateTime kq = DateTime.MinValue.AddDays(1);
        IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        if (fmt == "")
        {
            var json = new ADmin_JSON();
            var items = json.ad_systemconfigJSON();
            var ttc = items.FirstOrDefault();
            if (ttc != null)
            {
                if (ttc.format_ngay != null)
                {
                    string format = ttc.format_ngay;

                    if (!ttc.format_ngay.Contains("tt") & !ttc.format_ngay.Contains("TT"))
                    {
                        format = ttc.format_ngay.Replace("hh", "HH");
                    }
                    else
                    {
                        format = ttc.format_ngay.Replace("HH", "hh");
                    }

                    if (date.Length == 10)
                        format = format.Split(' ')[0];

                    if (VNN_Validate.check_number(date, "int"))
                    {
                        kq = DateTime.Now.AddDays(int.Parse(date));
                    }
                    else if (date != null & date != "")
                    {
                        try
                        {
                            kq = DateTime.ParseExact(date, format, culture);
                        }
                        catch
                        {
                            try { kq = DateTime.ParseExact(date.Replace(" ", "").ToString(), format, null); }
                            catch { kq = DateTime.MinValue.AddDays(1); }
                        }
                    }
                    else
                    {
                        kq = DateTime.MinValue;
                    }
                }
            }
        }
        else
        {
            kq = DateTime.ParseExact(date, fmt, culture);
        }
        return kq;
    }

    public static string[] get_colModel(System.Web.HttpContext context, string ma_module)
    {
        var lstDic = new List<Dictionary<string, object>>();
        var allTK = Security.all_taikhoan(context);
        var ma_user = allTK.ContainsKey("ma_user") ? allTK["ma_user"] : "";
        string roleId = allTK.ContainsKey("user_role") ? allTK["user_role"] + "" : "";
        var modelStr = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/CustomGrid/" + ma_user + "/grid" + ma_module + ".json");
        if (System.IO.File.Exists(modelStr))
            lstDic = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(System.IO.File.ReadAllText(modelStr));

        string[] kq = new string[10];
        kq[0] = ""; //Get Col_Model
        kq[1] = ""; //Colspan Edit
        kq[2] = ""; //Colspan Add
        kq[3] = ""; //Save default value
        kq[4] = ""; //Set default value
        kq[5] = ""; //Set Focus
        kq[6] = ""; //Disable column when Edit
        kq[7] = ""; //Disable column when Add
        string colspan_kq = "";
        string editoption_kq = "";
        string addoption_kq = "";
        string model_infor = "";

        int i_ = 0;
        var json = new ADmin_JSON();
        var cols = json.ad_columnJSON();
        var sels = json.ad_selectoptionJSON();
        var roleMMCols = json.ad_role_mmcolJSON();
        var roleMMVas = json.ad_role_mmvalueJSON();
        roleMMCols = roleMMCols.Where(s => s.ad_role_id == roleId).ToList();
        roleMMVas = roleMMVas.Where(s => s.ad_role_id == roleId).ToList();
        var col1s = cols.Where(s => s.ma_module == ma_module & s.hoatdong == true).ToList().Clone();
        var col2s = col1s.Clone();

        var lstDicShow = lstDic.Where(s => s["hidden"].ToString().ToLower() == "false").ToList();
        var lstDicHidden = lstDic.Where(s => s["hidden"].ToString().ToLower() == "true").ToList();

        var colsFilter = col1s.Where(s => lstDicShow.Select(t => t["name"].ToString()).Contains(s.ma_column)).OrderBy(s => s.sapxep).ToList();
        for (var i = 0; i < colsFilter.Count; i++)
        {
            var editCol = col2s.Where(s => s.ma_column == lstDicShow[i]["name"].ToString()).FirstOrDefault();
            if (editCol != null)
            {
                editCol.sapxep = colsFilter[i].sapxep;
                editCol.hidden = "false";
            }
        }

        foreach (var item in lstDicHidden)
        {
            var editCol = col2s.Where(s => s.ma_column == item["name"].ToString()).FirstOrDefault();
            if (editCol != null)
            {
                editCol.hidden = "true";
            }
        }

        col2s = col2s.OrderBy(p => p.sapxep).ToList();
        foreach (var cot in col2s)
        {
            var pqcot = roleMMCols.Where(s => s.ad_column_id == cot.ad_column_id).FirstOrDefault();
            if (pqcot != null)
            {
                cot.disable_modify = pqcot.disableEdit.GetValueOrDefault(false) ? "dis_all" : cot.disable_modify;
                cot.hidden = pqcot.disableView.ToString().ToLower();
            }

            var itemDic = lstDic.Where(s => s["name"].ToString() == cot.ma_column).FirstOrDefault();
            if (itemDic != null)
            {
                if (itemDic.ContainsKey("width"))
                    cot.width = itemDic["width"].ToString();
            }

            if (cot.disable_modify == "dis_all")
            {
                kq[6] += "khoa_column('" + cot.ma_column + "');";
                kq[7] += "khoa_column('" + cot.ma_column + "');";
            }
            else if (cot.disable_modify == "dis_edit")
            {
                kq[6] += "khoa_column('" + cot.ma_column + "');";
                kq[7] += "mokhoa_column('" + cot.ma_column + "');";
            }
            else if (cot.disable_modify == "dis_add")
            {
                kq[6] += "mokhoa_column('" + cot.ma_column + "');";
                kq[7] += "khoa_column('" + cot.ma_column + "');";
            }
            else
            {
                kq[6] += "mokhoa_column('" + cot.ma_column + "');";
                kq[7] += "mokhoa_column('" + cot.ma_column + "');";
            }

            if (cot.reset_modify == true)
            {
                kq[3] += "reset_column[" + i_ + "] = $('#" + cot.ma_column + "').val();";
                kq[4] += "$('#" + cot.ma_column + "').val(reset_column[" + i_ + "]);";
                i_++;
            }

            if (cot.focus == true)
            {
                kq[5] += "$('#" + cot.ma_column + "').focus();";
            }

            model_infor += "09378753400MACOL_VNN_" + cot.ma_column + "(##)" + cot.label + ")##(";
            string Label = "";
            if (cot.important == "true")
            {
                Label += "label:`" + cot.label + "` + dulieu_quantrong()";
            }
            else
            {
                Label += "label:`" + cot.label + "`";
            }

            kq[0] += "{";
            if (cot.key_cot != null & cot.key_cot != "")
                kq[0] += "key:" + cot.key_cot + ", xuatExcel:" + cot.editrules + ", khoaTuyChinh:" + cot.not_order.GetValueOrDefault(false).ToString().ToLower() + ",";
            else
                kq[0] += "key:false, xuatExcel" + cot.editrules + ", khoaTuyChinh:" + cot.not_order.GetValueOrDefault(false).ToString().ToLower() + ",";
            //--
            if (cot.@fixed != null & cot.@fixed != "")
                kq[0] += "fixed:" + cot.@fixed + ",";
            else
                kq[0] += "fixed:true,";
            //--
            if (cot.ten_column != null & cot.ten_column != "")
                kq[0] += "label:`" + cot.ten_column + "`,";
            //--
            if (cot.ma_column != null & cot.ma_column != "")
                kq[0] += "name:'" + cot.ma_column + "',";
            //--
            if (cot.index_cot != null & cot.index_cot != "")
                kq[0] += "index:'" + cot.index_cot.Replace("'", "\\'") + "',";
            //--
            if (cot.width != null & cot.width != "")
                kq[0] += "width:" + cot.width + ",";
            else
                kq[0] += "width:100,";
            //--
            if (cot.editable != null & cot.editable != "")
            {
                if (cot.editable == "true")
                    kq[0] += "editable:" + cot.editable + ", editrules:{ edithidden: true }, ";
                else
                    kq[0] += "editable:" + cot.editable + ",";
            }
            else
                kq[0] += "editable:true, editrules:{ edithidden: true },";
            //--
            if (cot.hidden != null & cot.hidden != "")
            {
                if (VNN_VariablePublic.view_origination == true)
                {
                    if (VNN_Validate.check_column_default_(cot.ma_column))
                        kq[0] += "hidden:false,";
                    else
                        kq[0] += "hidden:" + cot.hidden + ",";
                }
                else
                    kq[0] += "hidden:" + cot.hidden + ",";
            }
            else
            {
                kq[0] += "hidden:false,";
            }
            //--
            if (cot.formatter != null & cot.formatter != "")
            {
                string formatter = cot.formatter.Replace(" ", "");
                if (formatter.Length > 0)
                {
                    kq[0] += "formatter:" + cot.formatter + ",";
                    if (formatter != "'checkbox'" & formatter != "esc_date")
                        kq[0] += "unformat: disable_formatter, ";
                }
            }
            //--
            /*if (cot.unformat != null & cot.unformat != "")
                kq[0] += "unformat:" + cot.unformat + ",";*/
            //--
            if (cot.align != null & cot.align != "")
                kq[0] += "align:'" + cot.align + "',";
            else
                kq[0] += "align:'left',";
            //--
            if (cot.stype != null & cot.stype != "")
                kq[0] += "stype:'" + cot.stype + "',";
            //search option
            if (cot.sopt == "disable")
                kq[0] += "search:false,";

            kq[0] += "searchoptions:{sopt:['" + cot.sopt + "']";
            if (cot.searchoptions != null & cot.searchoptions != "")
            {
                if (cot.searchoptions.Replace(" ", "").Length > 0)
                {
                    string value_selectoption = cot.searchoptions.ToString();
                    int length_str = value_selectoption.LastIndexOf("%>");
                    int length_str0 = value_selectoption.LastIndexOf("%>0");
                    if (length_str > 0)
                    {
                        var value = sels.Where(s => value_selectoption.Contains(s.ma_selectoption)).Select(s => new { s.value_selectoption, s.ma_selectoption }).Take(1).FirstOrDefault();
                        if (value != null)
                        {
                            string gt_value = value.value_selectoption;
                            if (length_str0 > 0)
                            {
                                gt_value = value.value_selectoption.Remove(8, 6);
                            }
                            value_selectoption = value_selectoption.Replace(value.ma_selectoption, gt_value);
                        }
                    }
                    if (value_selectoption != "value: {0")
                        kq[0] += ", " + value_selectoption;
                }
            }
            kq[0] += "},";
            //--
            if (cot.edittype != null & cot.edittype != "")
            {
                if (cot.edittype.Replace(" ", "").Length > 0)
                    kq[0] += "edittype:'" + cot.edittype + "',";
            }
            //edit option
            if (cot.editoptions != null & cot.editoptions != "")
            {
                string gt_value = "";
                if (cot.editoptions.Replace(" ", "").Length > 0)
                {
                    string value_selectoption = cot.editoptions.ToString();
                    int length_str = value_selectoption.LastIndexOf("%>");
                    int length_str0 = value_selectoption.LastIndexOf("%>0");
                    if (length_str > 0)
                    {
                        var value = sels.Where(s => value_selectoption.Contains(s.ma_selectoption)).Select(s => new { s.value_selectoption, s.ma_selectoption }).Take(1).FirstOrDefault();
                        if (value != null)
                        {
                            gt_value = value.value_selectoption;
                            if (length_str0 > 0)
                            {
                                gt_value = value.value_selectoption.Remove(8, 6);
                                value_selectoption = value_selectoption.Replace(value.ma_selectoption + "0", gt_value);
                            }
                            else
                            {
                                value_selectoption = value_selectoption.Replace(value.ma_selectoption, gt_value);
                            }
                        }
                    }

                    var pqval = roleMMVas.Where(s => s.ad_column_id == cot.ad_column_id).FirstOrDefault();
                    if (pqval != null)
                    {
                        string jsonED = "";
                        Dictionary<string, string> jsonEDDT = null;
                        var vals = pqval.ten_column.Split(',');
                        foreach (var val in vals)
                        {
                            if (value_selectoption.Contains("value: {") | value_selectoption.Contains("value:{"))
                            {
                                if (jsonEDDT == null)
                                {
                                    jsonED = value_selectoption.Replace("value: {", "{").Replace("value:{", "{");
                                    try
                                    {
                                        jsonEDDT = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonED);
                                    }
                                    catch { }
                                }

                                if(jsonEDDT != null)
                                {
                                    jsonEDDT.Remove(val);
                                }
                            }
                            else
                            {
                                string mauVal1 = ";" + val + ":.*?;";
                                string mauVal2 = "'" + val + ":.*?;";
                                string mauVal3 = ";" + val + ":.*?'";
                                value_selectoption = Regex.Replace(value_selectoption, mauVal1, ";", RegexOptions.IgnoreCase);
                                value_selectoption = Regex.Replace(value_selectoption, mauVal2, "'", RegexOptions.IgnoreCase);
                                value_selectoption = Regex.Replace(value_selectoption, mauVal3, "'", RegexOptions.IgnoreCase);
                            }
                        }

                        if (jsonEDDT != null)
                        {
                            value_selectoption = "value: " + Newtonsoft.Json.JsonConvert.SerializeObject(jsonEDDT);
                        }
                    }

                    if (value_selectoption != "value: {")
                        kq[0] += "editoptions:{" + value_selectoption + "},";
                }
            }

            if (cot.frozen != null)
                kq[0] += "frozen:" + cot.frozen.ToString().ToLower() + ",";
            //--
            if (cot.formatoptions != null & cot.formatoptions != "")
            {
                if (cot.formatoptions.Replace(" ", "").Length > 0)
                    kq[0] += "formatoptions:{" + cot.formatoptions + "},";
            }
            if (cot.formoptions != null & cot.formoptions != "")
                kq[0] += "formoptions:{" + Label + "+" + cot.formoptions + "}";
            else
                kq[0] += "formoptions:{" + Label + "}";

            kq[0] += "},\n";

            if (cot.colspan != null & cot.colspan != "" & cot.colspan != " ")
            {
                int value_colspan = int.Parse(cot.colspan.ToString()) + 1;
                colspan_kq += "\ncolspanedit('tr_" + cot.ma_column + "','" + value_colspan + "'); $('.ui-jqdialog-content .CaptionTD').css('text-align','right');";
            }
            else if (cot.edittype == "textarea")
            {
                colspan_kq += "\n$('#" + cot.ma_column + "').css({'height':'inherit','width':'','resize':'vertical'});";
            }

            if (cot.formatter == "vnn_number" | cot.formatter == "esc_date")
            {
                colspan_kq += "\n$('#" + cot.ma_column + "').attr('autocomplete','off');";
            }
        }

        string funcAddEdit = "";
        funcAddEdit += "\nsetTimeout(function() {";
        funcAddEdit += "\n    let eleFirstFocus = formid.find('input[id!=\"id_g\"]:not(:disabled):visible,textarea:not(:disabled):visible,select:not(:disabled):visible');";
        funcAddEdit += "\n    let eleLastFocus = formid.find('input[id!=\"id_g\"]:not(:disabled),textarea:not(:disabled),select:not(:disabled)').last();";
        funcAddEdit += "\n    $('.changeValueToActiveFunc').change();";
        funcAddEdit += "\n    let eleFirstFocus1 = eleFirstFocus.not('.formatdate').first()";
        funcAddEdit += "\n    let eleFirstFocus2 = eleFirstFocus.first();";
        funcAddEdit += "\n    eleFirstFocus1.focus()";
        funcAddEdit += "\n    eleLastFocus.off('keydown');";
        funcAddEdit += "\n    eleLastFocus.keydown(function(e){ let code = e.keyCode || e.which; if(e.shiftKey) {  } else if(code == '9') { e.preventDefault(); eleFirstFocus2.focus(); } });";
        funcAddEdit += "\n}, 10);";

        funcAddEdit += string.Format("\nformVisible_Del_id = formVisible_addEdit_id = 'grid{0}';", ma_module);

        kq[1] = colspan_kq + editoption_kq;
        kq[2] = colspan_kq + addoption_kq;
        kq[6] += funcAddEdit;
        kq[7] += funcAddEdit;
        VNN_VariablePublic.Model_infor = model_infor;
        return kq;
    }

    public static string load_number(string number, int number_length)
    {
        int dodai = number_length - number.Length;
        string kq = "";
        for (int i = 0; i < dodai; i++)
        {
            kq += "0";
        }
        kq += number;

        return kq;
    }

    public static string Select_sql(string ma_module, EntityContext db)
    {
        string kq = "", tuybien = "@TuyBien";
        ADmin_JSON json = new ADmin_JSON();
        var columns = json.ad_columnJSON();
        var columnsModule = columns.Where(s => s.ma_module == ma_module & s.hoatdong == true);
        foreach (ad_column cot in columnsModule.OrderBy(p => p.key_cot).OrderBy(p => p.sapxep))
        {
            if (cot.mota != tuybien & cot.ten_column != tuybien & cot.label != tuybien)
            {
                if (cot.mota != null & cot.mota != "")
                {
                    kq += cot.mota + " as " + cot.ma_column + ",";
                }
                else
                {
                    if (cot.index_cot != null & cot.index_cot != "")
                        kq += cot.index_cot + " as " + cot.ma_column + ",";
                    else
                        kq += cot.ma_column + ",";
                }
            }
        }

        if (kq == "")
        {
            foreach (ad_column cot in db.ad_column.Where(s => s.ma_module == ma_module).OrderBy(p => p.key_cot).OrderBy(p => p.sapxep))
            {
                if (cot.mota != null & cot.mota != "")
                {
                    kq += cot.mota + " as " + cot.ma_column + ",";
                }
                else
                {
                    if (cot.index_cot != null & cot.index_cot != "")
                        kq += cot.index_cot + " as " + cot.ma_column + ",";
                    else
                        kq += cot.ma_column + ",";
                }
            }
        }

        return kq;
    }

    public static string get_NavFunc(System.Web.HttpContext context, string ma_module)
    {
        ADmin_JSON json = new ADmin_JSON();
        var cases = json.ad_caseJSON().Where(s => s.ma_module == ma_module & s.hoatdong == true).ToList();
        var modules = json.ad_moduleJSON();
        var role_mmcs = json.ad_role_mmcJSON();
        var user_mmcs = json.ad_user_mmcJSON();
        //--
        string class_btnclick = "";
        if (VNN_VariablePublic.view_origination == true) { class_btnclick = "btn_click"; }
        string hamxuly_refresh = "click_refresh(tengrid)";
        string icon_refresh = "glyphicon glyphicon-refresh";
        string title_refresh = "Làm mới dữ liệu";
        string hamxuly_clearserch = "click_refresh_clearsearch(tengrid)";
        string icon_clearserch = "glyphicon glyphicon-search";
        string title_clearserch = "Làm mới bộ tìm kiếm";
        string hamxuly_origination = "click_origination(tengrid)";
        string icon_origination = "glyphicon glyphicon-dashboard";
        string title_origination = "Xem nguồn gốc";
        string hamxuly_home = "click_loadtrangchu()";
        string icon_home = "glyphicon glyphicon-home";
        string title_home = "Trở về trang chủ";
        string kq_btnheader = "<div class=\"rightclick_header\" align=\"center\"><table align=\"center\"><tr>";
        kq_btnheader += "<td title = \"Làm mới dữ liệu\">" + VNN_Function.set_Icon(icon_refresh, "img_btnmacdinh", hamxuly_refresh, "img_btnrefresh", title_refresh) + "</td>";
        kq_btnheader += "<td title = \"Làm mới bộ tìm kiếm\">" + VNN_Function.set_Icon(icon_clearserch, "img_btnmacdinh", hamxuly_clearserch, "img_btnrefresh", title_clearserch) + "</td>";
        kq_btnheader += "<td title = \"Truy nguồn gốc\">" + VNN_Function.set_Icon(icon_origination, "img_btnmacdinh " + class_btnclick, hamxuly_origination, "img_btnorigination", title_origination) + "</td>";
        kq_btnheader += "<td title = \"Trở về trang chủ\">" + VNN_Function.set_Icon(icon_home, "img_btnmacdinh", hamxuly_home, "img_btntrangchu", title_home) + "</td>";
        kq_btnheader += "</tr></table></div>";
        string kq = "";
        kq += "\nfunction taonut_header() {";
        kq += "\nif(load_grid == 0) {";
        kq += "\n//lam moi nut header";
        kq += "\n$('.cl_div_chuathanhcongcu').empty();";
        kq += "\nvar nutnhan =  '<div style=\"margin: -5px 0 0 0; height:25px\">';";
        kq += "\nvar chuotphai = '<div id=\"div_chuotphai\" class=\"div_fixed\">';";
        kq += "\nchuotphai += '" + kq_btnheader + "';";
        kq += "\nchuotphai += '<table class=\"table_div_chuotphai\">';";
        foreach (ad_case cn in cases.OrderBy(p => p.sapxep))
        {
            if (cn.thuake == null | cn.thuake == "")
            {
                if (Security.PhanQuyen_ChucNang(context, cases, modules, role_mmcs, user_mmcs, ma_module, cn.ad_case_id) == true & cn.hamxuly != null & cn.hamxuly != "")
                {
                    string hamxuly = cn.hamxuly.Replace("ma_case", "\\'" + cn.ma_case + "\\'");
                    if (cn.id_parent == true) { hamxuly = hamxuly.Replace("id_parent", "null"); }
                    string id_case = "img_btn" + cn.ma_case, title_case = cn.ten_case;
                    kq += "\nnutnhan += '" + VNN_Function.set_Icon(cn.logo, "img_btnmacdinh", hamxuly, id_case, title_case) + "';";

                    kq += "\nchuotphai +='<tr onclick=\"" + hamxuly + "\">' +";
                    kq += "\n'<td style=\"width: 25px;\">' +";
                    kq += "\n'" + VNN_Function.set_Icon(cn.logo, "img_rightdiv", "", "", "") + "' +";
                    kq += "\n'</td>' +";
                    kq += "\n'<td>' +";
                    kq += "\n'<a>" + cn.ten_case + "</a>' +";
                    kq += "\n'</td>' +";
                    kq += "\n'</tr>';";
                }
            }
        }
        //refresh
        kq += "\n//refresh";
        kq += "\nnutnhan += '" + VNN_Function.set_Icon(icon_refresh, "img_btnmacdinh", hamxuly_refresh, "img_btnrefresh", title_refresh) + "';";
        //origination
        kq += "\n//origination";
        kq += "\nnutnhan += '" + VNN_Function.set_Icon(icon_origination, "img_btnmacdinh " + class_btnclick, hamxuly_origination, "img_btnorigination", title_origination) + "';";
        //home
        kq += "\n//trang chu";
        kq += "\nnutnhan += '" + VNN_Function.set_Icon(icon_home, "img_btnmacdinh", hamxuly_home, "img_btntrangchu", title_home) + "';";

        kq += "\n//tao nut tren header";
        kq += "\n$('.cl_div_chuathanhcongcu').append(nutnhan);";
        kq += "\nchuotphai += '</table></div>';";
        kq += "\nload_menurightclick(tengrid,chuotphai);";
        kq += "\nSetWidth_BtnHeader(10);";
        kq += "\n}";
        kq += "\n}";

        if (VNN_VariablePublic.view_origination == true)
            VNN_VariablePublic.view_origination = false;
        return kq;
    }

    public static string get_NavFunc2(System.Web.HttpContext context, string ma_module, int capmodule)
    {
        var json = new ADmin_JSON();
        var cases = json.ad_caseJSON().Where(s => s.ma_module == ma_module & (s.isview ?? false) == false & s.hoatdong == true).ToList();
        var modules = json.ad_moduleJSON();
        var role_mmcs = json.ad_role_mmcJSON();
        var user_mmcs = json.ad_user_mmcJSON();
        //--
        string class_btnclick = "";
        if (VNN_VariablePublic.view_origination == true) { class_btnclick = "btn_click"; }
        string hamxuly_refresh = "click_refresh(tengrid)";
        string icon_refresh = "glyphicon glyphicon-refresh";
        string title_refresh = "Làm mới dữ liệu";
        string hamxuly_clearserch = "click_refresh_clearsearch(tengrid)";
        string icon_clearserch = "glyphicon glyphicon-search";
        string title_clearserch = "Làm mới bộ tìm kiếm";
        string hamxuly_origination = "click_origination(tengrid)";
        string icon_origination = "glyphicon glyphicon-dashboard";
        string title_origination = "Xem nguồn gốc";
        string hamxuly_home = "click_loadtrangchu()";
        string icon_home = "glyphicon glyphicon-home";
        string title_home = "Trở về trang chủ";
        string id_img_btnorigination = "img_btnorigination_grid" + ma_module;
        string kq_btnheader = "";
        kq_btnheader += $@"<div class=""rightclick_header"" align=""center""><table align=""center""><tr>";
        kq_btnheader += $@"<td title=""Làm mới dữ liệu"">{VNN_Function.set_Icon(icon_refresh, "img_btnmacdinh", hamxuly_refresh, "img_btnrefresh", title_refresh)}</td>";
        kq_btnheader += $@"<td title=""Truy nguồn gốc"">{VNN_Function.set_Icon(icon_origination, id_img_btnorigination + " img_btnmacdinh " + class_btnclick, hamxuly_origination, "", title_origination)}</td>";
        kq_btnheader += $@"<td title=""Xem nhật ký"">{VNN_Function.set_Icon("glyphicon glyphicon-info-sign", "img_btnmacdinh", "view_log(tengrid)", "img_btntrangchu", "Xem nhật ký")}</td>";
        kq_btnheader += $@"<td title=""Cấu hình lưới dữ liệu"">{VNN_Function.set_Icon("glyphicon glyphicon-cog", "img_btnmacdinh", "configModelGridForUser(tengrid)", "img_btnrefresh", "Cấu hình lưới dữ liệu")}</td>";
        kq_btnheader += $@"</tr></table></div>";
        string kq = "";
        if (capmodule == 0)
            kq += "\nfunction taonut_header(tengrid_hd) {";
        else if (capmodule == 1)
            kq += "\nfunction taonut_header1(tengrid_hd) {";
        else if (capmodule == 2)
            kq += "\nfunction taonut_header2(tengrid_hd) {";

        kq += $@"{Environment.NewLine}//lam moi nut header";
        kq += $@"{Environment.NewLine}var nutnhan = `<div class='div-left-mouse'>`;";
        kq += $@"{Environment.NewLine}var chuotphai = `<div id='div_chuotphai' class='div_fixed'>`;";
        kq += $@"{Environment.NewLine}chuotphai += `{kq_btnheader}`;";
        kq += $@"{Environment.NewLine}chuotphai += `<table class='table_div_chuotphai'>`;";

        string detailfncChuotPhai = "";
        foreach (ad_case cn in cases.OrderBy(p => p.sapxep))
        {
            if (cn.thuake == null | cn.thuake == "")
            {
                if (Security.PhanQuyen_ChucNang(context, cases, modules, role_mmcs, user_mmcs, ma_module, cn.ad_case_id) == true & cn.hamxuly != null & cn.hamxuly != "")
                {
                    string hamxuly = cn.hamxuly.Replace("ma_case", "\\'" + cn.ma_case + "\\'");
                    if (cn.id_parent == true) { hamxuly = hamxuly.Replace("id_parent", "null"); }
                    string id_case = "img_btn" + cn.ma_case, title_case = cn.ten_case;
                    string mota = string.IsNullOrEmpty(cn.mota) ? "1==1" : cn.mota;

                    kq += "\nnutnhan += '" + VNN_Function.set_Icon(cn.logo, "img_btnmacdinh", hamxuly, id_case, title_case) + "';";
                    kq += "\nchuotphai +='<tr id=\"" + cn.ma_case + "\" onclick=\"" + hamxuly + "\">' +";
                    kq += "\n'<td style=\"width: 25px;\">' +";
                    kq += "\n'" + VNN_Function.set_Icon(cn.logo, "img_rightdiv", "", "", "") + "' +";
                    kq += "\n'</td>' +";
                    kq += "\n'<td>' +";
                    kq += "\n'<a>" + cn.ten_case + "</a>' +";
                    kq += "\n'</td>' +";
                    kq += "\n'</tr>';";
                    detailfncChuotPhai += "if(" + mota + ") {";
                    detailfncChuotPhai += "$('#" + cn.ma_case + "').show();";
                    detailfncChuotPhai += "$('#img_btn" + cn.ma_case + "').show();";
                    detailfncChuotPhai += "$('#img_btn2" + cn.ma_case + "').show();";
                    detailfncChuotPhai += "} else {";
                    detailfncChuotPhai += "$('#" + cn.ma_case + "').hide();";
                    detailfncChuotPhai += "$('#img_btn" + cn.ma_case + "').hide();";
                    detailfncChuotPhai += "$('#img_btn2" + cn.ma_case + "').hide();";
                    detailfncChuotPhai += "}";
                }
            }
        }
        //refresh
        kq += "\n//refresh";
        kq += "\nnutnhan += '" + VNN_Function.set_Icon(icon_refresh, "img_btnmacdinh", hamxuly_refresh, "img_btnrefresh", title_refresh) + "';";
        //origination
        kq += "\n//origination";
        kq += "\nnutnhan += '" + VNN_Function.set_Icon(icon_origination, id_img_btnorigination + " img_btnmacdinh " + class_btnclick, hamxuly_origination, "", title_origination) + "';";
        //home
        kq += "\n//trang chu";
        kq += "\nnutnhan += '" + VNN_Function.set_Icon(icon_home, "img_btnmacdinh", hamxuly_home, "img_btntrangchu", title_home) + "';";

        kq += string.Format(@"let fncChuotPhai = function(a) {{ 
            let idSel = $('#' + a).jqGrid('getGridParam', 'selrow');
			let rowData = $('#'+ a).jqGrid('getRowData', idSel);
            {0}
        }}", detailfncChuotPhai);

        kq += "\n//tao nut tren header";
        kq += "\nchuotphai += '</table></div>';";
        kq += "\nload_menurightclick(tengrid_hd, chuotphai, fncChuotPhai, nutnhan);";
        kq += "\nSetWidth_BtnHeader(10);";
        kq += "\nload_menuleftclick(tengrid_hd, nutnhan, fncChuotPhai);";
        kq += "\n$('#org_' + tengrid_hd).val(0);";
        kq += "\n}";


        if (VNN_VariablePublic.view_origination == true)
            VNN_VariablePublic.view_origination = false;

        return kq.Replace("tengrid", "tengrid" + capmodule).Replace("id_parent", "id_parent" + capmodule)
        .Replace("Form_infor", "Form_infor" + capmodule).Replace("Model_infor", "Model_infor" + capmodule)
        .Replace("load_stt", "load_stt" + capmodule);
    }

    //private static string get_CaseKT(ad_case cn, LinqDataContext db, System.Web.HttpContext context, string ma_case, bool id_parent)
    //{
    //    string kq = "";
    //    if (cn.thuake != null & cn.thuake != "")
    //    {
    //        cn = db.ad_cases.Where(s => s.ad_case_id == (cn.thuake) & s.hoatdong == (true)).FirstOrDefault();
    //        get_CaseKT(cn, db, context, ma_case, id_parent);
    //    }

    //    string ma_module = db.ad_modules.Where(s => s.ad_module_id == (cn.ad_module_id)).Select(s => s.ma_module).FirstOrDefault();
    //    if (Security.PhanQuyen_ChucNang(context, ma_module, cn.ad_case_id) == true & cn.hamxuly != null & cn.hamxuly != "")
    //    {
    //        string hamxuly = cn.hamxuly.Replace("ma_case", "\\'" + ma_case + "\\'");
    //        if (id_parent == true) { hamxuly = hamxuly.Replace("id_parent", "null"); }
    //        string id_case = "img_btn" + cn.ma_case, title_case = cn.ten_case;
    //        //kq += "\nnutnhan += '<img onclick=\"" + hamxuly + "\" id=\"img_btn" + ma_case + "\" class=\"img_btnmacdinh\"  title=\"" + cn.ten_case + "\" alt=\"\" src=\"" + cn.logo + "\"/>';";
    //        kq += "\nnutnhan += '" + VNN_Function.set_Icon(cn.logo, "img_btnmacdinh", hamxuly, id_case, title_case) + "';";
    //        kq += "\nchuotphai +='<tr onclick=\"" + hamxuly + "\">' +";
    //        kq += "\n'<td style=\"width: 25px;\">' +";
    //        kq += "\n'" + VNN_Function.set_Icon(cn.logo, "img_rightdiv", "", "", "") + "' +";
    //        kq += "\n'</td>' +";
    //        kq += "\n'<td>' +";
    //        kq += "\n'<a>" + cn.ten_case + "</a>' +";
    //        kq += "\n'</td>' +";
    //        kq += "\n'</tr>';";
    //    }
    //    return kq;
    //}

    public static Module_TK get_ModuleKeThua(ad_module mod, int i, string ma_module, string where_sql, string url, object db)
    {
        ADmin_JSON json = new ADmin_JSON();
        var modules = json.ad_moduleJSON();
        //Start Xet truong hop la module thua ke
        if (i == 0)
        {
            mod = modules.Where(s => s.ma_module == ma_module).Take(1).FirstOrDefault();
            where_sql = mod.where_sql;
            url = mod.url;
        }
        if (mod != null)
        {
            if (!string.IsNullOrEmpty(mod.thuake))
            {
                mod = modules.Where(s => s.ad_module_id == mod.thuake & s.hoatdong == true).FirstOrDefault();
                where_sql += " " + mod.where_sql;
                i = 1;
                get_ModuleKeThua(mod, i, ma_module, where_sql, url, db);
            }
        }
        string ma_moduletk = ma_module;
        if (mod.ma_module != ma_module) { ma_moduletk = mod.ma_module; }
        Module_TK mod_ = new Module_TK
        {
            ad_module_id = mod.ad_module_id,
            ma_module = ma_module,
            ma_moduletk = ma_moduletk,
            ten_module = mod.ten_module,
            select_sql = mod.select_sql,
            from_sql = mod.from_sql,
            where_sql = mod.where_sql,
            orderby_sql = mod.orderby_sql,
            groupby_sql = mod.groupby_sql,
            procedure_sql = mod.procedure_sql,
            capmodule = mod.capmodule.Value,
            ma_modulecha = mod.ma_modulecha.ToString(),
            url = url,
            row_count = mod.row_count.Value,
            loai_module = mod.loai_module.ToString()
        };
        return mod_;
        //End Xet truong hop la module thua ke
    }

    public static string get_navGrid(System.Web.HttpContext context, string ma_module)
    {
        string kq = "";
        string add = "true", edit = "true", del = "true", view = "true", search = "false", refresh = "true";

        kq += "add:" + add + ",";
        kq += "edit:" + edit + ",";
        kq += "del:" + del + ",";
        kq += "search:" + search + ",";
        kq += "view:" + view + ",";
        kq += "refresh:" + refresh;
        return kq;
    }

    public static string[][] get_ModifyFormInfor(string ma_module)
    {
        int khaibaomang = 5;
        String[][] kq = new String[khaibaomang][];
        for (int dem = 0; dem < khaibaomang; dem++)
        {
            kq[dem] = new string[khaibaomang];
        }
        ADmin_JSON json = new ADmin_JSON();
        var cases = json.ad_caseJSON();
        string default_canhgiua = "formid.closest('div.ui-jqdialog').dialogCenter();",
            default_title = "#edithd",
            default_WH = "#editmod",
            default_FWH = "#FrmGrid_";

        string form_infor = "";
        foreach (ad_case cn in cases.Where(s => s.ma_module == ma_module & s.hoatdong == true).ToList().OrderBy(p => p.sapxep))
        {
            if (cn.hamxuly != null)
            {
                int i = -1;
                if (cn.hamxuly == ("click_edit(tengrid)"))
                    i = 0;
                else if (cn.hamxuly == ("click_add(tengrid)"))
                    i = 1;
                else if (cn.hamxuly == ("click_del(tengrid)"))
                {
                    i = 2;
                    default_title = "#delhd";
                    default_WH = "#delmod";
                    default_FWH = "#DelTbl_";
                }
                else if (cn.hamxuly == ("click_view(tengrid)"))
                {
                    i = 3;
                    default_title = "#edithd";
                    default_WH = "#editmod";
                    default_FWH = "#FrmGrid_";
                }
                else
                    i = -1;
                //Check data
                if (i >= 0)
                {
                    if (i == 3)
                    {
                        kq[i][0] += "\n viewform(tengrid);";
                    }
                    //Width of form
                    if (cn.dodaiForm != null & cn.dodaiForm != "")
                    {
                        kq[i][0] += "\n$('" + default_WH + "' + tengrid).css('width'," + cn.dodaiForm + ");";
                        kq[i][0] += "\n$('" + default_FWH + "' + tengrid).css('width'," + cn.dodaiForm + ");";
                    }
                    else
                    {
                        kq[i][0] += "$('" + default_WH + "' +tengrid).css('width',500);";
                        kq[i][0] += "$('" + default_FWH + "' + tengrid).css('width',500);";
                    }
                    //Height of form
                    if (cn.docaoForm != null & cn.docaoForm != "")
                    {
                        kq[i][1] += "\n$('" + default_WH + "' + tengrid).css('height', " + cn.docaoForm + ");";
                        kq[i][1] += "\n$('" + default_FWH + "'+ tengrid).css('height', " + cn.docaoForm + " - 66);";
                    }
                    else
                    {
                        kq[i][1] += "\n$('" + default_WH + "' + tengrid).css('height', 'auto');";
                        kq[i][1] += "\n$('" + default_FWH + "'+ tengrid).css('height', 'auto');";
                    }

                    kq[i][1] += "\nsetTimeout(function() {";
                    kq[i][1] += "\n     if($('" + default_WH + "' + tengrid).height() > window.innerHeight - 6) { $('" + default_WH + "' + tengrid).height(window.innerHeight - 6); }";
                    kq[i][1] += "\n     if($('" + default_FWH + "' + tengrid).height() > window.innerHeight - 72) { $('" + default_FWH + "' + tengrid).height(window.innerHeight - 72); $('#FrmGrid_' + tengrid).closest('div.ui-jqdialog').css('top','0px'); }";
                    kq[i][1] += "\n}, 100);";

                    //Center for form
                    if (cn.canhgiua != null & cn.canhgiua == (false))
                        kq[i][2] = "";
                    else if (i < 3)
                        kq[i][2] = default_canhgiua;
                    else
                        kq[i][2] = "$('#FrmGrid_' + tengrid).closest('div.ui-jqdialog').dialogCenter();";
                    //Title of form
                    if (cn.tieude != null & cn.tieude != "")
                    {
                        kq[i][3] = @"$('" + default_title + "'+tengrid +' span').empty(); $('" + default_title + "'+tengrid +' > .img_title_jqgrid').remove();" +
                            "$('" + default_title + "'+tengrid +' span').prepend('" + cn.tieude + " - [ '+ get_now() +' ]');" +
                            "$('" + default_title + "'+tengrid).prepend('" + VNN_Function.set_Icon(cn.logo, "img_title_jqgrid", "", "", "") + "');";
                    }
                    else
                    {
                        kq[i][3] = @"$('" + default_title + "'+tengrid +' span').empty(); $('" + default_title + "'+tengrid +' > .img_title_jqgrid').remove();" +
                            "$('" + default_title + "'+tengrid +' span').prepend('" + cn.ten_case + " - [ '+ get_now() +' ]');" +
                            "$('" + default_title + "'+tengrid).prepend('" + VNN_Function.set_Icon(cn.logo, "img_title_jqgrid", "", "", "") + "');";
                    }

                    //Close form after modify
                    kq[i][4] = "$('.ui-state-error').css('display','none');";
                    if (cn.hidden_modify == true)
                        kq[i][4] += "$.jgrid.hideModal('" + default_WH + "' + tengrid, { gbox: '#gbox_'+tengrid});";

                    if (i == 3)
                    {
                        kq[i][3] += "\n hideform(tengrid);";
                    }
                }
                else
                {
                    form_infor += "09378753400MACAS_VNN_" + cn.ma_case + "(##)";
                    form_infor += cn.logo + "(##)";
                    form_infor += cn.dodaiForm + "(##)";
                    form_infor += cn.docaoForm + "(##)";
                    form_infor += cn.canhgiua + "(##)";
                    form_infor += cn.hidden_modify + "(##)";
                    if (cn.tieude != null & cn.tieude != "")
                        form_infor += cn.tieude + ")##(";
                    else
                        form_infor += cn.ten_case + ")##(";
                }
            }
        }
        VNN_VariablePublic.Form_infor = form_infor;
        return kq;
    }

    public static string[][] get_ModifyFormInfor2(string ma_module, int capmod)
    {
        string tengrid = "tengrid" + capmod;
        int khaibaomang = 7;
        String[][] kq = new String[khaibaomang][];
        for (int dem = 0; dem < khaibaomang; dem++)
        {
            kq[dem] = new string[khaibaomang];
        }

        ADmin_JSON json = new ADmin_JSON();
        var cases = json.ad_caseJSON();
        string default_canhgiua = "formid.closest('div.ui-jqdialog').dialogCenter();",
            default_title = "#edithd",
            default_WH = "#editmod",
            default_FWH = "#FrmGrid_";

        string form_infor = "";
        foreach (ad_case cn in cases.Where(s => s.ma_module == ma_module & (s.isview ?? false) == false).OrderBy(p => p.sapxep).ToList())
        {
            if (cn.hamxuly != null)
            {
                int i = -1;
                if (cn.hamxuly == ("click_edit(tengrid)"))
                    i = 0;
                else if (cn.hamxuly == ("click_add(tengrid)"))
                    i = 1;
                else if (cn.hamxuly == ("click_del(tengrid)"))
                {
                    i = 2;
                    default_title = "#delhd";
                    default_WH = "#delmod";
                    default_FWH = "#DelTbl_";
                }
                else if (cn.hamxuly == ("click_view(tengrid)"))
                {
                    i = 3;
                    default_title = "#edithd";
                    default_WH = "#editmod";
                    default_FWH = "#FrmGrid_";
                }
                else
                    i = -1;
                //Check data
                if (i >= 0)
                {
                    //Width of form
                    if (cn.dodaiForm != null & cn.dodaiForm != "")
                    {
                        kq[i][0] += $"\n$('{default_WH}' + {tengrid}).css('width', {cn.dodaiForm});";
                        kq[i][0] += $"\n$('{default_FWH}' + {tengrid}).css('width', 'calc({cn.dodaiForm} - 0.25em)');";
                    }
                    else
                    {
                        kq[i][0] += $"\n$('{default_WH}' + {tengrid}).css('width',500);";
                        kq[i][0] += $"\n$('{default_FWH}' + {tengrid}).css('width',500);";
                    }
                    //Height of form
                    if (cn.docaoForm != null & cn.docaoForm != "")
                    {
                        kq[i][1] += $"\n$('{default_WH}' + {tengrid}).css('height', {cn.docaoForm});";
                        kq[i][1] += $"\n$('{default_FWH}' + {tengrid}).css('height', {cn.docaoForm} - 86);";
                        //kq[i][1] += $"\n$('{default_WH}' + {tengrid}).dialog('option', 'height', {cn.docaoForm});";
                    }
                    else
                    {
                        kq[i][1] += $"\n$('{default_WH}' + {tengrid}).css('height', 'auto');";
                        kq[i][1] += $"\n$('{default_FWH}' + {tengrid}).css('height', 'auto');";
                    }
                    kq[i][1] += $"\n$('{default_WH}' + {tengrid}).css('max-height', window.innerHeight - 6);";
                    kq[i][1] += $"\n$('{default_FWH}' + {tengrid}).css('max-height', window.innerHeight - 92);";
                    kq[i][1] += "\nsetTimeout(function() {";
                    //kq[i][1] += "\n     if($('" + default_WH + "' + " + tengrid + ").height() > window.innerHeight - 6) { $('" + default_WH + "' + " + tengrid + ").height(window.innerHeight - 6); }";
                    //kq[i][1] += "\n     if($('" + default_FWH + "' + " + tengrid + ").height() > window.innerHeight - 72) { $('" + default_FWH + "' + " + tengrid + ").height(window.innerHeight - 72); $('#FrmGrid_' + " + tengrid + ").closest('div.ui-jqdialog').css('top','0px'); }";
                    if (!Security.PhanQuyen_ChucNang(System.Web.HttpContext.Current, ma_module, cn.ad_case_id) | !cn.hoatdong)
                    {
                        kq[i][1] += $"\n     $('td#edit_' + {tengrid}).attr('typeE') ? null : $.jgrid.hideModal('{default_WH}' + {tengrid}, {{ gbox: '#gbox_'+ {tengrid} }});";
                    }
                    kq[i][1] += "\n}, 100);";

                    //Center for form
                    if (cn.canhgiua != null & cn.canhgiua == false)
                        kq[i][2] = "";
                    else if (i < 3)
                        kq[i][2] = $"{default_canhgiua} setTimeout(function() {{ {default_canhgiua} }}, 0); ";
                    else
                    {
                        var dialogCenter = $"$('#FrmGrid_' + {tengrid}).closest('div.ui-jqdialog').dialogCenter();";
                        kq[i][2] = $"{dialogCenter} setTimeout(function() {{ {dialogCenter} }}, 0);";
                    }

                    //Title of form
                    if (cn.tieude != null & cn.tieude != "")
                        kq[i][3] = @"$('" + default_title + "'+" + tengrid + " +' span').empty(); $('" + default_title + "'+ " + tengrid + " +' > .img_title_jqgrid').remove();" +
                            "$('" + default_title + "'+ " + tengrid + " +' span').prepend('" + cn.tieude + " - [ '+ get_now() +' ]');" +
                            "$('" + default_title + "'+ " + tengrid + ").prepend('" + VNN_Function.set_Icon(cn.logo, "img_title_jqgrid", "", "", "") + "');";
                    else
                        kq[i][3] = @"$('" + default_title + "'+ " + tengrid + " +' span').empty(); $('" + default_title + "'+ " + tengrid + " +' > .img_title_jqgrid').remove();" +
                            "$('" + default_title + "'+ " + tengrid + " +' span').prepend('" + cn.ten_case + " - [ '+ get_now() +' ]');" +
                            "$('" + default_title + "'+ " + tengrid + ").prepend('" + VNN_Function.set_Icon(cn.logo, "img_title_jqgrid", "", "", "") + "');";
                    //Close form after modify
                    kq[i][4] = "$('.ui-state-error').css('display','none');";
                    if (cn.hidden_modify == true)
                        kq[i][4] += "$.jgrid.hideModal('" + default_WH + "' + " + tengrid + ", { gbox: '#gbox_'+ " + tengrid + "});";

                    if (i == 3)
                    {
                        kq[i][0] = "\nviewform(" + tengrid + ");" + kq[i][0];
                        kq[i][3] += "\n hideform(" + tengrid + ");";
                    }
                    else if (i == 1)
                    {
                        kq[i][4] += "load_grid" + capmod + " = 3;";
                    }

                    kq[i][5] = string.Format("\ninput_focus_public = $('{0}'+{1}).find(':focus');", default_FWH, tengrid);
                    kq[i][6] = string.Format("\nsetTimeout(function(){{input_focus_public.focus();}}, 10);");
                }
                else
                {
                    form_infor += "09378753400MACAS_VNN_" + cn.ma_case + "(##)";
                    form_infor += cn.logo + "(##)";
                    form_infor += cn.dodaiForm + "(##)";
                    form_infor += cn.docaoForm + "(##)";
                    form_infor += cn.canhgiua + "(##)";
                    form_infor += cn.hidden_modify + "(##)";
                    if (cn.tieude != null & cn.tieude != "")
                        form_infor += cn.tieude + ")##(";
                    else
                        form_infor += cn.ten_case + ")##(";
                }
            }
        }


        VNN_VariablePublic.Form_infor = form_infor;
        return kq;
    }

    public static string[] get_IDParent_STTLoad(string ma_module)
    {
        string[] number = new string[6];

        //Tìm STT
        number[0] = "null";
        //Tìm Id cha
        ADmin_JSON json = new ADmin_JSON();
        var mods = json.ad_moduleJSON();
        var cases = json.ad_caseJSON();
        ad_module mod = mods.Where(s => s.ma_module == ma_module).Take(1).FirstOrDefault();
        if (mod.ma_modulecha != null & mod.ma_modulecha != "")
        {
            ad_module mod_cha = mods.Where(s => s.ad_module_id == mod.ma_modulecha).Take(1).FirstOrDefault();
            if (mod_cha != null)
            {
                if (mod_cha.thuake == null | mod_cha.thuake == "")
                {
                    number[1] = "id_" + mod_cha.ma_module;
                }
                else
                {
                    ad_module mod_cha_ = mods.Where(s => s.ad_module_id == mod_cha.thuake).Take(1).FirstOrDefault();
                    number[1] = "id_" + mod_cha_.ma_module;
                }

                number[1] = string.Format(@"checkPostParamsToServer({0}) ? {0} : null;", number[1]);
            }
            else
            {
                number[1] = "null";
            }
        }
        else
        {
            number[1] = "null";
        }
        //Tìm header
        if (mod.header_grid != null)
        {
            if (mod.header_grid.Replace(" ", "").Length > 0)
                number[2] = mod.header_grid;
            else
                number[2] = "''";
        }
        else { number[2] = "''"; }
        //Tìm mutil select
        if (mod.mutil_select == true)
        {
            number[3] = "true";
        }
        else { number[3] = "false"; }
        //Tìm doublick
        if (mod.double_click != null)
        {
            string strReplace = mod.double_click.Replace(mod.ad_module_id, "");
            string hamxuly_case = cases.Where(s => s.ad_case_id == strReplace).Select(s => s.hamxuly).Take(1).FirstOrDefault();
            number[4] = hamxuly_case;
        }
        else { number[4] = ""; }

        string str = "";
        str += "jQuery('#' + tengrid).jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });";
        str += "jQuery('#pager' + tengrid + '_left table').css('display', 'none');";
        str += "jQuery('#' + tengrid).jqGrid('setFrozenColumns');";
        str += "jQuery('#' + tengrid).jqGrid('bindKeys', {";
        str += "    \"onEnter\":function(rowid) {";
        str += "    }";
        str += "});";

        str += "\nformVisible_addEdit_id = formVisible_Del_id = tengrid;";

        number[5] = str;

        return number;
    }

    public static string[] get_IDParent_STTLoad2(string ma_module, int capmodule)
    {
        string[] number = new string[6];
        //Tìm STT
        number[0] = "null";
        //Tìm Id cha
        ADmin_JSON json = new ADmin_JSON();
        var mods = json.ad_moduleJSON();
        var cases = json.ad_caseJSON();
        ad_module mod = mods.Where(s => s.ma_module == ma_module).Take(1).FirstOrDefault();
        if (mod.ma_modulecha != null & mod.ma_modulecha != "")
        {
            ad_module mod_cha = mods.Where(s => s.ad_module_id == mod.ma_modulecha).Take(1).FirstOrDefault();
            if (mod_cha != null)
            {
                if (mod_cha.thuake == null | mod_cha.thuake == "")
                {
                    number[1] = "id_" + mod_cha.ma_module;
                }
                else
                {
                    ad_module mod_cha_ = mods.Where(s => s.ad_module_id == mod_cha.thuake).Take(1).FirstOrDefault();
                    number[1] = "id_" + mod_cha_.ma_module;
                }
            }
            else
            {
                number[1] = "null";
            }
        }
        else
        {
            number[1] = "null";
        }
        //Tìm header
        if (mod.header_grid != null)
        {
            if (mod.header_grid.Replace(" ", "").Length > 0)
                number[2] = mod.header_grid;
            else
                number[2] = "''";
        }
        else { number[2] = "''"; }
        //Tìm mutil select
        if (mod.mutil_select == true)
        {
            number[3] = "true";
        }
        else { number[3] = "false"; }
        //Tìm doublick
        if (mod.double_click != null)
        {
            string strReplace = mod.double_click.Replace(mod.ad_module_id, "");
            var cas = cases.Where(s => s.ad_case_id == strReplace).Select(s => new { s.hamxuly, s.ma_case }).Take(1).FirstOrDefault();
            if (cas != null)
            {
                number[4] = cas.hamxuly.Replace("tengrid", "tengrid" + capmodule).Replace("id_parent", "id_parent" + capmodule)
                .Replace("Form_infor", "Form_infor" + capmodule).Replace("Model_infor", "Model_infor" + capmodule)
                .Replace("load_stt", "load_stt" + capmodule).Replace("ma_case", "'" + cas.ma_case + "'");
            }
            else
            {
                number[4] = "";
            }
        }
        else { number[4] = ""; }

        string str = "";
        str += "jQuery('#' + tengrid" + capmodule + ").jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });";
        str += "jQuery('#pager' + tengrid" + capmodule + " + '_left table').css('display', 'none');";
        str += "jQuery('#' + tengrid" + capmodule + ").jqGrid('setFrozenColumns');";
        str += "jQuery('#' + tengrid" + capmodule + ").jqGrid('bindKeys', {";
        str += "    \"onEnter\":function(rowid) {";
        str += "    }";
        str += "});";

        if (capmodule == 0)
            str += "\nformVisible_addEdit_id = formVisible_Del_id = tengrid" + capmodule + ";";

        number[5] = str;
        return number;
    }
}
