<%@ WebHandler Language="C#" Class="ChangePrintMethod" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using DataAcess;
using System.Text;

public class ChangePrintMethod : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.QueryString["oper"];
        switch (oper)
        {
            case "update":
                this.Update(context);
                break;
            case "excute":
                this.Excute(context);
                break;
            case "configColumns":
                this.ConfigColumns(context);
                break;
            case "configSizeLayout":
                this.ConfigSizeLayout(context);
                break;
            case "configMenu":
                this.configMenu(context);
                break;
        }
    }

    public void configMenu(HttpContext context)
    {
        string msg = "";
        dynamic result = new Dictionary<string, object>() {
            { "ok", false },
            { "msg", "" },
            { "btnDongMenuTuDong", false },
            { "btnDongMenuConTuDong", false }
        };
        string ck = Security.id_taikhoan(context);
        var dongMenuTuDong = context.Request.Form["dongMenuTuDong"].ToLower() == "true";
        var dongMenuConTuDong = context.Request.Form["dongMenuConTuDong"].ToLower() == "true";
        var moMenuConTuDong = context.Request.Form["moMenuConTuDong"].ToLower() == "true";

        try
        {
            var db = new EntityContext();
            var user = db.ad_user.Where(s => s.ad_user_id == ck).FirstOrDefault();
            if (user != null)
            {
                user.btnDongMenuTuDong = dongMenuTuDong;
                user.btnDongMenuConTuDong = dongMenuConTuDong;
                user.tuDongNhanDienCachIn = moMenuConTuDong;
                db.SaveChanges();
            }
            else
            {
                msg = "Tài khoản không tồn tại.";
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
        {
            msg = @"Cập nhật tùy chỉnh ""hiển thị menu"" thành công";
            result["ok"] = true;
            result["btnDongMenuTuDong"] = dongMenuTuDong;
            result["btnDongMenuConTuDong"] = dongMenuConTuDong;
            result["moMenuConTuDong"] = moMenuConTuDong;
        }

        result["msg"] = msg;
        context.Response.Write(JsonConvert.SerializeObject(result));
    }

    public void ConfigSizeLayout(HttpContext context)
    {
        try
        {
            var dicUser = Security.all_taikhoan(context);
            var ma_user = "";
            if (dicUser.ContainsKey("ma_user"))
                ma_user = dicUser["ma_user"].ToString();

            var itemModels = context.Request.Form["itemModels"];

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(itemModels);

            var directory = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/CustomGrid/" + ma_user);
            var filepath = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/CustomGrid/" + ma_user + "/layoutSize.json");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filepath))
            {
                var w = new StreamWriter(filepath, false, Encoding.UTF8);
                w.WriteLine("[]");
                w.Flush();
                w.Close();
            }

            var jsonPrev = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(File.ReadAllText(filepath));

            var item = jsonPrev.Where(s => s["module"].ToString() == json["module"].ToString()).FirstOrDefault();

            if (item == null)
            {
                jsonPrev.Add(json);
            }
            else
            {
                item["size1"] = json["size1"];
                item["size2"] = json["size2"];
            }

            var jsonData = JsonConvert.SerializeObject(jsonPrev);
            File.WriteAllText(filepath, jsonData);
            context.Response.Write(jsonData);
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }

    public void ConfigColumns(HttpContext context)
    {
        string msg = "";
        try
        {
            var type = context.Request.Form["type"];
            var itemModels = context.Request.Form["itemModels"];
            var grid = context.Request.Form["grid"];
            var dicUser = Security.all_taikhoan(context);
            var ma_user = "";
            if (dicUser.ContainsKey("ma_user"))
                ma_user = dicUser["ma_user"].ToString();
            else
            {
                var db = new EntityContext();
                var userId = dicUser["ad_user_id"].ToString();
                ma_user = db.ad_user.Where(s => s.ad_user_id == userId).Select(s => s.ma_user).FirstOrDefault();
            }

            var json = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(itemModels);

            var directory = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/CustomGrid/" + ma_user);
            var filepath = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/CustomGrid/" + ma_user + "/" + grid + ".json");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filepath))
            {
                StreamWriter w = new StreamWriter(filepath, false, Encoding.UTF8);
                w.WriteLine("[]");
                w.Flush();
                w.Close();
            }

            var jsonPrev = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(File.ReadAllText(filepath));

            if (type == "1")
            {
                foreach (var item in json)
                {
                    var haveItem = false;
                    foreach (var itemPrev in jsonPrev)
                    {
                        if (item["name"].ToString() == itemPrev["name"].ToString())
                        {
                            haveItem = true;
                            if (item.ContainsKey("width"))
                                itemPrev["width"] = item["width"];
                        }
                    }

                    if (!haveItem)
                    {
                        jsonPrev.Add(item);
                    }
                }
                json = jsonPrev;
            }
            else if (type == "2")
            {
                foreach (var item in json)
                {
                    var itemPrev = jsonPrev.Where(s => s["name"].ToString() == item["name"].ToString()).FirstOrDefault();
                    if (itemPrev != null)
                    {
                        if (itemPrev.ContainsKey("width"))
                            item["width"] = itemPrev["width"];
                    }
                }
            }

            string jsonData = JsonConvert.SerializeObject(json, Formatting.Indented);

            File.WriteAllText(filepath, jsonData);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        context.Response.Write(msg);
    }

    public void Excute(HttpContext context)
    {
        var chuyenCachInBTSangPDF = context.Request.Form["chuyenCachInBTSangPDF"].ToLower() == "true";
        var tuDongNhanDienCachIn = context.Request.Form["tuDongNhanDienCachIn"].ToLower() == "true";
        var userJSon = Security.all_taikhoan(context);
        userJSon["chuyenCachInBTSangPDF"] = chuyenCachInBTSangPDF;
        userJSon["tuDongNhanDienCachIn"] = tuDongNhanDienCachIn;
        var tokenFinally = JsonConvert.SerializeObject(userJSon);

        System.Web.Security.FormsAuthentication.RedirectFromLoginPage(tokenFinally, false);
    }

    public void Update(HttpContext context)
    {
        var db = new EntityContext();
        string ck = Security.id_taikhoan(context);
        var chuyenCachInBTSangPDF = context.Request.Form["chuyenCachInBTSangPDF"].ToLower() == "true";
        var tuDongNhanDienCachIn = context.Request.Form["tuDongNhanDienCachIn"].ToLower() == "true";

        string msg = "";
        dynamic result = new Dictionary<string, object>() { { "ok", false }, { "msg", "" }, { "chuyenCachInBTSangPDF", false }, { "tuDongNhanDienCachIn", false } };

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                ad_user tk = db.ad_user.SingleOrDefault(p => p.ad_user_id.Equals(ck));

                if (tk != null)
                {
                    tk.chuyenCachInBTSangPDF = chuyenCachInBTSangPDF;
                    tk.tuDongNhanDienCachIn = tuDongNhanDienCachIn;
                    db.SaveChanges();
                }
                else
                {
                    msg = "Tài khoản không tồn tại.";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = "<div class='nhan-thanhcong'>Cập nhật cách in thành công</div>";
                result["ok"] = true;
                result["chuyenCachInBTSangPDF"] = chuyenCachInBTSangPDF;
                result["tuDongNhanDienCachIn"] = tuDongNhanDienCachIn;
                transaction.Commit();
            }
            else
            {
                msg = string.Format(@"<div class='nhan-loi'>{0}</div>", msg);
                transaction.Rollback();
            }

            result["msg"] = msg;
            context.Response.Write(JsonConvert.SerializeObject(result));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}