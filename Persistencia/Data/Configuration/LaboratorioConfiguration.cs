using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class LaboratorioConfiguration : IEntityTypeConfiguration<Laboratorio>
{
    public void Configure(EntityTypeBuilder<Laboratorio> builder)
    {
        builder.ToTable("laboratorio");

        builder.Property(e => e.Nombre)
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(e => e.Direccion)
        .HasMaxLength(250)
        .IsRequired();

        builder.Property(e => e.Telefono)
        .HasMaxLength(15)
        .IsRequired();
    }
}