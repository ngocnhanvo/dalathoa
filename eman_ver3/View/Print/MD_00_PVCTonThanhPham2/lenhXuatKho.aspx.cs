using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_PVCTonThanhPham2_lenhXuatKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] LỆNH XUẤT KHO - Điều chuyển.repx";
        string nameRpt = "LỆNH XUÁT KHO {ngaylap}";
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
        string loaiCT = Helper.arrLoaiCT_LXK[3];
        string mau0 = Helper.arrMau_LXK[0];
        string sql = $@"
		    declare @khid nvarchar(32)= '{id}'
            select
                upper(N'{loaiCT}') as loaiCT,
                N'{mau0}' as mauCT,
	            vcnb.sochungtu as sophieu,
	            format(vcnb.ngaychuyen, 'dd/MM/yyyy') as ngaylap,
	            tukho.ten_kho as tukho,
                denkho.ten_kho as denkho,
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
                left join md_kho tukho on tukho.md_kho_id = vcnb.tukho
                left join md_kho denkho on denkho.md_kho_id = vcnb.denkho
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