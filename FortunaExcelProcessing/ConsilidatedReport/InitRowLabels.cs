using System.Collections.Generic;
using System.Data.SQLite;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    class InitRowLabels
    {
        public static List<string> labelList()
        {
            List<string> labels = new List<string>();
            string[] dataLabels = {  "Honey (kg)", "Honey to Date (kg)" , "Avg Honey Per Hive (kg)", "Royal Honey (kg)", "Avg Royal Honey Per Hive (kg)",
                                        "Royal Honey to Date (kg)","Beeswax (kg)","Feeding","Honey Store","Pollen Store","Honey Feed","Pollen Feed","Ener-H-Plus","HFCS-55",
                                        "Vita Feed Gold","Pollen Patty","Living Conditions","Hive Condition","Temper","Odor","Population","Laying Pattern",
                                        "Area Information","Total Area (m^2)","Total Frames","Total Frames Unused","Death Information","Deaths","Deaths to Date",
                                        "Disease Information","Diseased Hives","Hives Treated","Replacement Hives","Bees Bought (kg)","Conditions","Avg Temperature","New Queens" };

            foreach (string item in dataLabels)
            {
                labels.Add(item);
            }

            return labels;
        }

        public static int[] indentation()
        {
            int[] indent = {  };
            return indent;
        }
    }
}
