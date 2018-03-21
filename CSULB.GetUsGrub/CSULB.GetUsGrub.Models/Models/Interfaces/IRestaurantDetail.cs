using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Additional restraurant information interface
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/20/18
    /// @lastchange: Removed Category
    /// </summary>
    public interface IRestaurantDetail
    {
        int AvgFoodPrice { get; set; }

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

        string Category { get; set; }
    }
}