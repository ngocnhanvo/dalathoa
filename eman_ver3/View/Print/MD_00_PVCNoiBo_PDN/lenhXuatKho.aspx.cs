using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_PVCNoiBo_PDN_lenhXuatKho : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        inPDF = context.Request.QueryString["inPDF"];
        string nameTemp = "[KT] LỆNH XUẤT KHO - Điều chuyển.repx";
        string nameRpt = "LỆNH XUÁT KHO {ngaylap}";
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
        string loaiCT = Helper.arrLoaiCT_LXK[3];
        string mau0 = Helper.arrMau_LXK[0];
        string sql = $@"
		    declare @khid nvarchar(32)= '{id}'
            declare @loai nvarchar(32) = (select top 1 loaichuyen from md_vanchuyennoibo where md_vanchuyennoibo_id = @khid)
            declare @trangthai nvarchar(32) = (select top 1 md_trangthai_id from md_vanchuyennoibo where md_vanchuyennoibo_id = @khid)
            declare @laytonTPhoacBTP bit = (select top 1 laytonTPhoacBTP from md_vanchuyennoibo where md_vanchuyennoibo_id = @khid)

            if(@loai = 'VANCNBCTKGH')
            begin
                select
                    upper(N'{loaiCT}') as loaiCT,
                    N'{mau0}' as mauCT,
	                vcnb.sochungtu as sophieu,
	                format(vcnb.ngaydenghi, 'dd/MM/yyyy') as ngaylap,
	                tkho.ten_kho as tukho,
                    dkho.ten_kho as denkho,
	                vcnb.donhang_thamchieu as donhang,
	                sp.ma_sanpham as maVTHH,
                    sp.mota_tiengviet as tenVTHH,
                    dvt.ten_dvt as dvt,
	                sum(cdvc.soluong_muonchuyen) as slct,
                    null as sltn,
	                null as sldat,
	                null as slkdat,
                    null as makh
                from md_vanchuyennoibo vcnb
                    left join md_kho tkho on tkho.md_kho_id = vcnb.tukho
                    left join md_kho dkho on dkho.md_kho_id = vcnb.denkho
                    left join md_vanchuyennoibo_cdvc cdvc on cdvc.md_vanchuyennoibo_id = vcnb.md_vanchuyennoibo_id
	                left join md_sanpham spCDVC on spCDVC.md_sanpham_id = cdvc.md_sanpham_id
	                left join md_lenhsanxuat lsx on lsx.donhang_thamchieu = vcnb.donhang_thamchieu
	                left join md_lenhsanxuat_tosx_cdh cdh on cdh.md_lenhsanxuat_id = lsx.md_lenhsanxuat_id and cdh.mabo like N'%' + spCDVC.ma_sanpham + '%'
	                left join md_sanpham sp on sp.md_sanpham_id = cdh.md_sanpham_id
                    left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                where
                    vcnb.md_trangthai_id != '{Helper.SOANTHAO}'
                    and vcnb.md_vanchuyennoibo_id = @khid
	                and cdh.stt = (case when lsx.sxton = 1 or spCDVC.ban_thanhpham = 1 then 9998 else 9999 end) 
                    and cdvc.soluong_muonchuyen > 0
                group by
	                vcnb.sochungtu,
	                vcnb.ngaydenghi,
	                tkho.ma_kho,
                    dkho.ma_kho,
                    tkho.ten_kho,
                    dkho.ten_kho,
	                vcnb.donhang_thamchieu,
	                sp.ma_sanpham,
                    sp.mota_tiengviet,
                    dvt.ten_dvt
                order by
	                sp.ma_sanpham
            end
            else if(@laytonTPhoacBTP = 1)
            begin
                select
                    upper(N'{loaiCT}') as loaiCT,
                    N'{mau0}' as mauCT,
	                vcnb.sochungtu as sophieu,
	                format(vcnb.ngaydenghi, 'dd/MM/yyyy') as ngaylap,
	                tkho.ten_kho as tukho,
                    dkho.ten_kho as denkho,
	                vcnb.donhang_thamchieu as donhang,
	                sp.ma_sanpham as maVTHH,
                    sp.mota_tiengviet as tenVTHH,
                    dvt.ten_dvt as dvt,
	                sum(cdvc.soluong_muonchuyen) as slct,
                    null as sltn,
	                null as sldat,
	                null as slkdat,
                    null as makh
                from md_vanchuyennoibo vcnb
                    left join md_kho tkho on tkho.md_kho_id = vcnb.tukho
                    left join md_kho dkho on dkho.md_kho_id = vcnb.denkho
                    left join md_vanchuyennoibo_cdvc cdvc on cdvc.md_vanchuyennoibo_id = vcnb.md_vanchuyennoibo_id
	                left join md_lenhsanxuat lsx on lsx.donhang_thamchieu = vcnb.donhang_thamchieu
	                left join md_lenhsanxuat_tosx_cdh cdh on cdh.md_lenhsanxuat_id = lsx.md_lenhsanxuat_id and cdh.md_sanpham_id = cdvc.md_sanpham_id
	                left join md_sanpham sp on sp.md_sanpham_id = cdh.md_sanpham_id
                    left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                where
                    vcnb.md_trangthai_id != '{Helper.SOANTHAO}'
                    and vcnb.md_vanchuyennoibo_id = @khid
                    and cdvc.soluong_muonchuyen > 0
                group by
	                vcnb.sochungtu,
	                vcnb.ngaydenghi,
	                tkho.ma_kho,
                    dkho.ma_kho,
                    tkho.ten_kho,
                    dkho.ten_kho,
	                vcnb.donhang_thamchieu,
	                sp.ma_sanpham,
                    sp.mota_tiengviet,
                    dvt.ten_dvt
                order by
	                sp.ma_sanpham
            end
            else
            begin
                select
                    upper(N'{loaiCT}') as loaiCT,
                    N'{mau0}' as mauCT,
	                vcnb.sochungtu as sophieu,
	                format(vcnb.ngaydenghi, 'dd/MM/yyyy') as ngaylap,
	                tkho.ten_kho as tukho,
                    dkho.ten_kho as denkho,
	                vcnb.donhang_thamchieu as donhang,
	                sp.ma_sanpham as maVTHH,
                    sp.mota_tiengviet as tenVTHH,
                    dvt.ten_dvt as dvt,
	                cdvc.soluong_muonchuyen as slct,
                    null as sltn,
	                null as sldat,
	                null as slkdat,
                    null as makh
                from md_vanchuyennoibo vcnb
                    left join md_kho tkho on tkho.md_kho_id = vcnb.tukho
                    left join md_kho dkho on dkho.md_kho_id = vcnb.denkho
                    left join md_vanchuyennoibo_cdvc cdvc on cdvc.md_vanchuyennoibo_id = vcnb.md_vanchuyennoibo_id
                    left join md_sanpham sp on sp.md_sanpham_id = cdvc.md_sanpham_id
                    left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                where
                    vcnb.md_trangthai_id != '{Helper.SOANTHAO}'
                    and vcnb.md_vanchuyennoibo_id = @khid
                    and cdvc.soluong_muonchuyen > 0
                order by
	                sp.ma_sanpham
            end
		";
        return sql;
    }
}