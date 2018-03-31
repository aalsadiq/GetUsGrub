using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    [Table("GetUsGrub.FailedAttempts")]
    public class FailedAttempts : IEntity
    {
        [Key] [ForeignKey("UserAccount")] public int? Id { get; set; }

        public DateTime LastAttemptTime { get; set; }
        public int Count { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
    }
}
