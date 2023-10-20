namespace API.Dtos.DtosPost;

public class MedicamentoPDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public double Cantidad { get; set; }
    public decimal Precio { get; set; }
    public int Id_Laboratorio { get; set; }
    public int Id_Proveedor { get; set; }

}
