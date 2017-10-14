using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public class CountryForSave
    {
        public string name, description;

        [JsonConstructor]
        public CountryForSave(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}
