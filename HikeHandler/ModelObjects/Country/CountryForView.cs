using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HikeHandler.ModelObjects
{
    public class CountryForView
    {
        // Validity is checked in set accessors.
        public int CountryID
        {
            get
            {
                return CountryID;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("CountryID has to be positive.", "CountryID");
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
                    throw new ArgumentNullException("Name", "Country name cannot be whitespace or empty.");
                Name = value;
            }
        }
        public int HikeCount
        {
            get
            {
                return HikeCount;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("HikeCount has to be positive.", "HikeCount");
                HikeCount = value;
            }
        }
        public int RegionCount
        {
            get
            {
                return RegionCount;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("RegionCount has to be positive.", "RegionCount");
                RegionCount = value;
            }
        }
        public int CPCount
        {
            get
            {
                return CPCount;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("CPCount has to be positive.", "CPCount");
                CPCount = value;
            }
        }
        public string Description { get; set; }
        
        public CountryForView(int countryID, string name, int hikeCount, int regionCount, int cpCount, string description)
        {
            CountryID = countryID;
            HikeCount = hikeCount;
            RegionCount = regionCount;
            CPCount = cpCount;
            Name = name;
            Description = description;
        }
    }
}
