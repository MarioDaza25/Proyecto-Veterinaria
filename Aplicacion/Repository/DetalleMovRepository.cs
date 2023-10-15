
using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class DetalleMovRepository : GenericRepository<DetalleMovimiento>, IDetalleMovimiento
{
    private readonly DbAppContext _context;
    public DetalleMovRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
