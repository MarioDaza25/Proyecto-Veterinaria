using Dominio.Entidades;

namespace API.Dtos;

public class DetalleMovDto
{
    public int Id { get; set; }
    public Movimiento Movimiento { get; set; }
    public Medicamento Medicamento { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
}
