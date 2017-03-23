using System;
using System.Data.SQLite;

namespace FortunaExcelProcessing
{
    class FarmIdentificationManagement
    {
        SQLiteConnection dBConnection;


        /*---------------------------------------------------------------------------
         |This needs to be initialized first before other DB/Operations are performed|
         --------------------------------------------------------------------------- */

        public void EditTable(string farmName, double area)
        {
            dBConnection = new SQLiteConnection($"Data Source={FilePaths.DBFilePath};Version=3;");
            dBConnection.Open();
    
            if (!CheckForExistingFarm(farmName))
            {
                string date = DateTime.Now.ToString(Util.DForm());
                SQLiteCommand command = new SQLiteCommand($"INSERT INTO farms(name, area) values('{farmName}', {area})", dBConnection);
                command.ExecuteNonQuery();
            }
            dBConnection.Close();
        }

        public void CreateFarmTable()
        {
            dBConnection = new SQLiteConnection($"Data Source={FilePaths.DBFilePath};Version=3;");
            dBConnection.Open();
            if (!Util.CheckForTable("farms"))
            {
                SQLiteCommand command = new SQLiteCommand("CREATE TABLE farms (farmid INTEGER PRIMARY KEY, name VARCHAR(50), area REAL);", dBConnection);
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
