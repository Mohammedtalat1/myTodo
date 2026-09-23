using Domain.IRepository.IBaseRepository;
using TODO.Domain.Entities;
using System.Threading.Tasks;

namespace TODO.Domain.IRepository
{
    /// <summary>
    /// ISP: extends IBaseRepository only with user-specific query methods.
    /// </summary>
    public interface IUserRepository : IBaseRepository<Users>
    {
        Task<Users?> GetByEmailAsync(string email);
        Task<Users?> GetByUsernameAsync(string username);
    }
}
