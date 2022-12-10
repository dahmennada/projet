using Microsoft.EntityFrameworkCore;
using WebService.Data.Model;

namespace WebService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Congé> Congés { get; set; }
   

    }
}
