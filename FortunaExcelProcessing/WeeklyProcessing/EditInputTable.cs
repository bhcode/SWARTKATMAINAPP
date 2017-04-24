using System.Text;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace FortunaExcelProcessing.WeeklyProcessing
{
    public class EditInputTable : ITableEditor
    {
        ISheet _sheet;
        string sql;
        SQLiteCommand command; SQLiteConnection dBConnection;

        public void EditTable(ISheet sheet)
        {
            _sheet = sheet;
            dBConnection = new SQLiteConnection($"Data Source={FilePaths.DBFilePath};Version=3;");
            dBConnection.Open();

            SupplementsTable();

            dBConnection.Close();
        }

        private void SupplementsTable()
        {
            StringBuilder supplements = new StringBuilder();
            StringBuilder cows = new StringBuilder();

            if (!Util.CheckForTable("farmSupplements"))
            {
                command = new SQLiteCommand("CREATE TABLE farmSupplements (id  INTEGER PRIMARY KEY AUTOINCREMENT, sdate VARCHAR(30), farmid INTEGER, cows VARCHAR(512), supplements VARCHAR(512));", dBConnection);
                command.ExecuteNonQuery();
            }

            Util.Farmid = Util.GetFarmID(CheckCellData.CellTypeString(_sheet.GetRow(3).GetCell(4)));

            //default the date to the start of current week
            Util.Date = DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToString("yyyy-MM-dd");

            if (!CheckForExistingFarm("farmSupplements", "farmid", Util.Farmid.ToString()))
            {
                cows.Append("[" + CheckCellData.CellTypeNumeric(_sheet.GetRow(5).GetCell(4)) + ",");
                cows.Append(CheckCellData.CellTypeNumeric(_sheet.GetRow(6).GetCell(4)) + ",");
                cows.Append(CheckCellData.CellTypeNumeric(_sheet.GetRow(7).GetCell(4)) + "]");

                for (int r = 9; r < 18; r++)
                {
                    supplements.Append((r == 9) ? "[" : "" + CheckCellData.CellTypeNumeric(_sheet.GetRow(r).GetCell(4)) + ((r == 17) ? "]" : ","));
                }

                command = new SQLiteCommand(($"INSERT INTO farmSupplements(farmid, sdate, cows, supplements) values({Util.Farmid}, @date, '{cows}', '{supplements}')"), dBConnection);
                command.Parameters.AddWithValue("@date", Util.Date);
                command.ExecuteNonQuery();
            }
        }

        private bool CheckForExistingFarm(string tableName, string colName, string data)
        {
            sql = $"SELECT {colName} FROM {tableName} where {colName} = '{data}'";
            command = new SQLiteCommand(sql, dBConnection);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }


    }
}
