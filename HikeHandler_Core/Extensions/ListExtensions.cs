using System;
using System.Collections.Generic;

namespace HikeHandler.Extensions
{
    public static class ListExtensions
    {
        public static string ToIntString(this List<int> list)
        {
            string result = string.Empty;
            foreach(int item in list)
            {
                if (item < 0)
                {
                    throw new ArgumentException("Negative integers are not allowed.");
                }
                result += "." + item.ToString() + ".";
            }
            return result;
        }

        public static string ToCommaSeparatedList(this List<string> list)
        {
            string result = string.Empty;
            foreach (string item in list)
            {
                if (result != string.Empty)
                {
                    result += ", ";
                }
                result += item;
            }
            return result;
        }
    }
}
