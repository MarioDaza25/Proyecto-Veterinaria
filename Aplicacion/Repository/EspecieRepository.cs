using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class EspecieRepository : GenericRepository<Especie>, IEspecie
{
    private readonly DbAppContext _context; 
    public EspecieRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    //Mascotas que se encuentren registradas cuya especie sea felina.
    public async Task<IEnumerable<Especie>> MascotaXUnaEspecie(string especie)
    {
        return await _context.Especies
            .Where(e => e.Nombre.ToUpper() == especie.ToUpper())
            .Include(e => e.Razas).ThenInclude(r => r.Mascotas)
            .ToListAsync();
    }


    //Listar todas las mascotas agrupadas por especie..
    public async Task<IEnumerable<Especie>> MascotaXEspecie()
    {
        return await _context.Especies
            .Include(e => e.Razas).ThenInclude(r => r.Mascotas)
            .ToListAsync();
    }
}
