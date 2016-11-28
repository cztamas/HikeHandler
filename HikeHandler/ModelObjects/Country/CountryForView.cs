using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public class CountryForView
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public int HikeCount { get; set; }
        public int RegionCount { get; set; }
        public int CPCount { get; set; }
        public string Description { get; set; }
        
        public CountryForView(int countryID)
        {
            CountryID = countryID;
        }

        [JsonConstructor]
        public CountryForView(int countryID, string name, int hikeCount, int regionCount, int cpCount, string description)
        {
            CountryID = countryID;
            HikeCount = hikeCount;
            RegionCount = regionCount;
            CPCount = cpCount;
            Name = name;
            Description = description;
        }
    }
}
