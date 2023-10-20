namespace API.Dtos.DtosPost;

public class MascotaPDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Id_Raza { get; set; }
    public int Id_Propietario { get; set; }
    public DateOnly FechaNacimiento { get; set; }
}
