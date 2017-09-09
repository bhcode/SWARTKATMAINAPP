using System;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using FortunaExcelProcessing.Properties;
using System.Net;

/// <summary>
/// class for holding common utility methods or data
/// </summary>
public static class Util {

    public static string DateFormat()
    {
        return "yyyy-MM-dd hh:mm:ss";
    }

    public static string Date { get; set; }

    public static int Farmid { get; set; }

    /// <summary>
    /// retrieves the farm id from the local database
    /// </summary>
    static public int GetFarmID(string name)
    {
        string fn = name.Trim();
        using (SQLiteConnection dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", settings.Default.DbFilePath)))
        {
            dBConnection.Open();
            string sql = $"SELECT Branch_ID FROM Branch where Branch_ID = '{fn}';";
            using (SQLiteCommand command = new SQLiteCommand(sql, dBConnection))
            {
                if (command.ExecuteScalar() != null)
                {
                    return int.Parse(command.ExecuteScalar().ToString()); //return the farm id
                }
                else //cannot find a farm with the supplied name
                {
                    return 0; //return default value
                }
            }
        }
    }

    /// <summary>
    /// checks if a table exists within the local database
    /// </summary>
    public static bool CheckForTable(string tablename)
    {
        using (SQLiteConnection dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", settings.Default.DbFilePath)))
        {
            dBConnection.Open();
            string sql = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{tablename}'";
            using (SQLiteCommand command = new SQLiteCommand(sql, dBConnection))
            {
                if (command.ExecuteScalar() != null)
                {
                    return true; //table exists
                }
                return false;
            }
        }
    }


    /// <summary>
    /// checks if a sheet exists in the supplied workbook
    /// </summary>
    static public bool CheckForSheet(string sheetName, IWorkbook wb)
    {
        if(wb.GetSheet(sheetName) != null)
        {
            return true; //sheet exists
        }
        return false;
    }

    /// <summary>
    /// checks if the library has access the internet by attempting to establish a web connection
    /// </summary>
    public static bool CheckForInternetConnection()
    {
        try
        {
            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead("http://www.google.com"))  //try connect to google.com
                {
                    return true; //it successfully made the connection
                }
            }
        }
        catch
        {
            return false; //unable to establishc onnection
        }
    }
}