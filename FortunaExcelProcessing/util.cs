using System;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using FortunaExcelProcessing;

// <summary>
// The PaddockColumns enumerator is utilized outside of the library
// </summary>
public enum PaddockColumns
{
    PaddockIDCol = 0,
    PaddockSizeCol = 1,
    PaddockCropCol = 2
}

// <summary>
// The Util class is full of misc. methods that aid various other classes, think of it as a bunch of scrappy tools.
// The Util class should not be confused with the ConsolUtil class that is intended for code working with the Consolidated report.
// </summary>
public static class Util {

    static int farmid;
    static string date;

    // <summary>
    // Returns the string used for DateTime formatting, for the database inserts
    // </summary>
    // <returns>
    // String representing date format
    // </returns>
    public static string DForm()
    {
        return "yyyy-MM-dd hh:mm:ss";
    }

    // <summary>
    // 
    // </summary>
    public static string Date { get; set; }

    // <summary>
    // 
    // </summary>
    public static int Farmid { get; set; }

    // <summary>
    // 
    // </summary>
    // <param name="name"></param>
    // <returns></returns>
    static public int GetFarmID(string name)
    {
        string fn = name.Trim();
        using (SQLiteConnection dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath)))
        {
            dBConnection.Open();
            string sql = $"SELECT farmid FROM farms where name = '{fn}';";
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

    // <summary>
    // 
    // </summary>
    // <param name="tablename"></param>
    // <returns></returns>
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

    // <summary>
    // 
    // </summary>
    // <param name="sheetName"></param>
    // <param name="wb"></param>
    // <returns></returns>
    static public bool CheckForSheet(string sheetName, IWorkbook wb)
    {
        if(wb.GetSheet(sheetName) != null)
        {
            return true;
        }
        return false;
    }
}