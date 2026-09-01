using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

public partial class PrintControllers_MD_00_YCMVT_yeuCauMuaVatTu : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KH] YÊU CẦU MUA VẬT TƯ.repx";
        string nameRpt = "YÊU CẦU MUA VẬT TƯ {ngaylap}";
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
		    declare @id nvarchar(32)= '{id}'
            select 
	            null as loaiCT,
	            ycmvt.sochungtu,
	            FORMAT(ycmvt.ngaylap, 'dd/MM/yyyy') as ngaylap,
	            FORMAT(ycmvt.ngaycan, 'dd/MM/yyyy') as ngaycan,
	            sp.ma_sanpham as maVTHH,
                sp.mota_tiengviet as tenVTHH,
	            dvt.ten_dvt as dvt,
	            cdh.soluong_yeucau as sl,
	            ycmvt.mota as gc
            from c_yeucaumuavt ycmvt
	            left join c_yeucaumuavt_cdh cdh on cdh.c_yeucaumuavt_id = ycmvt.c_yeucaumuavt_id
	            left join md_sanpham sp on sp.md_sanpham_id = cdh.md_sanpham_id
                left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
            where
	            1=1 
	            and ycmvt.c_yeucaumuavt_id = @id
            order by
	            sp.ma_sanpham
		";
        return sql;
    }
}