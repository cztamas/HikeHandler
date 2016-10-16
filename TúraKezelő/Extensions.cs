using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HikeHandler
{
    public static class Extensions
    {
        public static bool IsCPString(this string text)
        {            
            return Regex.IsMatch(text, @"^(\.\d+\.)*$");
        }

         public static bool IsIntPile(this string text)
        {
            text = Regex.Replace(text, @"\s+", "");
            return Regex.IsMatch(text, @"^(\d*-?\d*,+)*(\d*-?\d*)?$");
        }

        public static bool IsIntInterval(this string text)
        {
            text = Regex.Replace(text, @"\s+", "");
            return Regex.IsMatch(text, @"^\d*-?\d*$");
        }

        public static IntInterval ToIntInterval(this string text)
        {
            if (!text.IsIntInterval())
                return null;
            text = Regex.Replace(text, @"\s+", "");
            char[] separator = new char[] { '-' };
            string[] limits = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (text == "-")
                return null;
            if (Regex.IsMatch(text, @"^\d*$"))
            {
                int a;
                int.TryParse(limits[0], out a);
                IntInterval interval = new IntInterval(a, a);
                return interval;
            }
            if (Regex.IsMatch(text, @"^\d*-$"))
            {
                int a;
                int.TryParse(limits[0], out a);
                IntInterval interval = new IntInterval(a, 0);
                return interval;
            }
            if (Regex.IsMatch(text, @"^-\d*$"))
            {
                int a;
                int.TryParse(limits[0], out a);
                IntInterval interval = new IntInterval(0, a);
                return interval;
            }
            if (Regex.IsMatch(text, @"^\d*-\d*$"))
            {
                int a, b;
                int.TryParse(limits[0], out a);
                int.TryParse(limits[1], out b);
                IntInterval interval = new IntInterval(a, b);
                return interval;
            }            
            return null;
        }

        public static IntPile ToIntPile(this string text)
        {
            if (!text.IsIntPile())
                return null;
            IntPile pile = new IntPile();
            text = Regex.Replace(text, @"\s+", "");
            char[] separator = new char[] { ',' };
            string[] intervalStrings = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in intervalStrings)
            {
                IntInterval interval = item.ToIntInterval();
                if (interval != null)
                    pile.Add(item.ToIntInterval());
            }
            return pile;
        }

        public static bool IsDateInterval(this string text)
        {
            text = Regex.Replace(text, @"\s+", string.Empty);
            text = Regex.Replace(text, @"\/", @"\.");
            if (Regex.IsMatch(text, @"^(\d{4}(\.\d{1,2})?(\.\d{1,2})?)?-?(\d{4}(\.\d{1,2})?(\.\d{1,2})?)?$") == false)
                return false;
            char[] separator = new char[] { '-' };
            string[] dates = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            DateTime dateTime;
            foreach (string date in dates)
            {
                if (DateTime.TryParse(date, out dateTime) == false)
                    return false;
            }
            return true;
        }

        public static bool IsDatePile(this string text)
        {
            text = Regex.Replace(text, @"\s+", string.Empty);
            text = Regex.Replace(text, @"\/", @"\.");
            char[] separator = new char[] { ',' };
            string[] intervals = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string interval in intervals)
            {
                if (interval.IsDateInterval() == false)
                    return false;
            }
            return true;
        }

        public static DateInterval ToDateInterval(this string text)
        {
            text = Regex.Replace(text, @"\s+", string.Empty);
            text = Regex.Replace(text, @"\/", @"\.");
            if (text.IsDateInterval() == false)
                return null;
            char[] separator = new char[] { '-' };
            string[] dates = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (dates.Count() == 0)
                return null;
            if (dates.Count() == 1)
            {
                DateTime date;
                DateInterval interval = null;
                DateTime.TryParse(dates[0], out date);
                if (Regex.IsMatch(text, @"-$"))
                    interval = new DateInterval(date, false);
                if (Regex.IsMatch(text, @"^-"))
                    interval = new DateInterval(date, true);
                return interval;
            }
            if (dates.Count() == 2)
            {
                DateTime date1, date2;
                DateTime.TryParse(dates[0], out date1);
                DateTime.TryParse(dates[1], out date2);
                DateInterval interval = new DateInterval(date1, date2);
                return interval;
            }
            return null;
        }

        public static DatePile ToDatePile(this string text)
        {
            if (!text.IsDatePile())
                return null;
            text = Regex.Replace(text, @"\s+", string.Empty);
            text = Regex.Replace(text, @"\/", @"\.");
            char[] separator = new char[] { ',' };
            string[] intervals = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            DatePile pile = new DatePile();
            foreach (string item in intervals)
            {
                DateInterval interval = item.ToDateInterval();
                if (interval != null)
                    pile.Add(interval);
            }
            return pile;
        }
    }
}
