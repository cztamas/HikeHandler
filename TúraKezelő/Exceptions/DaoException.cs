using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.Exceptions
{
    public class DaoException : Exception
    {
        public ActivityType Activity { get; }
        public ErrorType Error { get; }

        public DaoException(ActivityType activityType, ErrorType errorType, string message) : base(message)
        {
            Activity = activityType;
            Error = errorType;
        }
    }
}
