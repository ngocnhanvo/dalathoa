   <%@ WebHandler Language="C#" Class="JQGridMD_00_EditStyleModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using System.IO;
using System.Text;
using DataAcess;
public class JQGridMD_00_EditStyleModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
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
            default:
                break;
        }
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.QueryString["id"];
            //sort
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string ma_editstyle = context.Request.Form["ma_editstyle"];
            string value_editoption = context.Request.Form["value_editoption"];
            //sort

            //duong dan
            string filepath_ = Security.UrlBase() + "js/VNN_script/User_GridFormatter.js";
            filepath_ = context.Server.MapPath(filepath_);
            //doc file
            string ww = File.ReadAllText(filepath_, Encoding.Unicode);
            string check_format = "js/Custom_Grid_script/" + ma_editstyle + ".js";
            //doc file

            if (db.ad_editstyle.Where(p => p.ma_editstyle.Equals(ma_editstyle)).FirstOrDefault() != null)
            {
                msg = "false#Mã editstyle này đã tồn tại";
            }

            if (value_editoption == "")
            {
                value_editoption = "dataInit: function (elem) { " + ma_editstyle + "(elem); }";
            }

            if (ww.Contains(check_format))
            {
                msg = "false#Mã " + ma_editstyle + " đã tồn tại";
            }

            if (msg.Length <= 0)
            {
                string ad_module_id = context.Request.Form["ad_module_id"];
                ad_module ad = db.ad_module.Where(s => s.ad_module_id == ad_module_id).FirstOrDefault();
                //start truyền các giá trị cần thêm
                ad_editstyle object_ = new ad_editstyle();
                object_.ad_editstyle_id = id_new;
                object_.ad_module_id = ad_module_id;
                object_.ma_module = ad.ma_module;
                object_.ma_editstyle = ma_editstyle;
                object_.value_editstyle = context.Request.Form["value_editstyle"];
                object_.value_formatter = context.Request.Form["value_formatter"];
                object_.value_formatoptions = context.Request.Form["value_formatoptions"];
                object_.value_editoption = value_editoption;
                object_.value_searchoptions = context.Request.Form["value_searchoptions"];
                object_.ten_editstyle = context.Request.Form["ten_editstyle"];
                object_.sapxep = sapxep;
                object_.ngaytao = DateTime.Now;
                object_.nguoitao = Security.id_taikhoan(context);
                object_.ngaycapnhat = DateTime.Now;
                object_.nguoicapnhat = Security.id_taikhoan(context);
                object_.mota = context.Request.Form["mota"];
                object_.hoatdong = true;
                //#end truyền các giá trị cần thêm
                db.ad_editstyle.Add(object_);
                db.SaveChanges();       

                VNN_Function.SortColumn("ad_editstyle", sapxep, null, null, "ma_editstyle", ma_editstyle, null);
                msg = "true#Thêm thành công" + "#" + id_new;

                //Them file
                Modify_file(context, ma_editstyle, object_.ten_editstyle, object_.ma_module, ad.ma_menu);

                //Them code
                string str_new = "//Add function at here (don't remove this line, please)";
                str_new += "\n//start " + object_.ten_editstyle;
                //str_new += "\nfunction " + object_.ma_editstyle + "(elem) {";
                str_new += "\n$.getScript(\"js/Custom_Grid_script/" + object_.ma_editstyle + ".js\");";
                str_new += "\n//end " + object_.ten_editstyle;
                string str_replace = "//Add function at here (don't remove this line, please)";
                ww = ww.Replace(str_replace, str_new);
                File.WriteAllText(filepath_, ww, Encoding.Unicode);
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
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_editstyle object_ = db.ad_editstyle.Where(p => p.ad_editstyle_id == id).FirstOrDefault();
            if (object_ != null)
            {
                //sort
                string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
                string ma_editstyle = object_.ma_editstyle;
                //#sort
                //start truyền các giá trị cần sửa
                object_.ad_module_id = context.Request.Form["ad_module_id"];
                object_.ma_module = context.Request.Form["ma_module"];
                object_.ma_editstyle = context.Request.Form["ma_editstyle"];
                object_.value_editstyle = context.Request.Form["value_editstyle"];
                object_.value_formatter = context.Request.Form["value_formatter"];
                object_.value_formatoptions = context.Request.Form["value_formatoptions"];
                object_.value_editoption = context.Request.Form["value_editoption"];
                object_.value_searchoptions = context.Request.Form["value_searchoptions"];
                object_.ten_editstyle = context.Request.Form["ten_editstyle"];
                object_.ngaycapnhat = DateTime.Now;
                object_.nguoicapnhat = Security.id_taikhoan(context);
                object_.mota = context.Request.Form["mota"];
                object_.sapxep = sapxep;
                object_.hoatdong = bool.Parse(context.Request.Form["hoatdong"]);
                db.SaveChanges();

                //#end truyền các giá trị cần sửa
                VNN_Function.loaddulieu_Auto(db, ma_module);
                VNN_Function.SortColumn("ad_editstyle", sapxep, null, null, "ma_editstyle", ma_editstyle, null);
                msg = "true#Cập nhật thành công";

                /*string ad_module_id = context.Request.Form["ad_module_id"];
                ad_module ad = db.ad_module.Where(s => s.ad_module_id == ad_module_id).FirstOrDefault();
                Modify_file(context, ma_editstyle, object_.ten_editstyle, ad.ma_module, ad.ma_menu);
                */
            }
            else
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
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
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_editstyle object_ = db.ad_editstyle.SingleOrDefault(p => p.ad_editstyle_id == id);
            if (object_ != null)
            {
                string filepath_ = Security.UrlBase() + "js/Custom_Grid_script/" + object_.ma_editstyle + ".js";
                string filepath_delete = context.Server.MapPath(filepath_);
                if (System.IO.File.Exists(filepath_delete))
                {
                    try
                    {
                        System.IO.File.Delete(filepath_delete);
                        //--remove line
                        string filepath = Security.UrlBase() + "js/VNN_script/User_GridFormatter.js";
                        filepath = context.Server.MapPath(filepath);
                        string str_start = "//start " + object_.ten_editstyle;
                        string str_end = "//end " + object_.ten_editstyle;

                        string w = File.ReadAllText(filepath, Encoding.Unicode);
                        string str_replace = str_start + VNN_Function.FindString(w, str_start, str_end) + str_end;
                        w = w.Replace(str_replace, "");
                        File.WriteAllText(filepath, w, Encoding.Unicode);
                        //--remove line
                    }
                    catch (IOException e)
                    {
                        context.Response.Write(e.Message);
                    }
                }
                db.ad_editstyle.Remove(object_);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Xóa thành công";
            }
            else
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần xóa ";
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

    public void Modify_file(HttpContext context, string ma_editstyle, string ten_editstyle, string ma_module, string ma_menu)
    {
        //Tạo file js
        string filepath = Security.UrlBase() + "js/" + "Custom_Grid_script";
        string filename = "/" + ma_editstyle + ".js";
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(context.Server.MapPath(filepath));
        }
        filepath = context.Server.MapPath(filepath + filename);
        //--Add line
        string colModel = "";
        foreach (ad_column ad_c in db.ad_column.Where(s => s.ma_module == ma_module & s.hoatdong == true).OrderBy(s => s.sapxep).ToList())
        {
            string key = "", _fixed = "", label = "", name = "", index = "", width = "", editable = "", hidden = "", formatter = "", unformat = "", align = "", sopt = "", editoptions = "", frozen = "", formoptions = "";
            if (ad_c.key_cot != null & ad_c.key_cot != "") { key = "key: " + ad_c.key_cot; }
            if (ad_c.@fixed != null & ad_c.@fixed != "") { _fixed = ",fixed: " + ad_c.@fixed; }
            if (ad_c.ten_column != null & ad_c.ten_column != "") { label = ",label: '" + ad_c.ten_column + "'"; }
            if (ad_c.ma_column != null & ad_c.ma_column != "") { name = ",name: '" + ad_c.ma_column + "'"; }
            if (ad_c.index_cot != null & ad_c.index_cot != "") { index = ",index: '" + ad_c.index_cot + "'"; }
            if (ad_c.width != null & ad_c.width != "") { width = ",width: " + ad_c.width + ""; }
            if (ad_c.editable != null & ad_c.editable != "") { editable = ",editable: " + ad_c.editable + ""; }
            if (ad_c.hidden != null & ad_c.hidden != "") { hidden = ",hidden: " + ad_c.hidden + ""; }
            if (ad_c.formatter != null & ad_c.formatter.Trim() != "" & ad_c.formatter != "vnn_number") { formatter = ",formatter: " + ad_c.formatter + ""; }
            if (ad_c.unformat != null & ad_c.unformat != "") { unformat = ",unformat: " + ad_c.unformat + ""; }
            if (ad_c.align != null & ad_c.align != "") { align = ",align: '" + ad_c.align + "'"; }
            if (ad_c.sopt != null & ad_c.sopt != "") { sopt = "sopt: ['" + ad_c.sopt + "']"; }
            if (ad_c.editoptions != null & ad_c.editoptions != "" & !ad_c.editoptions.Contains("<%=")) { editoptions = ",editoptions: { " + ad_c.editoptions + " }"; }
            if (ad_c.frozen != null) { frozen = ",frozen: " + ad_c.frozen.ToString().ToLower() + ""; }
            if (ad_c.label != null & ad_c.label != "") { formoptions = "label: '" + ad_c.label + "'"; }
            colModel += "{" + key + _fixed + label + name + index + width + editable + " ,editrules: { edithidden: true }" + hidden + formatter + unformat + align + ", searchoptions: { " + sopt + " }" + editoptions + frozen + ", formoptions: { " + formoptions + " } }, \n";
        }
        //--Add line
        string filepath_read = Security.UrlBase() + "App_Data/Custom_Grid.js";
        filepath_read = context.Server.MapPath(filepath_read);
        //--
        string r = File.ReadAllText(filepath_read, Encoding.Unicode);
        r = r.Replace("${ma_editstyle}", ma_editstyle);
        r = r.Replace("${ten_editstyle}", ten_editstyle);
        r = r.Replace("${ma_module}", ma_module);
        r = r.Replace("${ma_menu}", ma_menu);
        r = r.Replace("${colModel}", colModel);
        //--
        if (!File.Exists(filepath))
        {
            StreamWriter w = new StreamWriter(filepath, false, Encoding.Unicode);
            w.Close();
            w.Dispose();
        }
        File.WriteAllText(filepath, r, Encoding.Unicode);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
