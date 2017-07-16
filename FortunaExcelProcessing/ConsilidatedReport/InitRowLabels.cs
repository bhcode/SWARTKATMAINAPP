using System.Collections.Generic;
using System.Data.SQLite;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    class InitRowLabels
    {
        public static List<string> labelList()
        {
            List<string> labels = new List<string>();
            string[] dataLabels = { "Farm:", "Week Ending:", "Total  Area(ha)", "Area available to milkers","Crop area", "Crop area Available","Area Grazed(avg for last 2 pickups)","Grazing interval","Pre Grazing Cover","Post Grazing Cover(Ave for week)","Average Cover(kgDM/ha)","Growth Rate(kgDM/ha/day)",
             "Predicted Growth Rate(kgDM/ha/day)","KgDM consumption/cow(Pasture)","Area shut-up for supplements","Total cows wintered","Milked into Vat","NOT milked into Vat", "% not in vat", "Total milking cows", "% cows calved", "Dry cows(On farm)",
             "Dry cows(Off farm)","Total cows at beginning of week","Kg Liveweight/cow","Kg Liveweight/Ha","Stocking Rate(milkersonly)","Production", "Average MS/day (last 2 pickups)","December DailyTarget","% to target",
             "KgMS/Cows in vat","Weekly % change","KgMS/total cows milked", "KgMS/Ha", "KgMS month to date", "Avg SCC (000) for last 2pickups","Protein Fat Ratio","Calf Milk (litres)",
             "Supplements Fed (kgDM/cow/day)","Grain (kgDM)", "Palm kernel (kgDM)", "Silage(kgDM)","Balage (kgDM)","Molasses (kgDM)","Straw(kgDM)","Hay (kgDM)","Other (kgDM)", "Total Consumption(kgDM/cow/day)","Pasture Requirements(Milkers Only)","Demand/ha/day","Predicted Surplus/Deficit(kgDM/ha)","Predicted average cover",
             "Area N applied(ha)","Rate per hectare(kgN/ha)","Total N applied(kgN/ha)","Total N applied Year To Date(kgN/ha)","Deaths","Deaths to date", "% deaths", "Cows Sold","Cows Sold to date","Total Cows at end of week","Balance Check" };

            foreach (string item in dataLabels)
            {
                labels.Add(item);

            }
            return labels;
        }

        public static int[] indentation()
        {
            int[] indent = { 18, 20, 28, 30, 31, 32, 33, 35, 36, 37, 38, 40, 41, 42, 43, 44, 45, 46, 47, 59 };
            return indent;
        }
    }
}
