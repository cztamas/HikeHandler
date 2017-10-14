using Newtonsoft.Json;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class HikeRegionForSave
    {
        public List<int> countryIDs;
        public string name, description;

        [JsonConstructor]
        public HikeRegionForSave(List<int> countryIDs, string name, string description)
        {
            this.countryIDs = countryIDs;
            this.name = name;
            this.description = description;
        }
    }
}
