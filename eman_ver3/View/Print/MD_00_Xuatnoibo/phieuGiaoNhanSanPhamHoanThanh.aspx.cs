using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_MD_00_Xuatnoibo_phieuGiaoNhanSanPhamHoanThanh : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string file = Server.MapPath(Security.UrlBase() + "/ReportsStorage/[SX] Phiếu Giao Nhận Sản Phẩm Hoàn Thành.repx");
        string nameTemp = "[SX] Phiếu Giao Nhận Sản Phẩm Hoàn Thành.repx";
        string nameRpt = "Phiếu Giao Nhận Sản Phẩm Hoàn Thành {sochungtu}";
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
        tbl.Columns.Add("sum_giao", Type.GetType("System.Double"));
        //Header
        var ngaydenghi = tbl.Rows[0]["ngaydenghi"] as DateTime?;
        tbl.Rows[0]["dd"] = ngaydenghi.Value.ToString("dd");
        tbl.Rows[0]["MM"] = ngaydenghi.Value.ToString("MM");
        tbl.Rows[0]["yyyy"] = ngaydenghi.Value.ToString("yyyy");
        //Footer
        int lastRow = tbl.Rows.Count - 1;
        tbl.Rows[lastRow]["sum_giao"] = double.Parse(tbl.Compute("Sum(giao)", string.Empty).ToString());

        var giao = ReportViewer1.Report.Report.FindControl("giao", true);
        giao.DataBindings[0].FormatString = sothapphan;
        var sum_giao = ReportViewer1.Report.Report.FindControl("sum_giao", true);
        sum_giao.DataBindings[0].FormatString = sothapphan;
    }

    public String CreateSql(HttpContext context)
    {
        string md_xuatkhonb_id = context.Request.QueryString["id"];
        string oper = context.Request.QueryString["oper"];
        string sql = string.Format(@"
		    select 
	            xb.sochungtu
	            , null as nguoigiao
	            , (select ten_to from md_phanxuong_to where md_to_id = kho.md_to_id) as tsx_giao
	            , (select ten_phanxuong from md_phanxuong where md_phanxuong_id = kho.md_phanxuong_id) as px_giao
	            , null as nguoinhan
	            , (select ten_to from md_phanxuong_to where md_to_id = xb.md_to_id) as tsx_nhan
	            , (select ten_phanxuong from md_phanxuong where md_phanxuong_id = xb.md_phanxuong_id) as px_nhan
	            , nknb_cdh.tenhang as thegiaoviec
                , substring(nknb_cdh.lsx_to, 0, 13) as ghichu
                , sp.ma_sanpham as ma_vthh
	            , dvtsp.ten_dvt as dvt
	            , nknb_cdh.sl_thucxuat as giao
                , xb.ngaydenghi
            from md_xuatkhonb xb
	            left join md_xuatkhonb_cdh nknb_cdh on xb.md_xuatkhonb_id = nknb_cdh.md_xuatkhonb_id
                left join md_sanpham sp on nknb_cdh.md_sanpham_id = sp.md_sanpham_id
                left join md_donvitinhsanpham dvtsp on nknb_cdh.md_donvitinhsanpham_id = dvtsp.md_donvitinhsanpham_id
                left join md_kho kho on xb.tukho = kho.md_kho_id
            where 
                nknb_cdh.md_xuatkhonb_id = '{0}'
                and nknb_cdh.sl_thucxuat > 0
            order by xb.ngaydenghi, nknb_cdh.tenhang, sp.ma_sanpham
		"
        , md_xuatkhonb_id
        );
        //
        return sql;
    }
}




