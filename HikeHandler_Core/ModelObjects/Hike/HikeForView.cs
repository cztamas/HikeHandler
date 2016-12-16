using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class HikeForView
    {
        public int HikeID { get; set; }
        public int CountryID { get; set; }
        public int RegionID { get; set; }
        public int? Position { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string Description { get; set; }
        public DateTime HikeDate { get; set; }
        public HikeType HikeType { get; set; }
        public List<int> CPList { get; set; }
        // Returns the CPList in string format, or sets it from a given string representation
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
            set
            {
                CPList = value.ToCPList();
            }
        }

        public HikeForView(int hikeID)
        {
            HikeID = hikeID;
            CPList = new List<int>();
        }

        [JsonConstructor]
        public HikeForView(int hikeID, int countryID, int regionID, int? position, string countryName, string regionName, string description, 
            DateTime hikeDate, HikeType hikeType, List<int> cpList)
        {
            HikeID = hikeID;
            CountryID = countryID;
            RegionID = regionID;
            Position = position;
            CountryName = countryName;
            RegionName = regionName;
            Description = description;
            HikeDate = hikeDate;
            HikeType = hikeType;
            CPList = cpList;
        }

        public HikeForView(int hikeID, int countryID, int regionID, int? position, string countryName, string regionName, string description,
            DateTime hikeDate, HikeType hikeType, string cpString)
        {
            HikeID = hikeID;
            CountryID = countryID;
            RegionID = regionID;
            Position = position;
            CountryName = countryName;
            RegionName = regionName;
            Description = description;
            HikeDate = hikeDate;
            HikeType = hikeType;
            CPString = cpString;
        }
    }
}
