using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.Data_Containers
{
    public class HikeRegionTemplate
    {
        public int? IDRegion { get; set; }
        public int? IDcountry { get; set; }
        public string CountryName { get; set; }
        public IntPile HikeCount { get; set; }
        public string Name { get; set; }

        public HikeRegionTemplate() { }

        public HikeRegionTemplate(int regionID)
        {
            IDRegion = regionID;
        }

        public HikeRegionTemplate(string nameOfCountry, string regionName, IntPile hikeNumber)
        {
            CountryName = nameOfCountry;
            Name = regionName;
            HikeCount = hikeNumber;
        }
    }
}
