namespace API.Dtos;

public class MascotaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public PropietarioDto Propietario { get; set; }
    public RazaDto Raza { get; set; }
    public DateOnly FechaNacimiento { get; set; }
}
