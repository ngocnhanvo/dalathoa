<%@ WebHandler Language="C#" Class="JQGridMD_00_DTKDLoad_Hand" %>
using System;
using System.Web;
using System.Linq;
using DataAcess;
public class JQGridMD_00_DTKDLoad_Hand : IHttpHandler , System.Web.SessionState.IRequiresSessionState {
   public void ProcessRequest(HttpContext context){
        if(Security.id_taikhoan(context) != "") {
			int page = Helper.getPage(context);
            int limit = int.Parse(context.Request.QueryString["rows"]);
			String filter = Helper.getFilter(context);
			string sql = @"select distinct A.* from
			(
			select ma_dtkd, ten_dtkd, diachi, isncc from md_doitackinhdoanh
			)A where 1=1 " + filter;

            string sqlcount = string.Format("select count(A.ma_dtkd) from (" + sql + ")A");
            System.Data.DataTable dt_count = Mbg.Data.SqlClient.SqlHelper.GetData(sqlcount);
            int count = int.Parse(dt_count.Rows[0][0].ToString());
            string sql_select = string.Format(sql);
            System.Data.DataTable dt_select = Mbg.Data.SqlClient.SqlHelper.GetData(sql_select);
            Mbg.Web.JqGrid.JqGResult rs = new Mbg.Web.JqGrid.JqGResult(dt_select, count, page, limit);
            context.Response.Write(rs.WriteJson());
        }
    }
    public bool IsReusable {
        get { return false; }
    }
}
