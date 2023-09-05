using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;
public class LogginDto{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}