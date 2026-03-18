using GranaFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Infrastructure.Data.ContextConfig
{
    public class ConfigCategoria : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(c => c.Cor)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.Finalidade)
                .IsRequired();

            builder.Property(c => c.CadastradoEm)
                .IsRequired();

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
