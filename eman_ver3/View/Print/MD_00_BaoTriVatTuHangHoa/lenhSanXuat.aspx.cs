using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_BaoTriVatTuHangHoa_lenhSanXuat : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KH] LỆNH SẢN XUẤT (Động lệnh).repx";
        string nameRpt = "LỆNH SẢN XUẤT (Động lệnh) {ngayin}";
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
		    declare @id nvarchar(32)= N'{id}'
            sELeCt
              format(GETDATE(), 'dd/MM/yyyy') as ngayin,
              pb.ten_phongban as xuongSX,
              lsx2.sochungtu as sctLSX, 
              lsx2.donhang as tenLSX, 
              format(lsx2.ngayhoanthanh, 'dd/MM/yyyy') as ngaygiao,
              lsx2.mota as hdlh,
              sp.ma_sanpham as maHHVT, 
              sp.mota_tiengviet as tenHHVT, 
              sum(
                cdh.sl_chiato - isnull(cdh.sl_chiato2, 0) - isnull(cdh.sl_datncc, 0) - isnull(cdh.sl_datncc2, 0) 
              ) as slcsx, 
              sum(
                isnull(cdh.sl_danhapkho, 0)
              ) as sldg, 
              sum(
                cdh.sl_chiato - isnull(cdh.sl_chiato2, 0) - isnull(cdh.sl_datncc, 0) - isnull(cdh.sl_datncc2, 0) - isnull(cdh.sl_danhapkho, 0)
              ) as slcl 
            fRoM 
              md_lenhsanxuat_tosx_cdh cdh 
              left join md_lenhsanxuat lsx on lsx.md_lenhsanxuat_id = cdh.md_lenhsanxuat_id 
              left join md_lenhsanxuat2 lsx2 on lsx2.sochungtu = cdh.lsxCT 
              left join md_sanpham sp on sp.md_sanpham_id = cdh.md_sanpham_id 
              left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id 
              left join ad_department pb on pb.md_phongban_id = lsx2.xuongPhu
            WHeRe 
              1 = 1 
              and pb.md_phongban_id = @id
              and lsx2.trangthai not in ('SOANTHAO', 'KETTHUC') 
            GrOuP bY 
              sp.md_sanpham_id, 
              sp.ma_sanpham, 
              sp.mota_tiengviet, 
              dvt.ten_dvt, 
              lsx2.sochungtu, 
              lsx2.donhang, 
              lsx2.ngaytao, 
              lsx2.md_lenhsanxuat2_id, 
              lsx2.ngayhoanthanh, 
              lsx2.mota,
              pb.ten_phongban
            OrDEr bY 
              lsx2.ngaytao desc, 
              sp.ma_sanpham
		";
        return sql;
    }
}