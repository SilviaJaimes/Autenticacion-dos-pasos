using System.ComponentModel.DataAnnotations;

namespace API.Dtos;
public class RegisterDto
{
    [Required]
    public string Usuario { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public DateOnly Fecha { get; set; }  = DateOnly.FromDateTime(DateTime.Now);
}