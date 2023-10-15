using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class EspecieConfiguration : IEntityTypeConfiguration<Especie>
{
    public void Configure(EntityTypeBuilder<Especie> builder)
    {
        builder.ToTable("Especie");

        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(30);
    }
}
