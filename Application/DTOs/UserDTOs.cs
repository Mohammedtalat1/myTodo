using Application.Models.Base;
using System.ComponentModel.DataAnnotations;

public class UserDTO : BaseDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public int RoleId { get; set; }
}
