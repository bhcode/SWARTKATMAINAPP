using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FarmSupplement
{
    int farmId;
    DateTime date;
    string cows;
    string supplements;

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

    public string Cows
    {
        get
        {
            return cows;
        }

        set
        {
            cows = value;
        }
    }

    public string Supplements
    {
        get
        {
            return supplements;
        }

        set
        {
            supplements = value;
        }
    }

    public FarmSupplement(int farmid, DateTime sdate, string cows, string supplements)
    {
        FarmId = farmid;
        Date = sdate;
        Cows = cows;
        Supplements = supplements;
    }
}