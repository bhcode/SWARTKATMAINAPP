using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using System.Globalization;
using System.Text;

namespace FortunaExcelProcessing.WeeklyProcessing
{
    class EditWeeklyDataTable : ITableEditor
    {

        ISheet _sheet;
        private int[] dataRows = { 6, 7, 8, 10, 11,12, 13, 14, 16, 18, 19, 21, 22, 24, 28, 32, 33, 34, 35, 37, 38, 39, 40, 41, 42, 43, 44, 45, 50, 51, 54, 56, 101, 102, 103, 104, 105, 106, 109, 110, 111, 112, 113, 114};
        private string[] dataLabels = { "Crop area", "Crop area Available", "Area Grazed (avg for last 2 pickups)", "Pre Grazing Cover", "Average Cover (kgDM/ha)", "Growth Rate (kgDM/ha/day)", "Predicted Growth Rate (kgDM/ha/day)", "Area shut-up for supplements", "Milked into Vat",
                                            "NOT milked into Vat", "Dry cows (On farm)", "Dry cows (Off farm)", "Kg Liveweight/cow", "Average MS/day (last 2 pickups)", "KgMS month to date", "Avg SCC (000) for last 2 pickups", "Protein Fat", "Ratio Calf Milk (litres)",
                                        "Grain (kgDM)", "Palm kernel (kgDM)", "Silage(kgDM)", "Balage (kgDM)", "Molasses (kgDM)", "Straw (kgDM)", "Hay (kgDM)", "Other (kgDM)", "Total Consumption (kgDM/cow/day)", "Area N applied (ha)", "Rate per hectare (kgN/ha)", "Deaths",
                                            "Cows Sold", "Two Year Dairy Heifers", "One Year Dairy Heifers", "AB Dairy Heifer Calves", "All Other Calves", "Bulls", "Other", "Two Year Dairy Heifers", "One Year Dairy Heifers", "AB Dairy Heifer Calves", "All Other Calves", "Bulls", "Other"};
        string sql; SQLiteCommand command; SQLiteConnection dbConn;


        public void EditTable(ISheet sheet)
        {
            dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));
            dbConn.Open();
            

                _sheet = sheet;
                if (!CheckForTable())
                {
                    sql = "CREATE TABLE Datas(id INTEGER PRIMARY KEY AUTOINCREMENT, farmid INTEGER, date VARCHAR(30), data VARCHAR(1024));";
                    command = new SQLiteCommand(sql, dbConn);
                    command.ExecuteNonQuery();
                }
                WeeklyDataTable(dbConn);
            dbConn.Close();
        }

        private void WeeklyDataTable(SQLiteConnection dbConn)
        {
            int FarmId = GetFarmID(CheckCellData.CellTypeString(_sheet.GetRow(2).GetCell(1)));

            for (int c = 3; c < _sheet.GetRow(6).LastCellNum; c++)
            {
                if (CheckCellData.CellTypeNumeric(_sheet.GetRow(7).GetCell(c)) != -1)
                {
                    //Console.WriteLine("I'm working!");
                    string date = CheckCellData.CellWeirdDate(_sheet.GetRow(3).GetCell(c)).ToString("yyyy-MM-dd");

                    if (!checkForExistingColumn(date, FarmId))
                    {
                        string output = "[";
                        for (int row = 0; row < dataRows.Length - 1; row++)
                        {
                            if (row != dataRows.Length)
                                output = output + CheckCellData.CellTypeNumeric(_sheet.GetRow(dataRows[row]).GetCell(c)) + ",";
                        }

                        output = output + CheckCellData.CellTypeNumeric(_sheet.GetRow(dataRows.Length - 1).GetCell(c)) + "]";
                        command.CommandText = $"INSERT INTO Datas(farmid, date, data) VALUES({FarmId}, @date,'{output}');";
                        command.Parameters.AddWithValue("@date", date);
                        command.ExecuteNonQuery();
                        output = "";
                    }
                }
            }
            }

        private int GetFarmID(string farmName)
        {
            sql = $"SELECT farmid FROM farms where name = '{farmName}';";
            command = new SQLiteCommand(sql, dbConn);
            return int.Parse(command.ExecuteScalar().ToString());
        }

        private bool checkForExistingColumn(string date, int farmID)
        {
            sql = $"SELECT date FROM Datas where date = '{date}' AND farmid = '{farmID}'";
            command = new SQLiteCommand(sql, dbConn);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }

        private bool CheckForTable()
        {
            sql = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'Datas'";
            command = new SQLiteCommand(sql, dbConn);
            if (command.ExecuteScalar() != null)
                return true;

            return false;
        }

        private void ExecuteDatabaseQuery(string command)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = command;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = dbConn;
            cmd.ExecuteNonQuery();
        }
    }
}

