<%@ WebHandler Language="C#" Class="JQGridMD_00_TaiKhoanModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Web.Security;
using DataAcess;

public class JQGridMD_00_TaiKhoanModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public string oper = "vnn";
    public void ProcessRequest(HttpContext context)
    {
        string cookie = Security.id_taikhoan(context);
        if (cookie != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];

        switch (oper)
        {
            case "add":
                this.add(context, cookie);
                break;
            case "edit":
                this.edit(context, cookie);
                break;
            case "del":
                this.del(context);
                break;
            case "save":
                this.save(context, cookie);
                break;
            case "loadtaikhoan":
                this.LoadTaiKhoan(context, cookie);
                break;
            case "getthongtin":
                this.Getthongtin(context, cookie);
                break;
            case "get_phongban":
                this.get_phongban(context, cookie);
                break;
            default:
                break;
        }
    }

    public void add(HttpContext context, string cookie)
    {
        var us = VNN_Function.get_user(
            Security.id_taikhoan(context),
            Security.id_vaitro(context),
            Security.id_phongban(context));
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string id_taikhoan = Security.id_taikhoan(context);
        ad_user tk_dangnhap = db.ad_user.Where(s => s.ad_user_id == id_taikhoan).Take(1).FirstOrDefault();
        try
        {
            string ma_user = context.Request.Form["ma_user"];
            string matkhau = "";

            if (db.ad_user.Where(p => p.ma_user == (ma_user)).FirstOrDefault() != null)
            {
                msg = "false#Mã tài khoản này đã tồn tại!";
            }
            else if (context.Request.Form["matkhau"] == "")
            {
                msg = "false#Mật khẩu <b style='color:red'>(*)</b>: Trường dữ liệu bắt buộc có ";
            }

            if (msg.Length <= 0)
            {
                ad_user tk = new ad_user
                {
                    ad_user_id = id_new,
                    ma_user = context.Request.Form["ma_user"],
                    ma_nhanvien = context.Request.Form["ma_nhanvien"],
                    matkhau = context.Request.Form["matkhau"] == "" ? matkhau : Security.EncodeMd5Hash(context.Request.Form["matkhau"]),
                    hoten = context.Request.Form["hoten"],
                    phone = context.Request.Form["phone"],
                    fax = context.Request.Form["fax"],
                    email = context.Request.Form["email"],
                    diachi = context.Request.Form["diachi"],
                    duyet_sms = bool.Parse(context.Request.Form["duyet_sms"]),
                    md_phongban_id = null,
                    mauBackground = "{}",
                    ad_role_id = null,
                    hoatdong = true,
                    nguoitao = us.ad_user_id,
                    value_nguoitao = us.ma_user,
                    nguoicapnhat = us.ad_user_id,
                    value_nguoicapnhat = us.ma_user,
                    bophantao = us.md_phongban_id,
                    value_bophantao = us.ten_phongban,
                    mota = context.Request.Form["mota"]
                };
                db.ad_user.Add(tk);
                db.SaveChanges();
                msg = "true#Thêm thành công!" + "#" + id_new;

                bool check_sl_tk = true;
                if (check_sl_tk == false)
                {
                    ad_user tk_del = db.ad_user.Where(s => s.ad_user_id == id_new).Take(1).FirstOrDefault();
                    db.ad_user.Remove(tk_del);
                    db.SaveChanges();
                    msg = "false#Server vượt quá số lượng tài khoản đã đăng ký.";
                }
                else
                {
                    VNN_Function.loaddulieu_Auto(db, ma_module);
                }
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context, string cookie)
    {
        var us = VNN_Function.get_user(
            Security.id_taikhoan(context),
            Security.id_vaitro(context),
            Security.id_phongban(context));
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.Form["id"];
            ad_user tk = db.ad_user.Where(p => p.ad_user_id == id).FirstOrDefault();
            if (tk == null)
            {
                msg = "false#Không tìm thấy tài khoản này";
            }
            else
            {
                if (tk.value_nguoitao != us.ma_user)
                {
                    msg = "false#Không phải chủ sở hửu";
                }
                else
                {
                    tk.ma_user = context.Request.Form["ma_user"];
                    tk.ma_nhanvien = context.Request.Form["ma_nhanvien"];
                    tk.matkhau = context.Request.Form["matkhau"] == "" ? tk.matkhau : Security.EncodeMd5Hash(context.Request.Form["matkhau"]);
                    tk.hoten = context.Request.Form["hoten"];
                    tk.phone = context.Request.Form["phone"];
                    tk.fax = context.Request.Form["fax"];
                    tk.email = context.Request.Form["email"];
                    tk.duyet_sms = bool.Parse(context.Request.Form["duyet_sms"]);
                    tk.diachi = context.Request.Form["diachi"];
                    tk.ngaycapnhat = null;
                    tk.hoatdong = bool.Parse(context.Request.Form["hoatdong"]);
                    tk.nguoicapnhat = us.ad_user_id;
                    tk.value_nguoicapnhat = us.ma_user;
                    tk.bophantao = us.md_phongban_id;
                    tk.value_bophantao = us.ten_phongban;
                    tk.mota = context.Request.Form["mota"];
                    VNN_Function.loaddulieu_Auto(db, ma_module);
                    msg = "true#Cập nhật thành công";
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
        var us = VNN_Function.get_user(
            Security.id_taikhoan(context),
            Security.id_vaitro(context),
            Security.id_phongban(context));
        var db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                foreach (var id_del_ in ids)
                {
                    var object_ = db.ad_user.Where(p => p.ad_user_id == id_del_).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else if (object_.nguoitao != us.ad_user_id)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không thuộc sở hửu của {1}.", object_.ma_user, us.ma_user);
                    }
                    else
                    {
                        VNN_Function.Write_log(context, ma_module, null, oper, "MTK:" + object_.ma_user + ", TTK:" + object_.hoten, db);
                        db.ad_user.Remove(object_);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = @"true#Xóa tài khoản đã chọn thành công.";
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = string.Format(@"false#{0}", msg.Substring(4));
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public void save(HttpContext context, string cookie)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        bool error = false;

        try
        {
            string ad_role_id = context.Request.Form["ad_role_id"];
            bool vaitro_macdinh = bool.Parse(context.Request.Form["vaitro_macdinh"]);
            ad_user tk_dangnhap = db.ad_user.Where(s => s.ad_user_id == cookie).Take(1).FirstOrDefault();
            ad_user_role tk_vtr = db.ad_user_role.Where(s => s.ad_user_id == cookie & s.ad_role_id == ad_role_id).Take(1).FirstOrDefault();

            //--update Password
            string oldPassword = context.Request.Form["oldpassword"];
            string newPassword = context.Request.Form["newpassword"];
            string confirmPassword = context.Request.Form["confirm"];
            var mixmode = context.Request.Form["giaodien_macdinh"].removeAllSpaceOrTrimText(false).ToNullableBool().GetValueOrDefault(false);
            var border = context.Request.Form["giaodien_luoi"].removeAllSpaceOrTrimText(false);
            var updated = false;

            if (tk_dangnhap != null)
            {
                //--update Password
                if (tk_dangnhap.ma_user != "giamdoc" & tk_dangnhap.ma_user != "vanthu" & tk_dangnhap.ma_user != "truongphong" & tk_dangnhap.ma_user != "nhanvien")
                {
                    if (!string.IsNullOrEmpty(newPassword) |
                        !string.IsNullOrEmpty(oldPassword) |
                        !string.IsNullOrEmpty(confirmPassword))
                    {
                        if (tk_dangnhap.matkhau.Equals(Security.EncodeMd5Hash(oldPassword)))
                        {
                            if (newPassword.Equals(confirmPassword))
                            {
                                if (!string.IsNullOrEmpty(newPassword)
                                        & !string.IsNullOrEmpty(confirmPassword))
                                {
                                    tk_dangnhap.matkhau = Security.EncodeMd5Hash(newPassword);
                                    updated = true;
                                }
                                else
                                {
                                    msg = "Mật khẩu mới không thể để trống";
                                    error = false;
                                }
                            }
                            else
                            {
                                msg = "Xác nhận mật khẩu không đúng";
                                error = false;
                            }
                        }
                        else
                        {
                            msg = "Sai mật khẩu cũ.";
                            error = false;
                        }
                    }
                }
                else
                {
                    msg = "Tài khoản này là tài khoản demo, bạn không thể thay đổi mật khẩu.";
                    error = false;
                }
                //--

                if (vaitro_macdinh == true)
                {
                    foreach (ad_user_role tk_vtr_ in db.ad_user_role.Where(s => s.ad_user_id == cookie & s.macdinh == true).ToList())
                    {
                        tk_vtr_.macdinh = false;
                    }
                    tk_vtr.macdinh = true;
                    updated = true;
                }

                var conf = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(tk_dangnhap.mauBackground);
                conf.mixmode = mixmode;
                conf.border = border;
                var new_mauBackground = Newtonsoft.Json.JsonConvert.SerializeObject(conf);
                if (tk_dangnhap.mauBackground != new_mauBackground)
                {
                    tk_dangnhap.mauBackground = new_mauBackground;
                    updated = true;
                }

                if (updated)
                    db.SaveChanges();

                if (msg == "")
                {
                    var userJSon = Security.all_taikhoan(context);
                    userJSon["ad_user_id"] = tk_dangnhap.ad_user_id;
                    userJSon["user_role"] = ad_role_id;
                    if(tk_vtr != null)
                        userJSon["user_part"] = tk_vtr.md_phongban_id;
                    userJSon["mauBackground"] = tk_dangnhap.mauBackground;
                    string token = Newtonsoft.Json.JsonConvert.SerializeObject(userJSon);
                    FormsAuthentication.SignOut();
                    System.Threading.Thread.Sleep(1000);
                    FormsAuthentication.SetAuthCookie(token, false);
                    msg = "Chỉnh sửa thông tin thành công!!!";
                    error = true;
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
            error = false;
        }

        context.Response.Write(string.Format(@"{0}(##){1}(##)<div class='{3}'>{2}</div>", error.ToString().ToLower(), Security.UrlBase(), msg, error ? "nhan-thanhcong" : "nhan-loi"));

    }

    public void LoadTaiKhoan(HttpContext context, string cookie)
    {
        EntityContext db = new EntityContext();
        var data = "";
        if (context.Request.QueryString["timkiem"] == "")
        {
            data = "Tất cả#";
            foreach (ad_user mn in db.ad_user.OrderBy(s => s.ma_user).Where(s => s.ad_user_id != cookie).ToList())
            {
                data += mn.ma_user + "#";
            }
        }
        else
        {
            data = "Tất cả#";
            foreach (ad_user mn in db.ad_user.Where(s => s.ma_user.Contains(context.Request.QueryString["timkiem"]) & s.ad_user_id != cookie).ToList())
            {
                data += mn.ma_user + "#";
            }
        }
        context.Response.Write(data);
    }

    public void Getthongtin(HttpContext context, string cookie)
    {
        EntityContext db = new EntityContext();
        //try
        {
            ad_user tk_dangnhap = db.ad_user.Where(s => s.ad_user_id == (cookie)).Take(1).FirstOrDefault();
            string id_phongban = Security.id_phongban(context);
            ad_department pb = db.ad_department.Where(p => p.md_phongban_id == id_phongban).FirstOrDefault();

            var vaitro = from a in db.ad_user_role
                         join b in db.ad_role on a.ad_role_id equals b.ad_role_id
                         where a.ad_user_id == cookie
                         orderby b.mota ascending
                         select new { a.ad_role_id, b.ten_role };
            string option = "<select>";
            foreach (var tk_vtr in vaitro)
            {
                string check = "";
                if (tk_vtr.ad_role_id == Security.id_vaitro(context))
                {
                    check = "selected";
                }
                option += string.Format("<option value=\"{0}\" {2}>{1}</option>", tk_vtr.ad_role_id, tk_vtr.ten_role, check);
            }
            option += "</select>";
            var userData = new
            {
                ma_user = tk_dangnhap.ma_user,
                hoten = tk_dangnhap.hoten,
                vaitro = option,
                phone = tk_dangnhap.phone,
                email = tk_dangnhap.email,
                md_phongban_id = pb.md_phongban_id,
                ten_phongban = pb.ten_phongban,
                mauBackground = tk_dangnhap.mauBackground,
                mota = tk_dangnhap.mota
            };

            string json_data = Newtonsoft.Json.JsonConvert.SerializeObject(userData);
            context.Response.Clear();
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(json_data);
        }
        //catch (Exception ex)
        {
            //context.Response.Write("Lỗi: " + ex.Message);
        }
    }

    public void get_phongban(HttpContext context, string cookie)
    {
        EntityContext db = new EntityContext();
        string id = context.Request.QueryString["id"];
        ad_user_role tk_vtr = db.ad_user_role.Where(s => s.ad_user_id == cookie & s.ad_role_id == id).Take(1).FirstOrDefault();
        ad_department pb = db.ad_department.Where(s => s.md_phongban_id == tk_vtr.md_phongban_id).Take(1).FirstOrDefault();
        context.Response.Write(pb.ten_phongban);
    }

    public class ThongTinUser
    {

        public string hoten;
        public string phone;
        public string email;

        public string ma_user;
        public string vaitro;
        public string md_phongban_id;
        public string ten_phongban;
        public bool sudungthanhtruot;
        public string mota;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
