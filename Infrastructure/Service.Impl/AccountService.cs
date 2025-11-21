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
    public class AccountService(IAccountRepository repository, IMapper mapper) : IAccountService
    {
        private readonly IAccountRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<int> DeleteAsync<Type>(Type Id)
        {
            return await _repository.DeleteAsync(Id);
        }

        public async Task<Results<IEnumerable<AccountDTO>>> GetAllAsync()
        {
            return SuccessResult.Success(_mapper.Map<IEnumerable<AccountDTO>>(await _repository.GetAllAsync()));
        }

        public async Task<Results<AccountDTO>> GetByIdAsync(int id)
        {
            return SuccessResult.Success(_mapper.Map<AccountDTO>(await _repository.GetByIdAsync(id)));
        }

        public async Task<int> InsertAsync(AccountDTO value)
        {
            return await _repository.InsertAsync(_mapper.Map<Account>(value));
        }

        public async Task<int> UpdateAsync(int id, AccountDTO value)
        {
            // افترض أن _repository.UpdateAsync يقبل الـ id أيضاً
            var account = _mapper.Map<Account>(value);
            account.Id = id; // تأكد تعيين الـ Id
            return await _repository.UpdateAsync(account);
        }

    }
}

