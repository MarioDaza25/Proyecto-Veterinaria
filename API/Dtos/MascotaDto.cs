using Dominio.Entidades;

namespace API.Dtos;

public class MascotaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public Propietario Propietario { get; set; }
    public Raza Raza { get; set; }
    public DateOnly FechaNacimiento { get; set; }
}
