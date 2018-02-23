using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Interface representing additional restaurant information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/22/18
    /// </summary>
    public interface IRestaurantDetail
    {
        string Category { get; set; }

        IEnumerable<BusinessHours> BusinessSchedule { get; set; }

        bool? HasReservations { get; set; }

        bool? HasDelivery { get; set; }

        bool? HasTakeOut { get; set; }

        bool? AcceptCreditCards { get; set; }

        string Attire { get; set; }

        bool? ServesAlcohol { get; set; }

        bool? HasOutdoorSeating { get; set; }

        bool? HasTv { get; set; }

        bool? HasDriveThru { get; set; }

        bool? Caters { get; set; }

        bool? AllowsPets { get; set; }
    }
}
