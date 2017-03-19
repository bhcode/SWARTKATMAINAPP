using System;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using FortunaExcelProcessing;

public enum PaddockColumns
{
    PaddockIDCol = 0,
    PaddockSizeCol = 1,
    PaddockCropCol = 2
}

public static class util{
    static int farmid;
    static string date;

    public static string Date
    {
        get
        {
            return date;
        }

        set
        {
            date = value;
        }
    }

    public static int Farmid
    {
        get
        {
            return farmid;
        }

        set
        {
            farmid = value;
        }
    }


    static public int GetFarmID(string name)
    {
        SQLiteConnection dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));
        dBConnection.Open();
        string sql = $"SELECT farmid FROM farms where name = '{name}';";
        SQLiteCommand command = new SQLiteCommand(sql, dBConnection);
        if (command.ExecuteScalar() != null)
            return int.Parse(command.ExecuteScalar().ToString());
        else
            return 0;
    }


    static public bool CheckForTable(String tablename)
    {
        SQLiteConnection dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));
        dBConnection.Open();
        string sql = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{tablename}'";
        SQLiteCommand command = new SQLiteCommand(sql, dBConnection);
        if (command.ExecuteScalar() != null)
            return true;
        return false;
    }
}