using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SQLite;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    class InitFormulae
    {
        public static List<FormulaEntry> formulaeList()
        {
           List<FormulaEntry> formulae = new List<FormulaEntry>();

            using (SQLiteConnection con = new SQLiteConnection(FilePaths.DBConString))
            {
                con.Open();
                string cstring = $"SELECT * FROM Formulae";

                using (SQLiteCommand cmd = new SQLiteCommand(cstring, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {

                            formulae.Add(new FormulaEntry(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetString(2)));
                        }
                    }
                }
                con.Close();
            }

            return formulae;
        }
    }
}
