using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel; 

namespace FortunaExcelProcessing
{
    public static class ErrorReporter
    {
        private List <String> _errorList;

        public static void HardErrorRecorder(string errorLine, ICell cell)
        {
            _hardErrorList.Add(errorLine,cell);
        }

        public static void RecordErrors()
        {

        }

        //THis method is supposed to check if any of the lists contain errors, and then
        // return a specific number to indicate what list should be read.
        public static int CheckStatus()
        {
            if (_softErrorList.Count == 0 && _hardErrorList.Count == 0)
            {
                return 1;
            }
            else if (_softErrorList.Count == 0 && _hardErrorList.Count >0)
            {
                return 2;
            }
            else if (_hardErrorList.Count == 0 && _softErrorList.Count > 0)
            {
                return 3;
            }
            else
                return 4;
        }

        //This method simply resets the errorLists
        public static void ResetErrors()
        {
            _softErrorList = new List<string>();
            _hardErrorList = new Dictionary<string, ICell>();
        }
    }
}
