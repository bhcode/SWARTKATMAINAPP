using System;
using System.Text;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using FortunaExcelProcessing.Properties;

namespace FortunaExcelProcessing.WeeklyProcessing
{
    public class EditObservationsTable : ITableEditor
    {
        ISheet _sheet;
        string _sql;
        SQLiteCommand _command;
        SQLiteConnection _dBConnection;

        //public string[] _category = { "'Animal Health'", "'Fertiliser Application'", "'Jobs Last Week'", "'Jobs This Week'", "'Stock'", "'General'", "'Resource Management Issues'" };

        public void EditTable(ISheet sheet)
        {
            _dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", settings.Default.DbFilePath));
            _dBConnection.Open();
            _sheet = sheet;

            //Create the table if its not in the Database
            if (!Util.CheckForTable("Observations"))
            {
                _sql = "CREATE TABLE Observations(Observation_ID INTEGER PRIMARY KEY AUTOINCREMENT, Branch_ID INTEGER, Date_Sent VARCHAR(30), Category VARCHAR(512), Description VARCHAR(512), Weather VARCHAR(512));";
                _command = new SQLiteCommand(_sql, _dBConnection);
                _command.ExecuteNonQuery();
            }
            else
                throw new Exception("Table already exists");

            CommentsTable(_dBConnection);
            _dBConnection.Close();
        }

        private string GetComment(ICell cell)
        {
            if (cell == null)
                return "Null";

            if (cell.ToString().Length > 0)
            {
                if (cell.CellType == CellType.Numeric)
                    return cell.NumericCellValue.ToString();

                if (cell.CellType == CellType.String)
                    return cell.StringCellValue;

                return "Comment Error! c:" + cell.ColumnIndex + ", r:" + cell.RowIndex;
            }
            else
                return "Empty Comment";
        }

        private void CommentsTable(SQLiteConnection dBConnection)
        {

            int BranchID = Util.GetFarmID(CheckCellData.CellTypeString(_sheet.GetRow(2).GetCell(1)));
            Console.WriteLine(BranchID);

            //Go through each column, to the last column with a date available
            for (int c = 1; c < _sheet.GetRow(1).LastCellNum; c++)
            {
                string date = CheckCellData.CellTypeDate(_sheet.GetRow(1).GetCell(c)).ToString("yyyy-MM-dd");
                Util.Date = date;

                //check for empty column, if 'emptycount' == 0 then column is empty
                int emptycount = 0;
                for (int r = 2; r < 13; r++)
                {
                    ICell checkCell = _sheet.GetRow(r).GetCell(c);
                    if (CheckCellData.CellTypeString(checkCell).Trim() == "" || checkCell == null)
                    {
                        emptycount++;
                    }
                }

                if (!checkForExistingColumn(date, BranchID) && emptycount < 10)
                {
                    for (int r = 2; r < 13; r += 4)
                    {
                        if (r != 5 || r != 9)
                        {
                            _command.CommandText = "INSERT INTO Observations(Branch_ID, Date_Sent, Category, Description, Weather) VALUES (@Branch_ID, @Date_Sent, @Category, @Description, @Weather)";
                            _command.Parameters.AddWithValue("@Branch_ID", BranchID);
                            _command.Parameters.AddWithValue("@Date_Sent", date);
                            _command.Parameters.AddWithValue("@Category", GetComment(_sheet.GetRow(r).GetCell(1)));
                            _command.Parameters.AddWithValue("@Description", GetComment(_sheet.GetRow(r + 1).GetCell(1)));
                            _command.Parameters.AddWithValue("@Weather", GetComment(_sheet.GetRow(r + 2).GetCell(1)));
                            _command.ExecuteNonQuery();

                        }
                    }
                }
            }
        }

        private bool checkForExistingColumn(string date, int farmID)
        {
            _sql = $"SELECT Date_Sent FROM Observations where Date_Sent = '{date}' AND Branch_ID = '{farmID}'";
            _command = new SQLiteCommand(_sql, _dBConnection);
            if (_command.ExecuteScalar() != null)
            {
                return true;
            }
            return false;
        }
    }
}