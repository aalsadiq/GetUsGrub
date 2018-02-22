namespace GitGrub.GetUsGrub.Models.Interfaces
{
    /// <summary>
    /// Interface representing restaurant details
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    interface IRestaurantDetails
    {
        string Street { get; set; }
        string City { get; set; }
        string State { get; set; }
        int ZipCode { get; set; }
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
