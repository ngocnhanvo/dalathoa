using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;

public partial class PrintControllers_MD_00_Baocaoketoan_BaoCaoTongHopNhapXuatTon : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] Báo Cáo Tổng Hợp Nhập Xuất Tồn.repx";
        string nameRpt = "Báo Cáo Tổng Hợp Nhập Xuất Tồn {ky}";
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
        tbl.Columns.Add("sum_tondauky", Type.GetType("System.Double"));
        tbl.Columns.Add("sum_nhaptrongky", Type.GetType("System.Double"));
        tbl.Columns.Add("sum_xuattrongky", Type.GetType("System.Double"));
        tbl.Columns.Add("sum_toncuoiky", Type.GetType("System.Double"));
        //Footer
        int lastRow = tbl.Rows.Count - 1;
            
        var sum_tondauky = tbl.Compute("Sum(tondauky)", string.Empty).ToString();
        var sum_nhaptrongky = tbl.Compute("Sum(nhaptrongky)", string.Empty).ToString();
        var sum_xuattrongky = tbl.Compute("Sum(xuattrongky)", string.Empty).ToString();
        var sum_toncuoiky = tbl.Compute("Sum(toncuoiky)", string.Empty).ToString();

        var tondauky_cell = ReportViewer1.Report.Report.FindControl("tondauky", true);
        tondauky_cell.DataBindings[0].FormatString = sothapphan;
        var nhaptrongky_cell = ReportViewer1.Report.Report.FindControl("nhaptrongky", true);
        nhaptrongky_cell.DataBindings[0].FormatString = sothapphan;
        var xuattrongky_cell = ReportViewer1.Report.Report.FindControl("xuattrongky", true);
        xuattrongky_cell.DataBindings[0].FormatString = sothapphan;
        var toncuoiky_cell = ReportViewer1.Report.Report.FindControl("toncuoiky", true);
        toncuoiky_cell.DataBindings[0].FormatString = sothapphan;
        var sum_tondauky_cell = ReportViewer1.Report.Report.FindControl("sum_tondauky", true);
        sum_tondauky_cell.DataBindings[0].FormatString = sothapphan;
        var sum_nhaptrongky_cell = ReportViewer1.Report.Report.FindControl("sum_nhaptrongky", true);
        sum_nhaptrongky_cell.DataBindings[0].FormatString = sothapphan;
        var sum_xuattrongky_cell = ReportViewer1.Report.Report.FindControl("sum_xuattrongky", true);
        sum_xuattrongky_cell.DataBindings[0].FormatString = sothapphan;
        var sum_toncuoiky_cell = ReportViewer1.Report.Report.FindControl("sum_toncuoiky", true);
        sum_toncuoiky_cell.DataBindings[0].FormatString = sothapphan;
        for(int i = 0; i < tbl.Rows.Count; i++)
        {
            DataRow row = tbl.Rows[i];
            if ((decimal)row["tondauky"] == 0)
                row["tondauky"] = DBNull.Value;
            if ((decimal)row["nhaptrongky"] == 0)
                row["nhaptrongky"] = DBNull.Value;
            if ((decimal)row["xuattrongky"] == 0)
                row["xuattrongky"] = DBNull.Value;
            if ((decimal)row["toncuoiky"] == 0)
                row["toncuoiky"] = DBNull.Value;

            if (string.IsNullOrEmpty(sum_tondauky))
                row["sum_tondauky"] = DBNull.Value;
            else
                row["sum_tondauky"] = double.Parse(sum_tondauky);

            if (string.IsNullOrEmpty(sum_nhaptrongky))
                row["sum_nhaptrongky"] = DBNull.Value;
            else
                row["sum_nhaptrongky"] = double.Parse(sum_nhaptrongky);

            if (string.IsNullOrEmpty(sum_xuattrongky))
                row["sum_xuattrongky"] = DBNull.Value;
            else
                row["sum_xuattrongky"] = double.Parse(sum_xuattrongky);

            if (string.IsNullOrEmpty(sum_toncuoiky))
                row["sum_toncuoiky"] = DBNull.Value;
            else
                row["sum_toncuoiky"] = double.Parse(sum_toncuoiky);
        }
    }

    public string CreateSql(HttpContext context)
    {
        string thang = context.Request.QueryString["thang"];
		string nam = context.Request.QueryString["nam"];
		string kho = context.Request.QueryString["kho"];
        string mavt = context.Request.QueryString["mavt"];
        string tenvt = context.Request.QueryString["tenvt"];
        thang = thang.Length < 2 ? "0" + thang : thang;

        mavt = !string.IsNullOrEmpty(mavt) ? string.Format(" AND A.ma_vthh = N'{0}'", mavt) : "";
        tenvt = !string.IsNullOrEmpty(tenvt) ? string.Format(" AND A.ten_vthh like N'%{0}%'", tenvt) : "";

        string sql = string.Format(@"	 
            declare @tungay datetime = convert(datetime,N'01/{0}/{1} 00:00:00',103);
            declare @denngay datetime = DATEADD(month, ((YEAR(@tungay) - 1900) * 12) + MONTH(@tungay), -1);
            set @denngay = DATEADD(second ,23 * 60 * 60 + 59 * 60 + 59, @denngay);
            declare @ngaycuoikytruoc datetime = @tungay - 1;
            declare @namkytruoc int = year(@ngaycuoikytruoc);
            declare @thangkytruoc int = month(@ngaycuoikytruoc);
            declare @kho nvarchar(32) = '{2}';
            declare @tenkho nvarchar(MAX) = (select ten_kho from md_kho where md_kho_id = @kho);

            declare @kgd table 
            (
	            soluong_dichchuyen decimal(18, 4),
	            md_kho_id nvarchar(32),
	            md_sanpham_id nvarchar(32),
	            md_donvitinhsanpham_id nvarchar(32),
	            kieuchuyen nvarchar(150)
            )
            insert into @kgd
            SELECT 
	            kgd.soluong_dichchuyen,
                kgd.md_kho_id,
                kgd.md_sanpham_id,
                kgd.md_donvitinhsanpham_id,
	            kgd.kieuchuyen
            FROM md_kho_giaodich (nolock) kgd
            WHERE 1=1
                AND kgd.md_kho_id = @kho
                AND kgd.ngaychuyen BETWEEN @tungay AND @denngay

            declare @slCuoiKyTruoc table 
            (
	            spid nvarchar(32),
	            dvtspId nvarchar(32),
	            sl_cuoiky decimal(18, 4)
            )
            insert into @slCuoiKyTruoc
            select md_sanpham_id, md_donvitinhsanpham_id, sl_cuoiky
            FROM md_tonghopkho (nolock)
            WHERE
                nam = @namkytruoc
                AND soky = @thangkytruoc
                AND md_kho_id = @kho

            declare @tblKyHienTai table (
	            md_sanpham_id nvarchar(32)
	            , ma_sanpham nvarchar(MAX)
	            , mota_tiengviet nvarchar(MAX)
                , md_donvitinhsanpham_id nvarchar(32)
	            , tondauky decimal(18, 4)
	            , nhaptrongky decimal(18, 4)
	            , xuattrongky decimal(18, 4)
            )

            insert into @tblKyHienTai
            select 
	            A.md_sanpham_id
	            , A.ma_sanpham
	            , A.mota_tiengviet
	            , A.md_donvitinhsanpham_id
	            , sum(A.tondauky) as tondauky
	            , sum(A.nhaptrongky) as nhaptrongky
	            , sum(A.xuattrongky) as xuattrongky
            from (
	            select
		            ksp.md_sanpham_id
		            , sp.ma_sanpham
		            , sp.mota_tiengviet
                    , isnull(kgd.md_donvitinhsanpham_id, sp.md_donvitinhsanpham_id) as md_donvitinhsanpham_id
		            , 0 as tondauky
		            , sum(isnull(kgd.soluong_dichchuyen, 0)) as nhaptrongky
		            , 0 as xuattrongky
	            FROM 
		            md_kho_sanpham (nolock) ksp
		            left join md_sanpham (nolock) sp on ksp.md_sanpham_id = sp.md_sanpham_id
		            LEFT JOIN (select kgd.* from @kgd kgd where kgd.kieuchuyen = N'Nhập kho') kgd ON kgd.md_kho_id = ksp.md_kho_id AND ksp.md_sanpham_id = kgd.md_sanpham_id
	            WHERE 
		            ksp.md_kho_id = @kho
	            GROUP BY
		            ksp.md_sanpham_id, kgd.md_donvitinhsanpham_id, sp.ma_sanpham, sp.mota_tiengviet, sp.md_donvitinhsanpham_id
	            union
	            select
		            ksp.md_sanpham_id
		            , sp.ma_sanpham
		            , sp.mota_tiengviet
		            , isnull(kgd.md_donvitinhsanpham_id, sp.md_donvitinhsanpham_id) as md_donvitinhsanpham_id
		            , 0 as tondauky
		            , 0 as nhaptrongky
		            , sum(isnull(kgd.soluong_dichchuyen, 0)) as xuattrongky
	            FROM 
		            md_kho_sanpham (nolock) ksp
		            left join md_sanpham (nolock) sp on ksp.md_sanpham_id = sp.md_sanpham_id
		            LEFT JOIN (select kgd.* from @kgd kgd where kgd.kieuchuyen = N'Xuất kho') kgd ON kgd.md_kho_id = ksp.md_kho_id AND ksp.md_sanpham_id = kgd.md_sanpham_id
	            WHERE 
		            ksp.md_kho_id = @kho
	            GROUP BY
		            ksp.md_sanpham_id, kgd.md_donvitinhsanpham_id, sp.ma_sanpham, sp.mota_tiengviet, sp.md_donvitinhsanpham_id
            )A
            GROUP BY
		            A.md_sanpham_id
		            , A.md_donvitinhsanpham_id
		            , A.ma_sanpham
		            , A.mota_tiengviet
            
            select A.*
            , (A.tondauky + A.nhaptrongky - A.xuattrongky) as toncuoiky 
            , N'Kỳ {0}/{1}' as ky
            from 
            (
                select 
	                a.ma_sanpham as ma_vthh
	                , a.mota_tiengviet as ten_vthh
	                , dvtsp.ten_dvt as dvt
                    , @tenkho as kho
	                , isnull(b.sl_cuoiky, 0) tondauky
	                , a.nhaptrongky
	                , a.xuattrongky 
                from 
	                @tblKyHienTai a outer apply (select isnull(b.sl_cuoiky, 0) as sl_cuoiky from @slCuoiKyTruoc b where b.spid = a.md_sanpham_id and b.dvtspId = a.md_donvitinhsanpham_id) b
	                left join md_donvitinhsanpham (nolock) dvtsp on dvtsp.md_donvitinhsanpham_id = a.md_donvitinhsanpham_id
            ) A        
            where 
                A.tondauky + A.nhaptrongky + A.xuattrongky != 0
                {3} {4} 
            order by
	            A.ma_vthh
		"
        , thang
        , nam
        , kho
        , mavt
        , tenvt
        );
		return sql;
    }
}

