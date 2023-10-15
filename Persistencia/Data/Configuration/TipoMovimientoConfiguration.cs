using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class TipoMovimientoConfiguration : IEntityTypeConfiguration<TipoMovimiento>
{
    public void Configure(EntityTypeBuilder<TipoMovimiento> builder)
    {
        builder.ToTable("TipoMovimiento");

        builder.Property(e => e.Descripcion)
        .HasMaxLength(50)
        .IsRequired();
    }
}