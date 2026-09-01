<%@ WebHandler Language="C#" Class="JQGridMD_01_VaiTroBoSungModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class JQGridMD_01_VaiTroBoSungModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "selectoption":
                this.selectoption(context);
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
        string ad_user_id = Security.id_taikhoan(context);
        ad_user tk_dangnhap = db.ad_user.Where(s => s.ad_user_id == ad_user_id).Take(1).FirstOrDefault();
        try
        {
            string id = context.Request.Form["id_parent"];
            string md_phongban_id = context.Request.Form["md_phongban_id"];
            string ad_role_id = context.Request.Form["ad_role_id"];
            bool macdinh = bool.Parse(context.Request.Form["macdinh"]);
            ad_user tk_parent = db.ad_user.Where(s => s.ad_user_id == id).Take(1).FirstOrDefault();

            if (db.ad_user_role.Where(s => s.ad_role_id == ad_role_id & s.ad_user_id == id).FirstOrDefault() != null)
            {
                msg = "false#Tài khoản đã tồn tại vai trò này.";
            }
            if (db.ad_user_role.Where(s => s.macdinh == true & s.ad_user_id == id).FirstOrDefault() != null)
            {
                if (macdinh == true)
                    msg = "false#Tài khoản đã tồn tại vai trò mặc định.";
            }

            if (msg.Length <= 0)
            {
                ad_user_role object_ = new ad_user_role();
                //start truyền các giá trị cần thêm
                object_.ad_user_role_id = id_new;
                object_.ad_user_id = id;
                object_.ad_role_id = ad_role_id;
                if (md_phongban_id != "")
                    object_.md_phongban_id = md_phongban_id;
                else
                    object_.md_phongban_id = tk_parent.md_phongban_id;

                object_.macdinh = macdinh;
                object_.nguoitao = tk_dangnhap.ad_user_id;
                object_.vaitrotao = tk_dangnhap.ad_role_id;
                object_.nguoicapnhat = tk_dangnhap.ad_user_id;
                object_.vaitrocapnhat = tk_dangnhap.ad_role_id;
                object_.ngaytao = DateTime.Now;
                object_.ngaycapnhat = DateTime.Now;
                object_.mota = context.Request.Form["mota"];
                object_.hoatdong = true;
                //#end truyền các giá trị cần thêm
                db.ad_user_role.Add(object_);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Thêm thành công" + "#" + id_new;
            }
        }
        catch(Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string ad_user_id = Security.id_taikhoan(context);
        ad_user tk_dangnhap = db.ad_user.Where(s => s.ad_user_id == ad_user_id).Take(1).FirstOrDefault();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id_parent"];
            string id_sel = context.Request.Form["id"];
            string md_phongban_id = context.Request.Form["md_phongban_id"];
            string ad_role_id = context.Request.Form["ad_role_id"];
            bool macdinh = bool.Parse(context.Request.Form["macdinh"]);
            ad_user tk_parent = db.ad_user.Where(s => s.ad_user_id == id).Take(1).FirstOrDefault();
            ad_user_role object_ = db.ad_user_role.SingleOrDefault(p => p.ad_user_role_id == id_sel);

            if (db.ad_user_role.Where(s => s.ad_role_id == ad_role_id & s.ad_user_id == id).FirstOrDefault() != null)
            {
                if (ad_role_id != object_.ad_role_id)
                    msg = "false#Tài khoản đã tồn tại vai trò này.";
            }

            if (object_ == null)
            {
                msg = "false#Không tìm thấy đối tượng.";
            }

            if (msg.Length <= 0)
            {
                //start truyền các giá trị cần sửa
                object_.ad_role_id = ad_role_id;
                if (md_phongban_id != "")
                    object_.md_phongban_id = md_phongban_id;
                else
                    object_.md_phongban_id = tk_parent.md_phongban_id;
                object_.macdinh = macdinh;
                object_.nguoicapnhat = tk_dangnhap.ad_user_id;
                object_.vaitrocapnhat = tk_dangnhap.ad_role_id;
                object_.ngaycapnhat = DateTime.Now;
                object_.mota = context.Request.Form["mota"];
                //#end truyền các giá trị cần sửa
                if(macdinh == true) {
                    foreach(ad_user_role us_rol in db.ad_user_role.
                            Where(s => s.macdinh == true & s.ad_user_id == id & s.ad_user_role_id != id_sel).ToList())
                    {
                        us_rol.macdinh = false;
                    }
                }
                db.SaveChanges();
                msg = "true#Cập nhật thành công";
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
        EntityContext db = new EntityContext();
        string msg = "",  msg_del = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            int count = context.Request.Form["id"].Split(',').Count();
            string[] id_del = new string[count];
            id_del = context.Request.Form["id"].Split(',');
            for (int i = 0; i < count; i++)
            {
                msg_del = ""; var id_del_ = id_del[i];
                string object_ = db.ad_user_role.Where(p => p.ad_user_role_id == id_del_).Select(s => s.ad_user_role_id).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg_del = "Lỗi:Không tìm thấy đối tượng cần xóa " + i;
                    msg += msg_del + "\n";
                }

                if (msg_del.Length <= 0)
                {
                    string action = context.Request.Params["oper"];
                    string[] column_ex = { };
                    VNN_Function.SetFormValue("id", object_);
                    VNN_Function.Set_DefaultvalueColumn(context, action);
                    VNN_Function.Modify_Function(context, ma_module, null, "ad_user_role", action, column_ex, db);
                }
            }
            VNN_Function.loaddulieu_Auto(db, ma_module);
            if (msg.Length <= 0)
            {
                msg = "true#Xóa thành công.";
            }
            else
            {
                msg = "false#" + msg;
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
                msg = "false#Lỗi: " + ex.Message;
            }
        }
        context.Response.Write(msg);
    }

    public void selectoption(HttpContext context)
    {
        EntityContext db = new EntityContext();
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        string id = context.Request.QueryString["id"];
        ad_user tk = db.ad_user.Where(s => s.ad_user_id == id).Take(1).FirstOrDefault();
        string select = "<select>";
        select += firstnull ? string.Format("<option value=\"\"></option>") : "";
        foreach (ad_role vtr in db.ad_role)
        {
            select += string.Format("<option value=\"{0}\">{1}</option>", vtr.ad_role_id, vtr.ten_role);
        }
        select += "</select>";
        context.Response.Write(select);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
