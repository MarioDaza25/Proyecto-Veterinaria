using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
    {
        private readonly DbAppContext _context;
        public MedicamentoRepository(DbAppContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Medicamento>> GetAllAsync()
        {
            return await _context.Medicamentos
                            .Include(c => c.Laboratorio)
                            .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Medicamento> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
        {
            var query = _context.Medicamentos as IQueryable<Medicamento>;
            if(!string.IsNullOrEmpty(_search))
            {
                query = query.Where(p => p.Nombre.ToUpper() == _search.ToUpper());
            }
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(q => q.Laboratorio)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRegistros, registros);
        }


        //Medicamentos que tenga un precio de venta mayor a 50000
        public async Task<IEnumerable<Medicamento>> MedicamentoXPrecio(decimal precio)
        {
            return await _context.Medicamentos
                        .Where(m => m.Precio >= precio)
                        .ToListAsync();
        }

        //Listar los proveedores que me venden un determinado medicamento.
        public async Task<IEnumerable<Medicamento>> MedicamentoConProveedor(string medicamento)
        {
            return await _context.Medicamentos.Include(m => m.MedicamentosProveedores).ThenInclude(mp => mp.Proveedor)
            .Where(m => m.Nombre.ToUpper() == medicamento.ToUpper())
            .ToListAsync();
        } 
    }
}