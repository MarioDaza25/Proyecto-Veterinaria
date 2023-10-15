using Dominio.Entidades;

namespace API.Dtos;

public class RazaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public Especie Especie { get; set; }
}
