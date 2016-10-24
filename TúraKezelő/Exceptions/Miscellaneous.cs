using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.Exceptions
{
    public enum ActivityType
    {
        Save, Update, Search, Delete, GetData, UpdateHikeCount, CountCPs, CountRegions, CheckDuplicateName
    }

    public enum ErrorType
    {
        DuplicateName, InvalidArgument, DBError, NoDBConnection, NotDeletable
    }
}
