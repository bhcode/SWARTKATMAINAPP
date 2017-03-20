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
        static SQLiteConnection dBConnection;
        static string[] rowLabels = { "Farm:", "Week Ending:", "Total  Area(ha)", "Area available to milkers","Crop area", "Crop area Available","Area Grazed(avg for last 2 pickups)","Grazing interval","Pre Grazing Cover","Post Grazing Cover(Ave for week)","Average Cover(kgDM/ha)","Growth Rate(kgDM/ha/day)",
             "Predicted Growth Rate(kgDM/ha/day)","KgDM consumption/cow(Pasture)","Area shut-up for supplements","Total cows wintered","Milked into Vat","NOT milked into Vat", "% not in vat", "Total milking cows", "% cows calved", "Dry cows(On farm)",
             "Dry cows(Off farm)","Total cows at beginning of week","Kg Liveweight/cow","Kg Liveweight/Ha","Stocking Rate(milkersonly)","Production", "Average MS/day (last 2 pickups)","December DailyTarget","% to target",
             "KgMS/Cows in vat","Weekly % change","KgMS/total cows milked", "KgMS/Ha", "KgMS month to date", "Avg SCC (000) for last 2pickups","Protein Fat Ratio","Calf Milk (litres)",
             "Supplements Fed (kgDM/cow/day)","Grain (kgDM)", "Palm kernel (kgDM)", "Silage(kgDM)","Balage (kgDM)","Molasses (kgDM)","Straw(kgDM)","Hay (kgDM)","Other (kgDM)", "Total Consumption(kgDM/cow/day)","Pasture Requirements(Milkers Only)","Demand/ha/day","Predicted Surplus/Deficit(kgDM/ha)","Predicted average cover",
             "Area N applied(ha)","Rate per hectare(kgN/ha)","Total N applied(kgN/ha)","Total N applied Year To Date(kgN/ha)","Deaths","Deaths to date", "% deaths", "Cows Sold","Cows Sold to date","Total Cows at end of week","Balance Check" };

        static public void MakeLabels(string filePath)
        {
            dBConnection = new SQLiteConnection($"Data Source={filePath};Version=3;");
            dBConnection.Open();
            if (!utils.CheckForTable("Labels"))
            {
                string sql = "CREATE TABLE Labels(id INTEGER PRIMARY KEY AUTOINCREMENT,  label VARCHAR(100));";
                SQLiteCommand command = new SQLiteCommand(sql, dBConnection);
                command.ExecuteNonQuery();

                foreach (string rl in rowLabels)
                {
                    sql = $"INSERT INTO Labels(label) VALUES('{rl}');";
                    command = new SQLiteCommand(sql, dBConnection);
                    command.ExecuteNonQuery();
                }
            }
            dBConnection.Close();
        }
    }
}
