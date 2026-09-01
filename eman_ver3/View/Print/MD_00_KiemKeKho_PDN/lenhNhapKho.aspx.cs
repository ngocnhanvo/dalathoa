using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_KiemKeKho_PDN_lenhNhapKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] LỆNH NHẬP KHO.repx";
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
        string loaiCT = Helper.arrLoaiCT_LNK[4];
        string sql = $@"
		    declare @khid nvarchar(32)= '{id}'
            select
                upper(N'{loaiCT}') as loaiCT,
	            vcnb.sochungtu as sophieu,
	            format(vcnb.ngay_kiemke, 'dd/MM/yyyy') as ngaylap,
	            kho.ten_kho as kho,
	            null as donhang,
                null as soDMH,
	            sp.ma_sanpham as maVTHH,
                sp.mota_tiengviet as tenVTHH,
                dvt.ten_dvt as dvt,
	            cdvc.sl_sosach as sltn,
                isnull(cdvc.sl_demduoc, 0) as slct,
	            null as sldat,
	            null as slkdat,
                null as makh,
                null as ghichu
            from md_kiemke vcnb
                left join md_kho kho on kho.md_kho_id = vcnb.md_kho_id
                left join md_kiemke_cdh cdvc on cdvc.md_kiemke_id = vcnb.md_kiemke_id
                left join md_sanpham sp on sp.md_sanpham_id = cdvc.md_sanpham_id
                left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
            where
                vcnb.ma_kiemke != '{Helper.SOANTHAO}'
                and vcnb.md_kiemke_id = @khid
                and isnull(cdvc.sl_sosach, 0) - isnull(cdvc.sl_demduoc, 0) > 0
            order by
	            sp.ma_sanpham
		";
        return sql;
    }
}