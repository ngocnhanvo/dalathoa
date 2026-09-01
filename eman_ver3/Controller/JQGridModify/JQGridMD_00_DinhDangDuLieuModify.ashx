<%@ WebHandler Language="C#" Class="JQGridMD_00_DinhDangDuLieuModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
using System.IO;
public class JQGridMD_00_DinhDangDuLieuModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "upload":
                this.upload(context);
                break;
            default:
                break;
        }
    }

    public void upload(HttpContext context)
    {
        HttpFileCollection files = context.Request.Files;
        string path = Security.UrlBase() + "mau_import";
        if (files.Count > 0)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(context.Server.MapPath(path));

            string filePath = path + "/" + files[0].FileName;
            files[0].SaveAs(context.Server.MapPath(filePath));
        }
    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        HttpFileCollection files = context.Request.Files;

        try
        {
            string id = context.Request.QueryString["id"];
            string ten_table_check = context.Request.Form["ten_table"];
            string path = Security.UrlBase() + "mau_import";
            string mau_import = context.Request.Form["mau_import"];
            if (check_table(ten_table_check) == false)
            {
                msg = "false#Không tồn tại table trong Database.";
            }
            if (msg.Length <= 0)
            {
                if (mau_import.Replace(" ", "") != "")
                {
                    mau_import = path + "/" + mau_import;
                }
                string action = "add";
                string[] column_ex = { };
                string ten_table = "ad_import";
                VNN_Function.SetFormValue("mau_import", mau_import);
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, id_new, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Thêm thành công." + "#" + id_new;
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
        string path = Security.UrlBase() + "mau_import";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_import object_ = db.ad_import.Where(p => p.ad_import_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }

            if (msg.Length <= 0)
            {
                string mau_import = object_.mau_import, mau_import_pos = context.Request.Form["mau_import"];
                if (mau_import_pos.Replace(" ", "") != "" & mau_import_pos != mau_import)
                {
                    VNN_Function.SetFormValue("mau_import", path + "/" + mau_import_pos);
                }
                string action = "edit";
                string[] column_ex = { };
                string ten_table = "ad_import";
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Cập nhật thành công.";

                if (mau_import != mau_import_pos)
                {
                    if (System.IO.File.Exists(context.Server.MapPath(mau_import)))
                    {
                        try
                        {
                            System.IO.File.Delete(context.Server.MapPath(mau_import));
                        }
                        catch (System.IO.IOException e)
                        {
                            context.Response.Write(e.Message);
                        }
                    }
                }
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
        string msg = "", msg_del = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string ten_table = "ad_import";
            int count = context.Request.Form["id"].Split(',').Count();
            string[] id_del = new string[count];
            id_del = context.Request.Form["id"].Split(',');
            for (int i = 0; i < count; i++)
            {
                msg_del = "";
                var id_del_ = id_del[i];
                ad_import object_ = db.ad_import.Where(p => p.ad_import_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg_del = "Lỗi:Không tìm thấy đối tượng cần xóa " + i;
                    msg += msg_del + "\n";
                }
                if (msg_del.Length <= 0)
                {
                    string mau_import = object_.mau_import;
                    string action = "del";
                    string[] column_ex = { };
                    VNN_Function.SetFormValue("id", object_.ad_import_id);
                    VNN_Function.Set_DefaultvalueColumn(context, action);
                    VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);

                    if (mau_import != "")
                    {
                        if (System.IO.File.Exists(context.Server.MapPath(mau_import)))
                        {
                            try
                            {
                                System.IO.File.Delete(context.Server.MapPath(mau_import));
                            }
                            catch (System.IO.IOException e)
                            {
                                context.Response.Write(e.Message);
                            }
                        }
                    }
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

    public bool check_table(string ten_table)
    {
        bool ok = false;
        string sql = "select top 1 * from " + ten_table;
        try
        {
            Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql);
            ok = true;
        }
        catch
        {
            ok = false;
        }
        return ok;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
