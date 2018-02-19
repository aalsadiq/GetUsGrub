namespace GitGrub.GetUsGrub.Models.Models
{
    public class RestaurantMenuItem
    {
        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public string ItemPicture { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        // Constructor
        public RestaurantMenuItem(string itemName, double itemPrice, string itemPicture, string tag, string description, bool active)
        {
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPicture = itemPicture;
            Tag = tag;
            Description = description;
            Active = active;
        }
    }
}