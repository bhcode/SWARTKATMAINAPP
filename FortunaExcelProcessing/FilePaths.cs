using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF;
using System.Data.SQLite;

namespace FortunaExcelProcessing
{
    public static class FilePaths
    {

        static private string _reportFilePath;
        static private string _dBFilePath;

        static public string ReportFilePath
        {
            get
            {
                return _reportFilePath;
            }

            set
            {
                _reportFilePath = value;
            }
        }

        static public string DBFilePath
        {
            get
            {
                return _dBFilePath;
            }

            set
            {
                _dBFilePath = value;
            }
        }

    }
}
