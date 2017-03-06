using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FortunaExcelProcessing
{
    public class ErrorReporter
    {
        private List <String> _errorList;

        public ErrorReporter()
        {
            //not sure if this is needed
            _errorList = new List<string>();
        }

        public void RecordErrors()
        {

        }

        public bool CheckStatus()
        {
            return true;
        }
    }
}
