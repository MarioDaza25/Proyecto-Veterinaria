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

    public override async Task<IEnumerable<Raza>> GetAllAsync()
    {
        return await _context.Razas
                        .Include(r => r.Especie)
                        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Raza> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.Razas as IQueryable<Raza>;
        if(!string.IsNullOrEmpty(_search))
        {
            query = query.Where(p => p.Nombre.ToUpper() == _search.ToUpper());
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Include(c => c.Especie)
            .ToListAsync();
        return (totalRegistros, registros);
    }

    //Listar las mascotas y sus propietarios cuya raza sea Golden Retriver
    public async Task<IEnumerable<Raza>> MascotasxRaza(string raza)
    {
        return await _context.Razas
                    .Where(r => r.Nombre.ToUpper() == raza.ToUpper())
                    .Include(r => r.Mascotas).ThenInclude(m => m.Propietario)
                    .ToListAsync();
    }

    //Listar la cantidad de mascotas que pertenecen a una raza
    public async Task<IEnumerable<TotalMascotasXRaza>> CantidadMascotasXRaza()
    {
        return await _context.Razas
                    .Include(r => r.Mascotas)
                    .Select(raza => new TotalMascotasXRaza
                    {
                        Nombre = raza.Nombre,
                        Cantidad = raza.Mascotas.Count()
                    })
                    .ToListAsync();
    }
}
