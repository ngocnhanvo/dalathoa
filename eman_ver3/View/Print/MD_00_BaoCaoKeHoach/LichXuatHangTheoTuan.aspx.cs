using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_BaoCaoKeHoach_LichXuatHangTheoTuan : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KH] LỊCH XUÁT HÀNG THEO TUẦN.repx";
        string nameRpt = "LỊCH XUÁT HÀNG THEO TUẦN {ngayin}";
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
        string date1 = context.Request.QueryString["ngayin1"];
        string date2 = context.Request.QueryString["ngayin2"];
        string fmt = "dd/MM/yyyy";
        string sql = $@"
            declare @date1 datetime = convert(datetime, N'{date1} 00:00:00', 103)
            declare @date2 datetime = convert(datetime, N'{date2} 23:59:59', 103)
		    --declare @monday datetime = (SELECT DATEADD(wk, DATEDIFF(wk,0,@date), 0) MondayOfCurrentWeek)
            --declare @sunday datetime = dateadd(SECOND, 23*60 *60 + 59*60 + 59, @monday + 6)

            select 
                format(getdate(), '{fmt}') as ngayin
	            , isnull(dsdh.nhanVienLH, dsdh.nguoi_dathang) as nvlh
	            , dsdh.so_po
	            , (
		            select STRING_AGG(A.sp1, ', ')
		            from (
			            select distinct top (1000) substring(cdh.sp1, 0, 3) as sp1 
                        from md_lenhsanxuat_tosx_cdh cdh 
                        where md_lenhsanxuat_id = lsx.md_lenhsanxuat_id and cdh.stt = 9999
                        order by substring(cdh.sp1, 0, 3)
                    )A
	            ) as chungloai
	            , (
		            select sum(cdh.sl_dat) 
                    from md_lenhsanxuat_tosx_cdh cdh 
                    where md_lenhsanxuat_id = lsx.md_lenhsanxuat_id and stt = 9999
	            ) as sl
	            , dsdh.cbm as cbmLe
	            , dsdh.cont20
	            , dsdh.cont40
	            , dsdh.cont40hc
	            , format(dsdh.ngayhieuluc, '{fmt}') as ngaydathang
	            , format(dsdh.hangiaohang_po, '{fmt}') as ngaygiaohang
	            , format(dsdh.giahangngaygiao, '{fmt}') as giahan
	            , dsdh.huongdanlamhang
	            , dsdh.huongdanlamhangchung
	            , (
		            select sum(cdh.sl_dagiao) 
                    from md_lenhsanxuat_tosx_cdh cdh 
                    where md_lenhsanxuat_id = lsx.md_lenhsanxuat_id and stt = 9999
	            ) as tiendoNKTP
                , dsdh.reportKCS as kcsFN
                , format(dsdh.ngaykhachkiemDK, '{fmt}') as ngaykhachkiem1
                , format(dsdh.ngaykhachkiemTT, '{fmt}') as ngaykhachkiem2
                , dsdh.ngaycoHDDG as ngaycoHDDG
                , format(dsdh.ngaycoTSDG, '{fmt}') as ngaycoTSDG
                , format(dsdh.ngaycoTem, '{fmt}') as tem
                , format(dsdh.ngaycoBaoBi, '{fmt}') as baobi
                , format(dsdh.ngaycoPallet, '{fmt}') as pallet
                , format(dsdh.ngaycoVTK, '{fmt}') as vtkhac
                , format(dsdh.ngaybookCont, '{fmt}') as ngaybookCont
                , format(dsdh.ngaythucteCont, '{fmt}') as ngaythucteCont
                --, (
                --    select top 1 format(xb.ngaychuyen, 'dd/MM/yyyy')
                --    from md_xuatban xb
                --    where xb.donhang_thamchieu = dsdh.so_po and xb.trangthai = '{Helper.HIEULUC}'
                --    order by xb.ngaychuyen desc
                --) as ngaythucteCont
                , format(dsdh.ngaytauchayETD, '{fmt}') as ngayETD
            from 
	            c_danhsachdathang dsdh
	            left join md_lenhsanxuat lsx on lsx.donhang_thamchieu = dsdh.so_po
            where
	            isnull(dsdh.giahangngaygiao, dsdh.hangiaohang_po) between @date1 and @date2
	            and isnull(dsdh.sanxuatton, 0) = 0
	            and isnull(dsdh.donhangtron, 0) = 0
	            and dsdh.trangthai != 'DAGUI'
		";
        return sql;
    }
}