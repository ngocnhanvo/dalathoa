<%@ WebHandler Language="C#" Class="JQGridMD_00_SelectOptionModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_SelectOptionModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
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
                this.loadlist(context);
                break;
            default:
                break;
        }
    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.QueryString["id"];
            //sort
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string ma_selectoption = context.Request.Form["ma_selectoption"];
            string new_select = context.Request.Form["select_sql"];
            string new_from = context.Request.Form["from_sql"];
            string new_where = context.Request.Form["where_sql"];
            string new_orderby = context.Request.Form["orderby_sql"];
            string display_member = context.Request.Form["display_member"];
            string sql = "sELeCt " + new_select + " fRoM " + new_from + " WHeRe 1=1 " + new_where;
            if (new_orderby != null)
            {
                if (new_orderby.Replace(" ", "").Length > 0)
                {
                    sql += " OrDEr bY " + new_orderby;
                }
            }
            string Testdisplay_member = "falsefalse#Display member0 không có giá trị để hiển thị.";
            //#sort
            if (db.ad_selectoption.Where(p => p.ma_selectoption.Equals(ma_selectoption)).FirstOrDefault() != null)
            {
                msg = "false#Mã selectoption này đã tồn tại";
            }
            else if(new_select.removeAllSpaceOrTrimText(true).Length <= 0)
            {
                msg = "false#Phải có Select";
            }
            else if(new_from.removeAllSpaceOrTrimText(true).Length <= 0)
            {
                msg = "false#Phải có From";
            }
            else if(new_where.removeAllSpaceOrTrimText(true).Length <= 0)
            {
                msg = "false#Phải có Where";
            }
            else if(new_orderby.removeAllSpaceOrTrimText(true).Length <= 0)
            {
                msg = "false#Phải có Order by";
            }
            else if (ma_selectoption.Length < 5)
            {
                msg = "false#Mã selectoption này phải có dạng là: <%=" + ma_selectoption + "%>";
            }
            else if (ma_selectoption.Substring(0, 3) != "<%=" | ma_selectoption.Substring(ma_selectoption.Length -2, 2) != "%>")
            {
                msg = "false#Mã selectoption này phải có dạng là: <%=" + ma_selectoption + "%>";
            }
            else if (ma_selectoption == "<%=MaSelect%>")
            {
                msg = "false#Đây là mã mặc định không được sử dụng.";
            }
            else if (VNN_Function.TestSQL(sql) == false)
            {
                msg = "false#Sai cú pháp SQL: " + sql;
            }
            if (msg.Length <= 0)
            {
                Testdisplay_member = VNN_Function.ADUpdateSelect(sql, display_member);
                if (Testdisplay_member.Substring(0, 5) == "false")
                {
                    msg = Testdisplay_member.Substring(5);
                }
            }
            if (msg.Length <= 0)
            {
                ad_selectoption object_ = new ad_selectoption();
                //start truyền các giá trị cần thêm
                object_.ad_selectoption_id = id_new;
                object_.ma_selectoption = ma_selectoption;
                object_.ten_selectoption = context.Request.Form["ten_selectoption"];
                object_.select_sql = new_select;
                object_.from_sql = new_from;
                object_.where_sql = new_where;
                object_.orderby_sql = new_orderby;
                object_.display_member = display_member;
                object_.sapxep = sapxep;
                object_.ngaytao = DateTime.Now;
                object_.nguoitao = Security.id_taikhoan(context);
                object_.ngaycapnhat = DateTime.Now;
                object_.nguoicapnhat = Security.id_taikhoan(context);
                object_.mota = context.Request.Form["mota"];
                object_.value_selectoption = Testdisplay_member;
                object_.hoatdong = true;

                //#end truyền các giá trị cần thêm
                db.ad_selectoption.Add(object_);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                VNN_Function.SortColumn("ad_selectoption", sapxep, null, null, "ma_selectoption", ma_selectoption, null);
                var ttc = Helper.getInfoDB();
                VNN_Function.create_Trigger(ttc["database"]);
                editJSON(context, object_, "modify");
                msg = "true#Thêm thành công" + "#" + id_new;
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
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_selectoption object_ = db.ad_selectoption.SingleOrDefault(p=>p.ad_selectoption_id == id);
            //sort
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string ma_selectoption = object_.ma_selectoption;
            string new_select = context.Request.Form["select_sql"];
            string new_from = context.Request.Form["from_sql"];
            string new_where = context.Request.Form["where_sql"];
            string new_orderby = context.Request.Form["orderby_sql"];
            string display_member = context.Request.Form["display_member"];
            string sql = "sELeCt " + new_select + " fRoM " + new_from + " WHeRe 1=1 " + new_where;
            if (new_orderby != null)
            {
                if (new_orderby.Replace(" ", "").Length > 0)
                {
                    sql += " OrDEr bY " + new_orderby;
                }
            }
            string Testdisplay_member = "false#Display member0 không có giá trị để hiển thị.";
            //#sort

            if(new_select.removeAllSpaceOrTrimText(true).Length <= 0)
            {
                msg = "false#Phải có Select";
            }
            else if(new_from.removeAllSpaceOrTrimText(true).Length <= 0)
            {
                msg = "false#Phải có From";
            }
            else if(new_where.removeAllSpaceOrTrimText(true).Length <= 0)
            {
                msg = "false#Phải có Where";
            }
            else if(new_orderby.removeAllSpaceOrTrimText(true).Length <= 0)
            {
                msg = "false#Phải có Order by";
            }
            else if (VNN_Function.TestSQL(sql) == false)
            {
                msg = "false#Sai cú pháp SQL: " + sql;
            }

            if (msg.Length <= 0)
            {
                Testdisplay_member = VNN_Function.ADUpdateSelect(sql, display_member);
                if (Testdisplay_member.Substring(0, 5) == "false")
                {
                    msg = Testdisplay_member.Substring(5);
                }
            }

            if (msg.Length <= 0)
            {
                if (object_ != null)
                {
                    //start truyền các giá trị cần sửa
                    object_.ten_selectoption = context.Request.Form["ten_selectoption"];
                    object_.select_sql = new_select;
                    object_.from_sql = new_from;
                    object_.where_sql = new_where;
                    object_.orderby_sql = new_orderby;
                    object_.display_member = display_member;
                    object_.sapxep = sapxep;
                    object_.ngaycapnhat = DateTime.Now;
                    object_.nguoicapnhat = Security.id_taikhoan(context);
                    object_.mota = context.Request.Form["mota"];
                    object_.value_selectoption = Testdisplay_member;
                    object_.hoatdong = bool.Parse(context.Request.Form["hoatdong"]);
                    //#end truyền các giá trị cần sửa
                    VNN_Function.loaddulieu_Auto(db, ma_module);
                    editJSON(context, object_, "modify");
                    VNN_Function.SortColumn("ad_selectoption", sapxep, null, null, "ma_selectoption", ma_selectoption, null);
                    msg = "true#Cập nhật thành công";
                }
                else
                {
                    msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
            }
        }
        catch(Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_selectoption object_ = db.ad_selectoption.SingleOrDefault(p=>p.ad_selectoption_id == id);
            if (object_ != null)
            {
                db.ad_selectoption.Remove(object_);
                editJSON(context, object_, "del");
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Xóa thành công";
            }
            else
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần xóa ";
            }
        }
        catch(Exception ex)
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

    public void loadlist(HttpContext context)
    {
        EntityContext db = new EntityContext();
        var data = "";
        foreach (ad_selectoption sel in db.ad_selectoption.ToList().OrderBy(p => p.sapxep))
        {
            data += "<a style='display:none'>" + sel.ma_selectoption + "</a><a>" + sel.ten_selectoption + "</a>#";
        }
        context.Response.Write(data.Remove(data.Length - 1));
    }

    public void editJSON(HttpContext context, ad_selectoption sel, string action)
    {
        ADmin_JSON json = new ADmin_JSON();
        json.urlData = typeof(ad_selectoption).Name;
        json.ClearCache(context, json.urlData);
        var selectoptions = json.ad_selectoptionJSON();
        var item = selectoptions.Where(s => s.ad_selectoption_id == sel.ad_selectoption_id).FirstOrDefault();
        if (action == "modify")
        {
            if(item != null)
            {
                sel.CopyPropertiesTo(item);
                //item.value_selectoption = sel.value_selectoption;
            }
            else
            {
                selectoptions.Add(sel);
            }
        }
        else
        {
            if(item != null)
                selectoptions.Remove(item);
        }
        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(selectoptions, Newtonsoft.Json.Formatting.Indented);
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
