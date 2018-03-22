using System.Data.Entity;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context containing all tables for user management.
    /// 
    /// @Created by: Brian Fann, Rachel Dang
    /// @Created: 3/9/18
    /// @Last Updated: 03/21/2018
    /// </summary>
    public class AuthorizationContext : DbContext
    {
        public DbSet<UserClaims> Claims { get; set; }

        public AuthorizationContext() : base("GetUsGrub") { }
    }

}