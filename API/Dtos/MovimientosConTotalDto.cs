namespace API.Dtos;

public class MovimientosConTotalDto
{
    public int Id { get; set; }
    public string Movimiento { get; set; }
    public List<string> Medicamento { get; set; }
    public decimal ValorTotal { get; set; }
}
