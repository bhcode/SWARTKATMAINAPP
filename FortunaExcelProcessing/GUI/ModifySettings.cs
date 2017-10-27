using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortunaExcelProcessing.Properties;

namespace FortunaExcelProcessing.GUI
{
    public static class ModifySettings
    {
        public static string GetWorkingPath()
        {
            return settings.Default.WorkingFolder;
        }

        public static string GetDbName()
        {
            return settings.Default.DbName;
        }

        public static string GetDbFilePath()
        {
            return settings.Default.DbFilePath;
        }

        public static string GetReportFileName()
        {
            return settings.Default.ReportName;
        }

        public static string GetWinColour()
        {
            return settings.Default.WinColour;
        }

        public static string GetWinTheme()
        {
            return settings.Default.WinTheme;
        }

        public static string GetWebsiteUrl()
        {
            return settings.Default.Website;
        }

        public static bool GetIsLoginRemembered()
        {
            return settings.Default.LoginRemember;
        }

        public static User GetRememberedUser()
        {
            return new User("", settings.Default.LoginEmail, settings.Default.LoginPassword);
        }

        public static void UpdateRememberedUser(User user)
        {
            settings.Default.LoginRemember = true;
            settings.Default.LoginEmail = user.Email;
            settings.Default.LoginPassword = user.Password;
            settings.Default.Save();
        }

        public static void UpdateWorkingPath(string newPath)
        {
            settings.Default.WorkingFolder = newPath;
            settings.Default.Save();
            UpdateDbFilePath();
        }

        public static void UpdateDbFilePath()
        {
            settings.Default.DbFilePath = GetWorkingPath() + "//" + GetDbName() + ".sqlite";
            settings.Default.Save();
        }

        public static void UpdateWinColour(string newColour)
        {
            settings.Default.WinColour = newColour;
            settings.Default.Save();
        }

        public static void UpdateWinTheme(string newTheme)
        {
            settings.Default.WinTheme = newTheme;
            settings.Default.Save();
        }

        public static void ResetSettings()
        {
            settings.Default.Reset();
        }
    }
}
