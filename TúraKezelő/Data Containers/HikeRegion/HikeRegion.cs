using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace HikeHandler.Data_Containers
{
    public class HikeRegion
    {
        public int ID { get; set; }
        public int CountryID { get; set; }
        public int HikeCount { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }

        public HikeRegion() { }

        public HikeRegion(int regionID)
        {
            ID = regionID;
        }

        public HikeRegion(int idOfCountry, string regionName, string regionDescription)
        {
            CountryID = idOfCountry;
            Name = regionName;
            Description = regionDescription;
        }
    }
}
