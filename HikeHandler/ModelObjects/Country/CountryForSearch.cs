using System;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class CountryForSearch
    {
        public string Name { get; set; }
        public IntPile HikeCount { get; set; }
        public IntPile CPCount { get; set; }
        public IntPile RegionCount { get; set; }

        public CountryForSearch(string countryName, IntPile hikePile, IntPile cpCount, IntPile regionCount)
        {
            HikeCount = hikePile;
            CPCount = cpCount;
            RegionCount = regionCount;
            Name = countryName;
        }
    }
}
