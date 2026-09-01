using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;

public partial class PrintControllers_MD_00_Baocaoketoan_BaoCaoXuatNhapTonTheoPOGiaTri : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "(KT) BÁO CÁO XUẤT NHẬP TỒN (Giá trị).repx";
        string nameRpt = "BÁO CÁO XUẤT NHẬP TỒN (Có giá trị) {ngayin}";
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
        if (tbl.Rows.Count > 0)
        {

        }
    }

    public String CreateSql(HttpContext context)
    {
        string donhanghuy = System.IO.File.ReadAllText(ExcuteSignalRStatic.mapPathSignalR("~/App_Data/JsonData/0_donhanghuy.txt"));
        donhanghuy = donhanghuy.Replace("\r\n", "',N'");
        string tu = context.Request.QueryString["tu"].removeAllSpaceOrTrimText(true).Replace("-", "/");
        string den = context.Request.QueryString["den"].removeAllSpaceOrTrimText(true).Replace("-", "/");
        //Kho
        string khoId = context.Request.QueryString["kho"].removeAllSpaceOrTrimText(true);
        string kho = khoId.Replace("'", "''");
        if (!string.IsNullOrWhiteSpace(kho))
        {
            kho = $@"and kgd.md_kho_id = N'{kho}'";
        }

        //San pham
        string masp = context.Request.QueryString["masp"].removeAllSpaceOrTrimText(true).Replace("'", "''");
        if (!string.IsNullOrWhiteSpace(masp))
            masp = $@"and sp.ma_sanpham = N'{masp}'";

        var fmtDate = Helper.fmtDate;
        var ngayin = DateTime.Now.ToString(fmtDate);
        var tuStr = tu.ToNullableDateTime(fmtDate).Value.ToString(fmtDate);
        var denStr = den.ToNullableDateTime(fmtDate).Value.ToString(fmtDate);
        string sql = $@"
		    declare @tu datetime = convert(datetime, N'{tu} 00:00:00', 103)
            declare @dauky datetime = convert(datetime, N'01/'+ cast(Month(@tu) as nvarchar(2)) +'/'+ cast(year(@tu) as nvarchar(4)) +' 00:00:00', 103)
            declare @den datetime = convert(datetime, N'{den} 23:59:59', 103)
            declare @tenkho nvarchar(MAX) = (select ten_kho from md_kho where md_kho_id = '{khoId}')

            ;with data as
            (
                select
                    kgd.ngaychuyen,
                    sp.ma_sanpham maVTHH,
                    sp.mota_tiengviet tenVTHH,
                    dvtsp.ten_dvt dvt,
                    isnull(cdh.dongiamua,0) dg,
                    kgd.donhang po,

                    case
                        when kgd.ngaychuyen < @dauky
                            then case when kgd.kieuchuyen = N'{Helper.XuatKho}'
                                      then -kgd.soluong_dichchuyen
                                      else  kgd.soluong_dichchuyen end
                        else 0
                    end sldk,

                    case
                        when kgd.ngaychuyen between @tu and @den
                             and kgd.kieuchuyen <> N'{Helper.XuatKho}'
                        then kgd.soluong_dichchuyen
                        else 0
                    end slntk,

                    case
                        when kgd.ngaychuyen between @tu and @den
                             and kgd.kieuchuyen = N'{Helper.XuatKho}'
                        then kgd.soluong_dichchuyen
                        else 0
                    end slxtk

                from md_kho_giaodich kgd

                left join md_sanpham sp
                    on sp.md_sanpham_id = kgd.md_sanpham_id

                left join md_donvitinhsanpham dvtsp
                    on dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id

                left join c_donmuahang dmh
                    on dmh.sochungtu = kgd.mota

                left join c_donmuahang_cdmh cdh
                    on cdh.c_donmuahang_id = dmh.c_donmuahang_id
                    and cdh.md_sanpham_id = kgd.md_sanpham_id

                where
                    kgd.soluong_dichchuyen > 0
                    and kgd.ngaychuyen <= @den
                    and isnull(kgd.donhang,'') != ''
                    {masp} {kho}
            ),

            dg_last as
            (
                select *
                from
                (
                    select
                        maVTHH,
                        dg,
                        row_number() over
                        (
                            partition by maVTHH
                            order by
                            case when dg > 0 then 0 else 1 end,
                            ngaychuyen desc
                        ) rn
                    from data
                )A
                where rn = 1
            )

            select
                N'{ngayin}' ngayin,
                N'{tuStr}' tungay,
                N'{denStr}' denngay,

                sum(A.sldk) tonDK,
                sum(A.slntk) nhapTK,
                sum(A.slxtk) xuatTK,

                A.maVTHH,
                max(A.tenVTHH) tenVTHH,
                max(A.dvt) dvt,
                B.dg,
                A.po,

                @tenkho kho

            from data A

            left join dg_last B
                on A.maVTHH = B.maVTHH

            group by
                A.maVTHH,
                B.dg,
                A.po

            having
                sum(A.sldk) != 0
                or sum(A.slntk) != 0
                or sum(A.slxtk) != 0

            order by
                A.po,
                A.maVTHH
		";

        //throw new Exception(sql);
        return sql;
    }
}

