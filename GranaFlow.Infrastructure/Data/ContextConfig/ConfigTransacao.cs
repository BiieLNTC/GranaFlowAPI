using GranaFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Infrastructure.Data.ContextConfig
{
    public class ConfigTransacao : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(c => c.DataTransacao)
                .IsRequired();

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(c => c.Tipo)
                .IsRequired();

            builder.Property(c => c.Valor)
                .IsRequired()
                .HasPrecision(20, 4);

            builder.Property(c => c.CadastradoEm)
                .IsRequired();

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(h => h.Categoria)
                .WithMany()
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Pessoa)
                .WithMany()
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
