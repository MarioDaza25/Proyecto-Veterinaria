namespace API.Dtos;

public class PropConMascotaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<MascotaNombreDto> Mascotas { get; set; }
}
