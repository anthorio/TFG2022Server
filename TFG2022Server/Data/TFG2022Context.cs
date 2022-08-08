using Microsoft.EntityFrameworkCore;
using TFG2022Server.Entities;

namespace TFG2022Server.Data
{
    public class TFG2022Context : DbContext
    {
        public TFG2022Context(DbContextOptions<TFG2022Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData.AddUsuarioData(modelBuilder);
        }

        public DbSet<Usuario> Usuarios { get; set; }// = null!;
        public DbSet<Cliente> Clientes { get; set; }// = null!;
    }
}
