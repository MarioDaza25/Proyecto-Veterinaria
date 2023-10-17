using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IMedicamento : IGenericRepository<Medicamento>
{
    Task<IEnumerable<Medicamento>> MedicamentoXPrecio(decimal precio);
}
