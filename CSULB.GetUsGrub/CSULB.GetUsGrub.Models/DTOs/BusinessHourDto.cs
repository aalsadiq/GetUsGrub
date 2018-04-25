using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>BusinessHourDto</c> class.
    /// Defines properties pertaining to a data transfer object for a business hour.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    public class BusinessHourDto
    {
        // Automatic Properties
        [Required]
        public string Day { get; set; }
        [Required]
        public string OpenTime { get; set; }
        [Required]
        public string CloseTime { get; set; }
        public string TwelveHourFormatOpenTime { get; set; }
        public string TwelveHourFormatCloseTime { get; set; }
        public Flag Flag { get; set; }
        private DateTime _odt;
        private DateTime _cdt;
        public DateTime OpenDateTime
        {
            get => _odt;
            set
            {
                _odt = DateTime.Today.AddDays((int)Enum.Parse(typeof(DayOfWeek), Day) * 1 - 1) + value.ToLocalTime().TimeOfDay;
                OpenTime = value.ToLocalTime().ToString("HH:mm");
                TwelveHourFormatOpenTime = value.ToLocalTime().ToString("hh:mm tt", CultureInfo.InvariantCulture);
            }
        }
        public DateTime CloseDateTime
        {
            get => _cdt;
            set
            {
                _cdt = DateTime.Today.AddDays((int)Enum.Parse(typeof(DayOfWeek), Day) * 1 - 1) + value.ToLocalTime().TimeOfDay;
                CloseTime = value.ToLocalTime().ToString("HH:mm");
                TwelveHourFormatCloseTime = value.ToLocalTime().ToString("hh:mm tt", CultureInfo.InvariantCulture);
            }
        }

        // Constructors
        public BusinessHourDto () { }

        public BusinessHourDto(string day, DateTime openDateTime, DateTime closeDateTime)
        {
            Day = day;
            OpenDateTime = openDateTime;
            CloseDateTime = closeDateTime;
        }
    }
}
