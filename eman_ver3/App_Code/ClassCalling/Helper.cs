using DataAcess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Helper
/// </summary>
public static class Helper
{
    public static string[] mimeTypes = { ".jpg", ".png", ".doc", ".docx", ".xls", ".xlsx", ".pdf" };
    public static string fmtDate = "dd/MM/yyyy";
    public static string VNN_notpost = "VNN_notpost";
    public static string SOANTHAO = "SOANTHAO";
    public static string HIEULUC = "HIEULUC";
    public static string HUYBO = "HUYBO";
    public static string KETTHUC = "KETTHUC";
    public static string CHUAHOANTHANH = "CHUAHOANTHANH";
    public static string HOANTHANH = "HOANTHANH";
    public static string KHDXL = "KHDXL";
    public static string MUAVT = "MUAVT";
    public static string MUATP = "MUATP";
    public static string BANTP = "BANTP";
    public static string NHANCONG = "NHANCONG";
    public static string ChoTTP = "ChoTTP";
    public static string DaXLTTP = "DaXLTTP";
    public static string ChoTBTP = "ChoTBTP";
    public static string DaXLTBTP = "DaXLTBTP";
    public static string DATNCC = "DATNCC";
    public static string DATSX = "DATSX";
    public static string DATHET = "DATHET";
    public static string DaNhapHang = "DaNhapHang";
    public static string ChoDG = "ChoDG";
    public static string DaDongGoi = "DaDongGoi";
    public static string ChoXH = "ChoXH";
    public static string DaXuatHang = "DaXuatHang";

    public static string CHUAGUI = "CHUAGUI";
    public static string DAGUI = "DAGUI";
    public static string TUCHOI = "TUCHOI";
    public static string DANHAN = "DANHAN";

    public static string CHODUYET = "CHODUYET";
    public static string DADUYET = "DADUYET";


    public static string NhapKho = "Nhập kho";
    public static string XuatKho = "Xuất kho";

    public static string trinhDuyets = System.Web.Configuration.WebConfigurationManager.AppSettings["trinhDuyets"];
    public static string KHOTRON = System.Web.Configuration.WebConfigurationManager.AppSettings["KHOTRON"];
    public static string KHOTONTHO = System.Web.Configuration.WebConfigurationManager.AppSettings["KHOTONTHO"];
    public static string KHOTONTP = System.Web.Configuration.WebConfigurationManager.AppSettings["KHOTONTP"];
    public static string KhoThoChoHoanThien = System.Web.Configuration.WebConfigurationManager.AppSettings["KhoThoChoHoanThien"];
    public static string KhoHangSauHoanThien = System.Web.Configuration.WebConfigurationManager.AppSettings["KhoHangSauHoanThien"];
    public static string KHOTP = System.Web.Configuration.WebConfigurationManager.AppSettings["KHOTP"];
    public static string KHOVT = System.Web.Configuration.WebConfigurationManager.AppSettings["KHOVT"];
    public static string KHODG = System.Web.Configuration.WebConfigurationManager.AppSettings["KHODG"];
    public static string XuongHoanThienKhiMuaNgoai = System.Web.Configuration.WebConfigurationManager.AppSettings["XuongHoanThienKhiMuaNgoai"];
    public static string NCCMacDinhKhiMuaNgoai = System.Web.Configuration.WebConfigurationManager.AppSettings["NCCMacDinhKhiMuaNgoai"];
    public static string hinhAnhSP_ANCO = System.Web.Configuration.WebConfigurationManager.AppSettings["hinhAnhSP_ANCO"];
    public static string hinhAnhSP_NEXX = System.Web.Configuration.WebConfigurationManager.AppSettings["hinhAnhSP_NEXX"];
    public static string urlReportExcel = System.Web.Configuration.WebConfigurationManager.AppSettings["urlReportExcel"];
    public static string[] arrLoaiCT_LNK = {
        "Nhập kho kết quả sản xuất",
        "Nhập kho điều chuyển nội bộ",
        "Nhập kho mua hàng",
        "Nhập kho SX hỗn hợp",
        "Nhập kho điều chỉnh kiểm kê thừa",
        "Nhập tồn"
    };

    public static string[] arrLoaiCT_LXK = {
        "Xuất kho sản xuất",
        "Xuất kho sản xuất bổ sung",
        "Xuất kho gộp bộ",
        "Xuất kho điều chuyển nội bộ",
        "Xuất kho điều chỉnh kiểm kê thiếu",
        "Xuất kho bán hàng",
        "Xuất kho thanh lý"
    };

    public static string[] arrLoaiCT_LSX = {
        "SẢN XUẤT HÀNG THÔ",
        "SẢN XUẤT THÀNH PHẨM"
    };

    public static string[] arrLoaiCT_KHMVT = {
        "THEO BOM",
        "NGOÀI BOM"
    };

    public static string[] arrLoaiCT_DMH = {
        "ĐƠN MUA VẬT TƯ",
        "ĐƠN MUA HÀNG THÔ",
        "ĐƠN MUA TP"
    };

    public static string[] arrLoaiCT_DBH = {
        "ĐƠN BÁN VẬT TƯ",
        "ĐƠN BÁN HÀNG THÔ",
        "ĐƠN BÁN THÀNH PHẨM"
    };

    public static string[] arrMau_LXK = {
        "103",
        "104"
    };

    public static Dictionary<string, string> dicTrangThaiDH = new Dictionary<string, string>() {
        {"HIEULUC", "Hiệu lực" },
        {"DaXLTTP", "Đã xử lý tồn TP" },
        {"DaXLTBTP", "Đã xử lý tồn thô" },
        {"DATSX", "Đặt hàng xong" },
        {"ChoDG", "Nhập hàng xong" },
        {"ChoXH", "Đóng gói xong" },
        {"DaXuatHang", "Đã xuất hàng" },
        {"SOANTHAO", "Soạn thảo" },
        {"DAGUI", "Chưa nhận" },
        {"DANHAN", "Đã nhận" },
        {"HUYBO", "Hủy bỏ" },
        {"KETTHUC", "Kết thúc" },
        {"TTBS1", "Chờ TSDG hoặc HD TEM & BB" },
        {"TTBS2", "Chờ VTDG về" },
        {"TTBS3", "Chờ đóng gói" }
    };

    public static string[] checkVT_BTP_TP(bool? vt, bool? btp, bool? sp, string loai)
    {
        //La Vat Tu
        int kq = 0;
        if (vt.GetValueOrDefault(false) & !btp.GetValueOrDefault(false) & !sp.GetValueOrDefault(false))
            kq = 0;
        //La Hon Hop Tron
        if (vt.GetValueOrDefault(false) & btp.GetValueOrDefault(false) & !sp.GetValueOrDefault(false))
            kq = 0;
        //La Hang Tho
        if (!vt.GetValueOrDefault(false) & btp.GetValueOrDefault(false))
            kq = 1;
        //La Thanh Pham
        if (!vt.GetValueOrDefault(false) & !btp.GetValueOrDefault(false) & sp.GetValueOrDefault(false))
            kq = 2;


        return new string[] { $@"T0{kq}", arrLoaiCT_DBH[kq], arrLoaiCT_DBH[int.Parse(loai.Substring(1))] };
    }

    public static dynamic getInfoDB()
    {
        var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(Mbg.Data.SqlClient.SqlHelper.connectionString);
        return new System.Collections.Generic.Dictionary<string, string>
        {
            { "server", builder.DataSource },
            { "database", builder.InitialCatalog }
        };
    }

    public static Bitmap GenerateBarcode(string text, bool pur = true)
    {
        var writer = new ZXing.BarcodeWriter
        {
            Format = ZXing.BarcodeFormat.CODE_128, // Chuẩn phổ biến cho mã đơn hàng
            Options = new ZXing.Common.EncodingOptions
            {
                Height = 30,
                Width = 2000,
                Margin = 0,
                PureBarcode = pur // Để true nếu muốn mất dòng chữ số bên dưới vạch
            }
        };

        return writer.Write(text); // Trả về đối tượng Bitmap
    }
    public static void removeFileWithPath(string path)
    {
        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);
    }

    public static void removeDirectoryWithPath(string path)
    {
        if (System.IO.Directory.Exists(path))
        {
            var filesLost = new System.IO.DirectoryInfo(path).GetFiles("*");
            foreach (var file in filesLost)
                System.IO.File.Delete(file.FullName);
            System.IO.Directory.Delete(path);
        }
    }
    public static string convertJsonStringToImage(string json, string folder, string name)
    {
        if (!(json.StartsWith("{") & json.EndsWith("}")))
        {
            if (json == "removeImage")
            {
                string pathimg = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/{0}/{1}.jpg", folder, name));
                removeFileWithPath(pathimg);
                return pathimg;
            }
            else
                return "";
        }

        var jsonHA = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        string mimeType = jsonHA["name"];
        var indexMimeType = mimeType.LastIndexOf(".");
        mimeType = indexMimeType <= -1 ? "" : mimeType.Substring(indexMimeType).ToLower();
        if (!new string[] { ".png", ".jpg" }.Contains(mimeType))
            throw new FormatException("Chỉ chấp nhận hình ảnh có định dạng .jpg hoặc .png");

        string imageBase64 = jsonHA["data"];
        var indexBase64 = imageBase64.IndexOf(",");
        imageBase64 = indexBase64 <= -1 ? "" : imageBase64.Substring(indexBase64 + 1);

        string path = "";
        if (!string.IsNullOrWhiteSpace(mimeType))
        {
            var bytes = Convert.FromBase64String(imageBase64);
            path = ExcuteSignalRStatic.mapPathSignalR("~/" + folder + "/" + name);
            using (var imageFile = new System.IO.FileStream(path + mimeType, System.IO.FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();

                var isJPEG = mimeType == ".jpg";
                if (!isJPEG)
                {
                    var myImage = System.Drawing.Image.FromStream(imageFile);
                    myImage.Save(path + ".jpg");
                    myImage.Dispose();
                }

                imageFile.Close();
                imageFile.Dispose();

                if (!isJPEG)
                {
                    System.IO.File.Delete(path + mimeType);
                }
            }
        }
        return path;
    }

    public static string getNewId()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }

    public static string EncodeHTML_VNN(string a)
    {
        if (a == null)
        {
            a = "";
        }
        a = a.Replace("&", "%26");
        a = a.Replace("+", "%2B");
        a = a.Replace("&", "%26");
        a = a.Replace("+", "%2B");
        return a;
    }

    public class checkBrowerRS
    {
        public bool ok { get; set; }
        public string userAgent { get; set; }
        public string[] lstAgents { get; set; }
    }
    public static checkBrowerRS checkBrower(HttpContext context)
    {
        var userAgent = context.Request.UserAgent.removeAllSpaceOrTrimText(true).ToLower();
        var oks = trinhDuyets.removeAllSpaceOrTrimText(true).Split(',');
        var result = false;
        foreach (var ok in oks)
        {
            if (userAgent.Contains(ok))
                result = true;
        }
        return new checkBrowerRS()
        {
            ok = result,
            lstAgents = oks,
            userAgent = userAgent
        };
    }

    public static System.Collections.Generic.Dictionary<string, string> pathReport(string name)
    {
        name = name.Replace("/", "-");
        var obj = new System.Collections.Generic.Dictionary<string, string>();
        obj["name"] = name;
        obj["link"] = HttpContext.Current.Server.MapPath("~/DEV_REPORT/pdfs/" + name);
        obj["linkView"] = EncodeHTML_VNN(Security.UrlBase() + "DEV_REPORT/pdfs/" + name);
        return obj;
    }

    public static System.Collections.Generic.Dictionary<string, string> pathImport(string name)
    {
        name = name.Replace("/", "-");
        var obj = new System.Collections.Generic.Dictionary<string, string>();
        obj["name"] = name;
        obj["link"] = HttpContext.Current.Server.MapPath("~/file_import/" + name);
        obj["linkView"] = Security.UrlBase() + "~/file_import/" + name;
        return obj;
    }

    public static string getConnectStrings(string ten_connectstring)
    {
        return System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ten_connectstring].ConnectionString;
    }

    public static string getFilter(HttpContext context)
    {
        string filter = "";
        jqGridHelper.Filter f = new jqGridHelper.Filter();
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            if (_filters != null & _filters != "")
            {
                f = jqGridHelper.Filter.CreateFilter(_filters);
                filter = f.ToScript();
            }
        }
        return filter;
    }

    public static string getFilterP(HttpContext context)
    {
        string filter = "";
        jqGridHelper.Filter f = new jqGridHelper.Filter();
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            if (_filters != null & _filters != "")
            {
                f = jqGridHelper.Filter.CreateFilter(_filters);
                filter = f.ToScriptP();
            }
        }
        return filter;
    }

    public static int getPage(HttpContext context)
    {
        int kq = 1;
        if (VNN_Validate.check_number(context.Request.QueryString["page"], "int"))
        {
            kq = int.Parse(context.Request.QueryString["page"]);
        }
        return kq;
    }

    public static int Week_(DateTime? nullable)
    {
        if (nullable.HasValue)
        {
            GregorianCalendar gCalendar = new GregorianCalendar();
            int WeekNumber = gCalendar.GetWeekOfYear(nullable.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return WeekNumber;
        }
        else
            return 0;
    }

    public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
    {
        DateTime jan1 = new DateTime(year, 1, 1);
        int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
        DateTime firstThursday = jan1.AddDays(daysOffset);
        var cal = CultureInfo.CurrentCulture.Calendar;
        int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        var weekNum = weekOfYear;
        if (firstWeek == 1)
        {
            weekNum -= 1;
        }
        var result = firstThursday.AddDays(weekNum * 7);
        return result.AddDays(-3);
    }

    public static md_kho layKhoCuaTo(string pxId, string toId, EntityContext db)
    {
        return db.md_kho.Where(s => s.md_to_id == pxId & s.hangton == false).FirstOrDefault();
    }

    public static decimal soLuongTonKhoThucTe(string khoId, string spId, EntityContext db)
    {
        var khosp = db.md_kho_sanpham.Where(s =>
            s.md_kho_id == khoId &
            s.md_sanpham_id == spId
        ).FirstOrDefault();

        return khosp == null ? 0 : khosp.soluong.GetValueOrDefault();
    }

    public static void downloadFiles(HttpContext context, string name, string link, int type)
    {
        string contentType = type == 0 ? "application/pdf" : "application/vnd.ms-excel";
        context.Response.Buffer = true;
        context.Response.Clear();
        context.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);
        context.Response.AddHeader("content-length", new System.IO.FileInfo(link).Length.ToString());
        context.Response.ContentType = contentType;
        context.Response.Charset = "";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.WriteFile(link);
        context.Response.End();
    }

    public static void viewFile(DevExpress.XtraReports.Web.ReportViewer viewer)
    {
        string linkExcel = viewer.Attributes["linkExcel"];
        string linkViewExcel = viewer.Attributes["linkViewExcel"];
        string nameExcel = viewer.Attributes["nameExcel"];
        string data = viewer.Attributes["files"];
        string url = "";

        string link = "", linkView = "", name = "";
        if (string.IsNullOrWhiteSpace(linkExcel))
        {
            var pathRp = pathReport(viewer.Report.DisplayName + ".pdf");
            viewer.Report.ExportToPdf(pathRp["link"]);
            linkView = pathRp["linkView"];
            link = pathRp["link"];
            name = pathRp["name"];
        }
        else
        {
            linkView = linkViewExcel;
            link = linkExcel;
            name = nameExcel;
        }

        if (!string.IsNullOrWhiteSpace(data))
        {
            var items = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
            var files = new List<string>();
            files.Add(link);
            foreach (var item in items)
            {
                files.Add(ExcuteSignalRStatic.mapPathSignalR("~/" + item["link"]));
            }

            string newName = name + ".merge.pdf";
            linkView = linkView.Replace(name, newName);
            MergeMultiplePDF(files.ToArray(), link.Replace(name, newName));
            url = linkView;
        }
        else
        {
            url = $"{Security.UrlBase()}ViewPDFPublic/index.aspx?url_pdf={linkView}&remove=true&zoom=1&zoomprint=0.999&id={Guid.NewGuid()}&namedown={name}";
        }

        HttpContext.Current.Response.Redirect(url);
    }

    public static void MergeMultiplePDF(string[] PDFfileNames, string OutputFile)
    {
        // Create document object  
        iTextSharp.text.Document PDFdoc = new iTextSharp.text.Document();
        // Create a object of FileStream which will be disposed at the end  
        using (System.IO.FileStream MyFileStream = new System.IO.FileStream(OutputFile, System.IO.FileMode.Create))
        {
            // Create a PDFwriter that is listens to the Pdf document  
            iTextSharp.text.pdf.PdfCopy PDFwriter = new iTextSharp.text.pdf.PdfCopy(PDFdoc, MyFileStream);
            if (PDFwriter == null)
            {
                return;
            }
            // Open the PDFdocument  
            PDFdoc.Open();
            int pageNum = 0;
            foreach (string fileName in PDFfileNames)
            {
                string type = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
                if (type == "pdf")
                {
                    // Create a PDFreader for a certain PDFdocument  
                    iTextSharp.text.pdf.PdfReader PDFreader = new iTextSharp.text.pdf.PdfReader(fileName);
                    PDFreader.ConsolidateNamedDestinations();
                    // Add content  
                    for (int i = 1; i <= PDFreader.NumberOfPages; i++)
                    {
                        iTextSharp.text.pdf.PdfImportedPage page = PDFwriter.GetImportedPage(PDFreader, i);
                        PDFwriter.AddPage(page);
                        pageNum++;
                    }
                    iTextSharp.text.pdf.PRAcroForm form = PDFreader.AcroForm;
                    if (form != null)
                    {
                        PDFwriter.CopyAcroForm(PDFreader);
                    }
                    // Close PDFreader  
                    PDFreader.Close();
                }
                else if (type == "jpg" | type == "png")
                {
                    var document = new iTextSharp.text.Document();
                    var memStream = new System.IO.MemoryStream();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, memStream).SetFullCompression();
                    document.Open();
                    var image = iTextSharp.text.Image.GetInstance(fileName);
                    image.ScaleToFit(document.PageSize.Width - 10, document.PageSize.Height - 10);
                    image.SetAbsolutePosition((document.PageSize.Width - image.ScaledWidth) / 2, (document.PageSize.Height - image.ScaledHeight) / 2);
                    document.Add(image);
                    document.Close();

                    iTextSharp.text.pdf.PdfReader PDFreader = new iTextSharp.text.pdf.PdfReader(memStream.GetBuffer());
                    PDFreader.ConsolidateNamedDestinations();
                    // Add content  
                    for (int i = 1; i <= PDFreader.NumberOfPages; i++)
                    {
                        iTextSharp.text.pdf.PdfImportedPage page = PDFwriter.GetImportedPage(PDFreader, i);
                        PDFwriter.AddPage(page);
                        pageNum++;
                    }
                    iTextSharp.text.pdf.PRAcroForm form = PDFreader.AcroForm;
                    if (form != null)
                    {
                        PDFwriter.CopyAcroForm(PDFreader);
                    }
                    // Close PDFreader  
                    PDFreader.Close();
                    memStream.Close();
                }
            }
            // Close the PDFdocument and PDFwriter  
            PDFwriter.Close();
            PDFdoc.Close();
        }// Disposes the Object of FileStream
    }

    public static dynamic setDefaultValueWhenInsertOrUpdate(dynamic a, User_TK us, bool edit)
    {
        if (!edit)
        {
            a.ngaytao = DateTime.Now;

            a.nguoitao = us.ad_user_id;
            a.value_nguoitao = us.ma_user;

            a.bophantao = us.md_phongban_id;
            a.value_bophantao = us.ten_phongban;

            a.vaitrotao = us.ad_role_id;
            a.value_vaitrotao = us.ten_role;

            a.hoatdong = true;
        }

        a.ngaycapnhat = DateTime.Now;

        a.nguoicapnhat = us.ad_user_id;

        a.value_nguoicapnhat = us.ma_user;

        a.bophancapnhat = us.md_phongban_id;

        a.value_bophancapnhat = us.ten_phongban;

        a.vaitrocapnhat = us.ad_role_id;

        a.value_vaitrocapnhat = us.ten_role;

        return a;
    }
}