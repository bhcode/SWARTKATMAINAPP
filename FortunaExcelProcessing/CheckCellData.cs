using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using System.Data.SQLite;

namespace FortunaExcelProcessing
{
    ///<summary>
    ///This class is utilized to check cell information, process it and return it.
    ///Error objects are also added to the static lists for soft errors.
    ///</summary>
    public static class CheckCellData
    {

        ///<summary>
        ///Method for processing any cells that have a string value in them.
        ///It also allows for conversion of numeric values. Return is always a string
        ///</summary>
        public static string CellTypeString(ICell _cell)
        {
            if (_cell == null)
            {
                ErrorHandling.ErrorReporter.SoftErrors.Add(new ErrorHandling.Error(_cell, "Cell is null"));
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

        ///<summary>
        ///This method is used for dates that do not appear in a typic dd:MM:YYYY way
        ///Most dates within the Weekly Data reports will be formatted as 'Jan 6'
        ///</summary>
        public static DateTime CellWeirdDate(ICell _cell)
        {
            if (DateUtil.IsCellDateFormatted(_cell))
            {
                DateTime date = _cell.DateCellValue;
                ICellStyle style = _cell.CellStyle;
                string format = style.GetDataFormatString().Replace('m', 'M');
                if (date.Month < 6)
                    date.AddYears(1);
                return date;
            }
            return DateTime.MinValue;
        }

        ///<summary>
        ///Reads in a numeric cell and returns a number.
        ///If -1 is returned assume that the cell is not a numeric format.
        ///</summary>
        public static double CellTypeNumeric(ICell _cell)
        {
            if (_cell == null)
            {
                ErrorHandling.ErrorReporter.SoftErrors.Add(new ErrorHandling.Error(_cell, "Cell is not of the correct format"));
                return -1; 
            }
            if (_cell.CellType != CellType.Numeric)
            {
                ErrorHandling.ErrorReporter.SoftErrors.Add(new ErrorHandling.Error(_cell, "Cell is not of the correct format"));
                return -1;
            }
            return _cell.NumericCellValue;
        }

        ///<summary>
        ///Method for processing typical dates that can be understood using C#'s datetime without formatting.
        ///</summary>
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

        ///<summary>
        ///Currently throws an exception, in the future this method is intended to diagnose
        ///and return a value for a cell that the user doesn't know the value for.
        ///</summary>
        public static string CellTypeUnknown(ICell _cell)
        {
            ErrorHandling.ErrorReporter.HardErrors.Add(new ErrorHandling.Error(_cell, "This cell is an unknown"));
            throw new NotImplementedException();
        }
    }
}
