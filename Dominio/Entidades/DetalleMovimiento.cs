namespace Dominio.Entidades;

public class DetalleMovimiento : BaseEntity
{
    public int Id_Movimiento { get; set; }
    public Movimiento Movimiento { get; set; }
    public int Id_Medicamento { get; set; }
    public Medicamento Medicamento { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
}
