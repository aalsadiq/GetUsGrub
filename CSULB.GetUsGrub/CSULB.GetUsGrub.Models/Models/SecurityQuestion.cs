namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Add data annotations? [-Jenn]
    /// <summary>
    /// The <c>SecurityQuestion</c> class.
    /// Defines properties pertaining to security question with corresponding answer.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class SecurityQuestion
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int Question { get; set; }
        public string Answer { get; set; }
    }
}
