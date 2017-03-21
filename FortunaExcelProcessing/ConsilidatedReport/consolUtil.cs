﻿using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SQLite;

namespace FortunaExcelProcessing.ConsilidatedReport
{
    class ConsolUtil
    {
        public static string NumToColName(int col)
        {
            int first = col / 26;
            int second = col % 26;
            string code = "";
            if (first > 0) code += (char)(first + 64);
            code += (char)(second + 65);
            return code;
        }

        public static void getDate()
        {
            DateTime date = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateStorage.PartialDate = date.ToString("dd MMM");
            DateStorage.FullDate = date.ToString("yyyy-MM-dd");
        }

        public static void getDate(DateTime dt)
        {
            DateTime date = dt.StartOfWeek(DayOfWeek.Monday);
            DateStorage.PartialDate = date.ToString("dd MMM");
            DateStorage.FullDate = date.ToString("yyyy-MM-dd");
        }


        public static void inputDataToSheet(string input, ICell cell)
        {
            double cellValue = double.Parse(input);
            cell.SetCellType(CellType.Numeric);
            cell.SetCellValue(cellValue);
        }

        public static string getFarmName(int farmId)
        {
            using (SQLiteConnection con = new SQLiteConnection(FilePaths.DBConString))
            {
                con.Open();
                string sqlQuery = $"Select name from farms where farmid = {farmId}";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, con);
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        return rdr.GetString(0);
                }
            }
            return "NULL";
        }

        public static string getFarmArea(int farmId)
        {
            using (SQLiteConnection con = new SQLiteConnection(FilePaths.DBConString))
            {
                con.Open();
                string sqlQuery = $"Select area from farms where farmid = {farmId}";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, con);
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        return rdr.GetDouble(1).ToString();
                }
            }
            return "NULL";
        }

        public static string getCows(int farmId)
        {
            using (SQLiteConnection con = new SQLiteConnection(FilePaths.DBFilePath))
            {
                con.Open();
                string sqlQuery = $"Select cows from farmSupplements where farmid = {farmId}";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlQuery, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            return rdr.GetString(0);
                        }
                    }
                }
                return "";
            }
        }

        public static Dictionary<int, string> getData(string fullDate)
        {
            Dictionary<int, string> _databaseDatas = new Dictionary<int, string>();

            using (SQLiteConnection con = new SQLiteConnection(FilePaths.DBConString))
            {
                con.Open();
                fullDate = "2016-12-26"; //overwrite will be taken away
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

        public static int getNumberofFarms()
        {
            using (SQLiteConnection con = new SQLiteConnection(FilePaths.DBConString))
            {
                con.Open();
                string cstring = "SELECT farmid, COUNT(farmid) FROM farms GROUP BY farmid";
                SQLiteCommand cmd = new SQLiteCommand(cstring, con);
                var farms = cmd.ExecuteScalar();
                return int.Parse(farms.ToString());
            }
        }
    }
}