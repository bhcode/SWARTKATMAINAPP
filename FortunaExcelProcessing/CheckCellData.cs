using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using System.Data.SQLite;

namespace FortunaExcelProcessing
{
    public static class CheckCellData
    {
        public static string CellTypeString(ICell _cell)
        {
            if (_cell == null)
            {
                return "-1";
            }
            else if (_cell.CellType == CellType.String)
            {
                return _cell.RichStringCellValue.ToString();
            }
            else
            {
                _cell.SetCellType(CellType.String);
            }

            //if (_cell.CellType == CellType.Numeric)
            //{
            //    return _cell.NumericCellValue.ToString();
            //}
            //if (_cell.CellType == CellType.Formula)
            //{
            //    return _cell.NumericCellValue.ToString();
            //}
            return _cell.RichStringCellValue.ToString();
        }

        public static double CellTypeNumeric(ICell _cell)
        {
            
            if (_cell == null)
            {
                //ErrorHandling.ErrorReporter.SoftErrors.Add(new ErrorHandling.Error(_cell, "Cell is not of the correct format"));
                return -1; 
            }
            if (_cell.CellType != CellType.Numeric)
            {
                //ErrorHandling.ErrorReporter.SoftErrors.Add(new ErrorHandling.Error(_cell, "Cell is not of the correct format"));
                return -1;
            }
            return _cell.NumericCellValue;
        }

        public static DateTime CellTypeDate(ICell _cell)
        {
            if (_cell == null)
            {
                return DateTime.MinValue;
            }
            if (DateUtil.IsCellDateFormatted(_cell))
            {
                DateTime date = _cell.DateCellValue;
                ICellStyle style = _cell.CellStyle;
                string format = style.GetDataFormatString().Replace('m', 'M');
                return date;
            }
            //if (_cell.CellType != CellType.)
            //{
            //    return DateTime.MinValue;
            //}
            return DateTime.MaxValue;
        }

        public static string CellTypeUnknown(ICell _cell)
        {
            //ErrorHandling.ErrorReporter.HardErrors.Add(new ErrorHandling.Error(_cell, "This cell is an unknown"));
            throw new NotImplementedException();
        }
    }
}
