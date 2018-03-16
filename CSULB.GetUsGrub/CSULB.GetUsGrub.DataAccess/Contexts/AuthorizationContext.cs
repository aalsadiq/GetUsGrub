using System.Data.Entity;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context containing all tables for user management.
    /// 
    /// @Created by: Brian Fann
    /// @Last Updated: 3/9/18
    /// </summary>

    public class AuthorizationContext : DbContext
    {
        // TODO Add DbSets once claims are implemented.
        DbSet<UserClaims> UserClaims { get; set; }

        public AuthorizationContext() : base("GetUsGrub") { }
    }

}
