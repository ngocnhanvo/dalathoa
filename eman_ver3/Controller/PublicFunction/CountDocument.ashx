<%@ WebHandler Language="C#" Class="CountDocument" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class CountDocument : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {

        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];

        switch (oper)
        {
            case "countCV":
                this.load_CountCV(context);
                break;
            default:
                break;
        }
    }

    public void load_CountCV(HttpContext context)
    {
        //LinqDataContext db = new LinqDataContext();
        ////// filter
        //String filter = "";
        //string id = null;
        string kq = "";
        //string ma_menu = context.Request.Form["ma_menu"];
        //int count_array = ma_menu.Split(',').Count();
        //string[] ma_module = new string[count_array - 1];
        //Module_TK mod_ = null;
        //ma_module = ma_menu.Split(',');
        //for (int i = 0; i < count_array; i++)
        //{
        //    try
        //    {
        //        mod_ = VNN_Config.get_ModuleKeThua(null, 0, ma_module[i], "", "", db);
        //        if (mod_.row_count == true)
        //        {
        //            string where_vaitro = db.ad_role_wheres.Where(s => s.ad_role_id == Security.id_vaitro(context) & s.ad_module_id == mod_.ad_module_id).Select(s => s.where_sql).FirstOrDefault();
        //            string select_sql = VNN_Config.Select_sql(mod_.ma_module, db);
        //            string id_count = select_sql.Split(',')[0];
        //            if (id_count.Contains(" as "))
        //            {
        //                int j_index = id_count.IndexOf(" as ");
        //                id_count = id_count.Substring(0, j_index);
        //            }

        //            string orderby = string.Format("{0}", id_count + " asc");
        //            string sql = string.Format(@"sELeCt * fRoM (
        //            sELeCt " + select_sql +
        //            @"ROW_NUMBER() OVER (ORDER BY {0}) as RowNum
        //            fRoM {1} 
        //            WHeRe 1=1 {2} {3} {4}
        //            ) P WHeRe RowNum > " + 0 + " AND RowNum < " + 10, orderby, mod_.from_sql, mod_.where_sql, filter, where_vaitro);
        //            sql = ADmin_ConvertStringToCode.Avariable(context, sql, id_count, id, mod_, db).Replace("'", "''");

        //            string sqlcount = string.Format("exec [dbo].[{3}] '{0}','{1}',{2}", sql, mod_.ma_module, 0, mod_.procedure_sql);
        //            System.Data.DataTable dt_count = Mbg.Data.SqlClient.SqlHelper.GetData(sqlcount);
        //            kq += "(" + dt_count.Rows[0][0].ToString() + ")";
        //        }
        //        else
        //        {
        //            kq += "";
        //        }
        //    }
        //    catch
        //    {
        //        kq += "";
        //    }
        //    if (i < count_array - 1)
        //    {
        //        kq += "(##)";
        //    }
        //}
        context.Response.Write(kq);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}