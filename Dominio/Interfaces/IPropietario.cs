using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IPropietario : IGenericRepository<Propietario>
{
    Task<IEnumerable<Propietario>> PropietarioConMascotas();
}
