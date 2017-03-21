using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SQLite;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    class FormulaFormatting
    {
        public static void inputCellFifteen(ICell cell, int col, int farmId)
        {
            string e6 = "", e7 = "";
            string procString = ConsolUtil.getCows(farmId);
            Console.WriteLine(procString);
            procString = procString.Substring(1, procString.Length - 1);
            string[] outstring = procString.Split(',');

            e6 = string.Format("{0}", outstring[0]);
            e7 = string.Format("{0}", outstring[1]);

            Console.WriteLine(e6 + e7);
            Console.ReadLine();
            string formula = string.Format("{0}+{1}", e6, e7);
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
