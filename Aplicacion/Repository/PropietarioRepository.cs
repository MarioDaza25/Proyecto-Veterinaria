using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class PropietarioRepository : GenericRepository<Propietario>, IPropietario
{
    private readonly DbAppContext _context;
    public PropietarioRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
