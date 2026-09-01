using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;

public partial class PrintControllers_MD_00_Baocaoketoan_BaoCaoSoSanhChiPhiSXBOM : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "(KT) Báo cáo so sanh chi phí sản xuất BOM.repx";
        string nameRpt = "Báo cáo so sanh chi phí sản xuất BOM {ngayin}";
        string sql = CreateSql(context);

        var task = new System.Threading.Tasks.Task(() =>
        {
            viewReport(sql);
        });

        PrintAnco2.exportDataWithType(task, sql, inPDF, nameTemp, nameRpt, ReportViewer1, true);
    }

    public void viewReport(String SqlQuery)
    {
        var tbl = ((DataSet)ReportViewer1.Report.DataSource).Tables[0];
        if (tbl.Rows.Count > 0)
        {
        }
    }

    public String CreateSql(HttpContext context)
    {
        string tu = context.Request.QueryString["tu"].removeAllSpaceOrTrimText(true).Replace("-", "/");
        string den = context.Request.QueryString["den"].removeAllSpaceOrTrimText(true).Replace("-", "/");
        string dtkd = context.Request.QueryString["dtkd"].removeAllSpaceOrTrimText(true).Replace("'", "''");
        if (!string.IsNullOrWhiteSpace(dtkd))
            dtkd = $@"and dtkd.ma_dtkd = N'{dtkd}'";

        var fmtDate = Helper.fmtDate;
        string sql = $@"
		    declare @tu datetime = convert(datetime, N'{tu} 00:00:00', 103)
            declare @den datetime = convert(datetime, N'{den} 23:59:59', 103)
            declare @fmtD nvarchar(10) = '{fmtDate}';

            select 
                null as xuong,
                format(getdate(), '{fmtDate}') as ngayin,
	            format(@tu, '{fmtDate}') as tungay,
	            format(@den, '{fmtDate}') as denngay,
	            sp.ma_sanpham as maVTHH,
	            sp.mota_tiengviet as tenVTHH,
	            dvt.ten_dvt as dvt,
	            null as dg,
	            sum(cdh.sl_dagiao) as slxk,
	            null as gtvtxk,
	            sum(cdh.sl_chiato) as sltb,
	            null as gtvttb,
	            sum(cdh.sl_chiato - isnull(cdh.sl_dagiao, 0)) as slcl,
	            null as gtcl
            from md_lenhsanxuat_tosx_cdh cdh
	            left join md_lenhsanxuat lsx on lsx.md_lenhsanxuat_id = cdh.md_lenhsanxuat_id
	            left join c_danhsachdathang dh on lsx.donhang_thamchieu = dh.so_po
	            left join md_lenhsanxuat2 lsx2 on lsx2.sochungtu = cdh.lsxCT
	            left join md_sanpham sp on sp.md_sanpham_id = cdh.md_sanpham_id
	            left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
            where
	            dh.hangiaohang_po between @tu and @den
            group by
	            sp.ma_sanpham,
	            sp.mota_tiengviet,
	            dvt.ten_dvt
            order by
                sp.ma_sanpham
		";

        return sql;
    }
}

