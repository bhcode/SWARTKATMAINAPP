using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WeeklyData
{
    int farmId;
    DateTime date;
    string data;

    public int FarmId
    {
        get
        {
            return farmId;
        }

        set
        {
            farmId = value;
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

    public WeeklyData(int farmid, DateTime sdate, string data)
    {
        FarmId = farmid;
        Date = sdate;
        Data = data;
    }
}