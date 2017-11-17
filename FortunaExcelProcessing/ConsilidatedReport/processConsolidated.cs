using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SQLite;
using FortunaExcelProcessing.Objects;
using System.Drawing;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    public class processConsolidated
    {
        static ISheet _sheet;
        static IWorkbook _wb;
        static int _numOfFarms; static string _farmArea; static string _farmName;
        static int[] _indent = InitRowLabels.indentation();
        static List<string> _rowLabels = InitRowLabels.labelList();
        static int[] _dataCells = InitFormulae.dataCellArray();

        public void createWorkBook(string path, string date, List<DataHolder> dict)
        {
            ConsolUtil.GetDate(date);
            _wb = new XSSFWorkbook();
            _sheet = _wb.CreateSheet(DateStorage.PartialDate);
            workbookData(dict);

            if (File.Exists(path))
                File.Delete(path);

            using (FileStream fs = File.Create(path))
            {
                _wb.Write(fs);
                _wb.Close();
            }
        }

        private static void workbookData(List<DataHolder> dict)
        {
            _numOfFarms = ConsolUtil.getNumberofFarms();

            for (int row = 0; row < _rowLabels.Count; row++)
            {
                rowLabeler(row, dict);
            }
            
            int col = 1;

            foreach (DataHolder b in dict)
            {
                ICell cell;
                
                string stripper = b.Data_array.Substring(1, b.Data_array.Length - 2);
                string[] sArray = stripper.Split(',');

                {
                    XSSFCellStyle style = (XSSFCellStyle)_wb.CreateCellStyle();
                    XSSFFont font = (XSSFFont)_wb.CreateFont();
                    font.FontName = "Arial";

                    cell = _sheet.GetRow(0).CreateCell(col);
                    style.SetFillForegroundColor(new XSSFColor(Color.FromArgb(255, 242, 204)));
                    style.FillPattern = FillPattern.SolidForeground;
                    style.Alignment = HorizontalAlignment.Left;

                    try
                    {
                        
                        cell.SetCellValue(ConsolUtil.GetFarmName(b.Branch_id));
                    }
                    catch
                    {
                        cell.SetCellValue("N/A");
                    }
                    font.FontHeightInPoints = 14;
                    style.SetFont(font);
                    cell.CellStyle = style;
                }

                for (int i = 0; i < sArray.Length; i++)
                {
                    XSSFCellStyle style = (XSSFCellStyle)_wb.CreateCellStyle();
                    XSSFFont font = (XSSFFont)_wb.CreateFont();
                    
                    cell = _sheet.GetRow(_dataCells[i]).CreateCell(col);
                    style.Alignment = HorizontalAlignment.Left;
                    ConsolUtil.InputDataToSheet(sArray[i], cell);
                    
                    font.FontHeightInPoints = 11;
                    style.SetFont(font);
                    cell.CellStyle = style;
                }

                col++;
            }
            _sheet.AutoSizeColumn(1);
            _sheet.AutoSizeColumn(2);
            _sheet.AutoSizeColumn(1);
        }


        private static void rowLabeler(int row, List<DataHolder> dict)
        {
            ICell cell; XSSFCellStyle style; XSSFFont font;

            font = (XSSFFont)_wb.CreateFont();
            if (row == 0)
            {
                font.FontHeightInPoints = 14;
                font.IsBold = true;
            }
            else if (row == 1 || row == 6 || row == 11 || row == 16 || row == 22 || row == 26 || row == 29 || row == 34)
            {
                font.FontHeightInPoints = 12;
                font.IsBold = true;
            }
            else
            {
                font.FontHeightInPoints = 11;
                font.IsBold = true;
            }

            font.FontName = "Arial";

            style = (XSSFCellStyle)_wb.CreateCellStyle();
            style.SetFont(font);
            style.SetFillForegroundColor(new XSSFColor(Color.FromArgb(255, 242, 204)));
            style.FillPattern = FillPattern.SolidForeground;
            style.Alignment = HorizontalAlignment.Left;

            if (_indent.Contains(row))
            {
                cell = _sheet.CreateRow(row).CreateCell(1);
            }
            else
            {
                cell = _sheet.CreateRow(row).CreateCell(0);
            }
            cell.SetCellValue(_rowLabels[row]);
            //cell.SetCellValue(dict[row].Data_array);
            cell.CellStyle = style;
        }
    }
}
