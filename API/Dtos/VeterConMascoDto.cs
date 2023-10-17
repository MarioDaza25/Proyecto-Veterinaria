namespace API.Dtos;

public class VeterConMascoDto
{
    public string Nombre { get; set; }
    public string Especialidad { get; set; }
    public List<CitaMascoDto> Citas { get; set; }
}
