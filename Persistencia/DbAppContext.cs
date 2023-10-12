using System.Reflection;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;

public class DbAppContext : DbContext
{
    public DbAppContext(DbContextOptions<DbAppContext> options) : base(options)
    {
    }
    //Aqui se establecen los DbSet<Entity> Entities { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; }
    protected override void OnModelCreating( ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
