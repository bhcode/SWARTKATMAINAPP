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
        public string StringData { get; set; }
        public int Severity { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }


        public Error(string s)
        {
            Cell = c;
            StringData = s;
        }

        public Error(ICell c, string s)
        {
            Cell = c;
            StringData = s;
        }

        ///<see cref="https://msdn.microsoft.com/en-us/library/seyhszts(v=vs.110).aspx"/>
        ///<see cref="https://docs.microsoft.com/en-us/dotnet/articles/csharp/programming-guide/exceptions/exception-handling"/>
    }
}
