using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WeeklyData
{
    int banchId;
    DateTime date;
    string data;

    public int BranchId
    {
        get
        {
            return banchId;
        }

        set
        {
            banchId = value;
        }
    }

    public DateTime Date
    {
        get
        {
            return date;
        }

        set
        {
            date = value;
        }
    }

    public string Data
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
    }

    public WeeklyData(int branchid, DateTime sdate, string data)
    {
        BranchId = branchid;
        Date = sdate;
        Data = data;
    }
}