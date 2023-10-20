using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuario
{
    private readonly DbAppContext _context;

    public UsuarioRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

     public async Task<Usuario> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuarios
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<Usuario> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios
                            .Include(u=>u.Roles)
                            .FirstOrDefaultAsync(u=>u.Username.ToLower() == username.ToLower());
    }

    public override async Task<(int totalRegistros, IEnumerable<Usuario> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var query = _context.Usuarios as IQueryable<Usuario>;
        if(!string.IsNullOrEmpty(_search))
        {
            query = query.Where(p => p.Username.ToUpper() == _search.ToUpper());
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        return (totalRegistros, registros);
    }
}
