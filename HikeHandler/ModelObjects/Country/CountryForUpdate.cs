using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CountryForUpdate
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
                    throw new ArgumentNullException("name", "Country name cannot be empty.");
            }
        }
        public string Description { get; set; }

        public CountryForUpdate(int countryID, string name, string description)
        {
            CountryID = countryID;
            Name = name;
            Description = description;
        }
    }
}
