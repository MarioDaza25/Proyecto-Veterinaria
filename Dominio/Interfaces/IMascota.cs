using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IMascota : IGenericRepository<Mascota>
{
    Task<IEnumerable<Mascota>> MascotasAtendidasXMotivoXtrimestre( int trim, int año, string motivo);
}
