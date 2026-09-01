<%@ WebHandler Language="C#" Class="JQGridMD_00_NhapkhotuNCCLoad" %>
using System;
using System.Web;
using System.Linq;
using DataAcess;
public class JQGridMD_00_NhapkhotuNCCLoad : IHttpHandler , System.Web.SessionState.IRequiresSessionState {
   public void ProcessRequest(HttpContext context){
        if(Security.id_taikhoan(context) != "") {
            EntityContext db = new EntityContext();
            //// filter
            String filter = Helper.getFilter(context);
            string id = context.Request.QueryString["id"];
            string id_sel = context.Request.QueryString["id_sel"];
            if (id == "null") { id = null; }
            string ma_menu = context.Request.QueryString["ma_menu"];
            string ma_module = context.Request.QueryString["ma_module"];
            string where_ex = context.Request.QueryString["where_ex"];
            string where_module_select = " and 1 = " +  context.Request.QueryString["module_select"];
            Module_TK mod_ = VNN_Config.get_ModuleKeThua(null, 0, ma_module, "", "", db);
string id_vaitro = Security.id_vaitro(context);            string where_vaitro = db.ad_role_where.Where(s => s.ad_role_id == id_vaitro & s.ad_module_id == mod_.ad_module_id).Select(s => s.where_sql).FirstOrDefault();
            string select_sql = VNN_Config.Select_sql(mod_.ma_module, db);
            int page = Helper.getPage(context);
            int limit = int.Parse(context.Request.QueryString["rows"]);
            String sidx = context.Request.QueryString["sidx"];
            String sord = context.Request.QueryString["sord"];
            String oper_action = context.Request.QueryString["oper_action"];
            if (oper_action == "noSel") { id_sel = "notData(404)"; }
            string id_count = select_sql.Split(',')[0];
            if (id_count.Contains(" as "))
            {
                int j_index = id_count.IndexOf(" as ");
                id_count = id_count.Substring(0, j_index);
            }


            int start, end;
            start = limit * page - limit;
            end = (page * limit) + 1;


            string orderby = "";
            if (sidx.Equals("") || sidx == null)
            {
                if (mod_.orderby_sql != null & mod_.orderby_sql != "")
                    orderby = string.Format("{0}", mod_.orderby_sql);
                else
                    orderby = string.Format("{0}", id_count + " asc");
            }
            else
            {
                orderby = sidx + " " + sord;
            }


            string ROW_NUMBER = "ROW_NUMBER() OVER (  ORDER BY "+ orderby +")";
            string sql = string.Format(@"sELeCt * fRoM (
            sELeCt " + select_sql + 
            @" (case when {4} = '{5}' and (" + ROW_NUMBER +" <= " + start + " or " + ROW_NUMBER + " >= " + end + ")  then -1 else " + ROW_NUMBER + " end) as RowNum" +
            @" fRoM {1}
            WHeRe 1=1 {2} {3} {6} {7} {8} ) P WHeRe RowNum > " + start + " AND RowNum < " + end + " or (RowNum = -1) order by RowNum asc",
            orderby, mod_.from_sql, mod_.where_sql, filter, id_count, id_sel, where_ex, where_vaitro, where_module_select);


            sql = ADmin_ConvertStringToCode.Avariable(context, sql, id_count, id, mod_, db).Replace("'", "''");


            string sqlcount = string.Format("exec [dbo].[{3}] N'{0}',N'{1}',{2}", sql, mod_.ma_moduletk, 0, mod_.procedure_sql);
            System.Data.DataTable dt_count = Mbg.Data.SqlClient.SqlHelper.GetData(sqlcount);
            int count = int.Parse(dt_count.Rows[0][0].ToString());
            string sql_select = string.Format("exec [dbo].[{3}] N'{0}',N'{1}',{2}", sql, mod_.ma_moduletk, 1, mod_.procedure_sql);
            System.Data.DataTable dt_select = Mbg.Data.SqlClient.SqlHelper.GetData(sql_select);
            Mbg.Web.JqGrid.JqGResult rs = new Mbg.Web.JqGrid.JqGResult(dt_select, count, page, limit);
            context.Response.Write(rs.WriteJson());
        }
    }
    public bool IsReusable {
        get { return false; }
    }
}
