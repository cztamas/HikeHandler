namespace HikeHandler.ModelObjects
{
    public class NameAndID
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public NameAndID(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}
