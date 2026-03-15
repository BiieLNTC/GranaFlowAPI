using GranaFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Infrastructure.Data.ContextConfig
{
    public class ConfigPessoa : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.DataNascimento)
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
