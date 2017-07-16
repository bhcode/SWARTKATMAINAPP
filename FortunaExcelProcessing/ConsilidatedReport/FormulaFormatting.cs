using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SQLite;
using System.Web.Script.Serialization;


namespace FortunaExcelProcessing.ConsilidatedReport
{
    class FormulaFormatting
    {
        public static void inputCellFifteen(ICell cell, int col, int farmId)
        {
            string e6 = "", e7 = "",e8 = "";
            string[] procString = ConsolUtil.getCows(farmId).Split(':');
            procString[1] = procString[1].Substring(2, procString[1].Length - 4);
            string[] tmp = procString[1].Split(',');

            e6 = string.Format("{0}", tmp[0]);
            e7 = string.Format("{0}", tmp[1]);
            e8 = string.Format("{0}", tmp[2]);

            string formula = string.Format("{0}+{1}+{2}", e6, e7,e8);
            cell.SetCellType(CellType.Formula);
            cell.SetCellFormula(formula);
        }

        public static void inputCellFiftyFive(ICell cell, int col, string farmArea)
        {
            string formula = string.Format("=IF({0}53=\"\";\"\";{0}53*{0}54/{1})", ConsolUtil.NumToColName(col), farmArea);
            cell.SetCellType(CellType.Formula);
            cell.SetCellFormula(formula);
        }

        public static void inputCellFormula(string formula, ICell cell)
        {
            cell.SetCellType(CellType.Formula);
            cell.SetCellFormula(formula);
        }
    }
}
