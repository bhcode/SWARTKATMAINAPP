using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing
{
    // <summary>
    // This is a factory class that takes in a sheet name and then determines what method to call.
    // <return>
    // The only return value is a selected class in the WeeklyProcessing namespace
    // </return>
    // </summary>
    public class WeeklySheetFactory
    {
        static public ITableEditor CreateSheet(String sheetName)
        {
            ITableEditor sheetChoser = null;

            switch (sheetName)
            {
                case "paddocks":
                    sheetChoser = new WeeklyProcessing.EditPaddocksTable();
                    break;
                case "Weekly Comments":
                    sheetChoser = new WeeklyProcessing.EditCommentsTable();
                    break;
                case "Input Page":
                    sheetChoser = new WeeklyProcessing.EditInputTable();
                    break;
                case "Weekly Data":
                    sheetChoser = new WeeklyProcessing.EditWeeklyDataTable();
                    break;
            }

            return sheetChoser;
        }

    }
}
