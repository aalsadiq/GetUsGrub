using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GitGrub.GetUsGrub.Models.Interfaces;

namespace GitGrub.GetUsGrub.Models.Models
{
    public class TokenSalt : ISalt
    {
        

        [Key]
        public int Id { get; set; }

        [ForeignKey("Tokens")]
        public int TokenId { get; set; }
        public string Salt { get; set; }
    }
}
