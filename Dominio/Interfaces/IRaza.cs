using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IRaza : IGenericRepository<Raza>
{
    Task<IEnumerable<Raza>> MascotasxRaza(string raza);  
    Task<IEnumerable<TotalMascotasXRaza>> CantidadMascotasXRaza();
}
