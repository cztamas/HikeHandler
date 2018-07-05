using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class HikeForSearch
    {
        public int? IDHike { get; set; }
        public int? IDCountry { get; set; }
        public int? IDRegion { get; set; }
        public IntPile Position { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        //public string Description { get; set; }
        public DatePile HikeDate { get; set; }
        public HikeType? HikeType { get; set; }
        public List<int> CPList { get; set; }

        public bool AnyCPOrder { get; set; }

        public HikeForSearch()
        {
            CPList = new List<int>();
        }
    }
}
