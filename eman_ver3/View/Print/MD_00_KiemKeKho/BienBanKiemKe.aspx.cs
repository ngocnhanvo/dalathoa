using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_KiemKeKho_BienBanKiemKe : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[GD] Biên bản kiểm kê 128.repx";
        string nameRpt = "Biên bản kiểm kê {kho} {ngayin}";
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
        string sql = $@"
            declare @tbl1 table (
                kho nvarchar(MAX),
	            maHHVT nvarchar(MAX),
	            tenHHVT nvarchar(MAX),
	            dvt nvarchar(MAX),
	            sltn decimal(18,4),
	            slct decimal(18,4),
	            thua decimal(18,4),
	            thieu decimal(18,4)
            )
		    declare @khid nvarchar(32) = '{id}'

            insert into @tbl1
            select
	            kho.ten_kho as kho,
	            sp.ma_sanpham as maHHVT,
                sp.mota_tiengviet as tenHHVT,
                dvt.ten_dvt as dvt,
	            isnull(cdvc.sl_sosach, 0) as sltn,
                isnull(cdvc.sl_demduoc, 0) as slct,
                (case when isnull(cdvc.sl_demduoc, 0) > isnull(cdvc.sl_sosach, 0) then 0 else isnull(cdvc.sl_sosach, 0) - isnull(cdvc.sl_demduoc, 0) end) as thua,
                (case when isnull(cdvc.sl_demduoc, 0) <= isnull(cdvc.sl_sosach, 0) then 0 else isnull(cdvc.sl_demduoc, 0) - isnull(cdvc.sl_sosach, 0) end) as thieu
            from md_kiemke vcnb
                left join md_kho kho on kho.md_kho_id = vcnb.md_kho_id
                left join md_kiemke_cdh cdvc on cdvc.md_kiemke_id = vcnb.md_kiemke_id
                left join md_sanpham sp on sp.md_sanpham_id = cdvc.md_sanpham_id
                left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
            where
                vcnb.ma_kiemke != '{Helper.SOANTHAO}'
                and vcnb.md_kiemke_id = @khid

            declare @tbl2 table (
	            maHHVT nvarchar(MAX),
	            dg decimal(18,4),
	            rank_no int
            )

            insert into @tbl2
            select
	            sp.ma_sanpham,
	            isnull(cdh.dongiamua, 0) as dg,
	            RANK () OVER (partition by sp.ma_sanpham ORDER BY (case when isnull(cdh.dongiamua, 0) > 0 then 0 else 1 end), ngaychuyen desc) AS Rank_no
            from md_kho_giaodich kgd
	            left join md_sanpham sp on sp.md_sanpham_id = kgd.md_sanpham_id
	            left join c_donmuahang dmh on dmh.sochungtu = kgd.mota
	            left join c_donmuahang_cdmh cdh on cdh.c_donmuahang_id = dmh.c_donmuahang_id and cdh.md_sanpham_id = kgd.md_sanpham_id
            where 1=1
                and kgd.kieuchuyen = N'{Helper.NhapKho}'
	            and kgd.soluong_dichchuyen > 0
	            and isnull(kgd.dongnhapxuat, '') like N'PNKNCC%'
                and sp.ma_sanpham in (select maHHVT from @tbl1)

            select
	            format(getdate(), 'dd/MM/yyyy') as ngayin,
	            A.kho,
	            null as donhang,
	            A.maHHVT,
                A.tenHHVT,
                A.dvt,
	            A.sltn,
                A.slct,
                A.thua,
                A.thieu,
	            dmh.dg as dongia,
	            dmh.dg * A.sltn as thanhtien
            from @tbl1 A
                outer apply (select dg from @tbl2 where rank_no = 1 and maHHVT = A.maHHVT) dmh
            order by
	            A.maHHVT
		";
        return sql;
    }
}