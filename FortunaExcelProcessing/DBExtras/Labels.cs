using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing.DBExtras
{
    static public class Labels
    {
        static SQLiteConnection _dBConnection;
        static string[] rowLabels = { "Honey (kg)", "Honey to Date (kg)" , "Avg Honey Per Hive (kg)", "Royal Honey (kg)", "Avg Royal Honey Per Hive (kg)",
                                        "Royal Honey to Date (kg)","Beeswax (kg)","Feeding","Honey Store","Pollen Store","Honey Feed","Pollen Feed","Ener-H-Plus","HFCS-55",
                                        "Vita Feed Gold","Pollen Patty","Living Conditions","Hive Condition","Temper","Odor","Population","Laying Pattern",
                                        "Area Information","Total Area (m^2)","Total Frames","Total Frames Unused","Death Information","Deaths","Deaths to Date",
                                        "Disease Information","Diseased Hives","Hives Treated","Replacement Hives","Bees Bought (kg)","Conditions","Avg Temperature","New Queens" };

        static public void MakeLabels(string filePath)
        {
            _dBConnection = new SQLiteConnection($"Data Source={filePath};Version=3;");
            _dBConnection.Open();
            if (!Util.CheckForTable("Labels"))
            {
                string sql = "CREATE TABLE Labels(id INTEGER PRIMARY KEY AUTOINCREMENT,  label VARCHAR(100));";
                SQLiteCommand command = new SQLiteCommand(sql, _dBConnection);
                command.ExecuteNonQuery();

                foreach (string rl in rowLabels)
                {
                    DBOperations.ExecuteDatabaseQuery($"INSERT INTO Labels(label) VALUES({rl});", _dBConnection);
                }
            }
            _dBConnection.Close();
        }
    }
}
