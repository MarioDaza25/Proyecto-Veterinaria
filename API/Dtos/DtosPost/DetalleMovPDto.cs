namespace API.Dtos.DtosPost;

public class DetalleMovPDto
{
    public int Id_Movimiento { get; set; }
    public int Id_Medicamento { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
}
