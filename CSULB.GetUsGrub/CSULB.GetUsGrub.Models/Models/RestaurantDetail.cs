namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Comment these [-Jenn]
    public enum RestaurantFoodTypes
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
        // Automatic Properties
        public RestaurantDetail() { }
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
        public string FoodType { get; set; }

        // Constructors
        public RestaurantDetail(
            int avgFoodPrice, bool? hasReservations,
            bool? hasDelivery, bool? hasTakeOut,
            bool? acceptCreditCards, string attire,
            bool? servesAlcohol, bool? hasOutdoorSeating,
            bool? hasTv, bool? hasDriveThru, bool? caters,
            bool? allowsPets, string foodType)
        {
            FoodType = foodType;
            AvgFoodPrice = avgFoodPrice;
            HasReservations = hasReservations;
            HasDelivery = hasDelivery;
            HasTakeOut = hasTakeOut;
            AcceptCreditCards = acceptCreditCards;
            Attire = attire;
            ServesAlcohol = servesAlcohol;
            HasOutdoorSeating = hasOutdoorSeating;
            HasTv = hasTv;
            HasDriveThru = hasDriveThru;
            Caters = caters;
            AllowsPets = allowsPets;
        }
    }
}
