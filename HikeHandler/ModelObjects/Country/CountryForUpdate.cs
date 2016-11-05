using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CountryForUpdate : CountryForSave
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
        
        public CountryForUpdate(int countryID, string name, string description) : base(name, description)
        {
            CountryID = countryID;
        }
    }
}
