using System;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
public partial class PrintControllers_MD_01_MD_00_KHMVT_keHoachMuaVatTu : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KH] KẾ HOẠCH MUA VẬT TƯ.repx";
        string nameRpt = "KẾ HOẠCH MUA VẬT TƯ {ngaylap}";
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
        var theoBOM = string.IsNullOrWhiteSpace(tbl.Rows[0]["ncvt"].ToString()) ? false : true;

        if (tbl.Rows.Count > 0)
            tbl.Rows[0]["loaiCT"] = theoBOM ? Helper.arrLoaiCT_KHMVT[0] : Helper.arrLoaiCT_KHMVT[1];
    }

    public String CreateSql(HttpContext context)
    {
        string id = context.Request.QueryString["id"];
        string sql = $@"
		    declare @id nvarchar(32)= '{id}'
            select 
	            N'' as loaiCT,
	            khmvt.sochungtu,
	            FORMAT(khmvt.ngaykehoach, 'dd/MM/yyyy') as ngaylap,
	            FORMAT(khmvt.ngaycan, 'dd/MM/yyyy') as ngaycan,
	            N'Từ ' + FORMAT(khmvt.tungayNCVT, 'dd/MM/yyyy') + N' đến ' + FORMAT(khmvt.denngayNCVT, 'dd/MM/yyyy') as kySX,
                FORMAT(khmvt.tungay, 'dd/MM/yy') as tungay,
                FORMAT(khmvt.denngay, 'dd/MM/yy') as denngay,
	            sp.ma_sanpham as maVTHH,
                sp.mota_tiengviet as tenVTHH,
	            dvt.ten_dvt as dvt,
	            cdh.sl_xuatkho as slxk,
	            cdh.sl_can as slcan,
	            cdh.sl_tonkho as sltk,
	            cdh.sl_tonkho_toithieu as sltktt,
	            cdh.sl_duyet as sldn,
	            cdh.sl_duyet2 as slduyet,
	            khmvt.mota as ghichu,
                (select top 1 ycmvt.c_nhucauvattu_id from c_yeucaumuavt ycmvt where ycmvt.c_kehoachmuavt_id = khmvt.sochungtu) as ncvt
            from c_kehoachmuavt khmvt
	            left join c_kehoachmuavt_cdh cdh on cdh.c_kehoachmuavt_id = khmvt.c_kehoachmuavt_id
	            left join md_sanpham sp on sp.md_sanpham_id = cdh.md_sanpham_id
                left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
            where
	            1=1 
	            and khmvt.c_kehoachmuavt_id = @id
            order by
                sp.ma_sanpham
		";
        return sql;
    }
}

