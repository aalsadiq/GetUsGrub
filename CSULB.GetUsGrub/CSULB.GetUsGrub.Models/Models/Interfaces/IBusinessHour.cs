namespace CSULB.GetUsGrub.Models
{
    public interface IBusinessHour
    {
        string Day { get; set; }

        string OpenTime { get; set; }

        string CloseTime { get; set; }
    }
}
