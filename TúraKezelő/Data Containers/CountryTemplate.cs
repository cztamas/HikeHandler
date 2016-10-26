using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.Data_Containers
{
    public class CountryTemplate
    {
        public string Name { get; set; }
        public IntPile HikeCount { get; set; }

        public CountryTemplate()
        {
            HikeCount = new IntPile();
            Name = String.Empty;
        }

        public CountryTemplate(string countryName, IntPile hikePile)
        {
            HikeCount = hikePile;
            Name = countryName;
        }
    }
}
