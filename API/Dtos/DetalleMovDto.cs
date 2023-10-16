namespace API.Dtos;

public class DetalleMovDto
{
    public int Id { get; set; }
    public MovimientoDto Movimiento { get; set; }
    public MedicamentoDto Medicamento { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
}
