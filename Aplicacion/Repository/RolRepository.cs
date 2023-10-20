using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly DbAppContext _context;
    public RolRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Rol> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.Roles as IQueryable<Rol>;
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
