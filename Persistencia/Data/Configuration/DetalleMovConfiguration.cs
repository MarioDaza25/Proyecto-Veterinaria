using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class DetalleMovConfiguration : IEntityTypeConfiguration<DetalleMovimiento>
{
    public void Configure(EntityTypeBuilder<DetalleMovimiento> builder)
    {
        
        builder.ToTable("DetalleMovimiento");

        builder.HasOne(p => p.Movimiento)
        .WithMany(p => p.DetalleMovimientos)
        .HasForeignKey(p => p.Id_Movimiento);

        builder.HasOne(p => p.Medicamento)
        .WithMany(p => p.DetalleMovimientos)
        .HasForeignKey(p => p.Id_Medicamento);

        builder.Property(p => p.Cantidad)
        .IsRequired();

         builder.Property(p => p.Precio)
        .IsRequired()
        .HasColumnType("Decimal(12,2)");
    }
}
