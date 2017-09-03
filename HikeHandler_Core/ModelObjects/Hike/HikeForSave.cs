using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public struct HikeForSave
    {
        public List<int> regionIDs, countryIDs, cpList;
        public int hikeType;
        public DateTime hikeDate;
        public string description;

        [JsonConstructor]
        public HikeForSave(List<int> countryIDs, List<int> regionIDs, int hikeType, DateTime hikeDate,
            List<int> cpList, string description)
        {
            this.countryIDs = countryIDs;
            this.regionIDs = regionIDs;
            this.hikeType = hikeType;
            this.hikeDate = hikeDate;
            this.cpList = cpList;
            this.description = description;
        }
    }
}
