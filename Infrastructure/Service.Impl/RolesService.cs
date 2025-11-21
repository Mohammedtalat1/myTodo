using Application.Common;
using AutoMapper;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entites;
using TODO.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Infrastructure.Service.Impl
{
    public class RolesService(IRolesRepository repository, IMapper mapper) : IRolesService
    {
        private readonly IRolesRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<int> DeleteAsync<Type>(Type Id)
        {
            return await _repository.DeleteAsync(Id);
        }

        public async Task<Results<IEnumerable<RolesDTO>>> GetAllAsync()
        {
            return SuccessResult.Success(_mapper.Map<IEnumerable<RolesDTO>>(await _repository.GetAllAsync()));
        }

        public async Task<Results<RolesDTO>> GetByIdAsync(int id)
        {
            return SuccessResult.Success(_mapper.Map<RolesDTO>(await _repository.GetByIdAsync(id)));
        }

        public async Task<int> InsertAsync(RolesDTO value)
        {
            return await _repository.InsertAsync(_mapper.Map<Roles>(value));
        }

        public async Task<int> UpdateAsync(RolesDTO value)
        {
            return await _repository.UpdateAsync(_mapper.Map<Roles>(value));
        }

        public Task<int> UpdateAsync(int id, RolesDTO value)
        {
            throw new NotImplementedException();
        }
    }
}
