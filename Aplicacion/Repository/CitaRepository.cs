using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class CitaRepository : GenericRepository<Cita>, ICita
{
    private readonly DbAppContext _context;
    public CitaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
