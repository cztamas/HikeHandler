using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public class CountryForSearch
    {
        public string name, description;
        public IntPile hikeCount, cpCount, regionCount;

        [JsonConstructor]
        public CountryForSearch(string name, string description, IntPile hikeCount, IntPile cpCount, IntPile regionCount)
        {
            this.name = name;
            this.description = description;
            this.hikeCount = hikeCount;
            this.cpCount = cpCount;
            this.regionCount = regionCount;
        }
    }
}
