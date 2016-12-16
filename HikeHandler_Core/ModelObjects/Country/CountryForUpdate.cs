using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public class CountryForUpdate
    {
        public int CountryID { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string Description { get; set; }

        [JsonConstructor]
        public CountryForUpdate(int countryID, string oldName, string newName, string description)
        {
            CountryID = countryID;
            OldName = oldName;
            NewName = newName;
            Description = description;
        }
    }
}
