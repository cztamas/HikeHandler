using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HikeHandler.ModelObjects
{
    public static class StringExtensions
    {
        public static bool IsCPString(this string text)
        {            
            return Regex.IsMatch(text, @"^(\.\d+\.)*$");
        }

        public static List<int> ToCPList(this string text)
        {
            if (!IsCPString(text))
            {
                throw new ArgumentException("Invalid cpstring.", "text");
            }
            char[] separator = new char[] { '.' };
            string[] listItems = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int item;
            List<int> cpList = new List<int>();
            foreach (string piece in listItems)
            {
                if (!int.TryParse(piece, out item))
                    throw new ArgumentException("Invalid cpstring.", "text");
                cpList.Add(item);
            }
            return cpList;
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

        public static string StandardizeDateInterval(this string text)
        {
            text = Regex.Replace(text, @"\s+", string.Empty);
            text = Regex.Replace(text, @"\.$", string.Empty);
            text = Regex.Replace(text, @"\.-", @"-");
            text = Regex.Replace(text, @"\/", @"\.");
            text = Regex.Replace(text, @"^(\d{4})$", @"$1.1.1-$1.12.31");
            text = Regex.Replace(text, @"(\d{4})-", @"$1.1.1-");
            text = Regex.Replace(text, @"-(\d{4})$", @"-$1.12.31");
            text = Regex.Replace(text, @"(\d{4}\.\d{1,2})-", @"$1.1-");
            text = Regex.Replace(text, @"-(\d{4}\.\d{1,2})$", @"-$1.e");
            text = Regex.Replace(text, @"^(\d{4}\.\d{1,2})$", @"$1.1-$1.e");
            //MessageBox.Show(text);
            return text;
        }

        public static bool IsDateInterval(this string text)
        {
            text = text.StandardizeDateInterval();
            if (!Regex.IsMatch(text, @"^(\d{4}(\.\d{1,2})?(\.\d{1,2})?)?-?(\d{4}\.\d{1,2}((\.\d{1,2})|(\.e)))?$"))
                return false;
            char[] separator = new char[] { '-' };
            string[] dates = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            DateTime dateTime;
            foreach (string date in dates)
            {
                string newDate = Regex.Replace(date, @"e", @"28");
                if (DateTime.TryParse(newDate, out dateTime) == false)
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
            text = text.StandardizeDateInterval();
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
                if (!DateTime.TryParse(dates[0], out date))
                {
                    if (!Regex.IsMatch(dates[0], @"^\d{4}\.\d{1,2}\.e$"))
                        return null;
                    char[] separatorDot = new char[] { '.' };
                    string[] datePieces = dates[0].Split(separatorDot);
                    int year, month, day;
                    int.TryParse(datePieces[0], out year);
                    int.TryParse(datePieces[1], out month);
                    day = DateTime.DaysInMonth(year, month);
                    date = new DateTime(year, month, day);
                }
                if (Regex.IsMatch(text, @"-$"))
                    interval = new DateInterval(begin: date);
                if (Regex.IsMatch(text, @"^-"))
                    interval = new DateInterval(end: date);
                if (!Regex.IsMatch(text, @"-"))
                    interval = new DateInterval(date, date);
                return interval;
            }
            if (dates.Count() == 2)
            {
                DateTime date1, date2;
                DateTime.TryParse(dates[0], out date1);
                if (Regex.IsMatch(dates[1], @"^\d{4}\.\d{1,2}\.e$"))
                {
                    char[] separatorDot = new char[] { '.' };
                    string[] datePieces = dates[1].Split(separatorDot);
                    int year, month, day;
                    int.TryParse(datePieces[0], out year);
                    int.TryParse(datePieces[1], out month);
                    day = DateTime.DaysInMonth(year, month);
                    date2 = new DateTime(year, month, day);
                }
                else
                {
                    if (!DateTime.TryParse(dates[1], out date2))
                        return null;
                }
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
