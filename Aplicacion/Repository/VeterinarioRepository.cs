using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class VeterinarioRepository : GenericRepository<Veterinario>, IVeterinario
{
    public readonly DbAppContext _context;
    public VeterinarioRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
