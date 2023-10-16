
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class DetalleMovRepository : GenericRepository<DetalleMovimiento>, IDetalleMovimiento
{
    private readonly DbAppContext _context;
    public DetalleMovRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<DetalleMovimiento> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<DetalleMovimiento>().CountAsync();
        var registros = await _context.Set<DetalleMovimiento>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
