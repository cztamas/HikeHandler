using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public struct HikeForUpdate
    {
        public int hikeID, oldHikeType, newHikeType;
        public string description;
        public DateTime oldHikeDate, newHikeDate;
        public List<int> oldRegionIDs, newRegionIDs, oldCountryIDs, newCountryIDs, oldCPList, newCPList;

        [JsonConstructor]
        public HikeForUpdate(int hikeID, int countryID, int regionID, string description, DateTime oldHikeDate, DateTime newHikeDate, 
            int oldHikeType, int newHikeType, List<int> oldCPList, List<int> newCPList, List<int> oldRegionIDs,
            List<int> newRegionIDs, List<int> oldCountryIDs, List<int> newCountryIDs)
        {
            this.hikeID = hikeID;
            this.description = description;
            this.oldHikeDate = oldHikeDate;
            this.newHikeDate = newHikeDate;
            this.oldHikeType = oldHikeType;
            this.newHikeType = newHikeType;
            this.oldCPList = oldCPList;
            this.newCPList = newCPList;
            this.oldRegionIDs = oldRegionIDs;
            this.newRegionIDs = newRegionIDs;
            this.oldCountryIDs = oldCountryIDs;
            this.newCountryIDs = newCountryIDs;
        }
    }
}
