using Newtonsoft.Json;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public struct HikeRegionForView
    {
        public int regionID, hikeCount, cpCount;
        public string name, description;
        public List<int> countryIDs;
        public List<string> countryNames;

        [JsonConstructor]
        public HikeRegionForView(int regionID, string name, List<int> countryIDs, List<string> countryNames, int hikeCount, int cpCount, string description)
        {
            this.regionID = regionID;
            this.countryIDs = countryIDs;
            this.name = name;
            this.countryNames = countryNames;
            this.hikeCount = hikeCount;
            this.cpCount = cpCount;
            this.description = description;
        }
    }
}
