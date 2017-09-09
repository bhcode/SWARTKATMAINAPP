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

        public static void ResetSettings()
        {
            settings.Default.Reset();
        }
    }
}
