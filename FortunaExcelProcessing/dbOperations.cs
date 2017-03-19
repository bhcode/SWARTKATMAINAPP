using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.Data.SQLite;

namespace FortunaExcelProcessing
{
    class dbOperations
    {
        public static void ExecuteDatabaseQuery(string command, SQLiteConnection dbConn)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = command;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = dbConn;
            cmd.ExecuteNonQuery();
        }

        public static void ExecuteDatabaseQuery(string command, SQLiteConnection dbConn, List<string> data)
        {
            string tmp = command;

            for (int i = 0; i < data.Count - 1; i++)
            {
                tmp += (data[i] + ",");
            }
            tmp += (data[data.Count - 1] + ")");

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = tmp;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = dbConn;
            cmd.ExecuteNonQuery();
        }
    }
}
