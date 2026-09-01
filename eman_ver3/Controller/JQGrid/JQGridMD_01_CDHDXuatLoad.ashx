<%@ WebHandler Language="C#" Class="JQGridMD_01_CDHDXuatLoad" %>
using System;
using System.Web;
using System.Linq;
using DataAcess;
public class JQGridMD_01_CDHDXuatLoad : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            EntityContext db = new EntityContext();
            //// filter
            String filter = Helper.getFilter(context);
            String filterP = Helper.getFilterP(context);
            string id = context.Request.QueryString["id"];
            string ids = context.Request.QueryString["ids"];
            string id_sel = context.Request.QueryString["id_sel"];
            if (id == "null") { id = null; }
            string ma_menu = context.Request.QueryString["ma_menu"];
            string ma_module = context.Request.QueryString["ma_module"];
            string where_ex = context.Request.QueryString["where_ex"];
            string where_module_select = " and 1 = " + context.Request.QueryString["module_select"];
            Module_TK mod_ = VNN_Config.get_ModuleKeThua(null, 0, ma_module, "", "", db);
            string id_vaitro = Security.id_vaitro(context);
            string where_vaitro = db.ad_role_where.Where(s => s.ad_role_id == id_vaitro & s.ad_module_id == mod_.ad_module_id).Select(s => s.where_sql).FirstOrDefault();
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

            string ROW_NUMBER = "";
            string orderby = "", orderbyP = "RowNum asc";
            if (sidx.Equals("") || sidx == null)
            {
                if (mod_.orderby_sql != null & mod_.orderby_sql != "")
                    orderby = string.Format("{0}", mod_.orderby_sql);
            }
            else
            {
                if (sidx.StartsWith("P."))
                {
                    orderbyP = $"{sidx} {sord}";
                }
                else
                {
                    orderby = sidx + " " + sord;
                }
            }

            if (string.IsNullOrWhiteSpace(orderby))
                orderby = string.Format("{0}", id_count + " asc");

            ROW_NUMBER = $"ROW_NUMBER() OVER (ORDER BY {orderby}) as RowNum";

            string groupby = "";
            if (mod_.groupby_sql != null & mod_.groupby_sql != "")
                groupby = string.Format("group by {0}", mod_.groupby_sql);

            string whereSE = "";
            if (limit < 999999)
                whereSE = $@"and RowNum > {start} AND RowNum < {end}";

            string whereSQL = mod_.where_sql;
            string sql = $@"
                sELeCt P.* 
                fRoM (
                   sELeCt 
                        {select_sql}
                        {ROW_NUMBER}
                   fRoM 
                        {mod_.from_sql}
                   WHeRe 1=1 
                        {whereSQL} 
                        {filter} 
                        {where_ex} 
                        {where_vaitro} 
                        {where_module_select}
                   {groupby}
                ) P 
                WHeRe 
                    1=1 {filterP} {whereSE}
                order by 
                    {orderbyP}
            ";

            sql = ADmin_ConvertStringToCode.Avariable(context, sql, id_count, id, mod_, db).Replace("'", "''");

            if (mod_.procedure_sql == "") { mod_.procedure_sql = "admin_excutesql"; }

            string sql_select = $@"exec [dbo].[{mod_.procedure_sql}] N'{sql}',N'{mod_.ma_moduletk}',1";
            var dt_select = Mbg.Data.SqlClient.SqlHelper.GetData(sql_select);

            int count = dt_select.Rows.Count;
            if (limit < 999999)
            {
                string sqlcount = $@"exec [dbo].[{mod_.procedure_sql}] N'{sql}',N'{mod_.ma_moduletk}',0";
                var dt_count = Mbg.Data.SqlClient.SqlHelper.GetData(sqlcount);
                count = int.Parse(dt_count.Rows[0][0].ToString());
            }
            var rs = new Mbg.Web.JqGrid.JqGResult(dt_select, count, page, limit);
            context.Response.Write(rs.WriteJson());
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
