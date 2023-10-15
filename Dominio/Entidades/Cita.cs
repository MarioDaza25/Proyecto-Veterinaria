namespace Dominio.Entidades;

public class Cita : BaseEntity
{
    public int Id_Mascota { get; set; }
    public Mascota Mascota { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public string Motivo { get; set; }
    public int Id_Veterinario { get; set; }
    public Veterinario Veterinario { get; set; }
    public ICollection<FormulaMedica> FormulasMedicas { get; set; }
}
