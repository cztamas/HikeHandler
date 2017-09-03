using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public struct CountryForSearch
    {
        public string name;
        public IntPile hikeCount, cpCount, regionCount;

        [JsonConstructor]
        public CountryForSearch(string name, IntPile hikeCount, IntPile cpCount, IntPile regionCount)
        {
            this.name = name;
            this.hikeCount = hikeCount;
            this.cpCount = cpCount;
            this.regionCount = regionCount;
        }
    }
}
