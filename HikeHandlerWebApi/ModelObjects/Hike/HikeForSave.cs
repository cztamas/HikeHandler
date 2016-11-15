using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class HikeForSave
    {
        public int RegionID { get; set; }
        public int CountryID { get; set; }
        public HikeType HikeType { get; set; }
        public DateTime HikeDate { get; set; }
        public List<int> CPList { get; set; }
        public string Description { get; set; }
        // Returns the CPList in string format
        public string CPString
        {
            get
            {
                string cpString = string.Empty;
                foreach (int item in CPList)
                {
                    cpString += "." + item + ".";
                }
                return cpString;
            }
        }

        public HikeForSave(int countryID, int regionID, HikeType hikeType, DateTime hikeDate, List<int> cpList, string description)
        {
            CountryID = countryID;
            RegionID = regionID;
            HikeType = hikeType;
            HikeDate = hikeDate;
            CPList = cpList;
            Description = description;
        }
    }
}
