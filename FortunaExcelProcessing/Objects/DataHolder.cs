using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing.Objects
{
    public class DataHolder
    {
        int branch_id;
        string data_array;

        public int Branch_id { get => branch_id; set => branch_id = value; }
        public string Data_array { get => data_array; set => data_array = value; }
    }
}
