using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class VeterinarioConfiguration : IEntityTypeConfiguration<Veterinario>
    {
        public void Configure(EntityTypeBuilder<Veterinario> builder)
        {
            builder.ToTable("Veterinario");

            builder.Property(e => e.Nombre)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(e => e.Correo)
            .HasMaxLength(255)
            .IsRequired();

            builder.Property(e => e.Telefono)
            .HasMaxLength(15)
            .IsRequired();

            builder.Property(e => e.Especialidad)
            .HasMaxLength(50)
            .IsRequired();
        }
    }