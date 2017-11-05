using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF;
using System.IO;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using FortunaExcelProcessing.Properties;

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

        public void OpenWorkBook()
        {
            _wb = WorkbookFactory.Create(_file);

            for (int i = 0; i < _wb.NumberOfSheets; i++)
            {
                _tabNames.Add(_wb.GetSheetName(i).ToString());
            }
        }

        /// <summary>
        /// creates a new local database if the directory or database does not currently exist
        /// Creates and populates a labels table
        /// </summary>
        public void createSQLiteDB()
        {
            if (!Directory.Exists(settings.Default.WorkingFolder))
            {
                Directory.CreateDirectory(settings.Default.WorkingFolder);
            }

            if (!Util.CheckForTable("Labels"))
            {
                DBExtras.Labels.MakeLabels(settings.Default.DbFilePath); //create labels table and populate it
            }

            if (!File.Exists(settings.Default.DbFilePath))
            {
                SQLiteConnection.CreateFile(settings.Default.DbFilePath); //create database at the specified file path
            }


        }

        public void processSheet(string tabName, int branchId)
        {
            if (Util.CheckForSheet(tabName, _wb))
            {
                ITableEditor o = WeeklySheetFactory.CreateSheet(tabName);
                if (o != null)
                {
                    o.EditTable(_wb.GetSheet(tabName), branchId);
                }
                else
                    throw new Exception("Object is null");
            }
            else
                throw new Exception("Sheet does not exist");
        }

        public void CloseWorkbook()
        {
            _wb.Close();
        }

        public bool ProcessAll(int branchId)
        {
            bool weekly = false, obs = false, hive = false;

            Task.Factory.StartNew(() => { processSheet("Weekly Data", branchId); weekly = true; });
            Task.Factory.StartNew(() => { processSheet("Weekly Observations", branchId); obs = true; });
            Task.Factory.StartNew(() => { processSheet("Hives", branchId); hive = true; });

            while(weekly == obs == hive == false) { }

            CloseWorkbook();
            return true;
        }
    }
}
