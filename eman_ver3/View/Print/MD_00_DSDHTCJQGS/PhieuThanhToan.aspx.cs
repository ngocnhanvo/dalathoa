using System;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using DataAcess;
using System.Linq;
public partial class PrintControllers_MD_00_DSDHTCJQGS_PhieuThanhToan : System.Web.UI.Page
{
    public DataTable dtPublic = null;
    public string c_danhsachdathang_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var c_danhsachdathang_id = Request.QueryString["id"];
        string sql = $@"
		    declare @id nvarchar(32) = '{c_danhsachdathang_id}'
            select 
                dh.sochungtu as sophieu,
                format(getdate(), 'dd/MM/yyyy') as ngayCT,
                nv.hoten as nhanvien,
                format(hangiaohang_po, 'dd/MM/yyyy') as ngaygiao,
                dh.trangthaithanhtoan,
                dh.mota as ghichu,
                dtkd.ten_dtkd as ten_khachhang,
                dtkd.tel,
                dtkd.diachi,
                ttnh.hoten as ten_nguoinhan,
                ttnh.sdt as sdt_nguoinhan,
                ttnh.diachi as diachi_nguoinhan,
	            sp.mota_tiengviet + ' ('+ dvtsp.ten_dvt +')' as mota_tiengviet,
	            ddh.sl_dathang as soluong,
                ddh.gianhap as giaban,
	            ddh.gianhap * ddh.sl_dathang as thanhtien,
	            CAST(NULL AS decimal(18, 0)) as da_thanhtoan,
                CAST(NULL AS decimal(18, 0)) as tienmat,
                CAST(NULL AS decimal(18, 0)) as thoilai,
	            dh.sochungtu as barcode_sophieu,
                N'' as soluongStr,
                N'' as giabanStr,
                N'' as thanhtienStr,
                N'' as tongtienStr,
                N'' as da_thanhtoanStr,
                N'' as tienmatStr,
                N'' as thoilaiStr
            from 
	            c_danhsachdathang dh
	            left join c_dongdsdh ddh on ddh.c_danhsachdathang_id = dh.c_danhsachdathang_id
	            left join md_sanpham sp on sp.md_sanpham_id = ddh.md_sanpham_id or sp.ma_sanpham = ddh.md_sanpham_id
	            left join md_donvitinhsanpham dvtsp on dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
                left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id
                left join c_danhsachdathang_thongtinnhanhang ttnh on ttnh.c_danhsachdathang_id = dh.c_danhsachdathang_id and ttnh.macdinh = 1
                left join ad_user nv on nv.ad_user_id = dh.nguoitao
            where
	            dh.c_danhsachdathang_id = @id
            order by 
	            sp.ma_sanpham asc
		";

        string kocodulieu = "<center><h2>Không có dữ liệu</h2></center>";
        dtPublic = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        if(dtPublic.Rows.Count <= 0)
        {
            Response.Write(kocodulieu);
            Response.End();
        }
        else
        {
            if (string.IsNullOrWhiteSpace((dtPublic.Rows[0]["mota_tiengviet"] as string)))
            {
                Response.Write(kocodulieu);
                Response.End();
            }
            else
            {
                dtPublic.Columns.Add("tongtien", Type.GetType("System.Decimal"));
                foreach (DataRow row in dtPublic.Rows)
                {
                    row["soluongStr"] = string.Format("{0:N0}", row["soluong"] as decimal?);
                    row["giabanStr"] = string.Format("{0:N0}", row["giaban"] as decimal?);
                    row["thanhtienStr"] = string.Format("{0:N0}", row["thanhtien"] as decimal?);
                    var tongtien = dtPublic.AsEnumerable().Sum(s => s.Field<decimal>("thanhtien"));
                    row["tongtien"] = tongtien;
                    row["tongtienStr"] = string.Format("{0:N0}", tongtien);
                    row["da_thanhtoanStr"] = string.Format("{0:N0}", row["da_thanhtoan"] as decimal?);
                    row["tienmatStr"] = string.Format("{0:N0}", row["tienmat"] as decimal?);
                    row["thoilaiStr"] = string.Format("{0:N0}", row["thoilai"] as decimal?);
                }
            }
        }
    }
}