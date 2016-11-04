using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CountryForSearch
    {
        public string Name { get; set; }
        public IntPile HikeCount { get; set; }

        public CountryForSearch()
        {
            HikeCount = new IntPile();
            Name = String.Empty;
        }

        public CountryForSearch(string countryName, IntPile hikePile)
        {
            HikeCount = hikePile;
            Name = countryName;
        }
    }
}
