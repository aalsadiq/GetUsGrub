using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Business hour class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>
    public class BusinessHour : IBusinessHour
    {
        public string Day { get; set; }

        public int OpenTime { get; set; }

        public int CloseTime { get; set; }

        public string TimeZone { get; set; }
    }
}
