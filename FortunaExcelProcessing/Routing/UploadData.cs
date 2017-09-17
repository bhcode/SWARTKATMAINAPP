using FortunaExcelProcessing.Properties;
using FortunaExcelProcessing.Objects;

public class UploadData
{
    static string GuiControllerUrl = settings.Default.Website + "/" + settings.Default.GUIController;
    static string DbControllerUrl = settings.Default.Website + "/" + settings.Default.GUIController;
    static string token = "Ltdq242pY8E4Nb36gP8y";

    public static void CreateUser(User user)
    {
        ServerCommunication.UploadDataGet(string.Format("{3}/createuser?t={0}&email={1}&password={2}", token, user.Email, user.Password, GuiControllerUrl)); //need to alter it to accept returns
    }

    public static void CreateBranch(Branch branch)
    {
        ServerCommunication.UploadDataGet(string.Format("{3}/createmodbranch?t={3}&id={0}&name={1}&area={2}", 0, branch.Name, branch.Area, GuiControllerUrl, token));
    }

    public static void ModifyBranch(Branch branch)
    {
        ServerCommunication.UploadDataGet(string.Format("{3}/createmodbranch?t={3}&id={0}&name={1}&area={2}", branch.Id, branch.Name, branch.Area, GuiControllerUrl));
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
        ServerCommunication.UploadDataGet(string.Format("{2}/assignbranch?t={3}&farmid={0}&email={1}", branchId, email, GuiControllerUrl, token));
    }



    public static void UploadWeeklyData(WeeklyData wdata)
    {
        ServerCommunication.UploadDataGet(string.Format("{3}/uploadweekly?branch_id={0}&date_sent={1}&data_array={2}", wdata.BranchId, wdata.Date.ToString("yyyy-MM-dd"), wdata.Data, DbControllerUrl));
    }

    public static void UploadLabel(Label label)
    {
        ServerCommunication.UploadDataGet(string.Format("{2}/uploadlabels?row={0}&label={1}", label.Row, label.LabelText, DbControllerUrl));
    }

    public static void UploadHive(Hive hive)
    {
        ServerCommunication.UploadDataGet(string.Format("{8}/uploadhives?branch_id={0}&date_sent={1}&location={2}&hive_body={3}&honey_super={4}&frames={5}&hive_species={6}&forage_environment={7}", hive.BranchId, hive.Date, hive.Location, hive.HiveBody, hive.HoneySuper, hive.Frames, hive.Species, hive.ForageEnv, DbControllerUrl));
    }

    public static void UploadObservation(Observation obs)
    {
        ServerCommunication.UploadDataGet(string.Format("{5}/uploadobservations?branch_id={0}&date_sent={1}&category={2}&description={3}&weather={4}", obs.BranchId, obs.Date.ToString("yyyy-MM-dd"), obs.Category, obs.Description, obs.Weather, DbControllerUrl));
    }
}