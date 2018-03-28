namespace CSULB.GetUsGrub.Models
{
    public interface IBusinessHour
    {
        int PublicHourId { get; set; }

        string Day { get; set; }

        string OpenTime { get; }

        string CloseTime { get; set; }
    }
}
