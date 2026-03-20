using GranaFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Infrastructure.Data.ContextConfig
{
    public class ConfigUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(h => h.Id);

            builder.HasIndex(h => h.Email).IsUnique();

            builder.Property(c => c.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.SenhaHash)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(c => c.CadastradoEm)
                .IsRequired();
        }
    }
}
