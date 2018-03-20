using CSULB.GetUsGrub.Models;
using System.Data.Entity;

namespace CSULB.GetUsGrub.DataAccess
{
    public class SsoContext : DbContext
    {
        public DbSet<SsoToken> SsoTokens { get; set; }
        public SsoContext() : base("GetUsGrub") { }
    }
}
