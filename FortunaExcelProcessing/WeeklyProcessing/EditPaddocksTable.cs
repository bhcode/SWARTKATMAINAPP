using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.Data.SQLite;

namespace FortunaExcelProcessing.WeeklyProcessing
{
    class EditPaddocksTable : ITableEditor
    {
        ISheet paddockSheet;
        SQLiteConnection _dbCon;

        public void EditTable(ISheet sheet)
        {
            paddockSheet = sheet;

            _dbCon = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));

            _dbCon.Open();

            if (!util.CheckForTable("Paddocks"))
                dbOperations.ExecuteDatabaseQuery("CREATE TABLE Paddocks (farmid int(11), date VARCHAR(30), paddockID varchar(20), paddockSize float, crop varchar(20))", _dbCon);

            ProcessData(_dbCon);

            _dbCon.Close();
        }

        private void ProcessData(SQLiteConnection dbCon)
        {
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

                    string data = string.Format("{0},'{1}','{2}',{3},'{4}')", util.Farmid, util.Date, row.GetCell((int)PaddockColumns.PaddockIDCol), row.GetCell((int)PaddockColumns.PaddockSizeCol), tmp);

                    dbOperations.ExecuteDatabaseQuery("INSERT INTO Paddocks values(" + data, dbCon);
                }
            }
        }

        private bool CheckForExistingData()
        {
            string sql = $"SELECT paddockID FROM Paddocks WHERE date = '{util.Date}' AND  farmid = '{util.Farmid}'";
            SQLiteCommand command = new SQLiteCommand(sql, _dbCon);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }
    }
}
