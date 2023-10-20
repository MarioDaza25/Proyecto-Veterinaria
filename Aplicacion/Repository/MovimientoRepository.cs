using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class MovimientoRepository  : GenericRepository<Movimiento>, IMovimiento
{
    private readonly DbAppContext _context;

    public MovimientoRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Movimiento>> GetAllAsync()
    {
        return await _context.Movimientos
                        .Include(c => c.TipoMovimiento)
                        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Movimiento> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.Movimientos as IQueryable<Movimiento>;
        if(!string.IsNullOrEmpty(_search))
        {
            query = query.Where(p => p.Id == int.Parse(_search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(c => c.TipoMovimiento)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }

    //Listar todos los movimientos de medicamentos y el valor total de cada movimiento.
    public async Task<IEnumerable<MovimientosConTotal>> ResumenMovimientos()
    {
        var movimientos = await _context.Movimientos.Include(m => m.TipoMovimiento).Include(m => m.DetalleMovimientos).ThenInclude(d => d.Medicamento).ToListAsync();

        var resultado = movimientos
            .GroupBy(m => m.Id)
            .Select(grupo => new MovimientosConTotal
            {
                Id = grupo.Key,
                Movimiento = grupo.FirstOrDefault().TipoMovimiento.Descripcion,
                Medicamento = grupo.SelectMany(m => m.DetalleMovimientos.Select(dm => dm.Medicamento.Nombre)).ToList(),
                ValorTotal = grupo.SelectMany(m => m.DetalleMovimientos).Sum(s => s.Cantidad * s.Precio)
            })
            .ToList();

        return resultado;
    }
}
