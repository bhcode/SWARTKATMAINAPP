using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing.ErrorHandling
{
    /// <summary>
    /// Instantiate one of these objects when a hard or soft error occurs
    /// It is reccomended to add these to the public static lists conatained in 'Error Reporter'
    /// </summary>
    public class Error
    {
        /// <summary>
        /// The cell the error occurs in
        /// </summary>
        public ICell Cell { get; set; }
        /// <summary>
        /// The description for the error
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The level of severity for the error
        /// </summary>
        public int Severity { get; set; }
        /// <summary>
        /// The code assigned for the error, refer to official documentation
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// For any extra information related to the error
        /// </summary>
        public string ExtraInfo { get; set; }

        /// <summary>
        /// Intended for handling of hard errors, that don't pertain to single cell errors
        /// </summary>
        /// <param name="s">String for the basic error information</param>
        public Error(string s)
        {
            Description = s;
        }

        /// <summary>
        /// Intended for handling of soft errors, soft errors require the offending cell and the description of the error
        /// </summary>
        /// <param name="c">The cell that the error occurs in</param>
        /// <param name="s">String for the basic error information</param>
        public Error(ICell c, string s)
        {
            Cell = c;
            Description = s;
        }
    }
}
