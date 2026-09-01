/**
 *  OfficeToPDF command line PDF conversion for Office 2007/2010/2013/2016
 *  Copyright (C) 2011-2018 Cognidox Ltd
 *  https://www.cognidox.com/opensource/
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 */

using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Data;
using System.Data.Linq;

namespace OfficeToPDF
{
    /// <summary>
    /// Handle conversion of Excel files
    /// </summary>
    /// 

    public class WordConverter : Converter
    {
        public bool isPDF { get; set; }
        public List<AvariablePrj.lstTextReplace> lstTextReplace { get; set; }
        public List<int> tablePos = new List<int>();
        public List<int> tableDetailPos = new List<int>();
        public List<System.Data.DataTable> tbls { get; set; }
        public WordConverter()
        {

        }

        // Convert method
        public string Convert(string input, string output)
        {
            object oMissing = System.Reflection.Missing.Value;
            object isVisible = true;
            object readOnly = false;
            object oFalse = false;
            object oTrue = true;

            Microsoft.Office.Interop.Word.Application oWord = null;
            Microsoft.Office.Interop.Word.Document oDoc = null;
            if (lstTextReplace == null)
                lstTextReplace = new List<AvariablePrj.lstTextReplace>();

            string msg = "";
            try
            {
                var format = isPDF ? WdSaveFormat.wdFormatPDF : WdSaveFormat.wdFormatDocument97;

                // Create an instance of Word.exe
                oWord = new Microsoft.Office.Interop.Word.Application
                {

                    // Make this instance of word invisible (Can still see it in the taskmgr).
                    Visible = false
                };

                object oInput = input;
                object oOutput = output;
                object oFormat = format;

                // Load a document into our instance of word.exe
                oDoc = oWord.Documents.Open(
                    ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                    );

                // Make this document the active document.
                oDoc.Activate();

                var lstRanges = new List<Range>();
                foreach (Range range in oDoc.StoryRanges)
                    lstRanges.Add(range);

                if (tablePos != null)
                {
                    var funcFindCellNext = new Func<Cell, int, Cell>(
                        (cell, so) =>
                            {
                                for (int iF = 0; iF < so; iF++)
                                    cell = cell.Previous;
                                return cell;
                            }
                        );

                    for (int i = 0; i < tablePos.Count; i++)
                    {
                        var tbl = tbls[i];
                        Table wTable = oDoc.Tables[tablePos[i]];
                        int posDT = tableDetailPos[i];
                        int rowC = 100;

                        Range range = wTable.Range;
                        var cells = range.Cells;
                        var lstCells = new List<Cell>();

                        foreach(Cell cell in cells) {
                            if (cell.RowIndex == posDT)
                                lstCells.Add(cell);
                        }

                        var limitRow = tbl.Rows.Count - 1;

                        for (int iTbl = 0; iTbl < tbl.Rows.Count; iTbl++)
                        {
                            if(iTbl < limitRow)
                                wTable.Rows.Add(lstCells[lstCells.Count - 1]);

                            for (int iCell = 0; iCell < lstCells.Count; iCell++)
                            {
                                var cellForm = lstCells[iCell];
                                if (iTbl < limitRow)
                                {
                                    var cellTo = funcFindCellNext(lstCells[iCell], lstCells.Count);

                                    var copyFrom = cellForm.Range;
                                    var copyTo = cellTo.Range;

                                    copyFrom.MoveEnd(WdUnits.wdCharacter, -1);
                                    copyTo.FormattedText = copyFrom.FormattedText;

                                    lstCells[iCell] = cellTo;
                                }

                                var ava = cellForm.Range.Text.Replace("\r\a", "").removeAllSpaceOrTrimText(false);
                                var avaTbl = ava.Replace("{", "").Replace("}", "");
                                var id = "{" + avaTbl + rowC + "}";
                                cellForm.Range.Find.Execute(ava, ref oMissing, ref oMissing, ref oMissing, ref oFalse, ref oMissing,
                                ref oMissing, WdFindWrap.wdFindStop, ref oMissing, tbl.Rows[limitRow - iTbl][avaTbl].ToString(),
                                WdReplace.wdReplaceOne, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                                lstRanges = lstRanges.Where(s => s != cellForm.Range).ToList();
                                //lstTextReplace.Add(new AvariablePrj.lstTextReplace()
                                //{
                                //    oldT = id,
                                //    newT = tbl.Rows[limitRow - iTbl][avaTbl].ToString()
                                //});
                            }
                            rowC++;
                        }
                    }
                }

                foreach (Range range in lstRanges)
                {
                    Find find = range.Find;
                    object replace = WdReplace.wdReplaceAll;
                    object findWrap = WdFindWrap.wdFindContinue;
                    foreach (var text in lstTextReplace)
                    {
                        object replaceThis = text.oldT;
                        object replaceThisWith = text.newT;
                        find.Execute(ref replaceThis, ref oMissing, ref oMissing, ref oMissing, ref oFalse, ref oMissing,
                        ref oMissing, ref findWrap, ref oMissing, ref replaceThisWith,
                        ref replace, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    }
                }

                var shapes = oDoc.Shapes;

                foreach (Shape shape in shapes)
                {
                    foreach (var text in lstTextReplace)
                    {
                        try
                        {
                            string replaceThis = text.oldT;
                            string replaceThisWith = text.newT;
                            var initialText = shape.TextFrame.TextRange.Text;
                            var resultingText = initialText.Replace(replaceThis, replaceThisWith);
                            shape.TextFrame.TextRange.Text = resultingText;
                        }
                        catch { }
                    }
                }

                // Save this document using Word
                oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                );
            }
            catch(Exception ex)
            {
                msg = ex + "";
            }

            if (oDoc != null)
                oDoc.Close(false);

            if (oWord != null)
                oWord.Quit(false);
            
            return msg;
        }
    }
}
