using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Observation
{
    int branchId;
    DateTime date;
    string category;
    string description;
    string weather;

    public int BranchId
    {
        get
        {
            return branchId;
        }

        set
        {
            branchId = value;
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

    public string Category
    {
        get
        {
            return category;
        }

        set
        {
            category = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public string Weather { get => weather; set => weather = value; }

    public Observation(int farmid, DateTime sdate, string cate, string desc, string weath)
    {
        BranchId = farmid;
        Date = sdate;
        Category = cate;
        Description = desc;
        Weather = weath;
    }
}