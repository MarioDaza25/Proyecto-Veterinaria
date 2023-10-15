using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class EspecieRepository : GenericRepository<Especie>, IEspecie
{
    private readonly DbAppContext _context; 
    public EspecieRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
