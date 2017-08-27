using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SQLite;
//using Newtonsoft.Json;
using System.Net;
using System.Text;


namespace FortunaExcelProcessing.ConsilidatedReport
{
    class ConsolUtil
    {
        public static string NumToColName(int col)
        {
            int first = col / 26;
            int second = col % 26;
            string code = "";
            if (first > 0)
            {
                code += (char)(first + 64);
            }
            code += (char)(second + 65);
            return code;
        }

        public static string ReceiveResponse(string url)
        {
            WebClient wc = new WebClient();
            byte[] raw = wc.DownloadData(url);
            return Encoding.UTF8.GetString(raw);
        }

        public static void GetDate()
        {
            DateTime date = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateStorage.PartialDate = date.ToString("dd MMM");
            DateStorage.FullDate = date.ToString("yyyy-MM-dd");
        }

        public static void GetDate(string date1)
        {
            DateTime dt = DateTime.Parse(date1);
            DateTime date = dt.StartOfWeek(DayOfWeek.Monday);
            DateStorage.PartialDate = date.ToString("dd MMM");
            DateStorage.FullDate = date.ToString("yyyy-MM-dd");
        }

        public static void InputDataToSheet(string input, ICell cell)
        {
            double cellValue = double.Parse(input);
            cell.SetCellType(CellType.Numeric);
            cell.SetCellValue(cellValue);
        }

        public static string GetFarmName(int farmId)
        {
            string tmp = ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getfarmname?farmid={0}", farmId));
            string stripped = tmp.Substring(1, tmp.Length - 2);
            return stripped;
        }

        public static string GetFarmArea(int farmId)
        {
            string tmp = ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getarea?farmid={0}", farmId));
            string stripped = tmp.Substring(1, tmp.Length - 2);
            return stripped;
        }

        public static string GetCows(int farmId)
        {
            string tmp = ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getcows?farmid={0}", farmId));
            string stripped = tmp.Substring(1, tmp.Length - 2);
            return stripped;
        }

        public static Dictionary<int, string> GetData(string fullDate)
        {
            Dictionary<int, string> _databaseDatas = new Dictionary<int, string>();

            using (SQLiteConnection con = new SQLiteConnection(FilePaths.DBConString))
            {
                con.Open();

                string cstring = $"SELECT Branch_ID, Data_Array FROM Weekly_Data where Date_Sent = '{fullDate}'";

                using (SQLiteCommand cmd = new SQLiteCommand(cstring, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            _databaseDatas.Add(rdr.GetInt32(0), rdr.GetString(1));
                        }
                    }
                }

                con.Close();
            }

            return _databaseDatas;
        }

        public static int getNumberofFarms()
        {            
                string tmp = ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getfarmcount"));
                return int.Parse(tmp);
        }
    }
}
