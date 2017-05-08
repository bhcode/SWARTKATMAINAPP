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

        // <summary>
        // 
        // </summary>
        // <param name="sheet"></param>
        public void EditTable(ISheet sheet)
        {
            paddockSheet = sheet;

            _dbCon = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));

            _dbCon.Open();

            if (!Util.CheckForTable("paddocks"))
                DBOperations.ExecuteDatabaseQuery("CREATE TABLE paddocks (farmid int(11), sdate VARCHAR(30), paddockid varchar(20), paddock float, crop varchar(20))", _dbCon);

            ProcessData(_dbCon);

            _dbCon.Close();
        }

        // <summary>
        // 
        // </summary>
        // <param name="dbCon"></param>
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

                    DBOperations.ExecuteDatabaseQuery("INSERT INTO Paddocks values(" + data, dbCon);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckForExistingData()
        {
            string sql = $"SELECT paddockID FROM Paddocks WHERE sdate = '{Util.Date}' AND  farmid = '{Util.Farmid}'";
            SQLiteCommand command = new SQLiteCommand(sql, _dbCon);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }
    }
}
