using ToyStore.Domain.Entities;

namespace ToyStore.Application.Interfaces
{
    public interface IStoreService
    {
        Task<IList<Store>> GetAllStoresAsync();
        Task<Store> GetStoreById(int id);
        Task AddStoreAsync(Store store);
        Task UpdateStoreAsync(Store store);
        Task DeleteStoreAsync(int id);
    }
}
