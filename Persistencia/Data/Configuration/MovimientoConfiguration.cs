using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
{
    public void Configure(EntityTypeBuilder<Movimiento> builder)
    {
        builder.ToTable("Movimiento");

        builder.HasOne(p => p.TipoMovimiento)
        .WithMany(p => p.Movimientos)
        .HasForeignKey(p => p.Id_TipoMovimiento);

        builder.Property(p => p.Fecha)
        .HasColumnType("Date");
    }
}
