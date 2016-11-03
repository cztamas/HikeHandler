using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HikeHandler.Data_Containers
{
    public class CountryForView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int HikeCount { get; set; }
        public int RegionCount { get; set; }
        public int CPCount { get; set; }
        public string Description { get; set; }

        public CountryForView(int countryID)
        {
            ID = countryID;
        }

        public CountryForView(int countryID, int hikeNumber, string countryName, string countryDescription)
        {
            ID = countryID;
            HikeCount = hikeNumber;
            Name = countryName;
            Description = countryDescription;
        }

        public CountryForView(string countryName, string countryDescription)
        {
            Name = countryName;
            Description = countryDescription;
            HikeCount = 0;
        }
    }
}
