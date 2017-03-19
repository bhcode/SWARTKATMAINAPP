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
        string name;
        string sql; SQLiteCommand command; SQLiteConnection dBConnection;

        public void EditTable(ISheet sheet)
        {
            _sheet = sheet;
            dBConnection = new SQLiteConnection($"Data Source={FilePaths.DBFilePath};Version=3;");
            dBConnection.Open();

            FarmInfoTable();

            SupplementsTable();

            dBConnection.Close();

        }

        private void FarmInfoTable()
        {
            if (!util.CheckForTable("farms"))
            {
                command = new SQLiteCommand("CREATE TABLE farms (farmid INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(50), area REAL);", dBConnection);
                command.ExecuteNonQuery();
            }
            
            name = CheckCellData.CellTypeString(_sheet.GetRow(3).GetCell(4));
            double area = CheckCellData.CellTypeNumeric(_sheet.GetRow(4).GetCell(4));

            if (!CheckForExistingFarm("farms","name", name))
            {
                command = new SQLiteCommand($"INSERT INTO farms(name, area) values('{name}', {area})", dBConnection);
                command.ExecuteNonQuery();
            }
        }

        private void SupplementsTable()
        {
            StringBuilder supplements = new StringBuilder();
            StringBuilder cows = new StringBuilder();

            //need to sort out date
            if (!util.CheckForTable("farmSupplements"))
            {
                command = new SQLiteCommand("CREATE TABLE farmSupplements (id  INTEGER PRIMARY KEY AUTOINCREMENT, farmid INTEGER, cows BLOB, supplements BLOB);", dBConnection);
                command.ExecuteNonQuery();
            }

            util.Farmid = util.GetFarmID(name);

            if (!CheckForExistingFarm("farmSupplements", "farmid", util.Farmid.ToString()))
            {
                cows.Append("[" + CheckCellData.CellTypeNumeric(_sheet.GetRow(5).GetCell(4)) + ",");
                cows.Append(CheckCellData.CellTypeNumeric(_sheet.GetRow(6).GetCell(4)) + ",");
                cows.Append(CheckCellData.CellTypeNumeric(_sheet.GetRow(7).GetCell(4)) + "]");

                for (int r = 10; r < 18; r++)
                {
                    supplements.Append((r == 10) ? "[" : "" + CheckCellData.CellTypeNumeric(_sheet.GetRow(r).GetCell(4)) + ((r == 17) ? "]" : ","));
                }

                command = new SQLiteCommand(($"INSERT INTO farmSupplements(farmid, cows, supplements) values({util.Farmid}, '{cows}', '{supplements}')"), dBConnection);
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
