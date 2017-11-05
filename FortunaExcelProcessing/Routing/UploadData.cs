using FortunaExcelProcessing.Properties;
using FortunaExcelProcessing.Objects;

public class UploadData
{
    static string GuiControllerUrl = settings.Default.Website + "/" + settings.Default.GUIController;
    static string DbControllerUrl = settings.Default.Website + "/" + settings.Default.GUIController;
    static string token = "Ltdq242pY8E4Nb36gP8y";

    public static void CreateUser(User user)
    {
        ServerCommunication.UploadDataGet(string.Format("{3}/createuser?t={0}&username={3}&email={1}&password={2}", token, user.Email, user.Password, GuiControllerUrl, user.Name)); //need to alter it to accept returns
    }

    public static void CreateBranch(Branch branch)
    {
        ServerCommunication.UploadDataGet(string.Format("{3}/createmodbranch?t={4}&id={0}&name={1}&area={2}", 0, branch.Name, branch.Area, GuiControllerUrl, token));
    }

    public static void ModifyBranch(Branch branch)
    {
        ServerCommunication.UploadDataGet(string.Format("{3}/createmodbranch?t={4}&id={0}&name={1}&area={2}", branch.Id, branch.Name, branch.Area, GuiControllerUrl,token));
    }

    public static void ModifyPermissions(string email, int permission)
    {
        ServerCommunication.UploadDataGet(string.Format("{2}/assignrole?t={3}&email={0}&role={1}", email, permission, GuiControllerUrl, token));
    }

    public static void ModifyUser(string email, User user, bool updatePassword)
    {
        if (updatePassword)
            ServerCommunication.UploadDataGet(string.Format("{4}/modifyuser?t={5}&email={0}&newname={1}&newemail={2}&newpassword={3}", email, user.Name, user.Email, user.Password, GuiControllerUrl, token));
        else
            ServerCommunication.UploadDataGet(string.Format("{3}/modifyuser?t={4}&email={0}&newname={1}&newemail={2}", email, user.Name, user.Email, GuiControllerUrl, token));
    }

    public static void AssignBranch(string email, int branchId)
    {
        ServerCommunication.UploadDataGet(string.Format("{2}/assignbranch?t={3}&branch_id={0}&email={1}", branchId, email, GuiControllerUrl, token));
    }



    public static void UploadWeeklyData(WeeklyData wdata)
    {
        string temp = string.Format("{3}/uploadweekly?t={4}&branch_id={0}&date_sent={1}&data_array={2}", wdata.BranchId, wdata.Date.ToString("yyyy-MM-dd"), wdata.Data, DbControllerUrl,token);
        //System.IO.File.Create(@"C:\Database\" + temp + ".txt");
        ServerCommunication.UploadDataGet(temp);
    }

    public static void UploadLabel(Label label)
    {
        
        string temp = string.Format("{2}/uploadlabels?t={3}&row={0}&label={1}", label.Row, label.LabelText, DbControllerUrl,token);
        //System.IO.File.Create(@"C:\Database\" + temp + ".txt");
        ServerCommunication.UploadDataGet(temp);
        
    }

    public static void UploadHive(Hive hive)
    {
        string temp = string.Format("{8}/uploadhives?t={9}&branch_id={0}&date_sent={1}&location={2}&hive_body={3}&honey_super={4}&frames={5}&hive_species={6}&forage_environment={7}", hive.BranchId, hive.Date.ToString("yyyy-MM-dd"), hive.Location, hive.HiveBody, hive.HoneySuper, hive.Frames, hive.Species, hive.ForageEnv, DbControllerUrl, token);
        //System.IO.File.Create(@"C:\Database\" + temp + ".txt");
        ServerCommunication.UploadDataGet(temp);
    }

    public static void UploadObservation(Observation obs)
    {
        string temp = string.Format("{5}/uploadobservations?t={6}&branch_id={0}&date_sent={1}&category={2}&description={3}&weather={4}", obs.BranchId, obs.Date.ToString("yyyy-MM-dd"), obs.Category, obs.Description, obs.Weather, DbControllerUrl, token);
        //System.IO.File.Create(@"C:\Database\" + temp + ".txt");
        ServerCommunication.UploadDataGet(temp);
    }
}