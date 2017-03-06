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
    public class FilePaths
    {

        private string _reportFilePath;
        private string _dBFilePath;

        public string ReportFilePath
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

        public string DBFilePath
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
