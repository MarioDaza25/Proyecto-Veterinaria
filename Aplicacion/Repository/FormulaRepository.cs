using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class FormulaRepository : GenericRepository<FormulaMedica>, IFormulaMedica
{
    private readonly DbAppContext _context;
    public FormulaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<FormulaMedica> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<FormulaMedica>().CountAsync();
        var registros = await _context.Set<FormulaMedica>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
