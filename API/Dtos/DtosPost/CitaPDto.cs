namespace API.Dtos.DtosPost;

public class CitaPDto
{
    public int Id { get; set; }
    public int Id_Mascota { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public string Motivo { get; set; }
    public int Id_Veterinario { get; set; }
}
