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

public static class Util {

    public static string DForm()
    {
        return "yyyy-MM-dd hh:mm:ss";
    }

    public static string Date { get; set; }

    public static int Farmid { get; set; }

    static public int GetFarmID(string name)
    {
        string fn = name.Trim();
        using (SQLiteConnection dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath)))
        {
            dBConnection.Open();
            string sql = $"SELECT Branch_ID FROM Branch where Branch_ID = '{fn}';";
            using (SQLiteCommand command = new SQLiteCommand(sql, dBConnection))
            {
                if (command.ExecuteScalar() != null)
                {
                    return int.Parse(command.ExecuteScalar().ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public static bool CheckForTable(String tablename)
    {
        using (SQLiteConnection dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath)))
        {
            dBConnection.Open();
            string sql = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{tablename}'";
            using (SQLiteCommand command = new SQLiteCommand(sql, dBConnection))
            {
                if (command.ExecuteScalar() != null)
                {
                    return true;
                }
                return false;
            }
        }
    }

    static public bool CheckForSheet(string sheetName, IWorkbook wb)
    {
        if(wb.GetSheet(sheetName) != null)
        {
            return true;
        }
        return false;
    }
}