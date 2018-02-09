using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitGrub.GetUsGrub.Models
{
    public class BusinessHour
    {
        private string Day { get; set; }

        private int OpenTime { get; set; }

        private int CloseTime { get; set; }

        // Constructor 
        public BusinessHour(string day, int openTime, int closeTime)
        {
            Day = day;
            OpenTime = openTime;
            CloseTime = closeTime;
        }
    }
}