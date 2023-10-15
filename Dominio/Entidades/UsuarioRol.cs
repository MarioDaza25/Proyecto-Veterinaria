namespace Dominio.Entidades;

public class UsuarioRol : BaseEntity
{
    public int Usuario_Id { get; set; }
    public Usuario Usuario { get; set; }
    public int Rol_Id { get; set; }
    public Rol Rol { get; set; }
}
