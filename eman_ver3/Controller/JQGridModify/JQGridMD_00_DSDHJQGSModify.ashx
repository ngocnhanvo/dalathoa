<%@ WebHandler Language="C#" Class="JQGridMD_00_DSDHJQGSModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using DataAcess;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using Newtonsoft.Json;
using System.IO;
public class JQGridMD_00_DSDHJQGSModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public string oper = "vnn";
    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
        switch (oper)
        {
            case "add":
                this.add(context);
                break;
            case "edit":
                this.edit(context);
                break;
            case "del":
                this.del(context);
                break;
            case "CA01DSAncotrading_MD00DSDHJQGS":
                this.CA01DSAncotrading_MD00DSDHJQGS(context);
                break;
            case "CA_01_UpdateDSX":
                this.CA_01_UpdateDSX(context);
                break;
            case "CA_01_DCHT":
                this.CA_01_DCHT(context);
                break;
            case "CA_01_HuyBoDonHangAnco":
                this.CA_01_HuyBoDonHangAnco(context);
                break;
            case "CA_01_Export_DH_Ancotrading":
                this.CA_01_Export_DH_Ancotrading(context);
                break;
            case "savefile":
                this.SaveFile(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_Export_DH_Ancotrading(HttpContext context)
    {
        string msg = "";
        EntityContext db = new EntityContext();
        string str = "Data Source=.;Initial Catalog=eMan;Persist Security Info=True;User ID=eMan;Password=edoc,123";
        string id = context.Request.Form["id"];
        string start = context.Request.Form["start"];
        string end = context.Request.Form["end"];
        try
        {
            SqlConnection cnn = Mbg.Data.SqlClient.SqlHelper.GetConnection;
            SqlCommand cmd = new SqlCommand(" select b.so_po,sp.ma_sanpham,a.gianhap " +
            "from c_dongdsdh a " +
            "left join c_danhsachdathang b on b.c_danhsachdathang_id = a.c_danhsachdathang_id " +
            "left join md_sanpham sp on sp.md_sanpham_id = a.md_sanpham_id " +
            "where b.anco_check = 1 and b.hangiaohang_po >= convert(datetime,N'" + start + "',103) " +
            "and b.hangiaohang_po <= convert(datetime,N'" + end + "',103)	order by b.so_po asc, sp.ma_sanpham asc", cnn);
            cmd.CommandTimeout = 60;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            EXport_PBanGia(context, dt);
            msg = "<div style='color:blue'>Đã lấy về " + dt.Rows.Count + " dòng</div>";
        }
        catch (Exception ex)
        {
            msg = "<div style='color:red'>Lỗi: " + ex.Message + "</div>";
        }
        context.Response.Write(msg);
    }

    public void EXport_PBanGia(HttpContext context, DataTable dt)
    {
        context.Session["hsswb"] = null;
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        IFont font12 = hssfworkbook.CreateFont();
        font12.FontHeightInPoints = 12;

        IFont font10 = hssfworkbook.CreateFont();
        font12.FontHeightInPoints = 8;

        IFont font12Bold = hssfworkbook.CreateFont();
        font12Bold.FontHeightInPoints = 12;
        font12Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont font10Bold = hssfworkbook.CreateFont();
        font10Bold.FontHeightInPoints = 8;
        font10Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont font16Bold = hssfworkbook.CreateFont();
        font16Bold.FontHeightInPoints = 16;
        font16Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont font12BoldItalic = hssfworkbook.CreateFont();
        font12BoldItalic.FontHeightInPoints = 12;
        font12BoldItalic.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
        font12BoldItalic.IsItalic = true;

        IFont font22Bold = hssfworkbook.CreateFont();
        font22Bold.FontHeightInPoints = 22;
        font22Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont font14Bold = hssfworkbook.CreateFont();
        font14Bold.FontHeightInPoints = 14;
        font14Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont font18Bold = hssfworkbook.CreateFont();
        font18Bold.FontHeightInPoints = 18;
        font18Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        // Cell Style 
        ICellStyle styleCenter18Bold = hssfworkbook.CreateCellStyle();
        styleCenter18Bold.SetFont(font18Bold);
        styleCenter18Bold.VerticalAlignment = VerticalAlignment.Center;
        styleCenter18Bold.Alignment = HorizontalAlignment.Center;
        styleCenter18Bold.WrapText = true;

        ICellStyle styleTop14Bold = hssfworkbook.CreateCellStyle();
        styleTop14Bold.SetFont(font14Bold);
        styleTop14Bold.VerticalAlignment = VerticalAlignment.Top;
        styleTop14Bold.WrapText = true;

        ICellStyle styleCenter22Bold = hssfworkbook.CreateCellStyle();
        styleCenter22Bold.SetFont(font22Bold);
        styleCenter22Bold.VerticalAlignment = VerticalAlignment.Center;
        styleCenter22Bold.Alignment = HorizontalAlignment.Center;
        styleCenter22Bold.WrapText = true;

        ICellStyle styleCenter12Bold = hssfworkbook.CreateCellStyle();
        styleCenter12Bold.SetFont(font12Bold);
        styleCenter12Bold.VerticalAlignment = VerticalAlignment.Center;
        styleCenter12Bold.Alignment = HorizontalAlignment.Center;
        styleCenter12Bold.WrapText = true;

        ICellStyle styleCenter16Bold = hssfworkbook.CreateCellStyle();
        styleCenter16Bold.SetFont(font16Bold);
        styleCenter16Bold.VerticalAlignment = VerticalAlignment.Center;
        styleCenter16Bold.Alignment = HorizontalAlignment.Center;
        styleCenter16Bold.WrapText = true;

        ICellStyle styleRight12Bold = hssfworkbook.CreateCellStyle();
        styleRight12Bold.SetFont(font12Bold);
        styleRight12Bold.VerticalAlignment = VerticalAlignment.Center;
        styleRight12Bold.Alignment = HorizontalAlignment.Right;
        styleRight12Bold.WrapText = true;

        ICellStyle styleCenterBorder12Bold = hssfworkbook.CreateCellStyle();
        styleCenterBorder12Bold.SetFont(font10Bold);
        styleCenterBorder12Bold.VerticalAlignment = VerticalAlignment.Center;
        styleCenterBorder12Bold.Alignment = HorizontalAlignment.Center;
        styleCenterBorder12Bold.WrapText = true;

        ICellStyle styleBorder12Bold = hssfworkbook.CreateCellStyle();
        styleBorder12Bold.SetFont(font12Bold);
        styleBorder12Bold.VerticalAlignment = VerticalAlignment.Center;
        styleBorder12Bold.WrapText = true;
        styleBorder12Bold.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12Bold.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12Bold.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12Bold.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle styleCenterBorderBottom12 = hssfworkbook.CreateCellStyle();
        styleCenterBorderBottom12.SetFont(font12);
        styleCenterBorderBottom12.VerticalAlignment = VerticalAlignment.Center;
        styleCenterBorderBottom12.Alignment = HorizontalAlignment.Center;
        styleCenterBorderBottom12.WrapText = true;
        styleCenterBorderBottom12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle style12BoldItalic = hssfworkbook.CreateCellStyle();
        style12BoldItalic.SetFont(font12BoldItalic);
        style12BoldItalic.VerticalAlignment = VerticalAlignment.Center;
        style12BoldItalic.WrapText = true;

        ICellStyle styleCenter12 = hssfworkbook.CreateCellStyle();
        styleCenter12.SetFont(font12);
        styleCenter12.VerticalAlignment = VerticalAlignment.Center;
        styleCenter12.Alignment = HorizontalAlignment.Center;
        styleCenter12.WrapText = true;

        ICellStyle style12 = hssfworkbook.CreateCellStyle();
        style12.SetFont(font12);
        style12.VerticalAlignment = VerticalAlignment.Center;
        style12.WrapText = true;

        ICellStyle style12Bold = hssfworkbook.CreateCellStyle();
        style12Bold.SetFont(font12Bold);
        style12Bold.VerticalAlignment = VerticalAlignment.Center;
        style12Bold.WrapText = true;

        ICellStyle style12TopBold = hssfworkbook.CreateCellStyle();
        style12TopBold.SetFont(font12Bold);
        style12TopBold.VerticalAlignment = VerticalAlignment.Top;
        style12TopBold.WrapText = true;

        ICellStyle style12Top = hssfworkbook.CreateCellStyle();
        style12Top.SetFont(font12);
        style12Top.VerticalAlignment = VerticalAlignment.Top;
        style12Top.WrapText = true;

        ICellStyle styleBorder12 = hssfworkbook.CreateCellStyle();
        styleBorder12.SetFont(font12);
        styleBorder12.VerticalAlignment = VerticalAlignment.Center;
        styleBorder12.WrapText = true;
        styleBorder12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle styleBorderCenter12 = hssfworkbook.CreateCellStyle();
        styleBorderCenter12.SetFont(font12);
        styleBorderCenter12.VerticalAlignment = VerticalAlignment.Center;
        styleBorderCenter12.Alignment = HorizontalAlignment.Center;
        styleBorderCenter12.WrapText = true;
        styleBorderCenter12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorderCenter12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorderCenter12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorderCenter12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle styleNumber0Border12 = hssfworkbook.CreateCellStyle();
        styleNumber0Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0");
        styleNumber0Border12.SetFont(font12);
        styleNumber0Border12.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0Border12.Alignment = HorizontalAlignment.Right;
        styleNumber0Border12.WrapText = true;
        styleNumber0Border12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;


        ICellStyle styleNumber0i0Border12 = hssfworkbook.CreateCellStyle();
        styleNumber0i0Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.0");
        styleNumber0i0Border12.SetFont(font12);
        styleNumber0i0Border12.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0i0Border12.Alignment = HorizontalAlignment.Right;
        styleNumber0i0Border12.WrapText = true;
        styleNumber0i0Border12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i0Border12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i0Border12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i0Border12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle styleNumber0i00Border12 = hssfworkbook.CreateCellStyle();
        styleNumber0i00Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i00Border12.SetFont(font12);
        styleNumber0i00Border12.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0i00Border12.Alignment = HorizontalAlignment.Right;
        styleNumber0i00Border12.WrapText = true;
        styleNumber0i00Border12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i00Border12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i00Border12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i00Border12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;


        ICellStyle styleNumber0i000Border12 = hssfworkbook.CreateCellStyle();
        styleNumber0i000Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.000");
        styleNumber0i000Border12.SetFont(font12);
        styleNumber0i000Border12.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0i000Border12.Alignment = HorizontalAlignment.Right;
        styleNumber0i000Border12.WrapText = true;
        styleNumber0i000Border12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i000Border12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i000Border12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i000Border12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;


        ICellStyle styleNumber012 = hssfworkbook.CreateCellStyle();
        styleNumber012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0");
        styleNumber012.SetFont(font12);
        styleNumber012.VerticalAlignment = VerticalAlignment.Center;
        styleNumber012.Alignment = HorizontalAlignment.Right;
        styleNumber012.WrapText = true;


        ICellStyle styleNumber0i012 = hssfworkbook.CreateCellStyle();
        styleNumber0i012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.0");
        styleNumber0i012.SetFont(font12);
        styleNumber0i012.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0i012.Alignment = HorizontalAlignment.Right;
        styleNumber0i012.WrapText = true;


        ICellStyle styleNumber0i0012 = hssfworkbook.CreateCellStyle();
        styleNumber0i0012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i0012.SetFont(font12);
        styleNumber0i0012.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0i0012.Alignment = HorizontalAlignment.Right;
        styleNumber0i0012.WrapText = true;

        ICellStyle styleNumber0i00012 = hssfworkbook.CreateCellStyle();
        styleNumber0i00012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.000");
        styleNumber0i00012.SetFont(font12);
        styleNumber0i00012.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0i00012.Alignment = HorizontalAlignment.Right;
        styleNumber0i00012.WrapText = true;


        ICellStyle styleNumber012Bold = hssfworkbook.CreateCellStyle();
        styleNumber012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0");
        styleNumber012Bold.SetFont(font12Bold);
        styleNumber012Bold.VerticalAlignment = VerticalAlignment.Center;
        styleNumber012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber012Bold.WrapText = true;


        ICellStyle styleNumber0i012Bold = hssfworkbook.CreateCellStyle();
        styleNumber0i012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.0");
        styleNumber0i012Bold.SetFont(font12Bold);
        styleNumber0i012Bold.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0i012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber0i012Bold.WrapText = true;


        ICellStyle styleNumber0i0012Bold = hssfworkbook.CreateCellStyle();
        styleNumber0i0012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i0012Bold.SetFont(font12Bold);
        styleNumber0i0012Bold.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0i0012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber0i0012Bold.WrapText = true;

        ICellStyle styleNumber0i00012Bold = hssfworkbook.CreateCellStyle();
        styleNumber0i00012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.000");
        styleNumber0i00012Bold.SetFont(font12Bold);
        styleNumber0i00012Bold.VerticalAlignment = VerticalAlignment.Center;
        styleNumber0i00012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber0i00012Bold.WrapText = true;

        IPrintSetup print = s1.PrintSetup;
        print.PaperSize = (short)PaperSize.A4;
        print.Scale = (short)80;
        print.FitWidth = (short)1;
        print.FitHeight = (short)0;

        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (i == 0 | i == 1)
            {
                s1.SetColumnWidth(i, 10000);
            }
            else
            {
                s1.SetColumnWidth(i, 5000);
            }
        }

        IRow rTitle = s1.CreateRow(0);
        rTitle.CreateCell(0).SetCellValue("Số đơn hàng");
        //rTitle.CreateCell(3).SetCellValue("Mô tả");
        rTitle.CreateCell(1).SetCellValue("Mã vật tư / hàng hóa");
        rTitle.CreateCell(2).SetCellValue("Giá");

        for (int j = 0; j <= 2; j++)
        {
            rTitle.GetCell(j).CellStyle = styleCenterBorder12Bold;
        }

        /*rDetails.CreateCell(9).SetCellValue(row["ngaycongvan"].ToString() != "" ? DateTime.Parse(row["ngaycongvan"].ToString()).ToString("dd/MM/yyyy") : "");
        rDetails.CreateCell(10).SetCellValue(row["ngaybatdau"].ToString() != "" ? DateTime.Parse(row["ngaybatdau"].ToString()).ToString("dd/MM/yyyy") : "");
        rDetails.CreateCell(11).SetCellValue(row["ngayketthuc"].ToString() != "" ? DateTime.Parse(row["ngayketthuc"].ToString()).ToString("dd/MM/yyyy") : "");
        rDetails.CreateCell(12).SetCellValue(row["trichyeu"].ToString().Length > 32767 ? row["trichyeu"].ToString().Substring(32767) : row["trichyeu"].ToString());*/
        int dem_row = 1;
        foreach (DataRow row in dt.Rows)
        {
            IRow rDetails = s1.CreateRow(dem_row);
            for (int dem = 0; dem < dt.Columns.Count; dem++)
            {
                string noidung = row[dem].ToString();
                if (noidung == "True")
                    noidung = "1";
                else if (noidung == "False")
                    noidung = "0";
                rDetails.CreateCell(dem).SetCellValue(noidung);
                //rDetails.GetCell(dem).CellStyle = styleBorder12;
            }
            dem_row++;
        }
        context.Session["hsswb"] = hssfworkbook;
    }

    public void SaveFile(HttpContext context)
    {
        HSSFWorkbook hsswb = (HSSFWorkbook)context.Session["hsswb"];
        String saveAsFileName = String.Format("DH_DH_Ancotrading-{0}.xls", DateTime.Now.ToString("dd-MM-yyyy"));
        MemoryStream exportData = new MemoryStream();
        hsswb.Write(exportData);
        context.Response.ContentType = "application/vnd.ms-excel";
        context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
        context.Response.Clear();
        context.Response.BinaryWrite(exportData.GetBuffer());
        context.Response.End();
    }

    public void CA_01_HuyBoDonHangAnco(HttpContext context)
    {
        EntityContext db = new EntityContext();
        User_TK us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context));
        string msg = "", msg_success = "";
        string id = context.Request.Form["id"];
        string check_1 = context.Request.Form["check"];
        string[] vnn = id.Split(',');
        try
        {
            foreach (c_danhsachdathang dsdh in db.c_danhsachdathang.Where(s => vnn.Contains(s.c_danhsachdathang_id)))
            {
                if (dsdh.trangthai != "DANHAN" & dsdh.trangthai != "SOANTHAO")
                {
                    if (check_1 == "1")
                    {
                        dsdh.trangthai = "HUYBO";
                        msg_success = "<div style='color:blue'> Hủy bỏ đơn hàng thành công.</div>";
                    }
                    else if (check_1 == "2")
                    {
                        dsdh.trangthai = "KETTHUC";
                        msg_success = "<div style='color:blue'> Kết thúc đơn hàng thành công.</div>";
                    }
                    else if (check_1 == "3" & dsdh.anco_check == false)
                    {
                        dsdh.trangthai = "HIEULUC";
                        msg_success = "<div style='color:blue'>Khởi động lại đơn hàng thành công.</div>";
                    }
                    else if (check_1 == "3" & dsdh.anco_check == true)
                    {
                        dsdh.trangthai = "HIEULUC";
                        msg_success = "<div style='color:blue'>Khởi động lại đơn hàng thành công.</div>";
                    }
                    else if (check_1 == "4" & dsdh.anco_check == false)
                    {
                        dsdh.trangthai = "HIEULUC";
                        msg_success = "<div style='color:blue'>Kích hoạt đơn hàng thành công.</div>";
                    }
                    else if (check_1 == "4" & dsdh.anco_check == true)
                    {
                        dsdh.trangthai = "HIEULUC";
                        msg_success = "<div style='color:blue'>Kích hoạt đơn hàng thành công.</div>";
                    }
                }
                else
                {
                    msg = "<div style='color:red'>Đơn hàng chưa hiệu lực.</div>";
                }
            }
            if (msg.Length <= 0)
            {
                db.SaveChanges();
                msg = msg_success;
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void CA_01_DCHT(HttpContext context)
    {
        var msg = "";
        context.Response.Write(msg);
    }

    public void CA_01_UpdateDSX(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string[] vnn = context.Request.Form["id"].Split(',');
        string thoigiansx = context.Request.Form["thoigiansx"];
        string thoigianht = context.Request.Form["thoigianht"];
        foreach (c_danhsachdathang dsdh in db.c_danhsachdathang.Where(s => vnn.Contains(s.c_danhsachdathang_id)).ToList())
        {
            if (dsdh.trangthai == "HIEULUC")
            {
                msg += "<div style='color:red'>Lỗi: Dòng " + dsdh.sochungtu + " đã hiệu lực.</div>";
            }
            else if (dsdh.trangthai == "HUYBO")
            {
                msg += "<div style='color:red'>Lỗi:Đơn hàng " + dsdh.sochungtu + " đã hủy.</div>";
            }
            else if (dsdh.trangthai == "KETTHUC")
            {
                msg += "<div style='color:red'>Lỗi:Đơn hàng " + dsdh.sochungtu + " đã kết thúc.</div>";
            }
            else if (dsdh.trangthai != "DANHAN")
            {
                msg += "<div style='color:red'>Lỗi: Dòng " + dsdh.sochungtu + " chưa được xác nhận.</div>";
            }
            else
            {
                dsdh.thoigiansx = int.Parse(thoigiansx);
                dsdh.ngaybatdau = dsdh.hangiaohang_po - TimeSpan.Parse(thoigiansx) - TimeSpan.Parse(thoigianht);
                dsdh.ngayhoanthanh = dsdh.hangiaohang_po - TimeSpan.Parse(thoigianht);
                db.SaveChanges();
                msg += "<div style='color:blue'>Dòng " + dsdh.sochungtu + " đã cập nhật thời gian sản xuất thành công.</div>";
            }
        }
        context.Response.Write(msg);
    }

    public void CA01DSAncotrading_MD00DSDHJQGS(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", msgSucess = "";
        var ids = context.Request.Form["id"].removeAllSpaceOrTrimText(false).Split(',').ToList();
        var dsdh = db.c_danhsachdathang.Where(s => ids.Contains(s.c_danhsachdathang_id)).FirstOrDefault();
        if (dsdh == null)
        {
            msg = "Đơn hàng không tồn tại";
            goto EndEventHandler;
        }

        string msgDetail = "";
        try
        {
            if (dsdh.trangthai == "HUYBO")
            {
                msgDetail += "<div style='color:red'>Lỗi: Đơn hàng " + dsdh.sochungtu + " đã hủy.</div>";
            }
            else if (dsdh.trangthai == "DANHAN")
            {
                msgDetail += "<div style='color:red'>Lỗi: Đơn hàng " + dsdh.sochungtu + " đã nhận hàng.</div>";
            }
            else if (dsdh.trangthai == "KETTHUC")
            {
                msgDetail += "<div style='color:red'>Lỗi: Đơn hàng " + dsdh.sochungtu + " đã kết thúc.</div>";
            }
            else if (dsdh.trangthai == "HIEULUC")
            {
                msgDetail += "<div style='color:red'>Lỗi: Đơn hàng " + dsdh.sochungtu + " đã hiệu lực.</div>";
            }
            else if (dsdh.md_trangthai_id == "DOICHIEUHANGTON")
            {
                msgDetail += "<div style='color:red'>Lỗi: Dòng " + dsdh.sochungtu + " đã đối chiếu hàng tồn.</div>";
            }
            else
            {
                var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == dsdh.md_doitackinhdoanh_id || s.md_doitackinhdoanh_id == dsdh.md_doitackinhdoanh_id).FirstOrDefault();
                if (dtkd == null)
                {
                    msgDetail += $@"<div style='color:red'>Lỗi: Dòng ""{dsdh.sochungtu}"" không tìm thấy khách hàng có mã ""{dsdh.md_doitackinhdoanh_id}"".</div>";
                }
                else
                {
                    dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
                    dsdh.ngaycapnhat = DateTime.Now;
                    dsdh.ngaynhan = DateTime.Now;
                    dsdh.nguoinhan = Security.id_taikhoan(context);
                    dsdh.trangthai = Helper.DANHAN;
                    dsdh.md_trangthai_id = Helper.HIEULUC;
                    db.SaveChanges();
                }
            }
        }
        catch (Exception ex)
        {
            msgDetail += string.Format(@"<div style='color:red'>{0}</div>", ex.Message);
        }

        if (msgDetail.Length <= 0)
        {
            msgSucess += string.Format(@"<div style='color:blue'>Nhận đơn đặt hàng ""{0}"" thành công.</div>", dsdh.sochungtu);
        }

        msg += msgDetail;

        if (msg.Length <= 0)
        {
            msg = msgSucess;
        }

    EndEventHandler:;

        context.Response.Write(msg);
    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string md_dtk_id = context.Request.Form["md_doitackinhdoanh_id"];
        string sochungtu = context.Request.Form["sochungtu"];
        DateTime ngaybatdau = VNN_Config.setDateTime(context.Request.Form["ngaybatdau"]);
        DateTime ngaylap = VNN_Config.setDateTime(context.Request.Form["ngaylap"]);
        int thoigiansx = int.Parse(context.Request.Form["thoigiansx"]);
        DateTime hangiaohang_po = VNN_Config.setDateTime(context.Request.Form["hangiaohang_po"]);
        try
        {
            ngaybatdau = hangiaohang_po - TimeSpan.Parse(thoigiansx.ToString());
            md_dtk_id = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == md_dtk_id).Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();
            string id = context.Request.QueryString["id"];
            if (msg.Length <= 0)
            {
                if (ngaybatdau == DateTime.MinValue)
                {
                    VNN_Function.SetFormValue("ngaybatdau", null);
                }
                if (ngaylap == DateTime.MinValue)
                {
                    VNN_Function.SetFormValue("ngaylap", null);
                }
                if (hangiaohang_po == DateTime.MinValue)
                {
                    VNN_Function.SetFormValue("hangiaohang_po", null);
                }
                if (sochungtu == null)
                {
                    sochungtu = VNN_VariablePublic.sochungtu(db, "DSDH", 1);
                }
                if (msg.Length <= 0)
                {
                    string action = "add";
                    string[] column_ex = { "md_trangthai_id" };
                    VNN_Function.SetFormValue("md_doitackinhdoanh_id", md_dtk_id);
                    VNN_Function.SetFormValue("sochungtu", sochungtu);
                    VNN_Function.SetFormValue("ngaybatdau", ngaybatdau.ToString(VNN_Config.get_FormatDate()));
                    string ten_table = "c_danhsachdathang";
                    VNN_Function.Set_DefaultvalueColumn(context, action);
                    VNN_Function.Modify_Function(context, ma_module, id_new, ten_table, action, column_ex, db);
                    VNN_Function.loaddulieu_Auto(db, ma_module);
                    msg = "true#Thêm thành công." + "#" + id_new;
                }
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        try
        {
            var object_ = db.c_danhsachdathang.Where(p => p.c_danhsachdathang_id.Equals(id)).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
                goto EndEventHandler;
            }

            var arrTT = new string[] { Helper.HIEULUC, Helper.KETTHUC, Helper.HUYBO };
            if (arrTT.Contains(object_.trangthai))
            {
                msg = "false#Lỗi:Đơn hàng không thể chỉnh sửa khi đã Hiệu Lưc, Hủy bỏ hoặc Kết thúc ";
                goto EndEventHandler;
            }

            if (msg.Length <= 0)
            {
                object_.cont20 = context.Request.Form["cont20"].ToNullableDecimal();
                object_.cont40 = context.Request.Form["cont40"].ToNullableDecimal();
                object_.cont40hc = context.Request.Form["cont40hc"].ToNullableDecimal();
                object_.mota = context.Request.Form["mota"];
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Cập nhật thành công.";
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.Message;
        }

    EndEventHandler:;
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

                foreach (var id_del_ in ids)
                {
                    var object_ = db.c_danhsachdathang.Where(p => p.c_danhsachdathang_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else if (new string[] { "HIEULUC", "HUYBO", "KETTHUC" }.Contains(object_.trangthai))
                    {
                        msg += string.Format(@"<br><b>{0} ({1})</b>: Không thể xóa khi đang trong trạng thái ""Hiệu Lực"", ""Hủy"" hoặc ""Kết Thúc"".", object_.sochungtu, object_.so_po);
                    }
                    else if (!new string[] { "HIEULUC", "SOANTHAO" }.Contains(object_.md_trangthai_id))
                    {
                        msg += string.Format(@"<br><b>{0} ({1})</b>: Không thể xóa khi đang triển khai.", object_.sochungtu, object_.so_po);
                    }
                    else if (db.md_hanngach.Where(s => s.c_danhsachdathang_id == object_.c_danhsachdathang_id).Count() > 0)
                    {
                        msg += string.Format(@"<br><b>{0} ({1})</b>: Đã tạo phiếu giảm hạn ngạch.", object_.sochungtu, object_.so_po);
                    }
                    else if (object_.dg_nangluc.GetValueOrDefault(false))
                    {
                        msg += string.Format(@"<br><b>{0} ({1})</b>: Đã từng hiệu lực không thể xóa.", object_.sochungtu, object_.so_po);
                    }
                    else
                    {
                        var taptins = db.md_taptin.Where(s => s.lienket == object_.c_danhsachdathang_id).ToList();
                        foreach (var taptin in taptins)
                        {
                            var path = ExcuteSignalRStatic.mapPathSignalR($@"~/{taptin.path}");
                            Helper.removeFileWithPath(path);
                            db.md_taptin.Remove(taptin);
                        }

                        var hdlhs = db.md_ghichuhdlh.Where(s => s.lienket == object_.c_danhsachdathang_id).ToList();
                        foreach (var hdlh in hdlhs)
                        {
                            db.md_ghichuhdlh.Remove(hdlh);
                        }

                        VNN_Function.Write_log(context, ma_module, null, oper, "MĐH:" + object_.sochungtu + ", TĐH:" + object_.so_po, db);
                        db.c_danhsachdathang.Remove(object_);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#Xóa đơn hàng đã chọn thành công");
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = string.Format(@"false#{0}", msg.Substring(4));
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}