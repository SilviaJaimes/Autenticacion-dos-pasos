using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class VerifyDto
{
    [Required]
    public string Code { get; set; }

    [Required]
    public int Id { get; set; }
}