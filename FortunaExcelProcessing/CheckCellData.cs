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
                //ErrorHandling.ErrorReporter.SoftErrors.Add(new ErrorHandling.Error(_cell, "Cell is null"));
                return "";
            }
            if (_cell.CellType == CellType.String)
            {
                return _cell.RichStringCellValue.ToString();
            }
            if (_cell.CellType == CellType.Numeric)
            {
                return _cell.NumericCellValue.ToString();
            }
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
            if (_cell.CellType != CellType.Numeric)
            {
                return DateTime.MinValue;
            }
            return _cell.DateCellValue;
        }

        public static string CellTypeUnknown(ICell _cell)
        {
            //ErrorHandling.ErrorReporter.HardErrors.Add(new ErrorHandling.Error(_cell, "This cell is an unknown"));
            throw new NotImplementedException();
        }
    }
}
