using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.Data.SQLite;

namespace FortunaExcelProcessing.WeeklyProcessing
{
    class EditHivesTable : ITableEditor
    {
        ISheet paddockSheet;
        SQLiteConnection _dbCon;

        public void EditTable(ISheet sheet)
        {
            paddockSheet = sheet;

            _dbCon = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));

            _dbCon.Open();

            if (!Util.CheckForTable("Hives"))
            {
                DBOperations.ExecuteDatabaseQuery("CREATE TABLE Hives (Hive_ID INTEGER PRIMARY KEY AUTOINCREMENT, Branch_ID INT (11), Date_Sent VARCHAR(30), Location VARCHAR(50), Honey_Super VARCHAR(50), Frames INT, Hive_Species VARCHAR(50), Forage_Enviornment(50))", _dbCon);
            }

            ProcessData(_dbCon);

            _dbCon.Close();
        }

        private void ProcessData(SQLiteConnection dbCon)
        {
            //default the date to the start of current week
            Util.Date = DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToString("yyyy-MM-dd");

            if (!CheckForExistingData())
            {
                for (int y = 0; y <= paddockSheet.LastRowNum; y++)
                {
                    IRow row = paddockSheet.GetRow(y);
                    if (row.GetCell(0) == null)
                        break;

                    string tmp;
                    if (row.GetCell((int)PaddockColumns.PaddockCropCol) == null)
                        tmp = "none";
                    else
                        tmp = row.GetCell((int)PaddockColumns.PaddockCropCol).ToString();

                    string data = string.Format("{0},'{1}','{2}',{3},'{4}')", Util.Farmid, Util.Date, row.GetCell((int)PaddockColumns.PaddockIDCol), row.GetCell((int)PaddockColumns.PaddockSizeCol), tmp);

                    DBOperations.ExecuteDatabaseQuery("INSERT INTO Hives VALUES(" + data, dbCon);
                }
            }
        }

        private bool CheckForExistingData()
        {
            string sql = $"SELECT Hive_ID FROM Hives WHERE Date_Sent = '{Util.Date}' AND  Branch_ID = '{Util.Farmid}'";
            SQLiteCommand command = new SQLiteCommand(sql, _dbCon);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }
    }
}
