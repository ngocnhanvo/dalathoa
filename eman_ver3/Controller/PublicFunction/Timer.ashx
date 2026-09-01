<%@ WebHandler Language="C#" Class="Timer" %>

using System;
using System.Web;
using System.Data;
using System.Linq;

public class Timer : IHttpHandler , System.Web.SessionState.IRequiresSessionState
{
    string connect = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["edoc2014ConnectionString"].ConnectionString;
    public void ProcessRequest(HttpContext context)
    {
        //      LinqDataContext db = new LinqDataContext();
        //      // update select option
        //      string tanso_biendong_selectoption = db.md_dbbiendongs.Where(t => t.md_dbbiendong_id.Equals("ad_selectoption")).Select(t=>t.tanso_biendong).Take(1).FirstOrDefault();
        //      if (VNN_VariablePublic.auto_update_ad_selectoption != tanso_biendong_selectoption)
        //      {
        //	//VNN_Function.loaddulieu_Auto(db, "MD_00_PhieuDongDau");
        //          VNN_Function.ADUpdateSelect_Auto(context);
        //          VNN_VariablePublic.auto_update_ad_selectoption = tanso_biendong_selectoption;
        //      }
        //      //0
        //      string s = VNN_VariablePublic.Serialize(VNN_VariablePublic.session_bd) + "*##/<>" + 0;
        //      string cookie = Security.id_taikhoan(context);
        //      string bien_nguoinhan = context.Request.QueryString["bien_nguoinhan"];
        //      int count = int.Parse(context.Request.QueryString["sessionmes"]);
        //      //1 + 2 + 3 + 4 + 5 + 6
        //      s = Key_License_Timer.Key_License_Timer.timer(s, cookie, bien_nguoinhan, count, context, connect);
        //      //7
        //      s += "*##/<>" + Security.id_taikhoan(context);
        //s += "*##/<>" + VNN_VariablePublic.auto_update_ad_selectoption;
        //      context.Response.Write(s);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
}
