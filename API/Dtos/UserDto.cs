namespace API.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}