using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_Xuatnoibo_PDN_lenhXuatKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] LỆNH XUẤT KHO.repx";
        string nameRpt = "LỆNH XUẤT KHO {ngaylap}";
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
        string loaiCT0 = Helper.arrLoaiCT_LXK[0];
        string loaiCT1 = Helper.arrLoaiCT_LXK[1];
        string mau0 = Helper.arrMau_LXK[0];
        string mau1 = Helper.arrMau_LXK[1];
        string sql = $@"
		    declare @id nvarchar(32)= '{id}'
            select
                (case when vcnb.bosung = 1 then upper(N'{loaiCT1}') else upper(N'{loaiCT0}') end) as loaiCT,
                (case when vcnb.bosung = 1 then N'{mau1}' else N'{mau0}' end) as mauCT,
	            vcnb.sochungtu as sophieu,
	            format(vcnb.ngaydenghi, 'dd/MM/yyyy') as ngaylap,
	            kho.ten_kho as kho,
	            vcnb.donhang_thamchieu as donhang,
	            sp.ma_sanpham as maVTHH,
                sp.mota_tiengviet as tenVTHH,
                dvt.ten_dvt as dvt,
	            cdvc.sl_muonxuat as slct,
                null as sltn,
	            null as sldat,
	            null as slkdat,
                pb.ten_phongban as makh,
                null as ghichu
            from md_xuatkhonb vcnb
                left join md_kho kho on kho.md_kho_id = vcnb.tukho
                left join md_xuatkhonb_cdh cdvc on cdvc.md_xuatkhonb_id = vcnb.md_xuatkhonb_id
                left join md_sanpham sp on sp.md_sanpham_id = cdvc.md_sanpham_id
                left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                left join ad_department pb on pb.md_phongban_id = vcnb.xuatden
            where
                1=1
                and vcnb.md_xuatkhonb_id = @id
                and cdvc.sl_muonxuat > 0
            order by
	            sp.ma_sanpham
		";
        return sql;
    }
}