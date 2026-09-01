<%@ WebHandler Language="C#" Class="DanhSachHangHoaCoGiaMua" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
public class DanhSachHangHoaCoGiaMua : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            String nccid = context.Request.QueryString["nccid"];
            string id_vaitro = Security.id_vaitro(context);
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
                declare @nccid nvarchar(32) = '${nccid}';
                sELeCt P.* 
                fRoM (
                   sELeCt
                        sp.ma_sanpham as md_sanpham_id,
                        sp.mota_tiengviet as mota_tiengviet,
                        dvtsp.ten_dvt as md_donvitinhsanpham_id,
                        1 as sl_dathang,
                        dmh.dongiamua,
                        1 as sl_nhaphang,
                        isnull(dsdh.dongiamua,0) as thanhtien,
                        dh.sochungtu,
                        dtkd.ten_dtkd as ten_khachhang,
                        dh.diachigiaohang,
                        {ROW_NUMBER}
                   fRoM 
                        md_sanpham sp
                        left join md_donvitinhsanpham dvtsp on dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                        outer apply (
                            select top 1 cdh.dongiamua
                            from c_donmuahang dmh
                            left join c_donmuahang_cdmh cdh on cdh.c_donmuahang_id = dmh.c_donmuahang_id
                            where 
                                sp.md_sanpham_id = cdh.md_sanpham_id
                                and dmh.md_doitackinhdoanh_id = @nccid
                            order by dmh.ngaygiaohang desc
                        ) dmh
                   WHeRe 1=1
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
