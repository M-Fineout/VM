using System;

namespace ExtensionMethods.Contracts
{
    public static class DateExtensions
    {
        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Formats date to UNIX timestamp in seconds
        /// </summary>
        /// <param name="dateTime">Date</param>
        /// <returns>Timestamp</returns>
        public static long ToUnixSeconds(this DateTime dateTime)
        {
            var totalSeconds = (dateTime.ToUniversalTime() - EPOCH).TotalSeconds;
            return (long) Math.Round(totalSeconds, 0);
        }
    }
}
