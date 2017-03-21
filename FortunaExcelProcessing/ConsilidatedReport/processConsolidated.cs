using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SQLite;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    public class processConsolidated
    {
        static ISheet _sheet; static IWorkbook _wb;
        static int _numOfFarms; static string _farmArea; static string _farmName;
        static int [] indent = InitRowLabels.indentation();
        static List<string> rowLabels = InitRowLabels.labelList();
        static int[] calcedCells = InitFormulae.calcCellArray();
        static int[] dataCells = InitFormulae.dataCellArray();

        public static void createWorkBook(string path)
        {
            ConsolUtil.getDate(); 
            FilePaths.DBFilePath = @"data source = C:\Database\database.sqlite; Version = 3;";
            _wb = new XSSFWorkbook();
            _sheet = _wb.CreateSheet(DateStorage.PartialDate);
            workbookData();

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                _wb.Write(fs);
                _wb.Close();
            }
        }

        private static void workbookData()
        {
            _numOfFarms = ConsolUtil.getNumberofFarms();

            for (int row = 0; row < 64; row++)
            {
                rowLabeler(row);
            }
            _sheet.AutoSizeColumn(1);

            Dictionary<int, string> databaseDatas = ConsolUtil.getData(DateStorage.FullDate);

            for (int col = 2; col < _numOfFarms + 2; col++)
            { 
                ICell cell;

                foreach (KeyValuePair<int, string> b in databaseDatas)
                {
                    string stripper = b.Value.Substring(1, b.Value.Length - 2);
                    string[] sArray = stripper.Split(',');
                    _farmName = ConsolUtil.getFarmName(b.Key);
                    _farmArea = ConsolUtil.getFarmArea(b.Key);

                    for (int i = 0; i < 33; i++)
                    {
                        if (i == 0) //Farm Name
                        {
                            XSSFCellStyle style = (XSSFCellStyle)_wb.CreateCellStyle();
                            XSSFFont font = (XSSFFont)_wb.CreateFont();
                            cell = _sheet.GetRow(0).CreateCell(col);
                            font.FontHeightInPoints = 10;
                            style.SetFont(font);
                            cell.CellStyle = style;
                            cell.SetCellValue(_farmName);
                        }
                        if (i == 1) //Farm Date
                        {
                            XSSFCellStyle style = (XSSFCellStyle)_wb.CreateCellStyle();
                            XSSFFont font = (XSSFFont)_wb.CreateFont();
                            cell = _sheet.GetRow(1).CreateCell(col);
                            font.FontHeightInPoints = 12;
                            font.SetColor(new XSSFColor(new byte[3] { 192, 0, 0 }));
                            style.Alignment = HorizontalAlignment.Center;
                            style.SetFont(font);
                            cell.CellStyle = style;
                            cell.SetCellValue(DateStorage.PartialDate);
                        }

                        if (i == 2) //Farm Area
                        {
                            cell = _sheet.GetRow(2).CreateCell(col);
                            ConsolUtil.inputDataToSheet(_farmArea, cell);
                        }

                        if (i == 15) //Needs particular formatting
                        {
                            cell = _sheet.GetRow(15).CreateCell(col);
                            FormulaFormatting.inputCellFifteen(cell, 15, b.Key);
                        }
                        else if (i == 55) //Needs particular formatting
                        {
                            cell = _sheet.GetRow(55).CreateCell(col);
                            FormulaFormatting.inputCellFiftyFive(cell, 55, _farmArea);
                        }

                        cell = _sheet.GetRow(dataCells[i]).CreateCell(col);
                        ConsolUtil.inputDataToSheet(sArray[i], cell);
                    }
                }

                List<FormulaEntry> formulae = InitFormulae.formulaeList(); 
                foreach (FormulaEntry formula in formulae)
                {
                    cell = _sheet.GetRow(formula.Row).CreateCell(col);
                    FormulaFormatting.inputCellFormula(string.Format(formula.Formula, ConsolUtil.NumToColName(col)), cell);
                }
            }
        }


        private static void rowLabeler(int row)
        {
            ICell cell; XSSFCellStyle style; XSSFFont font;

            font = (XSSFFont)_wb.CreateFont();
            if (row == 0)
                font.FontHeightInPoints = 14;
            else
                font.FontHeightInPoints = 12;
            font.FontName = "Arial";

            style = (XSSFCellStyle)_wb.CreateCellStyle();

            if (indent.Contains(row))
            {
                cell = _sheet.CreateRow(row).CreateCell(1);
                font.IsBold = false;
                style.SetFont(font);
            }
            else
            {
                cell = _sheet.CreateRow(row).CreateCell(0);
                font.IsBold = true;
                style.SetFont(font);
            }
            cell.SetCellValue(rowLabels[row]);
            cell.CellStyle = style;

            //PRFM and EFF
            if (row == 31)
            {
                ICell pCell = _sheet.GetRow(31).CreateCell(0);
                pCell.SetCellValue("Prfm. Index");
            }
            if (row == 33)
            {
                ICell eCell = _sheet.GetRow(33).CreateCell(0);
                eCell.SetCellValue("Eff. Index");
            }
        }
    }
}
