<%@ WebHandler Language="C#" Class="Download" %>

using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using System.IO;
using DataAcess;
using Newtonsoft.Json;

public class Download : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string md_taptin_id = context.Request.QueryString["id"];
        md_taptin tt = db.md_taptin.Where(p => p.md_taptin_id == md_taptin_id).FirstOrDefault();
        if (tt != null)
        {
            string encodedFileName = Uri.EscapeDataString(tt.tentaptin);
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.ContentType = tt.mimetype;
            context.Response.AddHeader("Content-Disposition", "attachment; filename*=UTF-8''" + encodedFileName);
            context.Response.Charset = "";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string path = ExcuteSignalRStatic.mapPathSignalR($@"~/{tt.path}");
            if(tt.doituong_dinhkem == "HDLH")
            {
                if (!tt.viewed.GetValueOrDefault(false))
                {
                    //tt.viewed = true;
                    //db.SaveChanges();
                    //var hubExec = new MainHubExcute();
                    //hubExec.Exec("daXemHuongDanLamHang", JsonConvert.SerializeObject(tt));
                }
            }
            context.Response.WriteFile(path);
            context.Response.End();
        }
        else if (md_taptin_id != null & md_taptin_id != "")
        {
            int j = md_taptin_id.LastIndexOf("/");
            string filename = md_taptin_id.Substring(j+1);
            string path = ExcuteSignalRStatic.mapPathSignalR("~/" + md_taptin_id);
            if (File.Exists(path))
            {
                string encodedFileName = Uri.EscapeDataString(filename);
                context.Response.Clear();
                context.Response.Buffer = true;
                context.Response.AddHeader("Content-Disposition", "attachment; filename*=UTF-8''" + encodedFileName);
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.WriteFile(path);
            }
            else
            {
                context.Response.AddHeader("Content-Disposition", "attachment;filename=notfound.txt");
            }
            context.Response.End();
        }
        else
        {
            context.Response.AddHeader("Content-Disposition", "attachment;filename=notfound.txt");
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