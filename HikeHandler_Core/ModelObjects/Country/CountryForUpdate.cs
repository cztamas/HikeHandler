using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public struct CountryForUpdate
    {
        public int countryID;
        public string newName, oldName, description;

        [JsonConstructor]
        public CountryForUpdate(int countryID, string newName, string oldName, string description)
        {
            this.countryID = countryID;
            this.newName = newName;
            this.oldName = oldName;
            this.description = description;
        }
    }
}
