using Newtonsoft.Json;

namespace CSULB.GetUsGrub.Models
{
    [JsonObject]
    public class RestaurantMenusDto
    {
        public RestaurantMenuDto[] Menus;
    }

    [JsonObject]
    public class RestaurantMenuDto
    {
        public string MenuName { get; set; }

        public RestaurantMenuItemDto[] Items { get; set; }
    }


    [JsonObject]
    public class RestaurantMenuItemDto
    {
        [JsonProperty]
        public string ItemName { get; set; }

        [JsonProperty]
        public decimal ItemPrice { get; set; }

        [JsonProperty]
        public string ItemPicture { get; set; }

        [JsonProperty]
        public string Tag { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public bool IsActive { get; set; }
    }
}
