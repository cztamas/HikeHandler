using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class HikeForUpdate
    {
        public int HikeID { get; set; }
        public int CountryID { get; set; }
        public int RegionID { get; set; }
        public string Description { get; set; }
        public DateTime OldHikeDate { get; set; }
        public DateTime NewHikeDate { get; set; }
        public HikeType OldHikeType { get; set; }
        public HikeType NewHikeType { get; set; }
        public List<int> OldCPList { get; set; }
        public List<int> NewCPList { get; set; }
        // Returns the NewCPList in string format
        public string NewCPString
        {
            get
            {
                string cpString = string.Empty;
                foreach (int item in NewCPList)
                {
                    cpString += "." + item + ".";
                }
                return cpString;
            }
        }

        [JsonConstructor]
        public HikeForUpdate(int hikeID, int countryID, int regionID, string description, DateTime oldHikeDate, DateTime newHikeDate, 
            HikeType oldHikeType, HikeType newHikeType, List<int> oldCPList, List<int> newCPList)
        {
            HikeID = hikeID;
            CountryID = countryID;
            RegionID = regionID;
            Description = description;
            OldHikeDate = oldHikeDate;
            NewHikeDate = newHikeDate;
            OldHikeType = oldHikeType;
            NewHikeType = newHikeType;
            OldCPList = oldCPList;
            NewCPList = newCPList;
        }
    }
}
