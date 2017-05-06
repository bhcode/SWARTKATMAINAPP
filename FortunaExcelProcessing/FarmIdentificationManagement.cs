﻿using System;
using System.Data.SQLite;

namespace FortunaExcelProcessing
{
    public class FarmIdentificationManagement
    {
        SQLiteConnection dBConnection;


        /*---------------------------------------------------------------------------
         |This needs to be initialized first before other DB/Operations are performed|
         --------------------------------------------------------------------------- */

        public void EditTable(int farmid, string farmName, double area)
        {
            Util.Date = DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToString("yyyy-MM-dd");
            dBConnection = new SQLiteConnection($"Data Source={FilePaths.DBFilePath};Version=3;");
            dBConnection.Open();
    
            if (!CheckForExistingFarm(farmName))
            {
                //string date = DateTime.Now.ToString(Util.DForm());
                //SQLiteCommand command = new SQLiteCommand($"INSERT INTO farms(farmid, name, area) values('{farmName}', {area})", dBConnection);

                SQLiteCommand cmd = new SQLiteCommand();
                cmd.CommandText = "INSERT INTO farms(farmid, name, area) values(@farmid,@farmname,@farmarea)";
                cmd.Parameters.AddWithValue("@farmid", farmid);
                cmd.Parameters.AddWithValue("@farmname", area);
                cmd.Parameters.AddWithValue("@farmarea", farmName);
                cmd.Connection = dBConnection;
                cmd.ExecuteNonQuery();
            }
            dBConnection.Close();
        }

        public void CreateFarmTable()
        {
            dBConnection = new SQLiteConnection($"Data Source={FilePaths.DBFilePath};Version=3;");
            dBConnection.Open();
            if (!Util.CheckForTable("farms"))
            {
                SQLiteCommand command = new SQLiteCommand("CREATE TABLE farms (farmid INTEGER PRIMARY KEY, fid INTEGER, name VARCHAR(50), area REAL);", dBConnection);
                command.ExecuteNonQuery();
            }
            dBConnection.Close();
        }



        private bool CheckForExistingFarm(string data)
        {
            string sql = $"SELECT farmid FROM farms where name = '{data}'";
            SQLiteCommand command = new SQLiteCommand(sql, dBConnection);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }
    }
}
