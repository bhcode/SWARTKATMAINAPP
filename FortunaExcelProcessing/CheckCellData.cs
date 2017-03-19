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
                return "";
            }
            if (_cell.CellType == CellType.String)
            {
                return _cell.RichStringCellValue.ToString();
            }
            return _cell.RichStringCellValue.ToString();
        }

        public static DateTime CellWeirdDate(ICell _cell)
        {
            if (DateUtil.IsCellDateFormatted(_cell))
            {
                DateTime date = _cell.DateCellValue;
                ICellStyle style = _cell.CellStyle;
                string format = style.GetDataFormatString().Replace('m', 'M');
                return date;
            }
            return DateTime.MinValue;
        }

        public static double CellTypeNumeric(ICell _cell)
        {
            if (_cell == null)
            {
                return -1;
            }
            if (_cell.CellType != CellType.Numeric)
            {
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
            if (_cell.CellType != CellType.Numeric) {
                return DateTime.MinValue;
            }
            return _cell.DateCellValue;
        }

        public static string CellTypeUnknown(ICell _cell)
        {
            throw new NotImplementedException();
        }
    }
}
