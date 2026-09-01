using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_PVCNoiBo_lenhNhapKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] LỆNH NHẬP KHO - Điều chuyển.repx";
        string nameRpt = "LỆNH NHẬP KHO {ngaylap}";
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
        string loaiCT = Helper.arrLoaiCT_LNK[1];
        string sql = $@"
		    declare @khid nvarchar(32)= '{id}'
            select
                upper(N'{loaiCT}') as loaiCT,
	            vcnb.sochungtu as sophieu,
	            format(isnull(vcnb.ngaychuyen, vcnb.ngaydenghi), 'dd/MM/yyyy') as ngaylap,
	            tkho.ten_kho as tukho,
                dkho.ten_kho as denkho,
	            vcnb.donhang_thamchieu as donhang,
	            sp.ma_sanpham as maVTHH,
                sp.mota_tiengviet as tenVTHH,
                dvt.ten_dvt as dvt,
	            cdvc.soluong_muonchuyen as slct,
                cdvc.soluong_dichchuyen as sltn,
	            null as sldat,
	            null as slkdat,
                null as makh
            from md_vanchuyennoibo vcnb
                left join md_kho tkho on tkho.md_kho_id = vcnb.tukho
                left join md_kho dkho on dkho.md_kho_id = vcnb.denkho
                left join md_vanchuyennoibo_cdvc cdvc on cdvc.md_vanchuyennoibo_id = vcnb.md_vanchuyennoibo_id
                left join md_sanpham sp on sp.md_sanpham_id = cdvc.md_sanpham_id
                left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
            where
                vcnb.md_trangthai_id != '{Helper.SOANTHAO}'
                and vcnb.md_vanchuyennoibo_id = @khid
                and cdvc.soluong_dichchuyen > 0
            order by
	            sp.ma_sanpham
		";
        return sql;
    }
}