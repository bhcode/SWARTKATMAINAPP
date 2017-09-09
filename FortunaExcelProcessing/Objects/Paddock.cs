using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Paddock
{
    int farmId;
    DateTime date;
    string id;
    string crop;
    double size;

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

    public string Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Crop
    {
        get
        {
            return crop;
        }

        set
        {
            crop = value;
        }
    }

    public double Size
    {
        get
        {
            return size;
        }

        set
        {
            size = value;
        }
    }

    public Paddock(int farmid, DateTime sdate, string paddock, string crop, double size)
    {
        FarmId = farmid;
        Date = sdate;
        Id = paddock;
        Crop = crop;
        Size = size;
    }
}
