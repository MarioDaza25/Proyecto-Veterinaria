using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IEspecie : IGenericRepository<Especie>
{
    Task<IEnumerable<Especie>> MascotaXUnaEspecie(string especie);
    Task<IEnumerable<Especie>> MascotaXEspecie();
}
