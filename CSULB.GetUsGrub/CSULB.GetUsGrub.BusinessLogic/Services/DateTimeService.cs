using System;

namespace CSULB.GetUsGrub.BusinessLogic
{
    // TODO: @Jenn Comment this yo [-Jenn]
    public class DateTimeService
    {
        public DateTime ConvertTimeToDateTimeUnspecifiedKind(string time)
        {
            string arbitraryDate = "2018-05-17";
            DateTime dateTimeUnspecifiedKind = DateTime.Parse($"{arbitraryDate} {time}");
            return dateTimeUnspecifiedKind;
        }

        public DateTime ConvertLocalMeanTimeToCoordinateUniversalTime(DateTime dateTime, string timeZone)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, tz);
            return utcDateTime;
        }

        public DateTime GetCurrentCoordinateUniversalTime()
        {
            return DateTime.UtcNow;
        }

        public DayOfWeek GetCurrentLocalDayOfWeekFromUtc(int offsetInSeconds, DateTime utcDateTime)
        {
            var localDateTime = utcDateTime.AddSeconds(offsetInSeconds);
            return localDateTime.DayOfWeek;
        }
    }
}
