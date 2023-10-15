namespace Dominio.Entidades;

public class Medicamento : BaseEntity
{
    public string Nombre { get; set; }
    public double Cantidad { get; set; }
    public decimal Precio { get; set; }
    public int Id_Laboratorio { get; set; }
    public Laboratorio Laboratorio { get; set; }

    public ICollection<DetalleMovimiento> DetalleMovimientos { get; set;}
    public ICollection<FormulaMedica> FormulasMedicas { get; set; }
    public ICollection<Proveedor> Proveedores { get; set; } = new HashSet<Proveedor>();
    public ICollection<MedicamentoProveedor> MedicamentosProveedores { get; set; } 
}
