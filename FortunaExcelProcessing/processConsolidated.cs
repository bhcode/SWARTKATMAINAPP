using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SQLite;

namespace FortunaExcelProcessing
{
    public class processConsolidated
    {
        ISheet _sheet; IWorkbook _wb; int _numOfFarms; string _fullDate; string _farmArea; string _farmName; string _partialDate;
        string DBFile = @"data source=C:\Database\database.sqlite; Version=3;";
        Dictionary<int, string> _databaseDatas = new Dictionary<int, string>();

        #region rowLabel hardcoded Data
        //eventually ill stop being lazy and grab the array below from the DB
        string[] rowLabels = { "Farm:", "Week Ending:", "Total  Area(ha)", "Area available to milkers","Crop area", "Crop area Available","Area Grazed(avg for last 2 pickups)","Grazing interval","Pre Grazing Cover","Post Grazing Cover(Ave for week)","Average Cover(kgDM/ha)","Growth Rate(kgDM/ha/day)",
             "Predicted Growth Rate(kgDM/ha/day)","KgDM consumption/cow(Pasture)","Area shut-up for supplements","Total cows wintered","Milked into Vat","NOT milked into Vat", "% not in vat", "Total milking cows", "% cows calved", "Dry cows(On farm)",
             "Dry cows(Off farm)","Total cows at beginning of week","Kg Liveweight/cow","Kg Liveweight/Ha","Stocking Rate(milkersonly)","Production", "Average MS/day (last 2 pickups)","December DailyTarget","% to target",
             "KgMS/Cows in vat","Weekly % change","KgMS/total cows milked", "KgMS/Ha", "KgMS month to date", "Avg SCC (000) for last 2pickups","Protein Fat Ratio","Calf Milk (litres)",
             "Supplements Fed (kgDM/cow/day)","Grain (kgDM)", "Palm kernel (kgDM)", "Silage(kgDM)","Balage (kgDM)","Molasses (kgDM)","Straw(kgDM)","Hay (kgDM)","Other (kgDM)", "Total Consumption(kgDM/cow/day)","Pasture Requirements(Milkers Only)","Demand/ha/day","Predicted Surplus/Deficit(kgDM/ha)","Predicted average cover",
             "Area N applied(ha)","Rate per hectare(kgN/ha)","Total N applied(kgN/ha)","Total N applied Year To Date(kgN/ha)","Deaths","Deaths to date", "% deaths", "Cows Sold","Cows Sold to date","Total Cows at end of week","Balance Check" };
        int[] indent = { 18, 20, 28, 30, 31, 32, 33, 35, 36, 37, 38, 40, 41, 42, 43, 44, 45, 46, 47, 59 };
        #endregion

        List<FormulaEntry> formulae = new List<FormulaEntry>();

        public void initFormulae()
        {
            using (SQLiteConnection con = new SQLiteConnection(DBFile))
            {
                con.Open();
                string cstring = $"SELECT * FROM Formulae";

                using (SQLiteCommand cmd = new SQLiteCommand(cstring, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {

                            formulae.Add(new FormulaEntry(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetString(2)));
                        }
                    }
                }
                con.Close();
            }
        }

        #region help
        int[] clumnDeets = { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 29, 30, 31, 32, 33, 34,
35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52,
53, 54, 55, 56, 57, 58, 59, 60, 101, 102, 103, 104, 105, 106, 107, 109,
110, 111, 112, 113, 114, 115 };
        int[] calcedCells = { 2,3, 7, 13, 15,18, 19,20, 23, 25, 26, 30, 31,
32,34, 39, 49, 50, 51, 52, 53, 55, 56, 58, 61,62,63 };
        int[] dataCells = { 4, 5, 6, 8, 9, 10, 11, 12, 14, 16, 17, 18, 21, 22, 24, 28, 35, 36, 37, 38, 40, 41, 42, 43, 44, 45, 46, 47, 48, 53, 54, 57, 60 };
        #endregion


        public void createWorkBook()
        {
            _wb = new XSSFWorkbook();

            //calculate date, by getting the start of the week provided (likely through the form)
            _sheet = _wb.CreateSheet(getDate());

            //call methods to read info into workbook
            workbookData();

            using (FileStream fs = new FileStream(@"C:\Database\testwb.xlsx", FileMode.Create, FileAccess.ReadWrite))
            {
                _wb.Write(fs);
                _wb.Close();
            }
        }

        private string getDate()
        {
            DateTime date = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            //take in date from form, will need to be jiggled
            _partialDate = date.ToString("dd MMM");
            _fullDate = date.ToString("yyyy-MM-dd");
            return date.ToString("dd MMM");
        }

        private void workbookData()
        {
            _numOfFarms = getNumberofFarms();

            for (int row = 0; row < 64; row++)
            {
                rowLabeler(row);
            }
            _sheet.AutoSizeColumn(1);
            getData();

            for (int col = 2; col < _numOfFarms + 2; col++)
            {
                Console.WriteLine("Im attempting to write the datas");
                ICell cell;

                foreach (KeyValuePair<int, string> b in _databaseDatas)
                {
                    //test that data was pulled from db
                    Console.WriteLine(b.Key + " " + b.Value);
                    string stripper = b.Value.Substring(1, b.Value.Length - 2);
                    string[] sArray = stripper.Split(',');

                    getFarmInfo(b.Key);

                    for (int i = 0; i < 33; i++)
                    {
                        Console.WriteLine(sArray[i]);
                        if (i == 0)
                        {
                            XSSFCellStyle style = (XSSFCellStyle)_wb.CreateCellStyle();
                            XSSFFont font = (XSSFFont)_wb.CreateFont();
                            cell = _sheet.GetRow(0).CreateCell(col);
                            font.FontHeightInPoints = 10;
                            style.SetFont(font);
                            cell.CellStyle = style;
                            cell.SetCellValue(_farmName);
                        }
                        if (i == 1)
                        {
                            XSSFCellStyle style = (XSSFCellStyle)_wb.CreateCellStyle();
                            XSSFFont font = (XSSFFont)_wb.CreateFont();
                            cell = _sheet.GetRow(1).CreateCell(col);
                            font.FontHeightInPoints = 12;
                            font.SetColor(new XSSFColor(new byte[3] { 192, 0, 0 }));
                            style.Alignment = HorizontalAlignment.Center;
                            style.SetFont(font);
                            cell.CellStyle = style;
                            cell.SetCellValue(_partialDate);
                        }

                        if (i == 2)
                        {
                            cell = _sheet.GetRow(2).CreateCell(col);
                            inputDataToSheet(_farmArea, cell);
                        }

                        if (i == 15)
                        {
                            cell = _sheet.GetRow(15).CreateCell(col);
                            inputCellFifteen(cell, 15, b.Key);

                        }
                        else if (i == 55)
                        {
                            cell = _sheet.GetRow(55).CreateCell(col);
                            inputCellFiftyFive(cell, 55);
                        }
                        cell = _sheet.GetRow(dataCells[i]).CreateCell(col);
                        inputDataToSheet(sArray[i], cell);
                    }
                }

                initFormulae();
                Console.WriteLine(formulae.Count);
                foreach (FormulaEntry formula in formulae)
                {
                    cell = _sheet.GetRow(formula.Row).CreateCell(col);
                    inputCellFormula(string.Format(formula.Formula, KensCode.NumToColName(col)), cell);
                    //Console.WriteLine("Im attempting to write the formulae");

                }

            }
        }

        private void inputDataToSheet(string input, ICell cell)
        {
            double cake = double.Parse(input);
            cell.SetCellType(CellType.Numeric);
            cell.SetCellValue(cake);
        }

        private void getFarmInfo(int farmId)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBFile))
            {
                con.Open();
                string sqlQuery = $"Select name, area from farms where farmid = {farmId}";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, con);
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        _farmName = rdr.GetString(0);
                        _farmArea = rdr.GetDouble(1).ToString();
                    }
                }

            }
        }

        private void inputCellFiftyFive(ICell cell, int col)
        {

            string formula = string.Format("=IF({0}53=\"\";\"\";{0}53*{0}54/{1})", KensCode.NumToColName(col), _farmArea);
            cell.SetCellType(CellType.Formula);
            cell.SetCellFormula(formula);
        }

        public string getCows(int farmId)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBFile))
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

        private void inputCellFifteen(ICell cell, int col, int farmId)
        {
            string e6 = "", e7 = "";
            string procString = getCows(farmId);
            Console.WriteLine(procString);
            procString = procString.Substring(1, procString.Length - 1);
            string[] outstring = procString.Split(',');

            e6 = string.Format("{0}", outstring[0]);
            e7 = string.Format("{0}", outstring[1]);

            Console.WriteLine(e6 + e7);
            Console.ReadLine();
            string formula = string.Format("{0}+{1}", e6, e7);
            cell.SetCellType(CellType.Formula);
            cell.SetCellFormula(formula);
        }

        private void inputCellFormula(string formula, ICell cell)
        {
            cell.SetCellType(CellType.Formula);
            cell.SetCellFormula(formula);
        }

        private void getData()
        {
            using (SQLiteConnection con = new SQLiteConnection(DBFile))
            {
                con.Open();
                _fullDate = "2016-12-26";
                string cstring = $"SELECT farmid, data FROM Datas where date = '{_fullDate}'";

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
        }

        private int getNumberofFarms()
        {
            using (SQLiteConnection con = new SQLiteConnection(DBFile))
            {
                con.Open();
                string cstring = "SELECT farmid, COUNT(farmid) FROM farms GROUP BY farmid";
                SQLiteCommand cmd = new SQLiteCommand(cstring, con);
                var farms = cmd.ExecuteScalar();
                return int.Parse(farms.ToString());
            }
        }

        private void rowLabeler(int row)
        {
            ICell cell; XSSFCellStyle style; XSSFFont font;

            font = (XSSFFont)_wb.CreateFont();
            if (row == 0)
                font.FontHeightInPoints = 14;
            else
                font.FontHeightInPoints = 12;
            font.FontName = "Arial";

            style = (XSSFCellStyle)_wb.CreateCellStyle();

            if (indent.Contains(row))
            {
                cell = _sheet.CreateRow(row).CreateCell(1);
                font.IsBold = false;
                style.SetFont(font);
            }
            else
            {
                cell = _sheet.CreateRow(row).CreateCell(0);
                font.IsBold = true;
                style.SetFont(font);
            }
            cell.SetCellValue(rowLabels[row]);
            cell.CellStyle = style;

            //PRFM and EFF
            if (row == 31)
            {
                ICell pCell = _sheet.GetRow(31).CreateCell(0);
                pCell.SetCellValue("Prfm. Index");
            }
            if (row == 33)
            {
                ICell eCell = _sheet.GetRow(33).CreateCell(0);
                eCell.SetCellValue("Eff. Index");
            }
        }
    }
}
