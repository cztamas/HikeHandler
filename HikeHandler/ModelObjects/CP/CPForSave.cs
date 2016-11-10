using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CPForSave
    {
        public int CountryID { get; set; }
        public int RegionID { get; set; }
        public string Name { get; set; }
        public CPType TypeOfCP { get; set; }
        public string Description { get; set; }

        public CPForSave(int countryID, int regionID, string name, CPType typeOfCP, string description)
        {
            CountryID = countryID;
            RegionID = regionID;
            Name = name;
            Description = description;
            TypeOfCP = typeOfCP;
        }
    }
}
