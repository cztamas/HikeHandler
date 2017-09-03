using Newtonsoft.Json;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public struct HikeForSearch
    {
        public int? countryID, regionID, hikeType;
        public IntPile position;
        public string description, countryName, regionName;
        public List<int> cpList;
        public bool anyCPOrder;

        [JsonConstructor]
        public HikeForSearch(string description, string countryName, string regionName, IntPile position,
            List<int> cpList, bool anyCPOrder, int? countryID = null, int? regionID = null, int? hikeType = null)
        {
            this.description = description;
            this.countryName = countryName;
            this.regionName = regionName;
            this.hikeType = hikeType;
            this.countryID = countryID;
            this.regionID = regionID;
            this.position = position;
            this.cpList = cpList;
            this.anyCPOrder = anyCPOrder;
        }
    }
}
