namespace Dominio.Entidades;

public class Movimiento : BaseEntity
{
    public int Id_TipoMovimiento { get; set; }
    public TipoMovimiento TipoMovimiento { get; set; }
    public DateOnly Fecha { get; set; }

    public ICollection<DetalleMovimiento> DetalleMovimientos { get; set;}
}
