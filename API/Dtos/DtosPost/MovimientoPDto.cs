namespace API.Dtos.DtosPost;

public class MovimientoPDto
{
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public int Id_TipoMovimiento { get; set; }
    public List<DetalleMovPDto> DetalleMovimientos { get; set;}
}
