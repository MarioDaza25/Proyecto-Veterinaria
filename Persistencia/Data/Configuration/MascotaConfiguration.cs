using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
{
    public void Configure(EntityTypeBuilder<Mascota> builder)
    {
        builder.ToTable("Mascota");

        builder.Property(e => e.Nombre)
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(e => e.FechaNacimiento)
        .HasColumnType("date")
        .IsRequired();

        builder.HasOne(p => p.Propietario)
        .WithMany(p => p.Mascotas)
        .HasForeignKey(p => p.Id_Propietario);

        builder.HasOne(p => p.Raza)
        .WithMany(p => p.Mascotas)
        .HasForeignKey(p => p.Id_Raza);
    }
}