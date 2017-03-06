using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing
{
    public class WeeklySheetFactory
    {
        static public ITableMaker CreateSheet(String sheetName)
        {
            ITableMaker sheetChoser = null;

            switch (sheetName)
            {
                case "paddocks":
                    sheetChoser = new WeeklyProcessing.CreatePaddocksTable();
                    break;
                case "Weekly Comments":
                    sheetChoser = new WeeklyProcessing.CreateCommentsTable();
                    break;
            }

            return sheetChoser;
        }

    }
}
