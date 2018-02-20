using GitGrub.GetUsGrub.Models.Interfaces;
using GitGrub.GetUsGrub.Models.Models;
using GitGrub.GetUsGrub.Models.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    public class RegisterUserWithSecurityDto : IUserAccount, ISecurityQuestionsList
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }

        [ExactListLengthValidation(3, ErrorMessage = "Must answer 3 security questions.")]
        public IEnumerable<SecurityQuestion> SecurityQuestionsList { get; set; }
    }
}
