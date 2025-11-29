using Application.DTOs.Base;
namespace TODO.Application.Entities
{
    public class RolesDTO : BaseDTO
    {
        public string Name_Ar { get; set; }
        public string? Name_En { get; set; }
        // public ICollection<UserDTO>? Users { get; set; }
    }
}
