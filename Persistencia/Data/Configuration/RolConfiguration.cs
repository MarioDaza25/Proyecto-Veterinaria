using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class RolConfiguration: IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {

        builder.ToTable("Rol");

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Descripcion)
        .HasMaxLength(50)
        .IsRequired();

    }
}