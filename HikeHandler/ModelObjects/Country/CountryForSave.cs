using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CountryForSave
    {
        // Validity is checked in set accessors.
        public string Name
        {
            get
            {
                return Name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("name", "Country name cannot be whitespace or empty");
                Name = value;
            }
        }
        public string Description { get; set; }

        public CountryForSave(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
