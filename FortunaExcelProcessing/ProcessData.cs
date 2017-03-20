using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF;
using System.IO;
using NPOI.SS.UserModel;
using System.Data.SQLite;

namespace FortunaExcelProcessing
{
    //https://github.com/tonyqus/npoi/tree/master/examples/xssf

    public class ProcessData
    {
        private FileStream _file;
        private IWorkbook _wb;
        private List<string> _tabNames = new List<string>();

        #region public properties
        public IWorkbook Wb
        {
            get { return _wb; }
        }

        public List<string> TabNames
        {
            get { return _tabNames; }
        }

        public ProcessData(string path)
        {
            _file = File.OpenRead(path);
        }
        #endregion

        public void CreateWorkBook()
        {
            _wb = WorkbookFactory.Create(_file);

            for (int i = 0; i < _wb.NumberOfSheets; i++)
            {
                _tabNames.Add(_wb.GetSheetName(i).ToString());
            }
        }

        public void createSQLiteDB()
        {
            if (!File.Exists(@"C:\Database\"))
                Directory.CreateDirectory(@"C:\Database\");
            FilePaths.DBFilePath = (@"C:\Database\database.sqlite");
            if (!util.CheckForTable("Formulae"))
                DBExtras.Formulas.MakeFormulae(@"C:\Database\database.sqlite");
            if (!util.CheckForTable("Labels"))
                DBExtras.Labels.MakeLabels(@"C:\Database\database.sqlite");
            if (!File.Exists(FilePaths.DBFilePath))
                SQLiteConnection.CreateFile(FilePaths.DBFilePath);
        }

        public void processSheet(string tabName)
        {
            ITableEditor o = WeeklySheetFactory.CreateSheet(tabName);
            if (o != null)
            {
                o.EditTable(_wb.GetSheet(tabName));
            }
        }
    }
}
