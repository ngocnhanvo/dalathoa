using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_XuatBanThanhLy_lenhXuatKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] PHIẾU XUẤT KHO.repx";
        string nameRpt = "PHIẾU XUẤT KHO {ngaylap}";
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
        string loaiCT0 = Helper.arrLoaiCT_LXK[6];
        string loaiCT1 = Helper.arrLoaiCT_LXK[1];
        string mau0 = Helper.arrMau_LXK[0];
        string mau1 = Helper.arrMau_LXK[1];
        string sql = $@"
		    declare @id nvarchar(32)= '{id}'
            select
                N'{loaiCT0}' as loaiCT,
                N'{mau0}' as mauCT,
	            vcnb.sochungtu as sophieu,
	            format(vcnb.ngaychuyen, 'dd/MM/yyyy') as ngaylap,
	            kho.ten_kho as kho,
	            vcnb.donhang_thamchieu as donhang,
	            sp.ma_sanpham as maVTHH,
                sp.mota_tiengviet as tenVTHH,
                dvt.ten_dvt as dvt,
	            cdvc.tong_sl_xuat as slct,
                cdvc.sl_xuat as sltn,
	            null as sldat,
	            null as slkdat,
                dtkd.ma_dtkd as makh,
                cdvc.mota as ghichu
            from md_xuatban vcnb
                left join md_kho kho on kho.md_kho_id = vcnb.tukho
                left join md_xuatban_cdh cdvc on cdvc.md_xuatban_id = vcnb.md_xuatban_id
                left join md_sanpham sp on sp.md_sanpham_id = cdvc.md_sanpham_id
                left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = vcnb.md_doitackinhdoanh_id
            where
                1=1
                and vcnb.md_xuatban_id = @id
                and cdvc.sl_xuat > 0
            order by
	            sp.ma_sanpham
		";
        return sql;
    }
}