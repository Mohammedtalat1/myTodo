using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace TODO.Domain.Entites
{
    public class Users : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;  

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;    

        [Required]
        public string Password { get; set; } = string.Empty; 

        [Required]
        public int RoleId { get; set; }
        public Roles? Role { get; set; }
    }
}
