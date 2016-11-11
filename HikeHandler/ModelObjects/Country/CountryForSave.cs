using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CountryForSave
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CountryForSave(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
