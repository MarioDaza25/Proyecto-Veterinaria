namespace Dominio.Entidades;

public class Raza : BaseEntity
{
    public string Nombre { get; set; }
    public int Id_Especie { get; set; }
    public Especie Especie { get; set; }

    public ICollection<Mascota> Mascotas { get; set; }
}
