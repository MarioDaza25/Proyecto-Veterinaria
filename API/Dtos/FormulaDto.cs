using Dominio.Entidades;

namespace API.Dtos;

public class FormulaDto
{
    public int Id { get; set; }
    public Cita Cita { get; set; }
    public Medicamento Medicamento { get; set; }
    public decimal Dosis { get; set; }
    public DateOnly Fecha { get; set; }
    public string Observacion { get; set; }
}
