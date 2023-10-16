namespace API.Dtos;

public class FormulaDto
{
    public int Id { get; set; }
    public CitaDto Cita { get; set; }
    public MedicamentoDto Medicamento { get; set; }
    public decimal Dosis { get; set; }
    public DateOnly Fecha { get; set; }
    public string Observacion { get; set; }
}
