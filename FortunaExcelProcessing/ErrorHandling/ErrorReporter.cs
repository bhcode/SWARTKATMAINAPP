using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace FortunaExcelProcessing.ErrorHandling
{
    /// <summary>
    /// The Error reporter is intended to take any exceptions from the GUI that occur when processing the excel documents
    /// Please note that exceptions must be added to the list, 
    /// </summary>
    public static class ErrorReporter
    {
        public static List<Error> SoftErrors { get; set; }

        public static List<Error> HardErrors { get; set; }

        /// <summary>
        /// Calls methods to process errors dependent on how many error are in the list
        /// </summary>
        public static void CheckStatus()
        {
            if (HardErrors.Count > 0)
            {
                ProcessHardErrrors processHardErrors = new ProcessHardErrrors();
            }
            if (SoftErrors.Count > 0)
            {
                ProcessSoftErrors processSoftErrors = new ProcessSoftErrors();
            }
        }

        /// <summary>
        /// Clear error lists
        /// </summary>
        public static void ResetErrors()
        {
            SoftErrors = new List<Error>();
            HardErrors = new List<Error>();
        }
    }
}
