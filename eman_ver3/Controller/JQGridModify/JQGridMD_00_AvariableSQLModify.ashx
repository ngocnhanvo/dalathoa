<%@ WebHandler Language="C#" Class="JQGridMD_00_AvariableSQLModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_00_AvariableSQLModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            EntityContext db = new EntityContext();
            string id = context.Request.QueryString["id"];
            bool update_ava = bool.Parse(context.Request.Form["update_ava"]);
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            string value = context.Request.Form["value"];
            ad_avariableSQL object_ = new ad_avariableSQL
            {
                //start truyền các giá trị cần thêm
                ad_avariableSQL_id = id_new,
                value = value,
                value_replace = context.Request.Form["value_replace"],
                iscode = bool.Parse(context.Request.Form["iscode"]),
                sapxep = sapxep,
                
                mota = context.Request.Form["mota"],
                ngaytao = DateTime.Now,
                nguoitao = Security.id_taikhoan(context),
                ngaycapnhat = DateTime.Now,
                nguoicapnhat = Security.id_taikhoan(context),
                hoatdong = true
                //end truyền các giá trị cần thêm
            };
            db.ad_avariableSQL.Add(object_);
            VNN_Function.loaddulieu_Auto(db, ma_module);
            VNN_Function.SortColumn("ad_avariableSQL", sapxep, null, null, "value", value, null);
            System.Threading.Thread.Sleep(500);
            if (update_ava == true)
            {
                ExecAvariableSQL(context, db);
            }
            msg = "true#Thêm thành công" + "#" + id_new;
        }
        catch(Exception ex)
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
            EntityContext db = new EntityContext();
            string id = context.Request.Form["id"];
            string sapxep = VNN_Config.load_number(context.Request.Form["sapxep"], 10);
            bool update_ava = bool.Parse(context.Request.Form["update_ava"]);
            ad_avariableSQL object_ = db.ad_avariableSQL.SingleOrDefault(p=>p.ad_avariableSQL_id == id);
            if (object_ != null)
            {
                //start truyền các giá trị cần sửa
                object_.value_replace = context.Request.Form["value_replace"];
                object_.iscode = bool.Parse(context.Request.Form["iscode"]);
                object_.mota = context.Request.Form["mota"];
                object_.sapxep = sapxep;
                
                object_.ngaycapnhat = DateTime.Now;
                object_.nguoicapnhat = Security.id_taikhoan(context);
                object_.hoatdong = bool.Parse(context.Request.Form["hoatdong"]);
                //#end truyền các giá trị cần sửa
                if (update_ava == true)
                {
                    ExecAvariableSQL(context, db);
                }
                VNN_Function.loaddulieu_Auto(db, ma_module);
                VNN_Function.SortColumn("ad_avariableSQL", sapxep, null, null, "value", object_.value, null);
                msg = "true#Cập nhật thành công";
            }
            else
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
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
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            EntityContext db = new EntityContext();
            string id = context.Request.Form["id"];
            ad_avariableSQL object_ = db.ad_avariableSQL.SingleOrDefault(p=>p.ad_avariableSQL_id == id );
            if (object_ != null)
            {
                db.ad_avariableSQL.Remove(object_);
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

    public void ExecAvariableSQL(HttpContext context, EntityContext db)
    {
        string filepath = Security.UrlBase() + "App_Code/ClassCalling/ADmin_ConvertStringToCode.cs";
        string str_start = "//Start Hàm này sẽ thay đổi khi ad_avariableSQL thực thi";
        string str_end = "//End Hàm này sẽ thay đổi khi ad_avariableSQL thực thi";
        string str_replace = "";
        string str_new = str_start + "\n";
        foreach (ad_avariableSQL ava in db.ad_avariableSQL)
        {
            str_new += "            if(kq.Contains(\"" + ava.value + "\"))" + "\n";
            str_new += "            {" + "\n";
            if (ava.iscode == false)
            {
                str_new += "                kq = kq.Replace(\"" + ava.value + "\",\"" + ava.value_replace.Replace("\"", "\\\"") + "\");" + "\n";
            }
            else
            {
                str_new += "                kq = kq.Replace(\"" + ava.value + "\",\"'\" + " + ava.value_replace.Replace("\"", "\\\"") + ".ToString() + \"'\");" + "\n";
            }
            str_new += "            }" + "\n";
        }

        str_new += "            " + str_end;
        filepath = context.Server.MapPath(filepath);
        string w = System.IO.File.ReadAllText(filepath, System.Text.Encoding.Unicode);
        str_replace = str_start + VNN_Function.FindString(w, str_start, str_end) + str_end;
        w = w.Replace(str_replace, str_new);
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
