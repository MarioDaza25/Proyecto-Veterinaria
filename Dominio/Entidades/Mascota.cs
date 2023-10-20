namespace Dominio.Entidades;

public class Mascota : BaseEntity
{
    public string Nombre { get; set; }
    public int Id_Raza { get; set; }
    public Raza Raza { get; set; }
    public int Id_Propietario { get; set; }
    public Propietario Propietario { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    
    public ICollection<Cita> Citas { get; set; }
}
