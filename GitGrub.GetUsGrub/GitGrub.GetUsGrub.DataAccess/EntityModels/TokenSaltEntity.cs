using GitGrub.GetUsGrub.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// A duplicate token salt entity to abstract the ORM framework.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 2/21/18
    /// </summary>
    [Table("GetUsGrub.TokenSalt")]
    public class TokenSaltEntity : ISalt
    { 
        [Key]
        public int Id { get; set; }

        [ForeignKey("Token")]
        public int TokenId { get; set; }

        public string Salt { get; set; }

        // NAVIGATION
        public TokenEntity Token { get; set; }
    }
}

