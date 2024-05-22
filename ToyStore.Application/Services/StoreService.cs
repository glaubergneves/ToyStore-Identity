using ToyStore.Application.Interfaces;
using ToyStore.Domain.Entities;

namespace ToyStore.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repository;
        public StoreService(IStoreRepository repository)
        {
            _repository = repository;
        }

        public Task AddStoreAsync(Store store)
        {
            return _repository.AddStoreAsync(store);
        }

        public Task DeleteStoreAsync(int id)
        {
            return _repository.DeleteStoreAsync(id);
        }

        public async Task<IList<Store>> GetAllStoresAsync()
        {
            return await _repository.GetAllStoresAsync();
        }

        public async Task<Store> GetStoreById(int id)
        {
            return await _repository.GetStoreById(id);
        }

        public Task UpdateStoreAsync(Store store)
        {
            return _repository.UpdateStoreAsync(store);
        }
    }
}
