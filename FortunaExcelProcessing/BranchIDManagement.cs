using System;
using System.Data.SQLite;
using FortunaExcelProcessing.Properties;

namespace FortunaExcelProcessing
{
    public class BranchIDManagement
    {
        /*---------------------------------------------------------------------------
        |This needs to be initialized first before other DB/Operations are performed|
        --------------------------------------------------------------------------- */

        SQLiteConnection _dBConnection;

        public void EditTable(int farmid, string farmName, double area)
        {
            Util.Date = DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToString("yyyy-MM-dd");
            using (_dBConnection = new SQLiteConnection($"Data Source={settings.Default.DbFilePath};Version=3;"))
            {
                _dBConnection.Open();

                if (!CheckForExistingFarm(farmName))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.CommandText = "INSERT INTO  Branch (Branch_ID, Branch_Name) VALUES (@farmid,@farmname)";
                        cmd.Parameters.AddWithValue("@farmid", farmid);
                        cmd.Parameters.AddWithValue("@farmname", farmName.Trim());
                        cmd.Parameters.AddWithValue("@farmarea", area);
                        cmd.Connection = _dBConnection;
                        cmd.ExecuteNonQuery();
                    }
                }
                _dBConnection.Close();
            }
        }

        public void CreateFarmTable()
        {
            _dBConnection = new SQLiteConnection($"Data Source={settings.Default.DbFilePath};Version=3;");
            _dBConnection.Open();
            if (!Util.CheckForTable("Branch"))
            {
                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE Branch (BID INTEGER PRIMARY KEY, Branch_ID INTEGER, Branch_Name VARCHAR(50));", _dBConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            _dBConnection.Close();
        }


        private bool CheckForExistingFarm(string data)
        {
            string sql = $"SELECT Branch_ID FROM Branch WHERE name = '{data}'";
            using (SQLiteCommand command = new SQLiteCommand(sql, _dBConnection))
            {
                if (command.ExecuteScalar() != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
