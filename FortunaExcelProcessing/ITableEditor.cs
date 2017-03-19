using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using NPOI.SS.UserModel;


namespace FortunaExcelProcessing
{
    public interface ITableEditor
    {
        void EditTable(ISheet sheet);
    }
}
