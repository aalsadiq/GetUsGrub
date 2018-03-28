namespace CSULB.GetUsGrub.Models
{
    public interface IBusinessHour
    {
        string Day { get; set; }

        string OpenTime { get; }

        string CloseTime { get; set; }
    }
}
