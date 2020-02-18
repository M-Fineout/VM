using System;

namespace ExternalMethods.Shared
{
    public class DateExtensions()
    {
        /// <summary>
        /// Formats date to standard string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string Format(this DateTime dateTime)
        {
            return dateTime.Year > 1930 ?
                   dateTime.ToString("1yyMMdd") :
                   dateTime.ToString("0yyMMdd");
        }
    }
}
