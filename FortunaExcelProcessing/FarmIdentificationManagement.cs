using System;
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
            using (dBConnection = new SQLiteConnection($"Data Source={FilePaths.DBFilePath};Version=3;"))
            {
                dBConnection.Open();

                if (!CheckForExistingFarm(farmName))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.CommandText = "INSERT INTO farms(farmid, name, area) values(@farmid,@farmname,@farmarea)";
                        cmd.Parameters.AddWithValue("@farmid", farmid);
                        cmd.Parameters.AddWithValue("@farmname", farmName.Trim());
                        cmd.Parameters.AddWithValue("@farmarea", area);
                        cmd.Connection = dBConnection;
                        cmd.ExecuteNonQuery();
                    }
                }
                dBConnection.Close();
            }
        }

        public void CreateFarmTable()
        {
            dBConnection = new SQLiteConnection($"Data Source={FilePaths.DBFilePath};Version=3;");
            dBConnection.Open();
            if (!Util.CheckForTable("farms"))
            {
                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE farms (fid INTEGER PRIMARY KEY, farmid INTEGER, name VARCHAR(50), area REAL);", dBConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            dBConnection.Close();
        }


        private bool CheckForExistingFarm(string data)
        {
            string sql = $"SELECT farmid FROM farms where name = '{data}'";
            using (SQLiteCommand command = new SQLiteCommand(sql, dBConnection))
            {
                if (command.ExecuteScalar() != null)
                    return true;
            }
            return false;
        }
    }
}
