<%@ WebHandler Language="C#" Class="ZDongMuaHangLoad" %>
using System;
using System.Web;
using System.Linq;
using DataAcess;
public class ZDongMuaHangLoad : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            if (id == "null") {
                id = null;
            }
            else
            {
                id = id.Replace(",", "','");
            }
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
                orderby = "dmh.so_donmuahang desc, sp.ma_sanpham asc";
            }
            else
            {
                orderby = sidx + " " + sord;
            }

            var khovts = db.md_kho.Where(s => s.vattu == true).OrderByDescending(s=>s.ma_kho).Select(s => s.md_kho_id).ToList();
            var kvtsStr = string.Join(",", khovts);

            string ROW_NUMBER = $"ROW_NUMBER() OVER (  ORDER BY {orderby})";
            string sql = $@"
            sELeCt 
                replace(newid(),'-','') as c_donmuahang_cdmh_id, 
                P.* 
            fRoM (
                 sELeCt 
	                sp.ma_sanpham as md_sanpham_id
	                , (select ten_dvt from md_donvitinhsanpham where md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id) as md_donvitinhsanpham_id
	                , cdmh.sl_dadat
                    , cdmh.sl_hanngach
	                , cdmh.dongiamua
	                , thue_sp.ten_thue_sanpham as thue
	                , cdmh.thanhtien
                    , dmh.so_donmuahang
                    , (case when kh.sanxuatton = 1 then sp.khoton else sp.khomacdinh end) as khomacdinh
                    , sp.khoton
                    , sp.vattu
                    , sp.ban_thanhpham
                    , kh.c_kehoachdathang_id
                    , N'{kvtsStr}' as kvts
                    , {ROW_NUMBER} as RowNum
               fRoM c_donmuahang_cdmh cdmh 
	                left join md_sanpham sp on cdmh.md_sanpham_id = sp.md_sanpham_id 
	                left join md_thue_sanpham thue_sp on thue_sp.md_thue_sanpham_id = cdmh.thue
                    left join c_donmuahang dmh on cdmh.c_donmuahang_id = dmh.c_donmuahang_id
                    left join c_kehoachdathang kh on dmh.c_kehoachdathang_dhncc_id = kh.c_kehoachdathang_id
                WHeRe 1=1 and cdmh.c_donmuahang_id in ('{id}') {filter}
            ) P 
            WHeRe 
                RowNum > {start} AND RowNum < {end} 
            order by 
                RowNum asc
            ";

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
    public bool IsReusable
    {
        get { return false; }
    }
}
