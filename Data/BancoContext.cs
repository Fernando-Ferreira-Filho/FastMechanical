using FastMechanical.Models;
using FastMechanical.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FastMechanical.Data {
    public class BancoContext : DbContext {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Person> Person { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Person>()
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
