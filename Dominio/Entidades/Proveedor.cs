namespace Dominio.Entidades;

public class Proveedor : BaseEntity
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    
    public ICollection<Medicamento> Medicamentos { get; set; } = new HashSet<Medicamento>();
    public ICollection<MedicamentoProveedor> MedicamentosProveedores { get; set; }
}

