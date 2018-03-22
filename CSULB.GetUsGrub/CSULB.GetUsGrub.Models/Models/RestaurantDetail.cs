namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Ask what are best ways to label the food price ranges [-Jenn]
    public enum AvgFoodPrices
    {
        ZeroToTen = 0,
        TenToFifty = 1,
        FiftyPlus = 2
    }
    // TODO: @Jenn Comment these [-Jenn]
    public enum RestaurantCategories
    {
        MexicanFood = 0,
        ItalianCuisine = 1,
        ThaiFood = 2,
        GreekCuisine = 3,
        ChineseFood = 4,
        JapaneseCuisine = 5,
        AmericanFood = 6,
        MediterraneanCuisine = 7,
        FrenchFood = 8,
        SpanishCuisine = 9,
        GermanFood = 10,
        KoreanFood = 11,
        VietnameseFood = 12,
        TurkishCuisine = 13,
        CaribbeanFood = 14
    }

    public class RestaurantDetail : IRestaurantDetail
    {
        public int AvgFoodPrice { get; set; }

        public bool? HasReservations { get; set; }

        public bool? HasDelivery { get; set; }

        public bool? HasTakeOut { get; set; }

        public bool? AcceptCreditCards { get; set; }

        public string Attire { get; set; }

        public bool? ServesAlcohol { get; set; }

        public bool? HasOutdoorSeating { get; set; }

        public bool? HasTv { get; set; }

        public bool? HasDriveThru { get; set; }

        public bool? Caters { get; set; }

        public bool? AllowsPets { get; set; }

        public string Category { get; set; }
    }
}
