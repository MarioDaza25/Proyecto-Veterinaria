using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class FormulaMedicaConfiguration : IEntityTypeConfiguration<FormulaMedica>
{
    public void Configure(EntityTypeBuilder<FormulaMedica> builder)
    {
        builder.ToTable("FormulaMedica");

        builder.HasOne(p => p.Cita)
        .WithMany(p => p.FormulasMedicas)
        .HasForeignKey(p => p.Id_Cita);

        builder.HasOne(p => p.Medicamento)
        .WithMany(p => p.FormulasMedicas)
        .HasForeignKey(p => p.Id_Medicamento);

        builder.Property(p => p.Dosis)
        .IsRequired();

        builder.Property(p => p.Fecha)
        .HasColumnType("Date");

        builder.Property(p => p.Observacion)
        .IsRequired()
        .HasMaxLength(500);
    }
}
