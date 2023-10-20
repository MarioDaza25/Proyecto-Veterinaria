using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class VeterinarioRepository : GenericRepository<Veterinario>, IVeterinario
{
    public readonly DbAppContext _context;
    public VeterinarioRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    //Veterinarios cuya especialidad sea Cirujano vascular.
    public async Task<IEnumerable<Veterinario>> VeterinariosXEspecilidad(string especialidad)
    {
        return await _context.Veterinarios.Where(v => v.Especialidad.ToUpper() == especialidad.ToUpper()).ToListAsync();
    }


    //Listar las mascotas que fueron atendidas por un determinado veterinario.
    public async Task<IEnumerable<Veterinario>> MascotaXVeterinario(string nombreVeterinario)
    {
        return await _context.Veterinarios
            .Where(v => v.Nombre.ToUpper() == nombreVeterinario.ToUpper())
            .Include(v => v.Citas).ThenInclude(c => c.Mascota)
            .ToListAsync();
    }

    
    public override async Task<(int totalRegistros, IEnumerable<Veterinario> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.Veterinarios as IQueryable<Veterinario>;
        if(!string.IsNullOrEmpty(_search))
        {
            query = query.Where(p => p.Nombre.ToUpper() == _search.ToUpper());
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
