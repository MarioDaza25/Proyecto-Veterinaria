using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class MascotaRepository : GenericRepository<Mascota>, IMascota
{
    private readonly DbAppContext _context;
    public MascotaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Mascota> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.Mascotas as IQueryable<Mascota>;
        if(!string.IsNullOrEmpty(_search))
        {
            query = query.Where(p => p.Nombre.ToUpper() == _search.ToUpper());
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(q =>q.Propietario)
            .Include(q => q.Raza).ThenInclude(r => r.Especie)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }


    //Listar las mascotas que fueron atendidas por motivo de vacunacion en el x trimestre del X --OK
    public async Task<IEnumerable<Mascota>> MascotasAtendidasXMotivoXtrimestre( int trim, int año, string motivo)
    {
        int primerMes = (trim - 1) * 3 + 1 ;
        return await _context.Mascotas
                            .Where( p => p.Citas.Any(a =>
                                    a.Motivo.ToUpper() == motivo.ToUpper() &&
                                    a.Fecha.Year == año &&
                                    a.Fecha.Month >= primerMes &&
                                    a.Fecha.Month <= primerMes + 2)
                                )
                           .ToListAsync();
    }
} 
