using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using System.Data.SQLite;

namespace FortunaExcelProcessing.MonthlyProcessing
{
    class EditMonthlyInputTable : ITableEditor
    {
        ISheet _sheet;
        string sql; SQLiteCommand command; SQLiteConnection dBConnection;
        Dictionary<string, int> tables = new Dictionary<string, int>() { { "MonthlyInputs", 6 }, { "Maintenance", 2 }, { "HR", 2 }, { "HealthSafety", 4 }, { "Effluent Management", 9 }, { "General", 7 } };

        public void EditTable(ISheet sheet)
        {
            //check if monthly data (gone by months) change with each upload, if so need to be able to overwrited existing data
            dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));
            _sheet = sheet;
            dBConnection.Open();

            readInData();

            dBConnection.Close();
        }

        private void readInData()
        {
            foreach (KeyValuePair<string, int> dict in tables)
            {
                tableCreation(dict.Key);
                int rowAmount = dict.Value;
                int[] rowStart = { 10, 18, 25, 29, 35, 46 };
                string input = "[";

                for (int i = 4; i < _sheet.GetRow(11).LastCellNum; i++)
                {
                    for (int a = rowStart[i - 4]; a < rowAmount; a++)
                    {
                        ICell cell = _sheet.GetRow(a).GetCell(i);
                        double yn = CheckCellData.CellTypeNumeric(cell);
                        if (yn == 0)
                            input += "N";
                        else if (yn == 1)
                            input += "Y";
                        else if (yn == -1)
                            input += "-1";
                        else
                            input += yn + "%";
                        input += a == rowAmount - 1 ? "]" : ",";
                    }
                    command.CommandText = $"INSERT INTO {dict.Key} (farmid,date,category,description) VALUES (0,PLACEHOLDMONTH,{input})";
                    command.ExecuteNonQuery();
                }
            }
        }

        /*  General Management Table:   AI, 0, Jun, [Y,Y,N,50%,Y] skip  updated repoduction
            Maintenance:                AI, 0, Jun, [Y,Y]
            HR:                         AI, 0, Jun, [Y,Y]
            Health and Safety:          AI, 0, Jun, [Y,N,N,N]
            Effluent Management:        AI, 0, Jun, [Y,Y,Y,Y,Y,Y,Y,Y,Y]
            General:                    AI, 0, Jun, [N,Y,Y,Y,Y,Y,0] herd BW&PW currently always null, exclude
        */


        private void tableCreation(string tableName)
        {
            if (!util.CheckForTable(tableName))
            {
                //issue: id for farm Betapahu does not exist, id creation is currently dependent on Weekly  excel files. manual input of farm names may be better (via form?)
                sql = $"CREATE TABLE {tableName} (id INTEGER PRIMARY KEY AUTOINCREMENT, farmid INTEGER, month VARCHAR(15), data varchar(600));";
                command = new SQLiteCommand(sql, dBConnection);
                command.ExecuteNonQuery();
            }
        }
    }


}
