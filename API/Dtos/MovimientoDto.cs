using Dominio.Entidades;

namespace API.Dtos;

public class MovimientoDto
{
    public int Id { get; set; }
    public TipoMovimiento TipoMovimiento { get; set; }
    public DateOnly Fecha { get; set; }
}
