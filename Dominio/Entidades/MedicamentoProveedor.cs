namespace Dominio.Entidades;

public class MedicamentoProveedor
{
    public int Id_Medicamento { get; set; }
    public Medicamento Medicamento { get; set; }
    public int Id_Proveedor { get; set; }
    public Proveedor Proveedor { get; set; }
}
