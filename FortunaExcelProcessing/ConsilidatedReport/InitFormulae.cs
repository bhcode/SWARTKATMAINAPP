using System.Collections.Generic;
using System.Data.SQLite;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    class InitFormulae
    {
        //public static List<FormulaEntry> formulaeList()
        public static Dictionary<int, string> formulaeList()
        {
            //List<FormulaEntry> formulae = new List<FormulaEntry>();

            /*using (SQLiteConnection con = new SQLiteConnection(FilePaths.DBConString))
            {
                con.Open();
                string cstring = $"SELECT * FROM Formulae";
                using (SQLiteCommand cmd = new SQLiteCommand(cstring, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            formulae.Add(new FormulaEntry(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetString(2)));
                    }
                }
                con.Close();
            }*/

            return DBExtras.Formulas.formulae;
        }

        public static int[] columnDetailArray()
        {
            int[] cd = { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 101, 102, 103, 104, 105, 106, 107, 109, 110, 111, 112, 113, 114, 115 };
            return cd;
        }

        public static int[] calcCellArray()
        {
            int[] cd = { 2, 3, 7, 13, 15, 18, 19, 20, 23, 25, 26, 30, 31, 32, 34, 39, 49, 50, 51, 52, 53, 55, 56, 58, 61, 62, 63 };
            return cd;
        }

        public static int[] dataCellArray()
        {
            int[] cd = { 4, 5, 6, 8, 9, 10, 11, 12, 14, 16, 17, 18, 21, 22, 24, 28, 35, 36, 37, 38, 40, 41, 42, 43, 44, 45, 46, 47, 48, 53, 54,55,56, 57,58, 60,61 };
            return cd;
        }
    }
}
