using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Hive
{
    int branchId;
    DateTime date;
    string location;
    string hiveBody;
    string honeySuper;
    int frames;
    string species;
    string forageEnv;

    public string ForageEnv { get => forageEnv; set => forageEnv = value; }
    public string Species { get => species; set => species = value; }
    public int Frames { get => frames; set => frames = value; }
    public string HoneySuper { get => honeySuper; set => honeySuper = value; }
    public string HiveBody { get => hiveBody; set => hiveBody = value; }
    public string Location { get => location; set => location = value; }
    public DateTime Date { get => date; set => date = value; }
    public int BranchId { get => branchId; set => branchId = value; }

    public Hive() { }

    public Hive(int branchid, DateTime date, string loc, string hbody, string hsuper, int frames, string spec, string forenv)
    {
        BranchId = branchid;
        Date = date;
        Location = loc;
        HiveBody = hbody;
        HoneySuper = hsuper;
        Frames = frames;
        Species = spec;
        ForageEnv = forenv;
    }
}