<%@ WebHandler Language="C#" Class="ThongTinSanPhamNangCao" %>
using System;
using System.Web;
using System.Linq;
using DataAcess;
public class ThongTinSanPhamNangCao : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            EntityContext db = new EntityContext();
            //// filter
            String filter = Helper.getFilter(context);
            String filterP = Helper.getFilterP(context);
            int page = Helper.getPage(context);
            int limit = int.Parse(context.Request.QueryString["rows"]);
            String sidx = context.Request.QueryString["sidx"];
            String sord = context.Request.QueryString["sord"];
            String oper_action = context.Request.QueryString["oper_action"];
            string id_vaitro = Security.id_vaitro(context);
            var ad_module_id = db.ad_module.Where(s => s.ma_module == "MD_01_DDSDHTNJQGS").Select(s => s.ad_module_id).FirstOrDefault();
            string where_vaitro = db.ad_role_where.Where(s => s.ad_role_id == id_vaitro & s.ad_module_id == ad_module_id).Select(s => s.where_sql).FirstOrDefault();
            int start, end;
            start = limit * page - limit;
            end = (page * limit) + 1;

            string ROW_NUMBER = "";
            string orderby = "", orderbyP = "RowNum asc";
            if (sidx.Equals("") || sidx == null)
            {
                orderby = $"sp.ma_sanpham, dh.ngaytao desc";
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
                orderby = $"sp.ma_sanpham, dh.ngaytao desc";

            ROW_NUMBER = $"ROW_NUMBER() OVER (ORDER BY {orderby}) as RowNum";

            string whereSE = "";
            if (limit < 999999)
                whereSE = $@"and RowNum > {start} AND RowNum < {end}";

            string sql = $@"
                sELeCt P.* 
                fRoM (
                   sELeCt 
                        dsdh.c_dongdsdh_id,
                        sp.ma_sanpham as md_sanpham_id,
                        sp.mota_tiengviet as mota_tiengviet,
                        dvtsp.ten_dvt as md_donvitinhsanpham_id,
                        dsdh.sl_dathang as sl_dathang,
                        dsdh.gianhap as gianhap,
                        dsdh.sl_nhaphang as sl_nhaphang,
                        isnull(dsdh.gianhap,0) * isnull(dsdh.sl_dathang,0) as thanhtien,
                        dh.sochungtu,
                        dtkd.ten_dtkd as ten_khachhang,
                        dh.diachigiaohang,
                        {ROW_NUMBER}
                   fRoM 
                        c_dongdsdh dsdh
                        left join md_sanpham sp on dsdh.md_sanpham_id = sp.md_sanpham_id
                        left join md_donvitinhsanpham dvtsp on dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                        left join c_danhsachdathang dh on dh.c_danhsachdathang_id = dsdh.c_danhsachdathang_id
                        left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id
                   WHeRe 1=1 
                        {where_vaitro} 
                        {Security.test_InjectionSQL(filter, "", "")}
                ) P 
                WHeRe 
                    1=1 {filterP} {whereSE}
                order by 
                    {orderbyP}
            ";

            //throw new Exception(sql);
            sql = ADmin_ConvertStringToCode.Avariable(context, sql, "dsdh.c_dongdsdh_id", "", null, db).Replace("'", "''");
            string sql_select = $@"exec [dbo].[admin_excutesql] N'{sql}',N'',1";
            var dt_select = Mbg.Data.SqlClient.SqlHelper.GetData(sql_select);

            int count = dt_select.Rows.Count;
            if (limit < 999999)
            {
                string sqlcount = $@"exec [dbo].[admin_excutesql] N'{sql}',N'',0";
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
