namespace Dominio.Entities;

public class User : BaseEntity
{
    public string Usuario { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}