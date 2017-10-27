using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using FortunaExcelProcessing.Properties;

namespace FortunaExcelProcessing.WeeklyProcessing
{
    class EditHivesTable : ITableEditor
    {
        ISheet _paddockSheet;
        SQLiteConnection _dbCon;
        SQLiteCommand _command;

        public void EditTable(ISheet sheet)
        {
            _paddockSheet = sheet;

            _dbCon = new SQLiteConnection($"Data Source={settings.Default.DbFilePath};Version=3;");
            _dbCon.Open();

            if (!Util.CheckForTable("Hives"))
            {
                string _sql = "CREATE TABLE Hives(Hive_ID INTEGER PRIMARY KEY AUTOINCREMENT, Branch_ID INT (11), Date_Sent VARCHAR(30), Location VARCHAR(50), Hive_Body VARCHAR(50),  Honey_Super VARCHAR(50), Frames INT(11), Hive_Species VARCHAR(50), Forage_Enviornment VARCHAR(50));";
                _command = new SQLiteCommand(_sql, _dbCon);
                _command.ExecuteNonQuery();
            }
            else
                throw new Exception("Table already exists");

            ProcessData(_dbCon);

            _dbCon.Close();
        }

        private void ProcessData(SQLiteConnection dbCon)
        {
            //default the date to the start of current week
            Util.Date = DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToString("yyyy-MM-dd");

            if (!CheckForExistingData())
            {
                for (int y = 2; y <= _paddockSheet.LastRowNum; y++)
                {
                    IRow row = _paddockSheet.GetRow(y);
                    if (row.GetCell(1) == null)
                    {
                        //break;
                        throw new Exception("Table already exists");
                    }

                    _command.CommandText = "INSERT INTO Hives(Branch_ID, Date_Sent, Location, Hive_Body, Honey_Super, Frames, Hive_Species, Forage_Enviornment) VALUES(@BranchId, @DateSent, @HiveLoc, @HiveBody, @HoneySup, @NumFrames, @Species, @ForangeEnv);";
                    _command.Parameters.AddWithValue("@BranchId", Util.Farmid);
                    _command.Parameters.AddWithValue("@DateSent", Util.Date);
                    _command.Parameters.AddWithValue("@HiveLoc", row.GetCell((int)HiveCol.LocCol));
                    _command.Parameters.AddWithValue("@HiveBody", row.GetCell((int)HiveCol.HiveBodyCol));
                    _command.Parameters.AddWithValue("@HoneySup", row.GetCell((int)HiveCol.HoneySupCol));
                    _command.Parameters.AddWithValue("@NumFrames", row.GetCell((int)HiveCol.FramesCol));
                    _command.Parameters.AddWithValue("@Species", row.GetCell((int)HiveCol.HiveSpeciesCol));
                    _command.Parameters.AddWithValue("@ForangeEnv", row.GetCell((int)HiveCol.ForageCol));
                    _command.ExecuteNonQuery();
                }
            }
        }

        private bool CheckForExistingData()
        {
            string sql = $"SELECT Hive_ID FROM Hives WHERE Date_Sent = '{Util.Date}' AND  Branch_ID = '{Util.Farmid}'";
            SQLiteCommand command = new SQLiteCommand(sql, _dbCon);
            if (command.ExecuteScalar() != null)
            {
                return true;
            }
            return false;
        }
    }
}
