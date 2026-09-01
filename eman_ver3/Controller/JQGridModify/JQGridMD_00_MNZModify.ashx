<%@ WebHandler Language="C#" Class="JQGridMD_00_MNZModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;

public class JQGridMD_00_MNZModify : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    private class MenuRole
    {
        public string icon { get; set; }
        public string label { get; set; }
        public bool? expanded { get; set; }
        public bool? selected { get; set; }
        public List<MenuRole> items { get; set; }
    }

    EntityContext db = new EntityContext();
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
            case "loadMenu":
                this.LoadMenu(context);
                break;
            case "loadlist":
                this.LoadList(context);
                break;
            default:
                break;
        }
    }

    public void add(HttpContext context)
    {
        ADmin_JSON json = new ADmin_JSON();
        List<ad_menu> lstAdMenu = json.ad_menuJSON();
        List<ad_module> lstAdModule = json.ad_moduleJSON();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id_taikhoan = Security.id_taikhoan(context);
            ad_user tk_dangnhap = db.ad_user.Where(p => p.ad_user_id == id_taikhoan).FirstOrDefault();
            string ma_menu = context.Request.Form["ma_menu"];
            string capmenu = context.Request.Form["capmenu"];
            string ma_menucha = context.Request.Form["ma_menucha"];
            string loai = context.Request.Form["loai_menu"];
            string ma_module_ = ma_menu.Replace("MN_01", "MD_00");
            string url = context.Request.Form["url"];
            bool taomodule = bool.Parse(context.Request.Form["taomodule"]);
            if (taomodule.Equals(true))
            {
                url = "View/Menu/Content/Module/" + ma_module_ + ".aspx";
            }
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);

            if (ma_menu == "" | ma_menu == null)
            {
                ma_menu = "MN_0" + capmenu + "_" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
            }
            else if (ma_menu.Length < 6)
            {
                msg = "false#0Mã menu phải bắt đầu bằng: " + "MN_0" + capmenu + "_";
            }
            else if (!ma_menu.Substring(0, 6).Contains("MN_0" + capmenu + "_"))
            {
                msg = "false#1Mã menu phải bắt đầu bằng: " + "MN_0" + capmenu + "_";
            }

            if (db.ad_menu.SingleOrDefault(p => p.ma_menu.Equals(ma_menu)) != null)
            {
                msg = "false#Mã menu này đã tồn tại";
            }
            else if (capmenu == "0" & ma_menucha.Length > 0)
            {
                msg = "false#Cấp menu này là cấp cao nhất, không thể có menu cha.";
            }
            else if (capmenu == "1" & ma_menucha.Length <= 0)
            {
                msg = "false#Cấp menu này cần có menu cha.";
            }

            if (msg.Length <= 0)
            {
                ad_menu mn = new ad_menu
                {
                    ad_menu_id = id_new,
                    ma_menu = ma_menu,
                    ten_menu = context.Request.Form["ten_menu"],
                    url = url,
                    ma_menucha = ma_menucha,
                    capmenu = int.Parse(capmenu),
                    logo = context.Request.Form["logo"],
                    sapxep = sapxep,
                    loai_menu = loai,

                    nguoitao = Security.id_taikhoan(context),
                    vaitrotao = Security.id_vaitro(context),
                    bophantao = Security.id_phongban(context),
                    nguoicapnhat = Security.id_taikhoan(context),
                    vaitrocapnhat = Security.id_vaitro(context),
                    bophancapnhat = Security.id_phongban(context),
                    ngaytao = DateTime.Now,
                    ngaycapnhat = DateTime.Now,
                    mota = context.Request.Form["mota"],
                    hoatdong = true,
                };
                db.ad_menu.Add(mn);
                db.SaveChanges();

                lstAdMenu.Add(mn);
                string jsonData = JsonConvert.SerializeObject(lstAdMenu, Formatting.Indented);
                json.urlData = typeof(ad_menu).Name;
                json.WriteJson(jsonData);

                if (taomodule.Equals(true))
                {
                    string str_ad_module_id = Helper.getNewId();
                    ad_module mod = new ad_module
                    {
                        ad_module_id = str_ad_module_id,
                        ad_menu_id = id_new,
                        ma_module = ma_module_,
                        ma_menu = mn.ma_menu,
                        loai_module = loai,
                        select_sql = "",
                        from_sql = "",
                        Join_sql = "",
                        where_sql = "",
                        orderby_sql = "",
                        thuake = "",
                        row_count = false,
                        ten_module = context.Request.Form["ten_menu"],
                        url = url,
                        capmodule = 0,
                        ma_modulecha = " ",
                        sapxep = VNN_Config.load_number("0", 10),

                        nguoitao = Security.id_taikhoan(context),
                        vaitrotao = Security.id_vaitro(context),
                        bophantao = Security.id_phongban(context),
                        nguoicapnhat = Security.id_taikhoan(context),
                        vaitrocapnhat = Security.id_vaitro(context),
                        bophancapnhat = Security.id_phongban(context),
                        ngaytao = DateTime.Now,
                        ngaycapnhat = DateTime.Now,
                        mota = String.Format("tự sinh ra từ menu có mã là \"{0}\"", ma_menu),
                        hoatdong = true
                    };
                    db.ad_module.Add(mod);
                    db.SaveChanges();
                    lstAdModule.Add(mod);

                    jsonData = JsonConvert.SerializeObject(lstAdModule, Formatting.Indented);
                    json.urlData = typeof(ad_module).Name;
                    json.WriteJson(jsonData);

                    VNN_Function.ThemChucNang(context, ma_menu, ma_module_, str_ad_module_id);
                    System.Threading.Thread.Sleep(500);
                    VNN_Function.ThemPhanQuyen(context, Security.id_vaitro(context), id_new, str_ad_module_id, null);

                    Module_TK mod_ = VNN_Config.get_ModuleKeThua(mod, 0, ma_module_, "", "", db);
                    if (loai == "JQG")
                    { msg = Admin_CreateBasicFile.CreateFileLoad_Modify(context, mod_, 0); }
                    else
                    { msg = Admin_CreateBasicFile.CreateFileLoad_Modify(context, mod_, 0); }
                }
                VNN_Function.SortColumn("ad_menu", sapxep, null, null, "ma_menu", ma_menu, ma_menucha);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                db.Dispose();
                msg = "true#Tạo mới Menu thành công. " + msg;
                msg += "#" + id_new;
            }
        }
        catch (Exception ex)
        {
            msg = String.Format("false#" + ex.Message);
        }
        context.Response.Write(msg);
    }


    public void edit(HttpContext context)
    {
        ADmin_JSON json = new ADmin_JSON();
        List<ad_menu> LstAdMenu = json.ad_menuJSON();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_menu mn = db.ad_menu.Where(p => p.ad_menu_id == id).FirstOrDefault();
            string ma_menu = mn.ma_menu;
            string ma_menucha = context.Request.Form["ma_menucha"];
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string capmenu = context.Request.Form["capmenu"];
            if (capmenu == "0" & ma_menucha.Length > 0)
            {
                msg = "false#Cấp menu này là cấp cao nhất, không thể có menu cha.";
            }
            else if (capmenu == "1" & ma_menucha.Length <= 0)
            {
                msg = "false#Cấp menu này cần có menu cha.";
            }
            else if (mn != null)
            {
                mn.ten_menu = context.Request.Form["ten_menu"];
                mn.url = context.Request.Form["url"];
                mn.ma_menucha = ma_menucha;
                mn.capmenu = int.Parse(capmenu);
                mn.logo = context.Request.Form["logo"];
                mn.sapxep = sapxep;
                mn.nguoicapnhat = Security.id_taikhoan(context);
                mn.vaitrocapnhat = Security.id_vaitro(context);
                mn.bophancapnhat = Security.id_phongban(context);
                mn.ngaycapnhat = DateTime.Now;
                mn.mota = context.Request.Form["mota"];
                mn.hoatdong = bool.Parse(context.Request.Form["hoatdong"]);
                db.SaveChanges();
                VNN_Function.SortColumn("ad_menu", sapxep, null, null, "ma_menu", ma_menu, ma_menucha);

                LstAdMenu = LstAdMenu.Where(s => s.ad_menu_id != mn.ad_menu_id).ToList();
                LstAdMenu.Add(mn);
                VNN_Function.loaddulieu_Auto(db, ma_module);

                string jsonData = JsonConvert.SerializeObject(LstAdMenu, Formatting.Indented);
                json.urlData = typeof(ad_menu).Name;
                json.WriteJson(jsonData);
                msg = "true#Cập nhật thành công";
            }
            else
            {
                msg = "false#Không tìm thấy đối tượng cần sửa.";
            }
        }
        catch (Exception ex)
        {
            msg = "false#Lỗi:" + ex.Message;
        }
        db.Dispose();
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        ADmin_JSON json = new ADmin_JSON();
        List<ad_menu> LstAdMenu = json.ad_menuJSON();
        List<ad_module> LstAdModule = json.ad_moduleJSON();
        List<ad_case> LstAdCase = json.ad_caseJSON();
        List<ad_column> LstAdColumn = json.ad_columnJSON();
        List<ad_role_mmc> LstAdRoleMMC = json.ad_role_mmcJSON();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_menu mn = db.ad_menu.Where(p => p.ad_menu_id == id).FirstOrDefault();
            if (mn != null)
            {

                //--Xoa phan quyen
                foreach (ad_role_mmc pq in db.ad_role_mmc.Where(s => s.ad_menu_id == mn.ad_menu_id).ToList())
                {
                    db.ad_role_mmc.Remove(pq);
                    LstAdRoleMMC = LstAdRoleMMC.Where(s => s.ad_role_mmc_id != pq.ad_role_mmc_id).ToList();
                }
                db.SaveChanges();
                //--Xoa chuc nang
                foreach (ad_case cn in db.ad_case.Where(s => s.ma_menu == mn.ma_menu).ToList())
                {
                    db.ad_case.Remove(cn);
                    LstAdCase = LstAdCase.Where(s => s.ad_case_id != cn.ad_case_id).ToList();
                }
                db.SaveChanges();
                //--Xoa column
                foreach (ad_column col in db.ad_column.Where(s => s.ma_menu == mn.ma_menu).ToList())
                {
                    db.ad_column.Remove(col);
                    LstAdColumn = LstAdColumn.Where(s => s.ad_column_id != col.ad_column_id).ToList();
                }
                db.SaveChanges();
                //Xoa module
                foreach (ad_module mod in db.ad_module.Where(s => s.ma_menu == mn.ma_menu).ToList())
                {
                    foreach (ad_module mod_tk in db.ad_module.Where(s => s.thuake.Equals(mod.ad_module_id)).ToList())
                    {
                        db.ad_module.Remove(mod);
                        LstAdModule = LstAdModule.Where(s => s.ad_module_id != mod_tk.ad_module_id).ToList();
                    }
                    Admin_CreateBasicFile.DeleteFileLoad_Modify(context, mod.ma_module, mod.url);
                    System.Threading.Thread.Sleep(100);
                    db.ad_module.Remove(mod);
                    LstAdModule = LstAdModule.Where(s => s.ad_module_id != mod.ad_module_id).ToList();
                }
                db.SaveChanges();
                System.Threading.Thread.Sleep(500);

                if (mn.ma_menucha == null)
                {
                    foreach (ad_menu mn_con in db.ad_menu.Where(s => s.ma_menucha.Equals(mn.ma_menu)).ToList())
                    {
                        //--Xoa phan quyen
                        foreach (ad_role_mmc pq in db.ad_role_mmc.Where(s => s.ad_menu_id == mn_con.ad_menu_id).ToList())
                        {
                            db.ad_role_mmc.Remove(pq);
                            LstAdRoleMMC = LstAdRoleMMC.Where(s => s.ad_role_mmc_id != pq.ad_role_mmc_id).ToList();
                        }
                        db.SaveChanges();
                        //--Xoa chuc nang
                        foreach (ad_case cn in db.ad_case.Where(s => s.ma_menu == mn_con.ma_menu).ToList())
                        {
                            db.ad_case.Remove(cn);
                            LstAdCase = LstAdCase.Where(s => s.ad_case_id != cn.ad_case_id).ToList();
                        }
                        db.SaveChanges();
                        //--Xoa column
                        foreach (ad_column col in db.ad_column.Where(s => s.ma_menu == mn_con.ma_menu).ToList())
                        {
                            db.ad_column.Remove(col);
                            LstAdColumn = LstAdColumn.Where(s => s.ad_column_id != col.ad_column_id).ToList();
                        }
                        db.SaveChanges();
                        //Xoa module
                        foreach (ad_module mod in db.ad_module.Where(s => s.ma_menu == mn_con.ma_menu).ToList())
                        {
                            foreach (ad_module mod_tk in db.ad_module.Where(s => s.thuake.Equals(mod.ad_module_id)).ToList())
                            {
                                db.ad_module.Remove(mod_tk);
                                LstAdModule = LstAdModule.Where(s => s.ad_module_id != mod_tk.ad_module_id).ToList();
                            }
                            Admin_CreateBasicFile.DeleteFileLoad_Modify(context, mod.ma_module, mod.url);
                            System.Threading.Thread.Sleep(100);
                            db.ad_module.Remove(mod);
                            LstAdModule = LstAdModule.Where(s => s.ad_module_id != mod.ad_module_id).ToList();
                        }
                        db.SaveChanges();
                        System.Threading.Thread.Sleep(500);
                        //--Xoa menu con
                        db.ad_menu.Remove(mn_con);
                        LstAdMenu = LstAdMenu.Where(s => s.ad_menu_id != mn_con.ad_menu_id).ToList();
                    }
                    db.SaveChanges();
                }

                //Xoa menu chinh thuc
                db.ad_menu.Remove(mn);
                LstAdMenu = LstAdMenu.Where(s => s.ad_menu_id != mn.ad_menu_id).ToList();
                db.SaveChanges();

                string jsonData = JsonConvert.SerializeObject(LstAdMenu, Formatting.Indented);
                json.urlData = typeof(ad_menu).Name;
                json.WriteJson(jsonData);

                jsonData = JsonConvert.SerializeObject(LstAdModule, Formatting.Indented);
                json.urlData = typeof(ad_module).Name;
                json.WriteJson(jsonData);

                jsonData = JsonConvert.SerializeObject(LstAdColumn, Formatting.Indented);
                json.urlData = typeof(ad_column).Name;
                json.WriteJson(jsonData);

                jsonData = JsonConvert.SerializeObject(LstAdCase, Formatting.Indented);
                json.urlData = typeof(ad_case).Name;
                json.WriteJson(jsonData);

                jsonData = JsonConvert.SerializeObject(LstAdRoleMMC, Formatting.Indented);
                json.urlData = typeof(ad_role_mmc).Name;
                json.WriteJson(jsonData);

                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Xóa thành công.";
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToUpper().Contains("REFERENCE"))
            {
                msg = "false#Menu này đang được sử dụng.";
            }
            else
            {
                msg = "false#Lỗi: " + ex.Message;
            }
        }
        db.Dispose();
        context.Response.Write(msg);
    }

    public void LoadMenu(HttpContext context)
    {
        string result_menu = "";
        string ad_user_id = Security.id_taikhoan(context);
        string ad_role_id = Security.id_vaitro(context);

        ADmin_JSON json = new ADmin_JSON();
        var menus = json.ad_menuJSON();
        var role_mmcs = json.ad_role_mmcJSON();
        var user_mmcs = json.ad_user_mmcJSON();
        user_mmcs = user_mmcs.Where(s => s.ad_user_id == ad_user_id)
                        .Where(s => string.IsNullOrEmpty(s.ad_role_id) | s.ad_role_id == ad_role_id).ToList();
        role_mmcs = role_mmcs.Where(s => s.ad_role_id == ad_role_id).ToList();

        var dt_menu0 = menus.Where(s => s.capmenu == 0 & s.hoatdong == true).OrderBy(s => s.sapxep);
        var dt_menu0_1 = menus.Where(s => s.capmenu == 1 & s.hoatdong == true
            & (role_mmcs.Where(s1 => s1.ad_menu_id == s.ad_menu_id).Take(1).Count() > 0 | user_mmcs.Where(s1 => s1.ad_menu_id == s.ad_menu_id).Take(1).Count() > 0)
        ).OrderBy(s => s.sapxep);
        foreach (var mn_cap0 in dt_menu0)
        {
            var dt_menu1 = dt_menu0_1.Where(s => s.ma_menucha == mn_cap0.ad_menu_id);
            if (dt_menu1.Take(1).Count() > 0)
            {
                //menu con
                var result_menucon = "";
                foreach (var mn_cap1 in dt_menu1)
                {
                    result_menucon += $@"
                        <tr 
                            id='tr_{mn_cap1.ma_menu}' 
                            class='menu-con-tr'
                        >
                            <td 
                                id='td_{mn_cap1.ma_menu}' 
                                onclick=""loadContent('{mn_cap1.ma_menu}','{mn_cap1.url}','{mn_cap1.ten_menu}','{mn_cap0.ten_menu}')""
                                onmouseover = ""overmouse_menu_lv1('td_{mn_cap1.ma_menu}')""
                                onmouseout = ""outmouse_menu_lv1('td_{mn_cap1.ma_menu}')""
                            >
                                <div>
                                    <table>
                                        <tr style='height:22px'>
                                            <td style='width: 20px'>{VNN_Function.set_Icon(mn_cap1.logo, "image_menu1", "", "", "")}</td>
                                            <td class='menu-con-td'>
                                                <span class='nhan_pre'>{mn_cap1.ten_menu}</span>
                                                <a id='a_{mn_cap1.ma_menu}' name='{mn_cap1.ma_module_count}'>&nbsp;&nbsp;</a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    ";
                }
                result_menucon = $@"<table style='display:none'>{result_menucon}</table>";
                //menu cha
                result_menu += $@"
                    <tr class='menu-cha-tr' id='tr_{mn_cap0.ma_menu}'>
                        <td>
                            <div 
                                style='padding:0 0 3 0' 
                                id='div_{mn_cap0.ma_menu}' 
                                onclick=""loadMenucon('{mn_cap0.ma_menu}','{mn_cap0.ten_menu}')""
                                onmouseover = ""overmouse_menu_lv0('tr_{mn_cap0.ma_menu}')""
                                onmouseout = ""outmouse_menu_lv0('tr_{mn_cap0.ma_menu}')""
                            >
                                <div class='logo-menu0'>
                                    {VNN_Function.set_Icon(mn_cap0.logo, "", "", "", "")}
                                </div>
                                <span class=""menu-cha"">{mn_cap0.ten_menu}</span>
                                <div class='logo-menu0 angle'>
                                    <i class=""fa fa-angle-right"" aria-hidden=""true""></i>
                                </div>
                            </div>
                            {result_menucon}
                        </td>
                    </tr>
                ";
            }
        }
        context.Response.Write(result_menu);
    }

    public void LoadList(HttpContext context)
    {
        var vtid = context.Request.QueryString["id"];
        var loadTheoVT = context.Request.QueryString["type"].removeAllSpaceOrTrimText(false) == "1";

        ADmin_JSON json = new ADmin_JSON();
        var menus = json.ad_menuJSON();

        var mn1coVTs = new List<string>();
        if(loadTheoVT)
        {
            var pqs = json.ad_role_mmcJSON();
            mn1coVTs = pqs.Where(s => s.ad_role_id == vtid).Select(s=>s.ad_menu_id).ToList();
        }

        var lst = new List<MenuRole>();
        foreach (var mn in menus.Where(p => p.capmenu == 0 & p.hoatdong == true).OrderBy(p => p.sapxep).ToList())
        {
            var itemMenu = new MenuRole();
            itemMenu.label = string.Format(@"<a style='display:none'>{0}</a><a style='font-weight:700'>{1}</a>", mn.ad_menu_id, mn.ten_menu);
            var mn_cap1s = menus.Where(p => p.capmenu == 1 & p.ma_menucha == mn.ad_menu_id & p.hoatdong == true);
            if(loadTheoVT)
            {
                mn_cap1s = mn_cap1s.Where(s => mn1coVTs.Contains(s.ad_menu_id));
            }

            foreach (var mn_cap1 in mn_cap1s.OrderBy(p => p.sapxep))
            {
                if (itemMenu.items == null)
                    itemMenu.items = new List<MenuRole>();

                itemMenu.items.Add(new MenuRole()
                {
                    label = string.Format(@"<a style='display:none'>{0}</a><a>{1}</a>", mn_cap1.ad_menu_id, mn_cap1.ten_menu)
                });
            }

            if(itemMenu.items != null)
                lst.Add(itemMenu);
        }
        context.Response.Write(JsonConvert.SerializeObject(lst));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
