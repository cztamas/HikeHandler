using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CountryForUpdate
    {
        public int CountryID { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
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
