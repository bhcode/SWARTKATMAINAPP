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
    public class CreateCommentsTable : ITableMaker
    {
        public MissingCellPolicy empty = MissingCellPolicy.RETURN_BLANK_AS_NULL;
        private FilePaths paths = new FilePaths();
        private Dictionary<int, String> hell;
        private ICell getCell;
        private ICell getNextCell;

        public void MakeTable(ISheet sheet)
        {
            SQLiteConnection dBConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", paths.DBFilePath));
            dBConnection.Open();


            string sql = "CREATE TABLE Comments (Comment_Date BLOB, Animal_Health BLOB, Fertiliser_Applications BLOB, Jobs_Complete BLOB, Jobs_For_This_Week BLOB, Stock_Comments BLOB, General_Comments BLOB, EHR_Issues BLOB);";
            SQLiteCommand command = new SQLiteCommand(sql, dBConnection);
            command.ExecuteNonQuery();

            StringBuilder sqlQuery = new StringBuilder();
            String cellContent;

            for (int columnIndex = 2; columnIndex < 12; columnIndex++)
            {
                sqlQuery.Clear();
                sqlQuery.Append(string.Format("INSERT INTO Comments (Comment_Date, Animal_Health, Fertiliser_Applications, Jobs_Complete, Jobs_For_This_Week, Stock_Comments, General_Comments, EHR_Issues) VALUES ("));

                for (int rowIndex = 3; rowIndex < 11; rowIndex++)
                {
                    cellContent = sheet.GetRow(rowIndex).GetCell(columnIndex).ToString();
                    cellContent = cellContent.Replace("'", "");
                    sqlQuery.Append("'" + cellContent + "'" + (rowIndex == 10 ? "" : ","));
                }

                sqlQuery.Append(");");
                command = new SQLiteCommand(sqlQuery.ToString(), dBConnection);
                command.ExecuteNonQuery();
            }


            dBConnection.Close();

        }
    }
}

