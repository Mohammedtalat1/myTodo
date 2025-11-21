using Application.Models.Base;
using TODO.Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Application.DTOs
{
    public class RolesDTO : BaseDTO
    {

     
        public string Name_Ar { get; set; }

        public string? Name_En { get; set; }

        // Navigation to Users (optional if you want to expose)
        public ICollection<UserDTO>? Users { get; set; }
    }
}
