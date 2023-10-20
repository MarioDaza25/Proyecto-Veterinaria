namespace API.Dtos;

public class MascotaConPropietarioDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public PropietarioDto Propietario { get; set; }
}
