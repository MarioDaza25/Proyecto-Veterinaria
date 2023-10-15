using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
{
    public void Configure(EntityTypeBuilder<Medicamento> builder)
    {
        builder.ToTable("Medicamento");

        builder.Property(e => e.Nombre)
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(e => e.Cantidad)    
        .IsRequired();

        builder.Property(e => e.Precio)
        .HasColumnName("precio")
        .HasColumnType("Decimal(15,2)")
        .IsRequired();

        builder.HasOne(p => p.Laboratorio)
        .WithMany(p => p.Medicamentos)
        .HasForeignKey(p => p.Id_Laboratorio);
    }
}
