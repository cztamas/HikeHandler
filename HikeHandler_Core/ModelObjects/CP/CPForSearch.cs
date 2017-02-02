namespace HikeHandler.ModelObjects
{
    public class CPForSearch
    {
        public int? CPID { get; set; }
        public int? IDCountry { get; set; }
        public int? IDRegion { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public CPType? TypeOfCP { get; set; }
        public string Description { get; set; }
        public IntPile HikeCount { get; set; }

        public CPForSearch()
        { }
          
    }
}
