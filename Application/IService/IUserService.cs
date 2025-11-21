using Application.DTOs;
using TODO.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using TODO.Application.IService;
using Application.IService;
using TODO.Domain.Entites;

namespace TODO.Application.IService
{
    public interface IUserService : IBaseServices<UserDTO>
    {
     
    }

}
