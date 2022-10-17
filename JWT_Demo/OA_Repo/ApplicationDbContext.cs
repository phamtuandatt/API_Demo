using Microsoft.EntityFrameworkCore;
using OA_Data;
using System.Runtime.CompilerServices;

namespace OA_Repo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserMap(modelBuilder.Entity<Users>());
        }
    }
}