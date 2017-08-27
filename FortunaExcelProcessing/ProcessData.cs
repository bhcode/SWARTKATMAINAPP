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
    public class ProcessData
    {
        private FileStream _file;
        private IWorkbook _wb;
        private List<string> _tabNames = new List<string>();

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
            {
                Directory.CreateDirectory(@"C:\Database\");
            }

            FilePaths.DBFilePath = (@"C:\Database\database.sqlite");

            if (!Util.CheckForTable("Labels"))
            {
                DBExtras.Labels.MakeLabels(@"C:\Database\database.sqlite");
            }

            if (!File.Exists(FilePaths.DBFilePath))
            {
                SQLiteConnection.CreateFile(FilePaths.DBFilePath);
            }

        }

        public void processSheet(string tabName)
        {
            if (Util.CheckForSheet(tabName, _wb))
            {
                ITableEditor o = WeeklySheetFactory.CreateSheet(tabName);
                if (o != null)
                {
                    o.EditTable(_wb.GetSheet(tabName));
                }
            }
        }

        public void CloseWorkbook()
        {
            _wb.Close();
        }
    }
}
