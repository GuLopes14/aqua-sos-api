using Microsoft.EntityFrameworkCore;
using AquaSOS.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AquaSOS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PontoDistribuicao> PontosDistribuicao { get; set; }
        public DbSet<PedidoAgua> PedidosAgua { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PedidoAgua>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<PedidoAgua>()
                .HasOne(p => p.PontoDistribuicao)
                .WithMany()
                .HasForeignKey(p => p.PontoDistribuicaoId);
        }
    }
}
