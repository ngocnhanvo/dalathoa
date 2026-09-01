<%@ WebHandler Language="C#" Class="ChangePassword" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAcess;
using Newtonsoft.Json;

public class ChangePassword : IHttpHandler, System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest (HttpContext context) {
        EntityContext db = new EntityContext();
        string ck = Security.id_taikhoan(context);
        String oldPassword= context.Request.Form["oldpassword"];
        String newPassword= context.Request.Form["newpassword"];
        String confirmPassword= context.Request.Form["confirm"];


        string msg = "";
        dynamic result = new Dictionary<string, object>() { { "ok", false }, { "msg", "" } };

        using (var transaction = db.Database.BeginTransaction())
        {
            ad_user tk = db.ad_user.SingleOrDefault(p => p.ad_user_id.Equals(ck));
            if (tk != null)
            {
                if (tk.matkhau.Equals(Security.EncodeMd5Hash(oldPassword)))
                {
                    if (newPassword.Equals(confirmPassword))
                    {
                        tk.matkhau = Security.EncodeMd5Hash(newPassword);
                        db.SaveChanges();
                        result["ok"] = true;
                    }
                    else
                    {
                        msg = "Xác nhận mật khẩu không đúng.";
                    }
                }
                else
                {
                    msg = "Sai mật khẩu cũ.";
                }
            }
            else
            {
                msg  = "Tài khoản không tồn tại.";
            }

            if(msg.Length <= 0)
            {
                msg = "Cập nhật mật khẩu thành công";
                result["ok"] = true;
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
        }

        result["msg"] = msg;
        context.Response.Write(JsonConvert.SerializeObject(result));
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}