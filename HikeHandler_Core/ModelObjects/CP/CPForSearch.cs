﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CPForSearch
    {
        public int? CPID { get; set; }
        public int? IDCountry { get; set; }
        public int? IDRegion { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public CPType? TypeOfCP { get; set; }
        public string Description { get; set; }
        public IntPile HikeCount { get; set; }

        public CPForSearch()
        { }
          
    }
}
