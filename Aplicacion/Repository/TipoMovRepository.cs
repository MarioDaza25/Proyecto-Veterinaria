using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class TipoMovRepository : GenericRepository<TipoMovimiento>, ITipoMovimiento
{
    private readonly DbAppContext _context;
    public TipoMovRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<TipoMovimiento> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.TipoMovimientos as IQueryable<TipoMovimiento>;
        if(!string.IsNullOrEmpty(_search))
        {
            query = query.Where(p => p.Descripcion.ToUpper() == _search.ToUpper());
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
