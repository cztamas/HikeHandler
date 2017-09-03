using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public struct HikeForView
    {
        public int hikeID, hikeType;
        public int? position;
        public string description;
        public DateTime hikeDate;
        public List<int> cpList, countryIDs, regionIDs;
        public List<string> countryNames, regionNames;

        [JsonConstructor]
        public HikeForView(int hikeID, int? position, List<int> countryIDs, List<int> regionIDs,
            List<string> countryNames, List<string> regionNames, string description, DateTime hikeDate,
            int hikeType, List<int> cpList)
        {
            this.hikeID = hikeID;
            this.position = position;
            this.description = description;
            this.hikeDate = hikeDate;
            this.hikeType = hikeType;
            this.cpList = cpList;
            this.countryIDs = countryIDs;
            this.regionIDs = regionIDs;
            this.countryNames = countryNames;
            this.regionNames = regionNames;
        }
    }
}
