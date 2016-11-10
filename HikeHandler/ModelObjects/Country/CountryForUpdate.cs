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
        public string OldName
        {
            get
            {
                return OldName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("OldName", "Country name cannot be whitespace or empty");
                OldName = value;
            }
        }
        public string NewName
        {
            get
            {
                return NewName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("NewName", "Country name cannot be whitespace or empty");
                NewName = value;
            }
        }
        public string Description { get; set; }

        public CountryForUpdate(int countryID, string oldName, string newName, string description)
        {
            CountryID = countryID;
            OldName = oldName;
            NewName = newName;
            Description = description;
        }
    }
}
