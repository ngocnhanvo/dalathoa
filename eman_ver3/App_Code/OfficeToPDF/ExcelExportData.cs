using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace OfficeToPDF
{
    /// <summary>
    /// Summary description for Objects
    /// </summary>

    public class ExcelExportData
    {
        private class groupsReport
        {
            public int rowF { get; set; }
            public int rowL { get; set; }
        }

        public string title { get; set; }
        public string fileNameTemp { get; set; }
        public string DisplayName { get; set; }
        public string sqlRun { get; set; }
        public bool? ghostscript { get; set; }

        public DataTable dt { get; set; }
        public DevExpress.XtraReports.Web.ReportViewer viewer { get; set; }

        public List<AvariablePrj.lstImage> lstImage = new List<AvariablePrj.lstImage>();
        public List<AvariablePrj.lstImage> lstBarcode = new List<AvariablePrj.lstImage>();
        public List<AvariablePrj.lstTextReplace> lstTextReplace = new List<AvariablePrj.lstTextReplace>();
        public List<AvariablePrj.lstFormula> lstFormula = new List<AvariablePrj.lstFormula>();
        public List<AvariablePrj.lstFontSize> lstFontSize = new List<AvariablePrj.lstFontSize>();
        public List<ICell> lstRemoveComment = new List<ICell>();
        public List<int> lstAutoSizeColumn = new List<int>();
        public List<int> lstBlankRow = new List<int>();

        public ExcelExportData()
        {

        }

        public void exec(string type)
        {
            var printCf = new PrintAnco2();
            printCf.isPDF = type == "pdf";

            var context = HttpContext.Current;
            var sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 0);
            sothapphan = PrintAnco2.Replace0ToHyphen2(sothapphan);
            var config = PrintAnco2.GetInfoPrint();
            string url = ExcuteSignalRStatic.mapPathSignalR("~/" + PrintAnco2.GetStoreNotApp(true, fileNameTemp));
            var urlFontPDF = url.Substring(0, url.LastIndexOf(".")) + ".pdf.xls";

            if (File.Exists(urlFontPDF) & type == "pdf")
                url = urlFontPDF;

            title = url.Substring(url.LastIndexOf("\\") + 1);

            HSSFWorkbook hssfwb;
            using (var file = new FileStream(url, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new HSSFWorkbook(file);
                file.Close();
                file.Dispose();
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    DisplayName = DisplayName.Replace("{" + column.ColumnName + "}", dt.Rows[0][column.ColumnName].ToString());

                    lstTextReplace.Add(new AvariablePrj.lstTextReplace()
                    {
                        oldT = "{" + column.ColumnName + "}",
                        newT = dt.Rows[0][column.ColumnName].ToString()
                    });

                    if (
                        column.DataType == Type.GetType("System.Double") |
                        column.DataType == Type.GetType("System.Decimal") |
                        column.DataType == Type.GetType("System.Int16") |
                        column.DataType == Type.GetType("System.Int32") |
                        column.DataType == Type.GetType("System.Int64")
                        )
                    {
                        lstTextReplace.Add(new AvariablePrj.lstTextReplace()
                        {
                            oldT = "{#" + column.ColumnName + "}",
                            newT = dt.Rows[0][column.ColumnName].ToString()
                        });
                    }
                }

                var s1 = hssfwb.GetSheetAt(0);
                var nPOIM = new NPOIM(hssfwb, s1);

                var cellRangeAddressAll = new List<CellRangeAddress>();
                for (int m = 0; m < s1.NumMergedRegions; m++)
                {
                    cellRangeAddressAll.Add(s1.GetMergedRegion(m));
                }

                var infoExcel = new PrintAnco2.InfoExcel();
                var maxColumn = -1;
                var area = "";
                var columnText = "";

                var groupsReport = new List<groupsReport>();


                var commentFirst = s1.GetRow(0).GetCell(0);
                if (commentFirst != null)
                {
                    if (commentFirst.CellComment != null)
                    {
                        var arrStr = commentFirst.CellComment.String.String.Split(new string[] { "\n" }, StringSplitOptions.None);
                        foreach (var str in arrStr)
                        {
                            if (str.LastIndexOf("print:") > -1)
                            {
                                area = str.Replace("print:", "");
                                columnText = area.Split(':')[1];
                                maxColumn = Array.IndexOf(infoExcel.Alphabet, columnText);
                                break;
                            }
                        }
                    }
                }

                var area2 = hssfwb.GetPrintArea(0);
                var columnText2 = area2.Split(':')[1];
                printCf.columnLast = Array.IndexOf(infoExcel.Alphabet, columnText2);
                if (maxColumn <= -1)
                    maxColumn = printCf.columnLast.Value;

                lstTextReplace.Add(new AvariablePrj.lstTextReplace()
                {
                    oldT = "{totalPage}",
                    newT = "{totalPage}"
                });

                lstTextReplace.Add(new AvariablePrj.lstTextReplace()
                {
                    oldT = "{dd}",
                    newT = config.dd
                });

                lstTextReplace.Add(new AvariablePrj.lstTextReplace()
                {
                    oldT = "{MM}",
                    newT = config.MM
                });

                lstTextReplace.Add(new AvariablePrj.lstTextReplace()
                {
                    oldT = "{yyyy}",
                    newT = config.yyyy
                });

                var findlogo = false;

                foreach (var item in cellRangeAddressAll)
                {
                    var cell = s1.GetRow(item.FirstRow).GetCell(item.FirstColumn);
                    if (cell.CellType == CellType.String)
                    {
                        if (cell.StringCellValue.StartsWith("{logo}"))
                        {
                            lstImage.Add(new AvariablePrj.lstImage()
                            {
                                column = item.FirstColumn,
                                columnLast = item.LastColumn,
                                row = item.FirstRow,
                                rowLast = item.LastRow,
                                link = context.Server.MapPath(config.logo.Substring(0, config.logo.LastIndexOf("?")))
                            });
                            findlogo = true;
                        }

                        if (cell.StringCellValue.StartsWith("{picture_"))
                        {
                            var a = cell.StringCellValue.Substring(1, cell.StringCellValue.Length - 2);
                            var link = ExcuteSignalRStatic.mapPathSignalR("~/" + dt.Rows[0][a].ToString());
                            if (lstImage.Where(s => s.link == link).Count() <= 0)
                            {
                                lstImage.Add(new AvariablePrj.lstImage()
                                {
                                    column = item.FirstColumn,
                                    columnLast = item.LastColumn,
                                    row = item.FirstRow,
                                    rowLast = item.LastRow,
                                    keepSize = true,
                                    link = link
                                });
                            }
                        }

                        if (cell.StringCellValue.StartsWith("{barcode_"))
                        {
                            var a = cell.StringCellValue.Substring(1, cell.StringCellValue.Length - 2);
                            string barcodeText = dt.Rows[0][a].ToString();
                            lstBarcode.Add(new AvariablePrj.lstImage()
                            {
                                column = item.FirstColumn,
                                columnLast = item.LastColumn + 1,
                                row = item.FirstRow,
                                rowLast = item.LastRow + 1,
                                link = barcodeText
                            });
                            cell.SetCellValue("");
                        }
                    }
                }

                for (var i = 0; i <= s1.LastRowNum; i++)
                {
                    string comment = "";
                    var rowI = s1.GetRow(i);
                    //if (nPOIM.IsRowBlank(rowI))
                    //    lstBlankRow.Add(i + 1);

                    if (rowI != null)
                    {
                        var cellI0 = rowI.GetCell(0);
                        if (cellI0 != null)
                        {
                            if (cellI0.CellComment != null)
                            {
                                comment = cellI0.CellComment.String.String;
                            }
                        }

                        for (var col = 0; col <= maxColumn; col++)
                        {
                            var cell = rowI.GetCell(col);
                            if (cell != null)
                            {
                                if (i == 0)
                                {
                                    string commentCol = "";
                                    if (cell.CellComment != null)
                                    {
                                        commentCol = cell.CellComment.String.String;
                                    }

                                    if (commentCol.StartsWith("autoFit"))
                                    {
                                        lstAutoSizeColumn.Add(col);
                                        cell.RemoveCellComment();
                                    }
                                }
                                else
                                {
                                    string commentCol = "";
                                    if (cell.CellComment != null)
                                    {
                                        commentCol = cell.CellComment.String.String;
                                    }

                                    if (commentCol.StartsWith("wrap"))
                                    {
                                        var itemRP = lstTextReplace.Where(s => s.oldT == cell.StringCellValue).FirstOrDefault();
                                        if (itemRP != null)
                                        {
                                            itemRP.wrap_Mer = true;
                                            itemRP.column_Mer = commentCol.Substring(4).ToNullableInt();
                                        }
                                        cell.RemoveCellComment();
                                    }
                                }

                                if (cell.CellType == CellType.String)
                                {
                                    if (!findlogo)
                                    {

                                        var cellTxt = cell.StringCellValue;
                                        if (cellTxt.StartsWith("{logo}"))
                                        {
                                            lstImage.Add(new AvariablePrj.lstImage()
                                            {
                                                column = col,
                                                columnLast = col,
                                                row = i,
                                                rowLast = i,
                                                link = context.Server.MapPath(config.logo.Substring(0, config.logo.LastIndexOf("?")))
                                            });
                                            findlogo = true;
                                        }
                                    }

                                    if (
                                        cell.StringCellValue.LastIndexOf("{!") > -1
                                        & cell.StringCellValue.LastIndexOf("{!!") <= -1
                                        & !new string[] { "{!SUMGROUP}", "{!SUMREPORT}" }.Contains(cell.StringCellValue)
                                        )
                                    {
                                        string cellFormula = cell.StringCellValue.Replace("{!", "").Replace("}", "");
                                        if (!cellFormula.Contains("[0]"))
                                        {
                                            lstFormula.Add(new AvariablePrj.lstFormula()
                                            {
                                                row = i + 1,
                                                col = col + 1,
                                                levelFML = 0,
                                                formula = cellFormula
                                            });
                                        }
                                    }
                                    else if (cell.StringCellValue.StartsWith("{picture_"))
                                    {
                                        var a = cell.StringCellValue.Substring(1, cell.StringCellValue.Length - 2);
                                        var link = ExcuteSignalRStatic.mapPathSignalR("~/" + dt.Rows[0][a].ToString());
                                        if (lstImage.Where(s => s.link == link).Count() <= 0)
                                        {
                                            lstImage.Add(new AvariablePrj.lstImage()
                                            {
                                                column = col,
                                                columnLast = col,
                                                row = i,
                                                rowLast = i,
                                                keepSize = true,
                                                link = link
                                            });
                                        }
                                    }

                                    var itemReps = lstTextReplace.Where(s => cell.StringCellValue.Contains(s.oldT)).ToList();
                                    foreach (var itemRep in itemReps)
                                    {
                                        if (itemRep.row == null)
                                        {
                                            itemRep.row = i;
                                            itemRep.column = col;
                                        }
                                        else
                                        {
                                            var cp = itemRep.Clone();
                                            cp.row = i;
                                            cp.column = col;
                                            lstTextReplace.Add(cp);
                                        }
                                    }
                                }
                                else if (cell.CellType == CellType.Formula)
                                {
                                    //lstFormula.Add(new AvariablePrj.lstFormula()
                                    //{
                                    //    row = i + 1,
                                    //    col = col + 1,
                                    //    levelFML = 0
                                    //});
                                }
                            }
                        }
                    }

                    if (comment.StartsWith("sizePDF:"))
                    {
                        infoExcel.sizePDF = true;
                        s1.GetRow(i).GetCell(0).RemoveCellComment();
                    }
                    if (comment == "detail")
                    {
                        infoExcel.detail = i;
                        infoExcel.startSumReport = i;
                        s1.GetRow(i).GetCell(0).RemoveCellComment();
                    }
                    if (comment == "rptFooter")
                    {
                        infoExcel.rptFooter = i;
                        s1.GetRow(i).GetCell(0).RemoveCellComment();
                    }
                    if (comment.StartsWith("rptGroupHeader"))
                    {
                        var strCm = comment.Replace("rptGroupHeader", "");

                        var avas = strCm.Replace("[", "").Replace("]", "").Split(',').ToList();

                        infoExcel.rptGroupHeader = new PrintAnco2.RptGroupHeader()
                        {
                            start = i,
                            end = i,
                            arrAva = avas,
                            key = ""
                        };
                        s1.GetRow(i).GetCell(0).RemoveCellComment();
                    }
                    if (comment.StartsWith("rptGroupFooter"))
                    {
                        infoExcel.rptGroupFooter = i;
                    }

                    if (infoExcel.detail != null & infoExcel.rptFooter != null & infoExcel.rptGroupHeader != null & infoExcel.rptGroupFooter != null)
                        break;
                }


                var rowDTCP = createIrowICell(s1, infoExcel.detail.GetValueOrDefault(0), maxColumn + 1, cellRangeAddressAll);

                var rowFTCP = createIrowICell(s1, infoExcel.rptFooter.GetValueOrDefault(0), maxColumn + 1, cellRangeAddressAll);

                var rptGroupHeader = infoExcel.rptGroupHeader;
                if (rptGroupHeader != null)
                {
                    rptGroupHeader.end = rptGroupHeader.start + (infoExcel.detail - rptGroupHeader.end - 1);
                    rptGroupHeader.rows = new List<PrintAnco2.IRowICellCopy>();
                    for (var iGH = rptGroupHeader.start.Value; iGH <= rptGroupHeader.end.Value; iGH++)
                    {
                        var item = createIrowICell(s1, iGH, maxColumn, cellRangeAddressAll);
                        rptGroupHeader.rows.Add(item);
                    }
                }

                var rowGFTCP = rptGroupHeader != null ? createIrowICell(s1, infoExcel.rptGroupFooter.GetValueOrDefault(0), maxColumn + 1, cellRangeAddressAll) : null;

                if (infoExcel.rptGroupFooter == null)
                    rowGFTCP = null;

                var rowsAboveFooter = new List<float>();
                for (var iGH = rowFTCP.row.RowNum + 1; iGH <= s1.LastRowNum; iGH++)
                {
                    var rowIGH = s1.GetRow(iGH);
                    if (rowIGH != null)
                        rowsAboveFooter.Add(s1.GetRow(iGH).HeightInPoints);
                }

                int rowAddGHeader = 0, totalRow = 0;
                var countDT = dt.Rows.Count;
                var countGroup = 0;

                if (rptGroupHeader != null)
                {
                    for (var iR = 0; iR < countDT; iR++)
                    {
                        var keyStr = "";
                        foreach (var item in rptGroupHeader.arrAva)
                        {
                            keyStr += dt.Rows[iR][item].ToString();
                        }

                        if (rptGroupHeader.key != keyStr)
                        {
                            countGroup++;
                            rptGroupHeader.key = keyStr;
                        }
                    }
                }

                if (rptGroupHeader != null)
                {
                    rowAddGHeader = rptGroupHeader.end.Value - rptGroupHeader.start.Value + 1;
                    totalRow += rowAddGHeader * (countGroup - 1);

                    if (rowGFTCP != null)
                    {
                        totalRow += countGroup - 1;
                    }
                    rptGroupHeader.key = "";
                }
                totalRow += countDT - 1;

                var lstFMLNotNull = lstFormula.Where(s => !string.IsNullOrEmpty(s.formula)).ToList();
                List<AvariablePrj.lstFormula> fmlContainsLastRows = null;
                List<AvariablePrj.lstTextReplace> lstRPLNotNull = null;
                List<AvariablePrj.lstImage> imageAfterDetails = null;

                if (infoExcel.detail != null)
                {
                    imageAfterDetails = lstImage.Where(s => s.row > infoExcel.detail.Value).ToList();
                    foreach (var imageAfterDetail in imageAfterDetails)
                    {
                        imageAfterDetail.row = imageAfterDetail.row + totalRow;
                        imageAfterDetail.rowLast = imageAfterDetail.rowLast + totalRow;
                    }

                    foreach (var itemBarcode in lstBarcode)
                    {
                        itemBarcode.row = itemBarcode.row + totalRow;
                        itemBarcode.rowLast = itemBarcode.rowLast + totalRow;
                    }

                    fmlContainsLastRows = lstFMLNotNull.Where(s => s.formula.Contains("[lastRow]")).ToList();
                    var dtFN = totalRow + infoExcel.detail.GetValueOrDefault(0) + 1;
                    foreach (var fmlContainsLastRow in fmlContainsLastRows)
                    {
                        fmlContainsLastRow.formula = fmlContainsLastRow.formula.Replace("[lastRow]", dtFN.ToString());
                    }

                    lstRPLNotNull = lstTextReplace.Where(s => s.row.GetValueOrDefault(0) > infoExcel.detail).ToList();
                    foreach (var rpl in lstRPLNotNull)
                        rpl.row = rpl.row.GetValueOrDefault(0) + totalRow;

                    lstBlankRow = lstBlankRow.Select(r => (r - 1 > infoExcel.detail) ? r + totalRow : r).ToList();
                    s1.ShiftRows(infoExcel.detail.GetValueOrDefault(0), s1.LastRowNum, totalRow, true, false);
                }

                int lastG = 0;
                for (var iR = 0; iR < countDT; iR++)
                {
                    if (rptGroupHeader != null)
                    {
                        var keyStr = "";

                        foreach (var item in rptGroupHeader.arrAva)
                        {
                            keyStr += dt.Rows[iR][item].ToString();
                        }


                        if (rptGroupHeader.key != keyStr)
                        {
                            rptGroupHeader.moreVal += rptGroupHeader.key == "" ? 0 : rowAddGHeader;
                            var minIGH = rptGroupHeader.start.Value + iR + rptGroupHeader.moreVal;
                            var maxIGH = rptGroupHeader.end.Value + iR + rptGroupHeader.moreVal;
                            for (var iGH = minIGH; iGH <= maxIGH; iGH++)
                            {
                                var rowGHMau = rptGroupHeader.rows.Skip(iGH - minIGH).Take(1).FirstOrDefault();
                                var rowGHT = s1.GetRow(iGH);
                                if (rowGHT == null)
                                {
                                    rowGHT = s1.CreateRow(iGH);
                                }
                                var maxHeight3 = rowGHMau.defaultHeight.GetValueOrDefault(0);
                                foreach (var cell in rowGHMau.cells)
                                {
                                    var columnT = rowGHT.CreateCell(cell.ColumnIndex);
                                    columnT.CellStyle = cell.CellStyle;
                                    var colObj = setCell(columnT, cell, infoExcel, s1, dt, rowGHT, printCf, maxHeight3, hssfwb, iR, sothapphan, maxColumn, rowGHMau.mergeCells);
                                }

                                if (rowGHMau.row.ZeroHeight)
                                    rowGHT.ZeroHeight = true;
                                else
                                {
                                    rowGHT.HeightInPoints = maxHeight3;
                                }
                            }

                            lastG = 0;
                            groupsReport.Add(new ExcelExportData.groupsReport()
                            {
                                rowF = maxIGH,
                                rowL = lastG
                            });
                            rptGroupHeader.key = keyStr;
                            infoExcel.startSumGroup = maxIGH + 1;
                        }
                        else
                        {
                            lastG++;
                            var grpN = groupsReport.OrderByDescending(s => s.rowF).FirstOrDefault();
                            if (grpN != null)
                            {
                                grpN.rowL = lastG;
                            }
                        }

                        var rowAtDetail = rptGroupHeader.start.Value + iR + rowAddGHeader + rptGroupHeader.moreVal;
                        var rowDT = s1.CreateRow(rowAtDetail);
                        var maxHeight = rowDTCP.defaultHeight.GetValueOrDefault(0);
                        foreach (var cell in rowDTCP.cells)
                        {
                            var columnT = rowDT.CreateCell(cell.ColumnIndex);
                            columnT.CellStyle = cell.CellStyle;
                            var colObj = setCell(columnT, cell, infoExcel, s1, dt, rowDT, printCf, maxHeight, hssfwb, iR, sothapphan, maxColumn, rowDTCP.mergeCells);
                        }
                        //rowDT.HeightInPoints = nPOIM.CalculateRowHeight(s1, rowDT.RowNum, maxHeight);

                        if (rowGFTCP != null)
                        {
                            var createGroupFooter = false;
                            if (iR == countDT - 1)
                                createGroupFooter = true;
                            else
                            {
                                var keyStrNext = "";
                                foreach (var item in rptGroupHeader.arrAva)
                                {
                                    keyStrNext += dt.Rows[iR + 1][item].ToString();
                                }

                                if (keyStr != keyStrNext)
                                    createGroupFooter = true;
                            }

                            if (createGroupFooter)
                            {
                                rptGroupHeader.moreVal += 1;
                                var rowGFT = s1.CreateRow(rowAtDetail + 1);
                                var maxHeight3 = rowFTCP.defaultHeight.GetValueOrDefault(0);
                                foreach (var cell in rowGFTCP.cells)
                                {
                                    var columnT = rowGFT.CreateCell(cell.ColumnIndex);
                                    columnT.CellStyle = cell.CellStyle;
                                    var colObj = setCell(columnT, cell, infoExcel, s1, dt, rowGFT, printCf, maxHeight3, hssfwb, iR, sothapphan, maxColumn, rowGFTCP.mergeCells);
                                }
                                //rowGFT.HeightInPoints = nPOIM.CalculateRowHeight(s1, rowGFT.RowNum, maxHeight3);
                            }
                        }
                    }
                    else
                    {
                        var rowDT = s1.CreateRow(iR + infoExcel.detail.GetValueOrDefault(0));
                        var maxHeight = rowDTCP.defaultHeight.GetValueOrDefault(0);
                        //rowDT.HeightInPoints = maxHeight;
                        foreach (var cell in rowDTCP.cells)
                        {
                            var columnT = rowDT.CreateCell(cell.ColumnIndex);
                            columnT.CellStyle = cell.CellStyle;
                            var colObj = setCell(columnT, cell, infoExcel, s1, dt, rowDT, printCf, maxHeight, hssfwb, iR, sothapphan, maxColumn, rowDTCP.mergeCells);
                        }
                        //rowDT.HeightInPoints = nPOIM.CalculateRowHeight(s1, rowDT.RowNum, maxHeight);
                    }
                }

                var grps = groupsReport.OrderBy(s => s.rowF).ToList();
                foreach (var g in grps)
                {
                    var rowLG = s1.GetRow(g.rowF);
                    if (rowLG != null)
                    {
                        foreach (var cell in rowLG.Cells)
                        {
                            if (cell.CellType == CellType.String)
                            {
                                if (cell.StringCellValue.Contains("{!!"))
                                {
                                    var str = cell.StringCellValue.Replace("[lastG]", (cell.RowIndex + 2 + g.rowL).ToString());
                                    str = str.Replace("[0]", (cell.RowIndex + 2).ToString());
                                    str = str.Replace("{!!", "").Replace("}", "");

                                    try
                                    {
                                        cell.SetCellFormula(str);
                                        cell.SetCellType(CellType.Formula);

                                        lstFormula.Add(new AvariablePrj.lstFormula()
                                        {
                                            row = cell.RowIndex + 1,
                                            col = cell.ColumnIndex + 1,
                                            formula = str
                                        });
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                }

                //rowAddGHeader = rowAddGHeader > 0 ? rowAddGHeader + 1 : 0;
                if (infoExcel.rptFooter != null)
                {
                    infoExcel.rptFooter = infoExcel.rptFooter.GetValueOrDefault(0) + totalRow;
                    var rowRptFooter = s1.GetRow(infoExcel.rptFooter.GetValueOrDefault(0));
                    var maxHeight2 = rowFTCP.defaultHeight.GetValueOrDefault(0);
                    foreach (var cell in rowFTCP.cells)
                    {
                        var columnT = rowRptFooter.GetCell(cell.ColumnIndex);
                        var colObj = setCell(columnT, cell, infoExcel, s1, dt, rowRptFooter, printCf, maxHeight2, hssfwb, 0, sothapphan, maxColumn, rowFTCP.mergeCells);
                        columnT = colObj["cell"] as ICell;
                    }
                    //rowRptFooter.HeightInPoints = nPOIM.CalculateRowHeight(s1, rowRptFooter.RowNum, maxHeight2);

                    var atFooter = infoExcel.rptFooter.GetValueOrDefault(0);
                    var roWsAboveAt = rowsAboveFooter.Count + atFooter;
                    for (var i = atFooter + 1; i <= roWsAboveAt; i++)
                    {
                        var rowI = s1.GetRow(i);
                        if (rowI != null)
                        {
                            var atPre = i - (atFooter + 1);
                            rowI.HeightInPoints = rowsAboveFooter[atPre];
                        }
                    }
                }

                foreach (var rmComment in lstRemoveComment)
                {
                    rmComment.RemoveCellComment();
                }

                lstFormula = lstFormula.OrderByDescending(s => s.levelFML).ToList();
                foreach (var fml in lstFormula)
                {
                    var cell = s1.GetRow(fml.row - 1).GetCell(fml.col - 1);
                    if (cell != null)
                    {
                        cell.SetCellFormula(fml.formula);
                        cell.SetCellType(CellType.Formula);
                    }
                }

                foreach (var itemBarcode in lstBarcode)
                {
                    using (Bitmap barcodeBitmap = Helper.GenerateBarcode(itemBarcode.link))
                    {
                        // --- Bước 2: Chuyển Bitmap sang Byte Array ---
                        byte[] imageBytes;
                        using (var ms = new System.IO.MemoryStream())
                        {
                            // Lưu bitmap vào stream dưới dạng PNG để giữ độ sắc nét
                            barcodeBitmap.Save(ms, ImageFormat.Png);
                            imageBytes = ms.ToArray();
                        }

                        // --- Bước 3: Chèn vào NPOI ---
                        int pictureIdx = hssfwb.AddPicture(imageBytes, PictureType.PNG);
                        IDrawing patriarch = s1.CreateDrawingPatriarch();

                        // Sử dụng CreationHelper để tạo Anchor chuẩn cho NPOI
                        var helper = hssfwb.GetCreationHelper();
                        IClientAnchor anchor = helper.CreateClientAnchor();

                        // Đặt vị trí (Ví dụ: dòng 20, cột A)
                        // Lưu ý: Chỉ số trong NPOI bắt đầu từ 0
                        anchor.Col1 = itemBarcode.column;
                        anchor.Col2 = itemBarcode.columnLast;
                        anchor.Row1 = itemBarcode.row;
                        anchor.Row2 = itemBarcode.rowLast;
                        anchor.AnchorType = (int)AnchorType.MoveAndResize;
                        IPicture pict = patriarch.CreatePicture(anchor, pictureIdx);
                        //pict.Resize(0.3);
                        // Tự động căn chỉnh theo kích thước ảnh gốc của Barcode
                        //pict.Resize();
                    }
                }
                HSSFFormulaEvaluator.EvaluateAllFormulaCells(hssfwb);

                lstTextReplace = lstTextReplace.Where(s => s.row != null & s.column != null).ToList();
                lstImage = lstImage.Where(s => !string.IsNullOrWhiteSpace(s.link)).ToList();

                //nPOIM.InsertImages(lstImage);

                printCf.hssfworkbook = hssfwb;
                //s1 = printCf.PrintExcel((HSSFSheet)s1, type);
                var filename = Guid.NewGuid().ToString();
                filename = System.Text.RegularExpressions.Regex.Replace(filename, @"[^0-9a-zA-Z]+", "-");
                var urlExcel = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/FileUpload/{0}.xls", filename));
                using (var xfile = new FileStream(urlExcel, FileMode.Create, FileAccess.ReadWrite))
                {
                    hssfwb.Write(xfile);
                    xfile.Close();
                    xfile.Dispose();
                }

                var urlPDF = urlExcel.Substring(0, urlExcel.LastIndexOf(".") + 1) + "pdf";
                string msg = "", urlFN = "";
                urlFN = printCf.isPDF.GetValueOrDefault(false) ? urlPDF : urlExcel;
                var req = new AvariablePrj.ExportRequest
                {
                    urlExcel = urlExcel,
                    urlFN = urlFN,
                    lstImage = lstImage,
                    lstTextReplace = lstTextReplace,
                    lstAutoSizeColumn = lstAutoSizeColumn,
                    //lstBlankRow = lstBlankRow,
                    endrow = s1.LastRowNum,
                    endColumn = maxColumn,
                    nameApp = "dalathoa",
                    ghostscript = ghostscript,
                };
                string urlInterop = Helper.urlReportExcel;
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlInterop);
                request.Method = "POST";
                request.ContentType = "application/json";

                request.Timeout = 120000;
                request.ReadWriteTimeout = 120000;

                // Convert object -> JSON
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(req);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

                request.ContentLength = bytes.Length;

                // Write body
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                // Read response
                using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();
                    msg = result;
                }

                if (msg.Length <= 0)
                {
                    var download = viewer.Attributes != null ? viewer.Attributes["download"] == "1" : false;
                    if (download)
                    {
                        context.Response.Clear();
                        context.Response.Write(urlFN);
                        context.Response.End();
                    }
                    else
                    {
                        if (printCf.isPDF.GetValueOrDefault(false))
                        {
                            viewer.Attributes["linkExcel"] = urlPDF;
                            viewer.Attributes["linkViewExcel"] = string.Format(Helper.EncodeHTML_VNN(Security.UrlBase() + "FileUpload/{0}.pdf"), filename);
                            viewer.Attributes["nameExcel"] = DisplayName;
                            Helper.viewFile(viewer);
                            //context.Response.Redirect(string.Format("../../../ViewPDFPublic/index.aspx?urlpdf=../FileUpload/{0}.pdf&zoomprint=0.999&zoom=page-width&remove=true&namedown={1}", filename, DisplayName));
                        }
                        else
                        {
                            context.Response.Clear();
                            context.Response.ContentType = "application/vnd.ms-excel";
                            context.Response.AddHeader("Content-Disposition", $"attachment;filename={DisplayName}.xls");

                            using (var memoryStream = new MemoryStream())
                            {
                                // Đọc file từ disk vào memory
                                using (var fileStream = new FileStream(urlExcel, FileMode.Open, FileAccess.Read))
                                {
                                    fileStream.CopyTo(memoryStream);
                                }

                                // Xóa file tạm ngay sau khi đã đọc vào RAM
                                File.Delete(urlExcel);

                                // Ghi trực tiếp vào Output stream để tiết kiệm RAM
                                memoryStream.WriteTo(context.Response.OutputStream);
                            }

                            context.Response.Flush();
                            context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    context.Response.Write(msg);
                }
            }
            else
            {
                context.Response.Write("<center><h1>Không có dữ liệu</h1></center>");
            }
        }

        public Dictionary<string, object> setCell(ICell columnT, ICell cell, PrintAnco2.InfoExcel infoExcel, ISheet s1, DataTable dt, IRow row
            , PrintAnco2 printCf, float maxHeight, HSSFWorkbook hssfwb, int iRow, string sothapphan, int columnMax, List<CellRangeAddress> merCell)
        {
            var StringCellValue = "";
            if (cell.CellType == CellType.Numeric)
                StringCellValue = cell.NumericCellValue.ToString();
            else if (cell.CellType != CellType.Formula)
                StringCellValue = cell.StringCellValue;

            var isDate = StringCellValue.StartsWith("{$");
            var isNumber = StringCellValue.StartsWith("{#");
            var isFomulaG = StringCellValue.StartsWith("{!!");
            var isFomula = StringCellValue.StartsWith("{!");
            var isImage = StringCellValue.StartsWith("{%");
            var isText = StringCellValue.StartsWith("{");

            var nameColumnData =
                isDate
                ?
                StringCellValue.Replace("{$", "").Replace("}", "")
                :
                isNumber
                ?
                StringCellValue.Replace("{#", "").Replace("}", "")
                :
                isFomulaG
                ?
                StringCellValue.Replace("{!!", "").Replace("}", "")
                :
                isFomula
                ?
                StringCellValue.Replace("{!", "").Replace("}", "")
                :
                isImage
                ?
                StringCellValue.Replace("{%", "").Replace("}", "")
                :
                isText
                ?
                StringCellValue.Replace("{", "").Replace("}", "")
                :
                StringCellValue;

            if (isDate)
            {
                var fmt = columnT.CellStyle.GetDataFormatString().Replace("\\{", "").Replace("\\}", "").Replace("m", "M").Replace("p", "m");
                var date = dt.Rows[iRow][nameColumnData];
                if (date != null & date != DBNull.Value)
                    columnT.SetCellValue(((DateTime?)date).Value.ToString(fmt));
            }
            else if (isNumber)
            {
                if (nameColumnData == "countRow")
                {
                    columnT.SetCellValue(iRow + 1);
                    columnT.SetCellType(CellType.Numeric);
                }
                else
                {
                    var numberData = dt.Rows[iRow][nameColumnData].ToString();
                    if (string.IsNullOrWhiteSpace(numberData))
                    {
                        columnT.SetCellType(CellType.Blank);
                    }
                    else
                    {
                        columnT.SetCellValue(double.Parse(numberData));
                        columnT.SetCellType(CellType.Numeric);
                        if (columnT.CellStyle.GetDataFormatString() == "#####")
                        {
                            columnT.CellStyle.DataFormat = HSSFUtils.CellDataFormat.GetDataFormat(hssfwb, sothapphan);
                        }
                    }
                }
            }
            else if (isFomulaG)
            {
                columnT.SetCellValue(StringCellValue);
            }
            else if (isFomula)
            {
                var cellFormula = nameColumnData;
                if (nameColumnData == "SUMGROUP")
                {
                    cellFormula = string.Format("SUM({0}{1}:{0}{2})", infoExcel.Alphabet[columnT.ColumnIndex], infoExcel.startSumGroup + 1, row.RowNum);
                }
                else if (nameColumnData == "SUMREPORT")
                {
                    cellFormula = string.Format("SUM({0}{1}:{0}{2})/{3}", infoExcel.Alphabet[columnT.ColumnIndex], infoExcel.startSumReport + 1, row.RowNum, infoExcel.rptGroupFooter == null ? 1 : 2);
                }
                else
                {
                    cellFormula = cellFormula
                    .Replace("[0]", (row.RowNum + 1).ToString())
                    .removeAllSpaceOrTrimText(true);
                }

                //lstFormula.Add(new AvariablePrj.lstFormula()
                //{
                //    row = row.RowNum + 1,
                //    col = columnT.ColumnIndex + 1,
                //    formula = cellFormula
                //});

                try
                {
                    columnT.SetCellFormula(cellFormula);
                    columnT.SetCellType(CellType.Formula);

                    if (columnT.CellStyle.GetDataFormatString() == "#####")
                    {
                        columnT.CellStyle.DataFormat = HSSFUtils.CellDataFormat.GetDataFormat(hssfwb, sothapphan);
                    }
                }
                catch
                {
                    columnT.SetCellType(CellType.Blank);
                }
            }
            else if (isImage)
            {
                var linkIMG_ = dt.Rows[iRow][nameColumnData].ToString();

                if (linkIMG_.StartsWith("http://"))
                {
                    cell.SetCellValue(linkIMG_);
                    linkIMG_ = "";
                }
                else if (!linkIMG_.StartsWith("https://"))
                    linkIMG_ = ExcuteSignalRStatic.mapPathSignalR("~/" + linkIMG_);

                float? top = null, left = null, width = null, height = null;
                if (cell.CellComment != null)
                {
                    var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(cell.CellComment.String.String);
                    if (json != null)
                    {
                        top = float.Parse(json["top"]);
                        left = float.Parse(json["left"]);
                        width = float.Parse(json["width"]);
                        height = float.Parse(json["height"]);
                    }

                    lstRemoveComment.Add(cell);
                }

                if (File.Exists(linkIMG_) | linkIMG_.StartsWith("https"))
                {
                    lstImage.Add(new AvariablePrj.lstImage()
                    {
                        id = StringCellValue,
                        column = columnT.ColumnIndex,
                        columnLast = columnT.ColumnIndex,
                        row = row.RowNum,
                        rowLast = row.RowNum,
                        link = linkIMG_,
                        topIMG = top,
                        leftIMG = left,
                        widthIMG = width,
                        heightIMG = height
                    });
                }

                //cell.SetCellValue("");
            }
            else if (isText)
            {
                var strTxt = dt.Rows[iRow][nameColumnData].ToString();
                columnT.SetCellValue(strTxt);
                //if (columnT.CellStyle.WrapText & !cell.IsMergedCell)
                //{
                //    var font = columnT.CellStyle.GetFont(hssfwb);
                //    var widthColumn = s1.GetColumnWidth(columnT.ColumnIndex);
                //    var widthOfVal = caculateWidthColumnOfValue(s1, columnT);
                //    if (widthOfVal > widthColumn)
                //        maxHeight = -1;
                //}
                columnT.SetCellType(CellType.String);
            }
            else
            {
                columnT.SetCellValue(StringCellValue);
                columnT.SetCellType(CellType.String);
            }

            if (columnT.ColumnIndex >= columnMax)
            {
                foreach (var item in merCell)
                {
                    s1.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, item.FirstColumn, item.LastColumn));
                    var cellFirst = row.GetCell(item.FirstColumn);

                    if (cellFirst.CellStyle.WrapText)
                    {
                        int maxWidth = 0;
                        for (var cc = item.FirstColumn; cc <= item.LastColumn; cc++)
                            maxWidth += s1.GetColumnWidth(cc);


                        var cellCopyAt = item.FirstColumn + columnMax * 2 + 1;
                        var cellCopy = row.CreateCell(cellCopyAt);
                        cellCopy.SetCellType(cellFirst.CellType);
                        cellCopy.CellStyle = cellFirst.CellStyle;
                        try
                        {
                            cellCopy.SetCellValue(cellFirst.StringCellValue);
                        }
                        catch { }

                        if (iRow == 0)
                        {
                            s1.SetColumnWidth(cellCopyAt, maxWidth);
                            s1.SetColumnHidden(cellCopyAt, true);
                        }

                        var font = cellFirst.CellStyle.GetFont(hssfwb);
                        //var defaultHeightMerge = printCf.GetHeight(row.HeightInPoints, maxWidth * PrintAnco2.hesoColumn, cellFirst.StringCellValue, font);
                        //if (defaultHeightMerge > row.HeightInPoints)
                        //    maxHeight = -1;
                    }
                }
            }

            return new Dictionary<string, object>()
            {
                { "cell", columnT },
                { "maxHeight", maxHeight }
            };
        }

        public int caculateWidthColumnOfValue(ISheet s1, ICell columnT)
        {
            var wrapTxt = columnT.CellStyle.WrapText;
            var indexCP = columnT.ColumnIndex + 53;
            var cellCP = columnT.CopyCellTo(indexCP);
            cellCP.CellStyle.WrapText = false;
            s1.AutoSizeColumn(indexCP);
            columnT.CellStyle.WrapText = wrapTxt;
            var cellCPWidth = s1.GetColumnWidth(indexCP);
            s1.GetRow(columnT.RowIndex).RemoveCell(s1.GetRow(columnT.RowIndex).GetCell(indexCP));

            return cellCPWidth;
        }

        public void setMaxHeight(IRow row, float maxHeight)
        {
            if (maxHeight > -1)
                row.HeightInPoints = maxHeight;
            else
                row.Height = -1;
        }

        public PrintAnco2.IRowICellCopy createIrowICell(ISheet s1, int row, int maxColumn, List<CellRangeAddress> cellRangeAddressAll)
        {
            var rowCP = new PrintAnco2.IRowICellCopy();
            rowCP.row = s1.GetRow(row);
            rowCP.mergeCells = cellRangeAddressAll.Where(s => s.FirstRow == row).ToList();
            rowCP.cells = new List<ICell>();
            for (var iCol = 0; iCol <= maxColumn; iCol++)
            {
                var cell = rowCP.row.GetCell(iCol);
                if (cell != null)
                    rowCP.cells.Add(cell);
            }
            var defaultHeightFT = rowCP.row.HeightInPoints;

            rowCP.defaultHeight = rowCP.row.HeightInPoints;
            return rowCP;
        }
    }
}