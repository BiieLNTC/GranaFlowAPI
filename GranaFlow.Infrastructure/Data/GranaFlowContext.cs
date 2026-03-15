using GranaFlow.Domain.Entities;
using GranaFlow.Infrastructure.Data.ContextConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Infrastructure.Data
{
    public class GranaFlowContext : DbContext
    {
        public GranaFlowContext(DbContextOptions<GranaFlowContext> options) : base(options)
        {
        }
        
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigCategoria());
            modelBuilder.ApplyConfiguration(new ConfigPessoa());
            modelBuilder.ApplyConfiguration(new ConfigTransacao());
        }
    }
}
