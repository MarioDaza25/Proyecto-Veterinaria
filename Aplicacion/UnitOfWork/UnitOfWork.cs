using System.Runtime.CompilerServices;
using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DbAppContext _context;
    public UnitOfWork(DbAppContext context)
    {
        _context = context;
    }

    CitaRepository _cita;
    DetalleMovRepository _detMov;
    EspecieRepository _especie;
    FormulaRepository _forMedica;
    LaboratorioRepository _laboratorio;
    MascotaRepository _mascota;
    MedicamentoRepository _medicamento;
    MovimientoRepository _movimiento;
    PropietarioRepository _propietario;
    ProveedorRepository _proveedor;
    RazaRepository _raza;
    RolRepository _rol;
    TipoMovRepository _tipoMovimiento;
    UsuarioRepository _usuario;
    VeterinarioRepository _veterinario;



    public ICita Citas
    {
        get
        {
            if (_cita is not null)
            {
                return _cita;
            }
            return _cita = new CitaRepository(_context);
        }
    }

    public IDetalleMovimiento DetalleMovimientos 
    {
        get
        {
            if( _detMov is not null)
            {
                return _detMov; 
            }
            return _detMov = new DetalleMovRepository(_context);
        }
    }

    public IEspecie Especies 
    {
        get
        {
            if( _especie is not null)
            {
                return _especie; 
            }
            return _especie = new EspecieRepository(_context);
        }
    }

    public IFormulaMedica FormulasMedicas 
    {
        get
        {
            if( _forMedica is not null)
            {
                return  _forMedica;
            }
            return _forMedica = new FormulaRepository(_context);
        }
    }

    public ILaboratorio Laboratorios 
    {
        get
        {
            if( _laboratorio is not null)
            {
                return _laboratorio ;
            }
            return _laboratorio = new LaboratorioRepository(_context);
        }
    }

    public IMascota Mascotas 
    {
        get
        {
            if( _mascota is not null)
            {
                return _mascota ;
            }
            return _mascota = new MascotaRepository(_context);
        }
    }

    public IMedicamento Medicamentos 
    {
        get
        {
            if( _medicamento is not null)
            {
                return _medicamento ;
            }
            return _medicamento = new MedicamentoRepository(_context);
        }
    }

    public IMovimiento Movimientos 
    {
        get
        {
            if( _movimiento is not null)
            {
                return _movimiento ;
            }
            return _movimiento = new MovimientoRepository(_context);
        }
    }

    public IPropietario Propietarios 
    {
        get
        {
            if( _propietario is not null)
            {
                return _propietario ;
            }
            return _propietario = new PropietarioRepository(_context);
        }
    }

    public IProveedor Proveedores 
    {
        get
        {
            if( _proveedor is not null)
            {
                return _proveedor ;
            }
            return _proveedor = new ProveedorRepository(_context);
        }
    }

    public IRaza Razas 
    {
        get
        {
            if( _raza is not null)
            {
                return _raza;
            }
            return _raza = new RazaRepository(_context);
        }
    }

    public IRol Roles
    {
        get
        {
            if (_rol is not null)
            {
                return _rol;
            }
            return _rol = new RolRepository(_context);
        }
    }
    public ITipoMovimiento TipoMovimientos 
    {
        get
        {
            if( _tipoMovimiento is not null)
            {
                return _tipoMovimiento ;
            }
            return _tipoMovimiento = new TipoMovRepository(_context);
        }
    }
    
    public IUsuario Usuarios
    {
        get
        {
            if (_usuario is not null)
            {
                return _usuario;
            }
            return _usuario = new UsuarioRepository(_context);
        }
    }
    public IVeterinario Veterinarios 
    {
        get
        {
            if( _veterinario is not null)
            {
                return _veterinario ;
            }
            return _veterinario = new VeterinarioRepository(_context);
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
