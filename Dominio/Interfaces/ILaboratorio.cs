using Dominio.Entidades;

namespace Dominio.Interfaces;
public interface ILaboratorio : IGenericRepository<Laboratorio>
{
    Task<IEnumerable<Laboratorio>> MedicamentoXLaboratorio(string laboratorio);
}
