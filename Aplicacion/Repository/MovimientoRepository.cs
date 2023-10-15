using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class MovimientoRepository  : GenericRepository<Movimiento>, IMovimiento
{
    private readonly DbAppContext _context;

    public MovimientoRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
