using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace FortunaExcelProcessing.ErrorHandling
{
    public static class ErrorReporter
    {
        public static List<Error> SoftErrors { get; set; }

        public static List<Error> HardErrors { get; set; }

        public static void ResetErrors()
        {
            SoftErrors = new List<Error>();
            HardErrors = new List<Error>();
        }
    }
}
