using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_Baocaoketoan_CongNoPhaiTraNhom : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] CÔNG NỢ PHẢI TRẢ (Nhóm KHNCC).repx";
        string nameRpt = "CÔNG NỢ PHẢI TRẢ {ngayin}";
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
            var tendtkds = tbl.AsEnumerable().Select(s => s.Field<string>("tenDTKD")).Distinct().ToList();
            if (tendtkds.Count > 1)
            {
                tbl.Rows[0]["tenDTKD"] = "";
            }
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
		    DECLARE @tu DATETIME = CONVERT(DATETIME, N'{tu} 00:00:00', 103)
            DECLARE @den DATETIME = CONVERT(DATETIME, N'{den} 23:59:59', 103)

            ;WITH data AS (
                SELECT
                    ncc.sochungtu,
                    ncc.ngaychuyen,
                    dmh.so_donmuahang,
                    dmh.donhang_thamchieu,
                    dtkd.ma_dtkd,
                    dtkd.ten_dtkd,
                    sp.ma_sanpham,
                    sp.mota_tiengviet,
                    dvtsp.ten_dvt,
                    dmhDH.sl_dadat,
                    dh.sl_nhap,
                    dmhDH.dongiamua,
                    thue.giatri,
                    FLOOR(dmhDH.dongiamua * dh.sl_nhap) AS thanhtien
                FROM md_nhapkho_ncc ncc
                    LEFT JOIN md_nhapkho_ncc_dh dh 
                        ON dh.md_nhapkho_ncc_id = ncc.md_nhapkho_ncc_id
                    LEFT JOIN c_donmuahang dmh 
                        ON dmh.sochungtu = dh.so_dmh
                    LEFT JOIN c_donmuahang_cdmh dmhDH 
                        ON dmhDH.c_donmuahang_id = dmh.c_donmuahang_id
                    LEFT JOIN c_kehoachdathang kh 
                        ON kh.c_kehoachdathang_id = dmh.c_kehoachdathang_dhncc_id
                    LEFT JOIN md_doitackinhdoanh dtkd 
                        ON dtkd.md_doitackinhdoanh_id = dmh.md_doitackinhdoanh_id
                    LEFT JOIN md_sanpham sp 
                        ON sp.md_sanpham_id = dh.md_sanpham_id
                    LEFT JOIN md_donvitinhsanpham dvtsp 
                        ON dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                    LEFT JOIN md_thue_sanpham thue 
                        ON thue.md_thue_sanpham_id = dmhDH.thue
                WHERE
                    dmhDH.md_sanpham_id = dh.md_sanpham_id
                    AND ncc.trangthai IN ('{Helper.HIEULUC}')
                    AND ncc.ngaychuyen BETWEEN @tu AND @den
                    AND dh.sl_nhap > 0
                    {dtkd}
            )

            SELECT
                N'{ngayin}' ngayin,
                N'{tuStr}' tungay,
                N'{denStr}' denngay,
                sochungtu AS sct,
                FORMAT(ngaychuyen, '{fmtDate}') AS ngaynk,
                so_donmuahang AS donhang,
                donhang_thamchieu AS so_po,
                ma_dtkd AS maDTKD,
                ten_dtkd AS tenDTKD,
                ma_sanpham AS maVTHH,
                mota_tiengviet AS tenVTHH,
                ten_dvt AS dvt,
                sl_dadat AS sldh,
                sl_nhap AS slnk,
                dongiamua AS dongia,
                giatri / 100 AS vat,
                thanhtien,
                FLOOR(thanhtien + thanhtien * giatri / 100) AS thanhtienThue,
                ROW_NUMBER() OVER (PARTITION BY ma_dtkd ORDER BY ma_dtkd ASC) AS cr
            FROM data
            ORDER BY
                ma_dtkd,
                ngaychuyen,
                ma_sanpham
		";
        return sql;
    }
}