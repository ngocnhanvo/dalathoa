using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;

public partial class PrintControllers_MD_00_BaoCaoKeHoach_TongHopDonHangBan : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KH] Tổng hợp đơn hàng bán.repx";
        string nameRpt = "TỔNG HỢP ĐƠN HÀNG BÁN {ngayin}";
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
        var ngayin = DateTime.Now.ToString(fmtDate);
        var tuStr = tu.ToNullableDateTime(fmtDate).Value.ToString(fmtDate);
        var denStr = den.ToNullableDateTime(fmtDate).Value.ToString(fmtDate);
        string sql = $@"
		    declare @tu datetime = convert(datetime, N'{tu} 00:00:00', 103)
            declare @den datetime = convert(datetime, N'{den} 23:59:59', 103)

            select
	            N'{ngayin}' ngayin,
                N'{tuStr}' tungay,
                N'{denStr}' denngay,
	            dtkd.ma_dtkd as maNCC,
	            dhb.so_po as dhb,
	            sp.ma_sanpham as maVTHH,
	            sp.mota_tiengviet as tenVTHH,
	            dvtsp.ten_dvt as dvt,
	            cdh.giadoichieu as dongia,
	            cdh.sl_dathang - isnull(cdh.sl_giamhanngach, 0) as slDMH,
	            cdh.sl_hanngach as slDG
            from c_danhsachdathang dhb
                left join c_dongdsdh cdh on cdh.c_danhsachdathang_id = dhb.c_danhsachdathang_id
	            left join md_sanpham sp on sp.md_sanpham_id = cdh.md_sanpham_id
	            left join md_donvitinhsanpham dvtsp on dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
	            left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = dhb.md_doitackinhdoanh_id
            where
	            1=1
                and dhb.trangthai not in ('SOANTHAO', 'DANHAN')
	            and dhb.hangiaohang_po between @tu and @den
                {dtkd}
            order by
	            dhb.so_po, sp.ma_sanpham
		";

        return sql;
    }
}

