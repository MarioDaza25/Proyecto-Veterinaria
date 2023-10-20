using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IMovimiento : IGenericRepository<Movimiento>
{
    Task<IEnumerable<MovimientosConTotal>> ResumenMovimientos();
}
