using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class HikeRegionForSave
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public HikeRegionForSave(int countryID, string name, string description)
        {
            CountryID = countryID;
            Name = name;
            Description = description;
        }
    }
}
