using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_Baocaoketoan_CongNoPhaiThu : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] CÔNG NỢ PHẢI THU.repx";
        string nameRpt = "CÔNG NỢ PHẢI THU {ngayin}";
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
		    declare @tu datetime = convert(datetime, N'{tu} 00:00:00', 103)
            declare @den datetime = convert(datetime, N'{den} 23:59:59', 103)

            select
                N'{ngayin}' ngayin,
                N'{tuStr}' tungay,
                N'{denStr}' denngay,

                ncc.sochungtu sct,
                format(ncc.ngaychuyen, '{fmtDate}') ngaynk,
                dmh.so_po donhang,

                dtkd.ma_dtkd maDTKD,
                dtkd.ten_dtkd tenDTKD,

                sp.ma_sanpham maVTHH,
                sp.mota_tiengviet tenVTHH,
                dvtsp.ten_dvt dvt,

                dmhDH.sl_dathang sldh,
                dh.sl_xuat slnk,

                dmhDH.gianhap dongia,
                thue.giatri/100 vat,

                T.thanhtien,
                FLOOR(T.thanhtien + T.thanhtien * thue.giatri / 100) thanhtienThue

            from md_xuatban ncc

            left join c_danhsachdathang dmh
                on dmh.c_danhsachdathang_id = ncc.c_danhsachdathang_id

            left join c_dongdsdh dmhDH
                on dmhDH.c_danhsachdathang_id = ncc.c_danhsachdathang_id

            left join md_xuatban_cdh dh
                on dh.md_xuatban_id = ncc.md_xuatban_id
                and dh.md_sanpham_id = dmhDH.md_sanpham_id

            left join md_doitackinhdoanh dtkd
                on dtkd.md_doitackinhdoanh_id = dmh.md_doitackinhdoanh_id

            left join md_sanpham sp
                on sp.md_sanpham_id = dh.md_sanpham_id

            left join md_donvitinhsanpham dvtsp
                on dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id

            left join md_thue_sanpham thue
                on thue.md_thue_sanpham_id = isnull(dmhDH.thue, sp.md_thue_sanpham_id)

            cross apply (
                select FLOOR(dmhDH.gianhap * dh.sl_xuat) thanhtien
            ) T

            where
                ncc.trangthai in ('{Helper.HIEULUC}')
                and ncc.ngaychuyen >= @tu
                and ncc.ngaychuyen <= @den
                and dh.sl_xuat > 0
                {dtkd}

            order by
                ncc.ngaychuyen,
                sp.ma_sanpham
		";

        return sql;
    }
}