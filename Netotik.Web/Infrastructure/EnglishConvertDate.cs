using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Netotik.Web.Infrastructure
{
    public static class EnglishConvertDate
    {

        public static string ConvertToFa(string miladiDate,string format)
        {
            var parts = miladiDate.Split('/');
            if (parts.Length != 3) return null;
            int month = GetMonth(parts[0].ToString());
            int day = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);
            var date = new DateTime(year, month, day);
            if (format == "")
            {
                return PersianDate.ConvertDate.ToFa(date);
            }
            return PersianDate.ConvertDate.ToFa(date,format);
        }
        public static DateTime ConvertToEn(string miladiDate)
        {
            var parts = miladiDate.Split(' ')[0].Split('/');
            if (parts.Length != 3) return DateTime.Now;
            int month = GetMonth(parts[0].ToString());
            int day = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);
            int sec = int.Parse(miladiDate.Split(' ')[1].Split(':')[2]);
            int min = int.Parse(miladiDate.Split(' ')[1].Split(':')[1]);
            int hour = int.Parse(miladiDate.Split(' ')[1].Split(':')[0]);
            return new DateTime(year, month, day, hour,min,sec);
        }

        private static int GetMonth(string monthName)
        {
            switch (monthName)
            {
                case "jan":
                    return 1;
                case "feb":
                    return 2;
                case "mar":
                    return 3;
                case "apr":
                    return 4;
                case "may":
                    return 5;
                case "jun":
                    return 6;
                case "jul":
                    return 7;
                case "aug":
                    return 8;
                case "sep":
                    return 9;
                case "oct":
                    return 10;
                case "nov":
                    return 11;
                case "dec":
                    return 12;

            }
            return 0;
        }
    }
}