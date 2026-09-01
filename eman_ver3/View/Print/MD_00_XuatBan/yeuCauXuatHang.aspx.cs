using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_XuatBan_yeuCauXuatHang : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KH] YÊU CẦU XUẤT HÀNG.repx";
        string nameRpt = "YÊU CẦU XUẤT HÀNG {today}";
        string sql = CreateSql(context);

        var task = new System.Threading.Tasks.Task(() =>
        {
            viewReport(sql);
        });

        ReportViewer1.Attributes["download"] = Request.QueryString["download"];
        PrintAnco2.exportDataWithType(task, sql, inPDF, nameTemp, nameRpt, ReportViewer1, true);
    }

    public void viewReport(String SqlQuery)
    {
        var tbl = ((DataSet)ReportViewer1.Report.DataSource).Tables[0];
        tbl.Columns.Add("sum_sokien", Type.GetType("System.Double"));
        decimal sumSLK = 0;
        string tenkien = "---";
        var rows = tbl.AsEnumerable().OrderBy(s => s.Field<string>("tenkien"));
        foreach (var r in rows)
        {
            if(r["tenkien"].ToString() != tenkien)
            {
                tenkien = r["tenkien"].ToString();
                sumSLK = sumSLK + r["sokien"].ToString().ToNullableDecimal().GetValueOrDefault(0);
            }
        }

        tbl.Rows[0]["sum_sokien"] = sumSLK;
        tbl.Rows[tbl.Rows.Count - 2]["sum_sokien"] = sumSLK;
        tbl.Rows[tbl.Rows.Count - 1]["sum_sokien"] = sumSLK;
    }

    public String CreateSql(HttpContext context)
    {
        string id = context.Request.QueryString["id"];
        string sql = $@"
		    declare @id nvarchar(32)= '{id}'
            select
	            dsdh.so_po,
	            xb.so_cont,
	            format(xb.ngaychuyen, 'dd/MM/yyyy') as ngayxuat,
                format(getdate(), 'dd/MM/yyyy') as today,
	            xb.so_seal,
	            lc.ten_cont as loaicont,
	            dtkd.ten_dtkd as ncu,
	            dsdh.sochungtu as sodshd,
	            dtkd.ten_dtkd as noigiaohang,
	            sp.ma_sanpham as mahang,
	            ddsdh.ma_sanpham_khach as makhach,
	            sp.mota_tiengviet as mota,
                dvt.ten_dvt as dvt,
	            cdh.sl_inner,
	            ddsdh.dvt_inner,
	            cdh.sl_outer,
	            ddsdh.dvt_outer,
	            ddsdh.sl_dathang - isnull(ddsdh.sl_giamhanngach, 0) as slpo,
	            ddsdh.sl_dagiao as sldx,
	            cdh.sl_muonxuat as slyc,
	            cdh.sl_xuat as sltx,
	            cdh.tenkien,
	            cdh.sokien,
	            cdh.mota as ghichu,
	            cdh.nw,
	            cdh.gw,
	            cdh.cbm,
                xb.tare,
                xb.mg,
                dsdh.huongdanlamhang as ddhh,
                dsdh.huongdanlamhangchung as hdlh
            from md_xuatban xb
            left join c_danhsachdathang dsdh on xb.c_danhsachdathang_id = dsdh.c_danhsachdathang_id
            left join md_xuatban_cdh cdh on xb.md_xuatban_id = cdh.md_xuatban_id
            left join c_dongdsdh ddsdh on ddsdh.md_sanpham_id = cdh.md_sanpham_id and ddsdh.c_danhsachdathang_id = dsdh.c_danhsachdathang_id
            left join md_loaicont lc on lc.md_loaicont_id = xb.loai_cont
            left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = xb.md_doitackinhdoanh_id
            left join md_sanpham sp on sp.md_sanpham_id = cdh.md_sanpham_id
            left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
            where
	            xb.md_xuatban_id = @id
            order by
	            sp.ma_sanpham
		";
        return sql;
    }
}