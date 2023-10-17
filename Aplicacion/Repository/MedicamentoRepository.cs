using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public override async Task<(int totalRegistros, IEnumerable<Medicamento> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
        {
            var totalRegistros = await _context.Set<Medicamento>().CountAsync();
            var registros = await _context.Set<Medicamento>()
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

    }
}