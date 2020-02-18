namespace System
{
    public static class DateExtensions
    {
        /// <summary>
        /// Formats date to standard string
        /// </summary>
        /// <param name="dateTime">Date</param>
        /// <returns>Formatted string</returns>
        public static string Format(this DateTime dateTime)
        {
            return dateTime.Year > 1930 ? 
                   dateTime.ToString("1yyMMdd") :
                   dateTime.ToString("0yyMMdd");
        }
    }
}
