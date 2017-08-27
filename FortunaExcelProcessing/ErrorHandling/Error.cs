using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing.ErrorHandling
{
    public class Error
    {
        public ICell Cell { get; set; }

        public string Description { get; set; }

        public int Severity { get; set; }

        public string ErrorCode { get; set; }

        public string ExtraInfo { get; set; }

        public Error(string s)
        {
            Description = s;
        }

        public Error(ICell c, string s)
        {
            Cell = c;
            Description = s;
        }
    }
}
