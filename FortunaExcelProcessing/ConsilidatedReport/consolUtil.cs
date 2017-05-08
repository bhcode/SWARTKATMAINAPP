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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string NumToColName(int col)
        {
            int first = col / 26;
            int second = col % 26;
            string code = "";
            if (first > 0) code += (char)(first + 64);
            code += (char)(second + 65);
            return code;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="url"></param>
       /// <returns></returns>
        public static string ReceiveResponse(string url)
        {
            WebClient wc = new WebClient();
            byte[] raw = wc.DownloadData(url);
            return Encoding.UTF8.GetString(raw);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void getDate()
        {
            DateTime date = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateStorage.PartialDate = date.ToString("dd MMM");
            DateStorage.FullDate = date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public static void getDate(DateTime dt)
        {
            DateTime date = dt.StartOfWeek(DayOfWeek.Monday);
            DateStorage.PartialDate = date.ToString("dd MMM");
            DateStorage.FullDate = date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cell"></param>
        public static void inputDataToSheet(string input, ICell cell)
        {
            double cellValue = double.Parse(input);
            cell.SetCellType(CellType.Numeric);
            cell.SetCellValue(cellValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmId"></param>
        /// <returns></returns>
        public static string getFarmName(int farmId)
        {
            string tmp = ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getfarmname?farmid={0}", farmId));
            //Takes in the farmname and strips the " " off it, then returns it.
            string stripper = tmp.Substring(1, tmp.Length - 1);
            return stripper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmId"></param>
        /// <returns></returns>
        public static string getFarmArea(int farmId)
        {
            string tmp = ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getarea?farmid={0}", farmId));
            string stripper = tmp.Substring(1, tmp.Length - 1);
            return stripper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmId"></param>
        /// <returns></returns>
        public static string getCows(int farmId)
        {
            string tmp = ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/gecows?farmid={0}", farmId));
            string stripper = tmp.Substring(1, tmp.Length - 1);
            return stripper;
        }

        /// <summary>
        /// GetData method grabs all information in the data/weekly data table in the SQLite database
        /// It uses the fullDate to do this, this method is intended to be used for local consilidated processing and testing of the database.
        /// </summary>
        /// <param name="fullDate">Date to pass that defines what week to grab data from</param>
        /// <returns>A dictionary<int, string> of database data related to the date passed in is returned</int></returns>
        public static Dictionary<int, string> getData(string fullDate)
        {
            Dictionary<int, string> _databaseDatas = new Dictionary<int, string>();

            using (SQLiteConnection con = new SQLiteConnection(FilePaths.DBConString))
            {
                con.Open();

                string cstring = $"SELECT farmid, data FROM Datas where date = '{fullDate}'";

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

        /// <summary>
        /// Recieves the JSON file as a respose, then substrings expected information out of it.
        /// </summary>
        /// <remarks>
        /// I still feel 'stripper' is a somewhat questionable variable name.
        /// </remarks>
        /// <returns>
        /// Returns integer value that is parsed from a substring pulled from a JSON file.
        /// </returns>
        public static int getNumberofFarms()
        {            
                string tmp = ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getfarmcount"));
                string stripper = tmp.Substring(1, tmp.Length - 1);
                return int.Parse(stripper);
        }
    }
}
