using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FortunaExcelProcessing.DBExtras
{
    public static class Formulas
    {
        static SQLiteConnection dBConnection;
        static Dictionary<int, string> formulae = new Dictionary<int, string>() {
            {3,  "{0}3-({0}5+{0}15)" },
            {7,  "IF({0}7=0,0,{0}4/{0}7)" },
            {13, "IF({0}9=0,\"\",IF({0}20=0,\"\",({0}9-{0}10)*{0}7/19))" },
            {18, "{0}18/{0}17" },
            {19, "SUM({0}17:{0}18)" },
            {20, "{0}20/{0}16" },
            {23, "{0}20+{0}22+{0}23" },
            {25, "IF({0}3=0,\"\",{0}20/({0}3-{0}5)*{0}25)" } ,
            {26, "IF({0}20=0,\"\", {0}20/{0}4)" } ,
            {30, "{0}29/{0}30"},
            {31, "IF({0}17=0,\"\",IF({0}29=\"\",\"\",29/({0}17-{0}58-{0}61)))" },
            {34, "IF({0}27=\"\",\"\",IF({0}32=\"\",\"\",{0}17/({0}3-{0}5)*{0}32))"},
            {39, "SUM({0}41:{0}48)"},
            {49, "IF({0}49=\"\",\"\",{0}40-{0}49)" },
            {50, "IF({0}20=0,\"\",IF({0}47=0,\"\",({0}20*{0}47)/{0}4+({0}63*{0}22)/{0}4))" },
            {51, "IF({0}51=\"\",\"\",{0}13-{0}49)" },
            {52, "IF({0}52=\"\",\"\",{0}11+({0}52*7))"},
            {56, "IF({0}56=\"\",\"\",IF({0}55=\"\",{0}56,{0}56+{0}55))"},
            {58, "IF({0}58=\"\",\"\",IF({0}60=\"\",{0}58,{0}58+{0}60))" },
            {59, "{0}59/{0}15" },
            {61, "IF({0}61=\"\",\"\",IF({0}62=\"\",{0}61,{0}61+{0}62))" },
            {62, "IF({0}59=\"\",\"\",IF({0}62=\"\",\"\",{0}20+{0}22+{0}23-{0}61))" },
            };

        static public void MakeFormulae(string filePath)
        {
            dBConnection = new SQLiteConnection($"Data Source={filePath};Version=3;");
            dBConnection.Open();
            if (!utils.CheckForTable("Formulae"))
            {
                string sql = "CREATE TABLE Formulae(fid INTEGER PRIMARY KEY AUTOINCREMENT, row INTEGER, formula VARCHAR(100));";
                SQLiteCommand command = new SQLiteCommand(sql, dBConnection);
                command.ExecuteNonQuery();
            }

            foreach (KeyValuePair<int, string> fo in formulae)
            {
                string sql = $"INSERT INTO Formulae(row, formula) VALUES({fo.Key},'{fo.Value}');";
                SQLiteCommand command = new SQLiteCommand(sql, dBConnection);
                command.ExecuteNonQuery();
            }

            dBConnection.Close();
        }
    }
}
