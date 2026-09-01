using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using DataAcess;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using HSSFUtils;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.HSSF.Record.AutoFilter;

public partial class PrintControllers_TongHopMaHH_Default : System.Web.UI.Page
{
	public EntityContext db = new EntityContext();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
		string md_lenhsanxuat_id = context.Request.QueryString["id"];	
		string dec = context.Request.QueryString["dec"];	
		string thous = context.Request.QueryString["thous"];
		
		string ntc = context.Request.QueryString["ntc"];
		string ky_ = context.Request.QueryString["ky"];
		string dtkd = context.Request.QueryString["dtkd"];
		
		if(dtkd != "") {
			dtkd = " and dtkd.ma_dtkd = N'" + dtkd + "'";
		}
		
		string thang = "", md_namtaichinh_ky_id = "";
		
		string sql = String.Format(@"
						select sp.ma_sanpham as ma_sanpham,
							   dtkd.ma_dtkd,
							   dmh.sochungtu as sochungtu,
							   dmh.md_trangthai_id as trangthai,
							   sp.mota_tiengviet as mota_tiengviet,
							   isnull(A.sl_duyet, 0) as sl_kehoach,
							   cdmh.sl_dadat as sl_dadat,
							   cdmh.sl_hanngach as sl_danhap,
							   cdmh.sl_dadat - cdmh.sl_hanngach as sl_thieu,
							   dmh.ngaydonhang as ngaydonhang,
							   dmh.ngaygiaohang as ngaygiaohang,
							   convert(datetime,N'{0} 00:00',103) as tungay,
							   convert(datetime,N'{1} 23:59',103) as denngay
						from c_donmuahang_cdmh cdmh
						left join c_donmuahang dmh on dmh.c_donmuahang_id = cdmh.c_donmuahang_id
						left join md_sanpham sp on cdmh.md_sanpham_id = sp.md_sanpham_id
						left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = dmh.md_doitackinhdoanh_id
						left join
						  (select khmvt.c_kehoachmuavt_id,
								  dmh.c_donmuahang_id,
								  khmvt_cdh.md_sanpham_id,
								  khmvt_cdh.sl_duyet
						   from c_kehoachmuavt khmvt
						   left join c_donmuahang dmh on dmh.sctkehoach = khmvt.sochungtu
						   left join c_kehoachmuavt_cdh khmvt_cdh on khmvt_cdh.c_kehoachmuavt_id = khmvt.c_kehoachmuavt_id)A on A.c_donmuahang_id = cdmh.c_donmuahang_id
						and cdmh.md_sanpham_id = A.md_sanpham_id
						where cdmh.ngaytao >= convert(datetime,N'{0} 00:00',103) AND cdmh.ngaytao <= convert(datetime,N'{1} 23:59',103) {2}
						order by dtkd.ma_dtkd asc,sp.ma_sanpham asc, dmh.ngaydonhang asc
						", ky_, ntc, dtkd);
			System.Data.DataTable dt = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
			//Response.Write(sql);
			if (dt.Rows.Count != 0)
			{
				HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
				String saveAsFileName = String.Format("TongHopMaHH-{0}.xls", DateTime.Now);
				this.SaveFile(hssfworkbook, saveAsFileName);
			}
			else
			{
				Response.Write("<h3 align='center'>Báo cáo không có dữ liệu</h3>");
			}
    }
	
	public HSSFWorkbook CreateWorkBookPO(DataTable dt)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;

        Excel_Format ex_fm = new Excel_Format(hssfworkbook);
		//--
        ICellStyle celltext = ex_fm.getcell(12, false, true, "", "L", "T");
		//--
        ICellStyle cellBold = ex_fm.getcell(11, true, true, "", "L", "T");
		//-- 
		ICellStyle cellHeader = ex_fm.getcell(11, false, true, "", "C", "T");
		//-- 
		ICellStyle cellHeader_n = ex_fm.getcell(16, true, true, "", "C", "T");
		//--
		ICellStyle celln = ex_fm.getcell2(11, false, false, "", "R", "c", "#,##0.00");
		//--
		ICellStyle cellnn_r = ex_fm.getcell(11, false, false, "", "R", "T");
		//--
		ICellStyle border_n = ex_fm.getcell2(11, false, false, "LRBT", "R", "T", "#,##0.0");
		//--
		ICellStyle border_n1 = ex_fm.getcell2(11, false, false, "LRBT", "R", "T", "#,##0.00");
		//--
		ICellStyle border_n2 = ex_fm.getcell2(11, false, false, "LRBT", "R", "T", "#,##");
		//--
		ICellStyle border = ex_fm.getcell(11, false, false, "LRBT", "L", "T");
		//--
		ICellStyle border_r = ex_fm.getcell(11, false, false, "LRBT", "R", "T");
		//--
		ICellStyle border_tt = ex_fm.getcell(11, false, false, "LRBT", "C", "T");
		//--
		ICellStyle borderWrap = ex_fm.getcell(11, true, true, "LRBT", "C", "T");
        //--
        int heigh = 22;
        int row = 0;
        //set A1 - A3
        string[] a = { "Tổng hợp mã hàng đặt mua từ NCC" };
        for (int i = 0; i < a.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(0).SetCellValue(a[i]);
            s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 7));
            s1.GetRow(row).HeightInPoints = heigh;
            if (i == 0)
            {
                s1.GetRow(row).HeightInPoints = 30;
                s1.GetRow(row).GetCell(0).CellStyle = cellHeader_n;
            }
            row++;
        }
        //--

        //set F4 - F5
        string[] b = { "Từ ngày:", "Đến ngày:" };
        string[] b_value = { "tungay", "denngay" };
        for (int i = 0; i < b.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(6).SetCellValue(b[i]);
            s1.GetRow(row).GetCell(6).CellStyle = celltext;
			s1.GetRow(row).CreateCell(7).SetCellValue(DateTime.Parse(dt.Rows[0][b_value[i]].ToString()).ToString("dd/MM/yyyy"));
            s1.GetRow(row).GetCell(7).CellStyle = celltext;
            s1.GetRow(row).HeightInPoints = heigh;
            row++;
        }
		
		string ncc = ""; 
		for (int nht = 0; nht < dt.Rows.Count; nht++)
        {	
			if(ncc != dt.Rows[nht]["ma_dtkd"].ToString())
			{
				row++;
				ncc = dt.Rows[nht]["ma_dtkd"].ToString();
				string[] c = { "Nhà cung cấp:" };
				string[] c_value = { "ma_dtkd" };
				for (int i = 0; i < c.Count(); i++)
				{
					row++;
					s1.CreateRow(row).CreateCell(1).SetCellValue(c[i]);
					s1.GetRow(row).GetCell(1).CellStyle = celltext;
					s1.GetRow(row).CreateCell(2).SetCellValue(dt.Rows[nht][c_value[i]].ToString());
					s1.GetRow(row).GetCell(2).CellStyle = celltext;
					s1.GetRow(row).HeightInPoints = heigh;
					s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 7));
					row++;
				}
				// set A13 - All
				// -- Header
				int cell = 0;
				string[] d = { "Mã HH/VT", "Đơn Mua Hàng", "Trạng Thái ĐH", "Số Lượng Đặt", "Đã Giao", "Còn lại", "Ngày đơn hàng", "Ngày giao hàng" };
				IRow rowColumnHeader = s1.CreateRow(row);
				rowColumnHeader.HeightInPoints = 30;
				for (int i = 0; i < d.Count(); i++)
				{
					int with = 5000;
					rowColumnHeader.CreateCell(cell).SetCellValue(d[i]);
					rowColumnHeader.GetCell(i).CellStyle = borderWrap;
					s1.SetColumnWidth(cell, with);
					cell++;
				}				
			}
			else
			{
				ncc = dt.Rows[nht]["ma_dtkd"].ToString();
			}
			row++;
			// -- Details
			// create column
			// create detail row			
			string[] e_value = { "ma_sanpham", "sochungtu", "trangthai", "sl_dadat", "sl_danhap", "sl_thieu", "ngaydonhang","ngaygiaohang" };
			IRow row_t = s1.CreateRow(row); row_t.HeightInPoints = 22;
			//
			int cell_t = 0;
			for (int j = 0; j < e_value.Count(); j++)
			{
				if(e_value[j] == "ma_sanpham" | e_value[j] == "sochungtu" | e_value[j] == "trangthai" )
				{
					row_t.CreateCell(cell_t).SetCellValue(dt.Rows[nht][e_value[j]].ToString());
					row_t.GetCell(cell_t).CellStyle = border; 
				}					
				else if(e_value[j] == "sl_dadat" | e_value[j] == "sl_danhap" | e_value[j] == "sl_thieu")
				{
					row_t.CreateCell(cell_t).SetCellValue(double.Parse(dt.Rows[nht][e_value[j]].ToString()));
					row_t.GetCell(cell_t).CellStyle = border_n; 
				}
				else
				{
					row_t.CreateCell(cell_t).SetCellValue(DateTime.Parse(dt.Rows[nht][e_value[j]].ToString()).ToString("dd/MM/yyyy"));
					row_t.GetCell(cell_t).CellStyle = border_tt; 
				}
				cell_t++;
			}
		}
		row++;
        #region Format Print Excel

        s1.PrintSetup.PaperSize = (short)PaperSize.A4;
        s1.FitToPage = true;
        s1.PrintSetup.FitWidth = 1;
        s1.PrintSetup.FitHeight = 0;
        s1.SetMargin(MarginType.TopMargin, 0.75);
        s1.SetMargin(MarginType.BottomMargin, 0.75);
        s1.SetMargin(MarginType.LeftMargin, 0.23);
        s1.SetMargin(MarginType.RightMargin, 0.23);
        s1.SetMargin(MarginType.HeaderMargin, 0.31);
        s1.SetMargin(MarginType.FooterMargin, 0.31);
        hssfworkbook.SetPrintArea(
            hssfworkbook.GetSheetIndex(s1.SheetName), //sheet index
            0, //start column
            6, //end column
            0, //start row
            row + 1
        );
        #endregion

        return hssfworkbook;
    }

    public void SaveFile(HSSFWorkbook hsswb, String saveAsFileName)
    {
        MemoryStream exportData = new MemoryStream();
        hsswb.Write(exportData);

        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
        Response.Clear();
        Response.BinaryWrite(exportData.GetBuffer());
        Response.End();
    }
}