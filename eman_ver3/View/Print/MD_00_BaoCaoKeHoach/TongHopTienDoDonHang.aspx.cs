using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;

public partial class PrintControllers_MD_00_BaoCaoKeHoach_TongHopTienDoDonHang : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "(GD) Tổng hợp tiến độ đơn hàng.repx";
        string nameRpt = "TỔNG HỢP TIẾN ĐỘ ĐƠN HÀNG {ngayin}";
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
		    DECLARE @tu DATETIME = CONVERT(DATETIME, N'{tu} 00:00:00', 103)
            DECLARE @den DATETIME = CONVERT(DATETIME, N'{den} 23:59:59', 103)

            ;WITH A AS
            (
                SELECT
                    dmh.so_po AS donhang,
                    dmh.hangiaohang_po,
                    sp.ma_sanpham AS maVTHH,
                    sp.mota_tiengviet AS tenVTHH,
                    dvtsp.ten_dvt AS dvt,
                    SUM(cdh.sl_dat - ISNULL(cdh.sl_giamhanngach,0)) AS sldh,
                    SUM(ISNULL(cdh.sl_nhapkho,0)) AS tontp,
                    SUM(
                        CASE 
                            WHEN ISNULL(cdh.sl_datncc,0) > 0 
                            THEN ISNULL(cdh.sl_dahoanthanh,0) 
                            ELSE ISNULL(cdh.sl_danhapkho,0) 
                        END
                    ) AS sltp,
                    SUM(cdh.sl_dat - ISNULL(cdh.sl_giamhanngach,0)) AS sltp0
                FROM c_danhsachdathang dmh
                    LEFT JOIN md_doitackinhdoanh dtkd
                        ON dtkd.md_doitackinhdoanh_id = dmh.md_doitackinhdoanh_id
                    LEFT JOIN md_lenhsanxuat lsx
                        ON lsx.donhang_thamchieu = dmh.so_po
                    LEFT JOIN md_lenhsanxuat_tosx_cdh cdh
                        ON cdh.md_lenhsanxuat_id = lsx.md_lenhsanxuat_id
                        AND cdh.stt = 9999
                    LEFT JOIN md_sanpham sp
                        ON sp.md_sanpham_id = cdh.md_sanpham_id
                    LEFT JOIN md_donvitinhsanpham dvtsp
                        ON dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                WHERE
                    dmh.hangiaohang_po BETWEEN @tu AND @den
                    {dtkd}
                GROUP BY
                    cdh.md_sanpham_id,
                    dmh.so_po,
                    dmh.hangiaohang_po,
                    sp.ma_sanpham,
                    sp.mota_tiengviet,
                    dvtsp.ten_dvt
            ),

            THO AS
            (
                SELECT
                    lsx.donhang_thamchieu AS donhang,
                    cdh.macuoi,
                    SUM(ISNULL(cdh.sl_nhapkho,0)) AS tontho,
                    SUM(
                        CASE 
                            WHEN ISNULL(cdh.sl_datncc,0) > 0 
                            THEN ISNULL(cdh.sl_dahoanthanh,0) 
                            ELSE ISNULL(cdh.sl_danhapkho,0) 
                        END
                    ) AS sltho,
                    SUM(ISNULL(cdh.sl_chiato,0)) AS sltho0
                FROM md_lenhsanxuat lsx
                    INNER JOIN md_lenhsanxuat_tosx_cdh cdh
                        ON cdh.md_lenhsanxuat_id = lsx.md_lenhsanxuat_id
                    INNER JOIN md_sanpham spc
                        ON spc.md_sanpham_id = cdh.md_sanpham_id
                WHERE
                    cdh.stt = 9998
                    AND spc.vattu = 0
                GROUP BY
                    lsx.donhang_thamchieu,
                    cdh.macuoi
            )

            SELECT
                N'{ngayin}' ngayin,
                N'{tuStr}' tungay,
                N'{denStr}' denngay,
                A.*,
                ISNULL(A.sltp0 - A.sltp - A.tontp,0) AS sltp2,
                ISNULL(T.tontho,0) AS tontho,
                ISNULL(T.sltho,0) AS sltho,
                ISNULL(T.sltho0 - T.sltho,0) AS sltho2
            FROM A
            LEFT JOIN THO T
                ON T.donhang = A.donhang
                AND T.macuoi = A.maVTHH
            ORDER BY
                A.donhang,
                A.maVTHH
		";

        return sql;
    }
}

