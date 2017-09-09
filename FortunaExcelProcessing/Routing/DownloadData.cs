using System.Collections.Generic;
using Newtonsoft.Json;

class DownloadData
{
    public static List<User> GetAllUsers()
    {
        string tmp = ServerCommunication.DownloadDataGet("http://swartkat.fossul.com/gui/getusers");
        return JsonConvert.DeserializeObject<List<User>>(tmp);
    }

    public static List<Farm> GetAllFarms()
    {
        string tmp = ServerCommunication.DownloadDataGet("http://swartkat.fossul.com/gui/getfarms");
        return JsonConvert.DeserializeObject<List<Farm>>(tmp);
    }

    public static Farm GetUserFarm(string email)
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("http://swartkat.fossul.com/gui/getusersfarm?email={0}", email));
        return JsonConvert.DeserializeObject<Farm>(tmp);
    }

    public static Dictionary<int, string> GetWeeklyFarmData(string date)
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("http://swartkat.fossul.com/gui/getdata?fulldate={0}", date)); //JSON decoded
        return JsonConvert.DeserializeObject<Dictionary<int, string>>(tmp);
    }

    public static PermissionLevel GetUserRole(string userEmail)
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("http://swartkat.fossul.com/gui/getuserrole?email={0}", userEmail));
        return (PermissionLevel)(int.Parse(tmp));
    }

    public static List<DateHolder> GetUserWeeklyDataDates(int userID)
    {
        try
        {
            string tmp = ServerCommunication.DownloadDataGet(string.Format("http://swartkat.fossul.com/gui/getfarmdates?userid={0}", userID));
            return JsonConvert.DeserializeObject<List<DateHolder>>(tmp);
        }
        catch
        {
            return null;
        }
    }

    public static List<DateHolder> GetUserWeeklyDataDates()
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("http://swartkat.fossul.com/gui/getuploaddates"));
        return JsonConvert.DeserializeObject<List<DateHolder>>(tmp);
    }

    public static bool GetLoginSuccess(string email, string password)
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("http://swartkat.fossul.com/gui/finduser?email={0}&password={1}", email, password));
        tmp = tmp.Substring(1, tmp.Length - 2);
        return bool.Parse(tmp);
    }
}