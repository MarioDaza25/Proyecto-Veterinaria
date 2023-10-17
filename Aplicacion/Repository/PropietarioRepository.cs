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
        return await _context.Propietarios
                    .Include(p => p.Mascotas)
                    .Where(p => p.Mascotas.Any())
                    .ToListAsync();
    }
}
