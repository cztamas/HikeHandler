using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public struct HikeRegionForSearch
    {
        public string name, countryName, description;
        public IntPile hikeCount, cpCount;
        public int? countryID;

        [JsonConstructor]
        public HikeRegionForSearch(string name, string description, string countryName, IntPile hikeCount, IntPile cpCount, int? countryID = null)
        {
            this.name = name;
            this.description = description;
            this.countryName = countryName;
            this.hikeCount = hikeCount;
            this.cpCount = cpCount;
            this.countryID = countryID;
        }
    }
}
