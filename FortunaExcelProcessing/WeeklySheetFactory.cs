using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing
{
    public class WeeklySheetFactory
    {
        static public ITableEditor CreateSheet(String sheetName)
        {
            ITableEditor sheetChoser = null;

            switch (sheetName)
            {
                case "paddocks":
                    sheetChoser = new WeeklyProcessing.EditHivesTable();
                    break;
                case "Weekly Comments":
                    sheetChoser = new WeeklyProcessing.EditObservationsTable();
                    break;
                case "Weekly Data":
                    sheetChoser = new WeeklyProcessing.EditWeeklyDataTable();
                    break;
            }

            return sheetChoser;
        }

    }
}
