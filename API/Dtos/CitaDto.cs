using Dominio.Entidades;

namespace API.Dtos;

public class CitaDto
{
    public int Id { get; set; }
    public Mascota Mascota { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public string Motivo { get; set; }
    public Veterinario Veterinario { get; set; }
}
