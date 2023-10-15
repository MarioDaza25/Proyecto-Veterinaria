using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
{
    private readonly DbAppContext _context;
    public ProveedorRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
