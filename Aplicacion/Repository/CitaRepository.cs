using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class CitaRepository : GenericRepository<Cita>, ICita
{
    private readonly DbAppContext _context;
    public CitaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Cita>> GetAllAsync()
    {
        return await _context.Set<Cita>()
                        .Include(c => c.Mascota).ThenInclude(m => m.Raza).ThenInclude(r => r.Especie)
                        .Include(c => c.Mascota).ThenInclude(m => m.Propietario)
                        .Include(c => c.Veterinario)
                        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Cita> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.Citas as IQueryable<Cita>;
        if(!string.IsNullOrEmpty(_search))
        {
            query = query.Where(p => p.Mascota.Nombre.ToUpper() == _search.ToUpper());
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                .Include(c => c.Mascota).ThenInclude(m => m.Raza).ThenInclude(r => r.Especie)
                .Include(c => c.Mascota).ThenInclude(m =>m.Propietario)
                .Include(c => c.Veterinario)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        return (totalRegistros, registros);
    }
}
