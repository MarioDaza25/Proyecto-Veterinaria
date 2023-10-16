using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        {
            builder.ToTable("Usuario");

            builder.Property(p => p.Username)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(p => p.Email)
           .HasColumnName("email")
           .IsRequired();

            builder.Property(p => p.Password)
           .HasMaxLength(255)
           .IsRequired();

            builder.HasMany(p => p.Roles)
           .WithMany(r => r.Usuarios)
           .UsingEntity<UsuarioRol>(

            j => j
                .HasOne(pt => pt.Rol)
                .WithMany(t => t.UsuariosRoles)
                .HasForeignKey(ut => ut.Rol_Id),


            j => j
                .HasOne(et => et.Usuario)
                .WithMany(et => et.UsuariosRoles)
                .HasForeignKey(el => el.Usuario_Id),

            j =>
            {
                j.ToTable("RolUsuario");
                j.HasKey(t => new { t.Rol_Id, t.Usuario_Id });

            });

        }

    }
}