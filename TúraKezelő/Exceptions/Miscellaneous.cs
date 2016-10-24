using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.Exceptions
{
    public enum ActivityType
    {
        Save, Update, Search, Delete, UpdateHikeCount, CountCPs, CountRegions, CheckDuplicateName
    }

    public enum ErrorType
    {
        DuplicateName, DBError, NoDBConnection, NotDeletable
    }
}
