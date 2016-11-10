using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class CPForUpdate
    {
        public int CPID { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string Description { get; set; }
        public CPType TypeOfCP { get; set; }

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
