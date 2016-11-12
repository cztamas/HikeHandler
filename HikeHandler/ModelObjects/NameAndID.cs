using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public struct NameAndID
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
