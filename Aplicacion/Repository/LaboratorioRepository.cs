using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class LaboratorioRepository : GenericRepository<Laboratorio>, ILaboratorio
{
    private readonly DbAppContext _context;
    public LaboratorioRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    //Medicamentos que pertenezcan a el laboratorio Genfar
    public async Task<IEnumerable<Laboratorio>> MedicamentoXLaboratorio(string laboratorio)
    {
        return await _context.Laboratorios
                    .Where(l => l.Nombre.ToUpper() == laboratorio.ToUpper())
                    .Include(l => l.Medicamentos)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Laboratorio> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Laboratorio>().CountAsync();
        var registros = await _context.Set<Laboratorio>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
