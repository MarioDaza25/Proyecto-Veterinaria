using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshToken");

        builder.HasOne(p => p.Usuario)
        .WithMany(p => p.RefreshTokens)
        .HasForeignKey(p => p.Usuario_Fk);

        builder.Property(p => p.Token)
        .IsRequired()
        .HasMaxLength(500);

        builder.Property(p => p.Creacion)
        .IsRequired()
        .HasColumnType("DateTime");

        builder.Property(p => p.Expiracion)
        .IsRequired()
        .HasColumnType("DateTime");

        builder.Property(p => p.Revoked)
        .HasColumnType("DateTime");
    }
}