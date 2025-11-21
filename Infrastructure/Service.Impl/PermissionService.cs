using Application.Common;
using AutoMapper;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entites;
using TODO.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODO.Infrastructure.Service.Impl
{
    public class PermissionService(IPermissionRepository repository, IMapper mapper) : IPermissionsService
    {
        private readonly IPermissionRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<Results<IEnumerable<PermissionsDTO>>> GetAllAsync()
        {
            var permissions = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<PermissionsDTO>>(permissions);
            return SuccessResult.Success(result);
        }

        public async Task<Results<PermissionsDTO>> GetByIdAsync(int id)
        {
            var permission = await _repository.GetByIdAsync(id);
            var result = _mapper.Map<PermissionsDTO>(permission);
            return SuccessResult.Success(result);
        }

        public async Task<int> InsertAsync(PermissionsDTO value)
        {
            var entity = _mapper.Map<Permissions>(value);
            return await _repository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(int id, PermissionsDTO value)
        {
            var entity = _mapper.Map<Permissions>(value);
            entity.Id = id; // تأكد من تعيين الـ Id قبل التحديث
            return await _repository.UpdateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }


        public Task CreateAsync(PermissionsDTO permissionDto)
        {
            throw new NotImplementedException(); // هذا ممكن تحذفه إذا استخدمت InsertAsync
        }

        public Task<int> DeleteAsync<Type>(Type Id)
        {
            throw new NotImplementedException();
        }
    }
}
