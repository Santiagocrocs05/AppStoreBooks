using System.ComponentModel.DataAnnotations;

namespace AppStore.Models.DTOs;

public class LoginModel
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}