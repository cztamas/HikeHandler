using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.Exceptions
{
    public enum ActivityType
    {
        Save, Update, Search, Delete, GetData, UpdateHikeCount, CountCPs, CheckDuplicateDate,
        CountRegions, CheckDuplicateName, UpdateHikePositions, MoveHikePositions
    }

    public enum ErrorType
    {
        DuplicateName, DuplicateDate, InvalidArgument, DBError, NoDBConnection, NotDeletable
    }
}
