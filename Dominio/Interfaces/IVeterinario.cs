using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IVeterinario : IGenericRepository<Veterinario>
{
    Task<IEnumerable<Veterinario>> VeterinariosXEspecilidad(string especialidad);
    Task<IEnumerable<Veterinario>> MascotaXVeterinario(string veterinario);
}
