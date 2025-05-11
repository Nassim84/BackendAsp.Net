using Microsoft.EntityFrameworkCore;
using MonBackendAspNet.Models;

namespace MonBackendAspNet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Ajoute tes DbSet ici
        public DbSet<User> Users { get; set; }
    }
}
