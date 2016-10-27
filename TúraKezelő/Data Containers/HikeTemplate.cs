using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.Data_Containers
{
    public class HikeTemplate
    {
        public int? IDHike { get; set; }
        public int? IDCountry { get; set; }
        public int? IDRegion { get; set; }
        public IntPile Position { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string Description { get; set; }
        public DatePile HikeDate { get; set; }
        public HikeType? HikeType { get; set; }
        public List<int> CPList { get; set; }

        public HikeTemplate()
        {
            CPList = new List<int>();
        }

        public HikeTemplate(int hikeID)
        {
            IDHike = hikeID;
            CPList = new List<int>();
        }

        public string GetCPString()
        {
            string cpString = string.Empty;
            foreach (int item in CPList)
            {
                cpString += "." + item + ".";
            }
            return cpString;
        }
    }
}
