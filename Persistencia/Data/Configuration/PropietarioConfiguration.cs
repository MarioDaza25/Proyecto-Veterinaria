using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class PropietarioConfiguration : IEntityTypeConfiguration<Propietario>
{
    public void Configure(EntityTypeBuilder<Propietario> builder)
    {
        builder.ToTable("Propietario");

        builder.Property(e => e.Nombre)
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(e => e.Correo)
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(e => e.Telefono)
        .HasMaxLength(15)
        .IsRequired();
    }
}