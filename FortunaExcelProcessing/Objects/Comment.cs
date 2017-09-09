using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Comment
{
    int farmId;
    DateTime date;
    string category;
    string description;

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

    public Comment(int farmid, DateTime sdate, string cate, string desc)
    {
        FarmId = farmid;
        Date = sdate;
        Category = cate;
        Description = desc;
    }
}