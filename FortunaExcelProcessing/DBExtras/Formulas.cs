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
            {3,  "{0}2-({0}4+{0}14)" },
            {7,  "IF({0}6=0,0,{0}3/{0}6)" },
            {13, "IF({0}8=0,\"\",IF({0}19=0,\"\",({0}8-{0}9)*{0}6/19))" },
            {18, "{0}17/{0}16" },
            {19, "SUM({0}16:{0}17)" },
            {20, "{0}19/{0}15" },
            {23, "{0}19+{0}21+{0}22" },
            {25, "IF({0}2=0,\"\",{0}19/({0}2-{0}4)*{0}24)" } ,
            {26, "IF({0}19=0,\"\", {0}19/{0}3)" } ,
            {30, "{0}28/{0}29"},
            {31, "IF({0}16=0,\"\",IF({0}28=\"\",\"\",28/({0}16-{0}57-{0}60)))" },
            {34, "IF({0}26=\"\",\"\",IF({0}31=\"\",\"\",{0}16/({0}2-{0}4)*{0}31))"},
            {39, "SUM({0}40:{0}47)"},
            {49, "IF({0}48=\"\",\"\",{0}39-{0}48)" },
            {50, "IF({0}19=0,\"\",IF({0}46=0,\"\",({0}19*{0}46)/{0}3+({0}62*{0}21)/{0}3))" },
            {51, "IF({0}50=\"\",\"\",{0}12-{0}48)" },
            {52, "IF({0}51=\"\",\"\",{0}10+({0}51*7))"},
            {56, "IF({0}55=\"\",\"\",IF({0}54=\"\",{0}55,{0}55+{0}54))"},
            {58, "IF({0}57=\"\",\"\",IF({0}59=\"\",{0}57,{0}57+{0}59))" },
            {59, "{0}58/{0}15" },
            {61, "IF({0}60=\"\",\"\",IF({0}61=\"\",{0}60,{0}60+{0}61))" },
            {62, "IF({0}58=\"\",\"\",IF({0}61=\"\",\"\",{0}19+{0}21+{0}22-{0}60))" },
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
