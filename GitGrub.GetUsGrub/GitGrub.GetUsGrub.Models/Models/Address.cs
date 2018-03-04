namespace GitGrub.GetUsGrub.Models
{
    // Serializes into a Json
    [System.Serializable]
    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}
