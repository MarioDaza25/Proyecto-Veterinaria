using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        builder.ToTable("Proveedor");

        builder.Property(e => e.Nombre)
        .HasMaxLength(50)
        .IsRequired();

         builder.Property(e => e.Direccion)
        .HasMaxLength(50)
        .IsRequired();

         builder.Property(e => e.Telefono)
        .HasMaxLength(50)
        .IsRequired();

        builder.HasMany(p => p.Medicamentos)
        .WithMany(m => m.Proveedores)
        .UsingEntity<MedicamentoProveedor>(
            j => j
                .HasOne(mp => mp.Medicamento)
                .WithMany(m => m.MedicamentosProveedores)
                .HasForeignKey(mp => mp.Id_Medicamento),
            j => j
                .HasOne(p => p.Proveedor)
                .WithMany(t => t.MedicamentosProveedores)
                .HasForeignKey(pt => pt.Id_Proveedor),
            j => 
                {
                    j.HasKey(t => new {t.Id_Medicamento, t.Id_Proveedor});
                });
    }
}
