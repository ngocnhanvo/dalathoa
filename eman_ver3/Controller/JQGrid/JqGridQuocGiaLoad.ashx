<%@ WebHandler Language="C#" Class="JqGridQuocGiaLoad" %>

using System;
using System.Web;
using System.Linq;
using DataAcess;


public class JqGridQuocGiaLoad : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        EntityContext db = new EntityContext();
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        //// filter
        String filter = Helper.getFilter(context);

        string sqlCount = string.Format(@"select count(qg.md_quocgia_id) as count from md_quocgia qg where 1=1 " + filter);

        int page = Helper.getPage(context);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        String sidx = context.Request.QueryString["sidx"];
        String sord = context.Request.QueryString["sord"];
        //db.ExecuteQuery<int>(sqlCount).First();
        int count = db.Database.SqlQuery<int>(sqlCount).FirstOrDefault<int>();
        int start, end;

        start = limit * page - limit;
        end = (page * limit) + 1;

        if (sidx.Equals("") || sidx == null)
        {
            sidx = "ma_quocgia";
        }


        string sql = string.Format(@"select * from ( 
                        select qg.md_quocgia_id, qg.ma_quocgia, qg.ten_quocgia
                                , qg.ngaytao, qg.nguoitao, qg.ngaycapnhat
                                , qg.nguoicapnhat, qg.mota, qg.hoatdong
                                , ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum 
                            from md_quocgia qg
                            where 1=1 {2}
                        ) P WHERE RowNum > @start AND RowNum < @end", sidx, sord, filter);

        System.Data.DataTable dt = Mbg.Data.SqlClient.SqlHelper.GetData(sql, "@start", start, "@end", end);
        Mbg.Web.JqGrid.JqGResult rs = new Mbg.Web.JqGrid.JqGResult(dt, count, page, limit);
        context.Response.Write(rs.WriteJson());
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}