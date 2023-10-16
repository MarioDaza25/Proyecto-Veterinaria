namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    ICita Citas { get; }
    IDetalleMovimiento DetalleMovimientos { get; }
    IEspecie Especies { get; }
    IFormulaMedica FormulasMedicas { get; }
    ILaboratorio Laboratorios { get; }
    IMascota Mascotas { get; }
    IMedicamento Medicamentos { get; }
    IMovimiento Movimientos { get; }
    IPropietario Propietarios { get; }
    IProveedor Proveedores { get; }
    IRaza Razas { get; }
    IRol Roles {get;}
    ITipoMovimiento TipoMovimientos { get; }
    IUsuario Usuarios {get;}
    IVeterinario Veterinarios {get;}    
    Task<int> SaveAsync();
}
