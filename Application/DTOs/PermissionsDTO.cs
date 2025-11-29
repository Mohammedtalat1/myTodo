using Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Application.DTOs
{
    public class PermissionsDTO : BaseDTO
    {

        [MaxLength(100)]
        public string? Name_Ar { get; set; }

        [MaxLength(100)]
        public string? Name_En { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
