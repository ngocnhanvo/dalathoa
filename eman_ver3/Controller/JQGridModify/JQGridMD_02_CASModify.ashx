<%@ WebHandler Language="C#" Class="JQGridMD_02_CASModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;
public class JQGridMD_02_CASModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private class CaseRole
    {
        public string text { get; set; }
        public bool? selected { get; set; }
        public int? index { get; set; }
    }

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
        EntityContext db = new EntityContext();
        List<ad_case> lstAdCase = json.ad_caseJSON();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            //sort
            string id = context.Request.Form["id_parent2"];
            ad_module mod = db.ad_module.Where(p => p.ad_module_id == id).FirstOrDefault();
            string ad_case_id = id_new;
            string ma_case = context.Request.Form["ma_case"].removeAllSpaceOrTrimText(false);
            string ten_case = context.Request.Form["ten_case"].removeAllSpaceOrTrimText(true);
            string hamxuly = context.Request.Form["hamxuly"].removeAllSpaceOrTrimText(false);
            string logo = context.Request.Form["logo"];
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string thuake = context.Request.Form["thuake"];
            string ma_moduletk = "";
            string ma_casetk = "";
            bool isview = bool.Parse(context.Request.Form["isview"]);
            bool id_parent = bool.Parse(context.Request.Form["id_parent"]);
            string dodaiForm = context.Request.Form["dodaiForm"];
            string docaoForm = context.Request.Form["docaoForm"];
            bool canhgiua = bool.Parse(context.Request.Form["canhgiua"]);
            bool hidden_modify = bool.Parse(context.Request.Form["hidden_modify"]);
            string tieude = context.Request.Form["tieude"];
            ad_case cn_checkhxl = db.ad_case.Where(s => s.hamxuly == hamxuly).FirstOrDefault();
            //#sort
            if (ma_case == "" | ma_case == null)
            {
                ma_case = "CA_00_" + DateTime.Now.ToString("ddMMyyyyhhmmssffftt");
            }

            if (db.ad_case.SingleOrDefault(p => p.ma_case == (ma_case)) != null)
            {
                msg = "false#Mã case này đã tồn tại.";
            }
            else if (hamxuly == "" & thuake == "")
            {
                msg = "false#Không thể bỏ trống hàm xử lý.";
            }
            else if (cn_checkhxl != null & thuake == "")
            {
                if (!cn_checkhxl.hamxuly.Contains("click_add(tengrid)") &
                    !cn_checkhxl.hamxuly.Contains("click_edit(tengrid)") &
                    !cn_checkhxl.hamxuly.Contains("click_del(tengrid)") &
                    !cn_checkhxl.hamxuly.Contains("click_view(tengrid)")
                    )
                    msg = "false#Hàm đã được khai báo trong Menu:" + cn_checkhxl.ma_menu + " - Module:" + cn_checkhxl.ma_module + " - Case:" + cn_checkhxl.ma_case;
            }
            else if (mod == null)
            {
                msg = "false#Không tìm thấy module.";
            }
            else if (mod.thuake != null & mod.thuake != "" & thuake == "")
            {
                var mod_tk = db.ad_module.Where(s => s.ad_module_id == mod.thuake).Select(s => new { s.ten_module, s.ma_module }).Take(1).FirstOrDefault();
                msg = "false#Không thể tạo mới, Module này đang thừa kế từ module: " + mod_tk.ten_module + " - (" + mod_tk.ma_module + ")";
            }

            if (msg.Length <= 0)
            {
                ad_case cn = null;
                if (thuake != "" & thuake != null)
                {
                    ad_case cn_thuake = db.ad_case.Where(p => p.ad_case_id == thuake).Take(1).FirstOrDefault();
                    if (cn_thuake != null)
                    {
                        if (cn_thuake.thuake != "" & cn_thuake.thuake != null)
                        {
                            msg = "false#Không thể thừa kế 1 chức năng đang phải thừa kế 1 chức năng khác.";
                        }
                        else
                        {
                            ten_case = cn_thuake.ten_case;
                            hamxuly = cn_thuake.hamxuly;
                            logo = cn_thuake.logo;

                            isview = cn_thuake.isview.Value;
                            dodaiForm = cn_thuake.dodaiForm;
                            docaoForm = cn_thuake.docaoForm;
                            canhgiua = cn_thuake.canhgiua.Value;
                            tieude = cn_thuake.tieude;
                        }
                        ma_moduletk = cn_thuake.ma_module;
                        ma_casetk = cn_thuake.ma_case;
                    }
                }
                if (msg.Length <= 0)
                {
                    cn = new ad_case
                    {
                        ad_case_id = ad_case_id,
                        ad_module_id = id,
                        ma_menu = mod.ma_menu,
                        ma_module = mod.ma_module,
                        ma_case = ma_case,
                        ten_case = ten_case,
                        hamxuly = hamxuly,
                        logo = logo,
                        sapxep = sapxep,
                        thuake = thuake,

                        isview = isview,
                        dodaiForm = dodaiForm,
                        docaoForm = docaoForm,
                        canhgiua = canhgiua,
                        tieude = tieude,
                        hidden_modify = hidden_modify,

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
                    db.ad_case.Add(cn);
                    db.SaveChanges();

                    string id_taikhoan = Security.id_taikhoan(context);
                    ad_user tk = db.ad_user.Where(p => p.ad_user_id == id_taikhoan).Take(1).FirstOrDefault();
                    if (ma_casetk == "")
                        VNN_Function.ThemPhanQuyen(context, Security.id_vaitro(context), mod.ad_menu_id, mod.ad_module_id, ad_case_id);

                    VNN_Function.SortColumn("ad_case", sapxep, "ad_module_id", id, "ma_case", ma_case, null);
                    System.Threading.Thread.Sleep(500);
                    var addCas = 0;
                    foreach (ad_module mod_thuake in db.ad_module.Where(s => s.thuake == mod.ad_module_id))
                    {
                        addCas++;
                        ad_case cn_tk = new ad_case
                        {
                            ad_case_id = Helper.getNewId(),
                            ad_module_id = mod_thuake.ad_module_id,
                            ma_case = "CA_01_" + DateTime.Now.AddSeconds(addCas).ToString("ddMMyyyyhhmmssffftt"),
                            ma_menu = mod_thuake.ma_menu,
                            ma_module = mod_thuake.ma_module,
                            thuake = ad_case_id,
                            sapxep = sapxep,

                            ten_case = ten_case,
                            hamxuly = hamxuly,
                            logo = logo,
                            isview = isview,
                            id_parent = id_parent,
                            dodaiForm = dodaiForm,
                            docaoForm = docaoForm,
                            canhgiua = canhgiua,
                            tieude = tieude,
                            hidden_modify = hidden_modify,

                            nguoitao = Security.id_taikhoan(context),
                            vaitrotao = Security.id_vaitro(context),
                            bophantao = Security.id_phongban(context),
                            nguoicapnhat = Security.id_taikhoan(context),
                            vaitrocapnhat = Security.id_vaitro(context),
                            bophancapnhat = Security.id_phongban(context),
                            ngaytao = DateTime.Now,
                            ngaycapnhat = DateTime.Now,
                            mota = cn.mota,
                            hoatdong = cn.hoatdong,
                        };
                        db.ad_case.Add(cn_tk);
                        lstAdCase.Add(cn_tk);
                    }
                    db.SaveChanges();
                    Create_Case(context, hamxuly, mod.ma_module, ma_case, ma_moduletk, ma_casetk);
                    VNN_Function.loaddulieu_Auto(db, ma_module);
                    db.Dispose();
                    msg = "true#Thêm mới thành công" + "#" + id_new;
                }
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
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id_ = context.Request.Form["id"];
            ad_case cn = db.ad_case.Where(p => p.ad_case_id == id_).Take(1).FirstOrDefault();
            ad_module mod = db.ad_module.Where(p => p.ad_module_id == cn.ad_module_id).FirstOrDefault();
            //sort
            string id = cn.ad_module_id;
            string ma_case = cn.ma_case;
            string ten_case = context.Request.Form["ten_case"];
            string hamxuly_old = cn.hamxuly;
            string hamxuly = context.Request.Form["hamxuly"];
            string logo = context.Request.Form["logo"];
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string thuake = context.Request.Form["thuake"];
            string ma_moduletk = "";
            string ma_casetk = "";
            string id_casetk_old = cn.thuake;
            bool isview = bool.Parse(context.Request.Form["isview"]);
            bool id_parent = bool.Parse(context.Request.Form["id_parent"]);
            bool hidden_modify = bool.Parse(context.Request.Form["hidden_modify"]);
            string dodaiForm = context.Request.Form["dodaiForm"];
            string docaoForm = context.Request.Form["docaoForm"];
            bool canhgiua = bool.Parse(context.Request.Form["canhgiua"]);
            string tieude = context.Request.Form["tieude"];
            ad_case cn_checkhxl = db.ad_case.Where(s => s.hamxuly == (hamxuly) & (s.thuake == null | s.thuake == "")).FirstOrDefault();
            //#sort
            if (cn == null)
            {
                msg = "false#Không tìm thấy đối tượng.";
            }
            else if (hamxuly == "" & thuake == "")
            {
                msg = "false#Không thể bỏ trống hàm xử lý.";
            }
            else if (cn_checkhxl != null & thuake == "")
            {
                if (!cn_checkhxl.hamxuly.Contains("click_add(tengrid)") &
                     !cn_checkhxl.hamxuly.Contains("click_edit(tengrid)") &
                     !cn_checkhxl.hamxuly.Contains("click_del(tengrid)") &
                     !cn_checkhxl.hamxuly.Contains("click_view(tengrid)") &
                      cn_checkhxl.ad_case_id != cn.ad_case_id
                     )
                    msg = "false#Hàm đã được khai báo trong Menu:" + cn_checkhxl.ma_menu + " - Module:" + cn_checkhxl.ma_module + " - Case:" + cn_checkhxl.ma_case;
            }
            else if (mod == null)
            {
                msg = "false#Không tìm thấy module.";
            }
            else if (mod.thuake != null & mod.thuake != "" & thuake == "")
            {
                var mod_tk = db.ad_module.Where(s => s.ad_module_id == (mod.thuake)).Select(s => new { s.ten_module, s.ma_module }).Take(1).FirstOrDefault();
                msg = "false#Không thể tạo mới, Module này đang thừa kế từ module: " + mod_tk.ten_module + " - (" + mod_tk.ma_module + ")";
            }
            else if (thuake != "" & thuake != null)
            {
                ad_case cn_thuake = db.ad_case.Where(p => p.ad_case_id == (thuake)).Take(1).FirstOrDefault();
                if (cn_thuake != null)
                {
                    if (cn_thuake.thuake != "" & cn.thuake != null & (cn.thuake != thuake))
                    {
                        msg = "false#Không thể thừa kế 1 chức năng đang phải thừa kế 1 chức năng khác.";
                    }
                    else
                    {
                        ten_case = cn_thuake.ten_case;
                        hamxuly = cn_thuake.hamxuly;
                        logo = cn_thuake.logo;

                        isview = cn_thuake.isview.Value;
                        dodaiForm = cn_thuake.dodaiForm;
                        docaoForm = cn_thuake.docaoForm;
                        canhgiua = cn_thuake.canhgiua.Value;
                        tieude = cn_thuake.tieude;
                    }
                    ma_moduletk = cn_thuake.ma_module;
                    ma_casetk = cn_thuake.ma_case;
                }
            }
            if (msg.Length <= 0)
            {
                cn.ten_case = ten_case;
                cn.hamxuly = hamxuly;
                cn.logo = logo;
                cn.thuake = thuake;
                cn.sapxep = sapxep;
                cn.hidden_modify = hidden_modify;

                cn.isview = isview;
                cn.id_parent = id_parent;
                cn.dodaiForm = dodaiForm;
                cn.docaoForm = docaoForm;
                cn.canhgiua = canhgiua;
                cn.tieude = tieude;

                cn.nguoicapnhat = Security.id_taikhoan(context);
                cn.vaitrocapnhat = Security.id_vaitro(context);
                cn.bophancapnhat = Security.id_phongban(context);
                cn.ngaycapnhat = DateTime.Now;
                cn.mota = context.Request.Form["mota"];
                cn.hoatdong = bool.Parse(context.Request.Form["hoatdong"]);
                db.SaveChanges();
                msg = "true#Cập nhật thành công";
                VNN_Function.SortColumn("ad_case", sapxep, "ad_module_id", id, "ma_case", ma_case, null);

                foreach (ad_case cn_con in db.ad_case.Where(s => s.thuake == cn.ad_case_id))
                {
                    cn_con.ten_case = ten_case;
                    cn_con.hidden_modify = hidden_modify;
                    cn_con.hamxuly = hamxuly;
                    cn_con.logo = logo;
                    cn_con.isview = isview;
                    cn_con.dodaiForm = dodaiForm;
                    cn_con.docaoForm = docaoForm;
                    cn_con.canhgiua = canhgiua;
                    cn_con.tieude = tieude;
                    if (context.Request.Form["updatehd"] == "HD_ALL")
                    {
                        cn_con.hoatdong = cn.hoatdong;
                    }
                    cn_con.nguoicapnhat = Security.id_taikhoan(context);
                    cn_con.vaitrocapnhat = Security.id_vaitro(context);
                    cn_con.bophancapnhat = Security.id_phongban(context);
                    cn_con.ngaycapnhat = DateTime.Now;
                }
                db.SaveChanges();
                if (mod.thuake == null)
                    Edit_Case(context, hamxuly, hamxuly_old, cn.ma_module, ma_case, ma_moduletk, ma_casetk, id_casetk_old);

                VNN_Function.loaddulieu_Auto(db, ma_module);
                db.Dispose();
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.ToString();
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        ADmin_JSON json = new ADmin_JSON();
        EntityContext db = new EntityContext();
        List<ad_case> LstAdCase = json.ad_caseJSON();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_case cn = db.ad_case.Where(p => p.ad_case_id == id).FirstOrDefault();

            if (cn != null)
            {
                if (msg.Length <= 0)
                {
                    VNN_Function.XoaPhanQuyen(context, cn.ad_module_id, cn.ad_case_id);
                    db.ad_case.Remove(cn);
                    Del_Case(context, cn.ma_module, cn.ma_case);
                    VNN_Function.loaddulieu_Auto(db, ma_module);
                    db.Dispose();
                    LstAdCase = LstAdCase.Where(s => s.ad_case_id != cn.ad_case_id).ToList();
                    string jsonData = JsonConvert.SerializeObject(LstAdCase, Formatting.Indented);
                    json.urlData = typeof(ad_case).Name;
                    json.WriteJson(jsonData);
                }
            }
        }
        catch (Exception ex)
        {
            msg += "false#Lỗi: " + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void LoadList(HttpContext context)
    {
        var json = new ADmin_JSON();
        var cases = json.ad_caseJSON();
        var role_mmcs = json.ad_role_mmcJSON();
        var dataJS = new List<CaseRole>();
        dataJS.Add(new CaseRole() {
            text = "Tất cả",
            selected = false,
            index = 0
        });

        string id = context.Request.QueryString["id"];
        string ad_module_id = context.Request.QueryString["ad_module_id"];

        int i = 1;
        foreach (var cn in cases.Where(s =>
            s.ad_module_id == ad_module_id
            & string.IsNullOrEmpty(s.thuake)
            & s.hoatdong == true
            ).OrderBy(p => p.sapxep))
        {
            var itemCas = new CaseRole();
            itemCas.text = "<a style='display:none'>" + cn.ad_case_id + "</a><a>" + cn.ten_case + "</a>";
            itemCas.selected = false;
            itemCas.index = i;
            foreach (ad_role_mmc pq in role_mmcs.Where(p => p.ad_module_id == cn.ad_module_id & p.ad_role_id == id))
            {
                if (pq.ad_case_id == cn.ad_case_id)
                    itemCas.selected = true;
            }
            dataJS.Add(itemCas);
            i++;
        }
        context.Response.Write(JsonConvert.SerializeObject(dataJS));
    }

    public void Create_Case(HttpContext context, string hamxuly, string ma_module, string ma_case, string ma_moduletk, string ma_casetk)
    {
        if (hamxuly != "click_add(tengrid)" &
            hamxuly != "click_edit(tengrid)" &
            hamxuly != "click_del(tengrid)" &
            hamxuly != "click_view(tengrid)" &
            hamxuly != null & hamxuly != "" &
            hamxuly != "TenHam(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt)"
            )
        {
            string filepath = ExcuteSignalRStatic.mapPathSignalR($"~/js/Module_script/{ma_module}.js");
            string w = System.IO.File.ReadAllText(filepath, System.Text.Encoding.Unicode);
            string str_new = "";

            if (ma_casetk != "")
            {
                str_new = "//Add function at here (don't remove this line, please)";
                str_new += $"\n\n//start {ma_case}";
                str_new += $"\n////{ma_moduletk}.js -> function {ma_casetk}";
                str_new += $"\n//end {ma_case}\n";
            }
            else
            {
                var temp = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/TempCode/CASE.js");
                var contentTemp =  System.IO.File.ReadAllText(temp,  System.Text.Encoding.Unicode);
                var content = contentTemp;
                content = content.Replace("ZzmoduleCasezZ", ma_case);
                content = content.Replace("ZzmodulezZ", ma_module);
                str_new = content;
            }

            string str_replace = "//Add function at here (don't remove this line, please)";
            w = w.Replace(str_replace, str_new);
            System.IO.File.WriteAllText(filepath, w, System.Text.Encoding.Unicode);
        }
    }

    public void Edit_Case(HttpContext context, string hamxuly, string hamxuly_old, string ma_module, string ma_case, string ma_moduletk, string ma_casetk, string id_casetk_old)
    {
        string filepath = Security.UrlBase() + "js/Module_script/" + ma_module + ".js";
        filepath = context.Server.MapPath(filepath);
        string str_start = "//start " + ma_case;
        string str_end = "//end " + ma_case;
        string w = System.IO.File.ReadAllText(filepath, System.Text.Encoding.Unicode);
        string str_replace = str_start + VNN_Function.FindString(w, str_start, str_end) + str_end;
        string str_new = "";
        if (ma_casetk == "")
        {
            if (id_casetk_old == "")
            {
                str_new = str_start + VNN_Function.FindString(w, str_start, str_end).Replace(hamxuly_old, hamxuly) + str_end;
            }
            else
            {
                Del_Case(context, ma_module, ma_case);
                Create_Case(context, hamxuly, ma_module, ma_case, ma_moduletk, ma_casetk);
            }
        }
        else
        {
            str_new = str_start + "\n////" + ma_moduletk + ".js -> function " + ma_casetk + "\n" + str_end + "\n";
        }
        w = w.Replace(str_replace, str_new);
        System.IO.File.WriteAllText(filepath, w, System.Text.Encoding.Unicode);
    }

    public void Del_Case(HttpContext context, string ma_module, string ma_case)
    {
        string filepath = Security.UrlBase() + "js/Module_script/" + ma_module + ".js";
        filepath = context.Server.MapPath(filepath);
        string str_start = "//start " + ma_case;
        string str_end = "//end " + ma_case;
        string w = System.IO.File.ReadAllText(filepath, System.Text.Encoding.Unicode);
        var strRPsav = str_start + VNN_Function.FindString(w, str_start, str_end) + str_end;
        string str_replace = "\n\n" + strRPsav;
        w = w.Replace(str_replace, null);
        str_replace = "\n" + strRPsav;
        w = w.Replace(str_replace, null);
        str_replace = strRPsav;
        w = w.Replace(str_replace, null);
        System.IO.File.WriteAllText(filepath, w, System.Text.Encoding.Unicode);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}