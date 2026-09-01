using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;

public partial class PrintControllers_MD_00_Baocaoketoan_BangKeXuatKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] BẢNG KÊ XUẤT KHO.repx";
        string nameRpt = "BẢNG KÊ XUẤT KHO {ngayin}";
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
            var khos = tbl.AsEnumerable().Select(s => s.Field<string>("kho")).Distinct().ToList();
            if (khos.Count > 1)
            {
                tbl.Rows[0]["kho"] = "";
            }

            var bophans = tbl.AsEnumerable().Select(s => s.Field<string>("bophan")).Distinct().ToList();
            if (bophans.Count > 1)
            {
                tbl.Rows[0]["bophan"] = "";
            }
        }
    }

    public String CreateSql(HttpContext context)
    {
        string donhanghuy = System.IO.File.ReadAllText(ExcuteSignalRStatic.mapPathSignalR("~/App_Data/JsonData/0_donhanghuy.txt"));
        donhanghuy = donhanghuy.Replace("\r\n", "',N'");
        string tu = context.Request.QueryString["tu"].removeAllSpaceOrTrimText(true).Replace("-", "/");
        string den = context.Request.QueryString["den"].removeAllSpaceOrTrimText(true).Replace("-", "/");
        //Kho
        string kho = context.Request.QueryString["kho"].removeAllSpaceOrTrimText(true).Replace("'", "''");
        if (!string.IsNullOrWhiteSpace(kho))
            kho = $@"and kh.md_kho_id = N'{kho}'";
        //Bo Phan
        string boPhan = context.Request.QueryString["boPhan"].removeAllSpaceOrTrimText(true).Replace("'", "''");
        string boPhanSel = boPhan;
        if (!string.IsNullOrWhiteSpace(boPhan))
            boPhan = $@"and (pb.md_phongban_id = N'{boPhan}' or pbCha1.md_phongban_id = N'{boPhan}' or pbCha2.md_phongban_id = N'{boPhan}')";
        //San pham
        string masp = context.Request.QueryString["masp"].removeAllSpaceOrTrimText(true).Replace("'", "''");
        if (!string.IsNullOrWhiteSpace(masp))
            masp = $@"and sp.ma_sanpham = N'{masp}'";

        var fmtDate = Helper.fmtDate;
        var ngayin = DateTime.Now.ToString(fmtDate);
        var tuStr = tu.ToNullableDateTime(fmtDate).Value.ToString(fmtDate);
        var denStr = den.ToNullableDateTime(fmtDate).Value.ToString(fmtDate);
        string sql = $@"
		    DECLARE @tu DATETIME = CONVERT(DATETIME, N'{tu} 00:00:00', 103)
            DECLARE @den DATETIME = CONVERT(DATETIME, N'{den} 23:59:59', 103)

            DECLARE @pbSel NVARCHAR(MAX) = (
                SELECT ten_phongban 
                FROM ad_department 
                WHERE md_phongban_id = '{boPhanSel}'
            )

            ;WITH data AS (
                SELECT
                    kgd.dongnhapxuat,
                    kgd.ngaychuyen,
                    kh.ten_kho,
                    pb.ten_phongban AS bophan2,
                    sp.ma_sanpham,
                    sp.mota_tiengviet,
                    dvtsp.ten_dvt,
                    kgd.soluong_dichchuyen,
                    ISNULL(pb.ten_phongban, dtkd.ma_dtkd) AS xuatden
                FROM md_kho_giaodich kgd
                    LEFT JOIN md_kho kh 
                        ON kh.md_kho_id = kgd.md_kho_id
                    LEFT JOIN md_sanpham sp 
                        ON sp.md_sanpham_id = kgd.md_sanpham_id
                    LEFT JOIN md_donvitinhsanpham dvtsp 
                        ON dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                    LEFT JOIN md_xuatkhonb xnb 
                        ON xnb.sochungtu = kgd.dongnhapxuat
                    LEFT JOIN ad_department pb 
                        ON pb.md_phongban_id = xnb.xuatden
                    LEFT JOIN ad_department pbCha1 
                        ON pbCha1.md_phongban_id = pb.phongbanChaId
                    LEFT JOIN ad_department pbCha2 
                        ON pbCha2.md_phongban_id = pbCha1.phongbanChaId
                    LEFT JOIN md_doitackinhdoanh dtkd 
                        ON dtkd.md_doitackinhdoanh_id = xnb.xuatden
                WHERE
                    kgd.kieuchuyen = N'{Helper.XuatKho}'
                    AND kgd.ngaychuyen BETWEEN @tu AND @den
                    AND kgd.soluong_dichchuyen > 0
                    AND ISNULL(kgd.mota,'') NOT LIKE N'%,%'
                    AND ISNULL(kgd.mota,'') NOT IN ('{donhanghuy}')
                    {kho} {boPhan} {masp}
            )

            SELECT
                N'{ngayin}' ngayin,
                N'{tuStr}' tungay,
                N'{denStr}' denngay,
                dongnhapxuat AS sct,
                FORMAT(ngaychuyen, '{fmtDate}') AS nxk,
                ten_kho AS kho,
                @pbSel AS bophan,
                bophan2,
                ma_sanpham AS maVTHH,
                mota_tiengviet AS tenVTHH,
                ten_dvt AS dvt,
                soluong_dichchuyen AS soluong,
                xuatden
            FROM data
            ORDER BY
                ngaychuyen,
                ma_sanpham
		";

        //throw new Exception(sql);
        return sql;
    }
}

