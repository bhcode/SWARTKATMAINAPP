using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FortunaExcelProcessing.DBExtras
{
    class utils
    {
        static public bool CheckForTable(String tablename)
        {
            SQLiteConnection dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", @"C:\Database\database.sqlite"));
            dBConnection.Open();
            string sql = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{tablename}'";
            SQLiteCommand command = new SQLiteCommand(sql, dBConnection);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }
    }
}
