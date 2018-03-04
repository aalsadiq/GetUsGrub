namespace GitGrub.GetUsGrub.Models
{
    [System.Serializable]
    public class BusinessHour
    {
        // TODO: Validate that it is in military time
        public string Day { get; set; }

        public int OpenTime { get; set; }

        public int CloseTime { get; set; }
    }
}