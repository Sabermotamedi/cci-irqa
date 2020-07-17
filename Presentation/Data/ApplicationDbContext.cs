using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using System.Linq;


namespace Presentation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
     

        public DbSet<Company> Companies { get; set; }
        public DbSet<Organization> Organizations { get; set; }
       // public DbSet<Category> Categories { get; set; }
    }
}
