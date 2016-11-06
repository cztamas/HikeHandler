using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class HikeRegionForSave
    {
        public int CountryID
        {
            get
            {
                return CountryID;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("CountryID has to be positive.", "countryID");
                CountryID = value;
            }
        }
        public string Name
        {
            get
            {
                return Name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("name", "Region name cannot be whitespace or empty");
                Name = value;
            }
        }
        public string Description { get; set; }

        public HikeRegionForSave(int countryID, string name, string description)
        {
            CountryID = countryID;
            Name = name;
            Description = description;
        }
    }
}
