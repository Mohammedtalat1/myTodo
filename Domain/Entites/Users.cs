using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Domain.Entites
{
    public class Users : BaseEntity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Roles? Role { get; set; }


    }
}
