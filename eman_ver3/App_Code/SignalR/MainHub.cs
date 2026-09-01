using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using NPOI.HSSF.UserModel;
using MainHubObjects;
[HubName("mainhub")]
public class MainHub : Hub
{
    public void ChinhSuaMauExcel(FileExcel fileExcel)
    {
        var taskExcute = new System.Threading.Tasks.Task(() =>
        {
            var msgErr = "<div class='errorExcelTemp'>{0}</div>";
            var msgSuc = "<div class='successExcelTemp'>{0}</div>";
            Clients.Caller.ChinhSuaMauExcel(new { end = false, mess = string.Format(msgSuc, "Đang kiểm tra tập tin...") });
            try
            {
                string fileFrom = "", fileStore = PrintAnco2.GetStoreBackUp(true, fileExcel.fileName);

                HSSFWorkbook hssfwb = null;

                fileStore = ExcuteSignalRStatic.mapPathSignalR("~" + fileStore.Substring(Security.UrlBase().Length - 1));

                var excelStr = PrintAnco2.GetStore(true, fileExcel.fileName);
                fileFrom = ExcuteSignalRStatic.mapPathSignalR("~" + excelStr.Substring(Security.UrlBase().Length - 1));

                var isRestore = fileExcel.data == "restore";

                var data = fileExcel.data.Replace("data:application/vnd.ms-excel;base64,", "");
                if (isRestore)
                {
                    using (var file = new FileStream(fileStore, FileMode.Open, FileAccess.Read))
                    {
                        hssfwb = new HSSFWorkbook(file);
                        file.Close();
                        file.Dispose();
                    }
                }
                else
                {
                    byte[] buffer = Convert.FromBase64String(data);
                    var stream = new MemoryStream();
                    stream.Write(buffer, 0, buffer.Length);
                    hssfwb = new HSSFWorkbook(stream);
                }



                var s1 = hssfwb.GetSheetAt(0);
                var area = hssfwb.GetPrintArea(0);

                var checkFile = true;
                var msg = "";
                if (string.IsNullOrEmpty(area))
                {
                    msg = string.Format(msgErr, "Không tìm thấy vùng in (Print area not found)");
                    checkFile = false;
                }
                else
                {
                    msg = string.Format(msgSuc, "Vùng in đã được xác định: " + area);
                }
                Clients.Caller.ChinhSuaMauExcel(new { end = false, mess = msg });

                if (s1.LastRowNum <= 0)
                {
                    msg = string.Format(msgErr, "Tập tin rỗng, vui lòng kiểm tra lại các sheet và để sheet có dữ liệu lên đầu tiên");
                    checkFile = false;
                }
                else
                {
                    msg = string.Format(msgSuc, "Tập tin được xác định có " + s1.LastRowNum + " dòng");
                }
                Clients.Caller.ChinhSuaMauExcel(new { end = false, mess = msg });

                var sizeFontPDF = s1.GetCellComment(0, 0) == null ? false : (s1.GetCellComment(0, 0).String.String.StartsWith("sizePDF") ? true : false);
                if (sizeFontPDF)
                    msg = string.Format(msgSuc, "Tập tin có chỉ định kích thước chữ riêng cho PDF");
                else
                    msg = string.Format(msgSuc, "Tập tin sử dụng kích thước chữ chung cho cả Excel và PDF");
                Clients.Caller.ChinhSuaMauExcel(new { end = false, mess = msg });

                if (!checkFile)
                    Clients.Caller.ChinhSuaMauExcel(new { end = true, mess = string.Format(msgErr, "Cập nhật thất bại, xem lỗi ở trên.") });
                else
                {
                    if (!isRestore)
                    {
                        Clients.Caller.ChinhSuaMauExcel(new { end = false, mess = string.Format(msgSuc, "Tạo bản dự phòng cho mẫu để có thể khôi phục khi cần.") });

                        File.Copy(fileFrom, fileStore, true);
                    }

                    var filePDF = fileFrom.Substring(0, fileFrom.LastIndexOf(".")) + ".pdf.xls";

                    if (File.Exists(filePDF))
                    {
                        Clients.Caller.ChinhSuaMauExcel(new { end = false, mess = string.Format(msgSuc, "Xóa tập tin chỉ định PDF cũ.") });
                        File.Delete(filePDF);
                    }

                    Clients.Caller.ChinhSuaMauExcel(new { end = false, mess = string.Format(msgSuc, isRestore ? "Khôi phục mẫu cũ" : "Cập nhật mẫu mới vào chương trình.") });
                    var xfile = new FileStream(fileFrom, FileMode.Create, FileAccess.ReadWrite);
                    hssfwb.Write(xfile);
                    xfile.Close();
                    xfile.Dispose();



                    if (sizeFontPDF)
                    {
                        File.Copy(isRestore ? fileStore : fileFrom, filePDF, true);

                        var taskDelay = new System.Threading.Tasks.Task(() => { });
                        taskDelay.delayTask(1000);

                        var convert = new OfficeToPDF.ExcelConverter();
                        convert.endRow = s1.LastRowNum;

                        var infoExcel = new PrintAnco2.InfoExcel();
                        var columnText = area.Substring(area.Length - 1);
                        var maxColumn = Array.IndexOf(infoExcel.Alphabet, columnText);
                        convert.endColumn = maxColumn;
                        convert.sizeAddPDF = float.Parse(s1.GetCellComment(0, 0).String.String.Replace("sizePDF:", ""));
                        msg = convert.setSizePDF(filePDF);

                        if (!string.IsNullOrEmpty(msg))
                            Clients.Caller.ChinhSuaMauExcel(new { end = false, mess = string.Format(msgErr, msg) });
                    }

                    if (isRestore)
                        File.Delete(fileStore);

                    Clients.Caller.ChinhSuaMauExcel(new { end = true, mess = string.Format(msgSuc, isRestore ? "Khôi phục thành công" : "Cập nhật thành công!!!") });
                }
            }
            catch (Exception ex)
            {
                Clients.Caller.ChinhSuaMauExcel(new { end = true, mess = string.Format(msgErr, "Lỗi:" + ex.Message) });
            }
        });
        var timeFist = DateTime.Now;

        taskExcute.Start();
        taskExcute.Wait(120000);

        try { taskExcute.Dispose(); } catch { }
        if ((DateTime.Now - timeFist).Seconds >= 120)
            Clients.Caller.ChinhSuaMauExcel(new { end = true, mess = @"Time out, please retry." });
    }

    public void sendReportToClient(string data)
    {
        var context = GlobalHost.ConnectionManager.GetHubContext<MainHub>();
        context.Clients.All.sendReportToClient(new { end = true, mess = data });
    }

    public void daXemHuongDanLamHang(string data)
    {
        var context = GlobalHost.ConnectionManager.GetHubContext<MainHub>();
        context.Clients.All.taiLaiHDLH(new { end = true, mess = data });
    }
}