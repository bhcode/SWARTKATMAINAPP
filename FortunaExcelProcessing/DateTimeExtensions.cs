using System;

namespace FortunaExcelProcessing
{
    public static class DateTimeExtensions
    {
        //<summary>
        //Used to define the start of the week - applied to any files uploaded.
        //Mainly of use for generating the consolidated report.
        //</summary>
        //<param name="dt">original date time passed in</param>
        //<param name="dt">expected day of week to start at</param>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek weekStart)
        {
            int diff = dt.DayOfWeek - weekStart;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
    }
}