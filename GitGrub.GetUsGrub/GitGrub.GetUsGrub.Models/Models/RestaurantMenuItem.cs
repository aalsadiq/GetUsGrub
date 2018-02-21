using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant menu item class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    public class RestaurantMenuItem
    {
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string ItemPicture { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}