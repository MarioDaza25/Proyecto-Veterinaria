namespace API.Dtos;

public class MovimientoDto
{
    public int Id { get; set; }
    public TipoMovDto TipoMovimiento { get; set; }
    public DateOnly Fecha { get; set; }
}
