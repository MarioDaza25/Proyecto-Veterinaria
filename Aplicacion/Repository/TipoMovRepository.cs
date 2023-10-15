using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class TipoMovRepository : GenericRepository<TipoMovimiento>, ITipoMovimiento
{
    private readonly DbAppContext _context;
    public TipoMovRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
