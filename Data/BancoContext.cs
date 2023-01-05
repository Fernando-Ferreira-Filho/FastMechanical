using FastMechanical.Models;
using FastMechanical.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FastMechanical.Data {
    public class BancoContext : DbContext {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Servicos> Servicos { get; set; }
        public DbSet<Materiais> Materiais { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Agenda> Agenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Pessoa>()
                .HasIndex(p => p.Cpf)
                .IsUnique(true);
            modelBuilder.Entity<Veiculo>()
                 .HasIndex(p => p.Renavam)
                 .IsUnique(true);
            modelBuilder.Entity<Veiculo>()
                .HasIndex(p => p.Placa)
                .IsUnique(true);
            modelBuilder.Entity<Materiais>()
                .HasIndex(p => p.Codigo)
                .IsUnique(true);
        }
    }

}
