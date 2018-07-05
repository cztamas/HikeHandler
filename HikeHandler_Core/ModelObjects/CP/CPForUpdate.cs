using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public class CPForUpdate
    {
        public int CPID { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string Description { get; set; }
        public CPType TypeOfCP { get; set; }

        [JsonConstructor]
        public CPForUpdate(int cpID, string oldName, string newName, CPType typeOfCP, string description)
        {
            CPID = cpID;
            OldName = oldName;
            NewName = newName;
            TypeOfCP = typeOfCP;
            Description = description;
        }
    }
}
