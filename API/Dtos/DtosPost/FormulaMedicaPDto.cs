namespace API.Dtos.DtosPost;

public class FormulaMedicaPDto
{
    public int Id { get; set; }
    public int Id_Cita { get; set; }
    public int Id_Medicamento { get; set; }
    public decimal Dosis { get; set; }
    public DateOnly Fecha { get; set; }
    public string Observacion { get; set; }
}
