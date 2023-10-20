namespace API.Dtos;

public class MedicamentoConProvDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<MedicProvDto> MedicamentosProveedores { get; set; }
}
