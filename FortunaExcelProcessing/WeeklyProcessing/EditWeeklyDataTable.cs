using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.Data.SQLite;
using System.Globalization;
using System.Text;

namespace FortunaExcelProcessing.WeeklyProcessing
{
    class EditWeeklyDataTable : ITableEditor
    {
        //I want to kill myself

        ISheet _sheet;
        private int[] dataRows = { 6, 7, 8, 10, 11,12, 13, 14, 16, 18, 19, 21, 22, 24, 28, 32, 33, 34, 35, 37, 38, 39, 40, 41, 42, 43, 44, 45, 50, 51, 54, 56, 101, 102, 103, 104, 105, 106, 109, 110, 111, 112, 113, 114};
        private string[] dataLabels = { "Crop area", "Crop area Available", "Area Grazed (avg for last 2 pickups)", "Pre Grazing Cover", "Average Cover (kgDM/ha)", "Growth Rate (kgDM/ha/day)", "Predicted Growth Rate (kgDM/ha/day)", "Area shut-up for supplements", "Milked into Vat",
                                            "NOT milked into Vat", "Dry cows (On farm)", "Dry cows (Off farm)", "Kg Liveweight/cow", "Average MS/day (last 2 pickups)", "KgMS month to date", "Avg SCC (000) for last 2 pickups", "Protein Fat", "Ratio Calf Milk (litres)",
                                        "Grain (kgDM)", "Palm kernel (kgDM)", "Silage(kgDM)", "Balage (kgDM)", "Molasses (kgDM)", "Straw (kgDM)", "Hay (kgDM)", "Other (kgDM)", "Total Consumption (kgDM/cow/day)", "Area N applied (ha)", "Rate per hectare (kgN/ha)", "Deaths",
                                            "Cows Sold", "Two Year Dairy Heifers", "One Year Dairy Heifers", "AB Dairy Heifer Calves", "All Other Calves", "Bulls", "Other", "Two Year Dairy Heifers", "One Year Dairy Heifers", "AB Dairy Heifer Calves", "All Other Calves", "Bulls", "Other"};
        string sql; SQLiteCommand command; SQLiteConnection dbConn;


        public void EditTable(ISheet sheet)
        {
            dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));
            dbConn.Open();
            

                _sheet = sheet;
                if (!CheckForTable())
                {
                    sql = "CREATE TABLE Datas(id INTEGER PRIMARY KEY AUTOINCREMENT, farmid INTEGER, date VARCHAR(30), data VARCHAR(1000));";
                    command = new SQLiteCommand(sql, dbConn);
                    command.ExecuteNonQuery();
                }
                WeeklyDataTable(dbConn);
            dbConn.Close();
        }

        private void WeeklyDataTable(SQLiteConnection dbConn)
        {
            int FarmId = GetFarmID(CheckCellData.CellTypeString(_sheet.GetRow(2).GetCell(1)));

            for (int c = 3; c < _sheet.GetRow(6).LastCellNum; c++)
            {
                if (CheckCellData.CellTypeNumeric(_sheet.GetRow(7).GetCell(c)) != -1)
                {
                    //Console.WriteLine("I'm working!");
                    string date = CheckCellData.CellWeirdDate(_sheet.GetRow(3).GetCell(c)).ToString("yyyy-MM-dd");

                    if (!checkForExistingColumn(date, FarmId))
                    {
                        string output = "[";
                        for (int row = 0; row < dataRows.Length - 1; row++)
                        {
                            if (row != dataRows.Length)
                                output = output + CheckCellData.CellTypeNumeric(_sheet.GetRow(dataRows[row]).GetCell(c)) + ",";


                        }

                        output = output + CheckCellData.CellTypeNumeric(_sheet.GetRow(dataRows.Length - 1).GetCell(c)) + "]";

                        command.CommandText = $"INSERT INTO Datas(farmid, date, data) VALUES({FarmId}, @date,'{output}');";
                        command.Parameters.AddWithValue("@date", date);
                        //command.Connection = dbConn;
                        command.ExecuteNonQuery();

                        output = "";
                    }
                }


            }





            }

        private int GetFarmID(string farmName)
        {
            sql = $"SELECT farmid FROM farms where name = '{farmName}';";
            command = new SQLiteCommand(sql, dbConn);
            return int.Parse(command.ExecuteScalar().ToString());
        }

        private bool checkForExistingColumn(string date, int farmID)
        {
            sql = $"SELECT date FROM Datas where date = '{date}' AND farmid = '{farmID}'";
            command = new SQLiteCommand(sql, dbConn);
            if (command.ExecuteScalar() != null)
                return true;
            return false;
        }

        private bool CheckForTable()
        {
            sql = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'Datas'";
            command = new SQLiteCommand(sql, dbConn);
            if (command.ExecuteScalar() != null)
                return true;

            return false;
        }

        private void ExecuteDatabaseQuery(string command)
        {
            //SQLiteCommand cmd = new SQLiteCommand(command, dbConn);
            //cmd.ExecuteNonQuery();

            //VVVV Apparently the safer way of doing the command VVVV
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = command;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = dbConn;
            cmd.ExecuteNonQuery();
        }

        //-------------------------------------------------------------------------------------------------\\
        //Code revision, everything down from here is kept for prosterity\\
        //-------------------------------------------------------------------------------------------------\\


        //requires updated version

        /*ISheet weeklySheet;
        SQLiteConnection dbConn;
        public StringBuilder insert = new StringBuilder();

        public void EditTable(ISheet sheet)
        {
            weeklySheet = sheet;

            dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FilePaths.DBFilePath));
            dbConn.Open();

            CreateTables();

            ProcessFarmData();
            //ProcessProduction();
            //ProcessCowsOnFarm();
            //ProcessCowsOffFarm();

            dbConn.Close();
        }

        private void ExecuteDatabaseQuery(string command)
        {
            SQLiteCommand cmd = new SQLiteCommand(command, dbConn);
            cmd.ExecuteNonQuery();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = command;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = dbConn;
            cmd.ExecuteNonQuery();
        }

        private void ExecuteDatabaseQuery(string command, List<float> parameters, string date)
        {
            insert.Append(command + date);
            for (int i = 0; i < parameters.Count; i++)
            {
                insert.Append("," + parameters[i]);
            }
            insert.Append(");");

            Console.WriteLine(insert.ToString());
            SQLiteCommand cmd = new SQLiteCommand(insert.ToString(), dbConn);
            cmd.ExecuteNonQuery();
            insert.Clear();
        }

        private void CreateTables()
        {
            Console.WriteLine("db 1");
            ExecuteDatabaseQuery("CREATE TABLE CowsandLand (Update_Date Numeric, Total_Area real, Area_Available_to_Milkers real, Crop_Area real, Crop_Area_Available real, Area_Grazed real, Grazing_Interval real,"
                    + " Pre_Grazing_Cover real, Post_Grazing_Cover real, Average_Cover real, Growth_Rate real, Pred_Growth_Rate real, KgDM_Consumption real, Area_Shut_for_Supplements real, Total_Cows_Wintered real,"
                    + " Milked_Into_Vat real, NOT_Milked_into_Vat real, Total_Milking_Cows real, Dry_Cows_On_Farm real, Dry_Cows_Off_Farm real, Total_Cows_Week_Start real, KG_Liveweight_Cow real, KG_Liveweight_Ha real,"
                    + " Stocking_Rate real);");
            Console.WriteLine("db 2");
            ExecuteDatabaseQuery("CREATE TABLE Production (Update_Date Numeric, Average_MS_Day real, KgMS_Cows real, KgMS_Ha real, KgMS_month real, AVG_SCC_Fortnight real, Protein_Fat_Ratio real, Calf_Milk real,"
                                + " Supplements_Fed real, Grain real, Palm_Kernal real, Silage real, Balage real, Molasses real, Straw real, Hay real, Other real, Total_Consumtion real, Pasture_Requirements real,"
                                + " Demands_Ha_Day real, Predicted_Surplus_Deficit real, Predicted_Avg_Cover real, Area_N_Applied real, Rate_N_Applied real, Total_N_Applied real, Total_N_Applied_toDate real, Deaths real,"
                                + " Deaths_toDate real, Cows_Sold real, Cows_Sold_toDate real, Total_Cows real);");
            Console.WriteLine("db 3");
            ExecuteDatabaseQuery("CREATE TABLE StockOnFarm (Update_Date Numeric, TwoYear_Heifers float, OneYear_Heifer float, AB_Heifer_Calves float, Other_Calves float, Bulls float, Other float);");
            Console.WriteLine("Imworking");
            ExecuteDatabaseQuery("CREATE TABLE StockOffFarm (Update_Date Numeric, TwoYear_Heifers float, OneYear_Heifer float, AB_Heifer_Calves float, Other_Calves float, Bulls float, Other float);");
        }
        
        private void ProcessFarmData()
        {
            for (int x = 3; x <= weeklySheet.LastRowNum; x++)
            {      
                List<float> CowsList = new List<float>();
                List<float> ProdList = new List<float>();
                List<float> CowsOnList = new List<float>();
                List<float> CowsOffList = new List<float>();


                string Date = "";

                if ((float)weeklySheet.GetRow(7).GetCell(x).NumericCellValue == 0.0)
                    break;
                //CowsandLand Table being populated
                for (int y = 4; y < 27; y++)
                {
                    if(y == 4)
                        Date = CheckCellData.CheckCell(weeklySheet.GetRow(3).GetCell(x));
                    CowsList.Add((float)weeklySheet.GetRow(y).GetCell(x).NumericCellValue);
                }

                ExecuteDatabaseQuery("INSERT INTO CowsandLand VALUES(", CowsList, Date);

                //ProductionTable
                for (int y = 29; y < 59; y++)
                {
                    //Console.WriteLine("Doing Prod");
                    ProdList.Add((float)weeklySheet.GetRow(y).GetCell(x).NumericCellValue);
                }

                ExecuteDatabaseQuery("INSERT INTO Production VALUES(", ProdList, Date);

                //CowsOnFarm Table
                for (int y = 102; y <= 107; y++)
                {
                    //Console.WriteLine("Doing yomom");
                    CowsOnList.Add((float)weeklySheet.GetRow(y).GetCell(x).NumericCellValue);
                }

                ExecuteDatabaseQuery("INSERT INTO StockOnFarm VALUES(", CowsOnList, Date);

                //CowsOffFarm Table
                for (int y = 110; y < 116; y++)
                {
                    //Console.WriteLine("Doing yodad");
                    if (weeklySheet.GetRow(y).GetCell(x) == null) { 
                        CowsOffList.Add((float)0);
                    break;
                }
                    CowsOffList.Add((float)weeklySheet.GetRow(y).GetCell(x).NumericCellValue);
                }

                ExecuteDatabaseQuery("INSERT INTO StockOffFarm VALUES(", CowsOffList, Date);

            }
        }*/

    }
}

