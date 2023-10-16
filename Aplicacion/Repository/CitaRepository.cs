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


    public override async Task<(int totalRegistros, IEnumerable<Cita> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Cita>().CountAsync();
        var registros = await _context.Set<Cita>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Include(c => c.Mascota).ThenInclude(m =>m.Propietario)
            .Include(c => c.Veterinario)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
