<%@ WebHandler Language="C#" Class="ToSanXuatLoad" %>
using System;
using System.Web;
using System.Linq;
using DataAcess;
public class ToSanXuatLoad : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            EntityContext db = new EntityContext();
            //// filter
            String filter = Helper.getFilter(context);
            string id = context.Request.QueryString["id"];
            string id_sel = context.Request.QueryString["id_sel"];
            if (id == "null") { id = null; }
            string ma_menu = context.Request.QueryString["ma_menu"];
            string ma_module = context.Request.QueryString["ma_module"];
            string where_ex = context.Request.QueryString["where_ex"];
            string where_module_select = " and 1 = " + context.Request.QueryString["module_select"];
            Module_TK mod_ = VNN_Config.get_ModuleKeThua(null, 0, ma_module, "", "", db);
            string id_vaitro = Security.id_vaitro(context); string where_vaitro = db.ad_role_where.Where(s => s.ad_role_id == id_vaitro & s.ad_module_id == mod_.ad_module_id).Select(s => s.where_sql).FirstOrDefault();
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
                orderby = "lsx.sochungtu desc, pxto.sapxep asc";
            }
            else
            {
                orderby = sidx + " " + sord;
            }


            string ROW_NUMBER = "ROW_NUMBER() OVER (  ORDER BY " + orderby + ")";
            string sql = string.Format(@"sELeCt * fRoM (
            sELeCt tosx.md_lenhsanxuat_tosx_id
                , lsx.sochungtu
                , cd.ten_phongban as cdoan
                , pxto.mota as cdoan2
                , px.ten_phongban as pxuong
                , pxto.phongbanId
                , pxto.phongbanId2
                , (select top 1 kho.md_kho_id from md_kho kho where kho.md_to_id = pxto.phongbanId and isnull(kho.hangton, 0) = 0) as md_kho_id
                , (select top 1 kho.md_kho_id from md_kho kho where kho.md_to_id = pxto.phongbanId and isnull(kho.hangton, 0) = 1) as md_kho_id2
                , (
                    select top 1 kho.md_kho_id 
                    from md_kho kho 
                    where 
                        1=1 
                        and kho.md_to_id = (select top 1 phongbanId from md_phanxuong_to where md_phanxuong_id = pxto.md_phanxuong_id and cast(sapxep as int) < cast(pxto.sapxep as int) order by sapxep desc) 
                        and isnull(kho.hangton, 0) = 0
                ) as khoprev
                , tosx.stt
                , " + ROW_NUMBER + " as RowNum " +
            @" fRoM md_lenhsanxuat_tosx tosx
                left join md_lenhsanxuat lsx on lsx.md_lenhsanxuat_id = tosx.md_lenhsanxuat_id
                left join md_phanxuong_to pxto on pxto.md_to_id = tosx.md_phanxuong_to_id
                left join ad_department cd on cd.md_phongban_id = pxto.phongbanId
                left join ad_department px on px.md_phongban_id = pxto.phongbanId2
            WHeRe 1=1 
            and tosx.ngayhoanthanh is null 
            and isnull(tosx.hoatdong, 0) = 1
            and N'{1}' like '%' + lsx.sochungtu + '%' {0} {2} ) P WHeRe RowNum > " + start + " AND RowNum < " + end + " order by RowNum asc",
            filter, id, where_ex);

            sql = ADmin_ConvertStringToCode.Avariable(context, sql, id_count, id, mod_, db).Replace("'", "''");
            //context.Response.Write(ADmin_ConvertStringToCode.Avariable(context, sql, id_count, id, mod_, db).Replace("''", "'"));

            string sqlcount = string.Format("exec [dbo].[{3}] N'{0}',N'{1}',{2}", sql, mod_.ma_moduletk, 0, mod_.procedure_sql);
            System.Data.DataTable dt_count = Mbg.Data.SqlClient.SqlHelper.GetData(sqlcount);
            int count = int.Parse(dt_count.Rows[0][0].ToString());
            string sql_select = string.Format("exec [dbo].[{3}] N'{0}',N'{1}',{2}", sql, mod_.ma_moduletk, 1, mod_.procedure_sql);
            System.Data.DataTable dt_select = Mbg.Data.SqlClient.SqlHelper.GetData(sql_select);
            Mbg.Web.JqGrid.JqGResult rs = new Mbg.Web.JqGrid.JqGResult(dt_select, count, page, limit);
            context.Response.Write(rs.WriteJson());
            //context.Response.Write(sql);
        }
    }
    public bool IsReusable
    {
        get { return false; }
    }
}
