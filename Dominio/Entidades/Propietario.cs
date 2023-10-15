namespace Dominio.Entidades;

public class Propietario : BaseEntity
{
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }

    public ICollection<Mascota> Mascotas { get; set; }
}
