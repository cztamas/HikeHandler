using Newtonsoft.Json;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public struct CPForSave
    {
        public List<int> countryIDs, regionIDs;
        public string name, description;
        public int cpType;

        [JsonConstructor]
        public CPForSave(List<int> countryIDs, List<int> regionIDs, string name, string description, int cpType)
        {
            this.countryIDs = countryIDs;
            this.regionIDs = regionIDs;
            this.name = name;
            this.description = description;
            this.cpType = cpType;
        }
    }
}
