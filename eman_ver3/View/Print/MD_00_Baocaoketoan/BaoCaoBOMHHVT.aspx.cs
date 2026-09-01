using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;

public partial class PrintControllers_MD_00_Baocaoketoan_BaoCaoBOMHHVT : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] BOM HHVT mới nhất.repx";
        string nameRpt = "BOM HHVT mới nhất {ngayin}";
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
    }

    public String CreateSql(HttpContext context)
    {
        string kho = context.Request.QueryString["kho"].removeAllSpaceOrTrimText(true).Replace("'", "''");
        if (!string.IsNullOrWhiteSpace(kho))
            kho = $@"and sp.khomacdinh = N'{kho}'";
        //San pham
        string masp = context.Request.QueryString["masp"].removeAllSpaceOrTrimText(true).Replace("'", "''");
        if (!string.IsNullOrWhiteSpace(masp))
            masp = $@"and sp.ma_sanpham = N'{masp}'";

        var fmtDate = Helper.fmtDate;
        var ngayin = DateTime.Now.ToString(fmtDate);
        string sql = $@"          
            select 
                N'{ngayin}' as ngayin,
                sp.ma_sanpham as maHH,
                bom.ten_phienban as phienban,
                bvtsp.ma_sanpham as maVT,
                bvt.soluong as sl
            from md_sanpham sp

            outer apply (
                select top (1)
                    bom.md_sanpham_bom_id,
                    bom.ten_phienban
                from md_sanpham_bom bom
                where bom.md_sanpham_id = sp.md_sanpham_id
                    and isnull(bom.ten_phienban,'') <> ''
                order by bom.ngay_hieuluc desc
            ) bom

            left join md_sanpham_bom_vattu bvt
                on bvt.md_sanpham_bom_id = bom.md_sanpham_bom_id

            left join md_sanpham bvtsp
                on bvtsp.md_sanpham_id = bvt.md_sanpham_id

            where
                (sp.sanpham = 1 or sp.ban_thanhpham = 1)
                {kho} {masp}

            order by
                sp.ma_sanpham,
                bom.ten_phienban,
                bvtsp.ma_sanpham
		";

        return sql;
    }
}

