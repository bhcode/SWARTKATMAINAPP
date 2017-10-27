using System.Collections.Generic;
using Newtonsoft.Json;
using FortunaExcelProcessing.Properties;
using FortunaExcelProcessing.Objects;

public class DownloadData
{
    static string GuiControllerUrl = settings.Default.Website + "/" + settings.Default.GUIController;
    static string token = "Ltdq242pY8E4Nb36gP8y";

    public static bool IsLoginCorrect(string email, string password)
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("{0}/finduser?t={3}&email={1}&password={2}", GuiControllerUrl, email, password, token));
        tmp = tmp.Substring(1, tmp.Length - 2);
        return bool.Parse(tmp);
    }

    public static Branch GetUserBranch(string email)
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("{0}/getusersbranch?t={2}&email={1}", GuiControllerUrl, email, token));
        string[] bits = tmp.Split('"');
        try
        {
            Branch branch = new Branch(int.Parse(bits[1]), bits[3], double.Parse(bits[5]));
            return branch;
        }
        catch
        {
            return null;
        }
    }

    public static PermissionLevel GetUserRole(string userEmail)
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("{0}/getuserrole?t={2}&email={1}", GuiControllerUrl, userEmail, token));
        return (PermissionLevel)(int.Parse(tmp));
    }

    //get branch name (string)

    //get branch area (double)

    //get cons data

    //get branch dates

    public static List<User> GetAllUsers()
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("{0}/getusers?t={1}", GuiControllerUrl, token));
        return JsonConvert.DeserializeObject<List<User>>(tmp);
    }

    public static List<Branch> GetAllBranches()
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("{0}/getbranches?t={1}", GuiControllerUrl, token)); //{0}/getbranches?t={1}
        return JsonConvert.DeserializeObject<List<Branch>>(tmp);
    }


    //OLD ROUTES VVVV

    public static Dictionary<int, string> GetWeeklyFarmData(string date)
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("{0}/getdata?fulldate={1}", GuiControllerUrl, date)); //JSON decoded
        return JsonConvert.DeserializeObject<Dictionary<int, string>>(tmp);
    }



    public static List<DateHolder> GetUserWeeklyDataDates(int userID)
    {
        try
        {
            string tmp = ServerCommunication.DownloadDataGet(string.Format("{0}/getfarmdates?userid={1}", GuiControllerUrl, userID));
            return JsonConvert.DeserializeObject<List<DateHolder>>(tmp);
        }
        catch
        {
            return null;
        }
    }

    public static List<DateHolder> GetUserWeeklyDataDates()
    {
        string tmp = ServerCommunication.DownloadDataGet(string.Format("{0}/getuploaddates", GuiControllerUrl));
        return JsonConvert.DeserializeObject<List<DateHolder>>(tmp);
    }


}