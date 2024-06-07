using PeopleHubAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace PeopleHubAPI.InfraestruturaBD
{
    public class ConnectionContext : DbContext
    {
        public DbSet<People> Peoples { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "Server=localhost;" +
            "Port=5432; Database=postgres;" +
            "User ID=postgres;" +
            "Password=igormiranda8;");
    }
}