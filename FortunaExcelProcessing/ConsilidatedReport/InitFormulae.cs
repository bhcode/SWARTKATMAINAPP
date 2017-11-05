using System.Collections.Generic;
using System.Data.SQLite;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    class InitFormulae
    {
        public static int[] dataCellArray()
        {
            //int[] cd = { 4, 5, 6, 8, 9, 10, 11, 12, 14, 16, 17, 18, 21, 22, 24, 28, 35, 36, 37, 38, 40, 41, 42, 43, 44, 45, 46, 47, 48, 53, 54, 55, 56, 57, 58, 60, 61 };
            int[] cd = { 2, 3, 4, 5, 7, 8, 9, 10, 12, 13, 14, 15, 17, 18, 19, 20, 21, 23, 24, 25, 27, 28, 30, 31, 32, 33, 35, 36, 37 };
            return cd;
        }
    }
}
