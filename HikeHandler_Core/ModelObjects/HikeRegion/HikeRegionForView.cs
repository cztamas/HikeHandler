using Newtonsoft.Json;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class HikeRegionForView
    {
        public int regionID, hikeCount, cpCount;
        public string name, description;
        public HashSet<NameAndID> countries;

        [JsonConstructor]
        public HikeRegionForView(int regionID, string name, HashSet<NameAndID> countries, int hikeCount, int cpCount, string description)
        {
            this.regionID = regionID;
            this.countries = countries;
            this.name = name;
            this.hikeCount = hikeCount;
            this.cpCount = cpCount;
            this.description = description;
        }
    }
}
