using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class PropietarioRepository : GenericRepository<Propietario>, IPropietario
{
    private readonly DbAppContext _context;
    public PropietarioRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    //Listar los propietarios y sus mascotas.
    public async Task<IEnumerable<Propietario>> PropietarioConMascotas()
    {
        return await _context.Propietarios.Include(p => p.Mascotas).Where(p => p.Mascotas.Any()).ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Propietario> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.Propietarios as IQueryable<Propietario>;
        if(!string.IsNullOrEmpty(_search))
        {
            query = query.Where(p => p.Nombre == _search);
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
