using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class LaboratorioRepository : GenericRepository<Laboratorio>, ILaboratorio
{
    private readonly DbAppContext _context;
    public LaboratorioRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
