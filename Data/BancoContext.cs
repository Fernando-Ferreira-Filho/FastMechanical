using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;

namespace FastMechanical.Data {
    public class BancoContext : DbContext {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Cliente>()
                .HasIndex(p => p.Cpf)
                .IsUnique(true);
            modelBuilder.Entity<Veiculo>()
                 .HasIndex(p => p.Renavam)
                 .IsUnique(true);
            modelBuilder.Entity<Veiculo>()
                .HasIndex(p => p.Placa)
                .IsUnique(true);
        }
    }

}
