using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortunaExcelProcessing.Objects
{
    public class Branch
    {
        int id;
        string branchname;
        double area;
        string remember_token; //optional variable, only used when taking whole farms directly from the server
        string created_at; //optional variable, only used when taking whole farms directly from the server
        string updated_at; //optional variable, only used when taking whole farms directly from the server

        public int Id
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

        public string Name
        {
            get
            {
                return branchname;
            }

            set
            {
                branchname = value;
            }
        }

        public double Area
        {
            get
            {
                return area;
            }

            set
            {
                area = value;
            }
        }

        public string Remember_token
        {
            get
            {
                return remember_token;
            }

            set
            {
                remember_token = value;
            }
        }

        public string Created_at
        {
            get
            {
                return created_at;
            }

            set
            {
                created_at = value;
            }
        }

        public string Updated_at
        {
            get
            {
                return updated_at;
            }

            set
            {
                updated_at = value;
            }
        }

        public Branch() { }

        public Branch(int id, string name, double area)
        {
            Id = id;
            Name = name;
            Area = area;
        }
    }
}
