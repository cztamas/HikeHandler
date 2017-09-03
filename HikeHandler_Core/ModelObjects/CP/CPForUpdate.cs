using Newtonsoft.Json;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public struct CPForUpdate
    {
        public int cpID, cpType;
        public string oldName, newName, description;
        public List<int> oldRegionIDs, newRegionIDs, oldCountryIDs, newCountryIDs;

        [JsonConstructor]
        public CPForUpdate(int cpID, string oldName, string newName, int cpType, string description, List<int> oldRegionIDs,
            List<int> newRegionIDs, List<int> oldCountryIDs, List<int> newCountryIDs)
        {
            this.cpID = cpID;
            this.oldName = oldName;
            this.newName = newName;
            this.cpType = cpType;
            this.description = description;
            this.oldRegionIDs = oldRegionIDs;
            this.newRegionIDs = newRegionIDs;
            this.oldCountryIDs = oldCountryIDs;
            this.newCountryIDs = newCountryIDs;
        }
    }
}
