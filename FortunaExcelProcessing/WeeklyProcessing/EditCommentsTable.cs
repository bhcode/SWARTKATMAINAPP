using System;
using System.Text;
using NPOI.SS.UserModel;
using System.Data.SQLite;


namespace FortunaExcelProcessing.WeeklyProcessing
{
    public class EditCommentsTable : ITableEditor
    {
        ISheet _sheet;
        string sql; SQLiteCommand command; SQLiteConnection dBConnection;
        public string[] category = {"'Animal Health'", "'Fertiliser Application'", "'Jobs Last Week'", "'Jobs This Week'", "'Stock'", "'General'", "'Resource Management Issues'"};


        public void EditTable(ISheet sheet)
        {
            dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));
            dBConnection.Open();
            _sheet = sheet;

            //Create the table if its not in the Database
            if (!util.CheckForTable("Comments"))
            {
                sql = "CREATE TABLE Comments(id INTEGER PRIMARY KEY AUTOINCREMENT, farmid INTEGER, date VARCHAR(30), category VARCHAR(30), description BLOB);";
                command = new SQLiteCommand(sql, dBConnection);
                command.ExecuteNonQuery();
            }

            CommentsTable(dBConnection);

            dBConnection.Close();
        }

        private void CommentsTable(SQLiteConnection dBConnection)
        {         
            bool complete = false;
            int FarmId = util.GetFarmID(CheckCellData.CellTypeString(_sheet.GetRow(2).GetCell(1)));
            Console.WriteLine(FarmId);
            for (int c = 2; c < _sheet.GetRow(3).LastCellNum; c++)
            {
                string date = CheckCellData.CellWeirdDate(_sheet.GetRow(3).GetCell(c)).ToString("yyyy-MM-dd");
                util.Date = date;

                //check for column
                if (!checkForExistingColumn(date, FarmId))
                {
                    for (int r = 4; r < 11; r++)
                    {
                        if (CheckCellData.CellTypeString(_sheet.GetRow(r).GetCell(c)) == "" && CheckCellData.CellTypeString(_sheet.GetRow(r + 1).GetCell(c)) == "")
                        {
                            complete = true;
                            break;
                        }
                        string cat = category[r - 4];
                        string cellData = "'" + CheckCellData.CellTypeString(_sheet.GetRow(r).GetCell(c)) + "'";
                        command.CommandText = $"INSERT INTO Comments(farmid,date,category,description) VALUES ({FarmId},@date,{cat},{cellData})";
                        command.Parameters.AddWithValue("@date",date);
                        command.ExecuteNonQuery();
                    }

                    if (complete == true)
                        break;
                }          
            }
        }

        private bool checkForExistingColumn(string date, int farmID)
        {
            sql = $"SELECT date FROM Comments where date = '{date}' AND farmid = '{farmID}'";
            command = new SQLiteCommand(sql, dBConnection);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }
    }
}