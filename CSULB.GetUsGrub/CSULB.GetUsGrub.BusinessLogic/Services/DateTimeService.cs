using System;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>DateTimeService</c> class.
    /// Contains methods specific to alterations or creation of a DateTime.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @update: 04/04/2018
    /// </para>
    /// </summary>
    public class DateTimeService
    {
        /// <summary>
        /// The ConvertTimeToDateTimeUnspecifiedKind method.
        /// Creates a DateTime type by concatenating and arbitray date to a given input time.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 04/04/2018
        /// </para>
        /// </summary>
        /// <param name="time"></param>
        /// <returns>DateTime</returns>
        public DateTime ConvertTimeToDateTimeUnspecifiedKind(string time)
        {
            string arbitraryDate = "2018-05-17";
            DateTime dateTimeUnspecifiedKind = DateTime.Parse($"{arbitraryDate} {time}");
            return dateTimeUnspecifiedKind;
        }

        /// <summary>
        /// The ConvertLocalMeanTimeToUtc method.
        /// Creates a DateTime UTC kind given a DateTime unspecified kind and a TimeZone.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 04/04/2018
        /// </para>
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timeZone"></param>
        /// <returns>DateTime</returns>
        public DateTime ConvertLocalMeanTimeToUtc(DateTime dateTime, string timeZone)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, tz);
            return utcDateTime;
        }

        /// <summary>
        /// The GetCurrentUtc() method.
        /// Gets the server's current time as Coordinate Universal Time.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 04/04/2018
        /// </para>
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetCurrentCoordinateUniversalTime()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// The GetCurrentLocalDayOfWeekFromUtc method.
        /// Gets the current local day of week given a DateTime UTC kind.
        /// </summary>
        /// <param name="offsetInSeconds"></param>
        /// <param name="utcDateTime"></param>
        /// <returns>DayOfWeek</returns>
        public DayOfWeek GetCurrentLocalDayOfWeekFromUtc(int offsetInSeconds, DateTime utcDateTime)
        {
            var localDateTime = utcDateTime.AddSeconds(offsetInSeconds);
            return localDateTime.DayOfWeek;
        }
    }
}
