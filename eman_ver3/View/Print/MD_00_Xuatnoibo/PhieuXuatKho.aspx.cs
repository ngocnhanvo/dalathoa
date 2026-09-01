using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_Xuatnoibo_PhieuXuatKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public EntityContext db = new EntityContext();
    md_xuatkhonb xk = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];

        string md_xuatkhonb_id = context.Request.QueryString["id"];
        xk = db.md_xuatkhonb.FirstOrDefault(s => s.md_xuatkhonb_id == md_xuatkhonb_id);

        string file = Server.MapPath(Security.UrlBase());

        string nameTemp = "[KT] Phiếu Xuất Kho.repx";
        string nameRpt = "Phiếu Xuất Kho {sct_lsx}";

        if (new int[] { 0, 4 }.Contains(xk.bosung.GetValueOrDefault(9999)))
        {
            nameTemp = "[SX] Phiếu Xuất Kho.repx";
        }
        
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
        var bosung = tbl.Rows[0]["bosung"].ToString();
        if (new string[] { "0", "1", "2", "3" }.Contains(bosung))
        {
            var query = from row in tbl.AsEnumerable()
                        group row by new
                        {
                            ma_vthh = row.Field<string>("ma_vthh"),
                            ten_vthh = row.Field<string>("ten_vthh"),
                            dvt = row.Field<string>("dvt"),
                            sct_lsx = row.Field<string>("sct_lsx"),
                            ngaychuyen = row.Field<DateTime?>("ngaychuyen"),
                            ngaydenghi = row.Field<DateTime?>("ngaydenghi"),
                            xuatcho = row.Field<string>("xuatcho"),
                            bophan = row.Field<string>("bophan"),
                            khoxuat = row.Field<string>("khoxuat"),
                            dongia = row.Field<decimal?>("dongia"),
                            thanhtien = row.Field<decimal?>("thanhtien"),
                            khonhap = row.Field<string>("khonhap"),
                            yeucau = row.Field<string>("yeucau"),
                            diengiai = row.Field<string>("diengiai")
                        } into g
                        orderby g.Key.ma_vthh
                        select new
                        {
                            g.Key.ma_vthh,
                            g.Key.ten_vthh,
                            g.Key.dvt,
                            g.Key.sct_lsx,
                            g.Key.ngaychuyen,
                            g.Key.ngaydenghi,
                            g.Key.xuatcho,
                            g.Key.bophan,
                            g.Key.khoxuat,
                            g.Key.dongia,
                            g.Key.thanhtien,
                            g.Key.khonhap,
                            g.Key.yeucau,
                            g.Key.diengiai,
                            soluong = g.Sum(s => s.Field<decimal?>("soluong")),
                            donhang = string.Join(", ", g.Select(s => s.Field<string>("donhang")).ToList())
                        };


            var tblDistinct = query.ToList().ToDataTable();
            tbl.Rows.Clear();
            foreach (DataRow row in tblDistinct.Rows)
            {
                tbl.ImportRow(row);
            }
        }

        var soluong = ReportViewer1.Report.Report.FindControl("soluong", true);
        soluong.DataBindings[0].FormatString = sothapphan;

        tbl.Columns.Add("sum_soluong", Type.GetType("System.Double"));
        tbl.Columns.Add("sum_thanhtien", Type.GetType("System.Double"));
        tbl.Columns.Add("dd_yeucau", Type.GetType("System.String"));
        tbl.Columns.Add("MM_yeucau", Type.GetType("System.String"));
        tbl.Columns.Add("yyyy_yeucau", Type.GetType("System.String"));
        //Header
        var ngaychuyen = tbl.Rows[0]["ngaychuyen"] as DateTime?;
        tbl.Rows[0]["dd"] = ngaychuyen.Value.ToString("dd");
        tbl.Rows[0]["MM"] = ngaychuyen.Value.ToString("MM");
        tbl.Rows[0]["yyyy"] = ngaychuyen.Value.ToString("yyyy");
        var ngaydenghi = tbl.Rows[0]["ngaydenghi"] as DateTime?;
        tbl.Rows[0]["dd_yeucau"] = ngaydenghi.Value.ToString("dd");
        tbl.Rows[0]["MM_yeucau"] = ngaydenghi.Value.ToString("MM");
        tbl.Rows[0]["yyyy_yeucau"] = ngaydenghi.Value.ToString("yyyy");
        //Footer
        int lastRow = tbl.Rows.Count - 1;
        var sumSL = tbl.Compute("Sum(soluong)", string.Empty).ToString();
        if (string.IsNullOrEmpty(sumSL))
            tbl.Rows[lastRow]["sum_soluong"] = DBNull.Value;
        else
            tbl.Rows[lastRow]["sum_soluong"] = double.Parse(sumSL);

        var sumTT = tbl.Compute("Sum(thanhtien)", string.Empty).ToString();
        if(string.IsNullOrEmpty(sumTT))
            tbl.Rows[lastRow]["sum_thanhtien"] =  DBNull.Value;
        else
            tbl.Rows[lastRow]["sum_thanhtien"] = double.Parse(sumTT);

        var sum_soluong = ReportViewer1.Report.Report.FindControl("sum_soluong", true);
        sum_soluong.DataBindings[0].FormatString = sothapphan;

        var kho_cell = ReportViewer1.Report.Report.FindControl("khoxuat", true);
        var withF = kho_cell.WidthF;
        kho_cell.WidthF = VNN_VariablePublic.GetWidthOfString(tbl.Rows[0]["khoxuat"].ToString(), "Tahoma", (float)9.5, true);
        var theochitietsau_cell = ReportViewer1.Report.Report.FindControl("theochitietsau", true);
        theochitietsau_cell.LeftF = theochitietsau_cell.LeftF + kho_cell.WidthF - withF;

        var yeucau = ReportViewer1.Report.Report.FindControl("yeucau", true);
        withF = yeucau.WidthF;
        yeucau.WidthF = VNN_VariablePublic.GetWidthOfString(tbl.Rows[0]["yeucau"].ToString(), "Tahoma", (float)9.5, true);
        var pnNgayThangNam_cell = ReportViewer1.Report.Report.FindControl("pnNgayThangNam", true);
        pnNgayThangNam_cell.LeftF = pnNgayThangNam_cell.LeftF + yeucau.WidthF - withF;
    }

    public String CreateSql(HttpContext context)
    {
        string oper = context.Request.QueryString["oper"];
        string bplvt = context.Request.QueryString["bplvt"];

        string khonhap = "null";
        if(xk.bosung == 4)
        {
            khonhap = "(select top 1 ten_kho from md_kho where md_to_id = xb.md_to_id and isnull(hangton, 0) = 0)";
        }
        string sql = string.Format(@"
            
            declare @xuatcho nvarchar(MAX), @px nvarchar(MAX);
            select top 1 @xuatcho = ten_to, @px = md_phanxuong_id from md_phanxuong_to where md_to_id = '{2}';
            if(@xuatcho is null)
            begin
                set @xuatcho = (select top 1 ten_phongban from ad_department where md_phongban_id = '{2}');
            end
            else
            begin
                set @px = (select top 1 ten_phanxuong from md_phanxuong where md_phanxuong_id = @px);
            end

		    select 
                sp.ma_sanpham as ma_vthh 
                , sp.mota_tiengviet as ten_vthh
		        , dvtsp.ten_dvt as dvt
                , xb.sochungtu as sct_lsx 
                , xb.ngaychuyen as ngaychuyen
                , xb.ngaydenghi
                , @xuatcho as xuatcho
                , @px as bophan
                , kho.ten_kho as khoxuat
                , xb.mota as diengiai
                , nknb_cdh.sl_thucxuat as soluong
                , null as dongia
                , null as thanhtien
                , {1} as khonhap
                , xb.sochungtu as yeucau
                , nknb_cdh.tenhang as donhang
                , xb.bosung
                , null as ghichu
		    from md_xuatkhonb xb
		        left join md_xuatkhonb_cdh nknb_cdh on xb.md_xuatkhonb_id = nknb_cdh.md_xuatkhonb_id
                left join md_sanpham sp on nknb_cdh.md_sanpham_id = sp.md_sanpham_id
                left join md_donvitinhsanpham dvtsp on nknb_cdh.md_donvitinhsanpham_id = dvtsp.md_donvitinhsanpham_id
                left join md_kho kho on xb.tukho = kho.md_kho_id
		    where 
                nknb_cdh.md_xuatkhonb_id = '{0}'
                and nknb_cdh.sl_thucxuat > 0
                and nknb_cdh.md_kho_id = '{2}'
            order by sp.ma_sanpham asc
		"
        , xk.md_xuatkhonb_id
        , khonhap
        , bplvt
        );
        return sql;
    }
}




