namespace API.Dtos;
public class MedicamentoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public double Cantidad { get; set; }
    public decimal Precio { get; set; }
    public LaboratorioDto Laboratorio { get; set; }

}
