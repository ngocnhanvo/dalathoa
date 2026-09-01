using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_01_KHLayTonTho_phieuYCKiemTraTonKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KH] PHIẾU YÊU CẦU KIỂM TRA TỒN KHO.repx";
        string nameRpt = "PHIẾU YÊU CẦU KIỂM TRA TỒN KHO {ngaylap}";
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
        string khdhId = context.Request.QueryString["id"];
        string sql = $@"
		    declare @khid nvarchar(32)= '{khdhId}'

            select
	            vcnb.sochungtu as sophieu,
	            format(vcnb.ngaychuyen, 'dd/MM/yyyy') as ngaylap,
	            kho.ten_kho as kho,
	            kh.donhang_thamchieu as donhang,
	            sp.ma_sanpham as maVTHH,
	            lttp.sldh,
	            lttp.sltk,
	            cdvc.soluong_toida as sldklt,
	            null as sldat,
	            null as slkdat,
	            null as sllt
            from c_kehoachdathang kh
            left join md_vanchuyennoibo vcnb on kh.c_kehoachdathang_id = vcnb.c_doichieuhangton_id
            left join md_kho kho on kho.md_kho_id = vcnb.tukho
            left join md_vanchuyennoibo_cdvc cdvc on cdvc.md_vanchuyennoibo_id = vcnb.md_vanchuyennoibo_id
            left join md_sanpham sp on sp.md_sanpham_id = cdvc.md_sanpham_id
            right join (
	            select 
		            sum(klhttp.sl_lsx) as sldh,
		            sum(isnull(klhttp.sl_tonkho, 0)) as sltk,
		            klhttp.md_sanpham_id,
		            lsx.c_kehoachdathang_id
	            from 
		            md_lenhsanxuat_tosx_dklht klhttp 
		            left join md_lenhsanxuat_tosx tsx on tsx.md_lenhsanxuat_tosx_id = klhttp.md_lenhsanxuat_tosx_id
		            left join md_lenhsanxuat lsx on lsx.md_lenhsanxuat_id = tsx.md_lenhsanxuat_id
	            where
		            lsx.c_kehoachdathang_id = @khid
	            group by klhttp.md_sanpham_id, lsx.c_kehoachdathang_id
            ) lttp on lttp.c_kehoachdathang_id = kh.c_kehoachdathang_id and lttp.md_sanpham_id = sp.md_sanpham_id
            where
                kh.trangthai != '{Helper.SOANTHAO}'
            order by
	            sp.ma_sanpham
		";
        return sql;
    }
}




