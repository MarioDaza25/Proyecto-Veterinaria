using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class RazaRepository : GenericRepository<Raza>, IRaza
{
    private readonly DbAppContext _context;
    public RazaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
