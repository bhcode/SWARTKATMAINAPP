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
        //HUGE WIP! should be called to check cell data & Return cell?? currently returns cell.tostring
        public static string CheckCell(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
            }
            return cell.ToString();
        }
    }
}
