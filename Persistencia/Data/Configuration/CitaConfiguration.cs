using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

    public class CitaConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("Cita");

            builder.Property(p => p.Fecha)
            .IsRequired()
            .HasColumnType("Date");

            builder.Property(p => p.Hora)
            .IsRequired()
            .HasColumnType("Time");

            builder.Property(p => p.Motivo)
            .IsRequired()
            .HasMaxLength(400);

            builder.HasOne(p => p.Mascota)
            .WithMany(p => p.Citas)
            .HasForeignKey(p => p.Id_Mascota)
            .IsRequired();

            builder.HasOne(p => p.Veterinario)
            .WithMany(p => p.Citas)
            .HasForeignKey(p => p.Id_Veterinario)
            .IsRequired();
        }
    }
