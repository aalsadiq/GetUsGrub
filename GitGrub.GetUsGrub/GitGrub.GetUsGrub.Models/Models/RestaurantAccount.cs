using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models.Models
{
    // TODO: Do we need this table annotation?
    [Table("RestaurantAccount")]
    public class RestaurantAccount : IRestaurantAccount
    {
        // TODO: Do we need this Key?
        [Key]
        public int Id { get; set; }

        // TODO: Do we still need to declare the FK?
        [ForeignKey("UserAccount")]
        public int UserId { get; set; }

        public IEnumerable<BusinessHour> BusinessHours { get; set; }

        public string PhoneNumber { get; set; }

        public Address Address { get; set; }
    }
}
