using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    public class DateStorage
    {
        static string _fullDate;
        static string _partialDate;

        public static string FullDate
        {
            get
            {
                return _fullDate;
            }

            set
            {
                _fullDate = value;
            }
        }

        public static string PartialDate
        {
            get
            {
                return _partialDate;
            }

            set
            {
                _partialDate = value;
            }
        }
    }
}
