using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CPForView
    {
        public int CPID { get; set; }
        public int CountryID { get; set; }
        public int RegionID { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public CPType TypeOfCP { get; set; }
        public string Description { get; set; }
        public int HikeCount { get; set; }

        public CPForView(int idCP)
        {
            CPID = idCP;
        }

        public CPForView(int cpID, int countryID, int regionID, string name, CPType typeOfCP, int hikeCount, string description)
        {
            CPID = cpID;
            CountryID = countryID;
            RegionID = regionID;
            Name = name;
            TypeOfCP = typeOfCP;
            HikeCount = hikeCount;
            Description = description;
        }
    }
}
