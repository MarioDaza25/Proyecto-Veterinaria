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

    public override async Task<IEnumerable<FormulaMedica>> GetAllAsync()
    {
        return await _context.Set<FormulaMedica>()
                        .Include(f => f.Cita).ThenInclude(c => c.Mascota).ThenInclude(m => m.Propietario)
                        .Include(f => f.Cita).ThenInclude(c => c.Mascota).ThenInclude(m => m.Raza).ThenInclude(r => r.Especie)
                        .Include(f => f.Cita).ThenInclude(c => c.Veterinario)
                        .Include(f => f.Medicamento).ThenInclude(m => m.Laboratorio)
                        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<FormulaMedica> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.FormulasMedicas as IQueryable<FormulaMedica>;
        if(!string.IsNullOrEmpty(_search))
        {
            query = query.Where(p => p.Cita.Mascota.Nombre.ToUpper() == _search.ToUpper());
        }
        var totalRegistros = await _context.Set<FormulaMedica>().CountAsync();
        var registros = await _context.Set<FormulaMedica>()
                        .Include(f => f.Cita).ThenInclude(c => c.Mascota).ThenInclude(m => m.Propietario)
                        .Include(f => f.Cita).ThenInclude(c => c.Mascota).ThenInclude(m => m.Raza).ThenInclude(r => r.Especie)
                        .Include(f => f.Cita).ThenInclude(c => c.Veterinario)
                        .Include(f => f.Medicamento).ThenInclude(m => m.Laboratorio)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
