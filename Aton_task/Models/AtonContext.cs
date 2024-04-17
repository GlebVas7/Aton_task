using Microsoft.EntityFrameworkCore;

namespace Aton_task.Models
{
    public class AtonContext: DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=AtonDB;Trusted_Connection=True;");
        }
    }
}
