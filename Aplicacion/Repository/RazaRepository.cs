using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class RazaRepository : GenericRepository<Raza>, IRaza
{
    private readonly DbAppContext _context;
    public RazaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Raza> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Raza>().CountAsync();
        var registros = await _context.Set<Raza>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Include(c => c.Especie)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
