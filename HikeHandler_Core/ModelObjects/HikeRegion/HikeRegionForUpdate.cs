using Newtonsoft.Json;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public struct HikeRegionForUpdate
    {
        public int regionID;
        public string oldName, newName, description;
        public List<int> oldCountryIDs, newCountryIDs;

        [JsonConstructor]
        public HikeRegionForUpdate(int regionID, string oldName, string newName, string description,
            List<int> oldCountryIDs, List<int> newCountryIDs)
        {
            this.regionID = regionID;
            this.oldName = oldName;
            this.newName = newName;
            this.description = description;
            this.oldCountryIDs = oldCountryIDs;
            this.newCountryIDs = newCountryIDs;
        }
    }
}
