namespace CSULB.GetUsGrub.Models
{
    public interface IBusinessHour
    {
        string Day { get; }

        string OpenTime { get; }

        string CloseTime { get; }
    }
}
