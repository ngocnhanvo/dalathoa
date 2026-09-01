using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_DSDHJQGS_DDHCoGiaTri : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public string c_danhsachdathang_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KH] Đơn hàng AncoNexx.repx";
        string nameRpt = "Đơn hàng {so_po}";
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

        var db = new EntityContext();
        var varCong = db.c_phidathang.Where(tg => tg.c_danhsachdathang_id == c_danhsachdathang_id && tg.isphicong == true);
        var varTru = db.c_phidathang.Where(gi => gi.c_danhsachdathang_id == c_danhsachdathang_id && gi.isphicong == false);
        var diengiaiCong = string.Join(", ", varCong.Select(s => s.mota).ToList());
        var diengiaiTru = string.Join(", ", varTru.Select(s => s.mota).ToList());
        var sumCong = varCong.Sum(s => s.sotien).GetValueOrDefault(0);
        var sumTru = varTru.Sum(s => s.sotien).GetValueOrDefault(0);

        var sumTT = tbl.Compute("Sum(thanhtien)", string.Empty).ToString().ToNullableDouble().GetValueOrDefault(0);
        var sumSL = tbl.Compute("Sum(sl)", string.Empty).ToString().ToNullableDouble().GetValueOrDefault(0);
        var sumP = tbl.Compute("Sum(phi)", string.Empty).ToString().ToNullableDouble().GetValueOrDefault(0);
        var sumTL = tbl.Compute("Sum(trongluong)", string.Empty).ToString().ToNullableDouble().GetValueOrDefault(0);
        var sumCBM = tbl.Compute("Sum(cbm)", string.Empty).ToString().ToNullableDouble().GetValueOrDefault(0);
        var discount = double.Parse(tbl.Rows[0]["discount"].ToString());
        var sumTTcoDis = sumTT * discount / 100;
        var sumTTcoDiscoPhi = sumTT - sumTTcoDis + (double)sumCong - (double)sumTru;
        //Header
        tbl.Columns.Add("sum_sl", Type.GetType("System.Double"));
        tbl.Columns.Add("sum_phi", Type.GetType("System.Double"));
        tbl.Columns.Add("tongcong", Type.GetType("System.Double"));
        tbl.Columns.Add("sum_tl", Type.GetType("System.Double"));
        tbl.Columns.Add("sum_cbm", Type.GetType("System.Double"));
        tbl.Columns.Add("discountVal", Type.GetType("System.Double"));
        tbl.Columns.Add("diengiai_phitru", Type.GetType("System.String"));
        tbl.Columns.Add("diengiai_phicong", Type.GetType("System.String"));
        tbl.Columns.Add("phitru", Type.GetType("System.Double"));
        tbl.Columns.Add("phicong", Type.GetType("System.Double"));
        tbl.Columns.Add("tongtiendatru", Type.GetType("System.Double"));
        tbl.Columns.Add("tienbangchu", Type.GetType("System.String"));

        //Footer
        foreach (DataRow row in tbl.Rows)
        {
            row["sum_sl"] = sumSL;

            row["sum_phi"] = sumP;

            row["tongcong"] = sumTT;

            row["sum_tl"] = sumTL;

            row["sum_cbm"] = sumCBM;

            row["discountVal"] = sumTTcoDis;

            row["diengiai_phitru"] = diengiaiCong + " ( + )";

            row["diengiai_phicong"] = diengiaiTru + " ( - )";

            row["phicong"] = sumCong;

            row["phitru"] = sumTru;

            row["tongtiendatru"] = sumTTcoDiscoPhi;

            row["tienbangchu"] = VNN_ConvertMoney.convert(sumTTcoDiscoPhi, "đồng").FirstOrDefault().Key;
        }
    }

    public String CreateSql(HttpContext context)
    {
        c_danhsachdathang_id = context.Request.QueryString["id"];
        var ancoName = "ANCO COMPANY LIMITED";
        var ancoAddress = "3B/2, Quarter 1B, An Phu Ward, Ho Chi Minh City, Vietnam";
        var ancoTelFaxEmail = "Tel.: (84-274) 3740 973   Fax:  (84-274) 3740 621  Email: anco@ancopottery.com";
        var nexxName = "NEXX DECOR COMPANY LIMITED";
        var nexxAddress = "3B/2, Quarter 1B, An Phu Ward, Ho Chi Minh City, Vietnam";
        var nexxTelFaxEmail = "Tel: +84 (0)961 802 325/+84 (0)903 342 885  Email: hoangbui@nexxdecor.vn / sales@nexxdecor.vn";
        string sql = $@"
		    declare @id nvarchar(32) = '{c_danhsachdathang_id}'

            select 
                dtkd.ten_dtkd_TA as tenCT,
                dtkd.diachi_TA as diachiCT,
	            (case when isnull(dtkd.tel, '') = '' then '' else N'Tel.: ' + dtkd.tel end) as sdtCT,
                (case when isnull(dtkd.fax, '') = '' then '' else N'   Fax: ' + dtkd.fax end) as faxCT,
                (case when isnull(dtkd.email, '') = '' then '' else N'   Email: ' + dtkd.email end) as emailCT,
                dh.sochungtu,
	            dh.so_po,
	            format(dh.ngaylap, 'dd/MM/yyyy') as ngaylap,
	            null as customer_no,
	            null as dienthoai,
	            null as fax,
	            isnull(sp.ma_sanpham, ddh.md_sanpham_id) as maVTHH,
	            ddh.ma_sanpham_khach as makhach,
	            isnull(sp.mota_tiengviet, ddh.mota_tiengviet) as tenVTHH,
                dtkd.hinhanh_link + sp.ma_sanpham as hinhanh,
	            isnull(dvtsp.ten_dvt, '') as dvt,
	            ddh.sl_dathang as sl,
	            isnull(ddh.phi, 0) as phi,
                isnull(ddh.phidg, 0) as phidg,
                ddh.gianhap as gia,
	            ddh.gianhap * ddh.sl_dathang as thanhtien,
	            (cast(ddh.sl_inner as nvarchar) + ' ' + ddh.dvt_inner) as dginner,
                (cast(ddh.sl_outer as nvarchar) + ' ' + ddh.dvt_outer) as dgouter,
	            isnull(dh.discount, 0) as discount,
                format(dh.hangiaohang_po, 'dd/MM/yyyy') as hangiaohang,
                dh.diachigiaohang as diadiemxuathang,
	            dh.huongdanlamhang,
                dh.huongdanlamhangchung,
                isnull(sp.trongluong, 0) * ddh.sl_dathang as trongluong,
                isnull(ddh.v2, 0) as cbm,
                isnull(sp.trongluong, 0) as tl_sp,
                round(isnull(ddh.v2, 0)/ddh.sl_dathang, 3) as cbm_sp
            from 
	            c_danhsachdathang dh
	            left join c_dongdsdh ddh on ddh.c_danhsachdathang_id = dh.c_danhsachdathang_id
	            left join md_sanpham sp on sp.md_sanpham_id = ddh.md_sanpham_id or sp.ma_sanpham = ddh.md_sanpham_id
	            left join md_donvitinhsanpham dvtsp on dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id
            where
	            dh.c_danhsachdathang_id = @id
            order by 
	            sp.ma_sanpham asc
		";
        return sql;
    }
}