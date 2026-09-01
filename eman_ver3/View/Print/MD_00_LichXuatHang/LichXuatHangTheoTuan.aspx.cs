using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_LichXuatHang_LichXuatHangTheoTuan : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KH] LỊCH XUÁT HÀNG.repx";
        string nameRpt = "LỊCH XUÁT HÀNG {ngayin}";
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
        foreach(DataRow row in tbl.Rows)
        {
            row["tiendo"] = Helper.dicTrangThaiDH[row["tiendo"].ToString()];
        }
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
                , null as tuan_nam
	            , isnull(dsdh.nhanVienLH, dsdh.nguoi_dathang) as nvlh
	            , dsdh.so_po
	            , dsdh.chungloai
	            , (
		            select sum(cdh.sl_dat) 
                    from md_lenhsanxuat_tosx_cdh cdh 
                    where md_lenhsanxuat_id = lsx.md_lenhsanxuat_id and stt = 9999
	            ) as sl
	            , (case when isnull(cont20, 0) + isnull(cont40, 0) + isnull(cont40hc, 0) + isnull(cont45hc, 0) > 0 then 0 else dsdh.cbm end) as cbmLe
	            , dsdh.cont20
	            , dsdh.cont40
	            , dsdh.cont40hc
                , dsdh.cont45hc
                , dsdh.cbm as cbmTong
	            , format(dsdh.ngayhieuluc, 'dd/MM/yyyy') as ngaydathang
	            , format(dsdh.hangiaohang_po, 'dd/MM/yyyy') as ngaygiaohang
                , format(dsdh.giahangngaygiao, 'dd/MM/yyyy') as giahan
                , format(dsdh.tau1, 'dd/MM/yyyy') as tau1
                , format(dsdh.tau2, 'dd/MM/yyyy') as tau2
                , dsdh.khachkiem
                , format(dsdh.ngayTTKCS, 'dd/MM/yyyy') as ngayTTkcs
	            , format(dsdh.chiHaTT, 'dd/MM/yyyy') as chiHaTT
                , format(dsdh.anhDungTT, 'dd/MM/yyyy') as anhDungTT
                , dsdh.ghichuLXH as ghichu1
                , format(dsdh.ngayHDTBBTT, 'dd/MM/yyyy') as ngayHDTBBTT
                , dsdh.ghichuLXH2 as ghichu2
                , format(dsdh.ngayXNTSDGTT, 'dd/MM/yyyy') as ngayXNTSDGTT
                , format(dsdh.ngayDKduTP, 'dd/MM/yyyy') as ngayDKduTP
                , format(dsdh.ngayNKTPcuoicung, 'dd/MM/yyyy') as ngayNKTPcuoicung
                , format(dsdh.ngayDKduTho, 'dd/MM/yyyy') as ngayDKduTho
                , format(dsdh.ngayNKTHOcuoicung, 'dd/MM/yyyy') as ngayNKTHOcuoicung
                , dsdh.ngayTTDDH
                , dsdh.ngaycoHDDG as ngayDGDB
                , dsdh.formKYC
	            , dsdh.huongdanlamhang
	            , dsdh.huongdanlamhangchung
	            , (
                    case 
                        when ngayHDTBBTT is null or ngayXNTSDGTT is null then N'TTBS1'
                        when chiHaTT is null or anhDungTT is null then N'TTBS2'
                        when ngayTTkcs is null then N'TTBS3'
                        else (case when dsdh.trangthai = 'HIEULUC' then dsdh.md_trangthai_id else dsdh.trangthai end)
                    end
                ) as tiendo
                , dsdh.ghichuLXH3 as ghichu3
                , dsdh.ngayktDG
            from 
	            c_danhsachdathang dsdh
	            left join md_lenhsanxuat lsx on lsx.donhang_thamchieu = dsdh.so_po
            where
	            isnull(dsdh.giahangngaygiao, dsdh.hangiaohang_po) between @date1 and @date2
	            and isnull(dsdh.sanxuatton, 0) = 0
	            and isnull(dsdh.donhangtron, 0) = 0
                and isnull(dsdh.banHHVT, 0) = 0
	            and dsdh.trangthai != 'DAGUI'
            order by
                isnull(dsdh.giahangngaygiao, dsdh.hangiaohang_po), dsdh.so_po
		";
        return sql;
    }
}