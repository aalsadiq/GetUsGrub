using CSULB.GetUsGrub.Models;
using System.Data.Entity;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context containing all tables for user management.
    /// 
    /// @Created by: Brian Fann
    /// @Created: 3/9/18
    /// @Last Updated: 03/21/2018
    /// </summary>
    public class AuthorizationContext : DbContext
    {
        public DbSet<UserClaims> Claims { get; set; }
        public AuthorizationContext() : base("GetUsGrub") { }
    }

}