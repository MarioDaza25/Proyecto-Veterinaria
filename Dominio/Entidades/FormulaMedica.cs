namespace Dominio.Entidades;

public class FormulaMedica : BaseEntity
{
    public int Id_Cita { get; set; }
    public Cita Cita { get; set; }
    public int Id_Medicamento { get; set; }
    public Medicamento Medicamento { get; set; }
    public decimal Dosis { get; set; }
    public DateOnly Fecha { get; set; }
    public string Observacion { get; set; }
}
