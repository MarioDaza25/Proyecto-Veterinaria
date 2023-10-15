using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class RazaConfiguration : IEntityTypeConfiguration<Raza>
{
    public void Configure(EntityTypeBuilder<Raza> builder)
    {
        builder.ToTable("Raza");

        builder.Property(e => e.Nombre)
        .HasMaxLength(50)
        .IsRequired();

        builder.HasOne(p => p.Especie)
        .WithMany(p => p.Razas)
        .HasForeignKey(p => p.Id_Especie);
    }
}