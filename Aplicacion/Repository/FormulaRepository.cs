using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class FormulaRepository : GenericRepository<FormulaMedica>, IFormulaMedica
{
    private readonly DbAppContext _context;
    public FormulaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
