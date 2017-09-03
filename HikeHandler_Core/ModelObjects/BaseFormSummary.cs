namespace HikeHandler.ModelObjects
{
    public class BaseFormSummary
    {
        public int HikeCount { get; set; }
        public int RegionCount { get; set; }
        public int CPCount { get; set; }
        public int CountryCount { get; set; }

        public BaseFormSummary(int countryCount, int regionCount, int cpCount, int hikeCount)
        {
            HikeCount = hikeCount;
            RegionCount = regionCount;
            CPCount = cpCount;
            CountryCount = countryCount;
        }
    }
}
