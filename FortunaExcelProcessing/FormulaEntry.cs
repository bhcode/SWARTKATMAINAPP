using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing
{
    public class FormulaEntry
    {
        public int Fid { get; set; }
        public int Row { get; set; }
        public string Formula { get; set; }

        public FormulaEntry(int fid, int row, string formula)
        {
            this.Fid = fid;
            this.Row = row;
            this.Formula = formula;
        }

    }
}
