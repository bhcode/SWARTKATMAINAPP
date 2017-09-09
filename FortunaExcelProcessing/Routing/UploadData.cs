
class UploadData
{
    public static void CreateUser(User user)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/gui/createuser?username={0}&email={1}&password={2}", user.Name, user.Email, user.Password));
    }

    public static void UpdateUser(string email, User user)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/gui/modifyuser?email={0}&newname={1}&newemail={2}&newpassword={3}", email, user.Name, user.Email, user.Password));
    }

    public static void UpdateUser(string email, User user, bool updatePassword)
    {
        if (updatePassword)
            ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/gui/modifyuser?email={0}&newname={1}&newemail={2}&newpassword={3}", email, user.Name, user.Email, user.Password));
        else
            ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/gui/modifyuser?email={0}&newname={1}&newemail={2}", email, user.Name, user.Email));
    }

    public static void CreateFarm(Farm farm)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/gui/createmodfarm?id={0}&name={1}&area={2}", farm.Name, farm.Name, farm.Area));
    }

    public static void UpdateFarm(Farm farm)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/gui/createmodfarm?id={0}&name={1}&area={2}", farm.Id, farm.Name, farm.Area));
    }

    public static void UpdatePermissions(string email, int permission)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/gui/assignrole?email={0}&role={1}", email, (int)permission));
    }

    public static void AssignFarm(int farmid, string email)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/gui/assignfarm?farmid={0}&email={1}", farmid, email));
    }

    public static void UploadWeeklyDataGet(WeeklyData wdata)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/data/insertdata?farmid={0}&sdate={1}&data={2}", wdata.FarmId, wdata.Date.ToString("yyyy-MM-dd"), wdata.Data));
    }

    public static void UploadPaddocksGet(Paddock padd)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/data/insertlabels?farmid={0}&sdate={1}&paddockid={2}&crop={3}&size={4}", padd.FarmId, padd.Date.ToString("yyyy-MM-dd"), padd.Id, padd.Crop, padd.Size));
    }

    public static void UploadFarmSuppGet(FarmSupplement sup)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/data/insertfarmsupplements?farmid={0}&sdate={1}&cows={2}&supplements={3}", sup.FarmId, sup.Date.ToString("yyyy-MM-dd"), sup.Cows, sup.Supplements));
    }

    public static void UploadCommentsGet(Comment com)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/data/insertcomments?farmid={0}&sdate={1}&category={2}&description={3}", com.FarmId, com.Date.ToString("yyyy-MM-dd"), com.Category, com.Description));
    }

    public static void UploadLabelsGet(Label label)
    {
        ServerCommunication.UploadDataGet(string.Format("http://swartkat.fossul.com/data/insertlabels?row={0}&label={1}", label.Row, label.LabelText));
    }
}