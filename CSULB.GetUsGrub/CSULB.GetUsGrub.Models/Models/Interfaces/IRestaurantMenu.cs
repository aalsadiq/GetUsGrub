using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant menu interface
    /// @author: Andrew Kao
    /// @updated: 3/20/18
    /// </summary>
    public interface IRestaurantMenu
    {
        int PublicMenuId { get; set; }

        string MenuName { get; set; }

        bool IsActive { get; set; }
    }
}
