using System;
using System.Text;
using NPOI.SS.UserModel;
using System.Data.SQLite;

namespace FortunaExcelProcessing.WeeklyProcessing
{
    public class EditObservationsTable : ITableEditor
    {
        ISheet _sheet;
        string sql; SQLiteCommand command; SQLiteConnection dBConnection;

        public string[] category = { "'Animal Health'", "'Fertiliser Application'", "'Jobs Last Week'", "'Jobs This Week'", "'Stock'", "'General'", "'Resource Management Issues'" };

        public void EditTable(ISheet sheet)
        {
            dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));
            dBConnection.Open();
            _sheet = sheet;

            //Create the table if its not in the Database
            if (!Util.CheckForTable("Observations"))
            {
                sql = "CREATE TABLE Observations(Observation_ID INTEGER PRIMARY KEY AUTOINCREMENT, Branch_ID INTEGER, Date_Sent VARCHAR(30), Category VARCHAR(512), Description VARCHAR(512), Weather VARCHAR(512));";
                command = new SQLiteCommand(sql, dBConnection);
                command.ExecuteNonQuery();
            }

            CommentsTable(dBConnection);
            dBConnection.Close();
        }

        private string GetComment(ISheet sheet, int r, int c)
        {
            IRow row = sheet.GetRow(r);
            if (row == null)
            {
                return "";
            }
            ICell cell = row.GetCell(c);
            if (cell == null)
            {
                return "";
            }
            if (cell.CellType == CellType.Numeric)
            {
                return cell.NumericCellValue.ToString();
            }
            if (cell.CellType == CellType.String) return cell.StringCellValue;
            {
                return cell.ToString();
            }
        }

        private void CommentsTable(SQLiteConnection dBConnection)
        {

            int BranchID = Util.GetFarmID(CheckCellData.CellTypeString(_sheet.GetRow(2).GetCell(1)));
            Console.WriteLine(BranchID);

            //Go through each column, to the last column with a date available
            for (int c = 2; c < _sheet.GetRow(3).LastCellNum; c++)
            {
                string date = CheckCellData.CellWeirdDate(_sheet.GetRow(3).GetCell(c)).ToString("yyyy-MM-dd");
                Util.Date = date;

                //check for empty column, if 'emptycount' == 0 then column is empty
                int emptycount = 0;
                for (int r = 4; r < 11; r++)
                {
                    ICell checkCell = _sheet.GetRow(r).GetCell(c);
                    if (CheckCellData.CellTypeString(checkCell).Trim() == "" || checkCell == null)
                    {
                        emptycount++;
                    }
                }

                if (!checkForExistingColumn(date, BranchID) && emptycount < 7)
                {
                    for (int r = 4; r < 11; r++)
                    {
                        string cat = category[r - 4];

                        string cellData;

                        if (CheckCellData.CellTypeNumeric(_sheet.GetRow(r).GetCell(c)) != -1)
                        {
                            cellData = CheckCellData.CellTypeNumeric(_sheet.GetRow(r).GetCell(c)).ToString().Trim();
                        }
                        else
                        {
                            cellData = CheckCellData.CellTypeString(_sheet.GetRow(r).GetCell(c)).Trim();
                        }

                        if (cellData != null && cellData != "")
                        {
                            command.CommandText = "INSERT INTO Observations(Branch_ID, Date_Sent, Category, Description, Weather) VALUES (@Branch_ID, @Date_Sent, @Category, @Description, @Weather)";
                            command.Parameters.AddWithValue("@Branch_ID", BranchID);
                            command.Parameters.AddWithValue("@Date_Sent", date);
                            command.Parameters.AddWithValue("@Category", cat);
                            command.Parameters.AddWithValue("@Description", cellData);
                            command.Parameters.AddWithValue("@Weather", cellData);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private bool checkForExistingColumn(string date, int farmID)
        {
            sql = $"SELECT Date_Sent FROM Observations where Date_Sent = '{date}' AND Branch_ID = '{farmID}'";
            command = new SQLiteCommand(sql, dBConnection);
            if (command.ExecuteScalar() != null)
            {
                return true;
            }
            return false;
        }
    }
}