using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    public interface IUserAccount
    {
        int Id { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string AccountType { get; set; }
        bool IsActive { get; set; }
    }
}
