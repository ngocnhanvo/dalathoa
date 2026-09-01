using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_NhapkhotuNCC_lenhNhapKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] PHIẾU NHẬP KHO.repx";
        string nameRpt = "PHIẾU NHẬP KHO {ngaylap}";
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
        string id = context.Request.QueryString["id"];
        string loaiCT = Helper.arrLoaiCT_LNK[2];
        string sql = $@"
		    declare @id nvarchar(32)= '{id}'
            select
                upper(N'{loaiCT}') as loaiCT,
	            vcnb.sochungtu as sophieu,
	            format(vcnb.ngaychuyen, 'dd/MM/yyyy') as ngaylap,
	            kho.ten_kho as kho,
	            vcnb.donhang_thamchieu as donhang,
                vcnb.so_donmuahang as soDMH,
	            sp.ma_sanpham as maVTHH,
                sp.mota_tiengviet as tenVTHH,
                dvt.ten_dvt as dvt,
	            cdvc.sl_muonnhap as slct,
                cdvc.sl_nhap as sltn,
	            null as sldat,
	            null as slkdat,
                dtkd.ma_dtkd as makh,
                dmh.donhang_thamchieu as ghichu
            from md_nhapkho_ncc vcnb
                left join md_kho kho on kho.md_kho_id = vcnb.kho
                left join md_nhapkho_ncc_dh cdvc on cdvc.md_nhapkho_ncc_id = vcnb.md_nhapkho_ncc_id
                left join md_sanpham sp on sp.md_sanpham_id = cdvc.md_sanpham_id
                left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = vcnb.md_doitackinhdoanh_id
                left join c_donmuahang dmh on dmh.sochungtu = cdvc.so_dmh
            where
                vcnb.trangthai != '{Helper.SOANTHAO}'
                and vcnb.md_nhapkho_ncc_id = @id
                and cdvc.sl_nhap > 0
            order by
	            sp.ma_sanpham
		";
        return sql;
    }
}