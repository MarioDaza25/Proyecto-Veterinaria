using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class MascotaRepository : GenericRepository<Mascota>, IMascota
{
    private readonly DbAppContext _context;
    public MascotaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
