using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public class CountryForView
    {
        public int countryID, hikeCount, regionCount, cpCount;
        public string name, description;

        [JsonConstructor]
        public CountryForView(int countryID, string name, int hikeCount, int regionCount, int cpCount, string description)
        {
            this.countryID = countryID;
            this.hikeCount = hikeCount;
            this.regionCount = regionCount;
            this.cpCount = cpCount;
            this.name = name;
            this.description = description;
        }
    }
}
