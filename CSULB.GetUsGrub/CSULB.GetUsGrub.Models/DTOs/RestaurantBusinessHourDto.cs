using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models
{
    public class RestaurantBusinessHourDto : IBusinessHourDto
    {
        public int? Id { get; set; }
        public string Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string TwelveHourFormatOpenTime { get; set; }
        public string TwelveHourFormatCloseTime { get; set; }
        public string TimeZone { get; set; }
        public Flag Flag { get; set; }
        public DateTime OpenDateTime { get; set; }
        public DateTime CloseDateTime { get; set; }

        public RestaurantBusinessHourDto() { }

        public RestaurantBusinessHourDto(int? id, string day, string openTime, string closeTime)
        {
            Id = id;
            Day = day;
            OpenTime = openTime;
            CloseTime = closeTime;
        }

        public RestaurantBusinessHourDto(int? id, string day, DateTime openDateTime, DateTime closeDateTime)
        {
            Id = id;
            Day = day;
            OpenDateTime = openDateTime;
            CloseDateTime = closeDateTime;
        }

        public RestaurantBusinessHourDto(int? id, string day, DateTime openDateTime, DateTime closeDateTime, string timeZone)
        {
            Id = id;
            Day = day;
            OpenDateTime = openDateTime;
            CloseDateTime = closeDateTime;
            TimeZone = timeZone;
        }
    }
}
