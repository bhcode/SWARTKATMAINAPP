using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.POIFS.FileSystem;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data.SQLite;


namespace FortunaExcelProcessing.WeeklyProcessing
{
    class CreatePaddocksTable : ITableMaker
    {
        public void MakeTable(ISheet sheet)
        {
            ISheet paddockSheet = sheet;
            SQLiteConnection dbConn;
            dbConn = new SQLiteConnection(DateTime.Now.ToString());
            dbConn.Open();

            string sql = "CREATE TABLE Paddocks (paddockID varchar(3), hectareSize float, crop varchar(3))";
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConn);
            cmd.ExecuteNonQuery();
            //written by Jesse Lilley
        }
    }
}
