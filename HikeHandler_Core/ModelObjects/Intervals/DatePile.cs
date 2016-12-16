using System;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class DatePile : List<DateInterval>
    {
        private List<int> months;
        private List<int> weekdays;

        public DatePile() : base()
        {
            months = new List<int>();
            weekdays = new List<int>();
        }
        
        public string SqlSearchCondition(string variable)
        {
            int conditionCount = 0;
            string condition = string.Empty;
            foreach (DateInterval interval in this)
            {
                if (interval.SqlSnippet(variable) != string.Empty)
                {
                    if (condition != String.Empty)
                        condition += " OR ";
                    condition += interval.SqlSnippet(variable);
                    conditionCount++;
                }
            }
            if (condition == string.Empty)
                return string.Empty;
            if (conditionCount == 1)
                return condition;
            return "(" + condition + ")";
        }
    }

}
