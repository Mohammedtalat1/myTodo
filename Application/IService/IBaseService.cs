using Application.Common;


namespace Application.IService
{
    public interface IBaseServices<T> where T : class
    {
        Task<Results<IEnumerable<T>>> GetAllAsync();
        Task<int> InsertAsync(T value);
        Task<int> UpdateAsync(int id, T value);
        Task<int> DeleteAsync<Type>(Type Id);
        Task<Results<T>> GetByIdAsync(int id);
    }
}
