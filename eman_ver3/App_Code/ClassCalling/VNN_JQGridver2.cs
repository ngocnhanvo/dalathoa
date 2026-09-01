using DevExpress.XtraRichEdit.Commands;
using System;
using System.IO;
using System.Linq;


public static class VNN_JQGridver2
{
    public static string get_layout(System.Web.HttpContext context, string ma_module)
    {
        ADmin_JSON json = new ADmin_JSON();
        var modules = json.ad_moduleJSON();
        var role_mmcs = json.ad_role_mmcJSON();
        var user_mmcs = json.ad_user_mmcJSON();
        var mod = modules.Where(s => s.ma_module == ma_module).FirstOrDefault();

        string li_mod0 = "", li_mod1 = "", li_mod2 = "";
        string div_mod0 = "", div_mod1 = "", div_mod2 = "";
        string[] arr_mod0_tk = new string[100];
        string[] arr_mod1_tk = new string[100];
        int dem_mod0 = 0, dem_mod1 = 0;
        var hideModules = context.Request.Params["hideModules"].removeAllSpaceOrTrimText(false).Split(',');
        foreach (var mod_0 in modules.Where(s => s.ad_menu_id == mod.ad_menu_id & s.capmodule == 0 & s.hoatdong == true
        ).OrderBy(s => s.sapxep))
        {
            if (Security.PhanQuyen_Module(context, role_mmcs, user_mmcs, mod_0.ad_module_id))
            {
                if (mod_0.thuake != "")
                {
                    arr_mod0_tk[dem_mod0] = mod_0.thuake;
                    li_mod0 += $@"<li sel_mod='{mod_0.thuake}'><a style='cursor:pointer;' href=""#tabs_{mod_0.ma_module}"">{mod_0.ten_module}</a></li>";
                }
                else
                {
                    arr_mod0_tk[dem_mod0] = mod_0.ad_module_id;
                    li_mod0 += $@"<li sel_mod='{mod_0.ad_module_id}'><a style='cursor:pointer;' href=""#tabs_{mod_0.ma_module}"">{mod_0.ten_module}</a></li>";
                }
                div_mod0 += "<div id=\"tabs_" + mod_0.ma_module + "\">";
                div_mod0 += "<table id='grid" + mod_0.ma_module + "'></table>";
                div_mod0 += "<div id='pagergrid" + mod_0.ma_module + "'>";
                div_mod0 += "</div>";
                div_mod0 += "</div>";
                dem_mod0++;
            }
        }

        var firstMod1 = "";
        foreach (var mod_1 in modules.Where(s =>
            s.capmodule == 1 &
            !hideModules.Contains(s.ma_module) &
            arr_mod0_tk.Contains(s.ma_modulecha) &
            s.hoatdong == true
            ).OrderBy(s => s.sapxep))
        {
            if (Security.PhanQuyen_Module(context, role_mmcs, user_mmcs, mod_1.ad_module_id))
            {
                if (mod_1.thuake != "")
                {
                    arr_mod1_tk[dem_mod1] = mod_1.thuake;
                    li_mod1 += $@"<li sel_mod='{mod_1.thuake}' class='modcha_{mod_1.ma_modulecha}'><a style='cursor:pointer;' onclick=""load_detail(0,'{mod_1.ma_module}',0,'{mod_1.thuake}',1)"" href=""#tabs_{mod_1.ma_module}"">{mod_1.ten_module}</a></li>";
                }
                else
                {
                    arr_mod1_tk[dem_mod1] = mod_1.ad_module_id;
                    li_mod1 += $@"<li sel_mod='{mod_1.ad_module_id}' class='modcha_{mod_1.ma_modulecha}'><a style='cursor:pointer;' onclick=""load_detail(0,'{mod_1.ma_module}',0,'{mod_1.ad_module_id}',1)"" href=""#tabs_{mod_1.ma_module}"">{mod_1.ten_module}</a></li>";
                }
                firstMod1 = mod_1.ad_module_id;
                div_mod1 += $@"<div class='modcha_{mod_1.ma_modulecha}' id=""tabs_{mod_1.ma_module}""></div>";
                dem_mod1++;
            }
        }

        var firstMod2 = "";
        foreach (var mod_2 in modules.Where(s =>
            s.capmodule == 2 &
            arr_mod1_tk.Contains(s.ma_modulecha) &
            !hideModules.Contains(s.ma_module) &
            s.hoatdong == true
            ).OrderBy(s => s.sapxep))
        {
            if (Security.PhanQuyen_Module(context, role_mmcs, user_mmcs, mod_2.ad_module_id))
            {
                if (mod_2.ma_modulecha == firstMod1)
                    firstMod2 = mod_2.ma_modulecha;

                li_mod2 += $@"<li sel_mod='{mod_2.ad_module_id}' class='modcha_{mod_2.ma_modulecha}'><a style='cursor:pointer;' mod_parent='{mod_2.ma_modulecha}' onclick=""load_detail(0,'{mod_2.ma_module}',0,'{mod_2.ad_module_id}',2)"" href=""#tabs_{mod_2.ma_module}"">{mod_2.ten_module}</a></li>";
                div_mod2 += $@"<div class='modcha_{mod_2.ma_modulecha}' id=""tabs_{mod_2.ma_module}""></div>";
            }
        }

        var countFirstLoad = !string.IsNullOrEmpty(firstMod2) ? 3 : (!string.IsNullOrEmpty(firstMod1) ? 2 : 1);
        string kq = "";
        //lay out 1
        kq += "<div class='ui-layout-north ui-widget-content' style='overflow:hidden' id='div_getdt_0'>";
        kq += "<ul class='ul_mod_0'>";
        kq += li_mod0;
        kq += "<li onclick=\"click_rutgon_div(0)\" class='btnResizeGrid'><span class='btn_check_div0 glyphicon glyphicon-minus' style='font-size:14px; cursor:pointer' /></li>";
        kq += "</ul>";
        kq += div_mod0;
        kq += "</div>";
        //lay out 2
        kq += "<div class='ui-layout-center ui-widget-content' style='overflow:hidden' id='div_getdt_1' sizeTH1='50,49,1' sizeTH2='34,33,33' count='" + countFirstLoad + "'>";
        kq += "<ul class='ul_mod_1'>";
        kq += li_mod1;
        kq += "<li onclick=\"click_rutgon_div(1)\" class='btnResizeGrid'><span class='btn_check_div1 glyphicon glyphicon-minus' style='font-size:14px; cursor:pointer' /></li>";
        kq += "</ul>";
        kq += div_mod1;
        kq += "</div>";
        //lay out 3
        kq += "<div class='ui-layout-south ui-widget-content' style='overflow:hidden' id='div_getdt_2'>";
        kq += "<ul class='ul_mod_2'>";
        kq += li_mod2;
        kq += "<li onclick=\"click_rutgon_div(2)\" class='btnResizeGrid'><span class='btn_check_div2 glyphicon glyphicon-minus' style='font-size:14px; cursor:pointer' /></li>";
        kq += "</ul>";
        kq += div_mod2;
        kq += "</div>";
        return kq;
    }

    public static string get_layout_face(int lv = 0)
    {
        string kq = "";
        if (lv == 0)
        {
            kq += "$('#input_docaogrid').val(102);";
            kq += "$('.ui-layout-north').tabs();\n";
            kq += "$('.ui-layout-center').tabs();\n";
            kq += "$('.ui-layout-south').tabs();\n";
            kq += "$('.ul_mod_1').dragScroll();$('.ul_mod_2').dragScroll();\n";
            kq += "let countFirstLoad = $('#div_getdt_1').attr('count');\n";

            kq += "var sizeNorth, sizeSouth;\n";
            kq += "var getLayoutSize = layoutSize.filter(function(a){ return a.module == getModuleCodeFromSpanSelect() })[0];\n";
            kq += "switch(countFirstLoad) {\n";
            kq += "     case '2': sizeNorth = getLayoutSize == null ? 49 : getLayoutSize.size1[0];\n";
            kq += "               sizeSouth = getLayoutSize == null ? 1 : getLayoutSize.size1[2]; break;\n";
            kq += "     case '1': sizeNorth = 99;\n";
            kq += "               sizeSouth = 1; break;";
            kq += "     default: sizeNorth = getLayoutSize == null ? 34 : getLayoutSize.size2[0];\n";
            kq += "              sizeSouth = getLayoutSize == null ? 33 : getLayoutSize.size2[2]; break;\n";
            kq += "}\n";
        }
        kq += File.ReadAllText(ExcuteSignalRStatic.mapPathSignalR("~/App_Data/TempCode/get_layout_face.js"));
        //kq += "var layout_vnn = $('#div_getdt_1').parent().layout({\n";
        //kq += "	north: {\n";
        //kq += "		size: sizeNorth + '%',\n";
        //kq += "		minSize: '0%',\n";
        //kq += "		maxSize: '100%',\n";
        //kq += "		spacing_open: 1,\n";
        //kq += "		spacing_closed: 1,\n";
        //kq += "		onresize_end: function() {\n";
        //kq += "			let o = getHeightGrid(0);\n";
        //kq += "			$('#' + tengrid0).setGridHeight(o);\n";
        //kq += "		}\n";
        //kq += "	},\n";
        //kq += "	center: {\n";
        //kq += "		onresize_end: function() {\n";
        //kq += "			let o = getHeightGrid(1);\n";
        //kq += "			$('#' + tengrid1).setGridHeight(o);\n";
        //kq += "		}\n";
        //kq += "	},\n";
        //kq += "	south: {\n";
        //kq += "		size: sizeSouth + '%',\n";
        //kq += "		minSize: '0%',\n";
        //kq += "		maxSize: '100%',\n";
        //kq += "		spacing_open: 1,\n";
        //kq += "		spacing_closed: 1,\n";
        //kq += "		onresize_end: function() {\n";
        //kq += "			let o = getHeightGrid(2);\n";
        //kq += "			$('#' + tengrid2).setGridHeight(o);\n";
        //kq += "		}\n";
        //kq += "	}\n";
        //kq += "});\n";
        return kq;
    }
}
