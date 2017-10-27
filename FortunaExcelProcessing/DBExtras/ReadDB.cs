using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortunaExcelProcessing.Properties;

namespace FortunaExcelProcessing.DBExtras
{
    public static class ReadDB
    {
        static string CSTRING = $"Data Source={settings.Default.DbFilePath};Version=3;";

        public static List<WeeklyData> LoadWeeklyData()
        {
            List<WeeklyData> list = new List<WeeklyData>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select Branch_ID, Date_Sent, Data_Array from Weekly_Data";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int branchId = reader.GetInt32(0);
                    DateTime date = reader.GetDateTime(1);
                    string data = reader.GetString(2);
                    list.Add(new WeeklyData(branchId, date, data));
                }
                conn.Close();
            }
            return list;
        }

        public static List<Observation> LoadObservationData()
        {
            List<Observation> list = new List<Observation>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select Branch_ID, Date_Sent, Category, Description, Weather from Observations";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int branchId = reader.GetInt32(0);
                    DateTime date = reader.GetDateTime(1);
                    string category = reader.GetString(2);
                    string description = reader.GetString(3);
                    string weather = reader.GetString(4);
                    list.Add(new Observation(branchId, date, category, description, weather));
                }
                conn.Close();
            }
            return list;
        }

        public static List<Hive> LoadHiveData()
        {
            List<Hive> list = new List<Hive>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select Branch_ID, Date_Sent, Location, Hive_Body, Honey_Super, Frames, Hive_Species, Forage_Enviornment from Hives";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int branchId = reader.GetInt32(0);
                    DateTime date = reader.GetDateTime(1);
                    string location = reader.GetString(2);
                    string hiveBody = reader.GetString(3);
                    string honeySuper = reader.GetString(4);
                    int frames = reader.GetInt32(5);
                    string species = reader.GetString(6);
                    string forageEnv = "n/a";
                    try { forageEnv = reader.GetString(7); } catch { }
                    list.Add(new Hive(branchId, date, location, hiveBody, honeySuper, frames, species, forageEnv));
                }
                conn.Close();
            }
            return list;
        }

        public static List<Label> LoadLabelData()
        {
            List<Label> list = new List<Label>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select id,label from Labels";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int row = reader.GetInt32(0);
                    string label = reader.GetString(1);
                    list.Add(new Label(row, label));
                }
                conn.Close();
            }
            return list;
        }
    }
}
