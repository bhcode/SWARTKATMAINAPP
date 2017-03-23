using System;

namespace FortunaExcelProcessing
{
    public static class DateTimeExtensions
    {
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
