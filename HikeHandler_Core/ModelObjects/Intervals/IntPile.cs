using System;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class IntPile : List<IntInterval>
    {
        public IntPile() : base()
        { }

        public string SqlSearchCondition(string variable)
        {
            string condition = String.Empty;
            foreach (IntInterval interval in this)
            {
                if (condition != String.Empty)
                    condition += " OR ";
                condition += interval.SqlSnippet(variable);
            }
            if (condition == String.Empty)
                return String.Empty;
            return "( " + condition + " )";
        }
    }

}
