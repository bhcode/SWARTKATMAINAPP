﻿using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using System.Globalization;
using System.Text;
using System.IO;

namespace FortunaExcelProcessing.WeeklyProcessing
{
    class EditWeeklyDataTable : ITableEditor
    {
        ISheet _sheet;
        // <summary>
        //
        // </summary>
        private int[] dataRows = { 6, 7, 8, 10, 11, 12, 13, 14, 16, 18, 19, 21, 22, 24, 28, 32, 33, 34, 35, 37, 38, 39, 40, 41, 42, 43, 44, 45, 50, 51, 52, 53, 54, 55, 56, 57, 101, 102, 103, 104, 105, 106, 109, 110, 111, 112, 113, 114 };
        // <summary>
        // 
        // </summary>
        private string[] dataLabels = { "Honey (kg)", "Honey to Date (kg)" , "Avg Honey Per Hive (kg)", "Royal Honey (kg)", "Avg Royal Honey Per Hive (kg)",
                                        "Royal Honey to Date (kg)","Beeswax (kg)","Feeding","Honey Store","Pollen Store","Honey Feed","Pollen Feed","Ener-H-Plus","HFCS-55",
                                        "Vita Feed Gold","Pollen Patty","Living Conditions","Hive Condition","Temper","Odor","Population","Laying Pattern",
                                        "Area Information","Total Area (m^2)","Total Frames","Total Frames Unused","Death Information","Deaths","Deaths to Date",
                                        "Disease Information","Diseased Hives","Hives Treated","Replacement Hives","Bees Bought (kg)","Conditions","Avg Temperature","New Queens"};
        string sql; SQLiteCommand command; SQLiteConnection dbConn;

        // <summary>
        // 
        // </summary>
        // <param name="sheet"></param>
        public void EditTable(ISheet sheet)
        {
            dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));
            dbConn.Open();
            _sheet = sheet;
            if (!CheckForTable())
            {
                sql = "CREATE TABLE WeeklyData(id INTEGER PRIMARY KEY AUTOINCREMENT, farmid INTEGER, date VARCHAR(30), data VARCHAR(1104));";
                command = new SQLiteCommand(sql, dbConn);
                command.ExecuteNonQuery();
            }
            WeeklyDataTable(dbConn);
            dbConn.Close();
        }

        // <summary>
        // 
        // </summary>
        // <param name="dbConn"></param>
        private void WeeklyDataTable(SQLiteConnection dbConn)
        {
            int FarmId = Util.GetFarmID(CheckCellData.CellTypeString(_sheet.GetRow(2).GetCell(1)));

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

        // <summary>
        // 
        // </summary>
        // <param name="farmName"></param>
        // <returns></returns>
        private int GetFarmID(string farmName)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT farmid FROM farms where name = '@fn'";
            cmd.Parameters.AddWithValue("@fn", farmName);
            cmd.Connection = dbConn;

            string response = cmd.ExecuteScalar().ToString();
            int converted = int.Parse(response);
            return converted;

            //sql = $"SELECT farmid FROM farms where name = '{fn}';";
            //command = new SQLiteCommand(sql, dbConn);
        }

        // <summary>
        // 
        // </summary>
        // <param name="date"></param>
        // <param name="farmID"></param>
        // <returns></returns>
        private bool checkForExistingColumn(string date, int farmID)
        {
            sql = $"SELECT date FROM Datas where date = '{date}' AND farmid = '{farmID}'";
            command = new SQLiteCommand(sql, dbConn);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }

        // <summary>
        // 
        // </summary>
        // <returns></returns>
        private bool CheckForTable()
        {
            sql = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'Datas'";
            command = new SQLiteCommand(sql, dbConn);
            if (command.ExecuteScalar() != null)
                return true;

            return false;
        }

        // <summary>
        // 
        // </summary>
        // <param name="command"></param>
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

