using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models
{
    public class RestaurantMenuItem
    {
        public string Id { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string ItemPicture { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}