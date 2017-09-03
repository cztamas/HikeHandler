using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public struct CPForSearch
    {
        public int? countryID, regionID, cpType;
        public string name, description, countryName, regionName;
        public IntPile hikeCount;

        [JsonConstructor]
        public CPForSearch(string name, string description, string countryName, string regionName, IntPile hikeCount,
            int? countryID = null, int? regionID = null, int? cpType = null)
        {
            this.name = name;
            this.description = description;
            this.countryName = countryName;
            this.regionName = regionName;
            this.hikeCount = hikeCount;
            this.cpType = cpType;
            this.countryID = countryID;
            this.regionID = regionID;
        }          
    }
}
