using System.ComponentModel.DataAnnotations;

namespace TODO.Application.DTOs
{
    /// <summary>
    /// DTO for user registration. ConfirmPassword is validated client-side and server-side via Compare.
    /// </summary>
    public class RegisterDTO
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
