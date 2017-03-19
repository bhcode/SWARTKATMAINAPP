using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing
{
    class MonthlySheetFactory
    {
        static public ITableEditor CreateSheet(String sheetName)
        {
            ITableEditor sheetChoser = null;

            switch (sheetName)
            {
                case "Monthly Inputes":
                    sheetChoser = new MonthlyProcessing.EditMonthlyInputTable();
                    break;
            }

            return sheetChoser;
        }
    }
}
